using System.ComponentModel.DataAnnotations;

public class Classe
{
    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();
    
    [Required]
    public string Name { get; set; }
    public string Level { get; set; }
    public string School { get; set; }
    
    public ICollection<Profil> Persons  { get; set; } = new List<Profil>();
}