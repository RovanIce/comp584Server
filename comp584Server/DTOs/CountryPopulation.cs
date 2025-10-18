namespace comp584Server.DTOs
{
    public class CountryPopulation
    {
        public required int id { get; set; }
        public required string name { get; set; }
        public required string iso2 { get; set; }
        public required string iso3 { get; set; }
        public decimal population { get; set; }
    }
}
