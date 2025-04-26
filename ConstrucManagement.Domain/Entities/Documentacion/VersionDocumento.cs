using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Domain.Entities.Documentacion
{
    public class VersionDocumento : BaseEntity
    {
        public int DocumentoId { get; private set; }
        public Documento Documento { get; private set; }

        public string NumeroVersion { get; private set; }
        public string Cambios { get; private set; }
        public DateTime FechaVersion { get; private set; }
        public string AprobadoPor { get; private set; }
        public bool EsVersionActual { get; private set; }

        public VersionDocumento(int documentoId, string numeroVersion,
            string cambios, string aprobadoPor, bool esVersionActual)
        {
            DocumentoId = documentoId;
            NumeroVersion = numeroVersion ?? throw new DomainException("El número de versión es requerido");
            Cambios = cambios ?? throw new DomainException("Los cambios son requeridos");
            FechaVersion = DateTime.UtcNow;
            AprobadoPor = aprobadoPor ?? throw new DomainException("El aprobador es requerido");
            EsVersionActual = esVersionActual;
        }
    }
}
