using Backend.Service.DTOs.RequestDTOs;
using Backend.Service.DTOs.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.Interfaces.Services
{
    public interface ILocationService
    {
        Task<List<LocationResponseDTO>> GetAllLocationsAsync();
        Task<LocationResponseDTO> GetLocationByIdAsync(long locationId);
        Task<LocationResponseDTO> AddLocationAsync(LocationRequestDTO location);
        Task<LocationResponseDTO> UpdateLocationAsync(LocationRequestDTO location, long LocationId);
        Task DeleteLocationAsync(long locationId);
    }
}
