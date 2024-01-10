using System.Data.SQLite;

public class ArticleManager
{
    public static void CreateArticle(Article article)
    {
        string databasePath = "db.db";

        using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
        {
            connection.Open();

            string insertArticleQuery = @"
                INSERT INTO product (name, description, price) 
                VALUES (@Name, @Description, @Price)";

            using (SQLiteCommand command = new SQLiteCommand(insertArticleQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", article.Name);
                command.Parameters.AddWithValue("@Description", article.Description);
                command.Parameters.AddWithValue("@Price", article.Price);

                command.ExecuteNonQuery();
            }
        }
    }

     public static Article? GetArticleById(int id)
{
    string databasePath = "db.db"; 

    using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
    {
        connection.Open();

        string getArticleQuery = @"
            SELECT * FROM product
            WHERE id = @Id";

        using (SQLiteCommand command = new SQLiteCommand(getArticleQuery, connection))
        {
            command.Parameters.AddWithValue("@Id", id);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Article
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = Convert.ToString(reader["name"]) ?? string.Empty,
                        Description = Convert.ToString(reader["description"]) ?? string.Empty,
                        Price = Convert.ToDouble(reader["price"])
                    };
                }
            }
        }

        return null;
    }
}

    public static void UpdateArticle(Article article)
    {
        string databasePath = "db.db";

        using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
        {
            connection.Open();

            string updateArticleQuery = @"
                UPDATE product
                SET name = @Name, description = @Description, price = @Price
                WHERE id = @Id";

            using (SQLiteCommand command = new SQLiteCommand(updateArticleQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", article.Name);
                command.Parameters.AddWithValue("@Description", article.Description);
                command.Parameters.AddWithValue("@Price", article.Price);
                command.Parameters.AddWithValue("@Id", article.Id);

                command.ExecuteNonQuery();
            }
        }
    }

    public static void DeleteArticle(int id)
    {
        string databasePath = "db.db"; 

        using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
        {
            connection.Open();

            string deleteArticleQuery = @"
                DELETE FROM product
                WHERE id = @Id";

            using (SQLiteCommand command = new SQLiteCommand(deleteArticleQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
