using System.Reflection;
using System.Text;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Types;
using Beyond.NET.Core;

using Settings = Beyond.NET.CodeGenerator.Generator.Kotlin.Settings;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public partial class KotlinTypeSyntaxWriter: IKotlinSyntaxWriter, ITypeSyntaxWriter
{
    public Settings Settings { get; }
    
    private readonly Dictionary<MemberTypes, IKotlinSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new KotlinConstructorSyntaxWriter() },
        { MemberTypes.Property, new KotlinPropertySyntaxWriter() },
        { MemberTypes.Method, new KotlinMethodSyntaxWriter() },
        { MemberTypes.Field, new KotlinFieldSyntaxWriter() },
        { MemberTypes.Event, new KotlinEventSyntaxWriter() }
    };
    
    private KotlinDestructorSyntaxWriter m_destructorSyntaxWriter = new();
    private KotlinTypeOfSyntaxWriter m_typeOfSyntaxWriter = new();
    
    public KotlinTypeSyntaxWriter(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((Type)@object, state, configuration);
    }

    public string Write(Type type, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        if (state.CSharpUnmanagedResult is null) {
            throw new Exception("No CSharpUnmanagedResult provided");
        }

        if (state.CResult is null) {
            throw new Exception("No CResult provided");
        }

        StringBuilder sb = new();
        
        return sb.ToString();
    }

    private IKotlinSyntaxWriter? GetSyntaxWriter(
        MemberKind memberKind,
        MemberTypes memberType
    )
    {
        if (memberKind == MemberKind.Destructor) {
            return m_destructorSyntaxWriter;
        } else if (memberKind == MemberKind.TypeOf) {
            return m_typeOfSyntaxWriter;
        }

        m_syntaxWriters.TryGetValue(
            memberType,
            out IKotlinSyntaxWriter? syntaxWriter
        );

        return syntaxWriter;
    }
}