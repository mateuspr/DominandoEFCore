using Curso.Data;

namespace Curso.Modulos
{
    public class Base
    {
        public static void EnsureCreated()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();
        }

        public static void EnsureDeleted()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
        }
    }
}