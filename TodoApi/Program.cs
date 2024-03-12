using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ToDoDbContext>();
//builder.Services.AddSingleton<ToDoDbContext>();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(option => option.AddPolicy("AllowAll",
    builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", async (ToDoDbContext context) =>
{
    var data = await context.Items.ToListAsync();
    return Results.Ok(data);
});
var x = 101;
app.MapPost("/{name}", async (ToDoDbContext context,string name) =>
{
    var data = await context.Items.ToListAsync();
    Item i = new Item();
    i.Name = name;
    i.Id = x++; 
    i.IsComplete = false;

    context.Items.Add(i);
    context.SaveChanges();
    return Results.Ok(data);
});

app.MapDelete("/{id}", async (ToDoDbContext context,int id) =>
{
    var data = await context.Items.ToListAsync();
    Item x=data.Find(x=>x.Id == id);
    if (x != null)
    {
        context.Items.Remove(x);
        context.SaveChanges();
    }
    return Results.Ok(data);
});

app.MapPut("/{id}/{isComplete}", async (ToDoDbContext context,int id,bool isComplete) =>
{
    var data = await context.Items.ToListAsync();
    Item x = data.Find(x => x.Id == id);
    if (x != null)
    {
        x.IsComplete=isComplete;
        context.Items.Update(x);
        context.SaveChanges();
    }
    return Results.Ok(data);
});

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
