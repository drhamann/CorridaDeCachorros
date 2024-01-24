namespace CorridaDeCachorros;

public class Premio
{
    public Premio(Posicoes posicao, double valorTotal, List<Apostador> apostadores)
    {
        Posicao = posicao;
        ValorTotal = valorTotal;
        Apostadores = apostadores;

        if (Apostadores?.Count > 0)
        {
            ValorParcial = (double)(ValorTotal / Apostadores?.Count);

            foreach (var apostador in Apostadores)
            {
                apostador.Saldo += ValorParcial;
            }
        }
        else
        {
            ValorParcial = 0;
            ValorBanca = valorTotal;
        }
    }

    public Posicoes Posicao { get; }
    public double ValorTotal { get; }
    public double ValorParcial { get; }
    public double ValorBanca { get; }
    public List<Apostador>? Apostadores { get; }

    public override string ToString()
    {
        return "";
    }
}
