namespace MyRentals.Domain.StorageModels
{
    public class Client
    {
        public Client(ulong clientId, string clientName)
        {
            this.ClientId = clientId;
            this.ClientName = clientName;
        }

        public ulong ClientId { get; private set; }
        public string ClientName { get; }

        public void SetId(ulong id)
        {
            this.ClientId = id;
        }
    }
}