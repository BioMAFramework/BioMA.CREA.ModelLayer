namespace CRA.ModelLayer.MetadataTypes
{
    using System.Collections.Generic;
    using System.Linq;

     /// <summary>
    /// Defines the publisher data of a software component by using a list of keys (publisher fields) and values (publisher data)
    /// </summary>
    /// <remarks>
    /// Example of usage
    /// 		PublisherData _pd = new PublisherData();
	///			_pd.Add("Creator", "Marcello Donatelli");
	///			_pd.Add("Date", "20/09/2012");
    ///			_pd.Add("Publisher", "CRA");
    ///			_pd.Add("Title", "My model");
    ///			_pd.Add("Contributor", "UNIMI");
    ///			_pd.Add("Subject", "An agronomical model");
    /// </remarks>
    public class PublisherData : Dictionary<string, string>
    {
     

        /// <summary>
        /// Create  an instance of PublisherData
        /// </summary>
        public PublisherData()
        {
            
        }


        /// <summary>
        /// Returns the data as collection of pairs of strings (keys-values): fields, field values
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, string>> GetValorizedItems()
        {
            return this.Where(kvp => (kvp.Value != null));
        }
    }
}

