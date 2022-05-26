using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Curso.Data;
using Curso.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Curso.Modulos
{
    public static class Consultas
    {
        /*
        public static void DivisaoDeConsulta()
        {
            using var db = new Curso.Data.ApplicationContext();
            //Setup(db);

            var departamentos = db.Departamentos
                .Include(p => p.Funcionarios)
                .Where(p => p.Id < 3)
                //.AsSplitQuery()
                .AsSingleQuery()
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descrição: {departamento.Descricao}");
                foreach (var funcionario in departamento.Funcionarios)
                {
                    Console.WriteLine($"\tNome: {funcionario.Nome}");
                }
            }
        }

        public static void EntendendoConsulta1NN1()
        {
            using var db = new Curso.Data.ApplicationContext();
            //Setup(db);

            var funcionarios = db.Funcionarios
                .Include(p => p.Departamento)
                .ToList();


            foreach (var funcionario in funcionarios)
            {
                Console.WriteLine($"Nome: {funcionario.Nome} / Departamento: {funcionario.Departamento.Descricao}");
            }

            var departamentos = db.Departamentos
                .Include(p => p.Funcionarios)
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descrição: {departamento.Descricao}");
                foreach (var funcionario in departamento.Funcionarios)
                {
                    Console.WriteLine($"\tNome: {funcionario.Nome}");
                }
            
    }

    public static void ConsultaComTAG()
    {
        using var db = new ApplicationContext();
        //Setup(db);

        var departamentos = db.Departamentos
            .TagWith(@"Estou enviando um comentario para o servidor")
            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine($"Descrição: {departamento.Descricao}");
        }
    }

    public static void ConsultaInterpolada()
    {
        using var db = new Curso.Data.ApplicationContext();
        //Setup(db);

        var id = 1;
        var departamentos = db.Departamentos
            .FromSqlInterpolated($"SELECT * FROM Departamentos WHERE Id>{id}")
            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine($"Descrição: {departamento.Descricao}");
        }
    }

    public static void ConsultaParametrizada()
    {
        using var db = new ApplicationContext();
        //Setup(db);

        var id = new SqlParameter
        {
            Value = 1,
            SqlDbType = SqlDbType.Int
        };

        var departamentos = db.Departamentos
            .FromSqlRaw("SELECT * FROM Departamentos WHERE Id>{0}", id)
            .Where(p => !p.Excluido)
            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine($"Descrição: {departamento.Descricao}");
        }
    }

    public static void ConsultaProjetada()
    {
        using var db = new Curso.Data.ApplicationContext();
        Setup(db);

        var departamentos = db.Departamentos
            .Where(p => p.Id > 0)
            .Select(p => new { p.Descricao, Funcionarios = p.Funcionarios.Select(f => f.Nome) })
            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine($"Descrição: {departamento.Descricao}");

            foreach (var funcionario in departamento.Funcionarios)
            {
                Console.WriteLine($"\t Nome: {funcionario}");
            }
        }
    }

    public static void IgnoreFiltroGlobal()
    {
        using var db = new Curso.Data.ApplicationContext();
        Setup(db);

        var departamentos = db.Departamentos.IgnoreQueryFilters()
                                            .Where(p => p.Id > 0)
                                            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine($"Descrição: {departamento.Descricao} \t Excluido: {departamento.Excluido}");
        }
    }

    public static void FiltroGlobal()
    {
        using var db = new Curso.Data.ApplicationContext();
        //Setup(db);

        var departamentos = db.Departamentos.Where(p => p.Id > 0)
                                            .ToList();

        foreach (var departamento in departamentos)
        {
            Console.WriteLine($"Descrição: {departamento.Descricao} \t Excluido: {departamento.Excluido}");
        }
    }

    static void Setup(ApplicationContext db)
    {
        if (db.Database.EnsureCreated())
        {
            db.Departamentos.AddRange(
                new Departamento
                {
                    //Ativo = true,
                    Descricao = "Departamento 01",
                    Funcionarios = new List<Funcionario>
                    {
                            new Funcionario
                            {
                                Nome = "Rafael Almeida",
                                CPF = "99999999911",
                                RG= "2100062"
                            }
                    },
                    Excluido = true
                },
                new Departamento
                {
                    //Ativo = true,
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
    */
    }
}