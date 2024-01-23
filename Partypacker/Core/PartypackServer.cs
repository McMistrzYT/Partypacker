﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Partypacker.Core
{
    internal class PartypackServer
    {
        public static string BaseURL =
#if DEBUG
                "http://localhost:6677";
#else
                "https://partypack.mcthe.dev";
#endif

        public static string DashboardURL =
#if DEBUG
                "http://localhost:5173";
#else
                "https://partypack.mcthe.dev";
#endif

        public static KeyValuePair<bool, string> GET(string URL = "/")
        {
            if (URL.StartsWith("/"))
                URL = BaseURL + URL;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            // request.Headers.Add("my-header", "my-value");

            string Response = "";
            try
            {
                var WebResponse = request.GetResponse();

                StreamReader sr = new StreamReader(WebResponse.GetResponseStream());
                Response = sr.ReadToEnd();
                sr.Close();

                return new KeyValuePair<bool, string>(true, Response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new KeyValuePair<bool, string>(false, string.Empty);
            }
        }
        public static KeyValuePair<bool, string> POST(string URL = "/", string Body = "")
        {
            if (URL.StartsWith("/"))
                URL = BaseURL + URL;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "text/plain";

            string Response = "";
            try
            {
                var WebRequest = request.GetRequestStream();
                WebRequest.Write(Encoding.UTF8.GetBytes(Body), 0, Body.Length);

                var WebResponse = request.GetResponse();

                StreamReader sr = new StreamReader(WebResponse.GetResponseStream());
                Response = sr.ReadToEnd();
                sr.Close();

                return new KeyValuePair<bool, string>(true, Response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new KeyValuePair<bool, string>(false, string.Empty);
            }
        }
    }


}