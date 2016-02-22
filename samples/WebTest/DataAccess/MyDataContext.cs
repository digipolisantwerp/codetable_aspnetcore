using Microsoft.Extensions.OptionsModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Toolbox.DataAccess.Context;
using Toolbox.DataAccess.Options;
using WebTest.Entities;

namespace WebTest.DataAccess
{
    public class MyDataContext : EntityContextBase
    {
        public MyDataContext(IOptions<EntityContextOptions> options) : base(options)
        {
            
        }

        public DbSet<RegistrationType> RegistrationType { get; set; }
    }
}
