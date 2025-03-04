using BBTT.CrosswordModel;
using BBTT.Web;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BBTT.TestCrossword
{
    public class CrossWordApiClientTests
    {
        private MockRepository mockRepository;

        private Mock<HttpClient> mockHttpClient;

        public CrossWordApiClientTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockHttpClient = this.mockRepository.Create<HttpClient>();
        }

        private CrossWordApiClient CreateCrossWordApiClient()
        {
            return new CrossWordApiClient(
                this.mockHttpClient.Object);
        }

        [Fact]
        public async Task GetDictionary_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var crossWordApiClient = this.CreateCrossWordApiClient();
            int maxItems = 0;
            CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await crossWordApiClient.GetDictionary(
                maxItems,
                cancellationToken);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task PostWordsGetGrid_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var crossWordApiClient = this.CreateCrossWordApiClient();
            CrosswordWord[] words = null;

            // Act
            var result = await crossWordApiClient.PostWordsGetGrid(
                words);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetClosestWord_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var crossWordApiClient = this.CreateCrossWordApiClient();
            string input = null;

            // Act
            var result = await crossWordApiClient.GetClosestWord(
                input);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
