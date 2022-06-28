using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        //public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto() {
                    Id = 1,
                    Name = "Zagreb",
                    Description = "Beli zagreb grad",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id = 1, Name = "Špansko trg", Description = "Mjesto gdje ima svacega" },
                        new PointOfInterestDto() { Id = 2, Name = "Jarun", Description = "Mjesto za rolanje i opustanje" }
                    }
                },
                new CityDto() {
                    Id = 2,
                    Name = "Vir",
                    Description = "Suncani otok",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id = 3, Name = "Torovi", Description = "Mjesto za odmor" },
                        new PointOfInterestDto() { Id = 4, Name = "Lepinja", Description = "Jako finooo" }
                    }
                },
                new CityDto() {
                    Id = 3,
                    Name = "Slavonski Brod",
                    Description = "Grad na Savi",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto() { Id = 5, Name = "Vinogorje", Description = "Mjesto gdje ima zelenila" },
                        new PointOfInterestDto() { Id = 6, Name = "Korzo", Description = "Mjesto za rolanje i opustanje" }
                    }
                }
            };
        }
    }
}
