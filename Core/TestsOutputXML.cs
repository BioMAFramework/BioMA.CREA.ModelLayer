namespace CRA.ModelLayer.Core
{
    using System;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Writes the output of pre/post conditions tests to a file using the XML format (the default file is "PrePostConditionsLogXML.xml", but it can be changed through the property LogFileName). 
    /// </summary>
    public class TestsOutputXML : ITestsOutput
    {
        private static string _logFileName = "PrePostConditionsLogXML.xml";

        private void CorrectFile()
        {
            FileStream stream = new FileStream(_logFileName, FileMode.Open, FileAccess.Read);
            FileStream stream2 = new FileStream("temp_" + _logFileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream2);
            bool flag = false;
            int num = 0;
            do
            {
                num++;
                try
                {
                    string str = reader.ReadLine();
                    if (!((num != 2) && str.Contains("PrePostConditionsTests")))
                    {
                        writer.WriteLine(str);
                    }
                }
                catch
                {
                    writer.WriteLine("</PrePostConditionsTests>");
                    flag = true;
                }
            }
            while (!flag);
            reader.Close();
            writer.Close();
            stream.Close();
            stream2.Close();
            File.Delete(_logFileName);
            File.Move("temp_" + _logFileName, _logFileName);
        }

        private void ParseAndWrite(XmlTextWriter writer, string testResult, string component)
        {
            int index;
            writer.WriteStartElement(component);
            DateTime now = DateTime.Now;
            writer.WriteAttributeString("dateTest", now.ToShortDateString() + " " + now.ToLongTimeString());
            int startIndex = 0;
            int num2 = 0;
            int num3 = 0;
            do
            {
                index = testResult.IndexOf("\r\n", (int) (startIndex + num2));
                string localName = "Pre-conditions";
                if (startIndex > 0)
                {
                    num3 = 1;
                }
                if (testResult.Substring((startIndex + num2) - num3, 3) == "POS")
                {
                    localName = "Post-conditions";
                }
                writer.WriteElementString(localName, testResult.Substring(startIndex, index - (startIndex + 1)));
                startIndex = index;
                num2 = 3;
            }
            while ((index + 5) < testResult.Length);
            writer.WriteEndElement();
        }


        /// <summary>
        /// Writes/saves the output of pre and post condition test to the XML file.
        /// </summary>
        /// <param name="testResult">String containing test results.</param>
        /// <param name="saveLog">ignored</param>
        /// <param name="componentName">Component name to be concatenated to the output string to trace the component that triggered the test</param>
        public void TestsOut(string testResult, bool saveLog, string componentName)
        {
            string component = componentName.Replace(" ", "_");
            FileStream stream = new FileStream(_logFileName, FileMode.Append, FileAccess.Write);
            StreamWriter w = new StreamWriter(stream);
            XmlTextWriter writer = new XmlTextWriter(w) {
                Formatting = Formatting.Indented
            };
            bool flag = false;
            if (stream.Length < 10L)
            {
                writer.WriteStartDocument(true);
                writer.WriteStartElement("PrePostConditionsTests");
                flag = true;
            }
            this.ParseAndWrite(writer, testResult, component);
            if (flag)
            {
                writer.WriteEndDocument();
            }
            w.Write("\r\n");
            writer.Close();
            w.Close();
            stream.Close();
            this.CorrectFile();
        }

        /// <summary>
        /// Name of the txt file to write the output pre and post conditions test results.
        /// The default name is PrePostConditionsLogXML.xml, saved on the same directory where the application is.
        /// The file name can include a path.
        /// </summary>
        public static string LogFileName
        {
            get
            {
                return _logFileName;
            }
            set
            {
                _logFileName = value;
            }
        }
    }
}

