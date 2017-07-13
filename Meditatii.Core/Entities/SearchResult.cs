using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Meditatii.Core.Entities
{
    /// <summary>
    /// Used to wrap results from search so that we can pass the paging information with them.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class SearchResult<T>
            where T : class
    {
        [DataMember]
        public int TotalRows { get; set; }

        [DataMember]
        public List<T> Entities { get; set; }
    }
}
