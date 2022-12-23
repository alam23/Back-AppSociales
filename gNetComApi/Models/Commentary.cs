using System;
using System.Collections.Generic;

namespace gNetComApi.Models
{
    public partial class Commentary
    {
        public string CommentId { get; set; } = null!;
        public string? PostId { get; set; }
        public string? UserId { get; set; }
        public string? Body { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
