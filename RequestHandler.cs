using System.Text.Json;
public class RequestHandler
{
    private DatabaseManager dbManager = new DatabaseManager();

    public string HandleRequest(string httpMethod, string path, string requestBody)
    {
        switch (httpMethod)
        {
            case "GET":
                return dbManager.GetProducts();
            case "POST":
                var newProduct = JsonSerializer.Deserialize<Product>(requestBody);
                return dbManager.AddProduct(newProduct);
            case "PUT":
                var productToUpdate = JsonSerializer.Deserialize<Product>(requestBody);
                int idToUpdate = ExtractIdFromPath(path);
                productToUpdate.Id = idToUpdate;
                return dbManager.UpdateProduct(productToUpdate);
            case "DELETE":
                int idToDelete = Convert.ToInt32(path);
                return dbManager.DeleteProduct(idToDelete);
            default:
                return "HTTP/1.1 405 Method Not Allowed\nContent-Type: text/plain\n\nMethod Not Allowed";
        }
    }

    private int ExtractIdFromPath(string path)
    {
        var parts = path.Split('/');
        if(parts.Length > 1 && int.TryParse(parts.Last(), out int id))
        {
            return id;
        }
        else
        {
            throw new ArgumentException("Le chemin d'accès ne contient pas un ID valide.");
        }
    }

}

