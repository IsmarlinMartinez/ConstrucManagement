using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Domain.ValueObjects
{
    public class Direccion
    {
        public string Calle { get; }
        public string Ciudad { get; }
        public string Estado { get; }
        public string CodigoPostal { get; }
        public string Pais { get; }

        public Direccion(string calle, string ciudad, string estado, string codigoPostal, string pais)
        {
            Calle = calle ?? throw new ArgumentNullException(nameof(calle));
            Ciudad = ciudad ?? throw new ArgumentNullException(nameof(ciudad));
            Estado = estado;
            CodigoPostal = codigoPostal;
            Pais = pais ?? throw new ArgumentNullException(nameof(pais));
        }

        public override bool Equals(object obj)
        {
            return obj is Direccion direccion &&
                   Calle == direccion.Calle &&
                   Ciudad == direccion.Ciudad &&
                   Estado == direccion.Estado &&
                   CodigoPostal == direccion.CodigoPostal &&
                   Pais == direccion.Pais;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Calle, Ciudad, Estado, CodigoPostal, Pais);
        }
    }
}
