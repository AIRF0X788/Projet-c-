using System;
using System.Data.SQLite;
using System.IO;

public class DatabaseManager
{
    public static void CreateDatabase()
    {
        string databasePath = "db.db"; 

        SQLiteConnection.CreateFile(databasePath);

        using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
        {
            connection.Open();

            string createUserTableQuery = @"
                CREATE TABLE IF NOT EXISTS data (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    email TEXT NOT NULL
                )";

            string createAddressTableQuery = @"
                CREATE TABLE IF NOT EXISTS address (
                    id INTEGER PRIMARY KEY,
                    user_id INTEGER,
                    street TEXT,
                    city TEXT,
                    postal_code TEXT
                )";

            string createProductTableQuery = @"
                CREATE TABLE IF NOT EXISTS product (
                    id INTEGER PRIMARY KEY,
                    name TEXT,
                    description TEXT,
                    price REAL
                )";

            string createCartTableQuery = @"
                CREATE TABLE IF NOT EXISTS cart (
                    id INTEGER PRIMARY KEY,
                    user_id INTEGER,
                    product_id INTEGER,
                    quantity INTEGER
                )";

            string createCommandTableQuery = @"
                CREATE TABLE IF NOT EXISTS command (
                    id INTEGER PRIMARY KEY,
                    user_id INTEGER,
                    product_id INTEGER,
                    quantity INTEGER,
                    status TEXT
                )";

            string createInvoicesTableQuery = @"
                CREATE TABLE IF NOT EXISTS invoices (
                    id INTEGER PRIMARY KEY,
                    user_id INTEGER,
                    command_id INTEGER,
                    total_amount REAL
                )";

            string createPhotoTableQuery = @"
                CREATE TABLE IF NOT EXISTS photo (
                    id INTEGER PRIMARY KEY,
                    user_id INTEGER,
                    product_id INTEGER,
                    file_path TEXT
                )";

            string createRateTableQuery = @"
                CREATE TABLE IF NOT EXISTS rate (
                    id INTEGER PRIMARY KEY,
                    user_id INTEGER,
                    product_id INTEGER,
                    rating INTEGER
                )";

            string createPaymentTableQuery = @"
                CREATE TABLE IF NOT EXISTS payment (
                    id INTEGER PRIMARY KEY,
                    user_id INTEGER,
                    method_name TEXT,
                    iban TEXT,
                    card_number TEXT
                )";

            ExecuteQuery(connection, createUserTableQuery);
            ExecuteQuery(connection, createAddressTableQuery);
            ExecuteQuery(connection, createProductTableQuery);
            ExecuteQuery(connection, createCartTableQuery);
            ExecuteQuery(connection, createCommandTableQuery);
            ExecuteQuery(connection, createInvoicesTableQuery);
            ExecuteQuery(connection, createPhotoTableQuery);
            ExecuteQuery(connection, createRateTableQuery);
            ExecuteQuery(connection, createPaymentTableQuery);
        }
    }



    private static void ExecuteQuery(SQLiteConnection connection, string query)
    {
        using (SQLiteCommand command = new SQLiteCommand(query, connection))
        {
            command.ExecuteNonQuery();
        }
    }
}

