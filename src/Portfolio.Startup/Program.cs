using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Common.Application.Configuration;
using Portfolio.Common.Infrastructure.Configuration;
using Portfolio.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonApplication(builder.Configuration);
builder.Services.AddCommonInfrastructure(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
