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
using System.Text;
using CRA.ModelLayer;
using CRA.ModelLayer.Core;



namespace CRA.ModelLayer.ParametersManagement
{
	/// <summary>
	/// Parameters set.
	/// </summary>
	/// <remarks>
	/// This structure represents a set of parameters of a domain class and theirs values.
	/// </remarks>
	public interface IParametersSet :
		IEquatable<IParametersSet>
	{
		/// <summary>
		/// Parameter set descriptor.
		/// Contains set context data.
		/// </summary>
		ISetDescriptor Descriptor
		{
			get;
		}

		/// <summary>
		/// Parameters definitions
		/// </summary>
		VarInfo[] Parameters
		{
			get;
		}

		/// <summary>
		/// Values of the parameters
		/// </summary>
		Dictionary<IKeyValue, Dictionary<VarInfo, List<string>>> Values
		{
			get;
		}

	}

    ///// <summary>
    ///// Factory to create a ISetDescriptor object
    ///// </summary>
    //public class  DescriptorFactory
    //{
      
    //    /// <summary>
    //    /// Method to create a ISetDescriptor object
    //    /// </summary>
    //    /// <param name="component"></param>
    //    /// <param name="model"></param>
    //    /// <param name="keyType"></param>
    //    /// <param name="url"></param>
    //    /// <param name="description"></param>
    //    /// <returns></returns>
    //    public static ISetDescriptor getInstance(string component, string model, string keyType, string url, string description)
    //    {
    //         SetDescriptor _instance= new SetDescriptor();
    //        _instance.Component = component;
    //        _instance.Model = model;
    //        _instance.KeyType = keyType;
    //        _instance.URL = url;
    //        _instance.Description = description;
    //        return (ISetDescriptor)_instance;
    //    }
    //}

	/// <summary>
	/// Descriptor of the parameters set.
	/// </summary>
	public interface ISetDescriptor
	{
		/// <summary>
		/// Component name.
		/// </summary>
		string Component
		{
			get;
		}

		/// <summary>
		/// Model.
		/// </summary>
		string Model
		{
			get;
		}

		/// <summary>
		/// Key type.
		/// </summary>
		string KeyType
		{
			get;
		}

		/// <summary>
		/// URL reference for the ontology.
		/// </summary>
		string URL
		{
			get;
		}

		/// <summary>
		/// Description.
		/// </summary>
		string Description
		{
			get;
		}
	}


	/// <summary>
	/// Value of a key type.
	/// </summary>
	public interface IKeyValue :
		IEquatable<IKeyValue>
	{
		/// <summary>
		/// Key value identifier.
		/// </summary>
		int Id
		{
			get;
		}

		/// <summary>
		/// Key value name.
		/// </summary>
		string Name
		{
			get;
		}

		/// <summary>
		/// Description of the key value.
		/// </summary>
		string Description
		{
			get;
		}
	}
}