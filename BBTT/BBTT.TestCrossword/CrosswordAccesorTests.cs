using BBTT.CrosswordCore;
using BBTT.CrosswordModel;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BBTT.TestCrossword
{
    public class CrosswordAccesorTests
    {
        private MockRepository mockRepository;

        private Mock<ICrosswordGenerator> mockCrosswordGenerator;

        public CrosswordAccesorTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockCrosswordGenerator = this.mockRepository.Create<ICrosswordGenerator>();
        }

        private CrosswordAccesor CreateCrosswordAccesor()
        {
            return new CrosswordAccesor(
                this.mockCrosswordGenerator.Object);
        }

        [Fact]
        public void AddBlankValuesToGrid_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var crosswordAccesor = this.CreateCrosswordAccesor();
            CrosswordGrid resultGrid = null;

            // Act
            var result = crosswordAccesor.AddBlankValuesToGrid(
                resultGrid);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ConstructCrossword_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var crosswordAccesor = this.CreateCrosswordAccesor();
            CrosswordWord[] words = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await crosswordAccesor.ConstructCrossword(
                words,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ConstructCrossword_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var crosswordAccesor = this.CreateCrosswordAccesor();
            CrosswordWord[] verticalWords = null;
            CrosswordWord[] horizantalWords = null;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await crosswordAccesor.ConstructCrossword(
                verticalWords,
                horizantalWords,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetCrosswordGrid_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var crosswordAccesor = this.CreateCrosswordAccesor();
            int id = 0;

            // Act
            var result = await crosswordAccesor.GetCrosswordGrid(
                id);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
