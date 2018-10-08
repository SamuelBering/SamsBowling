using SamsBowling.BL;
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

            laneService.Player1Sets = CreateSets(player1Set1, player1Set2, player1Set3);
            laneService.Player2Sets = CreateSets(player2Set1, player2Set2, player2Set3);


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


        [Fact]
        public void RegisterContest_WithMatches_AddsContestToStorage()
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

            var members = GetMembers();
            var matches = CreateMatches(members);
            contest.Matches = matches;

            var expectedContestTitle = contest.Title;
            var expectedNumberOfContests = 1;
            var expectedNumberOfMatches = 2;

            plant.RegisterContest(contest);

            Assert.StrictEqual(expectedNumberOfMatches, plantRepository.PlantStorage.Contests[contest.ContestNumber].Matches.Count);
            Assert.StrictEqual(expectedNumberOfContests, plantRepository.PlantStorage.Contests.Count);
            Assert.Equal(expectedContestTitle, plantRepository.PlantStorage.Contests[contest.ContestNumber].Title);
        }

        [Theory]
        [InlineData(0, 1, 2, 3, 0, 1)]
        [InlineData(0, 1, 2, 3, 1, 0)]
        public void RegisterContest_WithMatchesContainingSamePlayers_ThrowsException(int match1Player1, int match1Player2,
                                                                  int match2Player1, int match2Player2,
                                                                  int match3Player1, int match3Player2)
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

            var members = GetMembers();

            var matches = CreateMatches(members[match1Player1], members[match1Player2],
                                        members[match2Player1], members[match2Player2],
                                        members[match3Player1], members[match3Player2]);

            contest.Matches = matches;

            var ex = Assert.Throws<InvalidOperationException>(() => plant.RegisterContest(contest));
            Assert.Equal("Two matches can't contain the same players", ex.Message);
        }


        [Fact]
        public void GetContest_Recieves_Contest()
        {
            var dependencies = CreatePlantDependencies();
            var plantRepository = dependencies.PlantRepository as MockPlantRepository;

            var plant = new Plant(dependencies);

            var expectedContest = new Contest
            {
                ContestNumber = 1,
                Title = "Bengan cup",
                Description = "Bengans häftiga cup",
                StartDateTime = new DateTime(2019, 1, 1, 08, 15, 0),
                EndDateTime = new DateTime(2019, 06, 10, 23, 00, 0),
            };

            var members = GetMembers();
            var matches = CreateMatches(members);
            expectedContest.Matches = matches;


            plant.RegisterContest(expectedContest);

            var actualContest = plant.GetContest(expectedContest.ContestNumber);

            Assert.StrictEqual(expectedContest.Matches.Count, actualContest.Matches.Count);

            Assert.Equal(expectedContest.Title, actualContest.Title);
            Assert.Equal(expectedContest.Description, actualContest.Description);

        }





        [Theory]
        [InlineData(0, 1, 2, 3, 0, 3, 1, 2, "Samuel", "Linda")]
        public void GetChampionOfTheYear_WhenMoreThanOneChampion_ReturnsCorrectChampionResult(int match1Player1, int match1Player2,
                                                                  int match2Player1, int match2Player2,
                                                                  int match3Player1, int match3Player2,
                                                                  int match4Player1, int match4Player2,
                                                                  string expectedChampion1, string expectedChampion2)
        {
            var dependencies = CreatePlantDependencies();
            var laneService = dependencies.LaneService as MockLaneService;

            var plant = new Plant(dependencies);

            var members = GetMembers();



            var matches = CreateMatches(members[match1Player1], members[match1Player2],
                                        members[match2Player1], members[match2Player2],
                                        members[match3Player1], members[match3Player2],
                                        members[match4Player1], members[match4Player2]);

            var mockMatchResults = new List<MockMatchResult>
            {
                new MockMatchResult
                {
                    Player1Sets=CreateSets(50,50,100), //Samuel (0 / 1), Samuel (0)
                    Player2Sets=CreateSets(50,50,100) //Julian (0 / 1), Linda (0)
                },
                new MockMatchResult
                {
                    Player1Sets=CreateSets(50,50,100), //Linda (0 / 1), Julian (0)
                    Player2Sets=CreateSets(50,50,100) //Elliot (0 / 1), Elliot (0)
                },
                new MockMatchResult
                {
                    Player1Sets=CreateSets(50,50,100), //Samuel (1 / 2), Julian (1 av 2)
                    Player2Sets=CreateSets(50,50,50) //Elliot (0 / 2), Linda (0)
                },
                 new MockMatchResult
                {
                    Player1Sets=CreateSets(100,99,100), //Julian (0 / 2), Julian (0)
                    Player2Sets=CreateSets(100,100,100) //Linda (1 av 2), Elliot (1 av 2)
                }
            };

            RegisterMembers(plant, members);
            RegisterMatches(plant, matches);

            RunMatches(plant, laneService, matches, mockMatchResults);


            var expectedNumberOfChampions = 2;

            var actualChampionResults = plant.GetChampionsOfTheYear(2018);

            Assert.Equal(expectedNumberOfChampions, actualChampionResults.Count);

            Assert.True((expectedChampion1 == actualChampionResults[0].Member.FirstName
                        && expectedChampion2 == actualChampionResults[1].Member.FirstName)
                        ||
                        (expectedChampion2 == actualChampionResults[0].Member.FirstName
                        && expectedChampion1 == actualChampionResults[1].Member.FirstName));



        }

        [Theory]
        [InlineData(0, 2, 1, 3, 1, 2, 1, 3, "Elliot")]
        [InlineData(0, 1, 2, 3, 0, 3, 1, 3, "Samuel")]
        public void GetChampionOfTheYear_ReturnsCorrectChampionResult(int match1Player1, int match1Player2,
                                                                 int match2Player1, int match2Player2,
                                                                 int match3Player1, int match3Player2,
                                                                 int match4Player1, int match4Player2,
                                                                 string expectedChampion)
        {
            var dependencies = CreatePlantDependencies();
            var laneService = dependencies.LaneService as MockLaneService;

            var plant = new Plant(dependencies);

            var members = GetMembers();



            var matches = CreateMatches(members[match1Player1], members[match1Player2],
                                        members[match2Player1], members[match2Player2],
                                        members[match3Player1], members[match3Player2],
                                        members[match4Player1], members[match4Player2]);

            var mockMatchResults = new List<MockMatchResult>
            {
                new MockMatchResult
                {
                    Player1Sets=CreateSets(50,50,100), //Samuel (0), Samuel (0)
                    Player2Sets=CreateSets(50,50,100) //Julian (0), Linda (0)
                },
                new MockMatchResult
                {
                    Player1Sets=CreateSets(50,50,100), //Linda (0), Julian (0)
                    Player2Sets=CreateSets(50,50,100) //Elliot (0), Elliot (0)
                },
                new MockMatchResult
                {
                    Player1Sets=CreateSets(50,50,100), //Samuel (1 av 2), Julian (1 av 2)
                    Player2Sets=CreateSets(50,50,50) //Elliot (0), Linda (0)
                },
                 new MockMatchResult
                {
                    Player1Sets=CreateSets(100,99,100), //Julian (0), Julian (0)
                    Player2Sets=CreateSets(100,100,100) //Elliot (0 av 3), Elliot (1 av 2)
                }
            };

            RegisterMembers(plant, members);
            RegisterMatches(plant, matches);

            RunMatches(plant, laneService, matches, mockMatchResults);

            var expectedChampionOfTheYear = expectedChampion;
            var expectedNumberOfChampions = 1;

            var actualChampionResults = plant.GetChampionsOfTheYear(2018);

            Assert.Equal(expectedNumberOfChampions, actualChampionResults.Count);

            Assert.Equal(expectedChampionOfTheYear, actualChampionResults[0].Member.FirstName);

        }


        [Theory]
        [InlineData(0, 2, 1, 3, 1, 2, 1, 0, "Samuel")]
        [InlineData(0, 1, 2, 3, 0, 3, 1, 3, "Samuel")]
        public void RunContest_ReturnsCorrectContestResult(int match1Player1, int match1Player2,
                                                                 int match2Player1, int match2Player2,
                                                                 int match3Player1, int match3Player2,
                                                                 int match4Player1, int match4Player2,
                                                                 string expectedChampion)
        {
            var dependencies = CreatePlantDependencies();
            var laneService = dependencies.LaneService as MockLaneService;

            var plant = new Plant(dependencies);

            var members = GetMembers();



            var matches = CreateMatches(members[match1Player1], members[match1Player2],
                                        members[match2Player1], members[match2Player2],
                                        members[match3Player1], members[match3Player2],
                                        members[match4Player1], members[match4Player2]);

            var mockMatches = new List<Match>
            {
                new Match
                {
                    Player1Sets=CreateSets(50,50,100), //Samuel (0), Samuel (0)
                    Player2Sets=CreateSets(50,50,100) //Julian (0), Linda (0)
                },
                new Match
                {
                    Player1Sets=CreateSets(50,50,100), //Linda (0), Julian (0)
                    Player2Sets=CreateSets(50,50,100) //Elliot (0), Elliot (0)
                },
                new Match
                {
                    Player1Sets=CreateSets(50,50,100), //Samuel (1 av 2), Julian (1 av 2)
                    Player2Sets=CreateSets(50,50,50) //Elliot (0), Linda (0)
                },
                 new Match
                {
                    Player1Sets=CreateSets(100,99,100), //Julian (0), Julian (1 av 3)
                    Player2Sets=CreateSets(100,100,100) //Elliot (0 av 3), Samuel (1 av 2)
                }
            };

            laneService.MockMatches = mockMatches;

            RegisterMembers(plant, members);
            RegisterMatches(plant, matches);

            var contest = new Contest
            {
                ContestNumber = 1,
                Title = "Bengan cup",
                Description = "Bengans häftiga cup",
                StartDateTime = new DateTime(2019, 1, 1, 08, 15, 0),
                EndDateTime = new DateTime(2019, 06, 10, 23, 00, 0),
            };
            contest.Matches = matches;
            plant.RegisterContest(contest);


            var contestResult = plant.RunContest(contest);

            var expectedNumberOfChampions = 1;

            var actualChampionResults = contestResult.ChampionResults;

            Assert.Equal(expectedNumberOfChampions, actualChampionResults.Count);

            Assert.Equal(expectedChampion, actualChampionResults[0].Member.FirstName);

        }


        class MockMatchResult
        {
            public Set[] Player1Sets { get; set; }
            public Set[] Player2Sets { get; set; }
        }

        private void RunMatches(Plant plant, MockLaneService laneService, List<Match> matches, List<MockMatchResult> matchResults)
        {
            for (var i = 0; i < matches.Count; i++)
            {
                laneService.Player1Sets = matchResults[i].Player1Sets;
                laneService.Player2Sets = matchResults[i].Player2Sets;
                plant.RunMatch(matches[i]);
            }
        }

        private void RegisterMembers(Plant plant, List<Member> members)
        {
            foreach (var member in members)
                plant.RegisterMember(member);
        }

        private void RegisterMatches(Plant plant, List<Match> matches)
        {
            foreach (var match in matches)
                plant.RegisterMatch(match);
        }

        private Set[] CreateSets(int set1Points, int set2Points, int set3Points)
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
                LaneService = new MockLaneService(10, new MostPointsWinsStrategy()),
                CalculateChampionStrategy = new WonHighestProportionOfMatches()
            };

            var repository = plantDependencies.PlantRepository as MockPlantRepository;

            repository.PlantStorage.Members.Clear();
            repository.PlantStorage.Matches.Clear();
            repository.PlantStorage.Contests.Clear();

            return plantDependencies;
        }


        private List<Match> CreateMatches(List<Member> members)
        {
            if (members.Count < 2 || members.Count % 2 == 1)
                throw new InvalidOperationException("List must be minimum 2 members and even couples");

            var matches = new List<Match>();

            for (var i = 0; i < members.Count; i += 2)
            {
                matches.Add(new Match
                {
                    Player1 = new Player(members[i]),
                    Player2 = new Player(members[i + 1]),
                    MatchNumber = i / 2
                });
            }

            return matches;
        }


        private List<Match> CreateMatches(params Member[] members)
        {
            if (members.Length < 2 || members.Length % 2 == 1)
                throw new InvalidOperationException("Array must be minimum 2 members and even couples");

            var matches = new List<Match>();

            for (var i = 0; i < members.Length; i += 2)
            {
                matches.Add(new Match
                {
                    Player1 = new Player(members[i]),
                    Player2 = new Player(members[i + 1]),
                    MatchNumber = i / 2
                });
            }

            return matches;
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
                },
                 new Member
                {
                FirstName = "Linda",
                LastName = "Gagnestam",
                Email = "linda@gmail.com",
                MemberNumber = 3,
                PostCode = "12060",
                StreetAddress = "Järnlundsv 17",
                PostTown = "Årsta"
                },
                  new Member
                {
                FirstName = "Elliot",
                LastName = "Gagnestam",
                Email = "elliot@gmail.com",
                MemberNumber = 4,
                PostCode = "12060",
                StreetAddress = "Järnlundsv 17",
                PostTown = "Årsta"
                }
             };

            return members;
        }

    }
}