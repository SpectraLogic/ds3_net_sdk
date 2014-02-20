namespace Ds3.AwsModels
{
    public class Owner
    {
        private string _id;
        private string _displayName;
        public string Id
        {
            get { return _id; }
        }
        public string DisplayName
        {
            get { return _displayName; }
        }

        public Owner(string id, string displayName)
        {
            this._id = id;
            this._displayName = displayName;
        }

        public override string ToString()
        {
            return Id + ":" + DisplayName;
        }
    }
}
