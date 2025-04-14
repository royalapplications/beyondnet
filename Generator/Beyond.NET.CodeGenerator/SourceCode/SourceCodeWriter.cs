namespace Beyond.NET.CodeGenerator.SourceCode;

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

    public SourceCodeSection AddSection(string sectionName)
    {
        var tempSection = new SourceCodeSection(sectionName);
        var actualSection = AddSection(tempSection);

        return actualSection;
    }
    #endregion API

    #region Private Helpers
    private SourceCodeSection AddSection(SourceCodeSection section)
    {
        if (m_sectionsDict.TryGetValue(section.Name, out SourceCodeSection? existingSection)) {
            return existingSection;
        }

        m_sections.Add(section);
        m_sectionsDict[section.Name] = section;

        return section;
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