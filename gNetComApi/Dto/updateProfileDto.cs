namespace gNetComApi.Dto
{
    public class updateProfileDto
    {
        public string UserId { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? CellNumber { get; set; }
    }
}
