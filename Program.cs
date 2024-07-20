using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace bibliote;

class Programa
{
    public static void Main()
    {
        Biblio biblioteca = new Biblio();
        biblioteca.ExibirMenu();
    }
}


