namespace CRA.ModelLayer.Core
{
    using System;

    /// <summary>
    /// This is an internal utility class that allows validating pre/post-conditions
    /// </summary>
    internal class TestPreconditions
    {
        private string _callID;
        private static bool _RunPrePostConditionTests = true;
        private static ITestsOutput _StrategyTestsOutput = new TestsOutputDefault();

        private string AtLeast(VarInfo i, double[] vvector)
        {
            string str = "";
            bool flag = false;
            for (int j = 0; j < vvector.GetLength(0); j++)
            {
                if ((vvector[j] > 0.0) || (vvector[j] < 0.0))
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                str = string.Concat(new object[] { "all ", i.Name, "[ ] = ", 0, " (at least one should have been different from 0) ", this._callID, ";\r\n" });
            }
            return str;
        }

        private string AtLeast(VarInfo i, double[,] vmatrix)
        {
            string str = "";
            bool flag = false;
            for (int j = 0; j < vmatrix.GetLength(0); j++)
            {
                for (int k = 0; k < vmatrix.GetLength(1); k++)
                {
                    if ((vmatrix[j, k] > 0.0) || (vmatrix[j, k] < 0.0))
                    {
                        flag = true;
                    }
                }
            }
            if (!flag)
            {
                str = string.Concat(new object[] { "all ", i.Name, "[,] = ", 0, " (at least one should have been different from 0) ", this._callID, ";\r\n" });
            }
            return str;
        }

        private string AtLeastOneDifferent(VarInfo i)
        {
            string str4 = i.CurrentValue.GetType().ToString();
            if ((str4 != null) && (str4 == "System.Double[,]"))
            {
                double[,] vmatrix = (double[,]) i.CurrentValue;
                return this.AtLeast(i, vmatrix);
            }
            double[] currentValue = (double[]) i.CurrentValue;
            return this.AtLeast(i, currentValue);
        }

        private string Cannot(VarInfo a, VarInfo b)
        {
            double aValue = double.Parse(a.CurrentValue.ToString());
            double bValue = double.Parse(b.CurrentValue.ToString());
            return this.CannotBeZeroIf(a, aValue, b, bValue);
        }

        private string CannotBeZeroIf(VarInfo a, double aValue, VarInfo b, double bValue)
        {
            string str = "";
            if ((aValue == 0.0) && (bValue != 0.0))
            {
                str = a.Name + " cannot be = 0 if " + b.Name + " is <> 0 (" + b.Name + " = " + bValue.ToString() + " " + this._callID + ";\r\n";
            }
            return str;
        }

        private string Greater(VarInfo i, double v)
        {
            string str = "";
            if (v > i.MaxValue)
            {
                str = string.Concat(new object[] { i.Name, " = ", i.CurrentValue.ToString(), " (max=", i.MaxValue, " - min=", i.MinValue, ") ", this._callID, ";\r\n" });
            }
            return str;
        }

        private string Greater(VarInfo i, double[] v)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            for (int j = 0; j < v.GetLength(0); j++)
            {
                if (v[j] > i.MaxValue)
                {
                    str2 = string.Concat(new object[] { i.Name, "[", j.ToString(), "] = ", v[j].ToString(), " (max=", i.MaxValue, " - min=", i.MinValue, ") ", this._callID, ";\r\n" });
                    str = str + str2;
                }
            }
            return str;
        }

