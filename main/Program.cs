using Amazon;
using Amazon.DynamoDBv2;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using AutoMapper;
using main.src.Profilers;
using main.src.Repositories;
using main.src.Repositories.DynamoUserDB;
using main.src.Services.User;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//AWS Configuration
var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);

//Dynamo Configuration
var dynamoDbConfig = builder.Configuration.GetSection("DynamoDbTables");
builder.Services.Configure<DbSettings>(dynamoDbConfig);
builder.Services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(RegionEndpoint.USEast2));


var mapperConfiguration = new MapperConfiguration(cgf =>
{
    cgf.AddProfile(typeof(ModelToReadDtoProfile));
    cgf.AddProfile(typeof(EntityToModelProfile));
    cgf.AddProfile(typeof(UserWriteDtoToUserEntityProfile));
    cgf.AddProfile(typeof(EntityUserToReadDtoProfile));
    cgf.AddProfile(typeof(UpdateUserDtoToEntityUserWithoutPasswordProfile));
});

var mapper = mapperConfiguration.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IDynamoDBUserRepository, DynamoDBUserRepository>();

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("x-api-version"), new MediaTypeApiVersionReader("x-api-version"));
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo { Title = "MSK", Version = "v1.0" });
    options.SwaggerDoc("v2",
        new OpenApiInfo { Title = "MSK", Version = "v2.0" });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v1/swagger.json", $"MSK v1");
        options.SwaggerEndpoint($"/swagger/v2/swagger.json", $"MSK v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
