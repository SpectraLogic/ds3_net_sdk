namespace Ds3
{
    public class Credentials
    {

        public Credentials(string accessId, string key)
        {
            _accessId = accessId;
            _key = key;
        }

        private string _key;
        private string _accessId;

        public string AccessId
        {
            get { return _accessId; }
        }

        public string Key
        {
            get { return _key; }
        }
        
    }
}
