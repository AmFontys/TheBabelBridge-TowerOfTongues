using BBTT.CrosswordAPI.Controllers;
using BBTT.CrosswordCore;
using BBTT.CrosswordModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BBTT.TestCrossword.Controllers
{
    public class CrosswordControllerTests
    {
        private MockRepository mockRepository;

        private Mock<ILogger<CrosswordController>> mockLogger;
        private Mock<ICrosswordAccesor> mockCrosswordAccesor;

        public CrosswordControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockLogger = this.mockRepository.Create<ILogger<CrosswordController>>();
            this.mockCrosswordAccesor = this.mockRepository.Create<ICrosswordAccesor>();
        }

        private CrosswordController CreateCrosswordController()
        {
            return new CrosswordController(
                this.mockLogger.Object,
                this.mockCrosswordAccesor.Object);
        }

        [Fact]
        public void GetStateUnderTestExpectedBehavior ()
        {
            // Arrange
            var crosswordController = this.CreateCrosswordController();

            // Act
            var result = crosswordController.Get();

            // Assert
            Assert.True(result.Count()==4);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task PostCrosswordGenerationStateUnderTestExpectedBehavior ()
        {
            // Arrange
            var crosswordController = this.CreateCrosswordController();
            CrosswordWord[] words = [ new CrosswordWord("apple","Something red","DOWN")];
            words.Append(new CrosswordWord("banana","Something red","ACROSS"));
            CancellationToken cancellationToken = default;

            // Act
            var result = await crosswordController.PostCrosswordGeneration(
                words,
                cancellationToken);

            // Assert
            Assert.False(result == null);
            Assert.True(result.GetType() == typeof(OkObjectResult));
            this.mockRepository.VerifyAll();
        }
    }
}
