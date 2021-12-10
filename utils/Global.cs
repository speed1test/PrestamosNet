using System;
namespace Prestamos.Utils

{
    public class Global
    {
        //Server=207.246.69.79;Database=PrestamosDb;User Id=sa;password=Contraseña123*;Trusted_Connection=False;MultipleActiveResultSets=true;
        public static string connectionString = @"Server=207.246.69.79;Database=PrestamosDb;Uid=sa;Password=Contraseña123*";
        public static string getConnectionString()
        {
            return connectionString;
        }
    }
}