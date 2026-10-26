using Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.Interfaces.Repositories
{
    public interface ILocationRepository
    {
        Task<List<LocationDetail>> GetAllLocationsAsync();
        Task<LocationDetail> GetLocationByIdAsync(long locationId);
        Task AddLocationAsync(LocationDetail location);
        Task UpdateLocationAsync(LocationDetail location);
        Task DeleteLocationAsync(long locationId);
    }
}
