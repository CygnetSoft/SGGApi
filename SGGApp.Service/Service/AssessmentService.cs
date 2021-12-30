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
    public class AssessmentService : IAssessmentService
    {
        public readonly X509Certificate _certificate;
        public readonly IConfiguration configuration;
        public readonly IWebHostEnvironment hostEnvironment;
        private readonly string clientUrlAddress;
        public AssessmentService(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
            clientUrlAddress = configuration.GetSection("ClientAddress")["AssesmentBaseApi"];
        }
        /// <summary>
        /// AssessmentView
        /// </summary>
        /// <param name="assessmentNumber"></param>
        /// <returns></returns>
        public async Task<object> AssessmentView(string referenceNumber)
        {
            try
            {
                ApiResponse<object> resp = new ApiResponse<object>();
                Task<string> httpResponse = null;
                using (HttpClientHandler httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
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
                            RequestUri = new Uri(clientUrlAddress + "tpg/assessments/details/" + referenceNumber),
                        };
                        HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                        httpResponse = response.Content.ReadAsStringAsync();
                        resp.Result = JsonConvert.DeserializeObject<object>(Algorithm.Decrypt(httpResponse.Result));
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
        /// EnrollmentSearch
        /// </summary>
        /// <param name="assessment"></param>
        /// <returns></returns>
        public async Task<object> AssessmentSearch(AssessmentSearchModel assessment)
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
                        string encryptPayload = Algorithm.Encrypt(JsonConvert.SerializeObject(assessment));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "tpg/assessments/search"),
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
                return (response);
            }
        }
        /// <summary>
        /// AssessmentCreate
        /// </summary>
        /// <param name="assessment"></param>
        /// <returns></returns>
        public async Task<object> AssessmentCreate(AssessmentAddModel assessment)
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
                        string encryptPayload = Algorithm.Encrypt(JsonConvert.SerializeObject(assessment));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "tpg/assessments"),
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
        /// AssessmentUpdate
        /// </summary>
        /// <param name="assessmentNo"></param>
        /// <returns></returns>
        public async Task<object> AssessmentUpdate(AssessmentUpdateModel assessment, string referenceNumber)
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

                        string encryptPayload = Algorithm.Encrypt(JsonConvert.SerializeObject(assessment));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "tpg/assessments/details/" + referenceNumber),
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
                            resp.Status = httpResponse.Status.ToString();
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
        public void WriteLog(string query, int count)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = Path.Combine(hostEnvironment.ContentRootPath, "Logs/");
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists)
            {
                logDirInfo.Create();
            }

            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write);
            }
            log = new StreamWriter(fileStream);
            StringBuilder sb = new StringBuilder();
            sb.Append(count + "-\t");
            sb.Append(query);
            sb.Append("\n");
            sb.Append(DateTime.Now);
            sb.Append("\n");

            log.WriteLine(sb.ToString());
            log.Close();
        }
    }
}
