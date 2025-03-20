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
                GridEntries = new List<GridEntry>
                        {
                            new GridEntry { Row = 0, Column = 0, Value = 'a' },
                            new GridEntry { Row = 0, Column = 1, Value = 'p' },
                            new GridEntry { Row = 0, Column = 2, Value = 'p' },
                            new GridEntry { Row = 0, Column = 3, Value = 'l' },
                            new GridEntry { Row = 0, Column = 4, Value = 'e' },
                            new GridEntry { Row = 1, Column = 0, Value = 'b' },
                            new GridEntry { Row = 1, Column = 1, Value = 'a' },
                            new GridEntry { Row = 1, Column = 2, Value = 'n' },
                            new GridEntry { Row = 1, Column = 3, Value = 'a' },
                            new GridEntry { Row = 1, Column = 4, Value = 'n' },
                            new GridEntry { Row = 1, Column = 5, Value = 'a' }
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
                    int minRow = grid.GridEntries.Min(e => e.Row);
                    int maxRow = grid.GridEntries.Max(e => e.Row);

                    int minCol = grid.GridEntries.Min(e => e.Column);
                    int maxCol = grid.GridEntries.Max(e => e.Column);

                    for (int i = minRow; i <= maxRow; i++)
                    {
                        for (int j = minCol; j <= maxCol; j++)
                        {
                            if (!grid.GridEntries.Any(e => e.Row == i && e.Column == j))
                            {
                                grid.GridEntries.Add(new GridEntry { Row = i, Column = j, Value = '@' });
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
