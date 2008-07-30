// Copyright 2005-2008 Gallio Project - http://www.gallio.org/
// Portions Copyright 2000-2004 Jonathan de Halleux
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Text.RegularExpressions;
using Gallio.Framework;
using MbUnit.Framework;
using Gallio.Model;
using WatiN.Core;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;

namespace MbUnit.Samples
{
    /// <summary>
    /// This is a simple demonstration of integration opportunities between
    /// MbUnit v3 and WatiN.
    /// </summary>
    [TestFixture]
    public class WatiNDemo
    {
        private IE ie;

        [SetUp]
        public void CreateBrowser()
        {
            ie = new IE();

            Logger.LogWriter = new WatiNStreamLogger();
        }

        [TearDown]
        public void DisposeBrowser()
        {
            if (Context.CurrentContext.Outcome.Status == TestStatus.Failed)
                Snapshot("Final screen when failure occurred.", Log.Failures);

            if (ie != null)
                ie.Dispose();
        }

        /// <summary>
        /// Demonstrates capturing a screenshot on failure automatically.  This test does
        /// not contain any special logic for caputing the screenshot; it happens as part
        /// of the TearDown phase when it detects that the test has failed.
        /// </summary>
        [Test]
        public void DemoCaptureOnFailure()
        {
            using (Log.BeginSection("Go to Google, enter MbUnit as a search term and click I'm Feeling Lucky"))
            {
                ie.GoTo("http://www.google.com");

                ie.TextField(Find.ByName("q")).TypeText("MbUnit");
                ie.Button(Find.ByName("btnI")).Click();
            }

            // Of course this is ridiculous, we'll be on the MbUnit homepage...
            Assert.IsTrue(ie.ContainsText("NUnit"), "Expected to find NUnit on the page.");
        }

        /// <summary>
        /// Demonstrates capturing discretionary screenshots at will and labeling them.
        /// Unlike <see cref="DemoCaptureOnFailure" /> this test does not capture an
        /// extra automatic screenshot on termination because the TearDown phase can
        /// detect that the test has passed so it does not bother to capture an image.
        /// </summary>
        [Test]
        public void DemoNoCaptureOnSuccess()
        {
            using (Log.BeginSection("Go to Google, enter MbUnit as a search term and click I'm Feeling Lucky"))
            {
                ie.GoTo("http://www.google.com");

                ie.TextField(Find.ByName("q")).TypeText("MbUnit");
                ie.Button(Find.ByName("btnI")).Click();
            }

            using (Log.BeginSection("Click on About."))
            {
                Assert.IsTrue(ie.ContainsText("MbUnit"));
                ie.Link(Find.ByUrl(new Regex(@"About\.aspx"))).Click();
            }

            Snapshot("About the MbUnit project.");
        }

        private void Snapshot(string caption)
        {
            Snapshot(caption, Log.Default);
        }

        private void Snapshot(string caption, LogStreamWriter logStreamWriter)
        {
            using (logStreamWriter.BeginSection(caption))
            {
                logStreamWriter.WriteLine("Url: {0}", ie.Url);
                logStreamWriter.EmbedImage(null, new CaptureWebPage(ie).CaptureWebPageImage(false, false, 100));
            }
        }

        private class WatiNStreamLogger : ILogWriter
        {
            public void LogAction(string message)
            {
                Log.WriteLine(message);
            }
        }
    }
}
