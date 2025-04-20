using ConstrucManagement.Domain.Entities.Construction;
using ConstrucManagement.Domain.Enums;
using ConstructoManagement.Domain.Common;


namespace ConstructoManagement.Domain.Entities.Construction
{
    public class Proyecto : BaseEntity, IAggregateRoot
    {
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public string Ubicacion { get; private set; }
        public string Cliente { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFinEstimada { get; private set; }
        public decimal Presupuesto { get; private set; }
        public EstadoProyecto Estado { get; private set; }

        private readonly List<EtapaConstruccion> _etapas = new();
        public IReadOnlyCollection<EtapaConstruccion> Etapas => _etapas.AsReadOnly();

        private readonly List<EtapaDependencia> _dependencias = new();
        public IReadOnlyCollection<EtapaDependencia> Dependencias => _dependencias.AsReadOnly();

        public Proyecto(string nombre, string descripcion, string ubicacion, string cliente,
            DateTime fechaInicio, DateTime fechaFinEstimada, decimal presupuesto)
        {
            Nombre = nombre ?? throw new DomainException("El nombre del proyecto es requerido");
            Descripcion = descripcion;
            Ubicacion = ubicacion ?? throw new DomainException("La ubicación del proyecto es requerida");
            Cliente = cliente ?? throw new DomainException("El cliente del proyecto es requerido");
            FechaInicio = fechaInicio;
            FechaFinEstimada = fechaFinEstimada;
            Presupuesto = presupuesto > 0 ? presupuesto
                : throw new DomainException("El presupuesto debe ser mayor a cero");
            Estado = EstadoProyecto.Planificacion;
        }

        public void ActualizarEstado(EstadoProyecto nuevoEstado)
        {
            if (Estado == EstadoProyecto.Completada || Estado == EstadoProyecto.Cancelada)
                throw new DomainException("No se puede modificar un proyecto finalizado o cancelado");

            Estado = nuevoEstado;
            AddDomainEvent(new ProyectoEstadoCambiadoEvent(this));
        }

        public void AgregarEtapa(EtapaConstruccion etapa)
        {
            if (etapa == null)
                throw new DomainException("La etapa no puede ser nula");

            _etapas.Add(etapa);
        }

        public void AgregarDependencia(EtapaDependencia dependencia)
        {
            if (dependencia == null)
                throw new DomainException("La dependencia no puede ser nula");

            _dependencias.Add(dependencia);
        }
    }

    public class ProyectoEstadoCambiadoEvent : IDomainEvent
    {
        public Proyecto Proyecto { get; }

        public ProyectoEstadoCambiadoEvent(Proyecto proyecto)
        {
            Proyecto = proyecto;
        }
    }
}
