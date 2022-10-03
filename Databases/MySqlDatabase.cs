using System.Data.Common;
using DotNetEnv;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

/// <summary>
/// Base class for MySqlDatabases. Holds useful query tools
/// </summary>
public class MySqlDatabase
{
    internal MySqlConnection _connection;

    /// <summary>
    /// Retrieves connection string from config and opens a database connection
    /// </summary>
    public MySqlDatabase() 
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        
        if(connectionString == null || connectionString == "") {
            DotNetEnv.Env.TraversePath().Load();
            connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        }

        _connection = new MySqlConnection(connectionString);
        _connection.Open();
    }

    /// <summary>
    /// Reads the result of a command as a list of Objects
    /// </summary>
    internal async Task<List<T>> ReadAsList<T>(MySqlCommand command) 
    {
        var rows = await command.ExecuteReaderAsync();

        var list = new List<T>();
        
        while(await rows.ReadAsync())
        {
            var jObject = CurrentRowToJObject(rows);
            list.Add(jObject.ToObject<T>());
        }

        return list;
    }

    /// <summary>
    /// Converts the current database row to a Newtonsoft JObject 
    /// </summary>
    internal JObject CurrentRowToJObject(DbDataReader row)
    {
        var jObject = new JObject();
        for(int i = 0; i < row.FieldCount; i++)
        {
            var name = row.GetName(i);
            var type = row.GetFieldType(i);
            
            if(type == typeof(String))
                jObject.Add(name, TryGetFieldValue<String>(row, i));

            if(type == typeof(Int64))
                jObject.Add(name, TryGetFieldValue<Int64>(row, i));
        }

        return jObject;
    }

    /// <summary>
    /// Returns field value or the type's default value
    /// </summary>
    internal T TryGetFieldValue<T>(DbDataReader row, int index)
    {
        try
        {
            return row.GetFieldValue<T>(index);
        } catch(Exception ex) {
            return default(T);
        }
    }

    /// <summary>
    /// Closes the database connection
    /// </summary>
    ~MySqlDatabase()
    {
        _connection.Close();
    }
}