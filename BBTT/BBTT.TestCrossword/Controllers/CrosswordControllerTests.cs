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
        public void GetStateUnderTestExpectedBehavior()
        {
            // Arrange
            var crosswordController = this.CreateCrosswordController();

            // Act
            var result = crosswordController.Get();

            // Assert
            Assert.True(result.Count() == 4);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task PostCrosswordGenerationStateUnderTestExpectedBehavior()
        {
            // Arrange
            var crosswordGrid = new CrosswordGrid
            {
                Grid = new Dictionary<(int, int), char>
                {
                    { (0, 0), 'a' }, { (0, 1), 'p' }, { (0, 2), 'p' }, { (0, 3), 'l' }, { (0, 4), 'e' },
                    { (1, 0), 'b' }, { (1, 1), 'a' }, { (1, 2), 'n' }, { (1, 3), 'a' }, { (1, 4), 'n' }, { (1, 5), 'a' }
                }
            };

            // Adds the above grid that is wrong to the mockCrosswordAccesor
            this.mockCrosswordAccesor
                .Setup(m => m.ConstructCrossword(It.IsAny<CrosswordWord[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(crosswordGrid);

            // Does the same as the original implementation of AddBlankValuesToGrid
            this.mockCrosswordAccesor
                .Setup(m => m.AddBlankValuesToGrid(It.IsAny<CrosswordGrid>()))
                .Returns((CrosswordGrid grid) =>
                {
                    int minRow = grid.Grid.Keys.Min(k => k.Item1);
                    int maxRow = grid.Grid.Keys.Max(k => k.Item1);

                    int minCol = grid.Grid.Keys.Min(k => k.Item2);
                    int maxCol = grid.Grid.Keys.Max(k => k.Item2);

                    for (int i = minRow; i <= maxRow; i++)
                    {
                        for (int j = minCol; j <= maxCol; j++)
                        {
                            if (!grid.Grid.ContainsKey((i, j)))
                            {
                                grid.Grid.Add((i, j), '@');
                            }
                        }
                    }
                    return grid;
                });

            var crosswordController = this.CreateCrosswordController();
            CrosswordWord[] words = new CrosswordWord[]
            {
                new CrosswordWord { Word = "apple", Hint = "Something red", Direction = "DOWN" },
                new CrosswordWord { Word = "banana", Hint = "Something yellow", Direction = "ACROSS" }
            };
            CancellationToken cancellationToken = default;

            // Act
            var result = await crosswordController.PostCrosswordGeneration(words, cancellationToken);

            // Assert
            Assert.False(result == null);
            Assert.True(result.GetType() == typeof(OkObjectResult));
            this.mockRepository.VerifyAll();
        }
    }
}
