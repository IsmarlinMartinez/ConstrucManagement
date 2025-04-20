using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public void SetCreatedDate() => CreatedDate = DateTime.UtcNow;
        public void SetModifiedDate() => ModifiedDate = DateTime.UtcNow;
    }
}


