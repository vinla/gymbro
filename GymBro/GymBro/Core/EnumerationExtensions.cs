using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBro.Core
{
    public static class EnumerationExtensions
    {
        public static List<TEnum> EnumToList<TEnum>()
        {
            return Enum.GetValues(typeof (TEnum)).Cast<TEnum>().ToList();
        }
    }
}
