using System;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Curso.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data source=MPR\\SQLEXPRESS;Initial Catalog=CursoEF;Integrated Security=true;pooling=false";
            optionsBuilder.UseSqlServer(strConnection)
                          .EnableSensitiveDataLogging();
            //.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}