using piyoz.uz.Dtos.Driver;
using piyoz.uz.Services;

namespace piyoz.uz.Endpoints
{
    public static class DriverEndpoint
    {
        public static IEndpointRouteBuilder MapDriverEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/drivers", async (IDriverService driverService) =>
            {
                var drivers = await driverService.GetAll();
                return Results.Ok(drivers);
            })
            .WithName("GetAll")
            .WithOpenApi();

            app.MapGet("/api/drivers/{Id}", async (int id, IDriverService driverService) =>
            {
                var driver = await driverService.GetById(id);
                return Results.Ok(driver);
            })
            .WithName("GetById")
            .WithOpenApi();

            app.MapPost("/api/drivers/add", async (CreateDriverDto createDriverDto, IDriverService driverService) =>
            {
                await driverService.Add(createDriverDto);
                return Results.Ok();
            })
            .WithName("Create")
            .WithOpenApi();

            app.MapPut("/api/drivers/update/{id}",
                async (int id, UpdateDriverDto updateDriverDto, IDriverService driverService) =>
                {
                    await driverService.Update(id, updateDriverDto);
                    return Results.Ok();
                })
                .WithName("Update")
                .WithOpenApi();

            app.MapDelete("/api/drivers/delete/{id}", async (int id, IDriverService driverService) =>
            {
                await driverService.Delete(id);
                return Results.Ok();
            })
            .WithName("Delete")
            .WithOpenApi();

            return app;
        }
    }
}
