using Microsoft.EntityFrameworkCore;
using zStatsApi.Data;
using zStatsApi.Dtos.Set;
using zStatsApi.Entities;
using zStatsApi.Mapping;

namespace zStatsApi.Endpoints;

public static class SetEndpoints
{
    const string GetSetEndpointName = "GetSet";

    public static WebApplication MapSetEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/sets")
            .WithParameterValidation();
        
        // GET /sets
        group.MapGet("/", (ZStatsContext dbContext) =>
            dbContext.Sets
                .Select(set => set.ToDto()));

        // GET /sets/{id}
        group.MapGet("/{id}", (int id, ZStatsContext dbContext) =>
            {
                var set = dbContext.Sets.Find(id);
                
                return set is null ?
                    Results.NotFound() : Results.Ok(set.ToDto());
            })
            .WithName(GetSetEndpointName);

        // POST /sets
        group.MapPost("/", (CreateSetDto newSet, ZStatsContext dbContext) =>
        {
            Set set = newSet.ToEntity();
            
            dbContext.Sets.Add(set);
            dbContext.SaveChanges();
            
            return Results.CreatedAtRoute(
                GetSetEndpointName,
                new { id = set.Id },
                set.ToDto()
            );
        });

        // PUT /sets/{id}
        group.MapPut("/{id}", (int id, UpdateSetDto updatedSet, ZStatsContext dbContext) =>
        {
            var existingSet = dbContext.Sets.Find(id);

            if (existingSet is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingSet)
                .CurrentValues
                .SetValues(updatedSet.ToEntity(id));
            
            dbContext.SaveChanges();
            
            return Results.NoContent();
        });

        // DELETE /sets/{id}
        group.MapDelete("/{id}", (int id, ZStatsContext dbContext) =>
        {
            dbContext.Sets
                .Where(s => s.Id == id)
                .ExecuteDelete();

            dbContext.SaveChanges();
            
            return Results.NoContent();
        });

        return app;
    }
}