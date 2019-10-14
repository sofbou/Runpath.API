using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using RunPath.API.Client;
using RunPath.API.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace RunPath.Test
{
    public class AlbumPhotoClientTest
    {
        private HttpClient GetTestableClient(HttpStatusCode statusCode, object contentToReturn = null)
        {
            var unauthorizedResponseHttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            var serializedContentToReturn = JsonConvert.SerializeObject(contentToReturn);
            var content = new StringContent(serializedContentToReturn);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            unauthorizedResponseHttpMessageHandlerMock.Protected()
                 .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               ).ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = statusCode,
                   Content = content
               });

            return new HttpClient(unauthorizedResponseHttpMessageHandlerMock.Object);
        }

        [Fact]
        public void GetItems_UnauthorizedAccessException()
        {
            var httpClient = GetTestableClient(HttpStatusCode.Unauthorized); 

            var alClient = new AlbumPhotoClient(httpClient);

            var cancellationTokenSource = new CancellationTokenSource();

            Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => alClient.GetItems<Album>("albums", cancellationTokenSource.Token));
        }

        [Fact]
        public async Task GetAlbums_Valid()
        {
            var expected = new List<Album>
            {
                new Album
                {
                    Id = 1,
                    UserId = 1,
                    Title = "Album test - part 1"
                },
                new Album
                {
                    Id = 2,
                    UserId = 1,
                    Title = "Album test - part 2"
                },
            };

            var httpClient = GetTestableClient(HttpStatusCode.OK, expected);

            var alClient = new AlbumPhotoClient(httpClient);

            var cancellationTokenSource = new CancellationTokenSource();

            var actual = (await alClient.GetItems<Album>("albums", cancellationTokenSource.Token)).ToList();

            Assert.NotNull(actual);

            // Should be working. I'll debug that leter
            //Assert.True(expected.SequenceEqual(actual));

            Assert.True(expected.Count() == actual.Count());

            Assert.True(expected[0].Id == actual[0].Id);
            Assert.True(expected[0].UserId == actual[0].UserId);
            Assert.True(expected[0].Title == actual[0].Title);

            Assert.True(expected[1].Id == actual[1].Id);
            Assert.True(expected[1].UserId == actual[1].UserId);
            Assert.True(expected[1].Title == actual[1].Title);

        }

        [Fact]
        public async Task GetPhotos_Valid()
        {
            var expected = new List<Photo>
            {
                new Photo
                {
                    Id = 1,
                    AlbumId = 1,
                    Title = "photo test - part 1",
                    Url = "url_1",
                    ThumbnailUrl = "thumbnailUrl_1"
                },
                new Photo
                {
                    Id = 2,
                    AlbumId = 1,
                    Title = "photo test - part 2",
                    Url = "url_2",
                    ThumbnailUrl = "thumbnailUrl_2"
                },
            };

            var httpClient = GetTestableClient(HttpStatusCode.OK, expected);

            var alClient = new AlbumPhotoClient(httpClient);

            var cancellationTokenSource = new CancellationTokenSource();

            var actual = (await alClient.GetItems<Photo>("photos", cancellationTokenSource.Token)).ToList();


            Assert.NotNull(actual);

            // Should be working. I'll debug that leter
            //Assert.True(expected.SequenceEqual(actual));

            Assert.True(expected.Count() == actual.Count());

            Assert.True(expected[0].Id == actual[0].Id);
            Assert.True(expected[0].AlbumId == actual[0].AlbumId);
            Assert.True(expected[0].Title == actual[0].Title);
            Assert.True(expected[0].Url == actual[0].Url);
            Assert.True(expected[0].ThumbnailUrl == actual[0].ThumbnailUrl);

            Assert.True(expected[1].Id == actual[1].Id);
            Assert.True(expected[1].AlbumId == actual[1].AlbumId);
            Assert.True(expected[1].Title == actual[1].Title);
            Assert.True(expected[1].Url == actual[1].Url);
            Assert.True(expected[1].ThumbnailUrl == actual[1].ThumbnailUrl);

        }
    }
}
