using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    // modifying the swagger so we can send the token and test it
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt auth header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
    });
});
builder.Services.AddDbContext<StoreContext>(opt =>
{


    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddCors();
builder.Services.AddIdentityCore<User>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
    .AddRoles<Role>()
    .AddEntityFrameworkStores<StoreContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,  //dont need it, its actually our API  http://localhost:3000/catalog
            ValidateAudience = false,
            ValidateLifetime = true,    //when token expires, api shows 401 unauthorized
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:TokenKey"]))
            // validating the token 
        };
    });



builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<PaymentService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseDefaultFiles(); // its going to look for default directory wwwroot/index.html
app.UseStaticFiles(); // using static files so it can serve the content above /index.html

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapFallbackToController("Index", "Fallback"); /*to tell our API to do something 
when he encounter with routes he doesnt know*/

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await DbInitializer.Initialize(context, userManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "Problem migrating data");
}

await app.RunAsync(); 


// namespace API
// {
//     public class Program
//     {
//         public static async Task Main(string[] args)
//         {
//             var host = CreateHostBuilder(args).Build();
//             using var scope = host.Services.CreateScope();
//             var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
//             var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
//             var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
//             try
//             {
//                 await context.Database.MigrateAsync();
//                 await DbInitializer.Initialize(context, userManager);
//             }
//             catch(Exception ex)
//             {
//                 logger.LogError(ex, "Problem migrating data");
//             }
//                 /*I have already used "using" above at the variable scope 
//                 so I dont need to put finally*/
//             // finally
//             // {
//             //     scope.Dispose();
//             // }

//             await host.RunAsync(); 
//         }

//         public static IHostBuilder CreateHostBuilder(string[] args) =>
//             Host.CreateDefaultBuilder(args)
//                 .ConfigureWebHostDefaults(webBuilder =>
//                 {
//                     webBuilder.UseStartup<Startup>();
//                 });
//     }
// }
