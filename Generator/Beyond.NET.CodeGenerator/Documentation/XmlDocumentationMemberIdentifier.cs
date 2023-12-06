using System.Reflection;
using System.Text.RegularExpressions;

namespace Beyond.NET.CodeGenerator;

internal struct XmlDocumentationMemberIdentifier
{
    private readonly string m_key;
    internal bool IsValid => !string.IsNullOrEmpty(m_key);

    internal const string IDENTIFIER_NAMESPACE = "N";
    internal const string IDENTIFIER_TYPE = "T";
    internal const string IDENTIFIER_FIELD = "F";
    internal const string IDENTIFIER_PROPERTY = "P";
    internal const string IDENTIFIER_METHOD = "M";
    internal const string IDENTIFIER_EVENT = "E";
    internal const string IDENTIFIER_ERRORSTRING = "!";

    internal static readonly string[] Identifiers = new[] {
	    IDENTIFIER_NAMESPACE,
	    IDENTIFIER_TYPE,
	    IDENTIFIER_FIELD,
	    IDENTIFIER_PROPERTY,
	    IDENTIFIER_METHOD,
	    IDENTIFIER_EVENT,
	    IDENTIFIER_ERRORSTRING
    };

    internal XmlDocumentationMemberIdentifier(string key)
    {
        m_key = key ?? throw new ArgumentNullException(nameof(key));
    }

    internal XmlDocumentationMemberIdentifier(Type type)
    {
        if (type.IsArray) {
            m_key = string.Empty;
            
            return;
        }
        
        var typeFullName = type.FullName;

        if (string.IsNullOrEmpty(typeFullName)) {
            m_key = string.Empty;
            
            return;
        }
        
        m_key = $"{IDENTIFIER_TYPE}:{EncodeKey(typeFullName)}";
    }
    
    internal XmlDocumentationMemberIdentifier(FieldInfo fieldInfo)
    {
        var type = fieldInfo.DeclaringType;

        if (type is null) {
            m_key = string.Empty;
            
            return;
        }
        
        var typeFullName = type.FullName;

        if (string.IsNullOrEmpty(typeFullName)) {
            m_key = string.Empty;
            
            return;
        }

        var memberName = fieldInfo.Name;

        m_key = $"{IDENTIFIER_FIELD}:{EncodeKey(typeFullName, memberName)}";
    }
    
    internal XmlDocumentationMemberIdentifier(PropertyInfo propertyInfo)
    {
        var type = propertyInfo.DeclaringType;

        if (type is null) {
            m_key = string.Empty;
            
            return;
        }
        
        var typeFullName = type.FullName;

        if (string.IsNullOrEmpty(typeFullName)) {
            m_key = string.Empty;
            
            return;
        }

        var memberName = propertyInfo.Name;

        m_key = $"{IDENTIFIER_PROPERTY}:{EncodeKey(typeFullName, memberName)}";
    }
    
    internal XmlDocumentationMemberIdentifier(EventInfo eventInfo)
    {
        var type = eventInfo.DeclaringType;

        if (type is null) {
            m_key = string.Empty;
            
            return;
        }
        
        var typeFullName = type.FullName;

        if (string.IsNullOrEmpty(typeFullName)) {
            m_key = string.Empty;
            
            return;
        }

        var memberName = eventInfo.Name;

        m_key = $"{IDENTIFIER_EVENT}:{EncodeKey(typeFullName, memberName)}";
    }

    internal XmlDocumentationMemberIdentifier(MethodInfo methodInfo) 
	    : this(methodInfo, false)
    {
    }
    
    internal XmlDocumentationMemberIdentifier(ConstructorInfo constructorInfo) 
	    : this(constructorInfo, true)
    {
    }
    
    private XmlDocumentationMemberIdentifier(
	    MethodBase methodBase,
	    bool isConstructor
	)
    {
        var type = methodBase.DeclaringType;

        if (type is null) {
            m_key = string.Empty;
            
            return;
        }
        
        var typeFullName = type.FullName;

        if (string.IsNullOrEmpty(typeFullName)) {
            m_key = string.Empty;
            
            return;
        }

        var memberName = GetXmlMethodName(
	        type,
	        methodBase,
	        isConstructor
        );

        m_key = $"{IDENTIFIER_METHOD}:{memberName}";
    }

    private static string? GetXmlMethodName(
	    Type declaringType,
	    MethodBase methodBase,
	    bool isConstructor
	)
	{
		if (methodBase.IsGenericMethod ||
		    methodBase.ContainsGenericParameters ||
            methodBase.Name is "op_Implicit" ||
            methodBase.Name is "op_Explicit") {
			return null;
		}
		
		var parameterInfos = methodBase.GetParameters();

		var declarationTypeString = GetXmlDocumenationFormattedString(
			declaringType,
			false
		);
		
		var memberNameString = isConstructor
            ? "#ctor" 
            : methodBase.Name;
		
		string parametersString = parameterInfos.Length > 0 
			? "(" + string.Join(",", methodBase.GetParameters().Select(x => GetXmlDocumenationFormattedString(x.ParameterType, true))) + ")" 
			: string.Empty;

		string methodName = declarationTypeString +
		                    "." +
		                    memberNameString +
		                    parametersString;
        
		return methodName;
	}

    private static string? GetXmlDocumenationFormattedString(
		Type type,
		bool isMethodParameter
	)
	{
		if (type.IsGenericType ||
		    type.IsGenericParameter) {
			return null;
		} else if (type.HasElementType) {
			var elementType = type.GetElementType();

			if (elementType is null) {
				return null;
			}
			
			var elementTypeString = GetXmlDocumenationFormattedString(
				elementType,
				isMethodParameter
			);

			if (type.IsPointer) {
				return elementTypeString + "*";
			} else if (type.IsByRef) {
				return elementTypeString + "@";
			} else if (type.IsArray) {
				int rank = type.GetArrayRank();
				
				string arrayDimensionsString = rank > 1
					? "[" + string.Join(",", Enumerable.Repeat("0:", rank)) + "]"
					: "[]";
				
				return elementTypeString + arrayDimensionsString;
			} else {
				return null;
			}
		} else {
			var isNested = type.IsNested;

			string prefaceString;
			
			if (isNested) {
				var declaringType = type.DeclaringType;

				if (declaringType is null) {
					return null;
				}
				
				prefaceString = GetXmlDocumenationFormattedString(
					declaringType,
					isMethodParameter
				) + ".";
			} else {
				prefaceString = type.Namespace + ".";
			}

			string typeNameString = isMethodParameter
				? Regex.Replace(type.Name, @"`\d+", string.Empty)
				: type.Name;

			return prefaceString + typeNameString;
		}
	}
    
    private static string EncodeKey(
        string fullTypeName,
        string? memberName = null
    ) {
        string key = Regex.Replace(
            fullTypeName, 
            @"\[.*\]",
            string.Empty
        ).Replace('+', '.');

        if (!string.IsNullOrEmpty(memberName)) {
            key += "." + memberName;
        }
        
        return key;
    }

    public override string ToString()
    {
        return m_key;
    }

    public string ToStringWithoutIdentifier()
    {
	    var key = m_key;

	    if (string.IsNullOrEmpty(key)) {
		    return key;
	    }
	    
	    foreach (var identifier in Identifiers) {
		    var identifierPrefix = $"{identifier}:";
                    
		    if (!key.StartsWith(identifierPrefix) ||
		        key.Length <= identifierPrefix.Length) {
			    continue;
		    }

		    key = key.Substring(identifierPrefix.Length);
	    }

	    return key;
    }
}