using System.Collections;

namespace Beyond.NET.Sample.Source;

public class OutParameterTests
{
    #region Primitives
    public void Return_Int_1_NonOptional(out int returnValue)
    {
        returnValue = 1;
    }
    
    public void Return_Int_1_Optional(out int? returnValue)
    {
        returnValue = 1;
    }
    
    public void Return_Int_Null(out int? returnValue)
    {
        returnValue = null;
    }
    
    public void Return_Byte_1_NonOptional(out byte returnValue)
    {
        returnValue = 1;
    }
    
    public void Return_SByte_1_NonOptional(out sbyte returnValue)
    {
        returnValue = 1;
    }
    
    public void Return_UShort_1_NonOptional(out ushort returnValue)
    {
        returnValue = 1;
    }
    
    public void Return_UInt_1_NonOptional(out uint returnValue)
    {
        returnValue = 1;
    }
    
    public void Return_ULong_1_NonOptional(out ulong returnValue)
    {
        returnValue = 1;
    }
    
    public void Return_Bool_true_NonOptional(out bool returnValue)
    {
        returnValue = true;
    }
    
    // TODO: Currently not supported by Beyond.NET
    public void Return_Char_a_NonOptional(out char returnValue)
    {
        returnValue = 'a';
    }
    #endregion Primitives

    #region Enums
    public void Return_DateTimeKind_Utc_NonOptional(out DateTimeKind returnValue)
    {
        returnValue = DateTimeKind.Utc;
    }
    
    public void Return_DateTimeKind_Utc_Optional(out DateTimeKind? returnValue)
    {
        returnValue = DateTimeKind.Utc;
    }
    
    public void Return_DateTimeKind_Null(out DateTimeKind? returnValue)
    {
        returnValue = null;
    }
    #endregion Enums
    
    #region Structs
    public void Return_DateTime_MaxValue_NonOptional(out DateTime returnValue)
    {
        returnValue = DateTime.MaxValue;
    }
    
    public void Return_DateTime_MaxValue_Optional(out DateTime? returnValue)
    {
        returnValue = DateTime.MaxValue;
    }
    
    public void Return_DateTime_Null(out DateTime? returnValue)
    {
        returnValue = null;
    }
    #endregion Structs
    
    #region Classes
    public void Return_String_Abc_NonOptional(out string returnValue)
    {
        returnValue = "Abc";
    }
    
    public void Return_String_Abc_Optional(out string? returnValue)
    {
        returnValue = "Abc";
    }
    
    public void Return_String_Null(out string? returnValue)
    {
        returnValue = null;
    }
    #endregion Classes
    
    #region Interfaces
    public void Return_IEnumerable_String_Abc_NonOptional(out IEnumerable returnValue)
    {
        returnValue = "Abc";
    }
    
    public void Return_IEnumerable_String_Abc_Optional(out IEnumerable? returnValue)
    {
        returnValue = "Abc";
    }
    
    public void Return_IEnumerable_Null(out IEnumerable? returnValue)
    {
        returnValue = null;
    }
    #endregion Interfaces
}