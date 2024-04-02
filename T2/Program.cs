using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using T2.Classes;

var dataset = File.ReadAllText("..\\..\\..\\dataset.csv");

List<Produto> produtos = ProdutoParser.converterLista(dataset);
bool saiu = false;

while (!saiu)
{
    Console.WriteLine("-----MENU-----");
    Console.WriteLine("1)Produtos mais vendidos");
    Console.WriteLine("2)Produtos com mais estoque");
    Console.WriteLine("3)Categoria mais vendida");
    Console.WriteLine("4)Produtos menos vendidos");
    Console.WriteLine("5)Estoque de segurança");
    Console.WriteLine("6)Excesso de estoque");
    Console.WriteLine("7)Média de preço por categoria");
    Console.WriteLine("8)Sair");
    var opcao = Console.ReadLine();
    Console.Clear();

    switch (opcao)
    {
        case "1":
            maisVendido(produtos);
            break;
        case "2":
            maisEstoque(produtos);
            break;
        case "3":
            categoriaVendida(produtos);
            break;
        case "4":
            menosVendido(produtos);
            break;
        case "5":
            estoqueSeguranca(produtos);
            break;
        case "6":
            excessoEstoque(produtos);
            break;
        case "7":
            precoCategoria(produtos);
            break;
        case "8":
            return;
        default:
            break;
    }
    Console.WriteLine("\n");
}

static void maisVendido(List<Produto> produtos)
{
    var lista = produtos.OrderByDescending(p => p.QtdVendida).Take(5);
    Console.WriteLine("Produtos mais vendidos: ");
    foreach (Produto produto in lista)
    {
        Console.WriteLine($"{produto.Codigo} - {produto.Descricao}");
    }
}

static void maisEstoque(List<Produto> produtos)
{
    var lista = produtos.OrderByDescending(p => p.Estoque).Take(3);
    Console.WriteLine("Produtos com mais estoque: ");
    foreach (Produto produto in lista)
    {
        Console.WriteLine($"{produto.Codigo} - {produto.Descricao}: {produto.Estoque} em estoque!");
    }
}

static void categoriaVendida(List<Produto> produtos)
{
    var lista = produtos.OrderByDescending(p => p.QtdVendida).Take(1);
    Console.WriteLine("Categoria mais vendida: ");
    foreach (Produto produto in lista)
    {
        Console.WriteLine($"{produto.Categoria}");
    }
}

static void menosVendido(List<Produto> produtos)
{
    var lista = produtos.OrderBy(p => p.QtdVendida).Take(5);
    Console.WriteLine("Produtos menos vendidos: ");
    foreach (Produto produto in lista)
    {
        Console.WriteLine($"{produto.Codigo} - {produto.Descricao}: {produto.QtdVendida} vendidos!");
    }
}

static void estoqueSeguranca(List<Produto> produtos)
{
    var lista = from produto in produtos where produto.Estoque < (produto.QtdVendida * 0.33) select produto;
    foreach (var produto in lista)
    {
        Console.WriteLine($"{produto.Codigo} - {produto.Descricao}: {produto.Estoque}");
    }

}

static void excessoEstoque(List<Produto> produtos)
{
    var lista = from produto in produtos where (produto.Estoque >= produto.QtdVendida * 3) select produto;
    foreach (var produto in lista)
    {
        Console.WriteLine($"{produto.Codigo} - {produto.Descricao}: {produto.Estoque}");
    }
}

static void precoCategoria(List<Produto> produtos)
{
    var lista = from produto in produtos group produto by produto.Categoria into CategoriaAgrupada              
                select new
                {
                    categoria = CategoriaAgrupada.Key,
                    media = CategoriaAgrupada.Average(p => p.Preco),
                };


    foreach (var categoria in lista)
    {
        Console.WriteLine($"{categoria.categoria}: {categoria.media:n2}");
    }
}
