using System.Runtime.CompilerServices;
using CSharp.SourceGenerators.Extensions;
using CSharp.SourceGenerators.Extensions.Models;
using FluentAssertions;
using ProxyInterfaceSourceGenerator;
using ProxyInterfaceSourceGeneratorTests.Source;

namespace ProxyInterfaceSourceGeneratorTests;

[UsesVerify]
public class ProxyInterfaceSourceGeneratorTest
{
    [ModuleInitializer]
    public static void ModuleInitializer() => VerifySourceGenerators.Enable();

    private const bool Write = true;

    private readonly ProxyInterfaceCodeGenerator _sut;

    public ProxyInterfaceSourceGeneratorTest()
    {
        _sut = new ProxyInterfaceCodeGenerator();

        var pp = new PersonProxy(new Person());
        _ = pp.AddHuman(new HumanProxy(new Human()));
    }

    [Fact]
    public void GenerateFiles_ForStruct_Should_Not_GenerateProxyCode()
    {
        // Arrange
        var path = "./Source/IMyStruct.cs";
        var sourceFile = new SourceFile
        {
            Path = path,
            Text = File.ReadAllText(path),
            AttributeToAddToInterface = new ExtraAttribute
            {
                Name = "ProxyInterfaceGenerator.Proxy",
                ArgumentList = "typeof(ProxyInterfaceSourceGeneratorTests.Source.MyStruct)"
            }
        };

        // Act
        var result = _sut.Execute(new[] { sourceFile });

        // Assert
        result.Valid.Should().BeTrue();
        result.Files.Should().HaveCount(1);
    }

    [Fact]
    public Task GenerateFiles_ForClassWithArray_Should_GenerateCorrectFiles()
    {
        // Arrange
        var fileNames = new[]
        {
            "ProxyInterfaceSourceGeneratorTests.Source.IFoo.g.cs",
            "ProxyInterfaceSourceGeneratorTests.Source.FooProxy.g.cs"
        };

        var path = "./Source/IFoo.cs";
        var sourceFile = new SourceFile
        {
            Path = path,
            Text = File.ReadAllText(path),
            AttributeToAddToInterface = new ExtraAttribute
            {
                Name = "ProxyInterfaceGenerator.Proxy",
                ArgumentList = "typeof(ProxyInterfaceSourceGeneratorTests.Source.Foo)"
            }
        };

        // Act
        var result = _sut.Execute(new[]
        {
            sourceFile
        });

        // Assert
        result.Valid.Should().BeTrue();
        result.Files.Should().HaveCount(fileNames.Length + 1);

        // Verify
        var results = result.GeneratorDriver.GetRunResult().Results.First().GeneratedSources;
        return Verify(results);
    }

    [Fact]
    public void GenerateFiles_ForGenericType_Should_GenerateCorrectFiles()
    {
        // Arrange
        var fileNames = new[]
        {
            "ProxyInterfaceSourceGeneratorTests.Source.IGeneric.g.cs",
            "ProxyInterfaceSourceGeneratorTests.Source.Generic_T__1Proxy.g.cs"
        };

        var path = "./Source/IGeneric.cs";
        var sourceFile = new SourceFile
        {
            Path = path,
            Text = File.ReadAllText(path),
            AttributeToAddToInterface = new ExtraAttribute
            {
                Name = "ProxyInterfaceGenerator.Proxy",
                ArgumentList = "typeof(ProxyInterfaceSourceGeneratorTests.Source.Generic<>)"
            }
        };

        // Act
        var result = _sut.Execute(new[]
        {
            sourceFile
        });

        // Assert
        result.Valid.Should().BeTrue();
        result.Files.Should().HaveCount(fileNames.Length + 1);

        foreach (var fileName in fileNames.Select((fileName, index) => new { fileName, index }))
        {
            var builder = result.Files[fileName.index + 1]; // +1 means skip the attribute
            builder.Path.Should().EndWith(fileName.fileName);

            if (Write) File.WriteAllText($"../../../Destination/{fileName.fileName}", builder.Text);
            builder.Text.Should().Be(File.ReadAllText($"../../../Destination/{fileName.fileName}"));
        }
    }

