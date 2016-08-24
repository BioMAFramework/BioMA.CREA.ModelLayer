namespace CRA.ModelLayer.Core
{
    using System;

    /// <summary>
    /// This internal utility class represents the context for pre- post-conditions test output.
    /// </summary>
    internal class TestsOutputContext
    {
        private ITestsOutput MyOutput;

        internal TestsOutputContext(ITestsOutput tst)
        {
            this.MyOutput = tst;
        }

        internal void TestsOut(string testResult, bool saveLog, string componentName)
        {
            this.MyOutput.TestsOut(testResult, saveLog, componentName);
        }
    }
}

