namespace CRA.ModelLayer.Strategy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using CRA.ModelLayer.Core;

    ///<summary>
    /// Extension method for the IStrategy interface
    ///</summary>
    public static class IStrategyExtensionMethods
    {
        /// <summary>
        /// IStrategy extension method.
        /// Returns the parameters of the strategy depending on the switches that the strategy allows (if any).
        /// If the strategy has no switches the returned parameters set will be always the same.
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IEnumerable<VarInfo> Parameters(this IStrategy strategy)
        {
            return strategy.ModellingOptionsManager.Parameters;
        }

        /// <summary>
        /// Returns all the possible parameters of the strategy, not relying on the value of the strategy switches
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IEnumerable<VarInfo> AllPossibleParameters(this IStrategy strategy)
        {
            return strategy.ModellingOptionsManager.AllPossibleParameters;
        }

        ///<summary>
        /// Return the description of the input properties depending on the switches values
        ///</summary>
        public static IEnumerable<PropertyDescription> Inputs(this IStrategy strategy)
        {
            return strategy.ModellingOptionsManager.Inputs;
        }

        /// <summary>
        /// Returns all the possible inputs of the strategy, not relying on the value of the strategy switches
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyDescription> AllPossibleInputs(this IStrategy strategy)
        {
            return strategy.ModellingOptionsManager.AllPossibleInputs;
        }

        ///<summary>
        /// Return the description of the output properties depending on the switches values
        ///</summary>
        public static IEnumerable<PropertyDescription> Outputs(this IStrategy strategy)
        {
            return strategy.ModellingOptionsManager.Outputs;
        }

        /// <summary>
        /// Returns all the possible outputs of the strategy, not relying on the value of the strategy switches
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyDescription> AllPossibleOutputs(this IStrategy strategy)
        {
            return strategy.ModellingOptionsManager.AllPossibleOutputs;
        }

        ///<summary>
        /// Return the description of the associated strategies depending on the switches values
        ///</summary>
        public static IEnumerable<string> AssociatedStrategies(this IStrategy strategy)
        {
            return strategy.ModellingOptionsManager.AssociatedStrategies;
        }

        /// <summary>
        /// Returns all the possible associated classes of the strategy, not relying on the value of the strategy switches
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IEnumerable<string> AllPossibleAssociatedStrategies(this IStrategy strategy)
        {
            return strategy.ModellingOptionsManager.AllPossibleAssociatedStrategies;
        }

        /// <summary>
        /// Set the value to a property of the strategy by using the reflection to get the property. If the property is not found, or if the value type is not suitable for the property, it throws an exception
        /// </summary>
        /// <param name="strategy">The instance of the strategy</param>
        /// <param name="paramName">The property name</param>
        /// <param name="paramValue">The value to set in the property</param>
        public static void SetParameterValueThroughReflection(this IStrategy strategy, string paramName, object paramValue)
        {
            PropertyInfo info = (from a in strategy.GetType().GetProperties()
                where a.Name.Equals(paramName)
                select a).FirstOrDefault<PropertyInfo>();
            if (info != null)
            {
                info.SetValue(strategy, paramValue, new object[0]);
            }
        }
    }
}

