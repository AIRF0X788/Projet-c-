**Introduction**

Ce projet vise à développer une API en C# déployée sur une VM sous Debian pour gérer les opérations CRUD relatives aux utilisateurs et aux produits.L'API interagit avec une base de données MySQL et est conçue pour être déployée sur un serveur Apache Linux.

**Sommaires**
Introduction
Trello
Setup des serveurs
Aperçu de la Structure du Projet
Utilisation
À propos

**Trello**
Lien : https://trello.com/b/30iBVzT6/projet-csharp

**Setup des serveurs**

Voir le pdf "Docu-setup-serveurs.pdf"

**Aperçu de la Structure du Projet**
***• DatabaseManager.cs***
Gère les opérations et interactions avec la base de données, y compris les opérations CRUD pour les utilisateurs et les produits.

***• Server.cs***
Configure et exécute le serveur HTTP, gérant les requêtes entrantes et les dirigeant vers les gestionnaires appropriés.

***• RequestHandler.cs***
Traite les requêtes HTTP entrantes, extrait les données et délègue les tâches au DatabaseManager pour les opérations de base de données.

***• Program.cs***
Point d'entrée de l'application, initialisant et démarrant le serveur.

***• index.html, user.html, product.html***
Fichiers HTML servant d'interface utilisateur pour interagir avec l'API.

***• Projet c#.sln, ProjetCSGHK.csproj***
Fichiers de solution et de projet pour gérer la structure et les dépendances du projet C#.

**Utilisation**
Pour tester l'API, utilisez un outil tel que Postman ou un navigateur web. L'URL de base pour les requêtes est http://localhost:8080/. L'API prend en charge divers points de terminaison pour la gestion des utilisateurs et des produits.

**Flux de Haut Niveau**
L'API reçoit des requêtes HTTP, qui sont traitées par RequestHandler. Selon le type de requête, les opérations CRUD appropriées sont effectuées par DatabaseManager. Le serveur renvoie ensuite des réponses HTTP basées sur les résultats de ces opérations.

**À Propos**
Le projet est structuré pour fournir une séparation claire des préoccupations, avec des couches distinctes gérant des aspects spécifiques de l'application, de la gestion du serveur aux interactions avec la base de données.
