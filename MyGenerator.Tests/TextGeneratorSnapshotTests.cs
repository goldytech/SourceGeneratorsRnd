using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace MyGenerator.Tests;

[UsesVerify]
public class TextGeneratorSnapshotTests
{
    [Fact]
    public Task Text()
    {
        return TestHelper.VerifyTextExtension(""); 
    }
}