using Microsoft.EntityFrameworkCore;

namespace react_net_crud_be.Models
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }   
    }
}
