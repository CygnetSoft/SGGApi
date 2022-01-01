using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SGGApp.Service.Payload;
using SGGApp.Service.Service.IService;
using SGGApp.Utilities;
using SGGApp.Utilities.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SGGApp.Service.Service
{

    public class TrainerService : ITrainerService
    {
        public readonly X509Certificate _certificate;
        public readonly IConfiguration configuration;
        public readonly IWebHostEnvironment hostEnvironment;
        private readonly string clientUrlAddress;
        public TrainerService(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
            clientUrlAddress = configuration.GetSection("UATAddress")["TrainerBaseApi"];
        }

        /// <summary>
        /// AddTrainerRun
        /// </summary>
        /// <param name="enrollment"></param>
        /// <returns></returns>
        public async Task<object> AddTrainerRun(TrainerAddModel enrollment,string uen)
        {
            try
            {
                ApiResponse<object> resp = new ApiResponse<object>();

                Task<string> httpResponse = null;
                using (HttpClientHandler httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.SslProtocols = SslProtocols.Tls12;
                    httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    string cetificatePath = Path.Combine(hostEnvironment.WebRootPath, "client_certificate.pfx");
                    X509Certificate2 certificate = new X509Certificate2(cetificatePath, "123456");
                    httpClientHandler.ClientCertificates.Add(certificate);

                    using (HttpClient client = new(httpClientHandler))
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json;x-api-version=v1.3");
                        string encryptPayload = Algorithm.Encrypt(JsonConvert.SerializeObject(enrollment));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "trainingProviders/"+ uen + "/trainers"),
                            Content = new StringContent(encryptPayload, Encoding.UTF8, "application/json")
                        };
                       
                        HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                        httpResponse = response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            string decrypted = Algorithm.Decrypt(httpResponse.Result);
                            resp.Result = JsonConvert.DeserializeObject<object>(decrypted);
                        }
                        else
                        {
                            resp.Result = httpResponse;
                        }
                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(!string.IsNullOrEmpty(ex.Message.ToString()) ? ex.Message : ex.InnerException.ToString(), Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.InternalServerError
                };
                return response;
            }

        }

        /// <summary>
        /// UpdateRunsAndSessionByRunsId
        /// </summary>
        /// <param name="runId"></param>
        /// <param name="updateRun"></param>
        /// <returns></returns>
        public async Task<object> UpdateTrainerRuns(string uen, string trainersid, TrainerAddModel enrollment)
        {
            try
            {
                ApiResponse<object> resp = new ApiResponse<object>();
                Task<string> httpResponse = null;
                using (HttpClientHandler httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.SslProtocols = SslProtocols.Tls12;
                    httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    string cetificatePath = Path.Combine(hostEnvironment.WebRootPath, "client_certificate.pfx");
                    X509Certificate2 certificate = new X509Certificate2(cetificatePath, "123456");
                    httpClientHandler.ClientCertificates.Add(certificate);

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json;x-api-version=v1.3");
                        string json = Algorithm.Encrypt(JsonConvert.SerializeObject(enrollment));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "trainingProviders/"+ uen + "/trainers/"+ trainersid),
                            Content = new StringContent(JsonConvert.SerializeObject(enrollment), Encoding.UTF8, "application/json")
                        };
                        HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                        httpResponse = response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            resp.Result = JsonConvert.DeserializeObject<object>(httpResponse.Result);
                        }
                        else
                        {
                            resp.Result = httpResponse;
                        }
                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(!string.IsNullOrEmpty(ex.Message.ToString()) ? ex.Message : ex.InnerException.ToString(), Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.InternalServerError
                };
                return response;
            }
        }
    }
}
