using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameHelpers
{
    public static class ListExtensions
    {
        public static void AddMany<T>(this List<T> list, params T[] elements)
        {
            list.AddRange(elements);
        }
    }

}
