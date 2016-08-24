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


//namespace CRA.Core.MPE.Engine
namespace CRA.ModelLayer.ParametersManagement
{
	/// <summary>
    /// This interface must be implemented by the parameters writers in the Bioma framework.
	/// </summary>
	public interface IValuesWriter
	{
		/// <summary>
		/// Writes the parameters values.
		/// </summary>
		/// <param name="e">set to write</param>
		/// <exception cref="ArgumentNullException">The argument is null.</exception>
		/// <exception cref="InvalidOperationException">Data path has not been
		/// defined.</exception>
		void WriteValues(IParametersSet e);

		/// <summary>
		/// Writes the parameters values only for specified key values.
		/// </summary>
		/// <param name="e">set to write</param>
		/// <param name="keyValues">key values to write</param>
		/// <exception cref="ArgumentNullException">The argument is null.</exception>
		/// <exception cref="InvalidOperationException">Data path has not been
		/// defined.</exception>
		void WriteValues(IParametersSet e, IKeyValue[] keyValues);
	}
}