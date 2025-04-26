using ConstrucManagement.Frontend.Controllers.ConstrucManagement.Domain.Construction;
using System.ComponentModel.DataAnnotations;
namespace ConstrucManagement.Domain.Construction
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; } 
        public string Cliente { get; set; } = string.Empty; 

        public string? Calle { get; set; }    
        public string? Ciudad { get; set; }   
        public string? Estado { get; set; }   
        public string? CodigoPostal { get; set; }  
        public string? Pais { get; set; }   

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinEstimada { get; set; }
        public DateTime? FechaFinReal { get; set; }

        public decimal PresupuestoInicial { get; set; }
        public int EstadoId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        public string? CreatedBy { get; set; }   
        public string? ModifiedBy { get; set; } 

        public bool IsDeleted { get; set; } = false;
    }

}

