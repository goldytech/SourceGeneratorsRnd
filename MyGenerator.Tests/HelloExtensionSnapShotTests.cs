using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MyGenerator.Attributes;
using VerifyXunit;
using Xunit;

namespace MyGenerator.Tests;

[UsesVerify]
public class HelloExtensionSnapShotTests
{
    [Fact]
    public Task CodeGen()
    {
        const string source = @"
using MyGenerator.Attributes;
namespace GeneratorConsumer;
[HelloExtension]
public partial class HelloFromConsumer
{
    public string SayHello()
    {
        return new HelloLib.Hello().SayHello();
    }

   

}";
        // return Verifier.Verify(source);
        return TestHelper.Verify(source);
    }
}

public static class TestHelper
{
    public static Task Verify(string source)
    {
        
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var metadataReferences = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(HelloLib.Hello).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(HelloExtensionAttribute).Assembly.Location)
        };
        
        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree },
            references: metadataReferences);



        var generator = new HelloNameGenerator();
        
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        
        driver = driver.RunGenerators(compilation);

        
        return Verifier.Verify(driver);
    }
}