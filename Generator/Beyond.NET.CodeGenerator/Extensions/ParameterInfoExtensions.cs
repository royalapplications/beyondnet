using System.Reflection;

namespace Beyond.NET.CodeGenerator.Extensions;

public static class ParameterInfoExtensions
{
    public static bool IsNullabilityInfoCompatible(
        this ParameterInfo parameterInfo,
        ParameterInfo otherParameterInfo
    )
    {
        var ctx = new NullabilityInfoContext();

        var nullability = ctx.Create(parameterInfo);
        var readState = nullability.ReadState;
        var writeState = nullability.WriteState;
        
        var otherNullability = ctx.Create(otherParameterInfo);
        var otherReadState = otherNullability.ReadState;
        var otherWriteState = otherNullability.WriteState;

        if (readState != otherReadState &&
            !IsNullabilityStateCompatible(readState, otherReadState)) {
            return false;
        }
        
        if (writeState != otherWriteState &&
            !IsNullabilityStateCompatible(writeState, otherWriteState)) {
            return false;
        }

        return true;
    }

    private static bool IsNullabilityStateCompatible(
        this NullabilityState state,
        NullabilityState otherState
    )
    {
        bool isUnknown = state == NullabilityState.Unknown;
        bool isNullable = state == NullabilityState.Nullable;
        bool isNotNull = state == NullabilityState.NotNull;
        
        bool isOtherUnknown = otherState == NullabilityState.Unknown;
        bool isOtherNullable = otherState == NullabilityState.Nullable;
        bool isOtherNotNull = otherState == NullabilityState.NotNull;

        return isUnknown && isOtherNullable ||
               isNullable && isOtherUnknown ||
               (
                   isNotNull && isOtherNullable ||
                   isNullable && isOtherNotNull
                );
    }
}