using System;
using System.Linq;
using Curso.Data;
using Curso.Domain;

namespace Curso.Modulos
{
    public class OutrosBancosDeDados
    {
        public static void SQLLite()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureCreated();

            db.Pessoas.Add(new Pessoa
            {
                Nome = "Mateus",
                Telefone = "99999-1230"
            });

            db.SaveChanges();

            var pessoas = db.Pessoas.ToList();

            foreach (var pessoa in pessoas)
            {
                Console.WriteLine($"Nome: {pessoa.Nome}");
            }
        }

        public static void InMemory()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureCreated();

            db.Pessoas.Add(new Pessoa
            {
                Nome = "Mateus",
                Telefone = "99999-1230"
            });

            db.SaveChanges();

            var pessoas = db.Pessoas.ToList();

            foreach (var pessoa in pessoas)
            {
                Console.WriteLine($"Nome: {pessoa.Nome}");
            }
        }
    }
}