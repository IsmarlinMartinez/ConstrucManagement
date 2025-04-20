using ConstrucManagement.Domain.Entities.Resources;
using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Domain.Entities.Resources
{
    public class Equipo : BaseEntity
    {
        public string Codigo { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public string NumeroSerie { get; private set; }
        public DateTime FechaAdquisicion { get; private set; }
        public decimal CostoHora { get; private set; }
        public DateTime? UltimoMantenimiento { get; private set; }
        public DateTime? ProximoMantenimiento { get; private set; }
        public bool Disponible { get; private set; } = true;

        private readonly List<AsignacionRecurso> _asignaciones = new();
        public IReadOnlyCollection<AsignacionRecurso> Asignaciones => _asignaciones.AsReadOnly();

        public Equipo(string codigo, string nombre, string numeroSerie,
            DateTime fechaAdquisicion, decimal costoHora)
        {
            Codigo = codigo ?? throw new DomainException("El código del equipo es requerido");
            Nombre = nombre ?? throw new DomainException("El nombre del equipo es requerido");
            NumeroSerie = numeroSerie ?? throw new DomainException("El número de serie es requerido");
            FechaAdquisicion = fechaAdquisicion;
            CostoHora = costoHora > 0 ? costoHora
                : throw new DomainException("El costo por hora debe ser mayor a cero");
        }

        public void ProgramarMantenimiento(DateTime fechaMantenimiento)
        {
            if (fechaMantenimiento <= DateTime.UtcNow)
                throw new DomainException("La fecha de mantenimiento debe ser futura");

            UltimoMantenimiento = DateTime.UtcNow;
            ProximoMantenimiento = fechaMantenimiento;
        }

        public void ActualizarDisponibilidad(bool disponible)
        {
            Disponible = disponible;
        }

        public void AgregarAsignacion(AsignacionRecurso asignacion)
        {
            if (asignacion == null)
                throw new DomainException("La asignación no puede ser nula");

            _asignaciones.Add(asignacion);
        }
    }
}
