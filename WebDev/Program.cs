using Microsoft.EntityFrameworkCore;
using WebDev.BLL.Services;
using WebDev.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", builder =>
        builder.WithOrigins("https://web-application-for-ordering-groceries-70ve.onrender.com","http://frontend:3000","http://localhost:3000","http://frontend:80","http://localhost:80") // replace with actual frontend URL
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDBContext>(options => {
    //options.UseNpgsql(builder.Configuration.GetConnectionString("DeployConnection"));
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

    try
    {
        if (!dbContext.Database.CanConnect())
        {
            Console.WriteLine("Can't connect to the database.");
            // Optional: Apply migrations here manually if desired
        }
        else
        {
            dbContext.Database.Migrate();  // This should apply pending migrations
            Console.WriteLine("Migrations applied.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error while applying migrations: {ex.Message}");
    }
}



app.UseCors("AllowClient");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
