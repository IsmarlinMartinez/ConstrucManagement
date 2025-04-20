using ConstrucManagement.Domain.Common;

namespace ConstrucManagement.Domain.Entities
{
    public class AsignacionRecurso : BaseEntity
    {
        public int Id { get; set; }
        public int EtapaId { get; set; }
        public int RecursoId { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaAsignacion { get; set; }

        public EtapaConstruccion Etapa { get; set; }
        public Recurso Recurso { get; set; }
    }

}
