using Backend.Data.Data;
using Backend.Domain.Models;
using Backend.Service.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _context;
        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<LocationDetail>> GetAllLocationsAsync()
        {
            return await _context.LocationDetails.ToListAsync();
        }
        public async Task<LocationDetail> GetLocationByIdAsync(long locationId)
        {
            return await _context.LocationDetails.FindAsync(locationId);
        }
        public async Task AddLocationAsync(LocationDetail location)
        {
            _context.LocationDetails.Add(location);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateLocationAsync(LocationDetail location)
        {
            _context.LocationDetails.Update(location);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteLocationAsync(long locationId)
        {
            var location = await _context.LocationDetails.FindAsync(locationId);
            if (location != null)
            {
                _context.LocationDetails.Remove(location);
                await _context.SaveChangesAsync();
            }
        }
    }
}
