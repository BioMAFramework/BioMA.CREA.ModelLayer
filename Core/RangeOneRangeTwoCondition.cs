namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>    
    /// Condition that works on two VarInfo objects: it tests that if one VarInfo value is inside its validity range, even a second VarInfo value is inside its own range. 
    /// If the second is inside the range, while the first outside the range, it gives error since the condition is not satisfied. In all the other cases the condition is satisfied.
    /// To be in the validity range means that a VarInfo value is greater than the VarInfo minimum value and less than the VarInfo maximum value.
    /// It is an implementation of <see cref="ICondition">ICondition</see> interface.
    /// </summary>
    public class RangeOneRangeTwoCondition : ICondition
    {
        private static List<VarInfoValueTypes> _applicableVarInfoValueTypes = new List<VarInfoValueTypes>();
        private List<VarInfo> _controlledVarInfos;
        private VarInfo _firstVarInfo;
        private VarInfo _secondVarInfo;

        static RangeOneRangeTwoCondition()
        {
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.Double);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.Integer);
        }

        /// <summary>
        /// Builds the instance of this condition. It requires two VarInfo to be used in the test
        /// </summary>
        /// <param name="firstVarInfo"></param>
        /// <param name="secondVarInfo"></param>
        public RangeOneRangeTwoCondition(VarInfo firstVarInfo, VarInfo secondVarInfo)
        {
            if (firstVarInfo == null)
            {
                throw new ArgumentNullException("firstVarInfo");
            }
            if (secondVarInfo == null)
            {
                throw new ArgumentNullException("secondVarInfo");
            }
            this._firstVarInfo = firstVarInfo;
            this._secondVarInfo = secondVarInfo;
            this._controlledVarInfos = new List<VarInfo> { this._firstVarInfo, this._secondVarInfo };
        }

        /// <summary>
        /// See the corresponding inteface method documentation: <see cref="ICondition.IsApplicable">ICondition class, IsApplicable method</see>
        /// </summary>
        /// <param name="nonApplicabilityError"></param>
        /// <returns></returns>
        public bool IsApplicable(out string nonApplicabilityError)
        {
            nonApplicabilityError = null;
            if ((this._firstVarInfo.ValueType == null) || (this._secondVarInfo.ValueType == null))
            {
                throw new Exception("Error on VarInfo '" + this._firstVarInfo.Name + "' or '" + this._secondVarInfo.Name + "':cannot verify a VarInfo without a ValueType");
            }
            if (!_applicableVarInfoValueTypes.Contains(this._firstVarInfo.ValueType))
            {
                nonApplicabilityError = "Error on VarInfo '" + this._firstVarInfo.Name + "': cannot apply a RangeOneRangeTwoCondition to a " + this._firstVarInfo.ValueType.Name + " VarInfo";
                return false;
            }
            if (!_applicableVarInfoValueTypes.Contains(this._secondVarInfo.ValueType))
            {
                nonApplicabilityError = "Error on VarInfo '" + this._secondVarInfo.Name + "': cannot apply a RangeOneRangeTwoCondition to a " + this._secondVarInfo.ValueType.Name + " VarInfo";
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
            if (((this._firstVarInfo.ValueType != VarInfoValueTypes.Double) && (this._firstVarInfo.ValueType != VarInfoValueTypes.Integer)) || ((this._secondVarInfo.ValueType != VarInfoValueTypes.Double) && (this._secondVarInfo.ValueType != VarInfoValueTypes.Integer)))
            {
                throw new Exception("Unsupported VarInfoValueType: first VarInfo: " + this._firstVarInfo.ValueType.Name + ", second VarInfo: " + this._secondVarInfo.ValueType.Name);
            }
            double currentValue = 0.0;
            if (this._firstVarInfo.ValueType == VarInfoValueTypes.Integer)
            {
                currentValue = (int) this._firstVarInfo.CurrentValue;
            }
            else
            {
                currentValue = (double) this._firstVarInfo.CurrentValue;
            }
            double num2 = 0.0;
            if (this._secondVarInfo.ValueType == VarInfoValueTypes.Integer)
            {
                num2 = (int) this._secondVarInfo.CurrentValue;
            }
            else
            {
                num2 = (double) this._secondVarInfo.CurrentValue;
            }
            StringBuilder builder = new StringBuilder("");
            if ((((currentValue < this._firstVarInfo.MinValue) || (currentValue > this._firstVarInfo.MaxValue)) && (num2 >= this._secondVarInfo.MinValue)) && (num2 <= this._secondVarInfo.MaxValue))
            {
                builder.Append(this._firstVarInfo.Name).Append(" = ").Append(this._firstVarInfo.CurrentValue.ToString()).Append(". It cannot outrange (").Append(this._firstVarInfo.MinValue.ToString()).Append("-").Append(this._firstVarInfo.MaxValue.ToString()).Append(") if ").Append(this._secondVarInfo.Name).Append(" is within (").Append(this._secondVarInfo.MinValue.ToString()).Append("-").Append(this._secondVarInfo.MaxValue.ToString()).Append(") ").Append(callID).Append(";\r\n");
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
                return "Range One Range Two";
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

