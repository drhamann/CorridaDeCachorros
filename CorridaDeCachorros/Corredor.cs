namespace CorridaDeCachorros;

public class Corredor : BaseModel, ICorredor
{
    private static readonly Random Random = new();
    public double _distanciaPercorrida { get; set; }
    public Posicoes Posicao { get; set; }
    public DateTime HoraDaChegada { get; set; }
    public DateTime HoraDaSaida { get; set; }
    public TimeSpan TempoPercorrido { get; set; }

    public Corredor(int posicaoCorredor) : base()
    {
        Nome = $"Corredor-{posicaoCorredor}";
        BaseConstrutor();
    }
    public Corredor(string NomeCorredor) : base()
    {
        Nome = NomeCorredor;
        BaseConstrutor();
    }

    private void BaseConstrutor()
    {
        _distanciaPercorrida = 0.0;
        Posicao = Posicoes.NaoGanho;
    }

    public virtual async Task Mover()
    {
        int distanciaPercorrida = Random.Next(1, 6);

        _distanciaPercorrida += (distanciaPercorrida * 0.1);
    }

    public double DistanciaPercorrida()
    {
        return _distanciaPercorrida;
    }

    public int DistanciaPercorridaWeb()
    {
        return ((int)Math.Round(_distanciaPercorrida) * 10) - 250;
    }

    public async Task Correr()
    {
        HoraDaSaida = DateTime.Now;
        do
        {
            await Mover();

            //Thread.Sleep((int)DistanciaPercorrida() * 2);
        } while (DistanciaPercorrida() < 100.00);
        HoraDaChegada = DateTime.Now;
        TempoPercorrido = HoraDaChegada - HoraDaSaida;
    }
}
