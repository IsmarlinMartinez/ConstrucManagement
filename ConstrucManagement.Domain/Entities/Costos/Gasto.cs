using ConstructoManagement.Domain.Common;
using ConstructoManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructoManagement.Domain.Entities.Costos
{
    public class Gasto : BaseEntity
    {
        public int PresupuestoId { get; private set; }
        public Presupuesto Presupuesto { get; private set; }

        public string Descripcion { get; private set; }
        public decimal Monto { get; private set; }
        public DateTime Fecha { get; private set; }
        public string TipoGasto { get; private set; }
        public string Comprobante { get; private set; }
        public string AprobadoPor { get; private set; }

        public Gasto(int presupuestoId, string descripcion, decimal monto,
            DateTime fecha, string tipoGasto, string comprobante, string aprobadoPor)
        {
            PresupuestoId = presupuestoId;
            Descripcion = descripcion ?? throw new DomainException("La descripción es requerida");
            Monto = monto > 0 ? monto
                : throw new DomainException("El monto debe ser mayor a cero");
            Fecha = fecha;
            TipoGasto = tipoGasto ?? throw new DomainException("El tipo de gasto es requerido");
            Comprobante = comprobante;
            AprobadoPor = aprobadoPor ?? throw new DomainException("El aprobador es requerido");
        }
    }
}
