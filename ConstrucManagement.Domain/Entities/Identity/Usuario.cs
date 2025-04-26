using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Domain.Entities.Identity
{
    public class Usuario : BaseEntity
    {
        public string NombreUsuario { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string NombreCompleto { get; private set; }
        public bool Activo { get; private set; }
        public DateTime UltimoAcceso { get; private set; }

        private readonly List<Rol> _roles = new();
        public IReadOnlyCollection<Rol> Roles => _roles.AsReadOnly();

        public Usuario(string nombreUsuario, string email, string passwordHash,
            string nombreCompleto)
        {
            NombreUsuario = nombreUsuario ?? throw new DomainException("El nombre de usuario es requerido");
            Email = email ?? throw new DomainException("El email es requerido");
            PasswordHash = passwordHash ?? throw new DomainException("El hash de contraseña es requerido");
            NombreCompleto = nombreCompleto ?? throw new DomainException("El nombre completo es requerido");
            Activo = true;
            UltimoAcceso = DateTime.UtcNow;
        }

        public void ActualizarUltimoAcceso()
        {
            UltimoAcceso = DateTime.UtcNow;
        }

        public void AgregarRol(Rol rol)
        {
            if (rol == null)
                throw new DomainException("El rol no puede ser nulo");

            _roles.Add(rol);
        }
    }
}
