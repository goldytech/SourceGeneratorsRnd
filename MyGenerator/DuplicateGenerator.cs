using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace MyGenerator;

[Generator]
public class DuplicateGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var attributes = context.SyntaxProvider
            .CreateSyntaxProvider(predicate: PredicateFunc, transform: TransformFunc)
            .Where(t => t is not null)
            .Collect();
        context.RegisterSourceOutput(attributes,GenerateCode);
    }

    private void GenerateCode(SourceProductionContext sourceProductionContext, ImmutableArray<ClassDeclarationSyntax?> classDeclarationSyntaxes)
    {
        foreach (var classDeclarationSyntax in classDeclarationSyntaxes)
        {
            var duplicateAttribute = classDeclarationSyntax!.AttributeLists
                .SelectMany(x => x.Attributes)
                .FirstOrDefault(x => x.Name.ToString() == "Duplicate");
            if (duplicateAttribute is not null)
            {
              
                var className = duplicateAttribute.ArgumentList?.Arguments.FirstOrDefault(x => x.NameEquals?.Name.ToString() == "ClassName")?.Expression.ToString();
                className = className?.Replace("\"", "");
                var nameSpaceName = duplicateAttribute.ArgumentList?.Arguments.FirstOrDefault(x => x.NameEquals?.Name.ToString() == "NameSpaceName")?.Expression.ToString();
                nameSpaceName = nameSpaceName?.Replace("\"", "");
                var source = SourceGenHelper.GenerateDuplicateClass(className, nameSpaceName, classDeclarationSyntax,"public");
                sourceProductionContext.AddSource($"{className}.g.cs", SourceText.From(source, Encoding.UTF8));
            }
        }
    }

    private ClassDeclarationSyntax? TransformFunc(GeneratorSyntaxContext generatorSyntaxContext, CancellationToken cancellationToken = default)
    {
        var classDeclarationSyntax = generatorSyntaxContext.Node as ClassDeclarationSyntax;
        return classDeclarationSyntax;
    }

    

    private bool PredicateFunc(SyntaxNode syntaxNode, CancellationToken cancellationToken = default)
    {
       // cancellationToken.ThrowIfCancellationRequested();
        var retValue = syntaxNode is ClassDeclarationSyntax { AttributeLists.Count: > 0 } classDeclarationSyntax
            && classDeclarationSyntax.AttributeLists.Any(attributeList => attributeList.Attributes.Any(attribute => attribute.Name.ToString() == "Duplicate"));
        
        return retValue;
    }
    

}