using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace SimpleAPI
{
    class Program
    {
        static List<User> users = new List<User>();
        static List<Product> products = new List<Product>();

        static void Main(string[] args)
        {
            users.Add(new User { UserId = 1, Username = "User1", Password = "pass1", Email = "user1@example.com" });
            products.Add(new Product { ProductId = 1, Name = "Product1", Description = "Description1", Price = 10.0 });

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();

            Console.WriteLine("Serveur démarré, en attente de requêtes...");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();

                string response = HandleRequest(context.Request);

                byte[] buffer = Encoding.UTF8.GetBytes(response);
                context.Response.ContentLength64 = buffer.Length;
                Stream output = context.Response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }

        static string HandleRequest(HttpListenerRequest request)
        {
            string endpoint = request.Url.AbsolutePath.ToLower();

            if (request.HttpMethod == "GET")
            {
                if (endpoint == "/users")
                {
                    return JsonConvert.SerializeObject(users);
                }
                else if (endpoint == "/products")
                {
                    return JsonConvert.SerializeObject(products);
                }
            }
            else if (request.HttpMethod == "POST")
            {
                if (endpoint == "/users")
                {
                    string requestBody;
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        requestBody = reader.ReadToEnd();
                    }
                    User newUser = JsonConvert.DeserializeObject<User>(requestBody);
                    users.Add(newUser);
                    return "Utilisateur ajouté avec succès.";
                }
            }

            return "Endpoint non trouvé ou méthode non autorisée.";
        }
    }

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
