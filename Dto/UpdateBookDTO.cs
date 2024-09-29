using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Dto;

public record class UpdateBookDTO(
    [Required]string Title,
    [Required]string Description,
    [Required]string Author,
    decimal Price,
    [Required]int GenreId,
    string UserId
);
