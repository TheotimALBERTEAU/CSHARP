using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConsoleApp1.Model;

public class Profil
{
    [Key]
    public Guid Id {get;set;} = new Guid();
    
    [Required]
    private String firstname;

    [Required]
    private String lastname;
    
    [Required]
    private DateTime birthdate;
    
    [Required]
    //relation n..n vers Detail
    public ICollection<Detail> Details { get; set; } = new List<Detail>();
    
    [Required]
    private int size;

    [ForeignKey("profil_classe_fk")]
    public Guid IdClasse {get; set;}
    
    public Classe Classe {get; set;}

    #region Accesseur

    public string Firstname
    {
        get => firstname;
        set => firstname = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Lastname
    {
        get => lastname;
        set => lastname = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime Birthdate
    {
        get => birthdate;
        set => birthdate = value;
    }
    
    public int Size
    {
        get => size;
        set => size = value;
    }

    #endregion

    public int getYearsOld()
    {
        DateTime today = DateTime.Today;

        int years = today.Year - birthdate.Year;

        if (today.Month < birthdate.Month || today.Month == birthdate.Month && today.Day < birthdate.Day)
        {
            years--;
        }
        
        return years;
    }
}