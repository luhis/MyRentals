namespace MyRentals.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SaveRealtor
    {
        [Required]
        public string RealtorName { get; set; } = string.Empty;

        public string RealtorEmail { get; set; } = string.Empty;
    }
}