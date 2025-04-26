using ConstrucManagement.Domain.Exceptions.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException(string entityName, object key)
            : base($"La entidad '{entityName}' con ID '{key}' no fue encontrada.")
        {
            EntityName = entityName;
            Key = key;
        }

        public string EntityName { get; }
        public object Key { get; }
    }
}
