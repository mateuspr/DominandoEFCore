using System;
using System.Linq;
using System.Transactions;
using Curso.Data;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.Modulos
{
    public static class Transacoes
    {
        public static void TransactionScope()
        {
            CadastrarLivro();

            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
            };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                ConsultarAtualizar();
                CadastraLivroEnterprise();
                CadastrarLivroDominandoEFCore();

                scope.Complete();
            }
        }

        public static void ConsultarAtualizar()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {
                var livro = db.Livros.FirstOrDefault(p => p.Id == 1);
                livro.Autor = "Rafael Almeida";
                db.SaveChanges();
            }
        }

        public static void CadastraLivroEnterprise()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {
                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "ASP.NET Core Enterprise Applications",
                        Autor = "Eduardo Pires"
                    });
                db.SaveChanges();
            }
        }

        public static void CadastrarLivroDominandoEFCore()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {
                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Dominando o Entity Framework Core",
                        Autor = "Rafael Almeida"
                    });
                db.SaveChanges();
            }
        }

        public static void SalvarPontoTransacao()
        {
            CadastrarLivro();

            using (var db = new Curso.Data.ApplicationContext())
            {
                using var transacao = db.Database.BeginTransaction();

                try
                {
                    var livro = db.Livros.FirstOrDefault(p => p.Id == 1);
                    livro.Autor = "Rafael Almeida";
                    db.SaveChanges();

                    transacao.CreateSavepoint("desfazer_apenas_insercao");

                    db.Livros.Add(
                        new Livro
                        {
                            Titulo = "ASP.NET Core Enterprise Applications",
                            Autor = "Eduardo Pires"
                        });
                    db.SaveChanges();

                    db.Livros.Add(
                        new Livro
                        {
                            Titulo = "Dominando o Entity Framework Core",
                            Autor = "Rafael Almeida".PadLeft(16, '*')
                        });
                    db.SaveChanges();

                    transacao.Commit();
                }
                catch (DbUpdateException e)
                {
                    transacao.RollbackToSavepoint("desfazer_apenas_insercao");

                    if (e.Entries.Count(p => p.State == EntityState.Added) == e.Entries.Count)
                    {
                        transacao.Commit();
                    }
                }

            }
        }

        public static void ReverterTransacao()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {
                var transacao = db.Database.BeginTransaction();

                try
                {
                    var livro = db.Livros.FirstOrDefault(p => p.Id == 1);
                    livro.Autor = "Rafael Almeida";
                    db.SaveChanges();

                    db.Livros.Add(
                        new Livro
                        {
                            Titulo = "Dominando o Entity Framework Core",
                            Autor = "Rafael Almeida".PadLeft(16, '*')
                        });

                    db.SaveChanges();

                    transacao.Commit();
                }
                catch (Exception)
                {
                    transacao.Rollback();
                }

            }
        }

        public static void GerenciandoTransacaoManualmente()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {
                var transacao = db.Database.BeginTransaction();

                var livro = db.Livros.FirstOrDefault(p => p.Id == 1);
                livro.Autor = "Rafael Almeida";
                db.SaveChanges();

                Console.ReadKey();

                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Dominando o Entity Framework Core",
                        Autor = "Rafael Almeida"
                    });

                db.SaveChanges();

                transacao.Commit();
            }
        }

        public static void ComportamentoPadrao()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {
                var livro = db.Livros.FirstOrDefault(p => p.Id == 1);
                livro.Autor = "Rafael Almeida";

                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Dominando o Entity Framework Core",
                        Autor = "Rafael Almeida"
                    });

                db.SaveChanges();
            }
        }

        static void CadastrarLivro()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Introdução ao Entity Framework Core",
                        Autor = "Rafael"
                    });

                db.SaveChanges();
            }
        }

    }
}