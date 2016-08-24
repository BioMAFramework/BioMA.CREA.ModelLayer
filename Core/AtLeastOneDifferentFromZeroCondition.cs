namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Condition to test that a VarInfo value, composed by multiple values (e.g array of doubles, list of doubles,...), has at least of value different from zero.
    /// It is an implementation of <see cref="ICondition">ICondition</see> interface.
    /// </summary>
    public class AtLeastOneDifferentFromZeroCondition : ICondition
    {
        private static List<VarInfoValueTypes> _applicableVarInfoValueTypes = new List<VarInfoValueTypes>();
        private List<VarInfo> _controlledVarInfos;
        private VarInfo _varInfo;

        static AtLeastOneDifferentFromZeroCondition()
        {
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.ArrayDouble);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.ListDouble);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.ArrayInteger);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.ListInteger);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.Bidimensional);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.DoubleMatrix);
        }

        /// <summary>
        /// Builds the instance of this condition. It requires one VarInfo to be used in the test
        /// </summary>
        /// <param name="varInfo"></param>
        public AtLeastOneDifferentFromZeroCondition(VarInfo varInfo)
        {
            if (varInfo == null)
            {
                throw new ArgumentNullException("varInfo");
            }
            this._varInfo = varInfo;
            this._controlledVarInfos = new List<VarInfo> { this._varInfo };
        }

        /// <summary>
        /// See the corresponding inteface method documentation: <see cref="ICondition.IsApplicable">ICondition class, IsApplicable method</see>
        /// </summary>
        /// <param name="nonApplicabilityError"></param>
        /// <returns></returns>
        public bool IsApplicable(out string nonApplicabilityError)
        {
            nonApplicabilityError = null;
            if (this._varInfo.ValueType == null)
            {
                throw new Exception("Error on VarInfo '" + this._varInfo.Name + "': cannot verify a VarInfo without a ValueType");
            }
            if (!_applicableVarInfoValueTypes.Contains(this._varInfo.ValueType))
            {
                nonApplicabilityError = "Cannot apply a AtLeastOneDifferentFromZeroCondition to a " + this._varInfo.ValueType.Name + " VarInfo";
                return false;
            }
            if (!((this._varInfo.CurrentValue == null) || this._varInfo.ValueType.TypeForCurrentValue.IsAssignableFrom(this._varInfo.CurrentValue.GetType())))
            {
                nonApplicabilityError = string.Concat(new object[] { "VarInfo '", this._varInfo.Name, "' CurrentValue has incorrect type. Actual: ", this._varInfo.CurrentValue.GetType(), ". Expected: ", this._varInfo.ValueType.TypeForCurrentValue });
                return false;
            }
            return true;
        }

        /// <summary>
        /// See the corresponding inteface method documentation: <see cref="ICondition.TestCondition">ICondition class, TestCondition method</see>
        /// </summary>
        /// <param name="callID"></param>
        /// <returns></returns>
        public string TestCondition(string callID)
        {
            bool flag;
            int num;
            StringBuilder builder = new StringBuilder();
            if ((this._varInfo.ValueType == VarInfoValueTypes.ArrayDouble) || (this._varInfo.ValueType == VarInfoValueTypes.ListDouble))
            {
                double[] numArray = null;
                if (this._varInfo.ValueType == VarInfoValueTypes.ArrayDouble)
                {
                    numArray = (double[]) this._varInfo.CurrentValue;
                }
                else
                {
                    numArray = ((List<double>) this._varInfo.CurrentValue).ToArray();
                }
                flag = false;
                for (num = 0; num < numArray.GetLength(0); num++)
                {
                    if ((numArray[num] > 0.0) || (numArray[num] < 0.0))
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    builder.Append("all ").Append(this._varInfo.Name).Append("[ ] = 0  (at least one should have been different from 0) ").Append(callID).Append(";\r\n");
                }
                return builder.ToString();
            }
            if ((this._varInfo.ValueType == VarInfoValueTypes.ArrayInteger) || (this._varInfo.ValueType == VarInfoValueTypes.ListInteger))
            {
                int[] numArray2 = null;
                if (this._varInfo.ValueType == VarInfoValueTypes.ArrayInteger)
                {
                    numArray2 = (int[]) this._varInfo.CurrentValue;
                }
                else
                {
                    numArray2 = ((List<int>) this._varInfo.CurrentValue).ToArray();
                }
                flag = false;
                for (num = 0; num < numArray2.GetLength(0); num++)
                {
                    if ((numArray2[num] > 0) || (numArray2[num] < 0))
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    builder.Append("all ").Append(this._varInfo.Name).Append("[ ] = 0  (at least one should have been different from 0) ").Append(callID).Append(";\r\n");
                }
                return builder.ToString();
            }
            double[,] currentValue = (double[,]) this._varInfo.CurrentValue;
            flag = false;
            for (int i = 0; i < currentValue.GetLength(0); i++)
            {
                for (int j = 0; j < currentValue.GetLength(1); j++)
                {
                    if ((currentValue[i, j] > 0.0) || (currentValue[i, j] < 0.0))
                    {
                        flag = true;
                    }
                }
            }
            if (!flag)
            {
                builder.Append("all ").Append(this._varInfo.Name).Append("[,]").Append(" = 0 (at least one should have been different from 0) ").Append(callID).Append(";\r\n");
            }
            throw new Exception("Unreachable code segment!");
        }

        /// <summary>
        /// See the corresponding inteface method documentation: <see cref="ICondition.ApplicableVarInfoValueTypes">ICondition class, ApplicableVarInfoValueTypes property</see>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VarInfoValueTypes> ApplicableVarInfoValueTypes
        {
            get
            {
                return _applicableVarInfoValueTypes;
            }
        }

        /// <summary>
        /// See the corresponding inteface method documentation: <see cref="ICondition.ConditionName">ICondition class, ConditionName property</see>
        /// </summary>
        /// <returns></returns>
        public string ConditionName
        {
            get
            {
                return "At Least one different from Zero";
            }
        }

        /// <summary>
        /// See the corresponding inteface method documentation: <see cref="ICondition.ControlledVarInfos">ICondition class, ControlledVarInfos property</see>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VarInfo> ControlledVarInfos
        {
            get
            {
                return this._controlledVarInfos;
            }
        }
    }
}

