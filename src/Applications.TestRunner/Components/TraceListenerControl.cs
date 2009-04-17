using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Afterglow.Applications.TestRunner.Components
{
    public class TraceListenerControl : UserControl, IDisposable
    {
        public class MyTraceListener : TraceListener
        {
            private Action<string> mListingAction;

            public MyTraceListener(Action<string> mListingAction)
            {
                if(mListingAction == null)
                    throw new ArgumentNullException();

                this.mListingAction = mListingAction;
            }

            public override void Write(string message)
            {
                this.WriteLine(message);
            }

            public override void WriteLine(string message)
            {
                mListingAction(message);
            }
        }

        private MyTraceListener mListener;
        private ListBox mListBox;

        public TraceListenerControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.mListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // mListBox
            // 
            this.mListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mListBox.FormattingEnabled = true;
            this.mListBox.Location = new System.Drawing.Point(0, 0);
            this.mListBox.Name = "mListBox";
            this.mListBox.ScrollAlwaysVisible = true;
            this.mListBox.Size = new System.Drawing.Size(526, 303);
            this.mListBox.TabIndex = 0;
            // 
            // TraceListenerControl
            // 
            this.Controls.Add(this.mListBox);
            this.Name = "TraceListenerControl";
            this.Size = new System.Drawing.Size(526, 310);
            this.ResumeLayout(false);

        }

        public void RegisterListener()
        {
            mListener = new MyTraceListener(message => mListBox.Items.Add(message));
            Trace.Listeners.Add(mListener);
        }

        public void Clear()
        {
            mListBox.Items.Clear();
        }

        void IDisposable.Dispose()
        {
            base.Dispose();
            Trace.Listeners.Remove(mListener);
        }
    }
}
