namespace Domain.Contracts;

public record CreateBookDto(string Name, string Author, int PublishedYear, int Version);
