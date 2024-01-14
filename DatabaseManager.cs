using MySql.Data.MySqlClient;

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
    public string Price { get; set; }

    public Product()
    {
        Name = string.Empty;
        Description = string.Empty;
        Price = string.Empty;
    }
}

public class DatabaseManager
{
    private static string connectionString = "Server=localhost;Database=csharp;User=root;Password=;";

    public static async Task CreateDatabaseAsync()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string createUserTableQuery = @"
                CREATE TABLE IF NOT EXISTS data (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(255) NOT NULL,
                    email VARCHAR(255) NOT NULL
                )";

            string createProductTableQuery = @"
                CREATE TABLE IF NOT EXISTS product (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(255),
                    description TEXT,
                    price DECIMAL(10, 2)
                )";

             await ExecuteQueryAsync(connection, createUserTableQuery);
             await ExecuteQueryAsync(connection, createProductTableQuery);

        }
    }

    public static async Task AddPersonAsync(string name, string email)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        await connection.OpenAsync();

        string insertPersonQuery = $"INSERT INTO data (name, email) VALUES ('{name}', '{email}')";

        await ExecuteQueryAsync(connection, insertPersonQuery);
    }
    }

    public static List<Person> GetPeople()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM data";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
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

<<<<<<< HEAD
    public static Person GetPersonById(int id)
    {
        Person person = null;
        using (var connection = new SQLiteConnection("Data Source=db.db;Version=3;"))
        {
                connection.Open();
                var command = new SQLiteCommand("SELECT * FROM data WHERE id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        person = new Person
                        {
                            Id = Convert.ToInt32(reader["id"]),
                        Name = reader["name"].ToString(),
                        Email = reader["email"].ToString()
                    };
                }
            }
        }
        return person;
    }


    public static void AddProduct(string name, string description, string price)
=======
public static async Task AddProductAsync(string name, string description, string price)
{
    using (MySqlConnection connection = new MySqlConnection(connectionString))
>>>>>>> 2893d2b5ef441e14d6baa93d8dfc840a89282a08
    {
        await connection.OpenAsync();
        string insertProductQuery = $"INSERT INTO product (name, description, price) VALUES ('{name}', '{description}', '{price}')";

        await ExecuteQueryAsync(connection, insertProductQuery);
    }
}


    public static List<Product> GetProducts()
    {
        List<Product> products = new List<Product>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM product";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Description = reader["description"].ToString(),
                            Price = reader["price"].ToString(),
                        });
                    }
                }
            }
        }

        return products;
    }

public static async Task UpdatePersonAsync(int personId, string newName, string newEmail)
{
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        await connection.OpenAsync();

        string updatePersonQuery = $"UPDATE data SET name = '{newName}', email = '{newEmail}' WHERE id = {personId}";

        await ExecuteQueryAsync(connection, updatePersonQuery);
    }
}

public static async Task DeletePersonAsync(int personId)
{
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        await connection.OpenAsync();

        string deletePersonQuery = $"DELETE FROM data WHERE id = {personId}";

        await ExecuteQueryAsync(connection, deletePersonQuery);
    }
}


    private static async Task ExecuteQueryAsync(MySqlConnection connection, string query)
{
    using (MySqlCommand command = new MySqlCommand(query, connection))
    {
        await command.ExecuteNonQueryAsync();
    }
}
}
