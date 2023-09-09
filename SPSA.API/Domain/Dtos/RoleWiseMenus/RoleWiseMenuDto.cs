namespace SPSA.API.Domain.Dtos.RoleWiseMenus
{
    public class RoleWiseMenuDto
    {
        public long RoleId { get; set; }
        public long MenuId { get; set; }
        public long? ParentMenuId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public List<RoleWiseMenuDto> Childs { get; set; }   
    }
}
