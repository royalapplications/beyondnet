using System.Reflection;
using NativeAOT.CodeGenerator.Syntax;

namespace NativeAOT.CodeGenerator.Extensions;

public static class MemberKindExtensions
{
    public static string SwiftName(
        this MemberKind memberKind,
        MemberInfo? memberInfo 
    )
    {
        string? originalName = memberInfo?.Name;
        string? trimmedName = originalName;
        MethodBase? methodBase = memberInfo as MethodBase;
        bool isSpecialName = methodBase?.IsSpecialName ?? false;

        bool isGetter = false;
        bool isSetter = false;
        bool isAdder = false;
        bool isRemover = false;

        if (isSpecialName) {
            switch (memberKind) {
                case MemberKind.PropertyGetter:
                    isGetter = true;
                    
                    const string getterPrefix = "get_";

                    if (originalName?.StartsWith(getterPrefix) ?? false) {
                        trimmedName = originalName.Substring(getterPrefix.Length);
                    }
                    
                    break;
                case MemberKind.FieldGetter:
                    isGetter = true;
                    
                    break;
                case MemberKind.PropertySetter:
                    isSetter = true;
                    
                    const string setterPrefix = "set_";

                    if (originalName?.StartsWith(setterPrefix) ?? false) {
                        trimmedName = originalName.Substring(setterPrefix.Length);
                    }
                    
                    break;
                case MemberKind.FieldSetter:
                    isSetter = true;
                    
                    break;
                case MemberKind.EventHandlerAdder:
                    isAdder = true;
                    
                    const string adderPrefix = "add_";

                    if (originalName?.StartsWith(adderPrefix) ?? false) {
                        trimmedName = originalName.Substring(adderPrefix.Length);
                    }
                    
                    break;
                case MemberKind.EventHandlerRemover:
                    isRemover = true;
                    
                    const string removerPrefix = "remove_";

                    if (originalName?.StartsWith(removerPrefix) ?? false) {
                        trimmedName = originalName.Substring(removerPrefix.Length);
                    }
                    
                    break;
            }
        }

        if (memberKind == MemberKind.FieldGetter) {
            isGetter = true;
        } else if (memberKind == MemberKind.FieldSetter) {
            isSetter = true;
        }

        string swiftName;

        if (isGetter) {
            swiftName = $"get{trimmedName}";
        } else if (isSetter) {
            swiftName = $"set{trimmedName}";
        } else if (isAdder) {
            swiftName = $"add{trimmedName}";
        } else if (isRemover) {
            swiftName = $"remove{trimmedName}";
        } else {
            if (memberKind == MemberKind.Constructor) {
                swiftName = "convenience init?";
            } else if (memberKind == MemberKind.Destructor) {
                swiftName = "deinit";
            } else if (memberKind == MemberKind.TypeOf) {
                swiftName = "typeOf";
            } else {
                swiftName = trimmedName?.FirstCharToLower() ?? throw new Exception();
            }
        }

        return swiftName;
    }
}