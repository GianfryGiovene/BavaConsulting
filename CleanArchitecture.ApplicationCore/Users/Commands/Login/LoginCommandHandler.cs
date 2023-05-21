using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Utilities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanArchitecture.ApplicationCore.Users.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        string token = string.Empty;

        if (!await _unitOfWork.UserRepository.AnyAsync(u => u.Email == request.Email))
            throw new NullReferenceException();

        User? user = await _unitOfWork.UserRepository.GetAsync(u => u.Email == request.Email);

        if (!PswHashingUtil.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Password sbagliata");

        token = this.CreateToken(user);


        if (string.IsNullOrEmpty(token)) throw new Exception();

        return token;
    }


    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
            );
        //var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return $"Bearer {jwt}";
    }
}
