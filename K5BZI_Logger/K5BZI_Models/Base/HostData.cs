using System.Net;

namespace K5BZI_Models.Base
{
    public class HostData : IPAddress
    {
        public HostData(string operatorName,
            byte[] address) : base(address)
        {
            OperatorName = operatorName;
        }

        public string OperatorName { get; set; }
    }
}
