using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Xml.Serialization;

using Ds3.AwsModels;

namespace Ds3.Models
{
    public class GetBucketResponse : Ds3Response
    {
        private string _bucketName;

        public string BucketName
        {
            get { return _bucketName; }
        }

        private string _prefix;

        public string Prefix
        {
            get { return _prefix; }
        }

        private string _marker;

        public string Marker
        {
            get { return _marker; }
        }

        private int _maxKeys;

        public int MaxKeys
        {
            get { return _maxKeys; }
        }

        private bool _isTruncated;

        public bool IsTruncated
        {
            get { return _isTruncated; }
        }

        public GetBucketResponse(HttpWebResponse responseStream)
            : base(responseStream)
        {
            processResponse();
        }

        private void processResponse()
        {
            using (Stream content = response.GetResponseStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ListBucketResult));
                ListBucketResult results = (ListBucketResult)serializer.Deserialize(content);

                _bucketName = results.Name;
                _prefix = results.Prefix;
                _marker = results.Marker;
                _maxKeys = int.Parse(results.MaxKeys);
                _isTruncated = bool.Parse(results.IsTruncated);
            }
        }
    }
}
