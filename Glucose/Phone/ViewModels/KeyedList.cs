﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Glucose.Phone.ViewModels
{
    public class KeyedList<TKey, TItem> : List<TItem>
    {
        public TKey Key { get; protected set; }

        public KeyedList(TKey key, IEnumerable<TItem> items)
            : base(items)
        {
            Key = key;
        }

        public KeyedList(IGrouping<TKey, TItem> grouping)
            : base(grouping)
        {
            Key = grouping.Key;
        }
    }   
}