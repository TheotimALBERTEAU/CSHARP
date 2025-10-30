using Npgsql;

namespace ConsoleApp1;

public class DbConnection
{
    private readonly NpgsqlConnection _connection;
    public DbConnection(NpgsqlConnection connection)
    {
        _connection = connection;
    }
    
    public async Task init(Classe maClasse)
    {
         // Commence une transaction pour tout insérer ensemble
        await using var transaction = await _connection.BeginTransactionAsync();

        // --- Insert Classe ---
        var insertClasseCmd = new NpgsqlCommand(
            "INSERT INTO classe(name, level, school) VALUES (@name, @level, @school) RETURNING id", _connection, transaction);
        insertClasseCmd.Parameters.AddWithValue("name", maClasse.Name);
        insertClasseCmd.Parameters.AddWithValue("level", maClasse.Level);
        insertClasseCmd.Parameters.AddWithValue("school", maClasse.School);
        
        Guid classeId = (Guid)await insertClasseCmd.ExecuteScalarAsync();

        // --- Insert Persons ---
        
        foreach (var person in maClasse.Persons)
        {
            var insertPersonCmd = new NpgsqlCommand(
                "INSERT INTO profil(firstname, lastname, birthdate, size, idclasse) VALUES (@firstname, @lastname, @birthdate, @size, @idClasse) RETURNING id", _connection, transaction);
            insertPersonCmd.Parameters.AddWithValue("firstname", person.Firstname);
            insertPersonCmd.Parameters.AddWithValue("lastname", person.Lastname);
            insertPersonCmd.Parameters.AddWithValue("birthdate", person.Birthdate);
            insertPersonCmd.Parameters.AddWithValue("size", person.Size);
            insertPersonCmd.Parameters.AddWithValue("idclasse", classeId);
            
            Guid personId = (Guid)await insertPersonCmd.ExecuteScalarAsync();

            // --- Insert Details ---
                var insertDetailCmd = new NpgsqlCommand(
                    "INSERT INTO details(street, city, zipcode) VALUES (@street, @city, @zipCode) RETURNING id", _connection, transaction);
                insertDetailCmd.Parameters.AddWithValue("street", person.AdressDetails.Street);
                insertDetailCmd.Parameters.AddWithValue("city", person.AdressDetails.City);
                insertDetailCmd.Parameters.AddWithValue("zipCode", person.AdressDetails.ZipCode);
                
                Guid detailId = (Guid)await insertDetailCmd.ExecuteScalarAsync();

                var insertPersonDetailCmd = new NpgsqlCommand(
                    "INSERT INTO profildetails(id_profil, id_details) VALUES (@idPerson, @idDetail)", _connection, transaction);
                insertPersonDetailCmd.Parameters.AddWithValue("idPerson", personId);
                insertPersonDetailCmd.Parameters.AddWithValue("idDetail", detailId);
                await insertPersonDetailCmd.ExecuteNonQueryAsync();
                
        }

        // Commit
        await transaction.CommitAsync();

        Console.WriteLine("Insertion hiérarchique réussie");
    }

}