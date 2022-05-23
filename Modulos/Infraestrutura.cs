using System.Linq;
using Curso.Data;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.Modulos
{
    public static class Infraestrutura
    {
        public static void ExecutarEstrategiaResiliencia()
        {
            using var db = new ApplicationContext();

            var strategy = db.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var transaction = db.Database.BeginTransaction();

                db.Departamentos.Add(new Departamento { Descricao = "Departamento Transacao" });
                db.SaveChanges();

                transaction.Commit();
            });

        }


        public static void TempoComandoGeral()
        {
            using var db = new ApplicationContext();

            db.Database.SetCommandTimeout(10);

            db.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:07';SELECT 1");
        }

        public static void HabilitandoBatchSize()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            for (var i = 0; i < 50; i++)
            {
                db.Departamentos.Add(
                    new Departamento
                    {
                        Descricao = "Departamento " + i
                    });
            }

            db.SaveChanges();
        }

        public static void DadosSensiveis()
        {
            using var db = new ApplicationContext();

            var descricao = "Departamento";
            var departamentos = db.Departamentos.Where(p => p.Descricao == descricao).ToArray();
        }

        public static void ConsultarDepartamentos()
        {
            using var db = new ApplicationContext();

            var departamentos = db.Departamentos.Where(p => p.Id > 0).ToArray();
        }
    }

}
