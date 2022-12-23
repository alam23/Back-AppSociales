using System;
using System.Collections.Generic;

namespace gNetComApi.Models
{
    public partial class Post
    {
        public Post()
        {
            Commentaries = new HashSet<Commentary>();
        }

        public string PostId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? RouteId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Image { get; set; }

        public virtual Route? Route { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Commentary> Commentaries { get; set; }
    }
}
