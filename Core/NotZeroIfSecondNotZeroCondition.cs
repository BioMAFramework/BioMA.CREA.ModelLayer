namespace CRA.ModelLayer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>    
    /// Condition that works on two VarInfo objects: it tests that the first VarInfo value is different from zero, when the second VarInfo value is different from zero. 
    /// If the second is different from zero, while the first is equal to zero, it gives error since the condition is not satisfied. In all the other cases the condition is satisfied.
    /// It is an implementation of <see cref="ICondition">ICondition</see> interface.
    /// </summary>
    public class NotZeroIfSecondNotZeroCondition : ICondition
    {
        private static List<VarInfoValueTypes> _applicableVarInfoValueTypes = new List<VarInfoValueTypes>();
        private List<VarInfo> _controlledVarInfos;
        private VarInfo _firstVarInfo;
        private VarInfo _secondVarInfo;

        static NotZeroIfSecondNotZeroCondition()
        {
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.Double);
            _applicableVarInfoValueTypes.Add(VarInfoValueTypes.Integer);
        }

        /// <summary>
        /// Builds the instance of this condition. It requires two VarInfo to be used in the test
        /// </summary>
        /// <param name="firstVarInfo"></param>
        /// <param name="secondVarInfo"></param>
        public NotZeroIfSecondNotZeroCondition(VarInfo firstVarInfo, VarInfo secondVarInfo)
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
                throw new Exception("Error on VarInfo '" + this._firstVarInfo.Name + "' or '" + this._secondVarInfo.Name + "': cannot verify a VarInfo without a ValueType");
            }
            if (!_applicableVarInfoValueTypes.Contains(this._firstVarInfo.ValueType))
            {
                nonApplicabilityError = "Error on VarInfo '" + this._firstVarInfo.Name + "': cannot apply a NotZeroIfSecondNotZeroCondition to a " + this._firstVarInfo.ValueType.Name + " VarInfo";
                return false;
            }
            if (!_applicableVarInfoValueTypes.Contains(this._secondVarInfo.ValueType))
            {
                nonApplicabilityError = "Error on VarInfo '" + this._secondVarInfo.Name + "': cannot apply a NotZeroIfSecondNotZeroCondition to a " + this._secondVarInfo.ValueType.Name + " VarInfo";
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
            if ((currentValue == 0.0) && (num2 != 0.0))
            {
                builder.Append(this._firstVarInfo.Name).Append(" cannot be = 0 if ").Append(this._secondVarInfo.Name).Append(" is <> 0 (").Append(this._secondVarInfo.Name).Append(" = ").Append(this._secondVarInfo.CurrentValue.ToString()).Append(" ").Append(callID).Append(";\r\n");
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
                return "Cannot be Zero if second is not Zero";
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

