using System.Text;

public class RequestHandler
{
    public static string ProcessRequest(string request)
    {
        var lines = request.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        var requestLine = lines.Length > 0 ? lines[0] : string.Empty;
        var tokens = requestLine.Split(' ');
        var method = tokens.Length > 0 ? tokens[0] : string.Empty;
        var path = tokens.Length > 1 ? tokens[1] : string.Empty;

        if (method == "GET" && path == "/")
        {
            return "HTTP/1.1 200 OK\nContent-Type: text/html\n\n" + File.ReadAllText("index.html");
        }

        if (method == "GET" && path == "/user")
        {
            return "HTTP/1.1 200 OK\nContent-Type: text/html\n\n" + File.ReadAllText("user.html");
        }

        if (method == "GET" && path == "/product")
        {
            return "HTTP/1.1 200 OK\nContent-Type: text/html\n\n" + File.ReadAllText("product.html");
        }

        else if (method == "POST" && path == "/api/inventory")
        {
            var body = lines[lines.Length - 1]; 
            var product = body.Split('&');
            var name = product[0].Split('=')[1];
            var description = product[1].Split('=')[1];
            var price = product[2].Split('=')[1];


            DatabaseManager.AddProduct(name, description, price);

            return "HTTP/1.1 200 OK\nContent-Type: text/plain\n\n[Confirmation d'ajout]";
        }

        if (method == "GET" && path == "/inventory")
        {
            var products = DatabaseManager.GetProducts();
            var html = GenerateHtmlTable2(products);

            return $"HTTP/1.1 200 OK\nContent-Type: text/html\n\n{html}";
        }
        else if (method == "POST" && path == "/api/person")
        {
            var body = lines[lines.Length - 1]; 
            var data = body.Split('&');
            var name = data[0].Split('=')[1];
            var email = data[1].Split('=')[1];

            DatabaseManager.AddPerson(name, email);

            return "HTTP/1.1 200 OK\nContent-Type: text/plain\n\n[Confirmation d'ajout]";
        }
        else if (method == "GET" && path == "/people")
        {
            var people = DatabaseManager.GetPeople();
            var html = GenerateHtmlTable(people);

            return $"HTTP/1.1 200 OK\nContent-Type: text/html\n\n{html}";
        }

        

        return "HTTP/1.1 404 Not Found\nContent-Type: text/plain\n\nNot Found";
    }

    private static string GenerateHtmlTable(List<Person> people)
    {
        StringBuilder html = new StringBuilder();
        html.Append("<table border='1'>");
        html.Append("<tr><th>ID</th><th>Name</th><th>Email</th></tr>");

        foreach (var person in people)
        {
            html.Append($"<tr><td>{person.Id}</td><td>{person.Name}</td><td>{person.Email}</td><td><button onclick='editPerson({person.Id})'>Edit</button></td></tr>");
        }

        html.Append("</table>");

        return html.ToString();
    }

    private static string GenerateHtmlTable2(List<Product> products)
    {
        StringBuilder html = new StringBuilder();
        html.Append("<table border='1'>");
        html.Append("<tr><th>ID</th><th>Name</th><th>Description</th><th>Price</th></tr>");

        foreach (var product in products)
        {
            html.Append($"<tr><td>{product.Id}</td><td>{product.Name}</td><td>{product.Description}</td><td>{product.Price}</td></tr>");
        }

        html.Append("</table>");

        return html.ToString();
    }
}
