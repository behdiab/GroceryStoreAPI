using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class CustomerForCreationDto
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Length cannot exceed 50 characters")]
        public string Name { get; set; }

    }
}
