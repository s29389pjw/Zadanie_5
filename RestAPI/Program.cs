using RestAPI;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/animals", () => DataRepository.Animals);
app.MapGet("/animals/{id}", (int id) =>
    DataRepository.Animals.FirstOrDefault(a => a.Id == id));

app.MapPost("/animals", (Animal animal) => {
    DataRepository.Animals.Add(animal);
    return Results.Created($"/animals/{animal.Id}", animal);
});

app.MapPut("/animals/{id}", (int id, Animal updatedAnimal) => {
    var animal = DataRepository.Animals.FirstOrDefault(a => a.Id == id);
    if (animal == null) return Results.NotFound();
    animal.Name = updatedAnimal.Name;
    animal.Category = updatedAnimal.Category;
    animal.Weight = updatedAnimal.Weight;
    animal.FurColor = updatedAnimal.FurColor;
    return Results.Ok(animal);
});

app.MapDelete("/animals/{id}", (int id) => {
    var animal = DataRepository.Animals.FirstOrDefault(a => a.Id == id);
    if (animal == null) return Results.NotFound();
    DataRepository.Animals.Remove(animal);
    return Results.Ok();
});

app.MapGet("/visits", () => DataRepository.Visits);
app.MapGet("/visits/{id}", (int id) =>
    DataRepository.Visits.FirstOrDefault(v => v.Id == id));

app.MapPost("/visits", (Visit visit) => {
    DataRepository.Visits.Add(visit);
    return Results.Created($"/visits/{visit.Id}", visit);
});

app.Run();