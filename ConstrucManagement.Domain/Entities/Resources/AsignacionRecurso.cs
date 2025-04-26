using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Entities.Resources;

namespace ConstrucManagement.Domain.Entities.Construction
{
    public class AsignacionRecurso : BaseEntity
    {

        public int Cantidad { get; private set; }
        public DateTime FechaAsignacion { get; private set; }
        public DateTime? FechaRetiro { get; private set; }
        public string Observaciones { get; private set; }

        public int EtapaId { get; private set; }
        public EtapaConstruccion Etapa { get; private set; }
        public int RecursoId { get; private set; }
        public Recurso Recurso { get; private set; }

        private AsignacionRecurso() { }

        public AsignacionRecurso(int etapaId, int recursoId, int cantidad, string observaciones = null)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero", nameof(cantidad));

            EtapaId = etapaId;
            RecursoId = recursoId;
            Cantidad = cantidad;
            Observaciones = observaciones;
            FechaAsignacion = DateTime.UtcNow;
        }

        public void Retirar(string observaciones = null)
        {
            if (FechaRetiro.HasValue)
                throw new InvalidOperationException("El recurso ya ha sido retirado");

            FechaRetiro = DateTime.UtcNow;
            Observaciones = observaciones;
            UpdateModifiedDate();
        }

        public void ActualizarCantidad(int nuevaCantidad)
        {
            if (nuevaCantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero", nameof(nuevaCantidad));

            Cantidad = nuevaCantidad;
            UpdateModifiedDate();
        }
    }
}
