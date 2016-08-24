namespace CRA.ModelLayer.Core
{
    /// <summary>
    /// Identifies a generic data provider. The data provider is the way a generic class can receive data from an external source (e.g. a form of persistence, like a DB).
    /// This empty interface is used as a marker for identifieng the data providers, but does not require any implementation, since the real methods are specific to the kind of data to retrieve.
    /// </summary>
    public interface IDataProvider
    {
    }
}

