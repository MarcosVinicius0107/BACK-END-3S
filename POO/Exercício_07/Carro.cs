using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercício_07
{
    public class Carro : Veiculo
    {
        public override void Mover()
        {
            Console.WriteLine("O carro está andando sobre quatro rodas.");
        }
    }
}