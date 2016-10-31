using System;
using DtoGenerator.Descriptions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.CSharp.SyntaxKind;

namespace DtoGenerator.Generator
{
    internal sealed class CodeGenerator : ICodeGenerator
    {
        private readonly TypeTable _typeTable;

        private const string ClassIndent = "\t";
        private const string PropertyIndent = "\t\t";

        public CodeGenerator(TypeTable typeTable)
        {
            if (typeTable == null) throw new ArgumentNullException(nameof(typeTable));

            _typeTable = typeTable;
        }

        public string GenerateCode(DtoClassDescription classDescription, string classNamespace)
        {
            if (classDescription == null) throw new ArgumentNullException(nameof(classDescription));
            if (string.IsNullOrEmpty(classNamespace))
                throw new ArgumentException("Value cannot be null or empty.", nameof(classNamespace));

            var namespaceDeclaration = GenerateNamespace(classNamespace);
            var classDeclaration = GenerateDtoClass(classDescription);

            foreach (var property in classDescription.Properties)
            {
                var propertyDeclaration = GenerateDtoProperty(property);
                classDeclaration = classDeclaration.AddMembers(propertyDeclaration);
            }

            var generatedCode = namespaceDeclaration.AddMembers(classDeclaration);
            return generatedCode.ToString();
        }

        private NamespaceDeclarationSyntax GenerateNamespace(string classNamespace)
        {
            var namespaceDeclaration = NamespaceDeclaration(IdentifierName(Identifier(TriviaList(),
                classNamespace, TriviaList(Space))));
            namespaceDeclaration = namespaceDeclaration.WithNamespaceKeyword(Token(TriviaList(),
                NamespaceKeyword, TriviaList(Space)));
            namespaceDeclaration = namespaceDeclaration.WithOpenBraceToken(Token(TriviaList(),
                OpenBraceToken, TriviaList(LineFeed)));

            return namespaceDeclaration;
        }

        private ClassDeclarationSyntax GenerateDtoClass(DtoClassDescription classDescription)
        {
            var classDeclaration = ClassDeclaration(Identifier(TriviaList(Space),
                classDescription.ClassName, TriviaList(Space)));
            classDeclaration = classDeclaration.WithModifiers(TokenList(Token(TriviaList(Whitespace(ClassIndent)),
                PublicKeyword, TriviaList(Space)), Token(TriviaList(), SealedKeyword, TriviaList(Space))));
            classDeclaration = classDeclaration.WithOpenBraceToken(Token(TriviaList(),
                OpenBraceToken, TriviaList(LineFeed)));

            return classDeclaration;
        }

        private MemberDeclarationSyntax GenerateDtoProperty(DtoPropertyDescription propertyDescription)
        {
            var type = _typeTable.GetNetType(propertyDescription.Type, propertyDescription.Format);

            var propertyDeclaration = PropertyDeclaration(IdentifierName(type), Identifier(TriviaList(Space),
                propertyDescription.Name, TriviaList(Space)));
            propertyDeclaration = propertyDeclaration.WithModifiers(TokenList(Token(TriviaList(Whitespace(PropertyIndent)),
                PublicKeyword, TriviaList(Space))));
            propertyDeclaration = propertyDeclaration.WithAccessorList(AccessorList(
                List(new[]{
                   AccessorDeclaration(GetAccessorDeclaration).WithKeyword(Token(GetKeyword))
                   .WithSemicolonToken(Token(TriviaList(), SemicolonToken, TriviaList(Space))),
                   AccessorDeclaration(SetAccessorDeclaration).WithKeyword(Token(SetKeyword))
                   .WithSemicolonToken(Token(TriviaList(), SemicolonToken, TriviaList(Space)))}))
                .WithOpenBraceToken(Token(TriviaList(), OpenBraceToken, TriviaList(Space)))
                .WithCloseBraceToken(Token(TriviaList(), CloseBraceToken, TriviaList(LineFeed))));

            return propertyDeclaration;
        }
    }
}
