namespace DoTechSoftMVC.Data.Concrete.Entities
{
    public class Service : BaseEntity
    {
        public int ServiceId { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        
    }
}
