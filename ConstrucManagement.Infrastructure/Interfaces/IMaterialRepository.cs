using ConstrucManagement.Domain.Entities.Resources;
using ConstrucManagement.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructoManagement.Infrastructure.Interfaces
{
    public interface IMaterialRepository : IBaseRepository<Material>
    {
        Task<IEnumerable<Material>> GetMaterialsBelowStockAsync();
        Task UpdateStockAsync(int materialId, int quantity);
        Task<IEnumerable<Material>> GetMaterialsBySupplierAsync(string supplier);
    }
}
