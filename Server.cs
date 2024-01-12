using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

public class Server
{
    public static void Start()
    {
        DatabaseManager.CreateDatabase();
        Console.WriteLine("Database created successfully!");
        TcpListener listener = new TcpListener(IPAddress.Any, 8080);
        listener.Start();
        Console.WriteLine("http://localhost:8080");

        while (true)
        {
            using (var client = listener.AcceptTcpClient())
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                string response = RequestHandler.ProcessRequest(request);
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);

                if (response.Contains("HTTP/1.1 200 OK\nContent-Type: text/plain\n\n[Racine]"))
                {
                    string cheminFichierHtml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "index.html");

                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = cheminFichierHtml,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur : {ex.Message}");
                    }

                    OuvrirPageHtml(cheminFichierHtml);
                }
            }
        }
    }

    private static void OuvrirPageHtml(string cheminFichierHtml)
    {
        try
        {
            Process.Start(cheminFichierHtml);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur : {ex.Message}");
        }
    }
}