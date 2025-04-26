using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Entities.Finance;
using ConstrucManagement.Domain.Entities.Documentacion;
using ConstrucManagement.Domain.Enums;
using ConstrucManagement.Domain.ValueObjects;
using ConstrucManagement.Domain.Exceptions.DomainExeptions;


namespace ConstrucManagement.Domain.Entities.Construction
{
    public class Proyecto : BaseEntity
    {
        // Propiedades
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public string Cliente { get; private set; }
        public Direccion Ubicacion { get; private set; }
        public DateTime FechaInicio { get; private set; }
        public DateTime FechaFinEstimada { get; private set; }
        public DateTime? FechaFinReal { get; private set; }
        public decimal PresupuestoInicial { get; private set; }
        public EstadoProyecto Estado { get; private set; }

        
        public ICollection<EtapaConstruccion> Etapas { get; private set; } = new List<EtapaConstruccion>();
        public ICollection<Presupuesto> Presupuestos { get; private set; } = new List<Presupuesto>();
        public ICollection<Documento> Documentos { get; private set; } = new List<Documento>();

   
        private Proyecto() { }

      
        public Proyecto(string nombre, string descripcion, string cliente, Direccion ubicacion,
                       DateTime fechaInicio, DateTime fechaFinEstimada, decimal presupuestoInicial)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion;
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            Ubicacion = ubicacion ?? throw new ArgumentNullException(nameof(ubicacion));
            FechaInicio = fechaInicio;
            FechaFinEstimada = fechaFinEstimada;
            PresupuestoInicial = presupuestoInicial > 0 ? presupuestoInicial :
                throw new ArgumentException("El presupuesto debe ser mayor a cero", nameof(presupuestoInicial));
            Estado = EstadoProyecto.NoIniciada;
        }

  
        public void ActualizarInformacion(string nombre, string descripcion, string cliente, Direccion ubicacion)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion;
            Cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            Ubicacion = ubicacion ?? throw new ArgumentNullException(nameof(ubicacion));
            UpdateModifiedDate();
        }

        public void ActualizarFechas(DateTime fechaInicio, DateTime fechaFinEstimada)
        {
            if (fechaFinEstimada < fechaInicio)
                throw new DomainException("La fecha de fin estimada no puede ser anterior a la fecha de inicio");

            FechaInicio = fechaInicio;
            FechaFinEstimada = fechaFinEstimada;
            UpdateModifiedDate();
        }

        public void ActualizarPresupuesto(decimal nuevoPresupuesto)
        {
            if (nuevoPresupuesto <= 0)
                throw new DomainException("El presupuesto debe ser mayor a cero");

            PresupuestoInicial = nuevoPresupuesto;
            UpdateModifiedDate();
        }

        public void CambiarEstado(EstadoProyecto nuevoEstado)
        {
            if (Estado == nuevoEstado) return;

           
            if (nuevoEstado == EstadoProyecto.Completada && !TodasEtapasCompletadas())
                throw new DomainException("No se puede completar el proyecto si no todas las etapas están completadas");

            Estado = nuevoEstado;

            if (nuevoEstado == EstadoProyecto.Completada)
                FechaFinReal = DateTime.UtcNow;

            UpdateModifiedDate();
        }

        private bool TodasEtapasCompletadas()
        {
            return Etapas.All(e => e.Estado == EstadoEtapa.Completada);
        }
    }
}