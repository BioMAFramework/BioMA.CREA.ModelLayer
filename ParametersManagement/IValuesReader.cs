#region -- Copyright --
//-----------------------------------------------------------------------------
// Copyright (C) 2006 Informatica Ambientale Srl.
// All rights reserved.
//
// This source code is intended for internal use only as part of the
// "Parameter Editor" Development Project and/or Documentation.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF TITLE, MERCHANTABILITY, AGAINST INFRINGEMENT AND/OR FITNESS
// FOR A PARTICULAR PURPOSE.
//-----------------------------------------------------------------------------

//----------------------------------------------------------------
// Author: Marco Botta
// Created: 16/04/2007
//
// Last Modified: 16/04/2007 (Marco Botta)
//----------------------------------------------------------------
#endregion



using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;



namespace CRA.ModelLayer.ParametersManagement
{
    /* 10/10/2012 - DFa - make this an abstract class for completing the dependency injection modification - begin */
    /* old - begin */
    ///// <summary>
    ///// Parameters reader.
    ///// </summary>
    //public interface IValuesReader
    //{
    //    /// <summary>
    //    /// Reads the values of parameter set from the data source.
    //    /// </summary>
    //    /// <returns>parameters set</returns>
    //    /// <exception cref="InvalidOperationException">Data path has not been
    //    /// defined.</exception>
    //    /// <exception cref="InvalidDataException">The data have not the correct
    //    /// format.</exception>
    //    /// <exception cref="IOException">An I/O error is occured.</exception>
    //    IParametersSet ReadValues();
    //}
    /* old - end */
    /// <summary>
    /// This interface must be implemented by the parameters readers in the Bioma framework.
    /// </summary>
    public abstract class IValuesReader
    {
        private List<string> _parameterKeyValues = new List<string>();

        /// <summary>
        /// Reads the values of parameter sets from the form of persistence.
        /// </summary>
        /// <returns>instance of <see cref="IParametersSet"> parameters set</see></returns> 
        protected abstract IParametersSet InternalReadValues();

        /// <summary>
        /// Reads the values of parameter sets from the form of persistence using the passed reader
        /// </summary>
        /// <param name="reader">The reader to be used</param>
        /// <returns></returns>
        protected static IParametersSet InvokeInternalReadValues(IValuesReader reader)
        {
            return reader.InternalReadValues();
        }

        /// <summary>
        /// Return the acceptable values of the parameters key
        /// </summary>
        public IList<string> ParameterKeyValues
        {
            get { return _parameterKeyValues; }
        }


        /// <summary>
        /// Reads the values of parameter sets from the form of persistence.
        /// </summary>
        /// <returns>instance of <see cref="IParametersSet"> parameters set</see></returns> 
        public IParametersSet ReadValues()
        {
            // call the abstract version and store the list of parameter key values
            IParametersSet _parameterSet = InternalReadValues();
            _parameterKeyValues = _parameterSet.Values.Select(kvp => kvp.Key.Name).ToList();
            return _parameterSet;
        }

        /// <summary>
        /// Loads the values of parameter sets in the passed parameter class
        /// </summary>
        /// <param name="parametersKey">The parameter key</param>
        /// <param name="parameterClass">The parameter class to fill with parameter values</param>
        protected internal virtual void LoadParameters(string parametersKey, IParameters parameterClass/*, params  KeyValuePair<string, string>[] varInfoNamesToIgnore*/)
        {
            ParametersIO _parametersIO = new ParametersIO(parameterClass);
            _parametersIO.Reader = this;
            _parametersIO.LoadParameters(parametersKey/*, varInfoNamesToIgnore*/);
        }

        /// <summary>
        /// Loads the values of parameter sets in the passed parameter class using the specified reader
        /// </summary>
        /// <param name="reader">The reader to be used</param>
        /// <param name="parametersKey">The parameter key</param>
        /// <param name="parameterClass">The parameter class to fill with parameter values</param>
        protected static void InvokeLoadParameters(IValuesReader reader, string parametersKey, IParameters parameterClass)
        {
            reader.LoadParameters(parametersKey, parameterClass);
        }
    }

    /* 10/10/2012 - DFa - make this an abstract class for completing the dependency injection modification - end */
}