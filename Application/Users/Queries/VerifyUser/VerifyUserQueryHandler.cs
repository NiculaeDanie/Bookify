using Application.Abstract;
using Bookify.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.VerifyUser
{
    public class VerifyUserQueryHandler: IRequestHandler<VerifyUserQuery, JwtSecurityToken>
    {
        private readonly IUserRepository _unitOfWork;
        private readonly IConfiguration _configuration;
        public VerifyUserQueryHandler(IUserRepository unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<JwtSecurityToken> Handle(VerifyUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetByName(request.email);
            if (await _unitOfWork.CheckPassword(user, request.password))
            {
                var userRoles = await _unitOfWork.GetRoles(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole.Value));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                return new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
            }
            return null;
        }
    }
}
