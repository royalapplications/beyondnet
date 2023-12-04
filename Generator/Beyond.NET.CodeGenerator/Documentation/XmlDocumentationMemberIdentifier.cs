namespace Beyond.NET.CodeGenerator;

internal struct XmlDocumentationMemberIdentifier
{
    internal string RawName { get; }

    internal XmlDocumentationMemberIdentifier(string rawName)
    {
        RawName = rawName ?? throw new ArgumentNullException(nameof(rawName));
    }

    public override string ToString()
    {
        return RawName;
    }
}