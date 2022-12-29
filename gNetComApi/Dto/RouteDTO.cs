namespace gNetComApi.Dto
{
    public class RouteDTO
    {
        public string? RouteId { get; set; } = null!;
        public string? UserOwner { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public float? LatitudeInit { get; set; }
        public float? LongitudeInit { get; set; }
        public float? LatitudeFin { get; set; }
        public float? LongitudeFin { get; set; }
    }
}
