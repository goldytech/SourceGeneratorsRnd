using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using VerifyXunit;

namespace MyGenerator.Tests;

public static class TestHelper
{
    public static Task VerifyHelloExtension(string source)
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

        
        return Verifier.Verify(driver).UseDirectory("TestsOutput");
    }
    
    public static Task VerifyDuplicateExtension(string source)
    {
        
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var metadataReferences = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        };
        
        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree },
            references: metadataReferences);



        var generator = new DuplicateGenerator();
        
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        
        driver = driver.RunGenerators(compilation);

        
        return Verifier.Verify(driver).UseDirectory("TestsOutput");
    }

    public static Task VerifyTextExtension(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var metadataReferences = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        };
        
        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree },
            references: metadataReferences);



        var generator = new TextGenerator();
        
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        
        driver = driver.RunGenerators(compilation);

        
        return Verifier.Verify(driver).UseDirectory("TestsOutput");
    }
}