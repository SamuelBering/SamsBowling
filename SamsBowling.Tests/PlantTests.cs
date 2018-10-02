using SamsBowling.BL;
using SamsBowling.DL;
using SamsBowling.Models;
using SamsBowling.Services;
using System.Collections.Generic;
using Xunit;

namespace MyFirstUnitTests
{
    public class PlantTests
    {
        [Fact]
        public void RegisterMember_AddsMemberToStorage()
        {
            MockPlantRepository repository = new MockPlantRepository();
            LogService logService = new LogService();

           

            Plant plant = new Plant(repository, logService);

            var members = GetMembers();
            var expectedMembers = 1;
            var expectedMemberFirstName = "Samuel";

            plant.RegisterMember(members[0]);


            Assert.Equal(expectedMembers, repository.PlantStorage.Members.Count);
            Assert.Equal(expectedMemberFirstName, repository.PlantStorage.Members[members[0].MemberNumber].FirstName);


        }

        private List<Member> GetMembers()
        {
            List<Member> members = new List<Member>
            {
                new Member
                {
                FirstName = "Samuel",
                LastName = "Bering",
                Email = "samuel@gmail.com",
                MemberNumber = 1,
                PostCode = "18363",
                StreetAddress = "Rospiggsvägen 4",
                PostTown = "Täby"
                },
                new Member
                {
                FirstName = "Julian",
                LastName = "Gagnestam",
                Email = "julian@gmail.com",
                MemberNumber = 2,
                PostCode = "12060",
                StreetAddress = "Järnlundsv 17",
                PostTown = "Årsta"
                }
             };

            return members;
        }

        //[Fact]
        //public void FailingTest()
        //{
        //    Assert.Equal(5, Add(2, 2));
        //}

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}