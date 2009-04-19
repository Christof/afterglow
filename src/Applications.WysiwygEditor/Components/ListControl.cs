using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Afterglow.Infrastructure;

namespace WysiwygEditor.Components
{
    public partial class ListControl<T> : UserControl
    {
        private List<Element<T>> mElements = new List<Element<T>>();
        private Func<T[]> mOnAddClicked;

        public ListControl(Func<T[]> onAddClicked)
        {
            InitializeComponent();
            mOnAddClicked = onAddClicked;
        }

        public T[] Elements
        {
            get { return (from element in mElements select element.Instance).ToArray(); }
            set
            {
                if (mElements == null)
                {
                    Clear();
                }
                else
                {
                    mElements = value.Cast<Element<T>>().ToList();
                    UpdateListBox();
                }
            }
        }

        public uint[] SelectedIndices
        {
            get
            {
                return (from element in mElements
                        where element.IsSelected
                        select (uint)mElements.IndexOf(element)).ToArray();
            }
            set
            {
                if (value == null)
                    value = new uint[0];

                for (int i = 0; i < mElements.Count; i++)
                {
                    if(value.Contains( (uint)i) )
                        mElements[i].IsSelected = true;
                }
                this.UpdateListBox();
            }
        }

        private void Clear()
        {
            mElements.Clear();
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            mListBox.Items.Clear();
            mListBox.Items.AddRange(mElements.ToArray());
            for (int i = 0; i < mElements.Count; i++)
            {
                mListBox.SetSelected(i, mElements[i].IsSelected);
            }
        }

        private void mAddButton_Click(object sender, EventArgs e)
        {
            var newElements = mOnAddClicked();
            this.AddElements(newElements);
        }

        public void AddElements(T[] newElements)
        {
            mElements.AddRange(from newElement in newElements select new Element<T>(newElement));
            this.UpdateListBox();
        }

        private void mRemoveButton_Click(object sender, EventArgs e)
        {
            RemoveSelectedItems();
        }

        public void RemoveSelectedItems()
        {
            SelectedIndices.ForEach(i => mElements.RemoveAt((int)i));
            this.UpdateListBox();
        }

        #region Nested type: Element

        private class Element<T>
        {
            private T instance;
            private bool isSelected;

            public Element(T instance, bool isSelected)
            {
                this.isSelected = isSelected;
                this.instance = instance;
            }

            public Element(T instance)
                : this(instance, false)
            {
                ;
            }

            public bool IsSelected
            {
                get { return isSelected; }
                set { isSelected = value; }
            }

            public T Instance
            {
                get { return instance; }
                set { instance = value; }
            }

            public static implicit operator T(Element<T> element)
            {
                return element.Instance;
            }

            public static implicit operator Element<T>(T instance)
            {
                return new Element<T>(instance);
            }

            public override string ToString()
            {
                return string.Format("{0}, selected:{1}", instance, isSelected);
            }
        }

        #endregion
    }
}