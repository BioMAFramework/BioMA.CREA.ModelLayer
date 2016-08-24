﻿namespace CRA.ModelLayer.Strategy
{
    using CRA.ModelLayer.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Manages the parameters/inputs/outputs/associated strategies of a strategy depending on the switches that the strategy allows (if any).
    /// If the strategy has no switches the returned parameters/inputs/outputs/associated strategies sets will be always the same. 
    /// The parameters/inputs/outputs/associated strategies are cointanined in a <see cref="CRA.ModelLayer.Strategy.ModellingOptions">ModellingOptions object</see>.
    /// </summary>
    /// <remarks>
    /// This code snippet shows how to instantiate a ModellingOptionManager starting from the parameters/inputs/outputs/associated strategies of a strategy.
    /// This piece of code is automatically generated by the SCC application, when creating a new strategy, in the constructor of the strategy.
    /// 
    ///         //Instantiate the ModellingOptions object: this is the container to host all the properties of a strategy
    ///         ModellingOptions mo = new ModellingOptions();
    ///         
    ///         //Create the list of Parameters (in this case only one) by defining every VarInfo object and adding it to the ModellingOptions Parameters property
    /// 		List<VarInfo> _parameters = new List<VarInfo>();
    ///  		VarInfo v1 = new VarInfo();
    /// 		v1.DefaultValue = 5;
    /// 		v1.Description = "Minimum temperature for growth";
    /// 	    v1.Id = 0;
    ///         v1.MaxValue = 15;
    /// 		v1.MinValue = 0;
    /// 		v1.Name = "MinimumTemperatureForGrowth";
    ///         v1.Size = 1;
    ///         v1.Units = "C";
    /// 		v1.URL = "";
    /// 		v1.VarType = EC.JRC.MARS.ModelLayer.Core.VarInfo.Type.STATE;
    /// 		v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
    /// 		_parameters.Add(v1);				
    /// 		mo.Parameters=_parameters;
    /// 		
    /// 		//Create the list of Inputs (in this case only one) by defining every PropertyDescription object and adding it to the ModellingOptions Inputs property
    /// 		List<PropertyDescription> _inputs = new List<PropertyDescription>();
    /// 		PropertyDescription pd1 = new PropertyDescription();
    /// 		pd1.DomainClassType = typeof(CRA.Diseases.SoilBorne.Interfaces.Exogenous);
    /// 		pd1.PropertyName = "SoilTemperatureLayer1";
    /// 		pd1.PropertyType = (( CRA.Diseases.SoilBorne.Interfaces.ExogenousVarInfo.SoilTemperatureLayer1)).ValueType.TypeForCurrentValue;
    /// 		pd1.PropertyVarInfo =( CRA.Diseases.SoilBorne.Interfaces.ExogenousVarInfo.SoilTemperatureLayer1);
    /// 		inputs.Add(pd1);
    ///			mo.Inputs=_inputs;
    /// 			
    /// 		//Create the list of Outputs (in this case only one)by defining every PropertyDescription object and adding it to the ModellingOptions Outputs property
    /// 		List<PropertyDescription> _outputs = new List<PropertyDescription>();
    /// 		PropertyDescription pd3 = new PropertyDescription();
    /// 		pd3.DomainClassType = typeof(CRA.Diseases.SoilBorne.Interfaces.Rates);
    /// 		pd3.PropertyName = "RelativeGrowthRate";
    /// 		pd3.PropertyType =  (( CRA.Diseases.SoilBorne.Interfaces.RatesVarInfo.RelativeGrowthRate)).ValueType.TypeForCurrentValue;
    /// 		pd3.PropertyVarInfo =(  CRA.Diseases.SoilBorne.Interfaces.RatesVarInfo.RelativeGrowthRate);
    /// 		_outputs.Add(pd3);			
    /// 		mo.Outputs=_outputs;
    /// 		
    /// 		//Create the list of Associated strategies (in this case only one) by adding the strategy full name to the ModellingOptions AssociatedStrategies property (list of strings)
    /// 		List<string> lAssStrat = new List<string>();
    /// 		lAssStrat.Add("CRA.Diseases.MySimpleStrategy");
    /// 		mo.AssociatedStrategies = lAssStrat; 		
    ///         //Create the ModellingOptionsManager object using the constructor that requires the ModellingOptions parameters
    /// 		ModellingOptionsManager modellingOptionsManager = new ModellingOptionsManager(mo);
    /// 
    /// If the strategy contains a switch, and the switch value causes a change in the strategies properties, the ModellingOptionsManager must be created to reflect these possible changes.
    /// This piece of code is automatically generated by the SCC application, when creating a new strategy, in the constructor of the strategy, so usually the user does not need to care about this code. SCC can associate input/output/paameters of the strategy to the switches values: please see the SCC documentation for more informations.
    /// For example, imagine to add a switch in the strategy created in the previous example. The switch has 2 values "ONE" and "TWO". Lets say that, if the switch value is set to "TWO" the strategy needs another parameter, otherwise, if set to "ONE" there is only the parameter defined before.
    /// The user has to define two different ModellingOptions obejcts for the two scenarios.
    /// 
    ///         //Instantiate 2 ModellingOptions objects: these are the containers to host all the properties of a strategy in the 2 scenarios
    ///         ModellingOptions mo = new ModellingOptions();
    ///         ModellingOptions mo2 = new ModellingOptions();
    /// 
    ///         //-------------The definition of mo is identical--------------
    /// 
    ///         //Create the list of Parameters (in this case only one) by defining every VarInfo object and adding it to the ModellingOptions Parameters property
    /// 		List<VarInfo> _parameters = new List<VarInfo>();
    ///  		VarInfo v1 = new VarInfo();
    /// 		v1.DefaultValue = 5;
    /// 		v1.Description = "Minimum temperature for growth";
    /// 	    v1.Id = 0;
    ///         v1.MaxValue = 15;
    /// 		v1.MinValue = 0;
    /// 		v1.Name = "MinimumTemperatureForGrowth";
    ///         v1.Size = 1;
    ///         v1.Units = "C";
    /// 		v1.URL = "";
    /// 		v1.VarType = EC.JRC.MARS.ModelLayer.Core.VarInfo.Type.STATE;
    /// 		v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
    /// 		_parameters.Add(v1);				
    /// 		mo.Parameters=_parameters;
    /// 		
    /// 		//Create the list of Inputs (in this case only one) by defining every PropertyDescription object and adding it to the ModellingOptions Inputs property
    /// 		List<PropertyDescription> _inputs = new List<PropertyDescription>();
    /// 		PropertyDescription pd1 = new PropertyDescription();
    /// 		pd1.DomainClassType = typeof(CRA.Diseases.SoilBorne.Interfaces.Exogenous);
    /// 		pd1.PropertyName = "SoilTemperatureLayer1";
    /// 		pd1.PropertyType = (( CRA.Diseases.SoilBorne.Interfaces.ExogenousVarInfo.SoilTemperatureLayer1)).ValueType.TypeForCurrentValue;
    /// 		pd1.PropertyVarInfo =( CRA.Diseases.SoilBorne.Interfaces.ExogenousVarInfo.SoilTemperatureLayer1);
    /// 		inputs.Add(pd1);
    ///			mo.Inputs=_inputs;
    /// 			
    /// 		//Create the list of Outputs (in this case only one)by defining every PropertyDescription object and adding it to the ModellingOptions Outputs property
    /// 		List<PropertyDescription> _outputs = new List<PropertyDescription>();
    /// 		PropertyDescription pd3 = new PropertyDescription();
    /// 		pd3.DomainClassType = typeof(CRA.Diseases.SoilBorne.Interfaces.Rates);
    /// 		pd3.PropertyName = "RelativeGrowthRate";
    /// 		pd3.PropertyType =  (( CRA.Diseases.SoilBorne.Interfaces.RatesVarInfo.RelativeGrowthRate)).ValueType.TypeForCurrentValue;
    /// 		pd3.PropertyVarInfo =(  CRA.Diseases.SoilBorne.Interfaces.RatesVarInfo.RelativeGrowthRate);
    /// 		_outputs.Add(pd3);			
    /// 		mo.Outputs=_outputs;
    /// 		
    /// 		//Create the list of Associated strategies (in this case only one) by adding the strategy full name to the ModellingOptions AssociatedStrategies property (list of strings)
    /// 		List<string> lAssStrat = new List<string>();
    /// 		lAssStrat.Add("CRA.Diseases.MySimpleStrategy");
    /// 		mo.AssociatedStrategies = lAssStrat;
    /// 		
    ///         //-------------Here starts the definition of mo2: the parameter part changes --------------
    /// 
    ///         //Create the list of Parameters (in this case two!!!) by defining every VarInfo objects and adding them to the ModellingOptions Parameters property
    /// 		List<VarInfo> _parameters = new List<VarInfo>();
    ///  		VarInfo v1 = new VarInfo();
    /// 		v1.DefaultValue = 5;
    /// 		v1.Description = "Minimum temperature for growth";
    /// 	    v1.Id = 0;
    ///         v1.MaxValue = 15;
    /// 		v1.MinValue = 0;
    /// 		v1.Name = "MinimumTemperatureForGrowth";
    ///         v1.Size = 1;
    ///         v1.Units = "C";
    /// 		v1.URL = "";
    /// 		v1.VarType = EC.JRC.MARS.ModelLayer.Core.VarInfo.Type.STATE;
    /// 		v1.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
    /// 		_parameters.Add(v1);	
    /// 		VarInfo v2 = new VarInfo();
	///			v2.DefaultValue = 25;
	///			v2.Description = "Optimum temperature for growth";
	///			v2.Id = 0;
	///			v2.MaxValue = 40;
	///			v2.MinValue = 10;
	///			v2.Name = "OptimumTemperatureForGrowth";
	///			v2.Size = 1;
	///			v2.Units = "C";
	///			v2.URL = "";
	///			v2.VarType = EC.JRC.MARS.ModelLayer.Core.VarInfo.Type.STATE;
	///			v2.ValueType = VarInfoValueTypes.GetInstanceForName("Double");
    ///			_parameters.Add(v2);
    /// 		mo2.Parameters=_parameters;
    /// 		
    /// 		//Create the list of Inputs (in this case only one, as before) by defining every PropertyDescription object and adding it to the ModellingOptions Inputs property
    /// 		List<PropertyDescription> _inputs = new List<PropertyDescription>();
    /// 		PropertyDescription pd1 = new PropertyDescription();
    /// 		pd1.DomainClassType = typeof(CRA.Diseases.SoilBorne.Interfaces.Exogenous);
    /// 		pd1.PropertyName = "SoilTemperatureLayer1";
    /// 		pd1.PropertyType = (( CRA.Diseases.SoilBorne.Interfaces.ExogenousVarInfo.SoilTemperatureLayer1)).ValueType.TypeForCurrentValue;
    /// 		pd1.PropertyVarInfo =( CRA.Diseases.SoilBorne.Interfaces.ExogenousVarInfo.SoilTemperatureLayer1);
    /// 		inputs.Add(pd1);
    ///			mo2.Inputs=_inputs;
    /// 			
    /// 		//Create the list of Outputs (in this case only one, as before) by defining every PropertyDescription object and adding it to the ModellingOptions Outputs property
    /// 		List<PropertyDescription> _outputs = new List<PropertyDescription>();
    /// 		PropertyDescription pd3 = new PropertyDescription();
    /// 		pd3.DomainClassType = typeof(CRA.Diseases.SoilBorne.Interfaces.Rates);
    /// 		pd3.PropertyName = "RelativeGrowthRate";
    /// 		pd3.PropertyType =  (( CRA.Diseases.SoilBorne.Interfaces.RatesVarInfo.RelativeGrowthRate)).ValueType.TypeForCurrentValue;
    /// 		pd3.PropertyVarInfo =(  CRA.Diseases.SoilBorne.Interfaces.RatesVarInfo.RelativeGrowthRate);
    /// 		_outputs.Add(pd3);			
    /// 		mo2.Outputs=_outputs;
    /// 		
    /// 		//Create the list of Associated strategies (in this case only one, as before) by adding the strategy full name to the ModellingOptions AssociatedStrategies property (list of strings)
    /// 		List<string> lAssStrat = new List<string>();
    /// 		lAssStrat.Add("CRA.Diseases.MySimpleStrategy");
    /// 		mo2.AssociatedStrategies = lAssStrat;
    /// 
    ///         //-------------Now the conclusive part changes the two ModellingOptions objects mo and mo2, must be put togheter to the switch values
    ///         //Create a doctionary containing, as keys, the possible values of the switch, and as values the to ModellingOptions objects 
    ///         Dictionary<string, ModellingOptions> dict= new Dictionary<string, ModellingOptions>();
	///			dict.Add("ONE",mo);
	///			dict.Add("TWO",mo2);
    ///			//Create a switch object, using a constructor that requires: the switch name, the switch descritiption, the dictionary of the switch's values
	///			Switch s = new Switch("TestSwitch", "My switch", dict);
    ///			
    //          //Create the ModellingOptionsManager object using the constructor that requires the Switch parameters
    /// 		ModellingOptionsManager modellingOptionsManager = new ModellingOptionsManager(s);
    /// 		
    /// This piece of code shows ho to get the properties of a strategy (in this case the Outputs) using the ModellingOptionsManager object
    /// 
    ///         var o1 = myStrategy.ModellingOptionsManager.Outputs Return the current set of outputs (depending on the switches values)
    ///         var o2 = myStrategy.ModellingOptionsManager.AllPossibleOutputs Returns all the possible outputs for each combination of the switches acceptable values
    /// </remarks>
    public class ModellingOptionsManager
    {

        /// <summary>
        /// Name of the trivial switch
        /// </summary>
        public const string NOSWITCHNAME = "No options";

        /// <summary>
        /// Description of the trivial switch
        /// </summary>
        public const string NOSWITCHDESCRIPTION = "";

        private Dictionary<string, Switch> Switches;

        /// <summary>
        /// Constructs a ModellingOptionsManager from a single ModellingOptions instance (no switches defined). IN this case the ModellingOptions will never change.
        /// </summary>
        /// <param name="mo"></param>
        public ModellingOptionsManager(ModellingOptions mo)
        {
            Dictionary<string, ModellingOptions> dictionary2 = new Dictionary<string, ModellingOptions>();
            dictionary2.Add("The only option", mo);
            Dictionary<string, ModellingOptions> modelingOptions = dictionary2;
            this.FillSwitchesDictionary(new Switch("No options", "", modelingOptions), false);

        }

        /// <summary>
        /// Constructs a ModellingOptionsManager from other ModellingOptionsManagers. This constructor is used for building the ModellingOptionsManager of a composite strategy starting from the ModellingOptionsManagers of the simple strategies.
        /// It modelling options will be the union of the modelling options of the input ModellingOptionsManagers.
        /// </summary>
        /// <param name="modellingOptionsManagers">An array of existing ModellingOptionsManagers</param>
        public ModellingOptionsManager(params ModellingOptionsManager[] modellingOptionsManagers)
        {
            Dictionary<string, Switch> dictionary = new Dictionary<string, Switch>();
            bool flag = false;
            List<string> source = new List<string>();
            List<PropertyDescription> list2 = new List<PropertyDescription>();
            List<PropertyDescription> list3 = new List<PropertyDescription>();
            List<VarInfo> list4 = new List<VarInfo>();
            foreach (ModellingOptionsManager manager in modellingOptionsManagers)
            {
                foreach (KeyValuePair<string, Switch> pair in manager.Switches)
                {
                    if (pair.Value.IsNoOptionsSwitch)
                    {
                        source.AddRange(pair.Value.AssociatedStrategies);
                        list2.AddRange(pair.Value.Inputs);
                        list3.AddRange(pair.Value.Outputs);
                        list4.AddRange(pair.Value.Parameters);
                        flag = true;
                    }
                    else if (!dictionary.ContainsKey(pair.Key))
                    {
                        dictionary.Add(pair.Key, pair.Value);
                    }
                }
            }
            if (flag)
            {
                Dictionary<string, ModellingOptions> modelingOptions = new Dictionary<string, ModellingOptions>();
                modelingOptions.Add("The only option", new ModellingOptions(list4.Distinct<VarInfo>(new VarInfoNameComparer()), list2.Distinct<PropertyDescription>(new PropertyDescriptionComparer()), list3.Distinct<PropertyDescription>(new PropertyDescriptionComparer()), source.Distinct<string>()));
                Switch switch2 = new Switch("No options", "", modelingOptions);
                if (!dictionary.ContainsKey(switch2.SwitchName))
                {
                    dictionary.Add(switch2.SwitchName, switch2);
                }
            }
            this.Switches = dictionary;

        }

        /// <summary>
        /// Constructs a ModellingOptionsManager from a set of switches. Each switch defines the way the modelling options change according to the switch value.
        /// </summary>
        /// <param name="switches"></param>
        public ModellingOptionsManager(params Switch[] switches)
        {
            this.FillSwitchesDictionary(switches, false);

        }

        /// <summary>
        /// Constructs a ModellingOptionsManager from a set of switches plus a single ModellingOptions instance. Each switch defines the way the modelling options change according to the switch value. The new ModellingOptions is added to the ones defined by the switches.
        /// </summary>
        /// <param name="mo"></param>
        /// <param name="switches"></param>
        public ModellingOptionsManager(ModellingOptions mo, params Switch[] switches)
        {
            Dictionary<string, ModellingOptions> dictionary2 = new Dictionary<string, ModellingOptions>();
            dictionary2.Add("The only option", mo);
            Dictionary<string, ModellingOptions> modelingOptions = dictionary2;
            this.FillSwitchesDictionary(switches.Union<Switch>(new List<Switch> { new Switch("No options", "", modelingOptions) }), false);

        }

        private void FillSwitchesDictionary(Switch switche, bool addToExistingSwitches)
        {
            if (!addToExistingSwitches)
            {
                this.Switches = new Dictionary<string, Switch>();
            }
            if (!this.Switches.ContainsKey(switche.SwitchName))
            {
                this.Switches.Add(switche.SwitchName, switche);
            }
        }

        private void FillSwitchesDictionary(IEnumerable<Switch> switches, bool addToExistingSwitches)
        {
            if (!addToExistingSwitches)
            {
                this.Switches = new Dictionary<string, Switch>();
            }
            foreach (Switch switch2 in switches)
            {
                if (!this.Switches.ContainsKey(switch2.SwitchName))
                {
                    this.Switches.Add(switch2.SwitchName, switch2);
                }
            }
        }

        ///<summary>
        ///Returns the acceptable values of the specified switch
        ///</summary>
        public IEnumerable<string> GetAcceptableSwitchValues(string switchName)
        {
            if (!this.Switches.ContainsKey(switchName))
            {
                throw new Exception("Switch '" + switchName + "' does not exists");
            }
            return this.Switches[switchName].AcceptableSwitchValues;
        }

        /// <summary>
        /// Returns the enumerable of all the possible parameters for each combination of the switches acceptable values
        /// </summary>
        private Dictionary<string, VarInfo> GetAllPossibleParameters(int notused)
        {
            Dictionary<string, VarInfo> dictionary = new Dictionary<string, VarInfo>();
            foreach (VarInfo info in this.AllPossibleParameters)
            {
                dictionary.Add(info.Name, info);
            }
            return dictionary;
        }
        Dictionary<string, VarInfo> _cache;
        private Dictionary<string, VarInfo> GetAllPossibleParametersCached(int notused)
        {
            if (_cache == null) _cache = GetAllPossibleParameters(notused);
            return _cache;
        }

        /// <summary>
        /// Returns  the <see cref="PropertyDescription">PropertyDescription</see> representing the input specified by name and type of the domain class
        /// </summary>
        /// <param name="inputDomainClassType"></param>
        /// <param name="inputPropertyName"></param>
        /// <returns></returns>
        public PropertyDescription GetInputByName(System.Type inputDomainClassType, string inputPropertyName)
        {
            return (from a in this.Inputs
                    where a.PropertyName.Equals(inputPropertyName) && a.DomainClassType.Equals(inputDomainClassType)
                    select a).FirstOrDefault<PropertyDescription>();
        }

        /// <summary>
        /// Returns  the <see cref="PropertyDescription">PropertyDescription</see> representing the output specified by name and type of the domain class
        /// </summary>
        /// <param name="outputDomainClassType"></param>
        /// <param name="outputPropertyName"></param>
        /// <returns></returns>
        public PropertyDescription GetOutputByName(System.Type outputDomainClassType, string outputPropertyName)
        {
            return (from a in this.Outputs
                    where a.PropertyName.Equals(outputPropertyName) && a.DomainClassType.Equals(outputDomainClassType)
                    select a).FirstOrDefault<PropertyDescription>();
        }

        /// <summary>
        /// Returns the VarInfo object corresponding to the parameter specified by its name
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public VarInfo GetParameterByName(string parameterName)
        {
            VarInfo info;
            try
            {
                info = GetAllPossibleParametersCached(1)[parameterName];
            }
            catch (Exception exception)
            {
                throw new Exception("Error extracting parameter " + parameterName, exception);
            }
            return info;
        }

        private void CachedGetParameterByName()
        {

        }

        internal Switch GetSwitch(string switchName)
        {
            if (!this.Switches.ContainsKey(switchName))
            {
                throw new Exception("Switch '" + switchName + "' does not exists");
            }
            return this.Switches[switchName];
        }

        /// <summary>
        /// Returns the description of the switch
        /// </summary>
        /// <param name="switchName"></param>
        /// <returns></returns>
        public string GetSwitchDescription(string switchName)
        {
            if (!this.Switches.ContainsKey(switchName))
            {
                throw new Exception("Switch '" + switchName + "' does not exists");
            }
            return this.Switches[switchName].SwitchDescription;
        }

        /// <summary>
        /// Returns the current value for the specified switch
        /// </summary>
        public string GetSwitchValue(string switchName)
        {
            if (!this.Switches.ContainsKey(switchName))
            {
                throw new Exception("Switch '" + switchName + "' does not exists");
            }
            return this.Switches[switchName].SwitchValue;
        }

        /// <summary>
        /// For each parameters of the strategy, set the VarInfo default value as the CurrentValue of the VarInfo object representing the parameter.
        /// </summary>
        public void SetParametersDefaultValue()
        {
            foreach (VarInfo info in this.Parameters)
            {
                try
                {
                    List<string> list = new List<string> {
                        info.DefaultValue.ToString()
                    };
                    info.CurrentValue = info.ValueType.Converter.GetParsedValueFromMPE(list);
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Sets the value of the parameter specified by name in the strategy and in its associated strategies
        /// </summary>
        /// <param name="paramName">The parameter name</param>
        /// <param name="paramValue">The value to set as parameter value</param>
        public void SetParameterValueInStrategyAndAssociatedStrategies(string paramName, object paramValue)
        {
            List<VarInfo> list = new List<VarInfo>();
            foreach (KeyValuePair<string, Switch> pair in this.Switches)
            {
                list.AddRange(pair.Value.Parameters);
            }
            IEnumerable<VarInfo> enumerable = from a in list
                                              where a.Name.Equals(paramName)
                                              select a;
            foreach (VarInfo info in enumerable)
            {
                if (info == null)
                {
                    throw new Exception("Parameter '" + paramName + "' not found (or found null) in strategy '" + base.GetType().FullName + "'");
                }
                info.CurrentValue = paramValue;
            }
        }

        /// <summary>
        /// Sets the current value for the specified switch among the acceptable values returned from the GetAcceptableSwitchValues property of the switch
        /// </summary>
        public void SetSwitchValue(string switchName, string switchValue)
        {
            if (!this.Switches.ContainsKey(switchName))
            {
                throw new Exception("Switch '" + switchName + "' does not exists");
            }
            if (!this.GetAcceptableSwitchValues(switchName).Contains<string>(switchValue))
            {
                throw new Exception("Wrong switch value. '" + switchValue + "' is not a valid value for switch '" + switchName + "'");
            }
            this.Switches[switchName].SwitchValue = switchValue;
            _cache = null;
        }

        /// <summary>
        /// Returns the enumerable of all the possible associated strategies for each combination of the switches acceptable values
        /// </summary>
        public IEnumerable<string> AllPossibleAssociatedStrategies
        {
            get
            {
                List<string> source = new List<string>();
                foreach (KeyValuePair<string, Switch> pair in this.Switches)
                {
                    string switchValue = pair.Value.SwitchValue;
                    foreach (string str2 in pair.Value.AcceptableSwitchValues)
                    {
                        pair.Value.SwitchValue = str2;
                        source.AddRange(pair.Value.AssociatedStrategies);
                    }
                    if (switchValue != null)
                    {
                        pair.Value.SwitchValue = switchValue;
                    }
                    else
                    {
                        pair.Value.ResetValueToNull();
                    }
                }
                return source.Distinct<string>();
            }
        }


        /// <summary>
        /// Returns the enumerable of all the possible inputs for each combination of the switches acceptable values
        /// </summary>
        public IEnumerable<PropertyDescription> AllPossibleInputs
        {
            get
            {
                List<PropertyDescription> source = new List<PropertyDescription>();
                foreach (KeyValuePair<string, Switch> pair in this.Switches)
                {
                    string switchValue = pair.Value.SwitchValue;
                    foreach (string str2 in pair.Value.AcceptableSwitchValues)
                    {
                        pair.Value.SwitchValue = str2;
                        source.AddRange(pair.Value.Inputs);
                    }
                    if (switchValue != null)
                    {
                        pair.Value.SwitchValue = switchValue;
                    }
                    else
                    {
                        pair.Value.ResetValueToNull();
                    }
                }
                return source.Distinct<PropertyDescription>(new PropertyDescriptionComparer());
            }
        }

        /// <summary>
        /// Returns the enumerable of all the possible outputs for each combination of the switches acceptable values
        /// </summary>
        public IEnumerable<PropertyDescription> AllPossibleOutputs
        {
            get
            {
                List<PropertyDescription> source = new List<PropertyDescription>();
                foreach (KeyValuePair<string, Switch> pair in this.Switches)
                {
                    string switchValue = pair.Value.SwitchValue;
                    foreach (string str2 in pair.Value.AcceptableSwitchValues)
                    {
                        pair.Value.SwitchValue = str2;
                        source.AddRange(pair.Value.Outputs);
                    }
                    if (switchValue != null)
                    {
                        pair.Value.SwitchValue = switchValue;
                    }
                    else
                    {
                        pair.Value.ResetValueToNull();
                    }
                }
                return source.Distinct<PropertyDescription>(new PropertyDescriptionComparer());
            }
        }

        /// <summary>
        /// Returns the enumerable of all the possible parameters for each combination of the switches acceptable values
        /// </summary>
        public IEnumerable<VarInfo> AllPossibleParameters
        {
            get
            {
                List<VarInfo> source = new List<VarInfo>();
                foreach (KeyValuePair<string, Switch> pair in this.Switches)
                {
                    string switchValue = pair.Value.SwitchValue;
                    foreach (string str2 in pair.Value.AcceptableSwitchValues)
                    {
                        pair.Value.SwitchValue = str2;
                        source.AddRange(pair.Value.Parameters);
                    }
                    if (switchValue != null)
                    {
                        pair.Value.SwitchValue = switchValue;
                    }
                    else
                    {
                        pair.Value.ResetValueToNull();
                    }
                }
                return source.Distinct<VarInfo>(new VarInfoNameComparer());
            }
        }

        /// <summary>
        /// Return the current set of associated strategies (depending on the switches values) of the strategy whose the ModellingOptionsManager belongs to.
        /// </summary>
        public IEnumerable<string> AssociatedStrategies
        {
            get
            {
                List<string> source = new List<string>();
                foreach (KeyValuePair<string, Switch> pair in this.Switches)
                {
                    source.AddRange(pair.Value.AssociatedStrategies);
                }
                return source.Distinct<string>();
            }
        }

        /// <summary>
        /// Returns the current set of inputs (depending on the switches values) of the strategy whose the ModellingOptionsManager belongs to.
        /// </summary>
        public IEnumerable<PropertyDescription> Inputs
        {
            get
            {
                List<PropertyDescription> source = new List<PropertyDescription>();
                foreach (KeyValuePair<string, Switch> pair in this.Switches)
                {
                    source.AddRange(pair.Value.Inputs);
                }
                return source.Distinct<PropertyDescription>(new PropertyDescriptionComparer());
            }
        }

        /// <summary>
        /// Return the current set of inputs (depending on the switches values) of the strategy whose the ModellingOptionsManager belongs to.
        /// </summary>
        public IEnumerable<PropertyDescription> Outputs
        {
            get
            {
                List<PropertyDescription> source = new List<PropertyDescription>();
                foreach (KeyValuePair<string, Switch> pair in this.Switches)
                {
                    source.AddRange(pair.Value.Outputs);
                }
                return source.Distinct<PropertyDescription>(new PropertyDescriptionComparer());
            }
        }

        /// <summary>
        /// Returns the current set of parameters (depending on the switches values) of the strategy whose the ModellingOptionsManager belongs to.
        /// </summary>
        public IEnumerable<VarInfo> Parameters
        {
            get
            {
                List<VarInfo> source = new List<VarInfo>();
                foreach (KeyValuePair<string, Switch> pair in this.Switches)
                {
                    source.AddRange(pair.Value.Parameters);
                }
                return source.Distinct<VarInfo>(new VarInfoNameComparer());
            }
        }

        /// <summary>
        /// Returns the name of the switches defined in the ModellingOptionsManager. If a switch was defined with just one acceptable value (trivial switch) it will not be shown.
        /// </summary>
        public IEnumerable<string> SwitchesNames
        {
            get
            {
                return (from a in this.Switches.Values
                        where !a.IsNoOptionsSwitch
                        select a.SwitchName);
            }
        }
    }
}

