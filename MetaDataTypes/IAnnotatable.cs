namespace CRA.ModelLayer.MetadataTypes
{
    using System;

    /// <summary>
    /// The type of all classes that need to be documented with a URL and a description
    /// </summary>
    public interface IAnnotatable
    {
        /// <summary>
        /// Description of the class
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The URL of the class metadata
        /// </summary>
        string URL { get; }

    }
}

