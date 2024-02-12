using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IdentityServer4.Extensions;
using Words.Auth.Domain.Persistence.Models;

namespace Words.Auth.Domain.Persistence.IdentityManager
{
    public class IdentityManager(
        UserManager<GameUser> userManager,
        IUserClaimsPrincipalFactory<GameUser> claimsFactory) : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(sub);
            var principal = await claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.BirthDate, user.DoB.ToShortDateString()));
            claims.Add(new Claim(JwtClaimTypes.NickName, user.NickName));
            //claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            //claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            claims.Add(new Claim(JwtClaimTypes.Locale, user.CountryCode));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
