namespace BookStoreApi.Dto;

public record class CommentDTO(
    string Body,
    int BookId
);