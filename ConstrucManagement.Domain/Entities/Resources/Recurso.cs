using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Entities.Construction;
using ConstrucManagement.Domain.Enums;
using ConstrucManagement.Domain.Exceptions.DomainExeptions;

namespace ConstrucManagement.Domain.Entities.Resources
{
    public abstract class Recurso : BaseEntity
    {
        public string Codigo { get; protected set; }
        public string Nombre { get; protected set; }
        public string Descripcion { get; protected set; }
        public decimal CostoUnitario { get; protected set; }
        public int CantidadDisponible { get; protected set; }
        public TipoRecurso TipoRecurso { get; protected set; }

        public ICollection<AsignacionRecurso> Asignaciones { get; protected set; } = new List<AsignacionRecurso>();

        protected Recurso() { }

        protected Recurso(string codigo, string nombre, string descripcion, decimal costoUnitario, int cantidadDisponible)
        {
            Codigo = codigo ?? throw new ArgumentNullException(nameof(codigo));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));

            if (costoUnitario <= 0)
                throw new DomainException("El costo unitario debe ser mayor a cero");

            if (cantidadDisponible < 0)
                throw new DomainException("La cantidad disponible no puede ser negativa");

            CostoUnitario = costoUnitario;
            CantidadDisponible = cantidadDisponible;
        }

        protected Recurso(string codigo, string nombre, decimal costoUnitario, int cantidadDisponible)
        {
            Codigo = codigo;
            Nombre = nombre;
            CostoUnitario = costoUnitario;
            CantidadDisponible = cantidadDisponible;
        }

        public void ActualizarInformacion(string nombre, string descripcion, decimal costoUnitario)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion;

            if (costoUnitario <= 0)
                throw new DomainException("El costo unitario debe ser mayor a cero");

            CostoUnitario = costoUnitario;
            UpdateModifiedDate();
        }

        public void ActualizarCantidad(int nuevaCantidad)
        {
            if (nuevaCantidad < 0)
                throw new DomainException("La cantidad disponible no puede ser negativa");

            CantidadDisponible = nuevaCantidad;
            UpdateModifiedDate();
        }

        public void AjustarCantidad(int cantidad)
        {
            if (CantidadDisponible + cantidad < 0)
                throw new DomainException("No hay suficiente cantidad disponible");

            CantidadDisponible += cantidad;
            UpdateModifiedDate();
        }
    }
}