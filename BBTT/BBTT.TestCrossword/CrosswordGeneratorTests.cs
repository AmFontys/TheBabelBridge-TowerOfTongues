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
            CrosswordWord[] words = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await crosswordGenerator.ConstructCrossword(
                words,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
