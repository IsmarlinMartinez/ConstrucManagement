using ConstructoManagement.Domain.Common;
using ConstructoManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructoManagement.Domain.Entities.Construction
{
    public class EtapaDependencia : BaseEntity
    {
        public int EtapaPredecesoraId { get; private set; }
        public EtapaConstruccion EtapaPredecesora { get; private set; }

        public int EtapaSucesoraId { get; private set; }
        public EtapaConstruccion EtapaSucesora { get; private set; }

        public string DescripcionDependencia { get; private set; }

        public EtapaDependencia(int etapaPredecesoraId, int etapaSucesoraId, string descripcionDependencia)
        {
            if (etapaPredecesoraId == etapaSucesoraId)
                throw new DomainException("Una etapa no puede depender de sí misma");

            EtapaPredecesoraId = etapaPredecesoraId;
            EtapaSucesoraId = etapaSucesoraId;
            DescripcionDependencia = descripcionDependencia;
        }
    }
}
