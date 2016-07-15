using Digipolis.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using WebTest.Entities;

namespace WebTest.DataAccess
{
    public class MyDataContext : EntityContextBase<MyDataContext>
    {
        public MyDataContext(DbContextOptions<MyDataContext> options) : base(options)
    { }

        public DbSet<RegistrationType> RegistrationType { get; set; }
    }
}
