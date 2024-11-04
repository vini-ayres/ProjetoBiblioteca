using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBiblioteca
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcao;
            Livros acaoLivros = new Livros();

            do
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("| 0. Sair                            |");
                Console.WriteLine("| 1. Adicionar Livro                 |");
                Console.WriteLine("| 2. Pesquisar Livro (sintetico)     |");
                Console.WriteLine("| 3. Pesquisar Livro (análitico)     |");
                Console.WriteLine("| 4. Adicionar Exemplar              |");
                Console.WriteLine("| 5. Registrar empréstimo            |");
                Console.WriteLine("| 6. Registrar devolucao             |");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Escolha uma opcao: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    case 1:
                        AdicionarLivro(acaoLivros);
                        break;
                    case 2:
                        PesquisaSintetica(acaoLivros);
                        break;
                    case 3:
                        PesquisaAnalitica(acaoLivros);
                        break;
                    case 4:
                        AdicionarExemplar(acaoLivros);
                        break;
                    case 5:
                        RegistrarEmprestimo(acaoLivros);
                        break;
                    case 6:
                        RegistrarDevolucao(acaoLivros);
                        break;
                    default:
                        Console.WriteLine("Opção Invalida! Tente Novamente.");
                        break;
                }
                Console.WriteLine();

            } while (opcao != 0);
        }

        static void AdicionarLivro(Livros adicionarLivro)
        {
            Livro novoLivro = new Livro();

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Insira o isbn do Livro: ");
            novoLivro.Isbn = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o nome do Livro: ");
            novoLivro.Titulo = Console.ReadLine();
            Console.WriteLine("Insira o nome do Autor: ");
            novoLivro.Autor = Console.ReadLine();
            Console.WriteLine("Insira o nome da Editora: ");
            novoLivro.Editora = Console.ReadLine();
            Console.WriteLine("--------------------------------------");

            adicionarLivro.adicionar(novoLivro);
            Console.WriteLine("Livro adicionado com sucesso.");
        }

        static void PesquisaSintetica(Livros pesquisarLivro)
        {
            Console.WriteLine("Insira o numero isbn do Livro:");
            int isbn = int.Parse(Console.ReadLine());

            Livro resultado = pesquisarLivro.pesquisar(new Livro { Isbn = isbn });

            if (resultado != null)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Nome do Livro:" + resultado.Titulo);
                Console.WriteLine("Nome do Autor: " + resultado.Autor);
                Console.WriteLine("Nome da Editora: " + resultado.Editora);
                Console.WriteLine("Total de Exemplares: " + resultado.qtdeExemplares());
                Console.WriteLine("Exemplares dispóniveis: " + resultado.qtdeDisponiveis());
                Console.WriteLine("Exemplares em emprestimos: " + resultado.qtdeEmprestimo());
                Console.WriteLine("Percentual de disponibilidade: " + resultado.percDisponibilidade() + "%");
                Console.WriteLine("--------------------------------------");

            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        static void PesquisaAnalitica(Livros pesquisarLivro)
        {
            Console.WriteLine("Insira o numero isbn do Livro:");
            int isbn = int.Parse(Console.ReadLine());

            Livro resultado = pesquisarLivro.pesquisar(new Livro { Isbn = isbn });

            if (resultado != null)
            {
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Nome do Livro:" + resultado.Titulo);
                Console.WriteLine("Nome do Autor: " + resultado.Autor);
                Console.WriteLine("Nome da Editora: " + resultado.Editora);
                Console.WriteLine("Total de Exemplares: " + resultado.qtdeExemplares());
                Console.WriteLine("Exemplares dispóniveis: " + resultado.qtdeDisponiveis());
                Console.WriteLine("Exemplares em emprestimos: " + resultado.qtdeEmprestimo());
                Console.WriteLine("Percentual de disponibilidade: " + resultado.percDisponibilidade());
                Console.WriteLine("Exemplares: ");

                bool temEmprestimos = false;

                foreach (Exemplar exemplar in resultado.Exemplares)
                {
                    Console.WriteLine("-- Numero de tombo: " + exemplar.Tombo);
                    Console.WriteLine("-- Disponibilidade: " + (exemplar.disponivel() ? "Disponível" : "Indisponível"));
                    Console.WriteLine("-- Emprestimos: ");

                    if (exemplar.Emprestimos.Count > 0) 
                    {
                        temEmprestimos = true; 
                        foreach (Emprestimo emprestimo in exemplar.Emprestimos)
                        {
                            Console.WriteLine("---- Data do empréstimo: " + emprestimo.DtEmprestimo);
                            Console.WriteLine("---- Data da devolução: " + emprestimo.DtDevolucao);
                        }
                    }
                    else
                    {
                        Console.WriteLine("---- Nenhuma ação registrada."); 
                    }
                }

                if (!temEmprestimos)
                {
                    Console.WriteLine("Nenhuma ação registrada para este livro.");
                }
                Console.WriteLine("--------------------------------------");
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        static void AdicionarExemplar(Livros livros)
        {
            Console.WriteLine("Insira o número ISBN do Livro:");
            int isbn = int.Parse(Console.ReadLine());
            Livro livro = livros.pesquisar(new Livro { Isbn = isbn });

            if(livro != null)
            {
                Exemplar novoExemplar = new Exemplar();
                Console.WriteLine("Insira o numero do tombo: ");
                novoExemplar.Tombo = int.Parse(Console.ReadLine());
                livro.adicionarExemplar(novoExemplar);
                Console.WriteLine("Exemplar adicionado com sucesso.");
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        static void RegistrarEmprestimo(Livros livros)
        {
            Console.WriteLine("Insira o número ISBN do Livro:");
            int isbn = int.Parse(Console.ReadLine());

            Livro livro = livros.pesquisar(new Livro { Isbn = isbn });
            if (livro != null)
            {
                Console.WriteLine("Insira o número do tombo do exemplar:");
                int tombo = int.Parse(Console.ReadLine());
                Exemplar exemplar = livro.Exemplares.FirstOrDefault(e => e.Tombo == tombo);

                if (exemplar != null && exemplar.emprestar())
                {
                    Console.WriteLine("Empréstimo registrado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Exemplar não disponível.");
                }
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        static void RegistrarDevolucao(Livros livros)
        {
            Console.WriteLine("Insira o número ISBN do Livro:");
            int isbn = int.Parse(Console.ReadLine());

            Livro livro = livros.pesquisar(new Livro { Isbn = isbn });
            if (livro != null)
            {
                Console.WriteLine("Insira o número do tombo do exemplar:");
                int tombo = int.Parse(Console.ReadLine());
                Exemplar exemplar = livro.Exemplares.FirstOrDefault(e => e.Tombo == tombo);

                if (exemplar != null && exemplar.devolver())
                {
                    Console.WriteLine("Devolução efetuada com sucesso.");
                }
                else
                {
                    Console.WriteLine("Devolução não efetuada.");
                }
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }
    }
}
