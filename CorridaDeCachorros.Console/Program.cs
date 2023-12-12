// See https://aka.ms/new-console-template for more information
using CorridaDeCachorros;

Console.WriteLine("Olá, vamos começar a corrida, para sair digite 'exit' ");


CorridaDeCachorro corridaDeCachorro = new CorridaDeCachorro();


Console.WriteLine("Vamos adicionar os corredores");

corridaDeCachorro.AdicioneCorredores();

Console.WriteLine("Vamos adicionar os apostadores");
corridaDeCachorro.AdicioneApostadores();

Console.WriteLine("Vamos realizar apostas");
corridaDeCachorro.RealizarApostas();

Console.WriteLine("Correndo");
Console.WriteLine(corridaDeCachorro.Correr());


Console.WriteLine("Fim corrida");
