namespace CRA.ModelLayer.Core
{
    using System;

    /// <summary>
    /// ITestsOutput is the interface that represents a listener to the output of pre and post conditions test results.
    /// A valid implementation of this interface can be used by setting it in the <see cref="Preconditions">Preconditions</see> using the StrategyTestsOutput property of Preconditions class.
    /// </summary>
    public interface ITestsOutput
    {
        /// <summary>
        /// Writes/saves the output of pre and post condition test.
        /// </summary>
        /// <param name="testResult">String containing test results.</param>
        /// <param name="saveLog">Used ONLY by the default output strategy (<see cref="TestsOutputDefault">TestsOutputDefault</see>):
        /// saveLog=false: output of pre and post conditions goes on screen (console output)
        /// saveLog=true: output on txt file</param>
        /// <param name="componentName">Component name to be concatenated to the output string to trace the component that triggered the test</param>
        void TestsOut(string testResult, bool saveLog, string componentName);
    }
}

