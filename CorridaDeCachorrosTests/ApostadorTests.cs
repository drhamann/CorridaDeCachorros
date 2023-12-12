using Microsoft.VisualStudio.TestTools.UnitTesting;
using CorridaDeCachorros;
using Xunit;
using FluentAssertions;

namespace CorridaDeCachorros.Tests
{
    [TestClass()]
    public class ApostadorTests
    {

        [Fact]
        public void Deve_Um_Apostador_Iniciar_Com_Saldo_De_Vinte()
        {
            //Arrange
            var saldoEsperado = 20;

            Apostador apostador;

            //Act
            apostador = new Apostador(1);

            //Assert
            apostador.Saldo.Should().Be(saldoEsperado);
        }

       

        [Fact]
        public void CorridaDeCachorroTest()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}