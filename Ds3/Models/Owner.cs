namespace Ds3.Models
{
    public class Owner
    {
        public string Id { get; private set; }
        public string DisplayName { get; private set; }

        public Owner(string id, string displayName)
        {
            this.Id = id;
            this.DisplayName = displayName;
        }

        public override string ToString()
        {
            return Id + ":" + DisplayName;
        }
    }
}
