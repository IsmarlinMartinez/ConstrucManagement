using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Entities.Construction;
namespace ConstrucManagement.Domain.Entities.Finance
{
    public class Presupuesto : BaseEntity
    {
        
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public decimal MontoTotal { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaAprobacion { get; private set; }
        public string AprobadoPor { get; private set; }

        
        public int ProyectoId { get; private set; }
        public Proyecto Proyecto { get; private set; }
        public ICollection<RubroPresupuestario> Rubros { get; private set; } = new List<RubroPresupuestario>();

        
        private Presupuesto() { }
      
        public Presupuesto(string nombre, decimal montoTotal, int proyectoId, string descripcion = null)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            MontoTotal = montoTotal > 0 ? montoTotal :
                throw new ArgumentException("El monto total debe ser mayor a cero", nameof(montoTotal));
            ProyectoId = proyectoId;
            Descripcion = descripcion;
            FechaCreacion = DateTime.UtcNow;
        }

        public void ActualizarInformacion(string nombre, string descripcion)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion;
            UpdateModifiedDate();
        }

        public void Aprobar(string aprobadoPor)
        {
            if (string.IsNullOrWhiteSpace(aprobadoPor))
                throw new ArgumentNullException(nameof(aprobadoPor));

            FechaAprobacion = DateTime.UtcNow;
            AprobadoPor = aprobadoPor;
            UpdateModifiedDate();
        }

        public void ActualizarMontoTotal(decimal nuevoMonto)
        {
            if (nuevoMonto <= 0)
                throw new ArgumentException("El monto total debe ser mayor a cero", nameof(nuevoMonto));

            MontoTotal = nuevoMonto;
            UpdateModifiedDate();
        }

        public decimal CalcularMontoUtilizado()
        {
            return Rubros?.Sum(r => r.MontoUtilizado) ?? 0;
        }

        public decimal CalcularDisponibilidad()
        {
            return MontoTotal - CalcularMontoUtilizado();
        }
    }
}