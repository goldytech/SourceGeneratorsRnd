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