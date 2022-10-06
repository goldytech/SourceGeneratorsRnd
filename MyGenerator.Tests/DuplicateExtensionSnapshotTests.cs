using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace MyGenerator.Tests;

[UsesVerify]
public class DuplicateExtensionSnapshotTests
{
    [Fact]
    public Task CodeGenDuplicate()
    {
        const string source = @"

namespace GeneratorConsumer;

[Duplicate(ClassName = ""MyClass"", NameSpaceName = ""MyNamespace"")]
public class Person
{
    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        FullName = string.Concat(FirstName , "" "", LastName);
    }


    public  string FirstName { get;  }
    public  string LastName { get;  }
    /// <summary>
    /// Gets the full name.
    /// </summary>
    public string FullName { get;  } 
}";


        // return Verifier.Verify(source);
        return TestHelper.VerifyDuplicateExtension(source);
    }
}