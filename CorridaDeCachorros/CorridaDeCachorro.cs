﻿
using System.Net;
using System.Text;

namespace CorridaDeCachorros;

public class CorridaDeCachorro
{
    private const int NUMERO_MINIMO_DE_APOSTADORES = 5;
    private const int NUMERO_MINIMO_DE_CORREDORES = 4;

    public double ValorTotalDeApostas { get; set; } = 0.0;
    public List<Apostador> Apostadores { get; set; }
    public List<Corredor> Corredores { get; set; }

    public Corredor Primeiro { get; set; }
    public Corredor Segundo { get; set; }
    public Corredor Terceiro { get; set; }

    public Premio PrimeiroPremio { get; set; }
    public Premio SegundoPremio { get; set; }
    public Premio TerceiroPremio { get; set; }

    public CorridaDeCachorro()
    {
        InicializarObjetos();
    }

    public CorridaDeCachorro
        (int numeroDeApostadores = 5,
         int numeroDeCorredores = 4
        )
    {
        InicializarObjetos();
        GerarApostadoresRandomicos(numeroDeApostadores);
        GerarCorredoresRandomicos(numeroDeCorredores);

        VerificarRegrasDeApostadoresCorredores();
    }

    private void InicializarObjetos()
    {
        Apostadores = new List<Apostador>();
        Corredores = new List<Corredor>();
    }

    private void GerarCorredoresRandomicos(int numeroDeCorredores)
    {
        for (int i = 0; i < numeroDeCorredores; i++)
        {
            Corredores.Add(new Corredor(i));
        }
    }

    private void GerarApostadoresRandomicos(int numeroDeApostadores)
    {
        for (int i = 0; i < numeroDeApostadores; i++)
        {
            Apostadores.Add(new Apostador(i));
        }
    }

    private void VerificarRegrasDeApostadoresCorredores()
    {
        if (Apostadores.Count < NUMERO_MINIMO_DE_APOSTADORES)
        {
            throw new ArgumentException("No minimo é permitido 5 apostadores");
        }

        if (Corredores.Count < NUMERO_MINIMO_DE_CORREDORES)
        {
            throw new ArgumentException("No minimo é permitido 4 corredores");
        }
    }

    public void Apostar(Apostador apostador, Corredor corredor, double totalAposta)
    {
        if (apostador.Saldo < totalAposta)
        {
            throw new Exception("Não tem dinheiro");
        }

        ValorTotalDeApostas += totalAposta;
        apostador.CachorroApostado = corredor.Id;
        apostador.Saldo -= totalAposta;
    }

    public void Apostar(string NomeApostador, string NomeCorredor, double totalAposta)
    {
        var apostador = Apostadores.Find(apostador => apostador.Nome.Equals(NomeApostador));
        var cachorroCorredor = Corredores.Find(corredor => corredor.Nome.Equals(NomeCorredor));

        Apostar(apostador, cachorroCorredor, totalAposta);
    }

    public string Correr()
    {
        VerificarRegrasDeApostadoresCorredores();
        VerificarSeTodosApostaram();

        while (Corredores.Exists(corredor =>
            corredor.DistanciaPercorrida() <= 100)
            )
        {
            VerificarCorredoresEcorrer();
        }

        DefinirPremioGanhadores();

        var resumoCorrida = ResumoCorrida();
        return resumoCorrida;
    }

    private string ResumoCorrida()
    {
        //- Deve mostrar ao finalizar a corrida 
        //-Colocação dos corredores
        //-Quanto cada apostador tem de saldo

        StringBuilder resumoCorrida = new StringBuilder();
        resumoCorrida.AppendLine($"Ganhador {Primeiro.Nome}, valor para cada apostador {PrimeiroPremio.ValorParcial}");
        resumoCorrida.AppendLine($"Segundo {Segundo.Nome}, valor para cada apostador {SegundoPremio.ValorParcial}");
        resumoCorrida.AppendLine($"Terceiro {Terceiro.Nome}, valor para cada apostador {TerceiroPremio.ValorParcial}");
        foreach (var corredor in Corredores)
        {
            resumoCorrida.AppendLine($"Corredor {corredor.Nome} ficou em {corredor.Posicao}");
        }

        foreach (var apostador in Apostadores)
        {
            resumoCorrida.AppendLine($"Apostador {apostador.Nome} ficou com saldo de {apostador.Saldo}");
        }

        return resumoCorrida.ToString();


    }

