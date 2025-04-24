using BBTT.CrosswordCore;
using BBTT.CrosswordModel;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BBTT.TestCrossword
{
    public class CrosswordGeneratorTests
    {
        private MockRepository mockRepository;
        private Random random;

        public CrosswordGeneratorTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private CrosswordGenerator CreateCrosswordGenerator()
        {
            return new CrosswordGenerator();
        }

        [Fact]
        public async Task ConstructCrossword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var crosswordGenerator = this.CreateCrosswordGenerator();
            List<CrosswordWord> words =
            [
                new CrosswordWord { Word = "bread", Direction = "ACROSS" },
                new CrosswordWord { Word = "dream", Direction = "DOWN" },
            ]; // Initialize words list

            CancellationToken cancellationToken = default;

            // Act
            var result = await crosswordGenerator.ConstructCrossword(
                words.ToArray(),
                cancellationToken);

            // Assert
            Assert.True(result != null);
            //TODO fix
            //Assert.True(result.Count > 0);
            Assert.False(cancellationToken.IsCancellationRequested);
            this.mockRepository.VerifyAll();
        }
    }
}
