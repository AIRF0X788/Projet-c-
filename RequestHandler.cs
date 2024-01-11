using System;

public class RequestHandler
{
    public static string ProcessRequest(string request)
    {
        
        var lines = request.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        var requestLine = lines.Length > 0 ? lines[0] : string.Empty;
        var tokens = requestLine.Split(' ');
        var method = tokens.Length > 0 ? tokens[0] : string.Empty;
        var path = tokens.Length > 1 ? tokens[1] : string.Empty;

        // Page racine
        if (method == "GET" && path == "/")
        {
           
            return "HTTP/1.1 200 OK\nContent-Type: text/plain\n\n[Racine]";
        }

        // Logique de traitement de la requête
        if (method == "GET" && path == "/inventory")
        {
            // Pour récupérer l'inventaire
            return "HTTP/1.1 200 OK\nContent-Type: text/plain\n\n[Inventaire ici]";
        }
        else if (method == "POST" && path == "/inventory")
        {
            //  Pour ajouter un nouvel élément à l'inventaire
            return "HTTP/1.1 200 OK\nContent-Type: text/plain\n\n[Confirmation d'ajout]";
        }
        // Faire pour PUT, DELETE etc

        return "HTTP/1.1 404 Not Found\nContent-Type: text/plain\n\nNot Found";

        
    }
}
