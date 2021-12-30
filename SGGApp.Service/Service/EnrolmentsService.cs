using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SGGApp.Service.Payload;
using SGGApp.Service.Service.IService;
using SGGApp.Utilities;
using SGGApp.Utilities.ViewModel;

namespace SGGApp.Service.Service
{
    public class EnrolmentsService : IEnrolmentsService
    {
        public readonly X509Certificate _certificate;
        public readonly IConfiguration configuration;
        public readonly IWebHostEnvironment hostEnvironment;
        private readonly string clientUrlAddress;
        public EnrolmentsService(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
            clientUrlAddress = configuration.GetSection("ClientAddress")["EnrolmentBaseApi"];
        }
        /// <summary>
        /// EnrollmentSearch
        /// </summary>
        /// <param name="enrollmentNo"></param>
        /// <returns></returns>
        public async Task<object> EnrollmentSearch(EnrollmentsSearchModel enrollment)
        {
            try
            {
                object resp = new object();
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
                        string encryptPayload = Algorithm.Encrypt(JsonConvert.SerializeObject(enrollment));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "tpg/enrolments/search"),
                            Content = new StringContent(encryptPayload, Encoding.UTF8, "application/json")
                        };
                        HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                        httpResponse = response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            string decrypted = Algorithm.Decrypt(httpResponse.Result);
                            resp = JsonConvert.DeserializeObject<object>(decrypted);
                        }
                        else
                        {
                            resp = httpResponse;
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
        /// EnrollmentView
        /// </summary>
        /// <param name="enrollmentNumber"></param>
        /// <returns></returns>
        public async Task<object> EnrollmentView(string referenceNumber)
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
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri(clientUrlAddress + "tpg/enrolments/details/" + referenceNumber),
                        };
                        HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                        httpResponse = response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            resp.Result = JsonConvert.DeserializeObject<object>(Algorithm.Decrypt(httpResponse.Result));
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
        /// EnrollmentAdd
        /// </summary>
        /// <param name="enrollment"></param>
        /// <returns></returns>
        public async Task<object> EnrollmentAdd(EnrollmentsAddModel enrollment)
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
                        string encryptPayload = Algorithm.Encrypt(JsonConvert.SerializeObject(enrollment));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "tpg/enrolments"),
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
        /// EnrollmentUpdate
        /// </summary>
        /// <param name="enrollment"></param>
        /// <returns></returns>
        public async Task<object> EnrollmentUpdate(EnrollmentsUpdateModel enrollment, string enrollmentNo)
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
                        string encryptPayload = Algorithm.Encrypt(JsonConvert.SerializeObject(enrollment));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "tpg/enrolments/details/" + enrollmentNo),
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
        /// EnrollmentFeesCollect
        /// </summary>
        /// <param name="enrollment"></param>
        /// <returns></returns>
        public async Task<object> EnrollmentFeesCollect(EnrollmentsFeesCollectModel enrollment, string enrollmentNo)
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
                        string encryptPayload = Algorithm.Encrypt(JsonConvert.SerializeObject(enrollment));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "tpg/enrolments/feeCollections/" + enrollmentNo),
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
    }
}
