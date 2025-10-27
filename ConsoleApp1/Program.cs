namespace ConsoleApp1;

internal abstract class Program
{
    private static void Main()
        
        {
            #region Init CSV
           
            // On initialise le CSV dans une variable data
            var data = new StreamReader(@"C:\Users\Théotim\RiderProjects\ConsoleApp1\ConsoleApp1\Data.csv");
            // On passe la première ligne d'en-tête
            data.ReadLine();
            
            #endregion

            #region Read CSV
            
            // Création du dictionnaire qui contiendra toutes les personnes
            var profiles = new Dictionary<int, Profil>();

            while (data.ReadLine() is { } line)
            {
                var values = line.Split(',');

                var person = new Profil
                {
                    Nom = values[1],
                    Prenom = values[2],
                    Datenaissance = values[3],
                    Taille = int.Parse(values[5]),
                };

                var details = values[4].Split(';');
                person.Address = new Detail(details[0], int.Parse(details[1].Trim()), details[2]);

                profiles.Add(int.Parse(values[0]), person);
            }
            
            #endregion
            
            #region Write Values
            
            /* foreach (var i in profiles.Keys)
            {
                Console.WriteLine($"Bonjour {profiles[i].Prenom} {profiles[i].Nom},");
                Console.WriteLine(
                    $"tu as {profiles[i].YearsOld().ToString()} ans et tu habites au {profiles[i].Address.Rue} {profiles[i].Address.Codepostal} {profiles[i].Address.Ville}.\n");
            } */
            
            #endregion
            
            #region Moyenne + Tall Persons
            
            float moyenne = (float)profiles.Values.Average(p => p.Taille);
            List<Profil> compteurGrandesPersonnes = profiles.Values.Where(personne => personne.Taille > moyenne).ToList();
            // Console.WriteLine($"Il y a {compteurGrandesPersonnes.Count} personnes qui sont plus grandes que la moyenne {moyenne/100:F2} mètre");
            
            #endregion
            
            #region Création classe B2
            
            Classe B2 = new Classe(profiles.Values.ToList(), "Les goats", "Sup de Vinci", 2);
            Console.WriteLine($"Il y a {B2.Students.Count} élèves dans la classe de niveau {B2.Level} ({B2.ClassName}), de l'école : {B2.School}\n");
            
            #endregion
            
            #region Exercice 4
            
            // Création de la liste des personnes qui sont plus grandes que la moyenne et qui vivent à Nantes
            // + tri de cette liste dans l'ordre décroissant en fonction des tailles
            List<Profil> tallerPersonsList = 
                B2.Students.Where(personne => personne.Taille > moyenne && personne.Address.Ville == "Nantes")
                    .OrderByDescending(personne => personne.Taille).ToList();
            
            // Afficher les personnes plus grandes que la moyenne et qui viennent de Nantes 
            tallerPersonsList.Select((personne, index) =>
                $"{index + 1} - {personne.Prenom} - {(personne.Taille / 100.00):F2}")// :F2 pour arrondir a 2 chiffres après la virgule
                .ToList() // Convertir en liste pour ensuite passer par tous les éléments (avec .ForEach)
                .ForEach(Console.WriteLine); // Afficher le résultat

            #endregion
        }
            
}