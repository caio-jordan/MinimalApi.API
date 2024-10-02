using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinimalApi.API.DbContexts;
using MinimalApi.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PratoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:PratoDbConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

//Adcionando as politicas de aceso, sendo necessário ser do brazil e admin
builder.Services.AddAuthorizationBuilder()
.AddPolicy("RequiredAdminFromBrazil", policy =>
    policy
        .RequireRole("admin")
        .RequireClaim("country", "Brazil"));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PratosAPI",
        Version = "v1"
    });

    options.AddSecurityDefinition("TakenAuthPrato", new()
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new ()
            {
                Reference = new ()
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="TakenAuthPrato"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseSwagger();
app.UseSwaggerUI();
app.RegistrePratosEndpoints();
app.RegistreIngredientesEndpoints();

app.Run();