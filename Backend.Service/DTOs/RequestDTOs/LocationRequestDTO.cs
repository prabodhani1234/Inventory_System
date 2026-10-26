using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Service.DTOs.RequestDTOs
{
    public class LocationRequestDTO
    {
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

    }
}
