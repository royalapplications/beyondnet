namespace NativeAOT.CodeGenerator;

public class SourceCodeWriter
{
    private List<SourceCodeSection> m_sections = new();
    private Dictionary<string, SourceCodeSection> m_sectionsDict = new();

    #region API
    public IEnumerable<SourceCodeSection> Sections
    {
        get {
            return m_sections;
        }
    }
    
    public void Write(string code)
    {
        Write(code, string.Empty);
    }
    
    public void Write(string code, string sectionName)
    {
        SourceCodeSection section = GetSection(sectionName) ?? AddSection(sectionName);

        section.Code.AppendLine(code);
    }
    #endregion API

    #region Private Helpers
    private SourceCodeSection AddSection(string sectionName)
    {
        var section = new SourceCodeSection(sectionName);

        AddSection(section);

        return section;
    }
    
    private void AddSection(SourceCodeSection section)
    {
        m_sections.Add(section);
        m_sectionsDict[section.Name] = section;
    }

    private SourceCodeSection? GetSection(string? name)
    {
        if (m_sectionsDict.TryGetValue(name ?? string.Empty, out SourceCodeSection? section)) {
            return section;
        }

        return null;
    }
    #endregion Private Helpers
}