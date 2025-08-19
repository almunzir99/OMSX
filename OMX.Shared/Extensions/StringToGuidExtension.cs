using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMSX.Shared.Extensions
{
    public static class StringToGuidExtension
    {
        public static Guid ToGuid(this string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Guid.Empty;
            }
            if (Guid.TryParse(value, out var guid))
            {
                return guid;
            }
            throw new FormatException($"The string '{value}' is not a valid GUID.");
        }
    }
}
