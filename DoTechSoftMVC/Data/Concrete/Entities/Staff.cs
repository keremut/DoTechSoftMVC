namespace DoTechSoftMVC.Data.Concrete.Entities
{
    public class Staff : BaseEntity
    {
        public int StaffId { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string Position { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string? LinkedIn { get; set; } 
        public string? Instagram { get; set; } 
        public string? YouTube { get; set; } 
    }
}
