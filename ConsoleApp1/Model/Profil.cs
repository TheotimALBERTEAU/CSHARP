using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConsoleApp1;

public class Profil
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateTime Birthdate { get; set; }
    public int Size { get; set; }
    public ICollection<Detail> AddressDetails { get; set; } = new List<Detail>();

    [ForeignKey("Classe")] public Guid ClassId { get; set; }

    public Classe Classe { get; set; }
}