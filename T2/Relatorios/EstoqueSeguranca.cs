using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using T2.Classes;
using T2.Interfaces;

namespace T2.Relatorios
{
    public class EstoqueSeguranca : IRelatorio
    {
        public List<Produto> Imprimir(List<Produto> produtos)
        {
            return produtos.Where(p => p.Estoque < (p.QtdVendida * 0.33)).ToList();
        }
    }
}
