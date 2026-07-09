namespace Tour_Management.Models
{
    public class TourViewModel
    {
        public string TourName { get; set; }
        public string Place { get; set; }
        public string Days { get; set; }
        public string Locations { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile TourImage { get; set; }
        public string Price { get; set; }
        public string TourInfo { get; set; }
    }
}
