using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

using System.Net;
using System.IO;

using Ds3.AwsModels;
using System.Xml.Serialization;

namespace Ds3.Models
{
    public class GetServiceResponse : Ds3Response
    {
        private Owner _owner;
        public Owner Owner
        {
            get { return _owner; }
        }

        public GetServiceResponse(HttpWebResponse responseStream)
            : base(responseStream)
        {
            Console.WriteLine("Got content.");
            processReponse();
        }

        internal void processReponse()
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
                        Console.WriteLine("I have buckets!");
                    }
                    else {
                        Console.WriteLine("Unknown element");
                    }
                }
                
                Console.WriteLine(results.Items[1]);
            }                                              
        }            
    }

    public class Owner
    {
        private string _id;
        private string _displayName;
        public string Id
        {
            get {return _id;}
        }
        public string DisplayName
        {
            get {return _displayName;}
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
