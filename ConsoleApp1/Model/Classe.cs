using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.Model;

public class Classe
{
    [Key]
    public Guid Id {get;set;} = new Guid();
    
    [Required]
    private string name;
    
    [Required]
    private string level;
    
    [Required]
    private string school;
    
    private List<Profil> persons;

    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Level
    {
        get => level;
        set => level = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string School
    {
        get => school;
        set => school = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Profil> Persons
    {
        get => persons;
        set => persons = value ?? throw new ArgumentNullException(nameof(value));
    }
}