        private string Greater(VarInfo i, double[,] v)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            for (int j = 0; j < v.GetLength(0); j++)
            {
                for (int k = 0; k < v.GetLength(1); k++)
                {
                    if (v[j, k] > i.MaxValue)
                    {
                        str2 = string.Concat(new object[] { i.Name, "[", j.ToString(), ",", k.ToString(), "] = ", v[j, k].ToString(), "] (max=", i.MaxValue, " - min=", i.MinValue, ") ", this._callID, ";\r\n" });
                        str = str + str2;
                    }
                }
            }
            return str;
        }

        private string NoSmaller(VarInfo a, VarInfo b)
        {
            double aValue = double.Parse(a.CurrentValue.ToString());
            double bValue = double.Parse(b.CurrentValue.ToString());
            return this.SmallerTwoInputs(a, aValue, b, bValue);
        }

        private string Range(VarInfo i)
        {
            double num;
            string str = "";
            string str2 = i.CurrentValue.GetType().ToString();
            switch (str2)
            {
                case "System.Double[]":
                {
                    double[] currentValue = (double[]) i.CurrentValue;
                    return (str + this.Greater(i, currentValue) + this.Smaller(i, currentValue));
                }
                case "System.Double[,]":
                {
                    double[,] v = (double[,]) i.CurrentValue;
                    return (str + this.Greater(i, v) + this.Smaller(i, v));
                }
            }
            try
            {
                num = double.Parse(i.CurrentValue.ToString());
            }
            catch (Exception)
            {
                return string.Concat(new object[] { "Error testing range-based preconditions. Likely the type of the CurrentValue of the VarInfo is not supported. Type:", str2, " - CurrentValue:", i.CurrentValue });
            }
            return (str + this.Greater(i, num) + this.Smaller(i, num));
        }

        private string Smaller(VarInfo i, double v)
        {
            string str = "";
            if (v < i.MinValue)
            {
                str = string.Concat(new object[] { i.Name, " = ", i.CurrentValue.ToString(), " (max=", i.MaxValue, " - min=", i.MinValue, ") ", this._callID, ";\r\n" });
            }
            return str;
        }

        private string Smaller(VarInfo i, double[] v)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            for (int j = 0; j < v.GetLength(0); j++)
            {
                if (v[j] < i.MinValue)
                {
                    str2 = string.Concat(new object[] { i.Name, "[", j.ToString(), "] = ", v[j].ToString(), " (max=", i.MaxValue, " - min=", i.MinValue, ") ", this._callID, ";\r\n" });
                    str = str + str2;
                }
            }
            return str;
        }

        private string Smaller(VarInfo i, double[,] v)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            for (int j = 0; j < v.GetLength(0); j++)
            {
                for (int k = 0; k < v.GetLength(1); k++)
                {
                    if (v[j, k] < i.MinValue)
                    {
                        str2 = string.Concat(new object[] { i.Name, "[", j.ToString(), ",", k.ToString(), "] = ", v[j, k].ToString(), "] (max=", i.MaxValue, " - min=", i.MinValue, ") ", this._callID, ";\r\n" });
                        str = str + str2;
                    }
                }
            }
            return str;
        }

        private string SmallerTwoInputs(VarInfo a, double aValue, VarInfo b, double bValue)
        {
            string str = "";
            if (aValue < bValue)
            {
                str = a.Name + " < " + b.Name + " (";
                str = str + a.CurrentValue.ToString() + " < " + b.CurrentValue.ToString() + ") " + this._callID + ";\r\n";
            }
            return str;
        }

        private string SubRange(VarInfo a, double aValue, VarInfo b, double bValue)
        {
            string str = "";
            if ((((aValue < a.MinValue) || (aValue > a.MaxValue)) && (bValue >= b.MinValue)) && (bValue <= b.MaxValue))
            {
                str = a.Name + " = " + aValue.ToString() + ". It cannot outrange (" + a.MinValue.ToString() + "-" + a.MaxValue.ToString() + ") if " + b.Name + " is within (" + b.MinValue.ToString() + "-" + b.MaxValue.ToString() + ") " + this._callID + ";\r\n";
            }
            return str;
        }

        private string SubRanges(VarInfo a, VarInfo b)
        {
            double aValue = double.Parse(a.CurrentValue.ToString());
            double bValue = double.Parse(b.CurrentValue.ToString());
            return this.SubRange(a, aValue, b, bValue);
        }

        internal void TestsOut(string testResult, bool saveLog, string componentName)
        {
            try
            {
                new TestsOutputContext(_StrategyTestsOutput).TestsOut(testResult, saveLog, componentName);
            }
            catch (Exception exception)
            {
                string str = "An error occurred while using the";
                throw new Exception(str + " selected ITestsOutput strategy," + " try using another ITestsOutput strategy.", exception);
            }
        }

        internal string VerifyPostconditions(ConditionsCollection prd, string callID)
        {
            this._callID = callID;
            string str = "POST-CONDITIONS: ";
            string str2 = "";
            str2 = prd.VerifyConditions(callID);
            if (str2.Length > 10)
            {
                str2 = str + str2;
            }
            return str2;
        }

        internal string VerifyPreconditions(ConditionsCollection prd, string callID)
        {
            this._callID = callID;
            string str = "PRE-CONDITIONS: ";
            string str2 = "";
            str2 = prd.VerifyConditions(callID);
            if (str2.Length > 10)
            {
                str2 = str + str2;
            }
            return str2;
        }

        internal static bool RunPrePostConditionTests
        {
            get
            {
                return _RunPrePostConditionTests;
            }
            set
            {
                _RunPrePostConditionTests = value;
            }
        }

        internal static ITestsOutput StrategyTestsOutput
        {
            get
            {
                return _StrategyTestsOutput;
            }
            set
            {
                _StrategyTestsOutput = value;
            }
        }
    }
}

