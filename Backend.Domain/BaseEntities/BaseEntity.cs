using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Domain.BaseEntities
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        [MaxLength(50)]
        public string CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? ModifiedUser { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
