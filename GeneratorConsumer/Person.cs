
using MyGenerator;

namespace GeneratorConsumer;



[Duplicate(ClassName = "Person2", NameSpaceName = "PersonNameSpace")]
public class Person
{
    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        FullName = string.Concat(FirstName , " ", LastName);
    }


    public  string FirstName { get;  }
    public  string LastName { get;  }
    /// <summary>
    /// Gets the full name.
    /// </summary>
    public string FullName { get;  } 
}