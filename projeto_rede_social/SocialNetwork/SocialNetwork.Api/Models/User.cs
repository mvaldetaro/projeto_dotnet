//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocialNetwork.Api.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]

    public partial class User
    {
        [DataMember]
        public string uid { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string nickname { get; set; }
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Nullable<System.DateTime> brithday { get; set; }
        [DataMember]
        public string bio { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public string site { get; set; }
        [DataMember]
        public string photo_url { get; set; }
    }
}
