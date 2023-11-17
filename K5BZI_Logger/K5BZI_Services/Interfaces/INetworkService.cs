namespace K5BZI_Services.Interfaces
{
    public interface INetworkService
    {
        void StartListener();

        void StartNetworkServer(string hostName = "RADIO-LAPTOP");
    }
}
