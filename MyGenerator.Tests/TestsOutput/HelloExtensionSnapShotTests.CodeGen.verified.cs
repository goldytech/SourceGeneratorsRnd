﻿//HintName: HelloFromConsumer.g.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the MyGenerator source generator
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable
using HelloLib;
namespace GeneratorConsumer;
    public partial class HelloFromConsumer
    {
        public string SayHelloWithNameWrapper(string name)
        {
return new Hello().SayHello(name);
        }
    }
