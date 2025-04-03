using System.Collections.Immutable;
using piyoz.uz.Dtos.Car;
using piyoz.uz.Pagination;
using piyoz.uz.Services;

namespace piyoz.uz.Endpoints
{
    public static class CarEndpoint
    {
        public static IEndpointRouteBuilder MapCarEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/cars", 
                    async (ICarService carService) =>
                {
                    var cars = await carService.GetAll();
                    return Results.Ok(cars);
                })
                .WithName("GelAllCars")
                .WithOpenApi();

            app.MapGet("/api/cars/{id}", 
                    async (int id, ICarService carService) =>
                {
                    var car = await carService.GetById(id);
                    return Results.Ok(car);
                })
                .WithName("GetByIdCar")
                .WithOpenApi();

            // Pagination
            app.MapGet("/api/cars/pagination", 
                    async (int pageNumber, int pageSize, ICarService carService) =>
                {
                    var cars = await carService.GetAll();

                    var paginatedCars = cars
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    var paginatedData = new PaginationData<CarDto>
                    {
                        Data = cars,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        PageCount = (int)Math.Ceiling((double)cars.Count() / pageSize)
                    };


                    return Results.Ok(paginatedData);
                })
                .WithName("CarPagination")
                .WithOpenApi();

            app.MapGet("/api/cars/searching",
                    async (string searchBy, string searchKey, ICarService carService) =>
                {
                    var cars = await carService.GetAll();

                    var filteredCars = cars
                        .Where(r => r.GetType().GetProperty(searchBy).GetValue(r).ToString().Contains(searchKey))
                        .ToList();

                    return Results.Ok(filteredCars);
                })
                .WithName("CarSearching")
                .WithOpenApi();

            app.MapPost("/api/cars/add", 
                    async (CreateCarDto createCarDto, ICarService carService) =>
                {
                    await carService.Add(createCarDto);
                    return Results.Ok();
                })
                .WithName("CreateCar")
                .WithOpenApi();

            app.MapPut("/api/cars/update/{id}", 
                    async (int id, UpdateCarDto updateCarDto, ICarService carService) =>
                {
                    await carService.Update(id, updateCarDto);
                    return Results.Ok();
                })
                .WithName("UpdateCar")
                .WithOpenApi();

            app.MapDelete("/api/cars/delete/{id}",
                    async (int id, ICarService carService) =>
                {
                    await carService.Delete(id);
                    return Results.Ok();
                })
                .WithName("DeleteCar")
                .WithOpenApi();

            return app;
        }
    }
}
