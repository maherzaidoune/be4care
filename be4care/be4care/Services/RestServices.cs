using be4care.Helpers;
using be4care.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    class RestServices : IRestServices
    {
        public  string url = Utils.Constant.url;

        public  bool Inscription(string email, string password)
        {
            var client = new RestClient(url);
            var request = new RestRequest("users/", Method.POST);
            request.AddParameter("email", email);
            request.AddParameter("password", password);
            try
            {
                IRestResponse response = client.Execute(request);
                if(GetAccessToken(email, password))
                {
                    Console.WriteLine("Setting.token : " + Settings.AuthToken);
                }
                return response.IsSuccessful;
            }
            catch 
            {
                return false;
            }
        }
        public  bool  GetAccessToken(string data, string password)
        {

            var client =  new RestClient(url);
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

            try
            {
                IRestResponse response =  client.Execute(request);
                Login responseData =  JsonHelper.Deserialize<Login>(response);
                Settings.AuthToken = responseData.id;
                //Console.WriteLine(responseData.id);
                return response.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }

        public Task Init()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Document>> GetDocumentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Doctor>> GetDoctorsAsync()
        {
            throw new NotImplementedException();
        }

        public string Analyse()
        {
            throw new NotImplementedException();
        }

        public string UploadPhoto(string Url)
        {
            throw new NotImplementedException();
        }

        public User GetMyProfile()
        {
            throw new NotImplementedException();
        }

        public bool UpdateProfile(User user)
        {
            var client = new RestClient(url);
            var urlParams = "users/me?access_token=" + Settings.AuthToken;
            var request = new RestRequest(urlParams, Method.PATCH);
            request.AddParameter("email", user.email);
            request.AddParameter("name", user.name);
            request.AddParameter("lastName", user.lastName);
            request.AddParameter("phNumber", user.phNumber);
            request.AddParameter("bDate", user.bDate);
            request.AddParameter("sex", user.sex);
            request.AddParameter("pUrl", user.pUrl);
            request.AddParameter("username", user.username);
            //request.AddParameter("Authorization", "Bearer " +Settings.AuthToken, ParameterType.HttpHeader);
            try
            {
                Console.WriteLine(url);
                Console.WriteLine(urlParams);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                return  response.IsSuccessful;
            }
            catch
            {
                return false;
            }
        }
    }
}
