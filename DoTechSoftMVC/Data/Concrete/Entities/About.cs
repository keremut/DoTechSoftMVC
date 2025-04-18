namespace DoTechSoftMVC.Data.Concrete.Entities
{
    public class About
    {
        public int AboutId { get; set; }
        public string Title1 { get; set; } = string.Empty;
        public string Title2 { get; set; } = string.Empty;
        public string Desc1 { get; set; } = string.Empty;
        public string Desc2 { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
