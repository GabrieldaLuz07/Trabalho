using API.Database.Models;
using API.Database.Parser;
using System.Collections.Generic;
using System.IO;

namespace API.Database
{
    public class ContextDB
    {
        private readonly string _dataset;
        private readonly List<Produto> _produtos;
        public ContextDB() 
        {
            _dataset = File.ReadAllText("dataset.csv");
            _produtos = ProdutoParser.converterLista(_dataset);
        }

        public List<Produto> produtos => _produtos;

    }
}
