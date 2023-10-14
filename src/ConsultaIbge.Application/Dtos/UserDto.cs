using System.ComponentModel.DataAnnotations;

namespace ConsultaIbge.Application.Dtos;

public record UserDto(
    [Required, EmailAddress] string Email, 
    [Required, MinLength(8, ErrorMessage = "A senha deve conter no mínimo 8 caracteres.")] string Password);
