using piyoz.uz.Dtos.Driver;
using piyoz.uz.Pagination;
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
            .WithName("GetAllDrivers")
            .WithOpenApi();

            app.MapGet("/api/drivers/{Id}", async (int id, IDriverService driverService) =>
            {
                var driver = await driverService.GetById(id);
                return Results.Ok(driver);
            })
            .WithName("GetByIdDriver")
            .WithOpenApi();

            // Pagination
            app.MapGet("/api/drivers/pagination", async (int pageSize, int pageNumber, IDriverService driverService) =>
                {
                    var drivers = await driverService.GetAll();

                    var paginatedDrivers = drivers
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    var paginatedData = new PaginationData<DriverDto>
                    {
                        Data = drivers,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        PageCount = (int)Math.Ceiling((double)drivers.Count() / pageSize)
                    };

                    return Results.Ok(paginatedData);
                })
            .WithName("DriversPagination")
            .WithOpenApi();

            // Search by any property
            app.MapGet("/api/drivers/search", async (string searchBy, string searchKey, IDriverService driverService) =>
                {
                    var drivers = await driverService
                        .GetAll();

                    var filteredDrivers = drivers
                        .Where(d => d.GetType().GetProperty(searchBy).GetValue(d).ToString().Contains(searchKey))
                        .ToList();

                    return Results.Ok(filteredDrivers);
                })
                .WithName("DriversSearching")
                .WithOpenApi();

            app.MapPost("/api/drivers/add", async (CreateDriverDto createDriverDto, IDriverService driverService) =>
            {
                await driverService.Add(createDriverDto);
                return Results.Ok();
            })
            .WithName("CreateDriver")
            .WithOpenApi();

            app.MapPut("/api/drivers/update/{id}",
                async (int id, UpdateDriverDto updateDriverDto, IDriverService driverService) =>
                {
                    await driverService.Update(id, updateDriverDto);
                    return Results.Ok();
                })
                .WithName("UpdateDriver")
                .WithOpenApi();

            app.MapDelete("/api/drivers/delete/{id}", async (int id, IDriverService driverService) =>
            {
                await driverService.Delete(id);
                return Results.Ok();
            })
            .WithName("DeleteDriver")
            .WithOpenApi();

            return app;
        }
    }
}