    [Fact]
    public void GenerateFiles_ForClassWithOperator_Should_GenerateCorrectFiles()
    {
        // Arrange
        var fileNames = new[]
        {
            "ProxyInterfaceSourceGeneratorTests.Source.IOperatorTest.g.cs",
            "ProxyInterfaceSourceGeneratorTests.Source.OperatorTestProxy.g.cs"
        };

        var path = "./Source/IOperatorTest.cs";
        var sourceFile = new SourceFile
        {
            Path = path,
            Text = File.ReadAllText(path),
            AttributeToAddToInterface = new ExtraAttribute
            {
                Name = "ProxyInterfaceGenerator.Proxy",
                ArgumentList = "typeof(ProxyInterfaceSourceGeneratorTests.Source.OperatorTest)"
            }
        };

        // Act
        var result = _sut.Execute(new[]
        {
            sourceFile
        });

        // Assert
        result.Valid.Should().BeTrue();
        result.Files.Should().HaveCount(fileNames.Length + 1);

        foreach (var fileName in fileNames.Select((fileName, index) => new { fileName, index }))
        {
            var builder = result.Files[fileName.index + 1]; // +1 means skip the attribute
            builder.Path.Should().EndWith(fileName.fileName);

            if (Write) File.WriteAllText($"../../../Destination/{fileName.fileName}", builder.Text);
            builder.Text.Should().Be(File.ReadAllText($"../../../Destination/{fileName.fileName}"));
        }

        var name = "stef";
        var operatorTest = new OperatorTest
        {
            Name = name
        };
        string name1 = (string)operatorTest;
        name1.Should().Be(name);

        var p = new OperatorTestProxy(operatorTest);
        string name2 = (string)p;
        name2.Should().Be(name);

        var p2 = (OperatorTestProxy)name;
        p2.Should().BeEquivalentTo(new OperatorTestProxy(operatorTest));
    }

    [Fact]
    public void GenerateFiles_When_NoNamespace_Should_GenerateCorrectFiles()
    {
        // Arrange
        var fileNames = new[]
        {
            "INoNamespace.g.cs",
            "NoNamespaceProxy.g.cs"
        };

        var path = "./Source/INoNamespace.cs";
        var sourceFile = new SourceFile
        {
            Path = path,
            Text = File.ReadAllText(path),
            AttributeToAddToInterface = new ExtraAttribute
            {
                Name = "ProxyInterfaceGenerator.Proxy",
                ArgumentList = "typeof(ProxyInterfaceSourceGeneratorTests.Source.NoNamespace)"
            }
        };

        // Act
        var result = _sut.Execute(new[]
        {
            sourceFile
        });

        // Assert
        result.Valid.Should().BeTrue();
        result.Files.Should().HaveCount(fileNames.Length + 1);

        foreach (var fileName in fileNames.Select((fileName, index) => new { fileName, index }))
        {
            var builder = result.Files[fileName.index + 1]; // +1 means skip the attribute
            builder.Path.Should().EndWith(fileName.fileName);

            if (Write) File.WriteAllText($"../../../Destination/{fileName.fileName}", builder.Text);
            builder.Text.Should().Be(File.ReadAllText($"../../../Destination/{fileName.fileName}"));
        }
    }

    [Fact]
    public void GenerateFiles_When_MixedVisibility_Should_GenerateCorrectFiles()
    {
        // Arrange
        var fileNames = new[]
        {
            "ProxyInterfaceSourceGeneratorTests.Source.IMixedVisibility.g.cs",
            "ProxyInterfaceSourceGeneratorTests.Source.MixedVisibilityProxy.g.cs"
        };

        var path = "./Source/IMixedVisibility.cs";
        var sourceFile = new SourceFile
        {
            Path = path,
            Text = File.ReadAllText(path),
            AttributeToAddToInterface = new ExtraAttribute
            {
                Name = "ProxyInterfaceGenerator.Proxy",
                ArgumentList = "typeof(ProxyInterfaceSourceGeneratorTests.Source.MixedVisibility)"
            }
        };

        // Act
        var result = _sut.Execute(new[]
        {
            sourceFile
        });

        // Assert
        result.Valid.Should().BeTrue();
        result.Files.Should().HaveCount(fileNames.Length + 1);

        foreach (var fileName in fileNames.Select((fileName, index) => new { fileName, index }))
        {
            var builder = result.Files[fileName.index + 1]; // +1 means skip the attribute
            builder.Path.Should().EndWith(fileName.fileName);

            if (Write) File.WriteAllText($"../../../Destination/{fileName.fileName}", builder.Text);
            builder.Text.Should().Be(File.ReadAllText($"../../../Destination/{fileName.fileName}"));
        }
    }

    [Fact]
    public void GenerateFiles_ForSingleClass_Should_GenerateCorrectFiles()
    {
        // Arrange
        var attributeFilename = "ProxyInterfaceGenerator.ProxyAttribute.g.cs";
        var interfaceFilename = "ProxyInterfaceSourceGeneratorTests.Source.IPersonExtends.g.cs";
        var proxyClassFilename = "ProxyInterfaceSourceGeneratorTests.Source.PersonExtendsProxy.g.cs";

        var path = "./Source/IPersonExtends.cs";
        var sourceFile = new SourceFile
        {
            Path = path,
            Text = File.ReadAllText(path),
            AttributeToAddToInterface = new ExtraAttribute
            {
                Name = "ProxyInterfaceGenerator.Proxy",
                ArgumentList = new[] { "typeof(ProxyInterfaceSourceGeneratorTests.Source.PersonExtends)", "true" }
            }
        };

        // Act
        var result = _sut.Execute(new[] { sourceFile });

        // Assert
        result.Valid.Should().BeTrue();
        result.Files.Should().HaveCount(3);

        // Assert attribute
        var attribute = result.Files[0].SyntaxTree;
        attribute.FilePath.Should().EndWith(attributeFilename);

        // Assert interface
        var @interface = result.Files[1].SyntaxTree;
        @interface.FilePath.Should().EndWith(interfaceFilename);

        var interfaceCode = @interface.ToString();
        if (Write) File.WriteAllText($"../../../Destination/{interfaceFilename}", interfaceCode);
        interfaceCode.Should().NotBeNullOrEmpty().And.Be(File.ReadAllText($"../../../Destination/{interfaceFilename}"));

        // Assert Proxy
        var proxyClass = result.Files[2].SyntaxTree;
        proxyClass.FilePath.Should().EndWith(proxyClassFilename);

        var proxyCode = proxyClass.ToString();
        if (Write) File.WriteAllText($"../../../Destination/{proxyClassFilename}", proxyCode);
        proxyCode.Should().NotBeNullOrEmpty().And.Be(File.ReadAllText($"../../../Destination/{proxyClassFilename}"));
    }

