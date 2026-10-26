using Backend.Domain.Models;
using Backend.Service.DTOs.RequestDTOs;
using Backend.Service.DTOs.ResponseDTOs;
using Backend.Service.Interfaces.Repositories;
using Backend.Service.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.Services
{
    public class LocationService: ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }


        public async Task<List<LocationResponseDTO>> GetAllLocationsAsync()
        {
            var location =
                await _locationRepository.GetAllLocationsAsync();

            return location.Select(l => new LocationResponseDTO
            {
                LocationId =l.LocationId,
                LocationCode = l.LocationCode,
                LocationName = l.LocationName,
            }).ToList();
        }

        public async Task<LocationResponseDTO> GetLocationByIdAsync(long locationId)
        {
            var location =
                await _locationRepository.GetLocationByIdAsync(locationId);

            if (location is null)
            {
                return null;
            }

            return new LocationResponseDTO
            {
                LocationId = location.LocationId,
                LocationCode = location.LocationCode,
                LocationName = location.LocationName,
            };
        }

        public async Task<LocationResponseDTO> AddLocationAsync(LocationRequestDTO location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            if (string.IsNullOrWhiteSpace(location.LocationCode))
            {
                throw new ArgumentException("Location Code is required.");
            }

            if (string.IsNullOrWhiteSpace(location.LocationName))
            {
                throw new ArgumentException("Location Name is required.");
            }

            var newLocation = new LocationDetail
            {
                LocationCode = location.LocationCode.Trim(),
                LocationName = location.LocationName.Trim(),
                Address1 = location.Address1?.Trim() ?? string.Empty,
                Address2 = location.Address2?.Trim() ?? string.Empty,
                City = location.City?.Trim() ?? string.Empty,
                PostalCode = location.PostalCode?.Trim() ?? string.Empty,

                IsActive = true,
                IsDelete = false,
                CreatedDate = DateTime.Now
            };

            await _locationRepository.AddLocationAsync(newLocation);

            return new LocationResponseDTO
            {
                LocationId = newLocation.LocationId,
                LocationCode = newLocation.LocationCode,
                LocationName = newLocation.LocationName,
                Address1 = newLocation.Address1,
                Address2 = newLocation.Address2,
                City = newLocation.City,
                PostalCode = newLocation.PostalCode,
                IsActive = newLocation.IsActive
            };
        }

        public async Task<LocationResponseDTO> UpdateLocationAsync(LocationRequestDTO location, long locationId)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            if (string.IsNullOrWhiteSpace(location.LocationCode))
            {
                throw new ArgumentException("Location Code is required.");
            }

            if (string.IsNullOrWhiteSpace(location.LocationName))
            {
                throw new ArgumentException("Location Name is required.");
            }

            var existingLocation =
                await _locationRepository.GetLocationByIdAsync(locationId);

            if (existingLocation == null || existingLocation.IsDelete)
            {
                throw new KeyNotFoundException(
                    $"Location with ID {locationId} was not found.");
            }

            existingLocation.LocationCode = location.LocationCode.Trim();
            existingLocation.LocationName = location.LocationName.Trim();
            existingLocation.Address1 = location.Address1?.Trim() ?? string.Empty;
            existingLocation.Address2 = location.Address2?.Trim() ?? string.Empty;
            existingLocation.City = location.City?.Trim() ?? string.Empty;
            existingLocation.PostalCode = location.PostalCode?.Trim() ?? string.Empty;

            existingLocation.ModifiedDate = DateTime.Now;

            await _locationRepository.UpdateLocationAsync(existingLocation);

            return new LocationResponseDTO
            {
                LocationId = existingLocation.LocationId,
                LocationCode = existingLocation.LocationCode,
                LocationName = existingLocation.LocationName,
                Address1 = existingLocation.Address1,
                Address2 = existingLocation.Address2,
                City = existingLocation.City,
                PostalCode = existingLocation.PostalCode,
                IsActive = existingLocation.IsActive
            };
        }

        public async Task DeleteLocationAsync(long locationId)
        {
            var existingLocation = await _locationRepository.GetLocationByIdAsync(locationId);

            if (existingLocation == null || existingLocation.IsDelete)
            {
                throw new KeyNotFoundException(
                    $"Location with ID {locationId} was not found.");
            }

            existingLocation.IsDelete = true;
            existingLocation.IsActive = false;
            existingLocation.ModifiedDate = DateTime.Now;

            await _locationRepository.UpdateLocationAsync(existingLocation);
        }
    
    }
}
