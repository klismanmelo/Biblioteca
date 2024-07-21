using MySql.Data.MySqlClient;

namespace bibliote;

public class Livro
{
    public string name { get; set; }
    public bool reservado;

    public Livro(string nome)
    {
        name = nome;
    }

    public static void AdicionarLivro()
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
    public static void ListarLivros()
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
}