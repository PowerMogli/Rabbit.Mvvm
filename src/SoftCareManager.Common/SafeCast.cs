using System;

namespace SoftCareManager.Common
{
    public static class SafeCast
    {
        public static U Cast<T, U>(this T valueToCastFrom)
            where U : class
        {
            U castedValue = valueToCastFrom as U;
            if (castedValue == null)
            {
                throw new InvalidCastException(string.Format("Casting from {0} to {1} is not possible!", typeof(T), typeof(U)));
            }

            return castedValue;
        }
    }
}