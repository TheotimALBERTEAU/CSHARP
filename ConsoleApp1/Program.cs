using ConsoleApp1;
using ConsoleApp1.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Microsoft.EntityFrameworkCore.Design;

#region lancement services

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.Sources.Clear();
        builder.AddConfiguration(dbContext.configuration);
    })
    .ConfigureServices(services =>
    {
        // NpgsqlConnection singleton avec ouverture automatique
        services.AddSingleton(provider =>
        {
            var conn = new NpgsqlConnection(
                configuration.GetConnectionString("DefaultConnection"));
            conn.Open(); // ouverture unique
            return conn;
        });

        // On enregistre notre service applicatif
        services.AddTransient<DbConnection>();
        
        // On enregistre du service SCV
        services.AddTransient<csvService>();
    })
    .Build();

using var scope = host.Services.CreateScope();
DbConnection dbConnectionService = scope.ServiceProvider.GetRequiredService<DbConnection>();

csvService csvRead = scope.ServiceProvider.GetRequiredService<csvService>();

#endregion

List<Profil> persons = csvRead.ReadAllProfils();

Classe maClasse = new Classe();
maClasse.Level = "B2";
maClasse.Name = "B2 C#";
maClasse.School = "SupDeVinci";
maClasse.Persons = persons.ToList();

// await dbConnectionService.init(maClasse);

#region renseigne à la main

// Profil person1 = new Profil();
// Profil person2 = new Profil();
//
// Console.WriteLine("Quelle est le nom de P1 ?");
// person1.Lastname = Console.ReadLine();
// Console.WriteLine("Quelle est le nom de P2 ?");
// person2.Lastname = Console.ReadLine();
//
// Console.WriteLine("Quelle est le prénom de P1 ?");
// person1.Firstname = Console.ReadLine();
// Console.WriteLine("Quelle est le prénom de P2 ?");
// person2.Firstname = Console.ReadLine();
//
// Console.WriteLine("Quelle est votre Date de Naissance de P1 (au format JJ/MM/YYYY) ?");
// String birthDate1 = Console.ReadLine();
// Console.WriteLine("Quelle est votre Date de Naissance P2 (au format JJ/MM/YYYY) ?");
// String birthDate2 = Console.ReadLine();
//
// Console.WriteLine("Quelle est l'adresse de P1 (au format rue,codePostal,Ville) ?");
// String address1 = Console.ReadLine();
// Console.WriteLine("Quelle est l'adresse de P2 (au format rue,codePostal,Ville) ?");
// String address2 = Console.ReadLine();
//
// List<String> listAdress = address1.Split(",").ToList();
// List<String> listAdress2 = address2.Split(",").ToList();
//
// person1.AdressDetails = new Detail(listAdress[0], Int32.Parse(listAdress[1]), listAdress[2]);
// person2.AdressDetails = new Detail(listAdress2[0], Int32.Parse(listAdress2[1]), listAdress2[2]);

    #endregion

#region exercice taille et linq
// double tailleMoyenne = Persons.Average(person => person.Value.Size);
// double tailleMoyenneMetre = Math.Floor(tailleMoyenne) / 100;
//
// Dictionary<int, Profil> tallerPersons = Persons.Where(person => person.Value.Size > tailleMoyenne)
//     .ToDictionary(person => person.Key, person => person.Value);
//
// Console.WriteLine($"Il y a {tallerPersons.Count.ToString()} personnes qui sont plus grandes que la moyenne " +
//                   $"de la classe qui est de {tailleMoyenneMetre} mètre");
#endregion

#region boucle affiche toute la classe

// foreach (KeyValuePair<int, Profil> person in Persons)
// {
// Console.WriteLine($"Bonjour {person.Value.Firstname} {person.Value.Lastname},");
// Console.WriteLine($"tu as {person.Value.getYearsOld().ToString()} ans et tu habites au {person.Value.AdressDetails.Street}" +
//                   $" {person.Value.AdressDetails.ZipCode.ToString()} {person.Value.AdressDetails.City}.");

// }

#endregion

#region Console.WriteLine Exo 1 qui est plus quand des deux

    // if (person1.getYearsOld() > person2.getYearsOld())
// {
//     Console.WriteLine($"{person1.Firstname} {person1.Lastname} est plus agé(e) que {person2.Firstname} {person2.Lastname} de {(person1.getYearsOld() - person2.getYearsOld()).ToString()} an(s)");
// }
// else if (person1.getYearsOld() < person2.getYearsOld())
// {
//     Console.WriteLine($"{person2.Firstname} {person2.Lastname} est plus agé(e) que {person1.Firstname} {person1.Lastname} de {(person2.getYearsOld() - person1.getYearsOld()).ToString()} an(s)");
// }
// else
// {
//     Console.WriteLine($"{person1.Firstname} {person1.Lastname} a le même age que {person2.Firstname} {person2.Lastname}");
// }

    #endregion