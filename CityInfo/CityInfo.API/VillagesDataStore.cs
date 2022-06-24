using CityInfo.API.Models;

namespace CityInfo.API
{
    public class VillagesDataStore
    {
        public List<VillageDto> Villages = new List<VillageDto>();

        public static VillagesDataStore CurrentVillages { get;} = new VillagesDataStore();

        public VillagesDataStore()
        {
            Villages = new List<VillageDto>() {
                new VillageDto()
                {
                    Id = 1,
                    Name = "Đurđevdan",
                    Description = "Zeleni"
                },
                new VillageDto()
                {
                    Id = 2,
                    Name = "Ponkovci",
                    Description = "Sve je tamo"
                }
            };
        }
    }
}
