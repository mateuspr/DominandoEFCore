using System;
using System.Collections.Generic;
using System.Linq;
using Curso.Data;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore
{
    class Program
    {
        static int _count;

        static void Main(string[] args)
        {
            //EnsureCreated();
            //EnsureDeleted();
            //GapEnsureCreated();
            //HealthCheckDatabaseV1();
            //HealthCheckDatabaseV2();

            //warmup
            /*new ApplicationContext().Departamento.AsNoTracking().Any();
            _count = 0;
            GerenciarEstadoDaConexao(false);
            _count = 0;
            GerenciarEstadoDaConexao(true);*/

            //ExecuteSQL();
            //SqlInjection();

            //Migrações
            //AplicarMigracaoEmTempoDeExecucao();
            //ListarMigracoes();

            //ScriptGeralDoBancoDeDados();

            //Tipos de Carregamento
            //CarregamentoAdiantado();
            //CarregamentoExplicito();
            CarregamentoLento();
        }

        #region Migrações
        //Recuperar Todas Migrations pelo prompt
        //dotnet ef migrations list --context ApplicationContext
        //Adicionar Migrations
        //dotnet ef migrations add IncluirRGFuncionario --context ApplicationContext
        //Atualizar o banco
        //dotnet ef database update --context ApplicationContext
        static void ListarMigracoes()
        {
            using var db = new ApplicationContext();
            //var migracoes = db.Database.GetMigrations();//TodasMigrações
            //var migracoes = db.Database.GetAppliedMigrations();//Migrações Aplicadas
            var migracoes = db.Database.GetPendingMigrations();//Migrações Pendentes

            Console.WriteLine($"Total: {migracoes.Count()}");

            foreach (var migracao in migracoes)
            {
                Console.WriteLine($"Migração:{migracao}");
            }
        }

        static void AplicarMigracaoEmTempoDeExecucao()
        {
            EnsureDeleted();
            using var db = new ApplicationContext();
            db.Database.Migrate();
        }

        #endregion

        #region  Entity Framework
        static void ScriptGeralDoBancoDeDados()
        {
            using var db = new ApplicationContext();
            var script = db.Database.GenerateCreateScript();

            Console.WriteLine(script);
        }

        static void EnsureCreated()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();
        }

        static void EnsureDeleted()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
        }

        static void GapEnsureCreated()
        {
            using var db1 = new ApplicationContext();
            using var db2 = new ApplicationContextCidade();

            db1.Database.EnsureCreated();
            db2.Database.EnsureCreated();

            var databaseCreator = db2.GetService<IRelationalDatabaseCreator>();
            databaseCreator.CreateTables();
        }

        static void HealthCheckDatabaseV1()
        {
            using var db = new ApplicationContext();
            try
            {
                //1º Opção
                var connection = db.Database.GetDbConnection();
                connection.Open();

                //2º Opção
                db.Departamentos.AnyAsync();

                Console.WriteLine("Posso me conectar");
            }
            catch (Exception)
            {
                Console.WriteLine("Não posso me conectar");
            }
        }

        static void HealthCheckDatabaseV2()
        {
            using var db = new ApplicationContext();
            var canConnect = db.Database.CanConnect();
            if (canConnect)
            {
                Console.WriteLine("Posso me conectar");
            }
            else
            {
                Console.WriteLine("Não posso me conectar");
            }
        }

        static void GerenciarEstadoDaConexao(bool gerenciarEstadoConexao)
        {
            using var db = new ApplicationContext();
            var time = System.Diagnostics.Stopwatch.StartNew();

            var conexao = db.Database.GetDbConnection();

            conexao.StateChange += (_, __) => ++_count;

            if (gerenciarEstadoConexao) conexao.Open();

            for (int i = 0; i < 200; i++)
            {
                db.Departamentos.AsNoTracking().Any();
            }

            time.Stop();
            var mensagem = $"Tempo: {time.Elapsed.ToString()},{gerenciarEstadoConexao}, Contador: {_count}";

            Console.WriteLine(mensagem);
        }

        static void ExecuteSQL()
        {
            using var db = new ApplicationContext();

            //1º Opção
            using (var cmd = db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT 1";
                cmd.ExecuteNonQuery();
            }

            //2º Opção
            var descricao = "TESTE";
            db.Database.ExecuteSqlRaw("UPDATE Departamentos SET descricao={0} WHERE id=1", descricao);

            //3º Opção
            db.Database.ExecuteSqlInterpolated($"UPDATE Departamentos SET descricao={descricao} WHERE id=1");
        }

        static void SqlInjection()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Departamentos.AddRange(
                new Curso.Domain.Departamento
                {
                    Descricao = "Departamento 01"
                },
                new Curso.Domain.Departamento
                {
                    Descricao = "Departamento 02"
                }
            );

            db.SaveChanges();

            /*var descricao = "Departamento Alterado";
            db.Database.ExecuteSqlRaw("UPDATE Departamentos SET descricao={0} WHERE id=1", descricao);*/

            var descricao2 = "Teste' or 1='1";
            db.Database.ExecuteSqlRaw($"UPDATE Departamentos SET descricao='Ataque SQL Injection' WHERE descricao='{descricao2}'");

            foreach (var departamento in db.Departamentos.AsNoTracking())
            {
                Console.WriteLine($"Id: {departamento.Id}, Descricao: {departamento.Descricao}");
            }
        }
        #endregion

        #region  Tipos de Carregamento
        static void CarregamentoAdiantado()
        {

            using var db = new ApplicationContext();
            SetupTiposCarregamentos(db);

            var departamentos = db
                .Departamentos
                .Include(p => p.Funcionarios);

            foreach (var departamento in departamentos)
            {

                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Departamento: {departamento.Descricao}");

                if (departamento.Funcionarios?.Any() ?? false)
                {
                    foreach (var funcionario in departamento.Funcionarios)
                    {
                        Console.WriteLine($"\tFuncionario: {funcionario.Nome}");
                    }
                }
                else
                {
                    Console.WriteLine($"\tNenhum funcionario encontrado!");
                }
            }
        }

        static void CarregamentoExplicito()
        {
            using var db = new Curso.Data.ApplicationContext();
            SetupTiposCarregamentos(db);

            var departamentos = db
                .Departamentos
                .ToList();

            foreach (var departamento in departamentos)
            {
                if (departamento.Id == 2)
                {
                    //db.Entry(departamento).Collection(p=>p.Funcionarios).Load();
                    db.Entry(departamento).Collection(p => p.Funcionarios).Query().Where(p => p.Id > 2).ToList();
                }

                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Departamento: {departamento.Descricao}");

                if (departamento.Funcionarios?.Any() ?? false)
                {
                    foreach (var funcionario in departamento.Funcionarios)
                    {
                        Console.WriteLine($"\tFuncionario: {funcionario.Nome}");
                    }
                }
                else
                {
                    Console.WriteLine($"\tNenhum funcionario encontrado!");
                }
            }
        }

        static void CarregamentoLento()
        {
            using var db = new Curso.Data.ApplicationContext();
            SetupTiposCarregamentos(db);

            //db.ChangeTracker.LazyLoadingEnabled = false;

            var departamentos = db
                .Departamentos
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Departamento: {departamento.Descricao}");

                if (departamento.Funcionarios?.Any() ?? false)
                {
                    foreach (var funcionario in departamento.Funcionarios)
                    {
                        Console.WriteLine($"\tFuncionario: {funcionario.Nome}");
                    }
                }
                else
                {
                    Console.WriteLine($"\tNenhum funcionario encontrado!");
                }
            }
        }

        static void SetupTiposCarregamentos(ApplicationContext db)
        {
            if (!db.Departamentos.Any())
            {
                db.Departamentos.AddRange(
                    new Departamento
                    {
                        Descricao = "Departamento 01",
                        Funcionarios = new List<Funcionario>
                        {
                            new Funcionario
                            {
                                Nome = "Rafael Almeida",
                                CPF = "99999999911",
                                RG= "2100062"
                            }
                        }
                    },
                    new Departamento
                    {
                        Descricao = "Departamento 02",
                        Funcionarios = new List<Funcionario>
                        {
                            new Funcionario
                            {
                                Nome = "Bruno Brito",
                                CPF = "88888888811",
                                RG= "3100062"
                            },
                            new Funcionario
                            {
                                Nome = "Eduardo Pires",
                                CPF = "77777777711",
                                RG= "1100062"
                            }
                        }
                    });

                db.SaveChanges();
                db.ChangeTracker.Clear();
            }
        }
        #endregion
    }
}
