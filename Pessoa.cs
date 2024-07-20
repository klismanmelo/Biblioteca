namespace bibliote;

public class Pessoa
{
    public string nome { get; set; }
    private List<Livro> livrosReservados = new List<Livro>();

    public Pessoa(string nomePessoa)
    {
        nome = nomePessoa;
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