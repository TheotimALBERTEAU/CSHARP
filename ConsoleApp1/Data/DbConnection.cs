using ConsoleApp1.Data;
using ConsoleApp1.Model;
using Npgsql;

namespace ConsoleApp1;

public class DbConnection
{
    private readonly SchoolDbContext _schoolDbContext;
    public DbConnection(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }

    public void SaveFullClasse(Classe maClasse)
    {
        _schoolDbContext.Add(maClasse);
        
        _schoolDbContext.SaveChanges();
    }

    #region  sans EntityFramework

        // public async Task init(Classe maClasse)
    // {
    //      // Commence une transaction pour tout insérer ensemble
    //     await using var transaction = await _connection.BeginTransactionAsync();
    //
    //     // --- Insert Classe ---
    //     var insertClasseCmd = new NpgsqlCommand(
    //         "INSERT INTO classe(name, level, school) VALUES (@name, @level, @school) RETURNING id", _connection, transaction);
    //     insertClasseCmd.Parameters.AddWithValue("name", maClasse.Name);
    //     insertClasseCmd.Parameters.AddWithValue("level", maClasse.Level);
    //     insertClasseCmd.Parameters.AddWithValue("school", maClasse.School);
    //     
    //     Guid classeId = (Guid)await insertClasseCmd.ExecuteScalarAsync();
    //
    //     // --- Insert Persons ---
    //     
    //     foreach (var person in maClasse.Persons)
    //     {
    //         var insertPersonCmd = new NpgsqlCommand(
    //             "INSERT INTO person(firstname, lastname, birthdate, size, id_classe) VALUES (@firstname, @lastname, @birthdate, @size, @idClasse) RETURNING id", _connection, transaction);
    //         insertPersonCmd.Parameters.AddWithValue("firstname", person.Firstname);
    //         insertPersonCmd.Parameters.AddWithValue("lastname", person.Lastname);
    //         insertPersonCmd.Parameters.AddWithValue("birthdate", person.Birthdate);
    //         insertPersonCmd.Parameters.AddWithValue("size", person.Size);
    //         insertPersonCmd.Parameters.AddWithValue("idclasse", classeId);
    //         
    //         Guid personId = (Guid)await insertPersonCmd.ExecuteScalarAsync();
    //
    //         // --- Insert Details ---
    //             var insertDetailCmd = new NpgsqlCommand(
    //                 "INSERT INTO detail(street, city, zipCode) VALUES (@street, @city, @zipCode) RETURNING id", _connection, transaction);
    //             insertDetailCmd.Parameters.AddWithValue("street", person.Details.First().Street);
    //             insertDetailCmd.Parameters.AddWithValue("city", person.Details.First().City);
    //             insertDetailCmd.Parameters.AddWithValue("zipCode", person.Details.First().ZipCode);
    //             
    //             Guid detailId = (Guid)await insertDetailCmd.ExecuteScalarAsync();
    //
    //             var insertPersonDetailCmd = new NpgsqlCommand(
    //                 "INSERT INTO person_detail(id_person, id_detail) VALUES (@idPerson, @idDetail)", _connection, transaction);
    //             insertPersonDetailCmd.Parameters.AddWithValue("idPerson", personId);
    //             insertPersonDetailCmd.Parameters.AddWithValue("idDetail", detailId);
    //             await insertPersonDetailCmd.ExecuteNonQueryAsync();
    //             
    //     }
    //
    //     // Commit
    //     await transaction.CommitAsync();
    //
    //     Console.WriteLine("Insertion hiérarchique réussie");
    // }

    #endregion
}