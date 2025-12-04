using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Volte.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Assistance
{
    public class NewGetHttpHelper : AbstractHttpHelper
    {
        public NewGetHttpHelper()
        {
        }

        public NewGetHttpHelper(string baseAddr, string path, string auth)
        {
            sBaseAddr = baseAddr;
            sAuth = auth;
            sPath = path;
        }

        public NewGetHttpHelper(string baseAddr, string path)
        {
            sBaseAddr = baseAddr;
            sPath = path;
        }

        public override string GetResult()
        {
            string empty = string.Empty;

            HttpContent httpContent = new StringContent(string.Empty);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpContent.Headers.ContentType.CharSet = "utf-8";

            using HttpClient httpClient = NewHttpClient();
            if (!string.IsNullOrEmpty(sBaseAddr))
            {
                httpClient.BaseAddress = new Uri(sBaseAddr);
            }
            if (!string.IsNullOrEmpty(sAuth))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", sAuth);
            }

            Task<HttpResponseMessage> async = httpClient.GetAsync(sPath);
            async.Wait();
            return async.Result.Content.ReadAsStringAsync().Result;
        }

        public override Stream GetStream()
        {
            Stream stream = null;
            using HttpClient httpClient = NewHttpClient();
            if (!string.IsNullOrEmpty(sBaseAddr))
            {
                httpClient.BaseAddress = new Uri(sBaseAddr);
            }

            Task<HttpResponseMessage> async = httpClient.GetAsync(sPath);
            async.Wait();
            return async.Result.Content.ReadAsStreamAsync().Result;
        }
    }
}
