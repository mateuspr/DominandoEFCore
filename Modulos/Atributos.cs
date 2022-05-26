using System;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.Modulos
{
    public static class Atributos
    {
        public static void Atributo()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var script = db.Database.GenerateCreateScript();

                Console.WriteLine(script);

                db.Atributos.Add(new Atributo
                {
                    Descricao = "Exemplo",
                    Observacao = "Observacao"
                });

                db.SaveChanges();
            }
        }
    }
}