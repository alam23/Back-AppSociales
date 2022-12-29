namespace gNetComApi.Dto
{
    public class newCommentDto
    {
        //public string CommentId { get; set; } = null!;
        public string? PostId { get; set; }
        public string? UserId { get; set; }
        public string? Body { get; set; }
    }
}
