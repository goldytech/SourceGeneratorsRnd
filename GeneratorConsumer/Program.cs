// See https://aka.ms/new-console-template for more information

using GeneratorConsumer;
using PersonNameSpace;

var helloFromConsumer = new HelloFromConsumer();
Console.WriteLine(helloFromConsumer.SayHelloWithNameWrapper("afzal"));
var p2 = new Person2("afzal", "qureshi");
Console.WriteLine(p2.FullName);
