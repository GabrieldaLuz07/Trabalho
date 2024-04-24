using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2.Classes;
using T2.Interfaces;

namespace T2.Relatorios
{
    public class CategoriaVendida : IRelatorio
    {
        public void Imprimir(List<Produto> produtos)
        {
            var produto = produtos.OrderByDescending(p => p.QtdVendida).Take(1).First();
            Console.WriteLine($"{produto.Categoria}");
        }
    }
}
