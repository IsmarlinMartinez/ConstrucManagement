using ConstrucManagement.Domain.Common;

namespace ConstrucManagement.Domain.Entities
{
    public class Recurso : BaseEntity
    {
        public int Id { get; set; }
        public int TipoRecursoId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal CostoUnitario { get; set; }
        public int CantidadDisponible { get; set; }

        // Materiales
        public string? UnidadMedida { get; set; }
        public int? StockMinimo { get; set; }

        // Equipos
        public string? NumeroSerie { get; set; }
        public DateTime? FechaAdquisicion { get; set; }

        // Personal
        public string? Especialidad { get; set; }
        public decimal? SalarioPorHora { get; set; }

        public TipoRecurso TipoRecurso { get; set; } = null!;
        public ICollection<AsignacionRecurso> Asignaciones { get; set; } = new List<AsignacionRecurso>();
    }

}