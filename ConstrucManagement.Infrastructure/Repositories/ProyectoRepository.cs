using ConstrucManagement.Domain.Entities.Construction;
using ConstrucManagement.Domain.Enums;
using ConstrucManagement.Infrastructure.Context;
using ConstrucManagement.Infrastructure.Interfaces;
using ConstrucManagement.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConstructoManagement.Infrastructure.Core;

namespace ConstrucManagement.Infrastructure.Repositories
{
    public class ProyectoRepository : BaseRepository<Proyecto>, IProyectoRepository
    {
        public ProyectoRepository(ConstructionDbContext context) : base(context) { }

        public async Task<Proyecto> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Proyectos
                .Include(p => p.Etapas)
                .Include(p => p.Presupuestos)
                    .ThenInclude(pr => pr.Gastos)
                .Include(p => p.Documentos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Proyecto>> GetByStatusAsync(EstadoProyecto estado)
        {
            return await _context.Proyectos
                .Where(p => p.Estado == estado)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalBudgetConsumedAsync(int proyectoId)
        {
            return await _context.Gastos
                .Where(g => g.Presupuesto.ProyectoId == proyectoId)
                .SumAsync(g => g.Monto);
        }

        public async Task<IEnumerable<Proyecto>> GetProjectsWithStagesAsync()
        {
            return await _context.Proyectos
                .Include(p => p.Etapas)
                .ToListAsync();
        }
    }
}
