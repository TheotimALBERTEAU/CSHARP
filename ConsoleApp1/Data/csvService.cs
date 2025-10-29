using Microsoft.Extensions.Configuration;

namespace ConsoleApp1;

public class csvService
{
    private readonly IConfiguration _configuration;

    public csvService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Profil> ReadAllProfils()
    {
        String path = _configuration.GetRequiredSection("CSVFiles")["ConsoleApp1"];

        List<Profil> Persons = new List<Profil>();

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

            person.AdressDetails = new Detail(details[0], int.Parse(details[1]), details[2]);

            Persons.Add(person);
        }
        return Persons;
    }
}