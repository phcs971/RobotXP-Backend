using RobotXP.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
                      policy => {
                          policy
                            .WithOrigins("*")
                            .WithMethods("GET", "POST", "PUT", "DELETE");
                      });
});

MqttService.Instance.Build();
var _ = MqttService.Instance.Start();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();

