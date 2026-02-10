using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercício_05
{
    public class Funcionario : Pessoa
    {
        public double Salario;

        public Funcionario(string nome, int idade, double salario)
    
        {
            Salario = salario;
        }

        public override void ExibirDados()
        {
            pessoa.ExibirDados();
            Console.WriteLine($"Salário: {Salario}");
        }
    }
}