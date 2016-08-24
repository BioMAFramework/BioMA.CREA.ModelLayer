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
// Created: 23/04/2007
//
// Last Modified: 23/04/2007 (Marco Botta)
//----------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using CRA.ModelLayer.Core;
using CRA.ModelLayer;

namespace CRA.ModelLayer.ParametersManagement
{

   

    /// <summary>
    /// Utility class to help creating instances of the parameters management classes (<see cref="CRA.ModelLayer.ParametersManagement.IParametersSet"> CRA.ModelLayer.ParametersManagement.IParametersSet</see>,<see cref="CRA.ModelLayer.ParametersManagement.ISetDescriptor">CRA.ModelLayer.ParametersManagement.ISetDescriptor</see>,<see cref="IKeyValue">IKeyValue</see>)
    /// </summary>
    public static class DataTypeFactory
    {
        /// <summary>
        /// Creates a new IParametersSet
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="parameters"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IParametersSet NewParametersSet(ISetDescriptor descriptor, VarInfo[] parameters, Dictionary<IKeyValue, Dictionary<VarInfo, List<string>>> values)
        {
            ParametersSet _instance = new ParametersSet();
            _instance.Descriptor = descriptor;
            _instance.Parameters = parameters;
            _instance.Values = values;
            return _instance;
        }

        /// <summary>
        /// Creates a new ISetDescriptor
        /// </summary>
        /// <param name="component"></param>
        /// <param name="model"></param>
        /// <param name="keyType"></param>
        /// <param name="url"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ISetDescriptor NewSetDescriptor(string component, string model, string keyType, string url, string description)
        {
            SetDescriptor _instance = new SetDescriptor();
            _instance.Component = component;
            _instance.Model = model;
            _instance.KeyType = keyType;
            _instance.URL = url;
            _instance.Description = description;
            return _instance;
        }

        /// <summary>
        /// Creates a new IKeyValue
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static IKeyValue NewKeyValue(int id, string name, string description)
        {
            KeyValue _instance = new KeyValue();
            _instance.Id = id;
            _instance.Name = name;
            _instance.Description = description;
            return _instance;
        }

      
        /// <summary>
        /// <c>IParametersSet</c> implementation.
        /// </summary>
        private class ParametersSet :
            IParametersSet
        {
            #region object Members

            public override int GetHashCode()
            {
                return (Descriptor.Component + Descriptor.Model).GetHashCode();
            }

            #endregion

            #region IEquatable<IParametersSet> Members

            public bool Equals(IParametersSet other)
            {
                return (other.Descriptor.Component.Equals(Descriptor.Component) &&
                    other.Descriptor.Model.Equals(Descriptor.Model));
            }

            #endregion

            #region IParametersSet Members

            private ISetDescriptor descriptorVar = null;
            /// <summary>
            /// Set descriptor.
            /// Contains set context data.
            /// </summary>
            public ISetDescriptor Descriptor
            {
                get
                {
                    return descriptorVar;
                }
                set
                {
                    descriptorVar = value;
                }
            }

            private VarInfo[] parametersVar;
            /// <summary>
            /// Parameters definitions.
            /// </summary>
            public VarInfo[] Parameters
            {
                get
                {
                    return parametersVar;
                }
                set
                {
                    parametersVar = value;
                }
            }


            private Dictionary<IKeyValue, Dictionary<VarInfo, List<string>>> valuesVar = null;
            /// <summary>
            /// Values.
            /// </summary>
            public Dictionary<IKeyValue, Dictionary<VarInfo, List<string>>> Values
            {
                get
                {
                    return valuesVar;
                }
                set
                {
                    valuesVar = value;
                }
            }

            #endregion
        }



        /// <summary>
        /// <c>ISetDescriptor</c> implementation.
        /// </summary>
        private class SetDescriptor :
            ISetDescriptor
        {
            #region ISetDescriptor Members

            private string componentVar = null;
            /// <summary>
            /// Component name.
            /// </summary>
            public string Component
            {
                get
                {
                    return componentVar;
                }
                set
                {
                    componentVar = value;
                }
            }

            private string modelVar = null;
            /// <summary>
            /// Domain class.
            /// </summary>
            public string Model
            {
                get
                {
                    return modelVar;
                }
                set
                {
                    modelVar = value;
                }
            }

            private string keyTypeVar = null;
            /// <summary>
            /// Key type.
            /// </summary>
            public string KeyType
            {
                get
                {
                    return keyTypeVar;
                }
                set
                {
                    keyTypeVar = value;
                }
            }

            private string urlVar = null;
            /// <summary>
            /// URL reference for the ontology.
            /// </summary>
            public string URL
            {
                get
                {
                    return urlVar;
                }
                set
                {
                    urlVar = value;
                }
            }

            private string descriptionVar = null;
            /// <summary>
            /// Description.
            /// </summary>
            public string Description
            {
                get
                {
                    return descriptionVar;
                }
                set
                {
                    descriptionVar = value;
                }
            }

            #endregion
        }



        /// <summary>
        /// <c>IKeyValue</c> implementation.
        /// </summary>
        private class KeyValue :
            IKeyValue
        {
            #region IEquatable<IKeyValue> Members

            public bool Equals(IKeyValue other)
            {
                return (Name == other.Name);
            }

            public override string ToString()
            {
                return Name;
            }

            #endregion

            #region IKeyValue Members

            private int idVar;
            /// <summary>
            /// Key value identifier.
            /// </summary>
            public int Id
            {
                get
                {
                    return idVar;
                }
                set
                {
                    idVar = value;
                }
            }

            private string nameVar;
            /// <summary>
            /// Name.
            /// </summary>
            public string Name
            {
                get
                {
                    return nameVar;
                }
                set
                {
                    nameVar = value;
                }
            }

            private string descriptionVar;
            /// <summary>
            /// Description of the key value.
            /// </summary>
            public string Description
            {
                get
                {
                    return descriptionVar;
                }
                set
                {
                    descriptionVar = value;
                }
            }

            #endregion
        }
        /* 8/6/2012 - DFa - refactoring MPE - DCC - begin */
    }
    /* 8/6/2012 - DFa - refactoring MPE - DCC - end */
}