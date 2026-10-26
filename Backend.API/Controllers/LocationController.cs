using Backend.API.Contracts.Response;
using Backend.Service.DTOs.RequestDTOs;
using Backend.Service.DTOs.ResponseDTOs;
using Backend.Service.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(
            ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocation()
        {
            var locations = await _locationService.GetAllLocationsAsync();

            if (locations == null || !locations.Any<LocationResponseDTO>())
            {
                return NotFound(new ApiResponse<IEnumerable<LocationResponseDTO>>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "No locations found.",
                    IsSuccess = false,
                });
            }

            return Ok(new ApiResponse<IEnumerable<LocationResponseDTO>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Locations retrieved successfully.",
                IsSuccess = true,
                Data = locations
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdLocation(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
    
            if (location == null )
            {
                return NotFound(new ApiResponse<IEnumerable<LocationResponseDTO>>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "No locations found.",
                    IsSuccess = false,
                });
            }

            return Ok(new ApiResponse<IEnumerable<LocationResponseDTO>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Locations retrieved successfully.",
                IsSuccess = true,
                Data = new List<LocationResponseDTO> { location }
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation( LocationRequestDTO locationDto)
        {
            try
            {
                var location = await _locationService.AddLocationAsync(locationDto);

                return CreatedAtAction(
                    nameof(GetByIdLocation),
                    new { id = location.LocationId },
                    new ApiResponse<LocationResponseDTO>
                    {
                        StatusCode = (int)HttpStatusCode.Created,
                        Message = "Location created successfully.",
                        IsSuccess = true,
                        Data = location
                    });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<LocationResponseDTO>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLocation(int locationId,LocationRequestDTO LocationDto)
        {
            try
            {
                var updated = await _locationService.UpdateLocationAsync( LocationDto, locationId);

                return Ok(new ApiResponse<LocationResponseDTO>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Location updated successfully.",
                    IsSuccess = true,
                    Data = updated
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse<LocationResponseDTO>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<LocationResponseDTO>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLocation(int locationId)
        {
            try
            {
                await _locationService.DeleteLocationAsync(locationId);

                return Ok(new ApiResponse<object>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = "Location deleted successfully.",
                    IsSuccess = true,
                    Data = null
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse<object>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = ex.Message,
                    IsSuccess = false,
                    Data = null
                });
            }
        }
    }
}