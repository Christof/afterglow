using System;
using System.Collections.Generic;

namespace Afterglow.Applications.TestRunner.Components.Utils
{
    /// <summary>
    /// Priority List:
    /// Order of sortable objects in list.
    /// </summary>
    /// <typeparam name="ListItemType"></typeparam>
    public class PriorityList<ListItemType>

    {
        public int GetIndexOf(ListItemType item)
        {
            return mPriorities.IndexOf(item);
        }

        public int Size
        {
            get
            {
                return mPriorities.Count;
            }
        }

        public void Add(ListItemType item)
        {
            mPriorities.Add(item);
        }

        public void Add(int index, ListItemType item)
        {
            mPriorities.Insert(index, item);
        }

        public void AddRange(IEnumerable<ListItemType> items)
        {
            mPriorities.AddRange(items);
        }

        public void Clear()
        {
            mPriorities.Clear();
        }

        public ListItemType this[int index ]
        {
            get
            {
                if(index >= mPriorities.Count)
                {
                    return default(ListItemType);
                }
                return mPriorities[index];
            }
            set
            {
                if(index >= mPriorities.Count || mPriorities[index] != null)
                {
                    if (index > mPriorities.Count)
                    {
                        index = mPriorities.Count;
                    }
                    this.Add(index, value);
                }
                else
                {
                    mPriorities[index] = value;
                }
            }
        }

        public void Move(ListItemType item, int index)
        {
            if(this.mPriorities.Contains(item))
            {
                this.mPriorities.Remove(item);
            }
            this[index] = item;
        }

        private List<ListItemType> mPriorities = new List<ListItemType>();
    }
}