using System;
using System.Collections.Generic;
using System.Linq;
using TradingAnalyzer.Framework;

namespace TradingAnalyzer.Web.Framework
{
    public static class Extensions
    {
        public static List<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static List<ListItem> EnumToListItems<T>()
            where T : struct
        {
            List<ListItem> list = new List<ListItem>();
            foreach(Enum item in Enum.GetValues(typeof(T)))
            {
                if(item.ToString() != "None")
                    list.Add(new ListItem { Display = item.GetDisplay(), Value = ((int)(object)item).ToString() });
            }

            return list;
        }
    }
}