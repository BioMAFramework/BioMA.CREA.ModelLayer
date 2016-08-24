namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Linq;

    /// <summary>
    /// Condition to test that a VarInfo value is greater than the VarInfo minimum value and less than the VarInfo maximum value.
    /// If the VarInfo is outside the range, it gives error since the condition is not satisfied. In all the other cases the condition is satisfied.
    /// It is an implementation of <see cref="ICondition">ICondition</see> interface.
    /// </summary>
    public class RangeBasedCondition : ICondition
    {
        private static List<VarInfoValueTypes> _applicableVarInfoValueTypes = new List<VarInfoValueTypes>();
        private List<VarInfo> _controlledVarInfos;
        private VarInfo _varInfo;

        static RangeBasedCondition()
        {
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.Double);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.ArrayDouble);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.ListDouble);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.Integer);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.ArrayInteger);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.ListInteger);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.Bidimensional);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.DoubleMatrix);
        }

        /// <summary>
        /// Builds the instance of this condition. It requires one VarInfo to be used in the test
        /// </summary>
        /// <param name="varInfo"></param>

        public RangeBasedCondition(VarInfo varInfo)
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
                nonApplicabilityError = "Cannot apply a RangeBasedCondition to a " + this._varInfo.ValueType.Name + " VarInfo";
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
            int num;
            StringBuilder builder = new StringBuilder();
            if (this._varInfo.ValueType == VarInfoValueTypes.Double)
            {
                if ((((double) this._varInfo.CurrentValue) > this._varInfo.MaxValue) || (((double) this._varInfo.CurrentValue) < this._varInfo.MinValue))
                {
                    builder.Append(this._varInfo.Name).Append(" = ").Append(this._varInfo.CurrentValue.ToString()).Append(" (max=").Append(this._varInfo.MaxValue).Append(" - min=").Append(this._varInfo.MinValue).Append(") ").Append(callID).Append(";\r\n");
                }
                return builder.ToString();
            }
            if (this._varInfo.ValueType == VarInfoValueTypes.Integer)
            {
                if ((((int) this._varInfo.CurrentValue) > this._varInfo.MaxValue) || (((int) this._varInfo.CurrentValue) < this._varInfo.MinValue))
                {
                    builder.Append(this._varInfo.Name).Append(" = ").Append(this._varInfo.CurrentValue.ToString()).Append(" (max=").Append(this._varInfo.MaxValue).Append(" - min=").Append(this._varInfo.MinValue).Append(") ").Append(callID).Append(";\r\n");
                }
                return builder.ToString();
            }
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
                for (num = 0; num < numArray.Length; num++)
                {
                    if ((numArray[num] > this._varInfo.MaxValue) || (numArray[num] < this._varInfo.MinValue))
                    {
                        builder.Append(this._varInfo.Name).Append("[").Append(num.ToString()).Append("]").Append(" = ").Append(numArray[num].ToString()).Append(" (max=").Append(this._varInfo.MaxValue).Append(" - min=").Append(this._varInfo.MinValue).Append(") ").Append(callID).Append(";\r\n");
                    }
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
                for (num = 0; num < numArray2.Length; num++)
                {
                    if ((numArray2[num] > this._varInfo.MaxValue) || (numArray2[num] < this._varInfo.MinValue))
                    {
                        builder.Append(this._varInfo.Name).Append("[").Append(num.ToString()).Append("]").Append(" = ").Append(numArray2[num].ToString()).Append(" (max=").Append(this._varInfo.MaxValue).Append(" - min=").Append(this._varInfo.MinValue).Append(") ").Append(callID).Append(";\r\n");
                    }
                }
                return builder.ToString();
            }
            double[,] currentValue = (double[,]) this._varInfo.CurrentValue;
            for (int i = 0; i < currentValue.GetLength(0); i++)
            {
                for (int j = 0; j < currentValue.GetLength(1); j++)
                {
                    if ((currentValue[i, j] > this._varInfo.MaxValue) || (currentValue[i, j] < this._varInfo.MinValue))
                    {
                        builder.Append(this._varInfo.Name).Append("[").Append(i.ToString()).Append(",").Append(j.ToString()).Append("]").Append(" = ").Append(currentValue[i, j].ToString()).Append("]").Append(" (max=").Append(this._varInfo.MaxValue).Append(" - min=").Append(this._varInfo.MinValue).Append(") ").Append(callID).Append(";\r\n");
                    }
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// See the corresponding inteface method documentation: <see cref="ICondition.ApplicableVarInfoValueTypes">ICondition class, ApplicableVarInfoValueTypes property</see>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VarInfoValueTypes> ApplicableVarInfoValueTypes
        {
            get
            {
                return (from a in _applicableVarInfoValueTypes select a);
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
                return "Range Based";
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

