using System.Runtime.CompilerServices;
using VerifyTests;

namespace MyGenerator.Tests;

public static class Bootstrap
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Enable();
    }
}