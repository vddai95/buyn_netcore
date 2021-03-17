using System.Runtime.Serialization;

namespace byin_netcore_transver.Exception
{
    [DataContract(Name = "error")]
    public class ErrorModel
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "detail")]
        public string Detail { get; set; }
    }
}
