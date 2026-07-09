using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourManagement.Domain.Entities
{
    public class Tour
    {
        [Key]
        public int TourId { get; set; }
        [Required]
        public string TourName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public string Destination { get; set; }
        public string ImagePath { get; set; }
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // Admin, User
    }

    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfPeople { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // Pending, Confirmed, Cancelled
    }
}
