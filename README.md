## Projects Description

1) **GeneratorConsumer**  : Is the console application which consumes the generator. It is a .NET 6 core app.
2) **MyGenerator** : Is the library which generates the data. It is a .NET Standard 2.0 library. Here is the logic of actual code generation.
3) **MyGenerator.Tests** : Is the unit test project for the library. It is a .NET 6 class library app.
4) **MyGenerator.Attributes** : Is the .NET standard 2.0 class library project which only as marker attributes codes, These attributes are used to trigger the code generation.
5) **HelloLib**: Is a .NET core class library project. This project is like the third party library for which we want to generate a wrapper method via Source Generator.


## Intent of Source Generator
The purpose of `MyGenerator` is to generate a partial class , this partial class will have one method called `SayHelloWithNameWrapper`. This method internally calls the `SayHello(name)` method
of the `Hello` class in the `HelloLib` project. The `SayHelloWithNameWrapper` method will have a `string` parameter called `name`. 