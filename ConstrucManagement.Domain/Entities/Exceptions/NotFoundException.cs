using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructoManagement.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string entity, object key)
            : base($"Entity \"{entity}\" ({key}) was not found.") { }
    }
}
