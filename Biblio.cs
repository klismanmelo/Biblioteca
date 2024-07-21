using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace bibliote;

public class Biblio
{
    List<Livro> biblio = new List<Livro>();
    List<Pessoa> pessoas = new List<Pessoa>();
    
    
    public void ExibirMenu()
    {
        
        string escolha = "0";
        do
        {
            Console.WriteLine("Escolha uma opção: 0-Sair 1-Adicionar Livro 2-Reservar Livro 3- Devolver Livro 4-Adicionar Pessoa 5-Listar Livros 6-Listar Pessoas");
            escolha = Console.ReadLine();


            switch (escolha)
            {
                case "0":
                    Console.WriteLine("Sair do Programa");
                    break;
                case "1":
                    AdicionarLivro();
                    break;
                case "2":
                    ReservarLivro(biblio,pessoas);
                    break;
                case "3":
                    DevolucaoLivro(biblio,pessoas);
                    break;
                case "4":
                    AdicionarPessoa();
                    break;
                default:
                    Console.WriteLine("Número Inválido");
                    break;
                case "5":
                    ListarLivros(biblio);
                    break;
                case "6":
                    ListarPessoas();
                    break;
            }
        } while (escolha != "0");
    }
    
    static void AdicionarLivro()
    {
        string connectionString = "Server=localhost;Database=biblioteca;User Id=root;Password=klis05melo;";
        Console.WriteLine("Adicionar o NOME da LIVRO: ");
        string nomeLivro = Console.ReadLine();
        string query = "INSERT INTO Livro (Nome) VALUES (@Nome)";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand(query, connection);
                
            command.Parameters.AddWithValue("@Nome", nomeLivro);
            try
            {
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} linha(s) inserida(s) com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        Console.WriteLine("Pessoa ADICIONADA com sucesso!");
    }
    static void ReservarLivro(List<Livro> biblio, List<Pessoa> pessoas)
    {
        Console.WriteLine("Qual o nome da pessoa que deseja retirar o livro??");
        string nomePessoa = Console.ReadLine();
        Pessoa pessoaRetirarLivro =
            pessoas.Find(pessoa => pessoa.nome.Equals(nomePessoa, StringComparison.OrdinalIgnoreCase));
        if (pessoaRetirarLivro != null)
        {
            Console.WriteLine("Qual o nome do livro que deseka reservar?");
            string nomeLivro = Console.ReadLine();
            Livro livroParaReservar = biblio.Find(livro => livro.name.Equals(nomeLivro, StringComparison.OrdinalIgnoreCase));

            if (livroParaReservar != null)
            {
                if (!livroParaReservar.reservado)
                {
                    livroParaReservar.reservado = true;
                    pessoaRetirarLivro.adicionarLivro(livroParaReservar);
                    Console.WriteLine($"Livro RESERVADO com sucesso pelo(a) ! {nomePessoa}");
                }
                else
                {
                    Console.WriteLine($"O livro já está reservado pelo");
                }
            }
            else
            {
                Console.WriteLine("O livro não existe");
            }
        }
        else
        {
            Console.WriteLine("É necessário cadastrar essa pessoa no sistema!");
            Console.WriteLine("Gostaria de adiciona-la? Sim ou Nao");
            string escolhaAdicionar = Console.ReadLine();
            switch (escolhaAdicionar)
            {
                case "sim":
                    AdicionarPessoa();
                    break;
                case "nao":
                    break;
            }
        }
        
        
    }
    static void DevolucaoLivro(List<Livro> biblio, List<Pessoa> pessoas)
    {
        Console.WriteLine("Qual o nome da pessoa que deseja retirar o livro?");
        string nomePessoa = Console.ReadLine();
        Pessoa pessoaDevolverLivro =
            pessoas.Find(pessoa => pessoa.nome.Equals(nomePessoa, StringComparison.OrdinalIgnoreCase));
        if(pessoaDevolverLivro != null)
        {
            Console.WriteLine("Qual livro deseja devolver?");
            string nomeLivro = Console.ReadLine();
            Livro livroParaDevolver = biblio.Find(livro => livro.name.Equals(nomeLivro, StringComparison.OrdinalIgnoreCase));
            if (livroParaDevolver != null)
            {
                if (livroParaDevolver.reservado)
                {
                    livroParaDevolver.reservado = false;
                    pessoaDevolverLivro.devolverLivro(livroParaDevolver);
                    Console.WriteLine("Livro Devolvido com sucesso");
                }
                else
                {
                    Console.WriteLine("Este livro está disponível para retirada");
                }
            }
            else
            {
                Console.WriteLine("Ewte livro não existe!");
            }
        }
        else
        {
            Console.WriteLine("Pessoa não encontrada!");
        }
    }
    private void ListarLivros(List<Livro> biblio)
    {
        string connectionString = "Server=localhost;Database=biblioteca;User Id=root;Password=klis05melo;";
        string query = "SELECT Nome FROM Livro";
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string nome = reader["Nome"].ToString();

                            Console.WriteLine($"Nome: {nome}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhum registro encontrado.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    
    static void AdicionarPessoa()
    {
        string connectionString = "Server=localhost;Database=biblioteca;User Id=root;Password=klis05melo;";
        Console.WriteLine("Adicionar o NOME da PESSOA: ");
        string nomePessoa = Console.ReadLine();
        string query = "INSERT INTO Pessoa (Nome) VALUES (@Nome)";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand(query, connection);
                
            command.Parameters.AddWithValue("@Nome", nomePessoa);
            try
            {
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} linha(s) inserida(s) com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        Console.WriteLine("Pessoa ADICIONADA com sucesso!");
    }
    private void ListarPessoas()
    {
        string connectionString = "Server=localhost;Database=biblioteca;User Id=root;Password=klis05melo;";
        string query = "SELECT Nome FROM Pessoa";
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            MySqlCommand command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string nome = reader["Nome"].ToString();

                            Console.WriteLine($"Nome: {nome}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhum registro encontrado.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}