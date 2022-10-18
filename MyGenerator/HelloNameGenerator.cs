using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace MyGenerator
{
    [Generator]
    public class HelloNameGenerator : IIncrementalGenerator
    {
        /// <summary>
        /// This is the entry point for the generator. It is called by the host (either the IDE or the command-line compiler) 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
           
            var attributes = context.SyntaxProvider
                .CreateSyntaxProvider(predicate: PredicateFunc, transform: TransformFunc)
                .Where(t => t is not null)
                .Collect();
            
            context.RegisterSourceOutput(attributes,GenerateCode);

         
            
        }

        private void GenerateCode(SourceProductionContext sourceProductionContext, ImmutableArray<ITypeSymbol?> typeSymbols)
        {
            if (typeSymbols.IsDefaultOrEmpty)
            {
                return;
            }

            foreach (var typeSymbol in typeSymbols)
            {
                if (typeSymbol is null)
                {
                    continue;
                }

                var source = SourceGenHelper.GeneratePartialClass(typeSymbol);
                sourceProductionContext.AddSource($"{typeSymbol.Name}.g.cs", SourceText.From(source, Encoding.UTF8));
            }
        }

        private static ITypeSymbol? TransformFunc(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            var attributeSyntax = (AttributeSyntax) context.Node;
            if (attributeSyntax.Parent?.Parent is not ClassDeclarationSyntax classDeclarationSyntax)
            {
                return null;
            }
            
            var type = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax) as ITypeSymbol;
            
            return type is null || !IsTypeClassWithRequiredAttribute(type) ? null : type;
        }

        private static bool IsTypeClassWithRequiredAttribute(ISymbol type)
        {
            var retValue = type.GetAttributes().Any(a => a.AttributeClass?.Name == "HelloExtensionAttribute");
            return retValue;
        }

        private bool PredicateFunc(SyntaxNode syntaxNode, CancellationToken cancellationToken)
        {
            if (syntaxNode is not AttributeSyntax attributeSyntax)
            {
                return false;
            }

            var attributeName = ExtractName(attributeSyntax.Name);
            return attributeName is "HelloExtensionAttribute" or "HelloExtension";
        }

        private static string? ExtractName(NameSyntax? nameSyntax)
        {
            return nameSyntax switch
            {
                SimpleNameSyntax simpleNameSyntax => simpleNameSyntax.Identifier.Text,
                QualifiedNameSyntax qualifiedNameSyntax => qualifiedNameSyntax.Right.Identifier.Text,
                _ => null
            };
        }


        private static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classes,
            SourceProductionContext spc)
        {
            if (classes.IsDefaultOrEmpty)
            {
                return;
            }
        
            var distinctClasses = classes.Distinct();
            foreach (var classDeclarationSyntax in distinctClasses)
            {
                var classSymbol = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree)
                    .GetDeclaredSymbol(classDeclarationSyntax)!;
                var source = SourceGenHelper.GeneratePartialClass2(classDeclarationSyntax);
                spc.AddSource($"{classSymbol.Name}.g.cs", SourceText.From(source, Encoding.UTF8));
            }
        }

        private static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext ctx)
        {
            // we know the node is a ClassDeclarationSyntax thanks to IsSyntaxTargetForGeneration
            var classDeclarationSyntax = (ClassDeclarationSyntax)ctx.Node;
        
            // loop through all the attributes on the method
            foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (var attributeSyntax in attributeListSyntax.Attributes)
                {
                  //  if (ctx.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                    //{
                        // weird, we couldn't get the symbol, ignore it
                   //     continue;
                    //}
        
                    //INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                    string fullName = attributeSyntax.Name.ToString();
        
                    // Is the attribute the [HelloExtension] attribute?
                    if (fullName == "HelloExtension")
                    {
                        // return the enum
                        return classDeclarationSyntax;
                    }
                }
            }
        
            // we didn't find the attribute we were looking for
            return null;
        }

        private static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode)
        {
            var rtnValue = syntaxNode is ClassDeclarationSyntax { AttributeLists.Count: > 0 };
        
            
          // var rtnValue = syntaxNode is CompilationUnitSyntax { AttributeLists.Count: > 0 };
            return rtnValue;
           // return true;
        }
    }
}