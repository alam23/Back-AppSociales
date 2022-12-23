using System;
using System.Collections.Generic;

namespace gNetComApi.Models
{
    public partial class UserRoute
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? RouteId { get; set; }

        public virtual Route? Route { get; set; }
        public virtual User? User { get; set; }
    }
}
