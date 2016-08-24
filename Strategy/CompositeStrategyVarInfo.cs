namespace CRA.ModelLayer.Strategy
{
    using CRA.ModelLayer.Core;
    using System;

    /// <summary>
    /// <see cref="VarInfo">VarInfo</see> extension to manage the Current Value of the VarInfo of a composite strategy accordingly to the Current Value of the VarInfo of the simple class behind
    /// </summary>
    public class CompositeStrategyVarInfo : VarInfo
    {
        private readonly IStrategy _childStrategy;
        private readonly string _paramName;

        /// <summary>
        /// Creates the CompositeStrategyVarInfo from the associated strategy and the associated strategy's VarInfo name
        /// </summary>
        /// <param name="childStrategy"></param>
        /// <param name="varInfoName"></param>
        public CompositeStrategyVarInfo(IStrategy childStrategy, string varInfoName)
        {
            VarInfo parameterByName = childStrategy.ModellingOptionsManager.GetParameterByName(varInfoName);
            if (parameterByName == null)
            {
                throw new Exception("Parameter '" + varInfoName + "' not found (or found null) in strategy '" + childStrategy.GetType().FullName + "'");
            }
            base.DefaultValue = parameterByName.DefaultValue;
            base.MaxValue = parameterByName.MaxValue;
            base.MinValue = parameterByName.MinValue;
            base.Id = parameterByName.Id;
            base.Name = parameterByName.Name;
            base.Size = parameterByName.Size;
            base.Units = parameterByName.Units;
            base.URL = parameterByName.URL;
            base.ValueType = parameterByName.ValueType;
            base.VarType = parameterByName.VarType;
            base.Description = parameterByName.Description;
            this._childStrategy = childStrategy;
            this._paramName = varInfoName;
        }

        /// <summary>
        /// Creates the CompositeStrategyVarInfo from the associated strategy and the associated strategy's VarInfo. The name of the associated strategy's VarInfo can be different from the name of the VarInfo in the strategy
        /// </summary>
        /// <param name="childStrategy"></param>
        /// <param name="varInfoNameInTheCompositeStrategy">VarInfo name in the composite (parent) strategy</param>
        /// <param name="varInfoNameinTheAssociatedStrategy">VarInfo name in the associated (child) strategy</param>
        public CompositeStrategyVarInfo(IStrategy childStrategy, string varInfoNameInTheCompositeStrategy, string varInfoNameinTheAssociatedStrategy)
        {
            VarInfo parameterByName = childStrategy.ModellingOptionsManager.GetParameterByName(varInfoNameinTheAssociatedStrategy);
            if (parameterByName == null)
            {
                throw new Exception("Parameter '" + varInfoNameinTheAssociatedStrategy + "' not found (or found null) in strategy '" + childStrategy.GetType().FullName + "'");
            }
            base.DefaultValue = parameterByName.DefaultValue;
            base.MaxValue = parameterByName.MaxValue;
            base.MinValue = parameterByName.MinValue;
            base.Id = parameterByName.Id;
            base.Name = varInfoNameInTheCompositeStrategy;
            base.Size = parameterByName.Size;
            base.Units = parameterByName.Units;
            base.URL = parameterByName.URL;
            base.ValueType = parameterByName.ValueType;
            base.VarType = parameterByName.VarType;
            base.Description = parameterByName.Description;
            this._childStrategy = childStrategy;
            this._paramName = varInfoNameinTheAssociatedStrategy;
        }

        /// <summary>
        /// Set/get the value of the VarInfo to/from the VarInfo of the associated strategy
        /// </summary>
        public override object CurrentValue
        {
            get
            {
                return this._childStrategy.ModellingOptionsManager.GetParameterByName(this._paramName).CurrentValue;
            }
            set
            {
                this._childStrategy.ModellingOptionsManager.GetParameterByName(this._paramName).CurrentValue = value;
            }
        }
    }
}

