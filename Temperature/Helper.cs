using Newtonsoft.Json;
using Temperature.Model;

namespace Temperature
{
    public class Helper
    {
        private static string filePathCities = "Data/cities.json";
        private static string filePathMeasurements = "Data/measurements.txt";
        private static string timeHolder = "Data/timeChanged.txt";

        public static void ParseCsv()
        {         
            var cities = File.ReadLines(filePathMeasurements)
                         .Select(ParseLine) 
                         .GroupBy(c => c.Name) 
                         .Select(g => new City
                         {
                             Name = g.Key,
                             Temperatures = g.Select(c => c.Temperatures.First()).ToList()
                         })
                         .ToList();
            foreach ( var city in cities)
            {
                city.AvgTemp = city.Temperatures.Average();
                city.MinTemp = city.Temperatures.Min();
                city.MaxTemp = city.Temperatures.Max();
            }

            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            string json = JsonConvert.SerializeObject(cities, Formatting.Indented);
            string jsonFilePath = Path.Combine(directoryPath, "cities.json");


            File.WriteAllText(jsonFilePath, json);
            DateTime content = File.GetLastWriteTime(filePathMeasurements);
            File.WriteAllText(timeHolder, content.ToString());

        }

        internal static bool CheckIfChanged()
        {
            if (!File.Exists(timeHolder) || !File.ReadLines(timeHolder).Any())
            {
                return true;                
            }
            else
            {
                DateTime time = DateTime.Parse(File.ReadLines(timeHolder).First());
                DateTime lastModified = File.GetLastWriteTime(filePathMeasurements);
                if (lastModified > time)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }

        internal static List<CityViewModel> CitiesOutput()
        {
            string json = File.ReadAllText(filePathCities);

            var cities = JsonConvert.DeserializeObject<List<CityViewModel>>(json);

            return cities;
        }

        internal static CityViewModel CityOutput(string city)
        {
            string json = File.ReadAllText(filePathCities);

            var cities = JsonConvert.DeserializeObject<List<CityViewModel>>(json);

            return cities.FirstOrDefault(c => c.Name.Equals(city, StringComparison.OrdinalIgnoreCase));
        }

        internal static List<CityViewModel> FilterCitiesGreaterThan(double temp)
        {
            string json = File.ReadAllText(filePathCities);

            var cities = JsonConvert.DeserializeObject<List<CityViewModel>>(json) ?? new List<CityViewModel>();

            return cities.Where(c => c.AvgTemp > temp).ToList();
        }

        internal static List<CityViewModel> FilterCitiesSmallerThan(double temp)
        {
            string json = File.ReadAllText(filePathCities);

            var cities = JsonConvert.DeserializeObject<List<CityViewModel>>(json) ?? new List<CityViewModel>();
          
            return cities.Where(c => c.AvgTemp < temp).ToList();
        }

        private static City ParseLine(string line)
        {
            var parts = line.Split(';');
            return new City
            {
                Name = parts[0].Trim(),
                Temperatures = new List<double> { double.Parse(parts[1].Trim()) }
            };
        }
        
    }
}
