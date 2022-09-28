namespace HelloLib;

public class Hello
{
    public string SayHello()
    {
        return "Hello World!";
    }
    
    /// <summary>
    /// This is a test method
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string SayHello(string name)
    {
        return "Hello " + name + "!";
    }
}