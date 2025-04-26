using ConstrucManagement.Domain.Entities.Construction;
using ConstrucManagement.Domain.Enums;
using ConstrucManagement.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Infrastructure.Interfaces
{
    public interface IProyectoRepository : IBaseRepository<Proyecto>
    {
        Task<Proyecto> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Proyecto>> GetByStatusAsync(EstadoProyecto estado);
        Task<decimal> GetTotalBudgetConsumedAsync(int proyectoId);
        Task<IEnumerable<Proyecto>> GetProjectsWithStagesAsync();
    }
}
