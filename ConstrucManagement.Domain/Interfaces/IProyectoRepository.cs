using ConstrucManagement.Domain.Enums;
using ConstrucManagement.Domain.Interfaces;
using ConstrucManagement.Domain.Entities.Construction;
using ConstrucManagement.Domain.Construction;
using Proyecto = ConstrucManagement.Domain.Construction.Proyecto;

namespace ConstrucManagement.Domain.Interfaces
{
    public interface IProyectoRepository : IRepository<Proyecto>
    {
        Task<Proyecto> GetWithStagesAsync(int id);
        Task<IEnumerable<Proyecto>> GetByStatusAsync(EstadoProyecto estado);
        Task<decimal> GetTotalBudgetConsumedAsync(int proyectoId);
    }
}
