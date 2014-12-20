using System;

namespace SoftCareManager.Common
{
    public static class SafeCast
    {
        public static T Cast<T>(this object valueToCastFrom)
            where T : class
        {
            T castedValue = valueToCastFrom as T;
            if (castedValue == null)
            {
                throw new InvalidCastException(string.Format("Casting to {0} is not possible!", typeof (T)));
            }

            return castedValue;
        }
    }
}