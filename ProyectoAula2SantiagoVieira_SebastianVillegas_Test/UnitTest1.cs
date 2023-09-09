namespace ProyectoAula2SantiagoVieira_SebastianVillegas_Test
{
    [TestFixture]
    public class BusinessIdeaTests
    {
        private StringWriter consoleOutput;
        private TextWriter originalOutput;

        [SetUp]
        public void Setup()
        {
            // Redirect console output for testing
            consoleOutput = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(consoleOutput);
        }

        [TearDown]
        public void Cleanup()
        {
            // Restore original console output
            Console.SetOut(originalOutput);
            consoleOutput.Dispose();
        }

        [Test]
        public void EnterBusinessIdea_ShouldAddIdeaToBusinessIdeas()
        {
            // ... (Existing test for EnterBusinessIdea)

            // Assert (Continuing from previous test)
            var addedIdea = program.businessIdeas.FirstOrDefault();
            Assert.IsNotNull(addedIdea);
            Assert.AreEqual(expectedCode, addedIdea.Code);
            Assert.AreEqual(expectedName, addedIdea.Name);
            Assert.AreEqual(expectedImpact, addedIdea.SocialEconomicImpact);
            CollectionAssert.AreEquivalent(expectedDepartments, addedIdea.BeneficiaryDepartments.Select(d => d.Name));
            Assert.AreEqual(expectedInvestment, addedIdea.InvestmentValue);
            Assert.AreEqual(expectedIncome, addedIdea.First3YearsIncome);
            CollectionAssert.AreEquivalent(expectedTools, addedIdea.Industry4Tools);

            // Check if the success message is printed
            Assert.IsTrue(consoleOutput.ToString().Contains($"Business idea registered successfully. Idea code: {expectedCode}"));
        }

        [Test]
        public void AddTeamMember_ShouldAddMemberToIdea()
        {
            // Arrange
            var program = new Program();
            program.businessIdeas.Clear();
            var idea = new BusinessIdeaTests(1);
            program.businessIdeas.Add(idea);
            var expectedIdentification = "ID123";
            var expectedName = "John";
            var expectedSurname = "Doe";
            var expectedRole = "Developer";
            var expectedEmail = "john@example.com";

            // Act
            using (var sr = new StringReader(
                $"1\n{expectedIdentification}\n{expectedName}\n{expectedSurname}\n{expectedRole}\n{expectedEmail}\n"))
            {
                Console.SetIn(sr);
                program.AddTeamMember();
            }

            // Assert
            var addedMember = idea.TeamMembers.FirstOrDefault();
            Assert.IsNotNull(addedMember);
            Assert.AreEqual(expectedIdentification, addedMember.Identification);
            Assert.AreEqual(expectedName, addedMember.Name);
            Assert.AreEqual(expectedSurname, addedMember.Surname);
            Assert.AreEqual(expectedRole, addedMember.Role);
            Assert.AreEqual(expectedEmail, addedMember.Email);

            // Check if the success message is printed
            Assert.IsTrue(consoleOutput.ToString().Contains("Team member added successfully."));
        }

        [Test]
        public void RemoveTeamMember_ShouldRemoveMemberFromIdea()
        {
            // Arrange
            var program = new Program();
            program.businessIdeas.Clear();
            var idea = new BusinessIdeaTests(1);
            var memberToRemove = new TeamMember
            {
                Identification = "ID123",
                Name = "John",
                Surname = "Doe",
                Role = "Developer",
                Email = "john@example.com"
            };
            idea.TeamMembers.Add(memberToRemove);
            program.businessIdeas.Add(idea);

            // Act
            using (var sr = new StringReader(
                $"1\n{memberToRemove.Identification}\n"))
            {
                Console.SetIn(sr);
                program.RemoveTeamMember();
            }

            // Assert
            Assert.IsEmpty(idea.TeamMembers);

            // Check if the success message is printed
            Assert.IsTrue(consoleOutput.ToString().Contains("Team member removed successfully."));
        }

    }
}


}
