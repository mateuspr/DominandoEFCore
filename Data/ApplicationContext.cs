using System;
using System.IO;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Curso.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly StreamWriter _writer = new StreamWriter("LogEFCore.txt", append: true);
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data source=MPR\\SQLEXPRESS;Initial Catalog=CursoEF;Integrated Security=true;pooling=false;MultipleActiveResultSets=true";

            //Gravar Log em Arquivo
            /*optionsBuilder.UseSqlServer(strConnection)
                          .LogTo(_writer.WriteLine, LogLevel.Information);*/

            //EnableRetryOnFailure > Por padrão 6x de 30segundos até falhar

            optionsBuilder.UseSqlServer(strConnection)
                          /*.UseSqlServer(strConnection,
                             o => o
                             //.MaxBatchSize(100)
                             //.CommandTimeout(5)
                             //.EnableRetryOnFailure(4, TimeSpan.FromSeconds(10), null)
                             )*/
                          //.UseSqlServer(strConnection, p => p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                          //.UseLazyLoadingProxies()
                          //.EnableSensitiveDataLogging()
                          //.EnableDetailedErrors()
                          .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Departamento>().HasQueryFilter(p => !p.Excluido);
        }

        public override void Dispose()
        {
            base.Dispose();
            _writer.Dispose();
        }
    }
}