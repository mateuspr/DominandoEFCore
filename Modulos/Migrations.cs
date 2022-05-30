using System;
using System.Linq;
using Curso.Data;
using Microsoft.EntityFrameworkCore;

namespace Curso.Modulos
{
    public class Migrations : Base
    {
        //-p Projeto
        //-v Verbos
        //-i Idempotente

        //Recuperar Todas Migrations pelo prompt
        //dotnet ef migrations list --context ApplicationContext

        //Adicionar Migrations
        //dotnet ef migrations add IncluirRGFuncionario --context ApplicationContext

        //Desfazer Migrations (Tudo que for aplicado até "NomeMigracao")
        //dotnet ef database update NomeMigracao --context ApplicationContext

        //Excluir Migration (Arquivo)
        //dotnet ef migrations remove --context ApplicationContext

        //Atualizar o banco
        //dotnet ef database update --context ApplicationContext

        //Gerar Script
        //dotnet ef migrations script --context ApplicationContext -o .\MeuPrimeiroScript.sql -i

        //Engenharia Reversa
        //dotnet ef dbcontext scaffold "StringConexao" Microsoft.EntityFrameworkCore.SqlServer --table Pessoas --use-database-names --data-annotations --context-dir .\Contexto --output-dir .\Entidades --namespace Meu.NameSpace --context-namespace Meu.NameSpace.Contexto
        public static void ListarMigracoes()
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

        public static void AplicarMigracaoEmTempoDeExecucao()
        {
            //EnsureDeleted();
            using var db = new ApplicationContext();
            db.Database.Migrate();
        }

        public static void ScriptGeralDoBancoDeDados()
        {
            using var db = new ApplicationContext();
            var script = db.Database.GenerateCreateScript();

            Console.WriteLine(script);
        }
    }
}