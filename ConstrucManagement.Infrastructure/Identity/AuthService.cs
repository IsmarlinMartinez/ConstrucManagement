using ConstrucManagement.Domain.Entities.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Infrastructure.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(AuthRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IJwtGenerator jwtGenerator,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _logger = logger;
        }

        public async Task<AuthResponse> LoginAsync(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("El usuario no existe");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                throw new Exception("Credenciales incorrectas");
            }

            return new AuthResponse
            {
                Id = user.Id,
                Token = await _jwtGenerator.GenerateToken(user),
                Email = user.Email,
                UserName = user.UserName
            };
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("El email ya está registrado");
            }

            var newUser = new Usuario
            {
                Email = request.Email,
                NombreCompleto = request.NombreCompleto,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
            {
                throw new Exception($"{result.Errors.First().Description}");
            }

            return new RegistrationResponse
            {
                UserId = newUser.Id
            };
        }
    }
}
