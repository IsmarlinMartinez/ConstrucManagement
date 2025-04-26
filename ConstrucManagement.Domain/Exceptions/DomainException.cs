using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Domain.Exceptions.DomainExceptions
{
    public sealed class DomainException : Exception
    {
        public string ErrorCode { get; }
        public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;

        public DomainException() { }

        public DomainException(string message) : base(message) { }

        public DomainException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public DomainException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
