using SamsBowling.BL;
using SamsBowling.DL;
using SamsBowling.Models;
using SamsBowling.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsBowling
{
    class Program
    {
        static void Main(string[] args)
        {

            MockPlantRepository plantRepository = new MockPlantRepository();
            LogService logService = new LogService();

            Plant plant = new Plant(plantRepository, logService);

            Member member = new Member
            {
                FirstName="Samuel",
                LastName="Bering",
                Email="samuel@gmail.com",
                MemberNumber=1,
                PostCode="18363",
                StreetAddress="Rospiggsvägen 4",
                PostTown="Täby"
            };

            Member member2 = new Member
            {
                FirstName = "Julian",
                LastName = "Gagnestam",
                Email = "julian@gmail.com",
                MemberNumber = 2,
                PostCode = "12060",
                StreetAddress = "Järnlundsv 17",
                PostTown = "Årsta"
            };

            plant.RegisterMember(member);
            plant.RegisterMember(member2);


            Console.WriteLine(plant.GetMember(1)+"r\n");

            Console.WriteLine(plant.GetMember(2));

            Console.ReadKey();

        }
    }
}
