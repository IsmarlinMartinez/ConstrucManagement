using ConstrucManagement.Domain.Entities.Resources;
using ConstructoManagement.Domain.Common;
using ConstructoManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructoManagement.Domain.Entities.Resources
{
    public class Material : BaseEntity
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public string UnidadMedida { get; private set; }
        public decimal CostoUnitario { get; private set; }
        public int StockActual { get; private set; }
        public int StockMinimo { get; private set; }
        public string Proveedor { get; private set; }

        private readonly List<AsignacionRecurso> _asignaciones = new();
        public IReadOnlyCollection<AsignacionRecurso> Asignaciones => _asignaciones.AsReadOnly();

        public Material(string codigo, string nombre, string unidadMedida,
            decimal costoUnitario, int stockMinimo, string proveedor)
        {
            Codigo = codigo ?? throw new DomainException("El código del material es requerido");
            Nombre = nombre ?? throw new DomainException("El nombre del material es requerido");
            UnidadMedida = unidadMedida ?? throw new DomainException("La unidad de medida es requerida");
            CostoUnitario = costoUnitario > 0 ? costoUnitario
                : throw new DomainException("El costo unitario debe ser mayor a cero");
            StockMinimo = stockMinimo >= 0 ? stockMinimo
                : throw new DomainException("El stock mínimo no puede ser negativo");
            Proveedor = proveedor ?? throw new DomainException("El proveedor es requerido");
            StockActual = 0;
        }

        public void ActualizarStock(int cantidad)
        {
            if (StockActual + cantidad < 0)
                throw new DomainException("No hay suficiente stock disponible");

            StockActual += cantidad;
        }

        public void AgregarAsignacion(AsignacionRecurso asignacion)
        {
            if (asignacion == null)
                throw new DomainException("La asignación no puede ser nula");

            _asignaciones.Add(asignacion);
        }
    }
}
