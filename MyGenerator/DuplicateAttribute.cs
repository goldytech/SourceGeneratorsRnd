namespace MyGenerator;

[AttributeUsage(AttributeTargets.Class)]
public class DuplicateAttribute : Attribute
{
   

    public string NameSpaceName { get; set; }

    public object ClassName { get; set; }
}