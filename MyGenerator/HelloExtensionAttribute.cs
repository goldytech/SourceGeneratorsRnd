namespace MyGenerator;

[AttributeUsage(AttributeTargets.Class)]
[System.Diagnostics.Conditional("Sample")]
public class HelloExtensionAttribute : Attribute
{
    
}