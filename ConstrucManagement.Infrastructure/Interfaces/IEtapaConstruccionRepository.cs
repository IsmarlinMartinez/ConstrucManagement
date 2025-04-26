using ConstrucManagement.Domain.Entities.Construction;
using ConstrucManagement.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Infrastructure.Interfaces
{
    public interface IEtapaConstruccionRepository : IBaseRepository<EtapaConstruccion>
    {
        Task<IEnumerable<EtapaConstruccion>> GetStagesByProjectAsync(int proyectoId);
        Task<IEnumerable<EtapaConstruccion>> GetStagesWithDependenciesAsync(int proyectoId);
        Task<decimal> CalculateStageCostAsync(int etapaId);
    }
}
