using WebApplication3;
using System.Data.SqlClient;


        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        
        var animals = new List<Animal>();
        string connectionString = "Server=localhost;Database=apbd;User Id=SA;Password=248652Alexey;";
        SqlConnection connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new SqlCommand("SELECT * FROM Animal", connection);
        using SqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            int idAnimal = reader.GetInt32(reader.GetOrdinal("IdAnimal"));
            string name = reader.GetString(reader.GetOrdinal("Name"));
            string description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"));
            string category = reader.GetString(reader.GetOrdinal("Category"));
            string area = reader.GetString(reader.GetOrdinal("Area"));
            Animal animal = new Animal(idAnimal, name, description, category, area);
            animals.Add(animal);
        }
        
        
        app.MapGet("/animals", () => Results.Ok(animals));
        app.MapGet("/animals/{id:int}", (int id) => FindAnimalById(animals, id));
        app.MapPost("/animals", (Animal animal) => AddAnimal(animals, animal));
        app.MapPut("/animals/{id:int}", (int id, Animal animal) => UpdateAnimal(animals, id, animal));
        app.MapDelete("/animals/{id:int}", (int id) => DeleteAnimal(animals, id));

        app.Run();

//dokladna logika

    static IResult FindAnimalById(List<Animal> animals, int id) {
        var animal = animals.FirstOrDefault(a => a.IdAnimal == id);
        return animal is null ? Results.NotFound($"Animal with ID {id} not found.") : Results.Ok(animal);
    }

    static IResult AddAnimal(List<Animal> animals, Animal animal) {
        animal.IdAnimal = animals.Count + 1;
        animals.Add(animal);
        return Results.Created($"/animals/{animal.IdAnimal}", animal);
    }

    static IResult UpdateAnimal(List<Animal> animals, int id, Animal updatedAnimal) {
        var index = animals.FindIndex(a => a.IdAnimal == id);
        if (index == -1)
            return Results.NotFound($"Animal with ID {id} not found.");
        updatedAnimal.IdAnimal = id;
        animals[index] = updatedAnimal;
        return Results.Ok(updatedAnimal);
    }

    static IResult DeleteAnimal(List<Animal> animals, int id) {
        var animal = animals.FirstOrDefault(a => a.IdAnimal == id);
        if (animal is null)
            return Results.NotFound($"Animal with ID {id} not found.");
        animals.Remove(animal);
        return Results.NoContent();
    }
    