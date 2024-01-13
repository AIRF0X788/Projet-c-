using System.Data.SQLite;


public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Person()
    {
        Name = string.Empty; 
        Email = string.Empty; 
    }

}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    

    public Product()
    {
        Name = string.Empty; 
        Description = string.Empty; 
        Price = 0;
    }
}

public class DatabaseManager
{
    public static void CreateDatabase()
    {
        string databasePath = "db.db"; 

        if (!File.Exists(databasePath))
    {

        SQLiteConnection.CreateFile(databasePath);
        }

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

public static void AddPerson(string name, string email)
    {
        using (SQLiteConnection connection = new SQLiteConnection($"Data Source=db.db;Version=3;"))
        {
            connection.Open();

            string insertPersonQuery = $"INSERT INTO data (name, email) VALUES ('{name}', '{email}')";

            ExecuteQuery(connection, insertPersonQuery);
        }
    }

    private static void ExecuteQuery(SQLiteConnection connection, string query)
    {
        using (SQLiteCommand command = new SQLiteCommand(query, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    
    public static List<Person> GetPeople()
    {
        using (SQLiteConnection connection = new SQLiteConnection($"Data Source=db.db;Version=3;"))
        {
            connection.Open();

            string query = "SELECT * FROM data";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    List<Person> people = new List<Person>();

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string name = Convert.ToString(reader["name"]);
                        string email = Convert.ToString(reader["email"]);



                        people.Add(new Person { Id = id, Name = name, Email = email });

                    }

                    return people;
                }
            }
        }
    }

    public static void AddProduct(string name, string description, int price)
    {
        using (var connection = new SQLiteConnection("Data Source=db.db;Version=3;"))
        {
            connection.Open();
                var command = new SQLiteCommand(connection)
                {
                    CommandText = $"INSERT INTO data (name, email) VALUES ('{name}', '{description}', '{price}')"
                };
            command.ExecuteNonQuery();
        }
    }
    public static List<Product> GetProducts()
    {
        List<Product> products = new List<Product>();
        using (var connection = new SQLiteConnection("Data Source=db.db;Version=3;"))
        {
            connection.Open();
            var command = new SQLiteCommand("SELECT * FROM product", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Price = Convert.ToInt32(reader["price"])
                    });
                }
            }
        }
        return products;
    }
}


