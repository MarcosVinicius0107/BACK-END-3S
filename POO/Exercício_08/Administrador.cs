using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercício_08
{

    public class Administrador : IAutenticavel
    {
        private string Senha = "admin";

        public bool Autenticar(string senha)
        {
            return senha == Senha;
        }
    }  
}
