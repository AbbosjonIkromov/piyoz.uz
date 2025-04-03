using piyoz.uz.Dtos.Car;
using piyoz.uz.Dtos.Rental;
using piyoz.uz.Pagination;
using piyoz.uz.Services;

namespace piyoz.uz.Endpoints
{
    public static class RentalEndpoint
    {
        public static IEndpointRouteBuilder MapRentalEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/rentals",
                    async (IRentalService rentalService) =>
                {
                    var rentals = await rentalService.GetAll();
                    return Results.Ok(rentals);
                })
                .WithName("GelAllRental")
                .WithOpenApi();

            app.MapGet("/api/rentals/{id}", 
                    async (int id, IRentalService rentalService) =>
                {
                    var rental = await rentalService.GetById(id);
                    return Results.Ok(rental);
                })
                .WithName("GetByIdRental")
                .WithOpenApi();

            // Pagination
            app.MapGet("/api/rentals/pagination",
                    async (int pageNumber, int pageSize, IRentalService rentalService) =>
                {
                    var rentals = await rentalService.GetAll();

                    var paginatedRentals = rentals
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    var paginatedData = new PaginationData<RentalDto>
                    {
                        Data = rentals,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        PageCount = (int)Math.Ceiling((double)rentals.Count() / pageSize)
                    };

                    return Results.Ok(paginatedData);
                })
                .WithName("RentalPagination")
                .WithOpenApi();

            // Search by any property
            app.MapGet("/api/rentals/searching",
                    async (string searchBy, string searchKey, IRentalService rentalService) =>
                    {
                        var rentals = await rentalService.GetAll();

                        var filteredRentals = rentals
                            .Where(r => r.GetType().GetProperty(searchBy).GetValue(r).ToString().Contains(searchKey))
                            .ToList();

                        return Results.Ok(filteredRentals);
                    })
                .WithName("RentalSearching")
                .WithOpenApi();

            app.MapPost("/api/rentals/add",
                    async (CreateRentalDto createRentalDto, IRentalService rentalService) =>
                {
                    await rentalService.Add(createRentalDto);
                    return Results.Ok();
                })
                .WithName("CreateRental")
                .WithOpenApi();

            app.MapPut("/api/rentals/update/{id}", 
                    async (int id, UpdateRentalDto updateRentalDto, IRentalService rentalService) =>
                {
                    await rentalService.Update(id, updateRentalDto);
                    return Results.Ok();
                })
                .WithName("UpdateRental")
                .WithOpenApi();

            app.MapDelete("/api/rentals/delete/{id}",
                    async (int id, IRentalService rentalService) =>
                {
                    await rentalService.Delete(id);
                    return Results.Ok();
                })
                .WithName("DeleteRental")
                .WithOpenApi();

            return app;
        }
    }
}
