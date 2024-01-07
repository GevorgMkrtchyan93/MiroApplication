using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Miro.Server.Entities;
using Miro.Server.Interfaces;
using Miro.Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService<T> : ITokenService<T> where T : class
{
    private readonly string _secretKey;
    private readonly DbContext _dbContext;
    private readonly IRepository<User> _repository;

    public TokenService(string secretKey, DbContext dbContext, IRepository<User> repository)
    {
        _secretKey = secretKey;
        _dbContext = dbContext;
        _repository = repository;
    }

    public async Task<T> GetByTokenAsync(string token)
    {
        var userId = DecodeToken(token);
        try
        {
            var user = await _repository.GetByIdAsync(userId).ConfigureAwait(false);

            if (typeof(T) == typeof(User))
            {
                return user as T;
            }
            else
            {
                throw new InvalidOperationException($"Invalid type '{typeof(T).FullName}'.");
            }
        }
        catch (Exception ex) 
        {
            return null;
        }
    }

    public async Task<bool> UpdateTokenAsync(T user, string newToken)
    {
        if (user is User userModel)
        {
            userModel.SessionToken = newToken;

            await _repository.UpdateAsync(userModel).ConfigureAwait(false);

            return true;
        }
        else
        {
            return false;
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
            new Claim("Id", GetUserIdentifier(user))
        }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private int DecodeToken(string token)
    {
        try
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

            // If you have a specific claim where the user ID is stored, you can retrieve it directly.
            // For example, assuming the user ID is stored in a claim named "UserId":
            var claims = principal.Claims.Select(c => $"{c.Type}: {c.Value}");
            Console.WriteLine($"Decoded Claims: {string.Join(", ", claims)}");
            var userIdClaim = principal.FindFirst("Id");

            if (userIdClaim != null)
            {
                var userId = int.Parse(userIdClaim.Value);
                Console.WriteLine($"Decoded user ID: {userId}");
                return userId;
            }
            else
            {
                throw new InvalidOperationException("User ID claim not found in the token.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Token decoding failed: {ex.Message}");
            throw; // Re-throw the exception if needed.
        }
    }

    private string GetUserIdentifier(T user)
    {
        var userId = typeof(T).GetProperty("Id")?.GetValue(user)?.ToString();
        return userId ?? string.Empty;
    }
}