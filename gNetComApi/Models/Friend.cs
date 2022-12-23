using System;
using System.Collections.Generic;

namespace gNetComApi.Models
{
    public partial class Friend
    {
        public string FriendId { get; set; } = null!;
        public string? FromUserId { get; set; }
        public string? ToUserId { get; set; }
        public bool? IsReqAccepted { get; set; }

        public virtual User? FromUser { get; set; }
        public virtual User? ToUser { get; set; }
    }
}
