using piyoz.uz.Dtos.Car;
using piyoz.uz.Services;

namespace piyoz.uz.Endpoints
{
    public static class CarEndpoint
    {
        public static IEndpointRouteBuilder MapCarEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/cars", async (ICarService carService) =>
                {
                    var cars = await carService.GetAll();
                    return Results.Ok(cars);
                })
                .WithName("GelAll")
                .WithOpenApi();

            app.MapGet("/api/cars/{id}", async (int id, ICarService carService) =>
                {
                    var car = await carService.GetById(id);
                    return Results.Ok(car);
                })
                .WithName("GetById")
                .WithOpenApi();


            app.MapPost("/api/cars/add", async (CreateCarDto createCarDto, ICarService carService) =>
                {
                    await carService.Add(createCarDto);
                    return Results.Ok();
                })
                .WithName("Create")
                .WithOpenApi();

            app.MapPut("/api/cars/update/{id}", async (int id, UpdateCarDto updateCarDto, ICarService carService) =>
                {
                    await carService.Update(id, updateCarDto);
                    return Results.Ok();
                })
                .WithName("Update")
                .WithOpenApi();

            app.MapDelete("/api/cars/delete/{id}", async (int id, ICarService carService) =>
                {
                    await carService.Delete(id);
                    return Results.Ok();
                })
                .WithName("Delete")
                .WithOpenApi();

            return app;
        }
    }
}
