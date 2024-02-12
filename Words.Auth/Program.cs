using Words.Auth.Domain.Persistence.Extensions;
using Words.Auth.Infrastructure;
using Words.Auth.Infrastructure.Certificate;

var builder = WebApplication.CreateBuilder(args);

builder.UseLogging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.AddUserContext();
var cert = SecurityDocumentHandler.GetSecurityDocument();

builder.AddUserIdentity();
builder.AddIdentityProvider(cert);

var app = builder.Build();
app.UseIdentityProvider();
app.UseRouting(); 
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
