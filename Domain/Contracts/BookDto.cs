using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts;

public record BookDto(Guid Id, string Name, string Author, int PublishedYear, int Version);
