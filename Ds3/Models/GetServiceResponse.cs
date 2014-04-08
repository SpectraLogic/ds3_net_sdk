using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

using Ds3.AwsModels;
using Ds3.Runtime;

namespace Ds3.Models
{
    public class GetServiceResponse : Ds3Response
    {
        private Owner _owner = null;
        public Owner Owner
        {
            get { return _owner; }
        }

        private List<Bucket> _buckets;

        public List<Bucket> Buckets
        {
            get { return _buckets; }
        }

        internal GetServiceResponse(IWebResponse responseStream)
            : base(responseStream)
        {
            HandleStatusCode(HttpStatusCode.OK);
            this._buckets = new List<Bucket>();            
            ProcessReponse();
        }

        private void ProcessReponse()
        {
            using (Stream content = response.GetResponseStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ListAllMyBucketsResult));
                ListAllMyBucketsResult results = (ListAllMyBucketsResult)serializer.Deserialize(content);

                foreach(object obj in results.Items) {
                    if (obj.GetType().Equals(typeof(ListAllMyBucketsResultOwner)))
                    {
                        ListAllMyBucketsResultOwner owner = (ListAllMyBucketsResultOwner)obj;
                        this._owner = new Owner(owner.ID, owner.DisplayName);
                    }
                    else if (obj.GetType().Equals(typeof(ListAllMyBucketsResultBuckets)))
                    {
                        ConvertBuckets((ListAllMyBucketsResultBuckets)obj);
                    }
                    else {                        
                        //TODO need to figure out what exception to throw here.
                        Console.WriteLine("Unknown element");
                    }
                }                    
            }
        }

        private void ConvertBuckets(ListAllMyBucketsResultBuckets buckets)
        {
            if (buckets.Bucket == null)
            {
                return;
            }
            foreach (ListAllMyBucketsResultBucketsBucket bucket in buckets.Bucket)
            {
                this._buckets.Add(new Bucket(bucket.Name, bucket.CreationDate));       
            }
        }
    }

    public class Bucket
    {
        private string _name;
        private string _creationDate;

        public string Name
        {
            get { return _name; }
        }

        public string CreationDate
        {
            get { return _creationDate; }
        }

        public Bucket(string name, string creationDate)
        {
            this._name = name;
            this._creationDate = creationDate;
        }
    }

}