    private void VerificarSeTodosApostaram()
    {
        foreach ( var apostador in Apostadores )
        {
            if(apostador.CachorroApostado.Equals(Guid.Empty))
            {
                throw new Exception("É necessario ter apostado para iniciar a corriga");
            }
        }
    }

    public void DefinirPremioGanhadores()
    {
        var apostadoresEmPrimeiro
        = Apostadores.FindAll(apostador => apostador.CachorroApostado.Equals(Primeiro.Id));
        PrimeiroPremio =
            new Premio(Posicoes.Primeiro, (ValorTotalDeApostas * 0.7), apostadoresEmPrimeiro);

        var apostadoresEmSegundo
            = Apostadores.FindAll(apostador => apostador.CachorroApostado.Equals(Segundo.Id));
        SegundoPremio =
            new Premio(Posicoes.Segundo, (ValorTotalDeApostas * 0.2), apostadoresEmSegundo);

        var apostadoresEmTerceiro
        = Apostadores.FindAll(apostador => apostador.CachorroApostado.Equals(Terceiro.Id));
        TerceiroPremio =
            new Premio(Posicoes.Terceiro, (ValorTotalDeApostas * 0.1), apostadoresEmTerceiro);

    }

    private void VerificarCorredoresEcorrer()
    {
        foreach (var corredor in Corredores)
        {
            if(corredor.Posicao != Posicoes.NaoGanho)
            {
                continue;
            }
            corredor.Mover();

            if (corredor.DistanciaPercorrida() >= 100.00)
            {
                if (Primeiro is null)
                {
                    corredor.Posicao = Posicoes.Primeiro;
                    Primeiro = corredor;
                    continue;
                }
                if (Segundo is null)
                {
                    corredor.Posicao = Posicoes.Segundo;
                    Segundo = corredor;
                    continue;
                }
                if (Terceiro is null)
                {
                    corredor.Posicao = Posicoes.Terceiro;
                    Terceiro = corredor;
                    continue;
                }
                corredor.Posicao = Posicoes.NaoGanho;
            }
        }
    }

    public void AdicionarCorredor(string nomeCachorro)
    {
        var corredor = new Corredor(nomeCachorro);
        Corredores.Add(corredor);
    }

    public void AdicionarApostador(string nomeApostador)
    {
        var apostador = new Apostador(nomeApostador);
        Apostadores.Add(apostador);
    }

    public void AdicioneCorredores()
    {
        var continuar = "s";
        do
        {
            Console.WriteLine("Diga o nome do canhorro : ");
            AdicionarCorredor(Console.ReadLine());
            if(Corredores.Count >= NUMERO_MINIMO_DE_CORREDORES)
            {
                Console.WriteLine("Gostaria de adicionar mais, 'S' para sim e 'N' para não.");
                continuar = Console.ReadLine();
            }

        } while (continuar.ToLower().Equals("s"));
    }

    public void AdicioneApostadores()
    {
        var continuar = "s";
        do
        {
            Console.WriteLine("Diga o nome do apostador : ");
            AdicionarApostador(Console.ReadLine());
            if (Apostadores.Count >= NUMERO_MINIMO_DE_APOSTADORES)
            {
                Console.WriteLine("Gostaria de adicionar mais, 'S' para sim e 'N' para não.");
                continuar = Console.ReadLine();
            }

        } while (continuar.ToLower().Equals("s"));
    }

    public void RealizarApostas()
    {
        foreach (var item in Corredores)
        {
            Console.WriteLine($"Corredor de Nome '{item.Nome}' e id '{item.Id}'");
        }
        foreach (var apostador in Apostadores)
        {
            Console.WriteLine($"Diga o nome ou id do cachorro em qual o apostador {apostador.Nome}");
            var corredor = Console.ReadLine();
            Console.WriteLine($"Digital o valor da aposta para o apostador {apostador.Nome} com saldo de {apostador.Saldo}");
            var aposta = Convert.ToDouble(Console.ReadLine());
            Apostar(apostador.Nome, corredor, aposta);
        }
    }
}
