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
    public class CourseService : ICourseService
    {
        public readonly X509Certificate _certificate;
        public readonly IConfiguration configuration;
        public readonly IWebHostEnvironment hostEnvironment;
        private readonly string clientUrlAddress;
        public CourseService(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
            clientUrlAddress = configuration.GetSection("ClientAddress")["CourseBaseApi"];
        }

        /// <summary>
        /// AddCourseRun
        /// </summary>
        /// <param name="enrollment"></param>
        /// <returns></returns>
        public async Task<object> AddCourseRun(CourseAddModel enrollment)
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
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "courses/runs"),
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
        /// <summary>
        /// GetCourseRuns
        /// </summary>
        /// <param name="runId"></param>
        /// <param name="uen"></param>
        /// <param name="courseReferenceNumber"></param>
        /// <returns></returns>
        public async Task<object> GetCourseRunsByID(string runId)
        {
            try
            {
                ApiResponse<object> resp = new();
                Task<string> httpResponse = null;
                using (HttpClientHandler httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.SslProtocols = SslProtocols.Tls12;
                    httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    string cetificatePath = Path.Combine(hostEnvironment.WebRootPath, "client_certificate.pfx");
                    X509Certificate2 certificate = new(cetificatePath, "123456");
                    httpClientHandler.ClientCertificates.Add(certificate);

                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json;x-api-version=v1.3");
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri(clientUrlAddress + "courses/runs/" + runId.Trim()),
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runId"></param>
        /// <param name="uen"></param>
        /// <param name="courseReferenceNumber"></param>
        /// <returns></returns>
        public async Task<object> GetAttendanceInformation(string runId, string uen, string courseReferenceNumber, string sessionId)
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
                            RequestUri = new Uri(clientUrlAddress + "courses/runs/" + runId.Trim() + "/sessions/attendance?uen=" + uen.Trim() + "&courseReferenceNumber=" + courseReferenceNumber.Trim() + "&sessionId=" + sessionId),
                        };
                        HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                        httpResponse = response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            string json = Algorithm.Decrypt(httpResponse.Result);
                            resp.Result = JsonConvert.DeserializeObject<object>(json);
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
        /// 
        /// </summary>
        /// <param name="runId"></param>
        /// <param name="uen"></param>
        /// <param name="courseReferenceNumber"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public async Task<object> GetCourseSessions(string runId, string uen, string courseReferenceNumber, string sessionMonth)
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
                            RequestUri = new Uri(clientUrlAddress + "courses/runs/" + runId.Trim() + "/sessions?uen=" + uen.Trim() + "&courseReferenceNumber=" + courseReferenceNumber.Trim() + "&sessionMonth=" + sessionMonth),
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
        /// <summary>
        /// UpdateRunsAndSessionByRunsId
        /// </summary>
        /// <param name="runId"></param>
        /// <param name="updateRun"></param>
        /// <returns></returns>
        public async Task<object> UpdateCourseRuns(string runId, CourseUpdateModel updateRun)
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
                        string json = Algorithm.Encrypt(JsonConvert.SerializeObject(updateRun));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "courses/runs/" + runId),
                            Content = new StringContent(JsonConvert.SerializeObject(updateRun), Encoding.UTF8, "application/json")
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runId"></param>
        /// <param name="updateRun"></param>
        /// <returns></returns>
        public async Task<object> UploadAttendance(string runId, UploadAttachmentModel updateAttachRunJson)
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
                        string encryptPayload = Algorithm.Encrypt(JsonConvert.SerializeObject(updateAttachRunJson));
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "courses/runs/" + runId + "/sessions/attendance"),
                            Content = new StringContent(encryptPayload.ToString(), Encoding.UTF8, "application/json")
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
        /// <summary>
        /// DeleteCourseRuns
        /// </summary>
        /// <param name="deleteCourse"></param>
        /// <param name="courseRunId"></param>
        /// <returns></returns>
        public async Task<object> DeleteCourseRuns(CourseDeleteModel deleteCourse, string courseRunId)
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
                    string json = JsonConvert.SerializeObject(deleteCourse);
                    using (HttpClient client = new HttpClient(httpClientHandler))
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add("Accept", "application/json;x-api-version=v1.3");
                        HttpRequestMessage request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri(clientUrlAddress + "courses/runs/" + courseRunId),
                            Content = new StringContent(json, Encoding.UTF8, "application/json")
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
