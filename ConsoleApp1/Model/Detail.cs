using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Detail
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public String Street { get; set; }
    public int zipCode { get; set; }
    public String City { get; set; }
    
    public ICollection<Profil> Persons { get; set; } = new List<Profil>();
}