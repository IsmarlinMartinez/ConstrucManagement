using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Entities.Construction;

namespace ConstrucManagement.Domain.Entities.Finance
{
    public class Gasto : BaseEntity
    {
        public string Descripcion { get; private set; }
        public decimal Monto { get; private set; }
        public DateTime FechaGasto { get; private set; }
        public string Comprobante { get; private set; }
        public bool Aprobado { get; private set; }
        public string AprobadoPor { get; private set; }
        public DateTime? FechaAprobacion { get; private set; }

      
        public int RubroId { get; private set; }
        public RubroPresupuestario Rubro { get; private set; }
        public int? EtapaId { get; private set; }
        public EtapaConstruccion Etapa { get; private set; }

  
        private Gasto() { }

        public Gasto(string descripcion, decimal monto, int rubroId, DateTime fechaGasto,
                    string comprobante = null, int? etapaId = null)
        {
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            Monto = monto > 0 ? monto :
                throw new ArgumentException("El monto debe ser mayor a cero", nameof(monto));
            RubroId = rubroId;
            EtapaId = etapaId;
            FechaGasto = fechaGasto;
            Comprobante = comprobante;
            Aprobado = false;
        }

        public void Aprobar(string aprobadoPor)
        {
            if (string.IsNullOrWhiteSpace(aprobadoPor))
                throw new ArgumentNullException(nameof(aprobadoPor));

            Aprobado = true;
            AprobadoPor = aprobadoPor;
            FechaAprobacion = DateTime.UtcNow;
            UpdateModifiedDate();
        }

        public void Rechazar()
        {
            Aprobado = false;
            AprobadoPor = null;
            FechaAprobacion = null;
            UpdateModifiedDate();
        }

        public void ActualizarInformacion(string descripcion, string comprobante)
        {
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            Comprobante = comprobante;
            UpdateModifiedDate();
        }

        public void AsociarAEtapa(int etapaId)
        {
            EtapaId = etapaId;
            UpdateModifiedDate();
        }
    }
}