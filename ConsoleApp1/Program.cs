// See https://aka.ms/new-console-template for more information

using ConsoleApp1;
using ConsoleApp1.Data;
using ConsoleApp1.Data.InterfaceRepository;
using ConsoleApp1.Model;
using ConsoleApp1.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

#region lancement services

// Charger la configuration manuellement
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(@"C:\Users\Théotim\RiderProjects\ConsoleApp1\ConsoleApp1\appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<SchoolDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // On enregistre notre service applicatif
        services.AddTransient<DbConnection>();
        
        services.AddTransient<IPersonRepository, PersonRepository>();
    })
    .Build();

using var scope = host.Services.CreateScope();
IPersonRepository personRepository = scope.ServiceProvider.GetRequiredService<IPersonRepository>();

#endregion

#region  CSV

String path = configuration.GetRequiredSection("CSVFiles")["ConsoleApp1"];

List<Profil> persons = new List<Profil>(); 

var lignes = File.ReadAllLines(path);

for (int i = 1; i < lignes.Length; i++)
{
    String line = lignes[i];
    Profil person = new Profil();
    
    person.Lastname = line.Split(',')[1];
    person.Firstname = line.Split(',')[2];
    person.Birthdate = DateTimeUtils.ConvertToDateTime(line.Split(',')[3]);
    person.Size = Int32.Parse(line.Split(',')[5]);
    
    List<String> details = line.Split(',')[4].Split(';').ToList();
    
    person.Details.Add(new Detail(details[0], int.Parse(details[1]), details[2]));
    
    persons.Add(person);
}

Classe maClasse = new Classe();
maClasse.Level = "B2";
maClasse.Name = "B2 C#";
maClasse.School = "SupDeVinci";
maClasse.Persons = persons.ToList();

#endregion

DbConnection dbConnectionService = scope.ServiceProvider.GetRequiredService<DbConnection>();
// dbConnectionService.SaveFullClasse(maClasse);
List<Profil> personsDb = personRepository.GetAllEthan();

foreach (var person in personsDb)
{
    Console.WriteLine("Il y a " + personsDb.Count + " personnes qui s'appelles "+person.Firstname + " et habite à "
                      + person.Details.First().City);
}

#region renseigne à la main

// Person person1 = new Person();
// Person person2 = new Person();
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
// double tailleMoyenne = persons.Average(person => person.Value.Size);
// double tailleMoyenneMetre = Math.Floor(tailleMoyenne) / 100;
//
// Dictionary<int, Person> tallerPersons = persons.Where(person => person.Value.Size > tailleMoyenne)
//     .ToDictionary(person => person.Key, person => person.Value);
//
// Console.WriteLine($"Il y a {tallerPersons.Count.ToString()} personnes qui sont plus grandes que la moyenne " +
//                   $"de la classe qui est de {tailleMoyenneMetre} mètre");
#endregion

#region boucle affiche toute la classe

// foreach (KeyValuePair<int, Person> person in persons)
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