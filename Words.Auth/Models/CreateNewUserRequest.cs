namespace Words.Auth.Models;

public record CreateNewUserRequest(
    string FirstName,
    string LastName,
    string NickName,
    DateTime DoB,
    string Email,
    string CountryCode,
    string Password);