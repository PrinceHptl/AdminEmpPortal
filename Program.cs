using API_Dotnet.Data;
using API_Dotnet.Models;
using API_Dotnet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
//var configuration = builder.Configuration;
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddRouting();
//for entity framework
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(
   builder.Configuration.GetConnectionString("ConnStr")
));
//for identity
builder.Services.AddIdentity<ApplicationUser,IdentityRole<int>>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

//Add Authentication
builder.Services.AddAuthentication(Options=>{
    Options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
});


//Adding all scop here later i will create service extension class and add all scope there rather in program,cs file
builder.Services.AddScoped<IAccountservice,Accountservice>();




//Add jwt bearer
// .AddJwtBearer(Options=>{
//     Options.SaveToken=true;
//     Options.RequireHttpsMetadata=false;
//     Options.TokenValidationParameters=new TokenValidationParameters()
//     {
//         ValidateIssuer=true,
//         ValidateAudience=true,
//         ValidAudience=builder.Configuration["JWT:ValidAudience"],
//         ValidIssuer=builder.Configuration["JWT:ValidIssuer"],
//         IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
//     };
    
// });




                                                                                                                                



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
// app.UseEndpoints(endpoints =>
// {
//     _ = endpoints.MapControllers();
// });


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
