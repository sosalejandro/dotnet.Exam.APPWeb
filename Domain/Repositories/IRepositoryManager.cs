namespace dotnet.Exam.APPWeb.Domain.Repositories;

public interface IRepositoryManager
{
    IBookRepository BookRepository { get; }

    IUnitOfWork UnitOfWork { get; }
}