using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using API.Database.Models;

namespace API.Database.Parser
{
    public class ProdutoParser
    {
        public enum Header
        {
            codigo = 0,
            descricao = 1,
            categoria = 2,
            preco = 3,
            estoque = 4,
            qtdVendida = 5,
        }

        public static List<Produto> converterLista(string arquivo)
        {
            List<Produto> produtos = new();

            var linhas = arquivo.Split('\n').ToList();

            linhas.Remove(linhas.First());

            foreach (var linha in linhas)
            {
                Produto produto = new Produto()
                {
                    Codigo = Convert.ToInt32(linha.Split(';')[(int)Header.codigo]),
                    Descricao = linha.Split(";")[(int)Header.descricao],
                    Categoria = linha.Split(";")[(int)Header.categoria],
                    Preco = Convert.ToDouble(linha.Split(";")[(int)Header.preco], CultureInfo.InvariantCulture),
                    Estoque = Convert.ToInt32(linha.Split(";")[(int)Header.estoque]),
                    QtdVendida = Convert.ToInt32(linha.Split(";")[(int)Header.qtdVendida])
                };
                produtos.Add(produto);
            }
            return produtos;
        }
    }

}