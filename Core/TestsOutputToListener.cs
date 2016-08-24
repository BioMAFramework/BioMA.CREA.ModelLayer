namespace CRA.ModelLayer.Core
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Writes the output of pre/post conditions tests to the <see cref="TraceHelper">CRA.ModelLayer.TraceHelper</see> class. Then the user can attach a listener to this trace source, to output the log. The log level used is <see cref="TraceEventType.Warning">System.Diagnostic.TraceEventType.Warning</see>. The event id is 1000.
    /// </summary>
    public class TestsOutputToListener : ITestsOutput
    {
        /// <summary>
        /// Writes the output of pre and post condition test to the TraceHelper class.
        /// </summary>
        /// <param name="testResult">String containing test results.</param>
        /// <param name="saveLog">ignored</param>
        /// <param name="componentName">Component name to be concatenated to the output string to trace the component that triggered the test</param>
        public void TestsOut(string testResult, bool saveLog, string componentName)
        {
            TraceHelper.TraceEvent(TraceEventType.Warning, 0x3e8, componentName + "\r\n" + testResult);
        }
    }
}

