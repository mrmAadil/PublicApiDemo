namespace PublicApiDemo.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Capital { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Subregion { get; set; } = string.Empty;
        public long Population { get; set; }
        public double Area { get; set; }
        public string CountryCode { get; set; } = string.Empty;

    }
}
