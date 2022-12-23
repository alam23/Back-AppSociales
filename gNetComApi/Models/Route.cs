using System;
using System.Collections.Generic;

namespace gNetComApi.Models
{
    public partial class Route
    {
        public Route()
        {
            Posts = new HashSet<Post>();
            UserRoutes = new HashSet<UserRoute>();
        }

        public string RouteId { get; set; } = null!;
        public string? UserOwner { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public float? LatitudeInit { get; set; }
        public float? LongitudeInit { get; set; }
        public float? LatitudeFin { get; set; }
        public float? LongitudeFin { get; set; }

        public virtual User? UserOwnerNavigation { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserRoute> UserRoutes { get; set; }
    }
}
