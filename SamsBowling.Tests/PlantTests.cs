﻿using SamsBowling.BL;
using SamsBowling.DL;
using SamsBowling.Models;
using SamsBowling.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace MyFirstUnitTests
{
    public class PlantTests
    {


        [Fact]
        public void RegisterMember_AddsMemberToStorage()
        {
            var dependencies = CreatePlantDependencies();
            var plant = new Plant(dependencies);

            var members = GetMembers();
            var expectedMembers = 1;
            var expectedMemberFirstName = "Samuel";

            plant.RegisterMember(members[0]);

            var plantRepository = dependencies.PlantRepository as MockPlantRepository;

            Assert.Equal(expectedMembers, plantRepository.PlantStorage.Members.Count);
            Assert.Equal(expectedMemberFirstName, plantRepository.PlantStorage.Members[members[0].MemberNumber].FirstName);
        }



        [Fact]
        public void GetMember_RecievesMember()
        {
            var dependencies = CreatePlantDependencies();
            var plant = new Plant(dependencies);

            var members = GetMembers();
            plant.RegisterMember(members[0]);

            var expectedMemberFirstName = "Samuel";
            var expectedEmail = "samuel@gmail.com";
            var member = plant.GetMember(1);

            Assert.Equal(expectedMemberFirstName, member.FirstName);
            Assert.Equal(expectedEmail, member.Email);

        }


        [Fact]
        public void RegisterMatch_AddsMatchToStorage()
        {
            var dependencies = CreatePlantDependencies();
            var plantRepository = dependencies.PlantRepository as MockPlantRepository;

            var plant = new Plant(dependencies);

            var members = GetMembers();
            plant.RegisterMember(members[0]);
            plant.RegisterMember(members[1]);

            var member1 = members[0];
            var member2 = members[1];

            var expectedMatches = 1;
            var expectedPlayer1 = "Samuel";
            var expectedPlayer2 = "Julian";

            var match = new Match
            {
                MatchNumber = 1,
                Player1 = new Player(member1),
                Player2 = new Player(member2),
            };

            plant.RegisterMatch(match);


            Assert.Equal(expectedMatches, plantRepository.PlantStorage.Matches.Count);
            Assert.Equal(expectedPlayer1, plantRepository.PlantStorage.Matches[1].Player1.FirstName);
            Assert.Equal(expectedPlayer2, plantRepository.PlantStorage.Matches[1].Player2.FirstName);
        }


        [Theory]
        [InlineData(50, 100, 50, 50, 50, 99, "Samuel")]
        [InlineData(50, 100, 50, 100, 100, 100, "Julian")]
        [InlineData(50, 100, 50, 100, 100, 0, null)]
        public void RunMatch_ReturnsMatchResultWithCorrectWinner(int player1Set1, int player1Set2, int player1Set3,
                                                                 int player2Set1, int player2Set2, int player2Set3,
                                                                 string expectedWinner)
        {
            var dependencies = CreatePlantDependencies();
            var laneService = dependencies.LaneService as MockLaneService;


            var plant = new Plant(dependencies);

            var members = GetMembers();
            plant.RegisterMember(members[0]);
            plant.RegisterMember(members[1]);


            var match = new Match
            {
                MatchNumber = 1,
                Player1 = new Player(members[0]),
                Player2 = new Player(members[1]),
            };

            plant.RegisterMatch(match);

            laneService.Player1Sets = createSets(player1Set1, player1Set2, player1Set3);
            laneService.Player2Sets = createSets(player2Set1, player2Set2, player2Set3);


            var matchResult = plant.RunMatch(match);

            Assert.Equal(expectedWinner, matchResult.Winner?.FirstName);

        }

        [Fact]
        public void RegisterContest_WithNoMatches_AddsContestToStorage()
        {
            var dependencies = CreatePlantDependencies();
            var plantRepository = dependencies.PlantRepository as MockPlantRepository;

            var plant = new Plant(dependencies);

            var contest = new Contest
            {
                ContestNumber = 1,
                Title = "Bengan cup",
                Description = "Bengans häftiga cup",
                StartDateTime = new DateTime(2019, 1, 1, 08, 15, 0),
                EndDateTime = new DateTime(2019, 06, 10, 23, 00, 0),
            };

            var expectedContestTitle = contest.Title;
            var expectedNumberOfContests = 1;

            plant.RegisterContest(contest);

            Assert.StrictEqual(expectedNumberOfContests, plantRepository.PlantStorage.Contests.Count);
            Assert.Equal(expectedContestTitle, plantRepository.PlantStorage.Contests[contest.ContestNumber].Title);
        }


        private Set[] createSets(int set1Points, int set2Points, int set3Points)
        {
            var sets = new Set[3]
             {
                new Set
                {
                    Points=set1Points
                },
               new Set
                {
                    Points=set2Points
                },
               new Set
                {
                    Points=set3Points
                },
              };

            return sets;
        }

        class MainDependencies
        {
            public MockPlantRepository Repository { get; set; }
            public ILogService LogService { get; set; }
            public IPlant Plant { get; set; }
        }

        private PlantDependencies CreatePlantDependencies()
        {
            var plantDependencies = new PlantDependencies
            {
                PlantRepository = new MockPlantRepository(),
                LogService = new FileLogService(),
                ExportMemberService = new ExportMemberService(),
                LaneService = new MockLaneService(10, new MostPointsWinsStrategy())
            };

            var repository = plantDependencies.PlantRepository as MockPlantRepository;

            repository.PlantStorage.Members.Clear();
            repository.PlantStorage.Matches.Clear();
            repository.PlantStorage.Contests.Clear();

            return plantDependencies;
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