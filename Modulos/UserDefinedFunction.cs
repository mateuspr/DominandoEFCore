using System;
using System.Linq;
using Curso.Data;
using Curso.Domain;
using Curso.Funcoes;
using Microsoft.EntityFrameworkCore;

namespace Curso.Modulos
{
    public class UserDefinedFunction
    {
        /*public static void DateDIFF()
        {
            CadastrarLivro();

            using var db = new ApplicationContext();

            //var resultado = db.Livros.Select(p=>  EF.Functions.DateDiffDay(p.CadastradoEm, DateTime.Now));
                

            var resultado = db
                .Livros
                .Select(p => MinhasFuncoes.DateDiff("DAY", p.CadastradoEm, DateTime.Now));

            foreach (var diff in resultado)
            {
                Console.WriteLine(diff);
            }
        }

        public static void FuncaoDefinidaPeloUsuario()
        {
            CadastrarLivro();

            using var db = new ApplicationContext();

            db.Database.ExecuteSqlRaw(@"
                CREATE FUNCTION ConverterParaLetrasMaiusculas(@dados VARCHAR(100))
                RETURNS VARCHAR(100)
                BEGIN
                    RETURN UPPER(@dados)
                END");


            var resultado = db.Livros.Select(p => MinhasFuncoes.LetrasMaiusculas(p.Titulo));
            foreach (var parteTitulo in resultado)
            {
                Console.WriteLine(parteTitulo);
            }
        }

        public static void FuncaoLEFT()
        {
            CadastrarLivro();

            using var db = new ApplicationContext();

            var resultado = db.Livros.Select(p => MinhasFuncoes.Left(p.Titulo, 10));
            foreach (var parteTitulo in resultado)
            {
                Console.WriteLine(parteTitulo);
            }
        }

        static void CadastrarLivro()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Introdução ao Entity Framework Core",
                        Autor = "Rafael",
                        CadastradoEm = DateTime.Now.AddDays(-2)
                    });

                db.SaveChanges();
            }
        }
        */
    }
}