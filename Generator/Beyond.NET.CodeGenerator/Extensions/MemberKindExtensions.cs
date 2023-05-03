using System.Reflection;
using Beyond.NET.CodeGenerator.Syntax;

namespace Beyond.NET.CodeGenerator.Extensions;

public static class MemberKindExtensions
{
    public static string SwiftName(
        this MemberKind memberKind,
        MemberInfo? memberInfo 
    )
    {
        const string getterPrefix = "get_";
        const string getterSuffix = "";
        
        const string setterPrefix = "set_";
        const string setterSuffix = "_set";
        
        const string adderPrefix = "add_";
        const string adderSuffix = "_add";
        
        const string removerPrefix = "remove_";
        const string removerSuffix = "_remove";
        
        string? originalName = memberInfo?.Name;
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

                    if (originalName?.StartsWith(getterPrefix) ?? false) {
                        originalName = originalName.Substring(getterPrefix.Length);
                    }

                    break;
                case MemberKind.FieldGetter:
                    isGetter = true;
                    
                    break;
                case MemberKind.PropertySetter:
                    isSetter = true;
                    
                    if (originalName?.StartsWith(setterPrefix) ?? false) {
                        originalName = originalName.Substring(setterPrefix.Length);
                    }
                    
                    break;
                case MemberKind.FieldSetter:
                    isSetter = true;
                    
                    break;
                case MemberKind.EventHandlerAdder:
                    isAdder = true;
                    
                    if (originalName?.StartsWith(adderPrefix) ?? false) {
                        originalName = originalName.Substring(adderPrefix.Length);
                    }
                    
                    break;
                case MemberKind.EventHandlerRemover:
                    isRemover = true;
                    
                    if (originalName?.StartsWith(removerPrefix) ?? false) {
                        originalName = originalName.Substring(removerPrefix.Length);
                    }
                    
                    break;
            }
        }

        if (memberKind == MemberKind.FieldGetter) {
            isGetter = true;
        } else if (memberKind == MemberKind.FieldSetter) {
            isSetter = true;
        }

        string? swiftName = originalName?.FirstCharToLower();

        bool needsEscaping = true;

        if (isGetter) {
            swiftName = swiftName + getterSuffix;
        } else if (isSetter) {
            swiftName = swiftName + setterSuffix;
        } else if (isAdder) {
            swiftName = swiftName + adderSuffix;
        } else if (isRemover) {
            swiftName = swiftName + removerSuffix;
        } else {
            if (memberKind == MemberKind.Constructor) {
                needsEscaping = false;
                
                swiftName = "init";
            } else if (memberKind == MemberKind.Destructor) {
                needsEscaping = false;
                
                swiftName = "destroy";
            } else if (memberKind == MemberKind.TypeOf) {
                needsEscaping = false;
                
                swiftName = "typeOf";
            } else {
                swiftName = swiftName ?? throw new Exception();
            }
        }

        if (needsEscaping) {
            swiftName = swiftName.EscapedSwiftName();
        }

        return swiftName;
    }
}