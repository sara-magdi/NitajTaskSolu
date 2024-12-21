using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Nj.DAL
{
    public partial class NjDBContext : IdentityDbContext
    {

        public NjDBContext(DbContextOptions<NjDBContext> options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
