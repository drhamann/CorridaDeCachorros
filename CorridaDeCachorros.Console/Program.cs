// See https://aka.ms/new-console-template for more information
using CorridaDeCachorros;

Console.WriteLine("Olá, vamos começar a corrida, para sair digite 'exit' ");


CorridaDeCachorro corridaDeCachorro = new CorridaDeCachorro();


Console.WriteLine("Vamos adicionar os corredores");

corridaDeCachorro.AdicioneCorredoresConsole();

Console.WriteLine("Vamos adicionar os apostadores");
corridaDeCachorro.AdicioneApostadoresConsole();

Console.WriteLine("Vamos realizar apostas");
corridaDeCachorro.RealizarApostasConsole();

Console.WriteLine("Correndo");
Console.WriteLine(await corridaDeCachorro.Correr());


Console.WriteLine("Fim corrida");
