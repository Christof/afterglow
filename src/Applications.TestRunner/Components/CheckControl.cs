using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Afterglow.Infrastructure;

namespace Afterglow.Applications.TestRunner.Components
{
    public partial class CheckControl : UserControl
    {
        public CheckControl()
        {
            InitializeComponent();
            this.elementList.ItemCheck += new ItemCheckEventHandler(elementList_ItemCheck);
        }

        private void elementList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.OnSelectedChanged != null)
            {
                this.OnSelectedChanged(this, this.CheckedElements);
            }
        }

        #region checking 

        public void SetCheckedForAll(Boolean value)
        {
            StoreRestorePoint();
            for (int i = 0; i < elementList.Items.Count; i++)
            {
                elementList.SetItemChecked(i, value);
            }
        }

        public void SetCheckedForAll(Func<int, bool> checkLogic)
        {
            StoreRestorePoint();
            for (int i = 0; i < elementList.Items.Count; i++)
            {
                elementList.SetItemChecked(i, checkLogic(i));
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            SetCheckedForAll(true);
            this.filterTextBox.Text = string.Empty;
        }

        private void selectNoneButton_Click(object sender, EventArgs e)
        {
            SetCheckedForAll(false);
            this.filterTextBox.Text = string.Empty;
        }

        private void invertButton_Click(object sender, EventArgs e)
        {
            SetCheckedForAll(index => !elementList.GetItemChecked(index));
            this.filterTextBox.Text = string.Empty;
        }
        #endregion checking

        #region filter text
        private bool IsFilterTextValid()
        {
            var filterText = filterTextBox.Text;
            return !string.IsNullOrEmpty(filterText);
        }

        private void filterText_TextChanged(object sender, EventArgs e)
        {
            if(IsFilterTextValid())
            {
                StoreRestorePoint();
                for(int i = 0; i < elementList.Items.Count; i++)
                {
                    var text = elementList.Items[i].ToString();
                    if (text.Contains(filterTextBox.Text))
                    {
                        elementList.SetItemChecked(i, true);
                    }
                    else
                    {
                        elementList.SetItemChecked(i, false);
                    }
                }
            }
        }
        #endregion filter text

        #region undo

        private int[] mLastCheckedIndices = new int[0];

        private void StoreRestorePoint()
        {
            mLastCheckedIndices = elementList.CheckedItems.Cast<int>().ToArray();
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            UndoLastAction();
        }

        #endregion undo

        #region public interface

        public bool AllowDuplicates
        {
            get; set;
        }

        public void AddElements(IEnumerable elements)
        {
            foreach (var element in elements)
            {
                if(element != null &&
                    (AllowDuplicates || !elementList.Items.Contains(element)) )
                {
                    elementList.Items.Add(element);
                }
            }
        }

        public void ClearElements()
        {
            elementList.Items.Clear();
        }

        public object[] CheckedElements
        {
            get
            {
                var checkedElements = new object[elementList.CheckedIndices.Count];
                for (int i = 0; i < elementList.CheckedIndices.Count; i++)
                {
                    checkedElements[i] = elementList.Items[elementList.CheckedIndices[i]];
                }
                return checkedElements;
            }
        }

        public string FilterText
        {
            get
            {
                return filterTextBox.Text;
            }
            set
            {
                filterTextBox.Text = value;
            }
        }

        public void UndoLastAction()
        {
            var lastCheckedIndices = mLastCheckedIndices; 
            this.SetCheckedForAll(false); //this also stores restore point
            foreach (var lastCheckedIndex in lastCheckedIndices)
            {
                elementList.SetItemChecked(lastCheckedIndex, true);
            }
        }

        public Action<CheckControl, object[]> OnSelectedChanged
        {
            get; set;
        }

        public long Priority
        {
            get; set;
        }
        #endregion public interface

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void elementList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
