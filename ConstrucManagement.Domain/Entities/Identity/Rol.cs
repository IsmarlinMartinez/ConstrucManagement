using ConstrucManagement.Domain.Common;
using ConstrucManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstrucManagement.Domain.Entities.Identity
{
    public class Rol : BaseEntity
    {
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }

        private readonly List<Usuario> _usuarios = new();
        public IReadOnlyCollection<Usuario> Usuarios => _usuarios.AsReadOnly();

        public Rol(string nombre, string descripcion)
        {
            Nombre = nombre ?? throw new DomainException("El nombre del rol es requerido");
            Descripcion = descripcion;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new DomainException("El usuario no puede ser nulo");

            _usuarios.Add(usuario);
        }
    }
}
