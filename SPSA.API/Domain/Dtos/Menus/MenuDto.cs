namespace SPSA.API.Domain.Dtos.Menus
{
    public class MenuDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public long? MenuOrder { get; set; }
        public bool IsLeaf { get; set; }
        public string? Action { get; set; }
        public IEnumerable<MenuDto>? Children { get; set; }
    }
}
