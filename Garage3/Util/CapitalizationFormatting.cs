using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage3.Util
{
    public static class CapitalizationFormatting
    {
        public static string[] CapitalizeFirst(params string[] list)
        {
            var items = new string[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = char.ToUpper(list[i][0]) + list[i].Substring(1).ToLower();
                items[i] = list[i];
            }
            return items;
        }
    }
}
