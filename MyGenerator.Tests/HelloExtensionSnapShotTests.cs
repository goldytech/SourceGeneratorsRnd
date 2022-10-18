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
using HelloLib;
using MyGenerator;


namespace GeneratorConsumer;

[HelloExtension]
public partial class HelloFromConsumer
{
        public string SayHello()
        {
            return new Hello().SayHello();
        }
}
";
        
        return TestHelper.VerifyHelloExtension(source);
    }
}