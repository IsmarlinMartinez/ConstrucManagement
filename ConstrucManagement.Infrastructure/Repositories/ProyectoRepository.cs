using ConstrucManagement.Infrastructure.Data;
using ConstrucManagement.Infrastructure.Repositories;
using ConstrucManagement.Domain.Entities.Construction;
using ConstrucManagement.Domain.Enums;
using ConstrucManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Proyecto = ConstrucManagement.Domain.Construction.Proyecto;

namespace ConstrucManagement.Infrastructure.Repositories
{
    public class ProyectoRepository : IProyectoRepository
    {
        private readonly DbContext _context;

        public ProyectoRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Proyecto> GetWithStagesAsync(int id)
        {
            return await _context.Proyectos
                .Include(p => p.Etapas)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<decimal> GetTotalBudgetConsumedAsync(int proyectoId)
        {
            return await _context.Gastos
                .Where(g => g.Etapa.ProyectoId == proyectoId)
                .SumAsync(g => g.Monto);
        }
    }
}