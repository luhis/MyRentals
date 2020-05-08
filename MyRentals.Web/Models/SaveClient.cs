namespace MyRentals.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SaveClient
    {
        [Required] public string ClientName { get; set; } = string.Empty;
    }
}