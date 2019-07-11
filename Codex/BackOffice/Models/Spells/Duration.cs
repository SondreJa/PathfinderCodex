namespace BackOffice.Models.Spells
{
    public class Duration
    {
        public int? Interval { get; set; }
        public DurationType? DurationType { get; set; }
        public bool? IsDismissable { get; set; }
        public bool? IsConcentration { get; set; }
    }
}
