namespace Temperature.Model
{
    public class City
    {
        public string Name { get; set; }
        public List<double> Temperatures { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public double AvgTemp { get; set; }
    }
}
