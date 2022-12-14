using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string S = "A1";
int C = 5;

SalaDeAula Sala = new SalaDeAula(S,C);

app.MapGet("/", () => "Salas e Alunos");
app.MapGet("/Listar/", () => Sala.listar());
app.MapGet("/entrar/{Nome},{Numero}", (string Nome, int Numero) => Sala.entrar(new Aluno(Nome, Numero)));
app.MapGet("/sair/{Nome}", (string Nome) => Sala.sair(Nome));
app.Run();


/*

Criar um sistema de uma
sala de aula
que permita os alunos entrarem e sairem.
 
*/
class Aluno
{
    //Variáveis da classe
    public String Nome;
    public int Numero;
    //Constructor
    public Aluno(string Nome, int Numero)
    {
        this.Nome = Nome;
        this.Numero = Numero;
    }
}
class SalaDeAula
{
    //Lista dinâmica (Não precisa de tamanho)
    List<Aluno> EmSala = new List<Aluno>();
    int capacidade;
    string SalaNome;
    //Constructor
    public SalaDeAula(string SalaNome, int capacidade)
    {
        this.capacidade = capacidade;
        this.SalaNome = SalaNome;
    }

    //Lista todos os alunos que se encontram em sala
    public String listar()
    {

        String Alunos = "Alunos em sala:\n";

        foreach (Aluno alu in EmSala)
        {
            Alunos += alu.Nome + " com o Número " + alu.Numero + "\n";
        }

        return Alunos;

    }


    //Entrar um aluno na sala
    public String entrar(Aluno a)
    {
        //Ver se existe lugar na sala selecionada
        if (EmSala.Count < capacidade)
        {
            EmSala.Add(a);
            return $"O aluno com o nome {a.Nome} entrou na sala {SalaNome}.";
        }
        else
        {
            return "Sala completa";
        }


    }

    //Sair um aluno da sala
    public String sair(string Nome)
    {
        Aluno? toRemove = null;

        foreach (Aluno alu in EmSala)
        {
            if (alu.Nome.Equals(Nome))
            {
                toRemove = alu;
                break;
            }
        }

        if (toRemove != null)
        {
            EmSala.Remove(toRemove);
            return $"O aluno {toRemove.Nome} saiu da sala.";
        }
        else
        {
            return "O aluno não se encontra na sala.";
        }
    }
}



