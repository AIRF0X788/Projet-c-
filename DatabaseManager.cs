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

    public static void CreateDatabase()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

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

            ExecuteQuery(connection, createUserTableQuery);
            ExecuteQuery(connection, createProductTableQuery);

        }
    }

    public static void AddPerson(string name, string email)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string insertPersonQuery = $"INSERT INTO data (name, email) VALUES ('{name}', '{email}')";

            ExecuteQuery(connection, insertPersonQuery);
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

    public static void AddProduct(string name, string description, string price)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string insertProductQuery = $"INSERT INTO product (name, description, price) VALUES ('{name}', '{description}', '{price}')";

            ExecuteQuery(connection, insertProductQuery);
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

    public static void UpdatePerson(int personId, string newName, string newEmail)
{
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        connection.Open();

        string updatePersonQuery = $"UPDATE data SET name = '{newName}', email = '{newEmail}' WHERE id = {personId}";

        ExecuteQuery(connection, updatePersonQuery);
    }
}

public static void DeletePerson(int personId)
{
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        connection.Open();

        string deletePersonQuery = $"DELETE FROM data WHERE id = {personId}";

        ExecuteQuery(connection, deletePersonQuery);
    }
}

    private static void ExecuteQuery(MySqlConnection connection, string query)
    {
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            command.ExecuteNonQuery();
        }
    }
}
