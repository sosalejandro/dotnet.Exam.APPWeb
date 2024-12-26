namespace Domain.Contracts;

public record UpdateBookDto(string Name, string Author, int PublishedYear, int Version);