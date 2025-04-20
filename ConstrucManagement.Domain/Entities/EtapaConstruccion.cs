using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Enums;

namespace ConstrucManagement.Domain.Entities
{
    public class EtapaConstruccion : BaseEntity
    {
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaInicioPlaneada { get; set; }
        public DateTime FechaFinPlaneada { get; set; }
        public EstadoEtapa Estado { get; set; }

        public Proyecto Proyecto { get; set; } = null!;
        public ICollection<AsignacionRecurso> Asignaciones { get; set; } = new List<AsignacionRecurso>();
    }

}
