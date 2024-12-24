
using Domain.Models;

namespace dotnet.Exam.APPWeb.Domain.Models
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public int Version { get; set; }
    }
}