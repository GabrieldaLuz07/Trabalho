using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2.Classes;
using T2.Interfaces;

namespace T2.Relatorios
{
    public class ExcessoEstoque : IRelatorio
    {
        public List<Produto> Imprimir(List<Produto> produtos)
        {
            var lista = from produto in produtos where (produto.Estoque >= produto.QtdVendida * 3) select produto;
            return lista.ToList();
        }
    }
}
