using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Words.Auth.Domain.Persistence.Models;

public class GameUser : IdentityUser
{
    [MaxLength(256)]
    [RegularExpression(@"/^[A-Za-z]*$/")]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(256)]
    [RegularExpression(@"/^[A-Za-z]*$/")]
    public string LastName{ get; set; } = string.Empty;

    [MaxLength(32)]
    [RegularExpression(@"/^[A-Za-z][A-Za-z0-9]*$/")]
    public string NickName { get; set; } = string.Empty;
    public DateTime DoB { get; set; }

    [MaxLength(2)]
    public string CountryCode { get; set; } = string.Empty;
}