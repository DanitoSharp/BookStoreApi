namespace BookStoreApi.Dto;

public record class CommentUpdateDTO(
    string Body,
    int BookId
);