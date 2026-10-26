using Backend.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.DTOs.ResponseDTOs
{
    public class LocationResponseDTO :BaseEntity
    {
        public long LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class MinimalLocationResponseDTO
    {
        public long LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
    }
}
