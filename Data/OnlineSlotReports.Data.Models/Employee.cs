namespace OnlineSlotReports.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
    using OnlineSlotReports.Data.Common.Models;

    public class Employee : BaseDeletableModel<string>
    {
        public Employee()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MinLength(3)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public DateTime StartWorkDate { get; set; }

        public DateTime? EndWorkDate { get; set; }

        [Required]
        public string GamingHallId { get; set; }

        public virtual GamingHall GamingHall { get; set; }
    }
}
