using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Enums;
namespace ConstrucManagement.Domain.Entities
{
    public class Proyecto : BaseEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal PresupuestoInicial { get; set; }
        public EstadoProyecto Estado { get; set; }

        public ICollection<EtapaConstruccion> Etapas { get; set; } = new List<EtapaConstruccion>();
    }

}
