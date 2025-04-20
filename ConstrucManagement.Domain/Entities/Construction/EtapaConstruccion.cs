using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Entities.Resources;
using ConstrucManagement.Domain.Enums;
using ConstructoManagement.Domain.Common;

namespace ConstructoManagement.Domain.Entities.Construction
{
    public class EtapaConstruccion : BaseEntity
    {
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public DateTime FechaInicioPlaneada { get; private set; }
        public DateTime FechaFinPlaneada { get; private set; }
        public DateTime? FechaInicioReal { get; private set; }
        public DateTime? FechaFinReal { get; private set; }
        public EstadoEtapa Estado { get; private set; }
        public decimal CostoEstimado { get; private set; }
        public decimal CostoReal { get; private set; }

        public int ProyectoId { get; private set; }
        public Proyecto Proyecto { get; private set; }

        private readonly List<AsignacionRecurso> _asignaciones = new();
        public IReadOnlyCollection<AsignacionRecurso> Asignaciones => _asignaciones.AsReadOnly();

        public EtapaConstruccion(string nombre, string descripcion, DateTime fechaInicioPlaneada,
            DateTime fechaFinPlaneada, decimal costoEstimado, int proyectoId)
        {
            Nombre = nombre ?? throw new DomainException("El nombre de la etapa es requerido");
            Descripcion = descripcion;
            FechaInicioPlaneada = fechaInicioPlaneada;
            FechaFinPlaneada = fechaFinPlaneada > fechaInicioPlaneada ? fechaFinPlaneada
                : throw new DomainException("La fecha fin debe ser posterior a la fecha inicio");
            CostoEstimado = costoEstimado > 0 ? costoEstimado
                : throw new DomainException("El costo estimado debe ser mayor a cero");
            Estado = EstadoEtapa.NoIniciada;
            ProyectoId = proyectoId;
        }

        public void IniciarEtapa()
        {
            if (Estado != EstadoEtapa.NoIniciada)
                throw new DomainException("Solo se pueden iniciar etapas no iniciadas");

            Estado = EstadoEtapa.EnProgreso;
            FechaInicioReal = DateTime.UtcNow;
        }

        public void CompletarEtapa(decimal costoReal)
        {
            if (Estado != EstadoEtapa.EnProgreso)
                throw new DomainException("Solo se pueden completar etapas en progreso");

            Estado = EstadoEtapa.Completada;
            FechaFinReal = DateTime.UtcNow;
            CostoReal = costoReal > 0 ? costoReal
                : throw new DomainException("El costo real debe ser mayor a cero");
        }

        public void AgregarAsignacion(AsignacionRecurso asignacion)
        {
            if (asignacion == null)
                throw new DomainException("La asignación no puede ser nula");

            _asignaciones.Add(asignacion);
        }
    }
}
