using Xunit;
using FluentAssertions;

namespace CorridaDeCachorros.Tests;

public class CorridaDeCachorroTests
{
    [Fact]
    public void Deve_Iniciar_A_Corrida_Com_No_Minimo_5_Apostadores()
    {
        //Arrange
        var expectedApostadores = 5;

        //Act
        CorridaDeCachorro corridaDeCachorro
            = new CorridaDeCachorro(expectedApostadores);

        //Assert
        corridaDeCachorro.Apostadores.Count.Should().Be(expectedApostadores);
    }

    [Fact]
    public void Nao_Deve_Iniciar_A_Corrida_Com_Menos_De_5_Apostadores()
    {
        //Arrange
        var expectedApostadores = 4;

        //Act
        try
        {
            var corridaDeCachorro = new CorridaDeCachorro(expectedApostadores);
        }
        catch (ArgumentException ex)
        {
            ex.Message.Should().Be("No minimo é permitido 5 apostadores");
        }
    }

    [Fact]
    public void Deve_Iniciar_A_Corrida_Com_No_Minimo_4_Corredores()
    {
        //Arrange
        var expectedCorredores = 4;

        //Act
        CorridaDeCachorro corridaDeCachorro
            = new CorridaDeCachorro(5);

        //Assert
        corridaDeCachorro.Corredores.Count.Should().Be(expectedCorredores);
    }

    [Fact]
    public void Nao_Deve_Iniciar_A_Corrida_Com_No_Minimo_3_Corredores()
    {
        //Arrange
        var expectedCorredores = 3;

        //Act
        try
        {
            var corridaDeCachorro = new CorridaDeCachorro(5, expectedCorredores);
        }
        catch (ArgumentException ex)
        {
            ex.Message.Should().Be("No minimo é permitido 4 corredores");
        }
    }

    [Fact]
    public void Deve_Os_Corredores_Mover_Entre_Dezcm_E_Sessentacm()
    {
        //Arrange
        var corridaDeCachorro = new CorridaDeCachorro(5);
        double distanciaEsperadaMaiorQue = 0.1;
        double distanciaEsperadaMenorQue = 0.6;

        foreach (var corredor in corridaDeCachorro.Corredores)
        {
            //Act
            corredor.Mover();

            //Assert
            corredor.DistanciaPercorrida().Should().BeGreaterThanOrEqualTo(distanciaEsperadaMaiorQue);
            corredor.DistanciaPercorrida().Should().BeLessThanOrEqualTo(distanciaEsperadaMenorQue);
        }
    }

    [Fact]
    public void Deve_Permetir_O_Apostador_Escolher_Um_Corredor()
    {
        //Arrange
        var corridaDeCachorro = new CorridaDeCachorro(5);
        var corredorEsperado = corridaDeCachorro.Corredores.First();
        var apostadorEsperado = corridaDeCachorro.Apostadores.First();
        var valorDaAposta = 10;

        //Act
        corridaDeCachorro.Apostar(apostadorEsperado, corredorEsperado, valorDaAposta);

        //Assert
        apostadorEsperado.CachorroApostado.Should().Be(corredorEsperado.Id);

    }

    [Fact]
    public void Deve_Permetir_O_Apostador_Escolher_Um_Corredor_Pelos_Nomes()
    {
        //Arrange
        var corridaDeCachorro = new CorridaDeCachorro(5);
        var nomeDoApostador = "Apostador-1";
        var nomeDoCorredor = "Corredor-3"; 
        var valorDaAposta = 10;


        var apostadorExperado = 
        corridaDeCachorro.Apostadores.Find(apostador => apostador.Nome.Equals(nomeDoApostador));

        var cachorroEsperado 
        = corridaDeCachorro.Corredores.Find(corredor => corredor.Nome.Equals(nomeDoCorredor));

        //Act
        corridaDeCachorro.Apostar(nomeDoApostador, nomeDoCorredor,valorDaAposta);

        //Assert
        apostadorExperado.CachorroApostado.Should().Be(cachorroEsperado.Id);
        cachorroEsperado.Nome.Should().Be(nomeDoCorredor);
        apostadorExperado.Nome.Should().Be(nomeDoApostador);
    }

    [Fact]
    public void Deve_Executar_A_Corrida_E_Algum_Corredor_Finalizar_Ela()
    {
        //Arrange
        CorridaDeCachorro corridaDeCachorro = new CorridaDeCachorro(5);
        Random random = new Random();
        foreach (var apostador in corridaDeCachorro.Apostadores)
        {
            var cachorro = random.Next(0,corridaDeCachorro.Corredores.Count - 1);
            corridaDeCachorro.Apostar(apostador, corridaDeCachorro.Corredores[cachorro], 5.0);
        }

        //Act
        corridaDeCachorro.Correr();

        //Arrange
        corridaDeCachorro.Corredores.Exists(corredor => corredor.DistanciaPercorrida() >= 100.0).Should().BeTrue();
       
    }

    [Fact]
    public async Task Deve_Executar_A_Corrida_E_Ter_GanhadoresAsync()
    {
        //Arrange
        CorridaDeCachorro corridaDeCachorro = new CorridaDeCachorro(5);
        Random random = new Random();
        foreach (var apostador in corridaDeCachorro.Apostadores)
        {
            var cachorro = random.Next(0, corridaDeCachorro.Corredores.Count - 1);
            corridaDeCachorro.Apostar(apostador, corridaDeCachorro.Corredores[cachorro], 5.0);
        }

        //Act
        await corridaDeCachorro.Correr();

        //Arrange
        corridaDeCachorro.Primeiro.Should().NotBeNull();
        corridaDeCachorro.Segundo.Should().NotBeNull();
        corridaDeCachorro.Terceiro.Should().NotBeNull();

    }

    [Fact]
    public void Deve_Premiar_Os_Tres_Ganhadores()
    {
        //Arrange
        CorridaDeCachorro corridaDeCachorro = new CorridaDeCachorro(5);
        corridaDeCachorro.Apostadores[0].CachorroApostado = corridaDeCachorro.Corredores[0].Id;
        corridaDeCachorro.Apostadores[1].CachorroApostado = corridaDeCachorro.Corredores[1].Id;
        corridaDeCachorro.Apostadores[2].CachorroApostado = corridaDeCachorro.Corredores[2].Id;
        corridaDeCachorro.Apostadores[3].CachorroApostado = corridaDeCachorro.Corredores[3].Id;
        corridaDeCachorro.Apostadores[4].CachorroApostado = corridaDeCachorro.Corredores[0].Id;

        corridaDeCachorro.Corredores[0].Posicao = Posicoes.Primeiro;
        corridaDeCachorro.Primeiro = corridaDeCachorro.Corredores[0];

        corridaDeCachorro.Corredores[1].Posicao = Posicoes.Segundo;
        corridaDeCachorro.Segundo = corridaDeCachorro.Corredores[1];

        corridaDeCachorro.Corredores[2].Posicao = Posicoes.Terceiro;
        corridaDeCachorro.Terceiro = corridaDeCachorro.Corredores[2]; 

        //Act
        corridaDeCachorro.DefinirPremioGanhadores();

        //Arrange
        corridaDeCachorro.Primeiro.Should().NotBeNull();
        corridaDeCachorro.Segundo.Should().NotBeNull();
        corridaDeCachorro.Terceiro.Should().NotBeNull();

    }

}