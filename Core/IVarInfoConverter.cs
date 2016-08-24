using System.Collections.Generic;
using System.Xml.Linq;

namespace CRA.ModelLayer.Core
{
    /// <summary>
    /// Manages the logic for parsing string or XML representation of the <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see>
    /// to a correctly parsed value and back.<br/>
    /// An implementation of this interface has to be written for each parsed type of a <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see>,
    /// precisely for each of the <see cref="VarInfoValueTypes">VarInfoValueTypes</see> returned by
    /// <see cref="VarInfoValueTypes.Values">VarInfoValueTypes.Values</see><br/>
    /// Since this interface is used also as a central point in parsing/serializing <see cref="VarInfo">VarInfo</see> values in the
    /// MPE application (that has peculiar formats for serialization),
    /// specific methods are present, namely:
    /// <list type="bullet">
    ///      <listheader>
    ///          <term>
    ///              Method
    ///          </term>
    ///          <description>
    ///              Description
    ///          </description>
    ///      </listheader>
    ///      <item>
    ///          <term>
    ///              <see cref="IVarInfoConverter.GetParsedValue(XElement)">GetParsedValue(XElement)</see>
    ///          </term>
    ///          <description>
    ///              Parses to the correct type the <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> XML
    ///              representation peculiar to the MPE application.
    ///          </description>
    ///      </item>
    ///      <item>
    ///          <term>
    ///              <see cref="IVarInfoConverter.ParseValue(object, string)">ParseValue(object, string)</see>
    ///          </term>
    ///          <description>
    ///              Serializes the <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> parsed
    ///              in its correct type to the XML representation peculiar to the MPE application for the type managed by this instance.
    ///          </description>
    ///      </item>
    ///      <item>
    ///          <term>
    ///              <see cref="IVarInfoConverter.ParseValueForMPE(object)">ParseValueForMPE(object)</see>
    ///          </term>
    ///          <description>
    ///              Serializes the <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> parsed
    ///              in its correct type to the List&lt;string&gt; representation peculiar to the MPE application for the type managed by this instance.
    ///          </description>
    ///      </item>
    ///      <item>
    ///          <term>
    ///              <see cref="IVarInfoConverter.GetParsedValueFromMPE(List{string})">GetParsedValueFromMPE(List&lt;string&gt;)</see>
    ///          </term>
    ///          <description>
    ///              Parses to the correct type the <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> List&lt;string&gt;
    ///              representation peculiar to the MPE application.
    ///          </description>
    ///      </item>
    /// </list>
    /// </summary>
    public interface IVarInfoConverter
    {
        /// <summary>
        /// Parses to the correct type the <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> XML
        /// representation peculiar to the MPE application.
        /// </summary>
        /// <param name="elem">The MPE's peculiar XML representation of the <see cref="VarInfo.CurrentValue">
        /// VarInfo.CurrentValue</see>.</param>
        /// <returns>The correctly parsed value.</returns>
        object GetParsedValue(XElement elem);

        /// <summary>
        /// Serializes the <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> parsed
        /// in its correct type to the XML representation peculiar to the MPE application for the type managed by this instance.
        /// </summary>
        /// <param name="value">The value in its correct type.</param>
        /// <param name="varInfoName">The name of the VarInfo whose <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue
        /// </see> is being serialized.</param>
        /// <returns>The MPE's peculiar XML representation for the type managed by this instance.</returns>
        XElement ParseValue(object value, string varInfoName);

        /// <summary>
        /// Serializes the <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> parsed
        /// in its correct type to the List&lt;string&gt; representation peculiar to the MPE application for the type managed by this instance.
        /// </summary>
        /// <param name="value">The value in its correct type</param>
        /// <returns>The MPE's peculiar List&lt;string&gt; representation for the type managed by this instance.</returns>
        List<string> ParseValueForMPE(object value);

        /// <summary>
        /// Parses to the correct type the <see cref="VarInfo.CurrentValue">VarInfo.CurrentValue</see> List&lt;string&gt;
        /// representation peculiar to the MPE application.
        /// </summary>
        /// <param name="list">The MPE's peculiar List&lt;string&gt; representation of the <see cref="VarInfo.CurrentValue">
        /// VarInfo.CurrentValue</see>.</param>
        /// <returns></returns>
        object GetParsedValueFromMPE(List<string> list);

        /// <summary>
        /// Returns <c>true</c> if the 'elem' XML representation of the value and the 'value' have the same size,
        /// <c>false</c> otherwise.
        /// </summary>
        /// <param name="elem">The XML representation.</param>
        /// <param name="value">The value in its correct type.</param>
        /// <returns><c>true</c> if the 'elem' XML representation of the value and the 'value' have the same size,
        /// <c>false</c> otherwise.</returns>
        bool HaveConsistentSize(XElement elem, object value);

        /// <summary>
        /// Returns <c>true</c> if the 'elem' List&lt;string&gt; representation of the value and the 'value' have the same size,
        /// <c>false</c> otherwise.
        /// </summary>
        /// <param name="list">The List&lt;string&gt; representation.</param>
        /// <param name="value">The value in its correct type.</param>
        /// <returns><c>true</c> if the 'elem' List&lt;string&gt; representation of the value and the 'value' have the same size,
        /// <c>false</c> otherwise.</returns>
        bool HaveConsistentSize(List<string> list, object value);

        /// <summary>
        /// Returns a string representation for the type managed by this instance, compatible with the string representation
        /// used in MPE.
        /// </summary>
        /// <param name="size">The size, when needed for building the type name. (basically for arrays)</param>
        /// <returns>The string representation for the type managed by this instance, compatible with the string representation
        /// used in MPE.</returns>
        string GetTypeNameRepresentation(int size);

        /// <summary>
        /// Returns a string that can be used to instatiate an object conforming to the type represented by this converter, or <c>null</c>
        /// if an instantiation is not necessary, e.g. for primitive types like <c>int</c> or <c>double</c>. The ending semicolumn is not included.
        /// For example, for the VarInfo type 'Date', this method should return "new DateTime()", for a 'ArrayDate' it should return "new DateTime[" + size + "]"
        /// </summary>
        /// <param name="size">The size of the object to be instantiated. Used basically for arrays.</param>
        /// <returns>A string that can be used to instatiate an object conforming to the type represented by this converter.</returns>
        string GetConstructingString(int size);

        /// <summary>
        /// Returns a cloned (deep) copy of the object passed as parameter.
        /// </summary>
        /// <param name="original">The object to clone.</param>
        /// <returns>A cloned (deep) copy of the object passed as parameter.</returns>
        object GetClonedCopy(object original);
    }
}