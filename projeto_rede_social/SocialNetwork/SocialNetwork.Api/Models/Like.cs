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
    using System;
    using System.Collections.Generic;
    
    public partial class Like
    {
        public int post_id { get; set; }
        public int liker_uid { get; set; }
        public System.DateTime timestamp { get; set; }
    }
}
