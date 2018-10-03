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
            var dependencies = CreateMainDependencies();

            var members = GetMembers();
            var expectedMembers = 1;
            var expectedMemberFirstName = "Samuel";

            dependencies.Plant.RegisterMember(members[0]);

            Assert.Equal(expectedMembers, dependencies.Repository.PlantStorage.Members.Count);
            Assert.Equal(expectedMemberFirstName, dependencies.Repository.PlantStorage.Members[members[0].MemberNumber].FirstName);
        }


        [Fact]
        public void GetMember_RecievesMember()
        {
            var dependencies = CreateMainDependencies();
            var expectedMemberFirstName = "Samuel";
            var expectedEmail = "samuel@gmail.com";
            var member = dependencies.Plant.GetMember(1);

            Assert.Equal(expectedMemberFirstName, member.FirstName);
            Assert.Equal(expectedEmail, member.Email);

        }


        [Fact]
        public void RegisterMatch_AddsMatchToStorage()
        {
            var dependencies = CreateMainDependencies();
            var members = GetMembers();

            dependencies.Plant.RegisterMember(members[1]);

            var member1 = dependencies.Plant.GetMember(1);
            var member2 = dependencies.Plant.GetMember(2);

            var expectedMatches = 1;
            var expectedPlayer1 = "Samuel";
            var expectedPlayer2 = "Julian";

            var match = new Match
            {
                MatchNumber = 1,
                Player1 = new Player(member1),
                Player2 = new Player(member2),
            };

            dependencies.Plant.RegisterMatch(match);


            Assert.Equal(expectedMatches, dependencies.Repository.PlantStorage.Matches.Count);
            Assert.Equal(expectedPlayer1, dependencies.Repository.PlantStorage.Matches[1].Player1.FirstName);
            Assert.Equal(expectedPlayer2, dependencies.Repository.PlantStorage.Matches[1].Player2.FirstName);
        }

        class MainDependencies
        {
            public MockPlantRepository Repository { get; set; }
            public ILogService LogService { get; set; }
            public IPlant Plant { get; set; }
        }

        private MainDependencies CreateMainDependencies()
        {
            MainDependencies mainDependencies = new MainDependencies
            {
                Repository = new MockPlantRepository(),
                LogService = new FileLogService(),
            };
            mainDependencies.Plant = new Plant(mainDependencies.Repository, mainDependencies.LogService);
            return mainDependencies;
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

    }
}