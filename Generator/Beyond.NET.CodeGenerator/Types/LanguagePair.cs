namespace Beyond.NET.CodeGenerator.Types;

public record struct LanguagePair
{
    public CodeLanguage SourceLanguage { get; }
    public CodeLanguage TargetLanguage { get; }

    public LanguagePair(CodeLanguage sourceLanguage, CodeLanguage targetLanguage)
    {
        if (sourceLanguage == targetLanguage) {
            throw new Exception("Source Language is same as Target Language");
        }
        
        SourceLanguage = sourceLanguage;
        TargetLanguage = targetLanguage;
    }
}