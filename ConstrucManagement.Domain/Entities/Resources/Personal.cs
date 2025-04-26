using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Enums;
using System;

namespace ConstrucManagement.Domain.Entities.Resources
{
    public class Personal : Recurso
    {
        public string Especialidad { get; private set; }
        public decimal SalarioPorHora { get; private set; }
        public string Cedula { get; private set; }
        public DateTime? FechaNacimiento { get; private set; }

        public Personal(
            string codigo,
            string nombre,
            string descripcion,
            decimal costoUnitario,
            int cantidadDisponible,
            string especialidad,
            decimal salarioPorHora,
            string cedula,
            DateTime? fechaNacimiento = null)
            : base(codigo, nombre, descripcion, costoUnitario, cantidadDisponible)
        {
            Especialidad = especialidad ?? throw new ArgumentNullException(nameof(especialidad));
            SalarioPorHora = salarioPorHora > 0 ? salarioPorHora :
                throw new ArgumentException("El salario por hora debe ser mayor a cero", nameof(salarioPorHora));
            Cedula = cedula ?? throw new ArgumentNullException(nameof(cedula));
            FechaNacimiento = fechaNacimiento;
            TipoRecurso = TipoRecurso.Personal; 
        }

        public void ActualizarInformacionPersonal(
            string especialidad,
            decimal salarioPorHora,
            string cedula,
            DateTime? fechaNacimiento)
        {
            Especialidad = especialidad ?? throw new ArgumentNullException(nameof(especialidad));

            if (salarioPorHora <= 0)
                throw new ArgumentException("El salario por hora debe ser mayor a cero", nameof(salarioPorHora));

            SalarioPorHora = salarioPorHora;
            Cedula = cedula ?? throw new ArgumentNullException(nameof(cedula));
            FechaNacimiento = fechaNacimiento;

            UpdateModifiedDate();
        }

        public void ActualizarSalario(decimal nuevoSalario)
        {
            if (nuevoSalario <= 0)
                throw new ArgumentException("El salario por hora debe ser mayor a cero", nameof(nuevoSalario));

            SalarioPorHora = nuevoSalario;
            UpdateModifiedDate();
        }

        // Método para calcular costo mensual estimado (8 horas/día, 22 días/mes)
        public decimal CalcularCostoMensual()
        {
            return SalarioPorHora * 8 * 22;
        }
    }
}
