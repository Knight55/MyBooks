using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyBooks.Dal.Context;
using MyBooks.Dal.Entities;

namespace MyBooks.IdentityServer.Services
{
    public class TestUserService
    {
        private readonly ILogger<TestUserService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public TestUserService(
            ILogger<TestUserService> logger,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async void AddUsers()
        {
            var alice = await _userManager.FindByNameAsync("alice");
            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "alice"
                };
                var result = await _userManager.CreateAsync(alice, "Pass123$");
                if (!result.Succeeded)
                {
                    _logger.LogError(
                        $"User {alice.UserName} cannot be created: {result.Errors.First().Description}");
                    return;
                }

                result = await _userManager.AddClaimsAsync(alice, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address,
                        @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }",
                        IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                });
                if (!result.Succeeded)
                {
                    _logger.LogError(
                        $"Claims cannot be added to user {alice.UserName}: {result.Errors.First().Description}");
                    return;
                }

                _logger.LogInformation($"User {alice.UserName} created successfully!");
            }
            else
            {
                _logger.LogInformation($"User {alice.UserName} already exists!");
            }
        }
    }
}