using System;
using System.Linq;
using Curso.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Curso.Modulos
{
    public static class Procedures
    {/*
        public static void ConsultaViaProcedure()
        {
            using var db = new ApplicationContext();

            var dep = new SqlParameter("@dep", "Departamento");

            var departamentos = db.Departamentos
                //.FromSqlRaw("EXECUTE GetDepartamentos @dep", dep)
                .FromSqlInterpolated($"EXECUTE GetDepartamentos {dep}")
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine(departamento.Descricao);
            }
        }

        public static void CriarStoredProcedureDeConsulta()
        {
            var criarDepartamento = @"
            CREATE OR ALTER PROCEDURE GetDepartamentos
                @Descricao VARCHAR(50)
            AS
            BEGIN
                SELECT * FROM Departamentos Where Descricao Like @Descricao + '%'
            END        
            ";

            using var db = new ApplicationContext();

            db.Database.ExecuteSqlRaw(criarDepartamento);
        }

        public static void InserirDadosViaProcedure()
        {
            using var db = new ApplicationContext();

            db.Database.ExecuteSqlRaw("execute CriarDepartamento @p0, @p1", "Departamento Via Procedure", true);
        }

        public static void CriarStoredProcedure()
        {
            var criarDepartamento = @"
            CREATE OR ALTER PROCEDURE CriarDepartamento
                @Descricao VARCHAR(50),
                @Ativo bit
            AS
            BEGIN
                INSERT INTO 
                    Departamentos(Descricao, Ativo, Excluido) 
                VALUES (@Descricao, @Ativo, 0)
            END        
            ";

            using var db = new ApplicationContext();

            db.Database.ExecuteSqlRaw(criarDepartamento);
        }
    */
    }
}