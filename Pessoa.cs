using MySql.Data.MySqlClient;

namespace bibliote;

public class Pessoa
{
    public int id { get; set; }
    public string nome { get; set; }
    private List<Livro> livrosReservados = new List<Livro>();

    public Pessoa(string nomePessoa)
    {
        nome = nomePessoa;
    }
    
    public override string ToString()
    {
        return $"Id: {id}, Nome: {nome}";
    }
    
    public static void AdicionarPessoa()
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
    public static void ListarPessoas()
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

    public void adicionarLivro(Livro livro)
    {
        this.livrosReservados.Add(livro);
    }

    public void devolverLivro(Livro livro)
    {
        this.livrosReservados.Remove(livro);
    }
}