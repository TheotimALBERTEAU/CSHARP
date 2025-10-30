using ConsoleApp1.Data.InterfaceRepository;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Data;

public class PersonRepository : IPersonRepository
{
    private readonly SchoolDbContext _schoolDbContext;

    public PersonRepository(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }

    public List<Profil> GetAllPerson()
    {
        return _schoolDbContext.Profil.ToList();
    }

    public List<Profil> GetAllEthan()
    {
        return _schoolDbContext.Profil
            .Include(person => person.Details)
            .Where(person => person.Firstname.Equals("Ethan"))
            .ToList();
    }
}