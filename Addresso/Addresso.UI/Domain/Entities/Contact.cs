using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Addresso.UI.Domain.Entities;

public class Contact
{
    public Contact(){}

    [Required]
    [JsonPropertyName("first_name")]
    public string? FirstName { get;set;}
    
    [Required]
    [JsonPropertyName("last_name")]
    public string? LastName { get;set;}
    
    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public string? Email { get;set; }
    
    [Required]
    [Phone]
    [JsonPropertyName("phone")]
    public string? Phone { get;set; }
}