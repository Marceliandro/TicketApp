using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TicketApp.Dominio.Utils
{
    public static class MessagesException
    {
        public static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem, Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
                yield return current;
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(this TSource source, Func<TSource, TSource> nextItem) where TSource : class => FromHierarchy(source, nextItem, s => s != null);
        public static string Get(this Exception exception) => Regex.Replace(string.Join(" ", exception.FromHierarchy(ex => ex.InnerException).Select(ex => ex.Message)), @"\t|\n|\r", "");
    }
}
