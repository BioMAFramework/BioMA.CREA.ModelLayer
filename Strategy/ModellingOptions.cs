namespace CRA.ModelLayer.Strategy
{
    using System;
    using System.Collections.Generic;

    using CRA.ModelLayer.Core;

    ///<summary>
    /// This class is a container for the 'options' of a strategy: the inputs, the outputs, the parameters and the associated strategies.
    /// We use here the word 'option' because these strategy characteristics may change according to a switch value. When a switch value changes, the options that the strategy shows may change. If the strategy has no switches the returned parameters/inputs/outputs/associated strategies sets will be always the same. (see <see cref="CRA.ModelLayer.Strategy.ModellingOptionsManager">ModellingOptionsManager class</see>)
    /// Every one of this 4 kinds of 'options' is returned as a public property.
    ///</summary>
    public class ModellingOptions
    {
        /// <summary>
        /// The associate strategies of the current strategy
        /// </summary>
        public IEnumerable<string> AssociatedStrategies;

        /// <summary>
        /// The inputs  of the current strategy
        /// </summary>
        public IEnumerable<PropertyDescription> Inputs;

        /// <summary>
        /// The outputs  of the current strategy
        /// </summary>
        public IEnumerable<PropertyDescription> Outputs;

        /// <summary>
        /// The parameters of the current strategy
        /// </summary>         
        public IEnumerable<VarInfo> Parameters;


        /// <summary>
        /// Creates a new empty instance of the ModellingOptions
        /// </summary>
        public ModellingOptions()
        {
            Parameters = new List<VarInfo>();
            Inputs = new List<PropertyDescription>();
            Outputs = new List<PropertyDescription>();
            AssociatedStrategies = new List<string>();
        }

        /// <summary>
        /// Creates a new instance of the ModellingOptions from the specified values
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="inputs"></param>
        /// <param name="outputs"></param>
        /// <param name="associatedStrategies"></param>
        public ModellingOptions(IEnumerable<VarInfo> parameters,
                                IEnumerable<PropertyDescription> inputs,
                                IEnumerable<PropertyDescription> outputs,
                                IEnumerable<string> associatedStrategies
            )
        {
            Parameters = parameters;
            Inputs = inputs;
            Outputs = outputs;
            AssociatedStrategies = associatedStrategies;
        }
    }
}

