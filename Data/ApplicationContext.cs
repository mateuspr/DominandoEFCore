using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Curso.Configurations;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace Curso.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly StreamWriter _writer = new StreamWriter("LogEFCore.txt", append: true);
        public DbSet<Dictionary<string, object>> Configuracoes => Set<Dictionary<string, object>>("Configuracoes");

        #region Domain
        /*public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Conversor> Conversores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Ator> Atores { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Instrutor> Instrutores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Atributo> Atributos { get; set; }
        public DbSet<Aeroporto> Aeroportos { get; set; }*/
        public DbSet<Funcao> Funcoes { get; set; }

        #endregion


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
                          .EnableSensitiveDataLogging()
                          //.EnableDetailedErrors()
                          .AddInterceptors(new Interceptadores.InterceptadorPersistencia())
                          .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Modelo de Dados
            //Configurar Filtro Global
            //modelBuilder.Entity<Departamento>().HasQueryFilter(p => !p.Excluido);

            //Desconsiderar Case Sensitive e Ascentuação na pesquisa
            //modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI"); //Global
            //RAFAEL -> rafael || Jõao -> Joao

            /*/modelBuilder.Entity<Departamento>()
                        .Property(p => p.Descricao)
                        .UseCollation("SQL_Latin1_General_CP1_CS_AS");//Somente na Propriedade*/

            //Sequencia Customizada
            /*modelBuilder.HasSequence<int>("MinhaSequencia", "sequencias")
                        .StartsAt(1)
                        .IncrementsBy(2)
                        .HasMin(1)
                        .HasMax(10)
                        .IsCyclic();

            modelBuilder.Entity<Departamento>()
                        .Property(p => p.Id)
                        .HasDefaultValueSql("NEXT VALUE FOR sequencias.MinhaSequencia");*/

            //Criar Indices
            /*modelBuilder.Entity<Departamento>()
                        .HasIndex(p => new { p.Descricao, p.Ativo })
                        .HasDatabaseName("idx_meu_indice_composto")
                        .HasFilter("Descricao IS NOT NULL")
                        .HasFillFactor(80)
                        .IsUnique();*/

            //Gravar dados iniciais
            /*modelBuilder.Entity<Estado>()
                        .HasData(new[]
                        {
                            new Estado { Id = 1, Nome = "São Paulo"},
                            new Estado { Id = 2, Nome = "Sergipe"}
                        });*/

            //Schema
            /*modelBuilder.HasDefaultSchema("cadastros");
            modelBuilder.Entity<Estado>().ToTable("Estados", "SegundoEsquema");*/


            //Converter Valores
            //var conversao = new ValueConverter<Versao, string>(p => p.ToString(), p => (Versao)Enum.Parse(typeof(Versao), p));
            //var conversao1 = new EnumToStringConverter<Versao>();
            /*modelBuilder.Entity<Conversor>()
                        .Property(p => p.Versao)
                        .HasConversion(conversao1)
                        .HasConversion(conversao)
                        .HasConversion(p=>p.ToString(), p=> (Versao)Enum.Parse(typeof(Versao), p))
                        .HasConversion<string>();*/

            /*modelBuilder.Entity<Conversor>()
                        .Property(p => p.Status)
                        .HasConversion(new Curso.Conversores.ConversorCustomizado());*/

            //Propriedade de Sombra
            /*modelBuilder.Entity<Departamento>()
                        .Property<DateTime>("UltimaAtualizacao");*/


            //modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            modelBuilder.SharedTypeEntity<Dictionary<string, object>>("Configuracoes", b =>
            {
                b.Property<int>("Id");

                b.Property<string>("Chave")
                    .HasColumnType("VARCHAR(40)")
                    .IsRequired();

                b.Property<string>("Valor")
                    .HasColumnType("VARCHAR(255)")
                    .IsRequired();
            });
            #endregion

            #region Funções
            modelBuilder.Entity<Funcao>(conf =>
            {
                conf.Property<string>("PropriedadeSombra")
                    .HasColumnType("VARCHAR(100)")
                    .HasDefaultValueSql("'Teste'");
            });
            #endregion
        }

        public override void Dispose()
        {
            base.Dispose();
            _writer.Dispose();
        }
    }
}