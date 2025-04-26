using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConstrucManagement.Domain.Construction;


namespace ConstrucManagement.Frontend.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Proyecto> Proyectos { get; set; } = default!;
    }
}
