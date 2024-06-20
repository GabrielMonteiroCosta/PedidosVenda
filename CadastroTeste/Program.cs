using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using static Program;

class Program
{
    static void Main(string[] args)
    {
        // Lista para armazenar os produtos
        List<Produto> produtos = new List<Produto>();
        List<Cliente> clientes = new List<Cliente>();
        List<Pedido> pedidos = new List<Pedido>();

        Console.WriteLine("Cadastro de Produtos!");

        produtos.AddRange(new List<Produto>() {
            new(1){
                Status = true,
                Descricao = "Lápis Gaber Fastell",
                ValorCusto = 2.50,
                ValorVenda = 5,
                MargemLucro = 100
            },
            new(2){
                Status = true,
                Descricao = "Bala de Banana",
                ValorCusto = 0.30,
                ValorVenda = 1,
                MargemLucro = 234
            },
            new(3){
                Status = true,
                Descricao = "Monitor Gamer Samsung 144hz 27'",
                ValorCusto = 500,
                ValorVenda = 1250,
                MargemLucro = 150
            },
            new(4)
            {
                Status = true,
                Descricao = "Garrafa Térmica 500ml",
                ValorCusto = 12,
                MargemLucro = 60,
                ValorVenda = 19.20
            },
            new(5){
                Status = true,
                Descricao = "M&Ms Pacote 200g",
                ValorCusto = 3.98,
                MargemLucro = 88,
                ValorVenda = 7.50
            },
        });

        Cliente novoCliente = new Cliente(1)
        {
            Nome = "Gabriel",
            Status = true,
            CPF = 47457483888,
            Cidade = "Cotia",
            Rua = "Alagoinhas",
            Numero = 203,
        }; clientes.Add(novoCliente);
        Cliente novoCliente1 = new Cliente(2)
        {
            Nome = "Amanda",
            Status = true,
            CPF = 57457738488,
            Cidade = "Cotia",
            Rua = "Uganda",
            Numero = 920,
        }; clientes.Add(novoCliente1);
        Cliente novoCliente2 = new Cliente(3)
        {
            Status = true,
            Nome = "Naruto",
            CPF = 23245381052,
            Cidade = "Konoha",
            Numero = 15,
            Rua = "Lamen"
        }; clientes.Add(novoCliente2);
        Cliente novoCliente3 = new Cliente(4)
        {
            Status = true,
            Nome = "Brunão",
            CPF = 23456743477,
            Cidade = "Cotia",
            Numero = 31,
            Rua = "Rua dos Superiores"
        }; clientes.Add(novoCliente3);
        // Loop principal do programa - Opções iniciais para o usuário
        while (true)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(
                "\n(1) Começar um Pedido de Venda" +
                "\n(2) Realizar Cadastro/Atualização/Inativação ou Reativação de produtos" +
                "\n(3) Realizar o Cadastrar de um novo cliente" +
                "\n(4) Listar os Produtos do sistema" +
                "\n(5) Listar os Clientes do sistema" +
                "\n(6) Listar os Pedidos de Venda em Elaboração" +
                "\n(7) Listar os Pedidos de Venda Faturados" +
                "\n(8) Faturar pedido em Elaboração" +
                "\n(9) Atualizar o Cadastro de um Cliente" +
                "\n(10) Atualizar Pedido de Venda" +
                "\n(0) Para sair e encerrar o programa\n");
            Console.WriteLine("----------------------------------------");

            Console.Write("Selecione a Opção desejada: ");

            string opcao = Console.ReadLine();
            if (opcao == "0")
            {
                Console.WriteLine("O programa foi encerrado com sucesso!");
                break;
            }

            switch (opcao)
            {
                case "1": CriaPedido(pedidos, clientes, produtos); break;// Chama o método para criar um novo pedido
                case "2": Opcoes(produtos); break;
                case "3": CadastrarCliente(clientes); break; // Chama o método para atualizar atributos de produtos cadastrados
                case "4": ListarProdutos(produtos); break;// Chama o método para listar  os produtos cadastrados mas inativos
                case "5": ListarClientes(clientes); break; // Chama o método para reativar um produto inativo no sistema
                case "6": ListarPedidos(pedidos); break;// Chama o método para listar todos os produtos cadastrados
                case "7": ListarFaturados(pedidos); break;
                case "8": FaturarPedidos(pedidos); break;
                case "9": AtualizaCliente(clientes); break;
                case "10": AtualizarPedido(pedidos); break;
                default: mensagemPadrão(); break; // Chama o metódo para mostrar na tela a mensagem padrão de validação

            }
        }
    }
    // Método para solicitar e ler um valor numérico do usuário
    static double PegarValor(string texto)
    {
        double valor;
        while (true)
        {
            Console.Write(texto);
            if (double.TryParse(Console.ReadLine(), out valor))
            {
                return valor;
            }
            Console.WriteLine("\nDigite um valor em R$ válido.");
        }
    }
    static int PegarValorInt(string texto)
    {
        int valor;
        while (true)
        {
            Console.Write(texto);
            if (int.TryParse(Console.ReadLine(), out valor))
            {
                return valor;
            }
            Console.WriteLine("\nDigite um valor inteiro válido.");
        }
    }
    static long CpfCliente(string texto)
    {
        while (true)
        {
            long valor;
            Console.Write(texto);
            if (long.TryParse(Console.ReadLine(), out valor))
            {
                if (valor.ToString().Length != 11)
                {
                    Console.WriteLine("CPF não tem 11 números.");
                    continue;
                }
                Console.WriteLine("CPF Registrado com sucesso!");
                return valor;
            }
        }
    }
    // Método para cadastrar um novo produto
    static void CadastrarProduto(List<Produto> produtos)
    {
        int maxCodigo = 1;
        foreach (var produto in produtos)
        {
            if (produto.Codigo >= maxCodigo)
            {
                maxCodigo = produto.Codigo + 1;
            }
        }
        // Criar um novo produto com o código

        Produto novoProduto = new(maxCodigo);
        Console.Write("\nInsira a Descrição do Produto: ");
        novoProduto.Descricao = Console.ReadLine();
        novoProduto.ValorCusto = PegarValor("\nInsira o valor de Custo do produto: R$");
        novoProduto.MargemLucro = PegarValor("\nInsira a Margem de Lucro sobre o Produto(Em %): ");
        double calculoMargem = (novoProduto.ValorCusto * novoProduto.MargemLucro) / 100;
        novoProduto.ValorVenda = novoProduto.ValorCusto + calculoMargem;

        produtos.Add(novoProduto);
        Console.WriteLine($"\nProduto cadastrado com sucesso!");

    }
    // Método para listar todos os produtos cadastrados
    static void ListarProdutos(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        Console.WriteLine("\nLista de Produtos Cadastrados:\n");
        foreach (var produto in produtos)
        {
            if (produto.Status)
            {
                Console.WriteLine(produto);
            }
        }
    }
    static void ListarClientes(List<Cliente> clientes)
    {
        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente localizado.");
            return;
        }

        Console.WriteLine("Clientes já cadastrados:\n");
        foreach (var cliente in clientes)
        {
            if (cliente.Status)
            {
                Console.WriteLine(cliente);
            }
        }
    }
    static void ProdutosInativos(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Você não tem nenhum produto para ser inativado.");
        }
        foreach (var produto in produtos)
        {
            if (!produto.Status)
            {
                Console.WriteLine("Aqui está a lista de produtos inativos no seu sistema!");
                Console.WriteLine(produto);
            }
        }
    }
    static void ReativarProdutos(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado para ser reativado.");
            return;
        }

        ProdutosInativos(produtos);
        Console.WriteLine("Digite o código do produto que você deseja reativar: ");
        int codigoReativar;
        if (int.TryParse(Console.ReadLine(), out codigoReativar))
        {
            foreach (var produto in produtos)
            {
                if (!produto.Status && codigoReativar == produto.Codigo)
                {
                    Console.WriteLine($"O produto {produto.Descricao} foi Reativado com Sucesso!");
                    produto.Status = true;
                    break;
                }
            }
        }
    }
    // Método para deletar um produto cadastrado
    static void InativarProduto(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado." +
                "\nPor favor cadastre qualquer produto antes de tentar efetuar uma exclusão.");
            return;
        }

        ListarProdutos(produtos);

        Console.WriteLine("\nDigite o código do produto que você deseja inativar: ");
        if (int.TryParse(Console.ReadLine(), out int codigoInativo))
        {
            for (int i = 0; i < produtos.Count; i++)
            {

                if (produtos[i].Codigo == codigoInativo)
                {
                    produtos[i].Status = false;
                    Console.WriteLine($"Produto com código {codigoInativo} foi INATIVADO com sucesso.");
                    break;
                }
            }
        }
    }
    // Método para atualizar atributos de produtos cadastrados
    static void AtualizarProduto(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Você ainda não tem nenhum produto cadastrado!" +
                "\nPor favor cadastre o produto antes de tentar atualizar os atributos dele!");
            return;
        }

        ListarProdutos(produtos);
        Console.Write("\nDigite o CÓDIGO do produto que você deseja atualizar: ");

        if (int.TryParse(Console.ReadLine(), out int codigoAtualizar))
        {
            Produto? produtoParaAtualizar = null;

            // Encontrar o produto com o código fornecido
            foreach (var produto in produtos)
            {
                if (produto.Codigo == codigoAtualizar)
                {
                    produtoParaAtualizar = produto;
                    break;
                }
            }

            // Se o produto foi encontrado
            if (produtoParaAtualizar != null)
            {

                while (true)
                {
                    Console.Write("" +
                        "\n(1) Atualizar a Descrição do Produto" +
                        "\n(2) Atualizar o Valor de Custo do Produto" +
                        "\n(3) Atualizar a Margem de Lucro Sobre o Produto (Em %)" +
                        "\n(4) Atualizar o Valor de Venda do Produto" +
                        "\n(5) Atualizar o Código do Produto" +
                        "\n(0) Para sair da atualização do produto" +
                        "\n\nSelecione a opção para atualizar as informações de um produto: ");

                    string? opcaoAtualizar = Console.ReadLine();


                    switch (opcaoAtualizar)
                    {
                        case "1":
                            // Armazena a descrição antiga
                            string descricaoAntiga = produtoParaAtualizar.Descricao;
                            Console.WriteLine($"\nA descrição atual é [{descricaoAntiga}]");
                            Console.Write("\nInsira a nova descrição do produto: ");
                            produtoParaAtualizar.Descricao = Console.ReadLine();
                            Console.WriteLine("\nDescrição atualizada com sucesso!");
                            Console.WriteLine($"\nDescrição antiga: {descricaoAntiga} ; Nova descrição: {produtoParaAtualizar.Descricao}");
                            break;
                        case "2":
                            produtoParaAtualizar.ValorCusto = PegarValor("\nInsira o novo valor de Custo do produto: R$");
                            Console.WriteLine("Valor de Custo atualizado com sucesso!\nA partir de agora produto tem um novo Valor de Venda" +
                                " baseado no Valor de Custo atualizado!");
                            double novoCalculoMargem = (produtoParaAtualizar.ValorCusto * produtoParaAtualizar.MargemLucro) / 100;
                            produtoParaAtualizar.ValorVenda = produtoParaAtualizar.ValorCusto + novoCalculoMargem;
                            break;
                        case "3":
                            produtoParaAtualizar.MargemLucro = PegarValor("\nInsira a nova Margem de Lucro sobre o Produto: (%) ");
                            Console.WriteLine("Margem de Lucro atualizada com sucesso!");
                            double calculoMargem = (produtoParaAtualizar.ValorCusto * produtoParaAtualizar.MargemLucro) / 100;
                            produtoParaAtualizar.ValorVenda = produtoParaAtualizar.ValorCusto + calculoMargem;
                            break;
                        case "4":
                            double novoValorVenda = PegarValor("\nInsira o novo Valor de Venda do produto: R$");
                            produtoParaAtualizar.ValorVenda = novoValorVenda;
                            double novoCalculoVenda = ((produtoParaAtualizar.ValorVenda - produtoParaAtualizar.ValorCusto) / produtoParaAtualizar.ValorCusto) * 100;
                            produtoParaAtualizar.MargemLucro = novoCalculoVenda;
                            Console.WriteLine("O valor de venda foi alterado com sucesso!");
                            break;
                        case "5":
                            AtualizaCodigo(produtos);
                            break;
                        default:
                            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                            continue;
                    }
                    break;
                }
            }
        }
    }

    //todo refazer o atualizar pedido para a operação de alteração do produto ser feita de uma única vez
    static void AtualizarPedido(List<Pedido> pedidos)
    {
        Console.Write("Digite o ID do pedido que deseja atualizar: ");
        if (int.TryParse(Console.ReadLine(), out int idPedido))
        {
            bool pedidoEncontrado = false;
            foreach (var pedido in pedidos)
            {
                if (pedido.Id == idPedido)
                {
                    Console.WriteLine($"Pedido encontrado. ID: {pedido.Id}");

                    // Mostra produtos do pedido
                    Console.WriteLine("Produtos no pedido:");
                    foreach (var produtoPedido in pedido.ProdutosLista)
                    {
                        Console.WriteLine($"Código: {produtoPedido.Codigo} - Quantidade: {produtoPedido.Quantidade}");
                    }

                    Console.Write("Digite o código do produto que deseja alterar: ");
                    if (int.TryParse(Console.ReadLine(), out int codigoProduto))
                    {
                        bool produtoEncontrado = false;
                        foreach (var produtoPedido in pedido.ProdutosLista)
                        {
                            if (produtoPedido.Codigo == codigoProduto)
                            {
                                Console.Write("Digite a nova quantidade: ");
                                if (double.TryParse(Console.ReadLine(), out double novaQuantidade))
                                {
                                    produtoPedido.Quantidade = novaQuantidade;
                                    Console.WriteLine($"Quantidade do produto atualizada para {novaQuantidade}.");
                                    produtoEncontrado = true;
                                    break;
                                }
                                else
                                {
                                    mensagemPadrão();
                                }
                            }
                        }

                        if (!produtoEncontrado)
                        {
                            Console.WriteLine("Produto não encontrado no pedido.");
                        }
                    }
                    else
                    {
                        mensagemPadrão();
                    }

                    pedidoEncontrado = true;
                    break;
                }
            }

            if (!pedidoEncontrado)
            {
                Console.WriteLine("Pedido não encontrado.");
            }
        }
        else
        {
            mensagemPadrão();
        }
    }
    static void Opcoes(List<Produto> produtos)
    {
        while (true)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("(1) Cadastrar Produto" +
                "\n(2) Listar Produto" +
                "\n(3) Atualizar Produto" +
                "\n(4) Inativar Produto" +
                "\n(5) Reativar Produto" +
                "\n(0) <- Voltar");
            Console.WriteLine("----------------------------------------");

            string opcao = Console.ReadLine();
            if (opcao == "0") return;

            switch (opcao)
            {
                case "1": CadastrarProduto(produtos); break;
                case "2": ListarProdutos(produtos); break;
                case "3": AtualizarProduto(produtos); break;
                case "4": InativarProduto(produtos); break;
                case "5": ReativarProdutos(produtos); break;
                default: Console.WriteLine("Digite uma opção válida por favor."); break;
            }
        }
    }
    static void mensagemPadrão()
    {
        Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
    }
    static void AtualizaCodigo(List<Produto> produtos)
    {

        while (true)
        {
            Console.Write("Digite o código do produto que você deseja atualizar: ");
            if (int.TryParse(Console.ReadLine(), out int codigoAtualizar))
            {
                int novoCodigo;

                novoCodigo = PegarValorInt("\nDigite o novo código do produto: ");

                // Verificar se o novo código já existe
                bool produtoEncontrado = false;
                foreach (var produto in produtos)
                {
                    if (produto.Codigo == novoCodigo)
                    {
                        Console.WriteLine("Esse código de produto já existe, por favor digite outro código diferente");
                        produtoEncontrado = true;
                        break;
                    }
                }

                if (!produtoEncontrado && codigoAtualizar != novoCodigo)
                {
                    // Código não encontrado, pode atualizar
                    foreach (var produto in produtos)
                    {
                        if (produto.Codigo == codigoAtualizar)
                        {
                            produto.Codigo = novoCodigo;
                            Console.WriteLine($"Código do produto {codigoAtualizar} atualizado com sucesso para {novoCodigo}.");
                            break;
                        }
                    }
                }
                break;
            }
        }
    }
    static void CadastrarCliente(List<Cliente> clientes)
    {
        int maxCodigo = 1;
        foreach (var codigoCliente in clientes)
        {
            if (codigoCliente.Id >= maxCodigo)
            {
                maxCodigo = codigoCliente.Id + 1;
            }
        }

        Cliente cliente = new(maxCodigo);
        Console.Write("Digite o nome do cliente: ");
        cliente.Nome = Console.ReadLine();
        cliente.CPF = CpfCliente("Digite o CPF do cliente: ");
        Console.Write("Digite o Endereço do Cliente(Cidade): ");
        cliente.Cidade = Console.ReadLine();
        Console.Write("Digite o Endereço do Cliente(Rua): ");
        cliente.Rua = Console.ReadLine();
        cliente.Numero = PegarValorInt("Digite o Endereço do Cliente(Número): ");
        clientes.Add(cliente);
        Console.WriteLine("\nCliente cadastrado com sucesso!");
    }
    static void AtualizaCliente(List<Cliente> clientes)
    {
        while (true)
        {
            Console.WriteLine("\nLista de Clientes: ");
            foreach (var c in clientes)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("ID: " + c.Id + "\n" + "Nome: " + c.Nome + "\n" + "CPF: " + c.CPF);
            }
            Console.WriteLine("--------------------");
            Console.WriteLine("Qual é o ID do cliente que você deseja alterar: ");
            if (int.TryParse(Console.ReadLine(), out int clienteSelecionado))
            {
                foreach (var cliente in clientes)

                    if (clienteSelecionado == cliente.Id)
                    {
                        Console.Write("---------------------------------------" +
                            "\n(1) Nome" +
                            "\n(2) CPF" +
                            "\n(3) Endereço" +
                            "\nEm qual campo você deseja fazer a alteração: ");

                        string opcao = Console.ReadLine();

                        switch (opcao)
                        {
                            case "1":
                                string nomeAntigo = cliente.Nome; // Armazena o nome antigo do cliente
                                Console.Write("Digite o nome atualizado do cliente: ");
                                string nomeAtualizado = Console.ReadLine();
                                cliente.Nome = nomeAtualizado;
                                Console.WriteLine($"O nome do cliente foi alterado de {nomeAntigo} para {nomeAtualizado}");
                                break;
                            case "2":
                                long cpfAntigo = cliente.CPF;
                                long novoCpf = CpfCliente("Digite o CPF atualizado do cliente: ");
                                cliente.CPF = novoCpf;
                                Console.WriteLine($"O CPF do cliente foi atualizado de {cpfAntigo} para {novoCpf}");
                                break;
                            case "3":
                                string enderecoAntigoC = cliente.Cidade;
                                string enderecoAntigoR = cliente.Rua;
                                int enderecoAntigoN = cliente.Numero;
                                Console.WriteLine("Digite o nome da Cidade: ");
                                string novaCidade = Console.ReadLine();
                                Console.WriteLine("Digite o nome da Rua: ");
                                string novaRua = Console.ReadLine();
                                Console.WriteLine("Digite o novo número: ");
                                if (int.TryParse(Console.ReadLine(), out int novoNum))
                                    cliente.Cidade = novaCidade;
                                cliente.Rua = novaRua;
                                cliente.Numero = novoNum;

                                Console.WriteLine($"O endereço foi atualizado de:\n [Cidade: {enderecoAntigoC} | Rua: {enderecoAntigoR} | Número: {enderecoAntigoN}] \n Para: \n [Cidade: {novaCidade} | Rua: {novaRua} | Número: {novoNum}]");
                                break;

                        }
                        break;
                    }
            }
            break;
        }
    }
    static void CriaPedido(List<Pedido> pedidos, List<Cliente> clientes, List<Produto> produtos)
    {
        int codigoPedido = 1;
        foreach (var pedido in pedidos)
        {
            if (pedido.Id >= codigoPedido)
            {
                codigoPedido = pedido.Id + 1;
            }
        }

        Pedido novoPedido = new Pedido(codigoPedido);
        Console.WriteLine($"Pedido Criado, o número deste pedido é {codigoPedido} Status: {novoPedido.Status}");
        Console.WriteLine("Você precisa adicionar um cliente para efetuar o pedido.");

        ListarClientes(clientes);
        novoPedido.Cliente = new();
        while (true)
        {
            Console.Write("Digite o código do cliente: ");
            if (int.TryParse(Console.ReadLine(), out int codigoCliente))
            {
                bool clienteEncontrado = false;
                foreach (var cliente in clientes)
                {
                    if (cliente.Id == codigoCliente)
                    {
                        novoPedido.Cliente = cliente;
                        clienteEncontrado = true;
                        break;
                    }
                }
                if (clienteEncontrado)
                {
                    break;
                }
                Console.WriteLine("Cliente não encontrado. Tente novamente.");
            }
            Console.WriteLine("Código de cliente inválido. Digite um número inteiro.");
        }

        // Adicionar produtos ao pedido
        while (true)
        {
            ListarProdutos(produtos);
            Console.Write("Insira o código do produto que deseja adicionar (ou '0' para finalizar): ");
            if (int.TryParse(Console.ReadLine(), out int codigoProdutoDigitado))
            {
                bool produtoEncontrado, quantidadeEncontrada = false;
                //Todo: Se não tiver produto no pedido, não criar o pedido.
                if (codigoProdutoDigitado == 0)
                    break;

                foreach (var produto in produtos)
                {
                    if (produto.Codigo == codigoProdutoDigitado)
                    {
                        produtoEncontrado = true;
                        Console.Write("Insira a quantidade: ");
                        if (double.TryParse(Console.ReadLine(), out double quantidadeProduto))
                        {
                            quantidadeEncontrada = true;
                            ProdutoPedido produtoPedido = new(codigoProdutoDigitado);
                            produtoPedido.Quantidade = quantidadeProduto;
                            novoPedido.ValorTotal += produto.ValorVenda * quantidadeProduto;
                            novoPedido.QuantidadeTotal += quantidadeProduto;
                            novoPedido.ProdutosLista.Add(produtoPedido);
                            Console.WriteLine($"Produto [{produto.Descricao}] Adicionado ao pedido com sucesso, com a Quantidade: [{quantidadeProduto}] e o Valor de Venda é de [R${produtoPedido.ValorVenda}] Cada - TOTAL PRODUTO: R${produtoPedido.ValorVenda * quantidadeProduto}");
                            break;

                        }
                    }
                    if (!quantidadeEncontrada)
                    {
                        Console.WriteLine("Quantidade inválida. Digite um número válido.");
                        continue;
                    }
                }
            }
            Console.WriteLine("Código de produto inválido. Digite um número inteiro.");
        }

        while (true)
        {
            Console.Write("Você gostaria de Faturar o pedido agora (S/N): ");
            string? faturarPedido = Console.ReadLine();
            if ("s".Equals(faturarPedido, StringComparison.OrdinalIgnoreCase))
            {
                novoPedido.Status = PedidoStatus.Faturado;
                pedidos.Add(novoPedido);
                Console.WriteLine("Pedido criado e faturado com sucesso!");
                break;
            }
            if ("n".Equals(faturarPedido, StringComparison.OrdinalIgnoreCase))
            {
                pedidos.Add(novoPedido);
                Console.WriteLine("Pedido criado com sucesso!");
                Console.WriteLine("Status do pedido é {ELABORAÇÃO}");
                break;
            }
        }
    }
    static void ListarPedidos(List<Pedido> pedidos)
    {
        if (pedidos.Count == 0)
        {
            Console.WriteLine("Você não tem nenhum pedido cadastrado.");
            return;
        }
        Console.WriteLine("Aqui está a lista de pedidos em ELABORAÇÃO:");
        foreach (var pedido in pedidos)
        {
            if (pedido.Status == PedidoStatus.Elaboração)
            {
                Console.WriteLine(pedido);
                Console.WriteLine("\n**Produtos do Pedido:**");
                for (int i = 0; i < pedido.ProdutosLista.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - Produto: ");
                }
                Console.WriteLine($"\n QUANTIDADE TOTAL: {pedido.QuantidadeTotal}");
                Console.WriteLine($"\n VALOR TOTAL: R${pedido.ValorTotal}");
            }
        }
    }
    static void FaturarPedidos(List<Pedido> pedidos)
    {
        if (pedidos.Count == 0)
        {
            Console.WriteLine("Nenhum pedido cadastrado.");
            return;
        }

        Console.Write("Qual é o ID do pedido que você deseja faturar: ");
        if (int.TryParse(Console.ReadLine(), out int faturarId))
        {
            bool pedidoEncontrado = false;
            foreach (var pedido in pedidos)
            {
                if (pedido.Id == faturarId)
                {
                    if (pedido.Status == PedidoStatus.Faturado)
                    {
                        Console.WriteLine("Este pedido já está faturado.");
                    }
                    else
                    {
                        pedido.Status = PedidoStatus.Faturado;
                        Console.WriteLine("Pedido faturado com sucesso.");
                    }
                    pedidoEncontrado = true;
                    break;
                }
            }

            if (!pedidoEncontrado)
            {
                Console.WriteLine("Número de pedido não encontrado.");
            }
        }
        else
        {
            Console.WriteLine("ID do pedido inválido.");
        }
    }
    static void ListarFaturados(List<Pedido> pedidos)
    {
        if (pedidos.Count == 0)
        {
            Console.WriteLine("Não existe nenhum pedido faturado no sistema.");
        }
        else
        {
            Console.WriteLine("Exibindo a lista de pedidos faturados");
            foreach (var pedido in pedidos)
            {
                if (pedido.Status == PedidoStatus.Faturado)
                {
                    Console.WriteLine(pedido);
                }
            }
        }
    }

    public class Cliente
    {
        private int id;

        public Cliente(int _id)
        {
            this.id = _id;
        }

        public Cliente()
        {

        }
        public string Nome { get; set; }
        public long CPF { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public bool Status { get; set; } = true;

        public override string ToString()
        {
            return $"\n*************************\nID: {id} | Status: {(Status ? "Ativo" : "Inativo")} \nNome: {Nome}\nCPF: {CPF}\n*** Endereço ***\nCidade: {Cidade} | Rua: {Rua} | Número: {Numero}\n*************************";
        }
    }
    // Classe que representa um Produto, todos os seus getters e setters
    public class Produto
    {
        private int codigo;

        public Produto(int _codigo)
        {
            Codigo = _codigo;
        }

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public bool Status { get; set; } = true;
        public string Descricao { get; set; }
        public double ValorVenda { get; set; }
        public double ValorCusto { get; set; }
        public double MargemLucro { get; set; }

        // Sobrescrita do método ToString para formatar a exibição do produto
        public override string ToString()
        {
            return $"\n*************************\nCódigo: {Codigo} | {(Status ? "Ativo" : "Inativo")} \nDescrição: {Descricao}\nValor de Custo: R${ValorCusto} \nMargem de Lucro: {MargemLucro}%\nValor de Venda: R${ValorVenda}\n*************************";
        }
    }
    public enum PedidoStatus
    {
        Elaboração = 0,
        Faturado = 1,
        Cancelado = 2
    }


    public class Pedido
    {
        public Pedido(int _id)
        {
            Id = _id;
            Status = PedidoStatus.Elaboração;
            ProdutosLista = new List<ProdutoPedido>();
        }
        public int Id { get; set; }
        public PedidoStatus Status { get; set; }
        public Cliente Cliente { get; set; }
        //public Produto Produtos { get; set; }
        public List<ProdutoPedido> ProdutosLista { get; set; }
        public double ValorTotal { get; set; }
        public double QuantidadeTotal { get; set; }
        public override string ToString()
        {
            return $"\n*************************\nNúmero do Pedido: {Id}\nStatus: {Status}\nCliente: {Cliente.Nome} | CPF: {Cliente.CPF}";
        }
    }

    public class ProdutoPedido : Produto
    {
        public ProdutoPedido(int id) : base(id) { }
        public double Quantidade { get; set; }
    }
}
