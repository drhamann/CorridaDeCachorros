
namespace CorridaDeCachorros
{
    public interface ICorredor
    {
        DateTime HoraDaChegada { get; set; }
        DateTime HoraDaSaida { get; set; }
        Posicoes Posicao { get; set; }
        TimeSpan TempoPercorrido { get; set; }

        Task Correr();
        double DistanciaPercorrida();
        Task Mover();
    }
}