namespace ConsoleApp1;

public class Profil
{
    private Detail address;
    private DateTime today = DateTime.Now;

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string Datenaissance { get; set; }

    public Detail Address
    {
        get => address;
        set => address = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime Today
    {
        get => today;
        set => today = value;
    }

    public DateTime Date()
    {
        var date = DateTime.ParseExact(Datenaissance, "dd/MM/yyyy", null);
        return date;
    }

    public int YearsOld()
    {
        var date = DateTime.ParseExact(Datenaissance, "dd/MM/yyyy", null);
        if (today.Month < date.Month) return today.Year - date.Year - 1;
        if (today.Month == date.Month && today.Day < date.Day) return today.Year - date.Year - 1;

        return today.Year - date.Year;
    }

    public int Taille { get; set; }
}