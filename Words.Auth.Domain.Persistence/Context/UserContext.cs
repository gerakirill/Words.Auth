using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Words.Auth.Domain.Persistence.Models;

namespace Words.Auth.Domain.Persistence.Context
{
    public class UserContext(DbContextOptions<UserContext> options) : IdentityDbContext<GameUser>(options);
}
