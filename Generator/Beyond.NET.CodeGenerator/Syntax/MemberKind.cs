namespace Beyond.NET.CodeGenerator.Syntax;

public enum MemberKind
{
    Automatic,

    Method,

    Constructor,
    Destructor,

    TypeOf,

    PropertyGetter,
    PropertySetter,

    FieldGetter,
    FieldSetter,

    EventHandlerAdder,
    EventHandlerRemover
}