using System.Threading.Tasks;
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
        return TestHelper.VerifyHelloExtension(source);
    }
}