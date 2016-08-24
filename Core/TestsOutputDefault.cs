namespace CRA.ModelLayer.Core
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Default output for the pre/post conditions tests. It either shows results on screen (saveLog=false), or it writes the results on a TXT file (the default file is "PrePostConditionsLog.txt", but it can be changed through the property LogFileName). 
    /// </summary>    
    public class TestsOutputDefault : ITestsOutput
    {
        private static string localLogFileName = "PrePostConditionsLog.txt";

        /// <summary>
        /// Writes/saves the output of pre and post condition test to the TXT file.
        /// </summary>
        /// <param name="result">String containing test results.</param>
        /// <param name="saveLog">Used ONLY by the default output strategy (<see cref="TestsOutputDefault">TestsOutputDefault</see>):
        /// saveLog=false: output of pre and post conditions goes on screen (console output)
        /// saveLog=true: output on txt file</param>
        /// <param name="componentName">Component name to be concatenated to the output string to trace the component that triggered the test</param>
        public void TestsOut(string result, bool saveLog, string componentName)
        {
            if (!saveLog)
            {
                MessageBox.Show(result, componentName + " - Violation of pre/post-conditions");
            }
            else
            {
                DateTime now = DateTime.Now;
                FileStream stream = new FileStream(localLogFileName, FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(string.Concat(new object[] { componentName, " - ", now, "\r\n", result }));
                writer.Close();
                stream.Close();
            }
        }

        /// <summary>
        /// Name of the txt file to write the output pre and post conditions test results.
        /// The default name is PrePostConditionsLog.txt, saved on the application is.
        /// The file name can include a path.
        /// </summary>
        public static string LogFileName
        {
            get
            {
                return localLogFileName;
            }
            set
            {
                localLogFileName = value;
            }
        }
    }
}

