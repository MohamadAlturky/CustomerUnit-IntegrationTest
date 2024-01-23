using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomersManagement.Presentation.ApiModels;

public class CreateCustomerRequest
{

    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string CustomerId { get; set; } = string.Empty;
}
