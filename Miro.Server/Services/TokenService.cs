using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Miro.Server.Entities;
using Miro.Server.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService<T> : ITokenService<T> where T : class
{
    private readonly string _secretKey;
    private readonly DbContext _dbContext;
    private readonly IRepository<User> _repository;

    public TokenService(string secretKey,DbContext dbContext,IRepository<User> repository)
    {
        _secretKey = secretKey;
        _dbContext = dbContext;
        _repository = repository;
    }

    public async Task<T> GetByTokenAsync(string token)
    {
 
        var userId = DecodeToken(token);
        return null;
    }

    public async Task UpdateTokenAsync(T user, string newToken)
    {
        if (user is User userModel)
        {
            userModel.SessionToken = newToken;

            await _repository.UpdateAsync(userModel).ConfigureAwait(false);
        }
        else
        {

        }
    }

    public string GenerateToken(T user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, GetUserIdentifier(user))
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private int DecodeToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
        var jwtToken = (JwtSecurityToken)validatedToken;
        return int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    private string GetUserIdentifier(T user)
    {
        var userId = typeof(T).GetProperty("Id")?.GetValue(user)?.ToString();
        return userId ?? string.Empty;
    }
}