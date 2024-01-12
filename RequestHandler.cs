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

        if (method == "GET" && path == "/inventory")
        {
            return "HTTP/1.1 200 OK\nContent-Type: text/plain\n\n[Inventaire ici]";
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

        return "HTTP/1.1 404 Not Found\nContent-Type: text/plain\n\nNot Found";
    }
}
