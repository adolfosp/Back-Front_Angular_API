using System.Collections.Generic;

namespace ProEventos.Domain.Identity
{
    internal class Role
    {
        public IEnumerable<UserRole> FuncoesUsuarios { get; set; }
    }
}
