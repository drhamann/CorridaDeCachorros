using CorridaDeCachorros;
using Xunit;
using FluentAssertions;

namespace CorridaDeCachorros.Tests;

public class CorredorTests
{
    [Fact]
    public void Deve_Mover_Uma_Vez()
    {
        //Arrange
        var corredor = new Corredor(1);
        double distanciaEsperadaMaiorQue = 0.1;
        double distanciaEsperadaMenorQue = 0.6;

        //Act
        corredor.Mover();

        //Assert
        corredor.DistanciaPercorrida().Should().BeGreaterThanOrEqualTo(distanciaEsperadaMaiorQue);
        corredor.DistanciaPercorrida().Should().BeLessThanOrEqualTo(distanciaEsperadaMenorQue);

    }
}