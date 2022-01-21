using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Rentals_API_NET6.Context;
using Rentals_API_NET6.Models;
using Rentals_API_NET6.Services;
using System.Reflection;
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Rentals_Database"); 
builder.Services.AddDbContext<RentalsDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<FileStorageManager>();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddControllersWithViews(options =>
{
    options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
})
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.Authority = builder.Configuration["Authority:Server"];
    options.RequireHttpsMetadata = true;
    options.Audience = "RentalsApi";
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy =>
    {
        policy.RequireClaim(AuthorizationConstants.ADMIN_CLAIM, "1");
    });
    options.AddPolicy("Employee", policy =>
    {
        policy.RequireClaim(AuthorizationConstants.EMPLOYEE_CLAIM, "1");
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_Rentals", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseTus(httpContext =>
{
    var uploadPath = Path.Combine(app.Environment.ContentRootPath, builder.Configuration["ImgFolder"]);
    var fsm = httpContext.RequestServices.GetService<FileStorageManager>();
    var TUSconf = new DefaultTusConfiguration
    {
        Store = new TusDiskStore(uploadPath),
        UrlPath = "/files",
        MaxAllowedUploadSizeInBytes = 100000000,
        Events = new tusdotnet.Models.Configuration.Events
        {
            OnFileCompleteAsync = async eventContext =>
            {
                ITusFile file = await eventContext.GetFileAsync();
                await fsm.StoreTus(file, eventContext);
            }
        }
    };
    return TUSconf;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
{
    var builder = new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
        .Services.BuildServiceProvider();

    return builder
        .GetRequiredService<IOptions<MvcOptions>>()
        .Value
        .InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>()
        .First();
}
