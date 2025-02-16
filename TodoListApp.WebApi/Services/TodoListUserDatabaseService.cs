namespace TodoListApp.WebApi.Services;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TodoListApp.WebApi.Exceptions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Data;
using Entities;
using Generators;
using Models;
using Models.Post;
using Interfaces;

public class TodoListUserDatabaseService : ITodoListUserDatabaseService
{
    private readonly JwtTokenGenerator generator;
    private readonly TodoListUserDbContext dbContext;
    private readonly IMapper mapper;

    public TodoListUserDatabaseService(JwtTokenGenerator generator, TodoListUserDbContext dbContext, IMapper mapper)
    {
        this.generator = generator;
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<TodoListUserModel> Login(TodoListUserLoginModel model)
    {
        var user = await this.dbContext.Users!.FirstOrDefaultAsync(user => user.Login == model.Login);

        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
        {
            throw new ArgumentException("Invalid login or password");
        }

        var refreshToken = this.generator.GenerateToken(user.Login, 7 * 24 * 60);

        user.RefreshToken = refreshToken;

        await this.dbContext.SaveChangesAsync();

        return new TodoListUserModel { AccessToken = this.generator.GenerateToken(user.Login, 5) };
    }

    public async Task Register(TodoListUserPostModel model)
    {
        var user = this.mapper.Map<TodoListUserEntity>(model);
        user.RefreshToken = this.generator.GenerateToken(user.Login, 7 * 24 * 60);

        await this.dbContext.AddAsync(user);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task<TodoListUserModel> RefreshToken(string accessToken)
    {
        var claims = this.generator.GetClaimsFromToken(accessToken);

        var userLogin = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userLogin == null)
        {
            throw new UnauthorizedAccessException();
        }

        var user = await this.dbContext.Users!.FirstOrDefaultAsync(user => user.Login == userLogin);

        if (user == null)
        {
            throw new UnauthorizedAccessException();
        }

        var refreshClaims = this.generator.GetClaimsFromToken(user.RefreshToken);
        var expirationDate = refreshClaims.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

        if (long.TryParse(expirationDate, out var secs) && DateTimeOffset.FromUnixTimeSeconds(secs).UtcDateTime > DateTime.UtcNow)
        {
            var newToken = new TodoListUserModel();
            newToken.AccessToken = this.generator.GenerateToken(userLogin, 5);
            return newToken;
        }

        throw new SessionExpiredException("Session expired");
    }

    public async Task Logout(string? issuer)
    {
        if (issuer == null)
        {
            throw new UnauthorizedAccessException();
        }

        var user = await this.dbContext.Users!.FirstOrDefaultAsync(user => user.Login == issuer);
        if (user == null)
        {
            return;
        }

        user.RefreshToken = string.Empty;

        await this.dbContext.SaveChangesAsync();
    }
}
