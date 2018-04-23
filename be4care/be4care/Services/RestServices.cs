using be4care.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Services
{
    class RestServices
    {
        public static string url = Utils.Constant.url;

        public static string inscription(string email, string password)
        {
            var client = new RestClient(url);
            var request = new RestRequest("users/", Method.POST);
            request.AddParameter("email", email);
            request.AddParameter("password", password);
            request.AddParameter("username", "testing");
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            return content;
        }
        public  async static  string login(string data, string password)
        {
            var client = new RestClient(url);
            var request = new RestRequest("users/login", Method.POST);

            if (Utils.EntryCheck.checkMail(data))
            {
                request.AddParameter("email", data);
            }
            else
            {
                request.AddParameter("username", data);
            }
            request.AddParameter("password", password);
            IRestResponse response =   client.Execute(request);
            var content = response.Content;
            return content;
        }
    }
}
