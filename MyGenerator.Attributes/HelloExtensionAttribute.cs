using System;

namespace MyGenerator.Attributes;

[AttributeUsage(AttributeTargets.Class)]
[System.Diagnostics.Conditional("Sample")]
public class HelloExtensionAttribute : Attribute
{
    
}