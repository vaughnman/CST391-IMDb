using System.Data.Common;
using DotNetEnv;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

public class MySqlDatabase 
{
    internal MySqlConnection _connection;

    public MySqlDatabase() 
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        
        if(connectionString == null || connectionString == "") {
            DotNetEnv.Env.TraversePath().Load();
            connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
        }

        Console.WriteLine("String: " + connectionString);

        _connection = new MySqlConnection(connectionString);
        _connection.Open();
    }

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

    internal T TryGetFieldValue<T>(DbDataReader row, int index)
    {
        try
        {
            return row.GetFieldValue<T>(index);
        } catch(Exception ex) {
            return default(T);
        }
    }
}