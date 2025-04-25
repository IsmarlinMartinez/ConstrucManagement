using ConstructoManagement.Domain.Common;
using ConstructoManagement.Domain.Entities.Construction;
using ConstructoManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructoManagement.Domain.Entities.Costos
{
    public class Presupuesto : BaseEntity
    {
        public int ProyectoId { get; private set; }
        public Proyecto Proyecto { get; private set; }

        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }
        public decimal Monto { get; private set; }
        public DateTime FechaAprobacion { get; private set; }
        public string AprobadoPor { get; private set; }

        private readonly List<Gasto> _gastos = new();
        public IReadOnlyCollection<Gasto> Gastos => _gastos.AsReadOnly();

        public Presupuesto(int proyectoId, string codigo, string descripcion,
            decimal monto, DateTime fechaAprobacion, string aprobadoPor)
        {
            ProyectoId = proyectoId;
            Codigo = codigo ?? throw new DomainException("El código del presupuesto es requerido");
            Descripcion = descripcion;
            Monto = monto > 0 ? monto
                : throw new DomainException("El monto debe ser mayor a cero");
            FechaAprobacion = fechaAprobacion;
            AprobadoPor = aprobadoPor ?? throw new DomainException("El aprobador es requerido");
        }

        public void AgregarGasto(Gasto gasto)
        {
            if (gasto == null)
                throw new DomainException("El gasto no puede ser nulo");

            _gastos.Add(gasto);
        }

        public decimal CalcularDisponible()
        {
            return Monto - _gastos.Sum(g => g.Monto);
        }
    }
}
