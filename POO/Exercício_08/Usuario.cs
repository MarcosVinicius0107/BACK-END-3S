using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercício_08
{
    public class Usuario : IAutenticavel
    {
        private string Senha = "1234";

        public bool Autenticar(string senha)
        {
            return senha == Senha;
        }
    }
}
