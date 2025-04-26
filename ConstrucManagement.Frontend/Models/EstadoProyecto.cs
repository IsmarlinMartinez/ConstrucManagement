using System.ComponentModel.DataAnnotations;

namespace ConstrucManagement.Frontend.Controllers
{
    namespace ConstrucManagement.Domain.Construction
    {
        public class EstadoProyecto
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [MaxLength(50)]
            public string Nombre { get; set; }

            [MaxLength(200)]
            public string Descripcion { get; set; }
        }
    }
}
