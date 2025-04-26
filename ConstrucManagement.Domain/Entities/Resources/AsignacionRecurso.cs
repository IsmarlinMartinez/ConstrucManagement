using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Entities.Construction;
using ConstrucManagement.Domain.Exceptions;

namespace ConstrucManagement.Domain.Entities.Resources
{
    public class AsignacionRecurso : BaseEntity
    {
        public int EtapaConstruccionId { get; private set; }
        public EtapaConstruccion EtapaConstruccion { get; private set; }

        public int RecursoId { get; private set; }
        public string TipoRecurso { get; private set; }
        public decimal Cantidad { get; private set; }
        public DateTime FechaAsignacion { get; private set; }
        public DateTime? FechaLiberacion { get; private set; }
        public string Observaciones { get; private set; }

        public AsignacionRecurso(int etapaConstruccionId, int recursoId,
            string tipoRecurso, decimal cantidad, string observaciones)
        {
            EtapaConstruccionId = etapaConstruccionId;
            RecursoId = recursoId;
            TipoRecurso = tipoRecurso ?? throw new DomainException("El tipo de recurso es requerido");
            Cantidad = cantidad > 0 ? cantidad
                : throw new DomainException("La cantidad debe ser mayor a cero");
            FechaAsignacion = DateTime.UtcNow;
            Observaciones = observaciones;
        }

        public void LiberarRecurso(string observaciones)
        {
            if (FechaLiberacion.HasValue)
                throw new DomainException("El recurso ya fue liberado");

            FechaLiberacion = DateTime.UtcNow;
            Observaciones = observaciones;
        }
    }
}
