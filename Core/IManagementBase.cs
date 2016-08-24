namespace CRA.ModelLayer.Core
{
    using CRA.ModelLayer.MetadataTypes;
    using System;
    using System.Xml;

    /// <summary>
    /// Base interface for impact classes, implemented via management specific interfaces. It contains the logics for saving/reading the impact content to/from XML nodes of the management definition XML files.
    /// One of the possible implementation of the IManagementBase, is the interface "CRA.AgroManagement.IManagement" defined in library "CRA.AgroManagemet2014.dll". In this specific case we reference it as the "AGROmanagement".
    /// </summary>
    public interface IManagementBase : IDomainClass, IAnnotatable, IVarInfoClass
    {
        /// <summary>
        /// Reads management data from an XmlNode from the management definition XML content
        /// </summary>
        /// <param name="node">The xml node containing management data</param>
        void LoadXml(XmlNode node);

        /// <summary>
        /// Stores the actual data of the management using the specified xml writer
        /// </summary>
        /// <param name="writer">The XmlTextWriter used to write the data</param>
        void SaveXml(XmlTextWriter writer);

        /// <summary>
        /// Tests the pre-conditions on the management input data
        /// </summary>
        /// <param name="callID"></param>
        string TestPreconditions(string callID);
    }
}

