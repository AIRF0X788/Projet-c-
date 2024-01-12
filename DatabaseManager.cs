using System.Data.SQLite;
using System.Text.Json;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}
public class DatabaseManager
{

    private string connectionString = "Data Source=db.db;Version=3;";
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

    public string GetProducts()
    {
        List<Product> products = new List<Product>();
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM product";
            using (var command = new SQLiteCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"].ToString(),
                            Price = Convert.ToDouble(reader["price"])
                        });
                    }
                }
            }
        }
        return JsonSerializer.Serialize(products);
    }

    public string AddProduct(Product product)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = "INSERT INTO product (name, description, price) VALUES (@Name, @Description, @Price)";
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.ExecuteNonQuery();
            }
        }
        return "Product added";
    }

    public string UpdateProduct(Product product)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = "UPDATE product SET name = @Name, description = @Description, price = @Price WHERE id = @Id";
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.ExecuteNonQuery();
            }
        }
        return "Product updated";
    }

    public string DeleteProduct(int productId)
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string sql = "DELETE FROM product WHERE id = @Id";
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", productId);
                command.ExecuteNonQuery();
            }
        }
        return "Product deleted";
    }


}

