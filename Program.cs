
using AI_Career_Guidence.Data;
using AI_Career_Guidence.Models;
using AI_Career_Guidence.Services;
using AI_Career_Guidence.Filters;
using Microsoft.OpenApi.Models;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson();  // For working with file uploads and custom serialization

// Register Swagger and add FileUploadOperationFilter
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ResumeDataRepository>();
builder.Services.AddScoped<PersonalDetailRepository>();
builder.Services.AddScoped<ResumeCertificationsRepository>();
builder.Services.AddScoped<ResumeEducationRepository>();
builder.Services.AddScoped<ResumeExperienceRepository>();
builder.Services.AddScoped<ResumeInterestsRepository>();
builder.Services.AddScoped<ResumeLanguagesRepository>();
builder.Services.AddScoped<ResumeProjectsRepository>();
builder.Services.AddScoped<ResumeSkillsRepository>();
builder.Services.AddScoped<ResumeSummaryRepository>();
builder.Services.AddScoped<UserResumeRepository>();



// Add CORS support
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173")  // Adjust the React app URL as necessary
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Register Cloudinary configuration and service
builder.Services.AddSingleton<CloudinaryService>();
var cloudinarySettings = builder.Configuration.GetSection("Cloudinary");
var cloudinaryAccount = new Account(
    cloudinarySettings["CloudName"],
    cloudinarySettings["ApiKey"],
    cloudinarySettings["ApiSecret"]
);
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "AI Career Guidance API", Version = "v1" });

    // ✅ Apply filter for Resume Photo (requires `file` + `userId`)
    options.OperationFilter<SwaggerFileUploadOperationFilter>();

    // ✅ Apply filter for Resume Images (requires `file` + `resumeId`)
    options.OperationFilter<SwaggerResumeImagesUploadFilter>();
});





//// Add HttpClient for external API requests (if required)
//builder.Services.AddHttpClient("Lightcast", client =>
//{
//    client.BaseAddress = new Uri("https://api.lightcast.io");
//    client.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_API_KEY");
//});

var app = builder.Build();

// Enable CORS to allow requests from React frontend
app.UseCors("AllowReactApp");

// Use Swagger for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Other middleware configurations
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

// Run the application
app.Run();
