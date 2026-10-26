using Backend.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Domain.Models
{
    public class LocationDetail: BaseEntity
    {
        public long LocationId { get; set; }
        [Required]
        [MaxLength(20)]
        public string LocationCode { get; set; }
        [MaxLength(50)]
        public string LocationName { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Address1 { get; set; }
        [MaxLength(50)]
        public string Address2 { get; set; }
        [MaxLength(20)]
        public string City { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }


    }
}
