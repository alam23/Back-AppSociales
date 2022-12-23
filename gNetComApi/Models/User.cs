using System;
using System.Collections.Generic;

namespace gNetComApi.Models
{
    public partial class User
    {
        public User()
        {
            Commentaries = new HashSet<Commentary>();
            FriendFromUsers = new HashSet<Friend>();
            FriendToUsers = new HashSet<Friend>();
            Posts = new HashSet<Post>();
            Routes = new HashSet<Route>();
            UserRoutes = new HashSet<UserRoute>();
        }

        public string UserId { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? CellNumber { get; set; }

        public virtual ICollection<Commentary> Commentaries { get; set; }
        public virtual ICollection<Friend> FriendFromUsers { get; set; }
        public virtual ICollection<Friend> FriendToUsers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<UserRoute> UserRoutes { get; set; }
    }
}
