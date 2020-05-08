namespace MyRentals.Domain.StorageModels
{
    public class Realtor
    {
        public Realtor(ulong realtorId, string realtorName, string realtorEmail)
        {
            this.RealtorId = realtorId;
            this.RealtorName = realtorName;
            this.RealtorEmail = realtorEmail;
        }

        public ulong RealtorId { get; private set; }

        public string RealtorName { get; }

        public string RealtorEmail { get; private set; }

        public void SetId(ulong id)
        {
            this.RealtorId = id;
        }
    }
}