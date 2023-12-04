﻿using System;
using System.Collections.Generic;
using TP_POO.Controllers;
using TP_POO.Models;

namespace TP_POO.Views
{
    public class EncomendaView
    {
        #region Attributes

        private EncomendaController encomendaController;
        private ProdutoController produtoController;
        private ClienteController clienteController;
        private ColaboradorController colaboradorController;

        #endregion

        #region Constructor

        public EncomendaView(EncomendaController encomendaController, ProdutoController produtoController, ClienteController clienteController, ColaboradorController colaboradorController)
        {
            this.encomendaController = encomendaController;
            this.produtoController = produtoController;
            this.clienteController = clienteController;
            this.colaboradorController = colaboradorController;
            encomendaController.CarregaEncomendasBin("encomendas.bin");
        }

        #endregion

        #region Methods

        #region Menu Encomenda

        public void MenuEncomenda()
        {
            int opcao;
            do
            {
                Console.WriteLine("========== Encomendas ==========");
                Console.WriteLine("1. Adicionar encomenda");
                Console.WriteLine("2. Ver encomendas");
                Console.WriteLine("3. Remover encomenda");
                Console.WriteLine("4. Voltar");
                Console.Write("Escolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {
                    Opcao(opcao);
                }
                else
                {
                    Console.WriteLine("Opção inválida");
                }
            } while (opcao != 4);
        }

        private void Opcao(int opcao)
        {
            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    AdicionarEncomendaView();
                    encomendaController.SalvaEncomendasBin("encomendas.bin");
                    produtoController.SalvaProdutosBin("produtos.bin");
                    break;
                case 2:
                    Console.Clear();
                    VerEncomendasView();
                    break;
                case 3:
                    Console.Clear();
                    RemoverEncomendaView();
                    encomendaController.SalvaEncomendasBin("encomendas.bin");
                    break;
                case 4:
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }

        #endregion

        #region Outros Métodos

        private void AdicionarEncomendaView()
        {
            Console.WriteLine("Insira o ID do cliente: ");
            if (int.TryParse(Console.ReadLine(), out int idCliente))
            {
                Console.WriteLine("Insira o ID do colaborador: ");
                if (int.TryParse(Console.ReadLine(), out int idColaborador))
                {
                    Cliente cliente = clienteController.findClienteById(idCliente);
                    Colaborador colaborador = colaboradorController.findColaboradorById(idColaborador);

                    if( cliente != null && colaborador != null)
                    {
                        Encomenda novaEncomenda = new Encomenda(cliente, colaborador);

                        char adicionarMaisProdutos;
                        do
                        {
                            Console.WriteLine("Insira o ID do produto: ");
                            if (int.TryParse(Console.ReadLine(), out int idProduto))
                            {
                                Produto produtoExistente = produtoController.findProdutoById(idProduto);

                                if (produtoExistente != null)
                                {
                                    Console.WriteLine("Insira a quantidade desejada: ");
                                    if (int.TryParse(Console.ReadLine(), out int quantidade))
                                    {
                                        if (quantidade <= produtoExistente.Stock)
                                        {
                                            novaEncomenda.AdicionarProdutoQuantidade(produtoExistente, quantidade);
                                            Console.WriteLine("Produto adicionado à encomenda");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Quantidade não disponível em stock");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Quantidade inválida");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Produto não encontrado");
                                }
                            }
                            else
                            {
                                Console.WriteLine("ID do produto inválido");
                            }

                            Console.WriteLine("\nDeseja adicionar mais produtos? (S/N): ");
                            adicionarMaisProdutos = Console.ReadKey().KeyChar;
                            Console.WriteLine();
                        } while (char.ToUpper(adicionarMaisProdutos) == 'S');

                        if (encomendaController.AdicionarEncomendaController(novaEncomenda))
                        {
                            Console.WriteLine("Encomenda adicionada com sucesso");
                        }
                        else
                        {
                            Console.WriteLine("Erro a adicionar encomenda");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cliente ou colaborador não encontrados");
                    }
                }
                else
                {
                    Console.WriteLine("ID do colaborador inválido");
                }
            }
            else
            {
                Console.WriteLine("ID do cliente inválido");
            }
        }

        private void VerEncomendasView()
        {
            List<Encomenda> encomendas = encomendaController.ListarEncomendasController();

            Console.WriteLine("Lista de encomendas:\n");

            if (encomendas.Count == 0)
            {
                Console.WriteLine("Não existe nenhuma encomenda\n");
            }
            else
            {
                foreach (Encomenda encomenda in encomendas)
                {
                    Console.WriteLine($"Encomenda #{encomenda.IdEncomenda}\nColaborador: {encomenda.colaborador.Nome}\nCliente: {encomenda.cliente.Nome}\nData: {encomenda.Data}");

                    if (encomenda.Produtos.Count > 0)
                    {
                        Console.WriteLine("Produtos:");
                        for (int i = 0; i < encomenda.Produtos.Count; i++)
                        {
                            Produto produto = encomenda.Produtos[i];
                            int quantidade = encomenda.Quantidades[i];
                            Console.WriteLine($"- {produto.Nome}, Quantidade: {quantidade}\n\n");
                        }
                    }

                    Console.WriteLine();
                }
            }
        }

        private void RemoverEncomendaView()
        {
            Console.WriteLine("Insira o ID da encomenda a remover: ");
            if (int.TryParse(Console.ReadLine(), out int idEncomenda))
            {
                if (encomendaController.RemoverEncomendaController(idEncomenda))
                {
                    Console.WriteLine("Encomenda removida com sucesso");
                }
                else
                {
                    Console.WriteLine("Erro ao remover encomenda");
                }
            }
            else
            {
                Console.WriteLine("ID da encomenda inválido");
            }
        }

        #endregion

        #endregion
    }
}
