using Microsoft.OpenApi.Models;
using PizzaStore.DB;

var builder = WebApplication.CreateBuilder(args);
// adding middleware prior to build
builder.Services.AddCors(options => {});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
  {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pizza API", Description = "Keep track of your tasks", Version = "v1" });
  });
// build app
var app = builder.Build();
// use middleware with using OpenApi.Models
app.UseSwagger();
app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
  });
// api endpoints
app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));

app.Run();
