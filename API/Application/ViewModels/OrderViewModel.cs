using System;

namespace API.Application.ViewModels
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public int NumberOfSeats { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime BookedDate { get; set; }
        public string Status { get; set; }
    }
}
