namespace CorridaDeCachorros;

public class Apostador : BaseModel
{
    private const double VALOR_INICIAL_CORRIDA = 20.0;

    public double Saldo { get; set; }
    public bool Apostou { get; set; }
    public Guid CachorroApostado { get; set; }

    public Apostador(int posicaoApostador) : base()
    {
        Nome = $"Apostador-{posicaoApostador}";
        BaseConstrutor();
    }
    public Apostador(string nomeApostador)
    {
        Nome = nomeApostador;
        BaseConstrutor();
    }

    private void BaseConstrutor()
    {
        Saldo = VALOR_INICIAL_CORRIDA;
    }
}
