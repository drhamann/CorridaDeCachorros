namespace CorridaDeCachorros;

public class Premio
{
    public Premio(Posicoes posicao, double valorTotal, List<Apostador> apostadores) 
    {
        Posicao = posicao;
        ValorTotal = valorTotal;
        Apostadores = apostadores;

        ValorParcial = ValorTotal / (Apostadores?.Count > 0 ? Apostadores.Count : 1);
        foreach (var apostador in Apostadores)
        {
            apostador.Saldo += ValorParcial;
        }
    }

    public Posicoes Posicao { get; }
    public double ValorTotal { get; }
    public double ValorParcial { get; }
    public List<Apostador>? Apostadores { get; }

    public override string ToString()
    {
        return "";
    }
}
