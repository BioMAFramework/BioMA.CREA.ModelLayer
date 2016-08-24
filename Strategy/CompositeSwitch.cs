namespace CRA.ModelLayer.Strategy
{
    using CRA.ModelLayer.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// <see cref="CRA.ModelLayer.Strategy.Switch"> CRA.ModelLayer.Strategy.Switch</see> extension to manage manage the Value of the Switch of a composite strategy accordingly to the Value of the Switch of the simple class behind
    /// </summary>
    public class CompositeSwitch : Switch
    {
        private readonly IStrategy _childStrategy;

        /// <summary>
        /// Creates a CompositeSwitch from the instance of the associated strategy, the associated strategy's switch name and the associated strategy's switch description
        /// </summary>
        /// <param name="childStrategy"></param>
        /// <param name="switchName"></param>
        /// <param name="switchDescription"></param>
        public CompositeSwitch(IStrategy childStrategy, string switchName, string switchDescription) : base(switchName, switchDescription, childStrategy.ModellingOptionsManager.GetSwitch(switchName).ModelingOptions)
        {
            this._childStrategy = childStrategy;
        }


        /// <summary>
        /// Returns the current set of associated strategies (depending on the switch value) of the strategy whose the Switch belongs to.
        /// </summary>
        internal override IEnumerable<string> AssociatedStrategies
        {
            get
            {
                if (string.IsNullOrEmpty(this.SwitchValue))
                {
                    throw new Exception("Switch '" + base.SwitchName + "' value not set");
                }
                return base.AssociatedStrategies.Union<string>(this._childStrategy.AllPossibleAssociatedStrategies());
            }
        }

        /// <summary>
        /// Returns the current set of inputs (depending on the switch value) of the strategy whose the Switch belongs to.
        /// </summary>
        internal override IEnumerable<PropertyDescription> Inputs
        {
            get
            {
                if (string.IsNullOrEmpty(this.SwitchValue))
                {
                    throw new Exception("Switch '" + base.SwitchName + "' value not set");
                }
                return base.Inputs.Union<PropertyDescription>(this._childStrategy.AllPossibleInputs());
            }
        }

        /// <summary>
        /// Returns the current set of outputs (depending on the switch value) of the strategy whose the Switch belongs to.
        /// </summary>
        internal override IEnumerable<PropertyDescription> Outputs
        {
            get
            {
                if (string.IsNullOrEmpty(this.SwitchValue))
                {
                    throw new Exception("Switch '" + base.SwitchName + "' value not set");
                }
                return base.Outputs.Union<PropertyDescription>(this._childStrategy.AllPossibleOutputs());
            }
        }

        /// <summary>
        /// Returns the current set of parameters (depending on the switch value) of the strategy whose the Switch belongs to.
        /// </summary>
        internal override IEnumerable<VarInfo> Parameters
        {
            get
            {
                if (string.IsNullOrEmpty(this.SwitchValue))
                {
                    throw new Exception("Switch '" + base.SwitchName + "' value not set");
                }
                return base.Parameters.Union<VarInfo>(this._childStrategy.AllPossibleParameters());
            }
        }

        /// <summary>
        /// Get/set the current value for the switch on the basis of the value of the switch of the child strategy
        /// </summary>
        public override string SwitchValue
        {
            get
            {
                return this._childStrategy.ModellingOptionsManager.GetSwitchValue(base.SwitchName);
            }
            set
            {
                this._childStrategy.ModellingOptionsManager.SetSwitchValue(base.SwitchName, value);
            }
        }
    }
}

