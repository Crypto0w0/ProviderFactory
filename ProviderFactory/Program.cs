using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;

class Program
{
    static string connectionString = "Data Source=localhost;Database=FruitsAndVegetables;Integrated Security=false;User ID=root;Password=Alex228420;";
    static Stopwatch timer = new Stopwatch();
    static async Task Main()
    {
        DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        
        using(var connection = factory.CreateConnection())
        {
            try
            {
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();
                Console.WriteLine("Connection succeded");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Connection error: {e.Message}");
            }
        }
        
        int ans;
        Console.WriteLine("1 - Get all information");
        Console.WriteLine("2 - Get all names");
        Console.WriteLine("3 - Get all colours");
        Console.WriteLine("4 - Get Max calories");
        Console.WriteLine("5 - Get Min calories");
        Console.WriteLine("6 - Get average calories");
        Console.WriteLine("7 - Get amount of vegetables");
        Console.WriteLine("8 - Get amount of fruits");
        Console.WriteLine("9 - Get amount of fruits and vegetables of the defined colour");
        Console.WriteLine("10 - Get amount of fruits and vegetables of every colour");
        Console.WriteLine("11 - Get fruits and vegetables with lower calories than defined");
        Console.WriteLine("12 - Get fruits and vegetables with higher calories than defined");
        Console.WriteLine("13 - Get fruits and vegetables with calories in the defined diapasone");
        Console.WriteLine("14 - Get fruits and vegetables with red or yellow colour");
        
        ans = Convert.ToInt32(Console.ReadLine());
        if (ans == 1)
        {
            await GetAllInfo();
        }
        else if (ans == 2)
        {
            await GetAllNames();
        }
        else if (ans == 3)
        {
            await GetAllColours();
        }
        else if (ans == 4)
        {
            await GetMaxCal();
        }
        else if (ans == 5)
        {
            await GetMinCal();
        }
        else if (ans == 6)
        {
            await GetAvgCal();
        }
        else if (ans == 7)
        {
            await VegetablesNum();
        }
        else if (ans == 8)
        {
            await FruitsNum();
        }
        else if (ans == 9)
        {
            Console.WriteLine("Colour: ");
            string c = Console.ReadLine();
            await FrAndVegNumWithCol(c);
        }
        else if (ans == 10)
        {
            await FrAndVegEveryCol();
        }
        else if (ans == 11)
        {
            Console.WriteLine("Calories: ");
            int cal = Convert.ToInt32(Console.ReadLine());
            await FrAndVegWithCaloriesLower(cal);
        }
        else if (ans == 12)
        {
            Console.WriteLine("Calories: ");
            int cal = Convert.ToInt32(Console.ReadLine());
            await FrAndVegWithCaloriesHigher(cal);
        }
        else if (ans == 13)
        {
            Console.WriteLine("Min calories: ");
            int min = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Max calories: ");
            int max = Convert.ToInt32(Console.ReadLine());
            await FrAndVegWithCaloriesInDiapasone(min, max);
        }
        else if (ans == 14)
        {
            await FrAndVegWithRedOrYellColour();
        }
        else Console.WriteLine("Unknown command");
    }

    static async Task GetAllInfo()
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using(var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT * FROM fruits_and_vegetables";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(
                            $"id: {reader["id"]}, name: {reader["name"]}, type: {reader["type"]}, colour: {reader["colour"]}, calories: {reader["calories"]}");
                    }
                }
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task GetAllNames()
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using(var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT name FROM fruits_and_vegetables";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader["name"]);
                    }
                }
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task GetAllColours()
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT colour FROM fruits_and_vegetables";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader["colour"]);
                    }
                }
            }
        }
        timer.Stop(); 
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task GetMaxCal()
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT MAX(calories) FROM fruits_and_vegetables";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                object result = await command.ExecuteScalarAsync();
                Console.WriteLine(result);
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task GetMinCal()
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT MIN(calories) FROM fruits_and_vegetables";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                object result = await command.ExecuteScalarAsync();
                Console.WriteLine(result);
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task GetAvgCal()
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT AVG(calories) FROM fruits_and_vegetables";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                object result = await command.ExecuteScalarAsync();
                Console.WriteLine(result);
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task VegetablesNum()
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM fruits_and_vegetables WHERE type = 'Vegetable'";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                object result = await command.ExecuteScalarAsync();
                Console.WriteLine(result);
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task FruitsNum()
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT COUNT(*) FROM fruits_and_vegetables WHERE type = 'Fruit'";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                object result = await command.ExecuteScalarAsync();
                Console.WriteLine(result);
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task FrAndVegNumWithCol(string userColour)
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT name FROM fruits_and_vegetables WHERE colour = @Colour";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                DbParameter p = command.CreateParameter();
                p.ParameterName = "@Colour";
                p.Value = userColour;
                command.Parameters.Add(p);
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Console.WriteLine(reader["name"]);
                }
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task FrAndVegEveryCol()
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT colour, COUNT(*) AS count FROM fruits_and_vegetables GROUP BY colour";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine($"Colour: {reader["colour"]}, amount: {reader["count"]}");
                    }
                }
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task FrAndVegWithCaloriesLower(int cal)
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT name FROM fruits_and_vegetables WHERE calories < @Calories";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                DbParameter p = command.CreateParameter();
                p.ParameterName = "@Calories";
                p.Value = cal;
                command.Parameters.Add(p);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader["name"]);
                    }
                }
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task FrAndVegWithCaloriesHigher(int cal)
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT name FROM fruits_and_vegetables WHERE calories > @Calories";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                DbParameter p = command.CreateParameter();
                p.ParameterName = "@Calories";
                p.Value = cal;
                command.Parameters.Add(p);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader["name"]);
                    }
                }
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task FrAndVegWithCaloriesInDiapasone(int min, int max)
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT name FROM fruits_and_vegetables WHERE calories BETWEEN @MinCal AND @MaxCal";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                DbParameter p1 = command.CreateParameter();
                p1.ParameterName = "@MinCal";
                p1.Value = min;
                command.Parameters.Add(p1);
                DbParameter p2 = command.CreateParameter();
                p2.ParameterName = "@MaxCal";
                p2.Value = max;
                command.Parameters.Add(p2);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader["name"]);
                    }
                }
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
    static async Task FrAndVegWithRedOrYellColour()
    {
        timer.Reset();
        timer.Start();
        var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        using (var connection = factory.CreateConnection())
        {
            connection.ConnectionString = connectionString;
            await connection.OpenAsync();
            string query = "SELECT Name FROM fruits_and_vegetables WHERE colour = 'Red' OR colour = 'Yellow'";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = query;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader["name"]);
                    }
                }
            }
        }
        timer.Stop();
        Console.WriteLine("Time: " + timer.Elapsed.TotalSeconds);
    }
}