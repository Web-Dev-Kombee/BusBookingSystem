using System;

namespace BusBookingSystem.Domain.Entities
{
    public class Bus
    {
        public string BusNumber { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
    }
} 