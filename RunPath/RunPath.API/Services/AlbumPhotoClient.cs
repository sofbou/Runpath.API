using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RunPath.API.Services
{
    public class AlbumPhotoClient
    {
        private HttpClient _client;

        public AlbumPhotoClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com");
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<IEnumerable<T>> GetItems<T>(string query, CancellationToken cancellationToken)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, query);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            try
            {
                using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    if(!response.IsSuccessStatusCode)
                    {
                        switch (response.StatusCode)
                        {
                            default:
                                return null;
                        }
                    }
                    response.EnsureSuccessStatusCode();

                    var stream = await response.Content.ReadAsStreamAsync();
                    var items = ReadAndDeserializeFromJson<IEnumerable<T>>(stream);

                    return items;
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"An operation was cancelled with message {ex.Message}.");
                return null;
            }
        }

        public static T ReadAndDeserializeFromJson<T>(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new NotSupportedException("Can't read from this stream.");
            }

            using (var streamReader = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    return jsonSerializer.Deserialize<T>(jsonTextReader);
                }
            }
        }
    }
}
