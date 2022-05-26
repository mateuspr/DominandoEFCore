using System;
using System.Linq;
using Curso.Data;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.Modulos
{
    public static class Interceptacao
    {

        public static void TesteInterceptacaoSaveChanges()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Funcoes.Add(new Funcao
                {
                    Descricao1 = "Teste"
                });

                db.SaveChanges();
            }
        }

        public static void TesteInterceptacao()
        {
            using (var db = new ApplicationContext())
            {
                var consulta = db.Funcoes
                                 .TagWith("Use NOLOCK")
                                 .FirstOrDefault();

                Console.WriteLine($"Consulta: {consulta?.Descricao1}");
            }
        }

    }
}