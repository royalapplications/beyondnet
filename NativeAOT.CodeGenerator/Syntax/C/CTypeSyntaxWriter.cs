using System.Reflection;
using NativeAOT.CodeGenerator.Generator;
using Settings = NativeAOT.CodeGenerator.Generator.C.Settings;

namespace NativeAOT.CodeGenerator.Syntax.C;

public class CTypeSyntaxWriter: ICSyntaxWriter, ITypeSyntaxWriter
{
    public Settings Settings { get; }
    
    private readonly Dictionary<MemberTypes, ICSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new CConstructorSyntaxWriter() },
        { MemberTypes.Property, new CPropertySyntaxWriter() },
        { MemberTypes.Method, new CMethodSyntaxWriter() },
        { MemberTypes.Field, new CFieldSyntaxWriter() },
        { MemberTypes.Event, new CEventSyntaxWriter() }
    };
    
    private IDestructorSyntaxWriter m_destructorSyntaxWriter = new CDestructorSyntaxWriter();
    
    public CTypeSyntaxWriter(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public string Write(object @object, State state)
    {
        return Write((Type)@object, state);
    }

    public string Write(Type type, State state)
    {
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        
        return "// TODO (Type)";
    }
    
    private ICSyntaxWriter? GetSyntaxWriter(MemberTypes memberType)
    {
        m_syntaxWriters.TryGetValue(
            memberType,
            out ICSyntaxWriter? syntaxWriter
        );

        return syntaxWriter;
    }
}