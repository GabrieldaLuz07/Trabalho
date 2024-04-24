using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2.Classes;
using T2.Interfaces;

namespace T2.Relatorios
{
    public class PrecoCategoria : IRelatorio
    {
        public List<Produto> Imprimir(List<Produto> produtos)
        {
            var lista = from produto in produtos
                        group produto by produto.Categoria into CategoriaAgrupada
                        select new
                        {
                            categoria = CategoriaAgrupada.Key,
                            media = CategoriaAgrupada.Average(p => p.Preco),
                        };


            return lista.ToList();
        }
    }
}