    [Fact]
    public void GenerateFiles_ForTwoClasses_Should_GenerateCorrectFiles()
    {
        // Arrange
        var attributeFilename = "ProxyInterfaceGenerator.ProxyAttribute.g.cs";
        var interfaceHumanFilename = "ProxyInterfaceSourceGeneratorTests.Source.IHuman.g.cs";
        var proxyClassHumanFilename = "ProxyInterfaceSourceGeneratorTests.Source.HumanProxy.g.cs";
        var interfacePersonFilename = "ProxyInterfaceSourceGeneratorTests.Source.IPerson.g.cs";
        var proxyClassPersonFilename = "ProxyInterfaceSourceGeneratorTests.Source.PersonProxy.g.cs";

        var pathPerson = "./Source/IPerson.cs";
        var sourceFilePerson = new SourceFile
        {
            Path = pathPerson,
            Text = File.ReadAllText(pathPerson),
            AttributeToAddToInterface = new ExtraAttribute
            {
                Name = "ProxyInterfaceGenerator.Proxy",
                ArgumentList = "typeof(ProxyInterfaceSourceGeneratorTests.Source.Person)"
            }
        };

        var pathHuman = "./Source/IHuman.cs";
        var sourceFileHuman = new SourceFile
        {
            Path = pathHuman,
            Text = File.ReadAllText(pathHuman),
            AttributeToAddToInterface = new ExtraAttribute
            {
                Name = "ProxyInterfaceGenerator.Proxy",
                ArgumentList = "typeof(ProxyInterfaceSourceGeneratorTests.Source.Human)"
            }
        };

        // Act
        var result = _sut.Execute(new[] { sourceFileHuman, sourceFilePerson });

        // Assert
        result.Valid.Should().BeTrue();
        result.Files.Should().HaveCount(5);

        // Assert attribute
        var attribute = result.Files[0].SyntaxTree;
        attribute.FilePath.Should().EndWith(attributeFilename);


        // Assert interface Human
        var interfaceHuman = result.Files[1].SyntaxTree;
        interfaceHuman.FilePath.Should().EndWith(interfaceHumanFilename);

        var interfaceCodeHuman = interfaceHuman.ToString();
        if (Write) File.WriteAllText($"../../../Destination/{interfaceHumanFilename}", interfaceCodeHuman);
        interfaceCodeHuman.Should().NotBeNullOrEmpty().And.Be(File.ReadAllText($"../../../Destination/{interfaceHumanFilename}"));


        // Assert interface Person
        var interfacePerson = result.Files[2].SyntaxTree;
        interfacePerson.FilePath.Should().EndWith(interfacePersonFilename);

        var interfaceCodePerson = interfacePerson.ToString();
        if (Write) File.WriteAllText($"../../../Destination/{interfacePersonFilename}", interfaceCodePerson);
        interfaceCodePerson.Should().NotBeNullOrEmpty().And.Be(File.ReadAllText($"../../../Destination/{interfacePersonFilename}"));


        // Assert Proxy Human
        var proxyClassHuman = result.Files[3].SyntaxTree;
        proxyClassHuman.FilePath.Should().EndWith(proxyClassHumanFilename);

        var proxyCodeHuman = proxyClassHuman.ToString();
        if (Write) File.WriteAllText($"../../../Destination/{proxyClassHumanFilename}", proxyCodeHuman);
        proxyCodeHuman.Should().NotBeNullOrEmpty().And.Be(File.ReadAllText($"../../../Destination/{proxyClassHumanFilename}"));


        // Assert Proxy Person
        var proxyClassPerson = result.Files[4].SyntaxTree;
        proxyClassPerson.FilePath.Should().EndWith(proxyClassPersonFilename);

        var proxyCode = proxyClassPerson.ToString();
        if (Write) File.WriteAllText($"../../../Destination/{proxyClassPersonFilename}", proxyCode);
        proxyCode.Should().NotBeNullOrEmpty().And.Be(File.ReadAllText($"../../../Destination/{proxyClassPersonFilename}"));
    }


}