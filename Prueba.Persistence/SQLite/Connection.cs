using System;
using System.Data.SQLite;
using System.IO;

namespace Prueba.Persistence
{
    public class Connection
    {
        private const string DBName = "database.sqlite";
        private static bool IsDbRecentlyCreated = false;

        public static void Up()
        {
            // Crea la base de datos y registra usuario solo una vez
            if (!File.Exists(Path.GetFullPath(DBName)))
            {
                SQLiteConnection.CreateFile(DBName);
                IsDbRecentlyCreated = true;
            }

            using var ctx = GetInstance();
            if (IsDbRecentlyCreated)
            {
                string query = @"CREATE TABLE IF NOT EXISTS products 
                        (id INTEGER PRIMARY KEY AUTOINCREMENT, code VARCHAR(20) NOT NULL UNIQUE, name VARCHAR(50) NOT NULL);";

                using var command = new SQLiteCommand(query, ctx);
                command.ExecuteNonQuery();

            }
        }

        public static SQLiteConnection GetInstance()
        {
            var db = new SQLiteConnection(
                string.Format("Data Source={0};Version=3;", DBName)
            );

            db.Open();
            return db;
        }
    }
}