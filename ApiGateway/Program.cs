using ApiCommon.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddStandardSerilog("Api-Gateway");
builder.Services.AddStandardOpenTelemetry(builder.Configuration, "Api-Gateway");

// Register YARP
builder.Services.AddReverseProxy()
.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the Master Swagger Dashboard
//app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.RoutePrefix = string.Empty; // Dashboard accessed at http://localhost:5297/

	// Add dropdown options mapping to the downstream Swagger JSONs
	c.SwaggerEndpoint("/user-docs/v1/swagger.json", "User API");
	c.SwaggerEndpoint("/product-docs/v1/swagger.json", "Product API");
	c.SwaggerEndpoint("/cart-docs/v1/swagger.json", "Cart API");
	c.SwaggerEndpoint("/order-docs/v1/swagger.json", "Order API");
	c.SwaggerEndpoint("/ai-docs/v1/swagger.json", "AI API");
});

// Map the YARP routing middleware
app.MapReverseProxy();

app.Run();