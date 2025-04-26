using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Entities.Finance;
public class RubroPresupuestario : BaseEntity
    {
       
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public decimal MontoAsignado { get; private set; }
        public decimal MontoUtilizado { get; private set; }

       
        public int PresupuestoId { get; private set; }
        public Presupuesto Presupuesto { get; private set; }
        public ICollection<Gasto> Gastos { get; private set; } = new List<Gasto>();

        private RubroPresupuestario() { }

      
        public RubroPresupuestario(string nombre, decimal montoAsignado, int presupuestoId, string descripcion = null)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            MontoAsignado = montoAsignado > 0 ? montoAsignado :
                throw new ArgumentException("El monto asignado debe ser mayor a cero", nameof(montoAsignado));
            PresupuestoId = presupuestoId;
            Descripcion = descripcion;
            MontoUtilizado = 0;
        }

       
        public void ActualizarInformacion(string nombre, string descripcion)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion;
            UpdateModifiedDate();
        }

        public void ActualizarMontoAsignado(decimal nuevoMonto)
        {
            if (nuevoMonto <= 0)
                throw new ArgumentException("El monto asignado debe ser mayor a cero", nameof(nuevoMonto));

            MontoAsignado = nuevoMonto;
            UpdateModifiedDate();
        }

        public void RegistrarGasto(Gasto gasto)
        {
            if (gasto == null)
                throw new ArgumentNullException(nameof(gasto));

            if (gasto.Monto <= 0)
                throw new ArgumentException("El monto del gasto debe ser mayor a cero");

            if (MontoUtilizado + gasto.Monto > MontoAsignado)
                throw new InvalidOperationException("El gasto excede el monto asignado al rubro");

            Gastos.Add(gasto);
            MontoUtilizado += gasto.Monto;
            UpdateModifiedDate();
        }

        public decimal CalcularDisponibilidad()
        {
            return MontoAsignado - MontoUtilizado;
        }

        public decimal PorcentajeUtilizado()
        {
            return MontoAsignado == 0 ? 0 : (MontoUtilizado / MontoAsignado) * 100;
        }
    }
