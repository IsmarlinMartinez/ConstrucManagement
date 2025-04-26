using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Entities.Construction;
using ConstrucManagement.Domain.Enums;
using ConstrucManagement.Domain.Exceptions;
using ConstrucManagement.Domain.Exceptions.DomainExeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Domain.Entities.Documentacion
{
    public class Documento : BaseEntity
    {
        public int ProyectoId { get; private set; }
        public Proyecto Proyecto { get; private set; }

        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public TipoDocumento Tipo { get; private set; }
        public string UbicacionFisica { get; private set; }
        public string UbicacionDigital { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public string CreadoPor { get; private set; }

        private readonly List<VersionDocumento> _versiones = new();
        public IReadOnlyCollection<VersionDocumento> Versiones => _versiones.AsReadOnly();

        public Documento(int proyectoId, string nombre, string descripcion,
            TipoDocumento tipo, string ubicacionFisica, string ubicacionDigital,
            string creadoPor)
        {
            ProyectoId = proyectoId;
            Nombre = nombre ?? throw new DomainException("El nombre del documento es requerido");
            Descripcion = descripcion;
            Tipo = tipo;
            UbicacionFisica = ubicacionFisica;
            UbicacionDigital = ubicacionDigital;
            FechaCreacion = DateTime.UtcNow;
            CreadoPor = creadoPor ?? throw new DomainException("El creador es requerido");
        }

        public void AgregarVersion(VersionDocumento version)
        {
            if (version == null)
                throw new DomainException("La versión no puede ser nula");

            _versiones.Add(version);
        }
    }
}
