namespace bibliote;

public class Livro
{
    public string name { get; set; }
    public bool reservado;

    public Livro(string nome)
    {
        name = nome;
    }
}