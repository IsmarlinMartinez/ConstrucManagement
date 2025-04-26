using ConstrucManagement.Domain.Entities.Resources;
using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConstrucManagement.Domain.Enums;
using System;

namespace ConstrucManagement.Domain.Entities.Resources
{
    public class Material : Recurso
    {
    
        public int? UnidadMedidaId { get; private set; }
        public int? StockMinimo { get; private set; }
        public string Proveedor { get; private set; }

     
        public Material(string codigo, string nombre, decimal costoUnitario, int cantidadDisponible,
                        string proveedor, int? stockMinimo = null)
            : base(codigo, nombre, costoUnitario, cantidadDisponible)
        {
            Proveedor = proveedor;
            StockMinimo = stockMinimo;
            this.TipoRecurso = TipoRecurso.Material;
        }

        public void ActualizarInformacionMaterial(string proveedor, int? stockMinimo)
        {
            Proveedor = proveedor ?? throw new ArgumentNullException(nameof(proveedor));
            StockMinimo = stockMinimo;
            UpdateModifiedDate();
        }

        public bool NecesitaReposicion()
        {
            return StockMinimo.HasValue && CantidadDisponible < StockMinimo.Value;
        }
    }
}
