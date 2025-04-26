using ConstrucManagement.Domain.Enums;
using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Entities.Finance;
using ConstrucManagement.Domain.Exceptions.DomainExeptions;

namespace ConstrucManagement.Domain.Entities.Construction
{
    public class EtapaConstruccion : BaseEntity
    {
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public DateTime FechaInicioPlaneada { get; private set; }
        public DateTime FechaFinPlaneada { get; private set; }
        public DateTime? FechaInicioReal { get; private set; }
        public DateTime? FechaFinReal { get; private set; }
        public decimal PorcentajeAvance { get; private set; }
        public EstadoEtapa Estado { get; private set; }

       
        public int ProyectoId { get; private set; }
        public Proyecto Proyecto { get; private set; }
        public ICollection<EtapaConstruccion> EtapasPredecesoras { get; private set; } = new List<EtapaConstruccion>();
        public ICollection<AsignacionRecurso> AsignacionesRecursos { get; private set; } = new List<AsignacionRecurso>();
        public ICollection<Gasto> Gastos { get; private set; } = new List<Gasto>();

        
        private EtapaConstruccion() { }

     
        public EtapaConstruccion(string nombre, DateTime fechaInicioPlaneada, DateTime fechaFinPlaneada, int proyectoId)
        {
            if (fechaFinPlaneada < fechaInicioPlaneada)
                throw new DomainException("La fecha de fin planeada no puede ser anterior a la fecha de inicio");

            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            FechaInicioPlaneada = fechaInicioPlaneada;
            FechaFinPlaneada = fechaFinPlaneada;
            ProyectoId = proyectoId;
            Estado = EstadoEtapa.NoIniciada;
            PorcentajeAvance = 0;
        }

       
        public void ActualizarInformacion(string nombre, string descripcion)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion;
            UpdateModifiedDate();
        }

        public void ActualizarFechasPlaneadas(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaFin < fechaInicio)
                throw new DomainException("La fecha de fin planeada no puede ser anterior a la fecha de inicio");

            FechaInicioPlaneada = fechaInicio;
            FechaFinPlaneada = fechaFin;
            UpdateModifiedDate();
        }

        public void ActualizarAvance(decimal porcentaje)
        {
            if (porcentaje < 0 || porcentaje > 100)
                throw new DomainException("El porcentaje de avance debe estar entre 0 y 100");

            PorcentajeAvance = porcentaje;

            if (porcentaje == 100)
                CompletarEtapa();

            UpdateModifiedDate();
        }

        public void IniciarEtapa()
        {
            if (Estado != EstadoEtapa.NoIniciada)
                throw new DomainException("Solo se pueden iniciar etapas no iniciadas");

            if (EtapasPredecesoras.Any(e => e.Estado != EstadoEtapa.Completada))
                throw new DomainException("No se puede iniciar la etapa hasta que todas las predecesoras estén completadas");

            Estado = EstadoEtapa.EnProgreso;
            FechaInicioReal = DateTime.UtcNow;
            UpdateModifiedDate();
        }

        public void CompletarEtapa()
        {
            if (Estado != EstadoEtapa.EnProgreso)
                throw new DomainException("Solo se pueden completar etapas en progreso");

            Estado = EstadoEtapa.Completada;
            PorcentajeAvance = 100;
            FechaFinReal = DateTime.UtcNow;
            UpdateModifiedDate();
        }

        public void AgregarPredecesora(EtapaConstruccion predecesora)
        {
            if (predecesora == null)
                throw new ArgumentNullException(nameof(predecesora));

            if (predecesora.Id == Id)
                throw new DomainException("Una etapa no puede ser predecesora de sí misma");

            EtapasPredecesoras.Add(predecesora);
            UpdateModifiedDate();
        }
    }
}
