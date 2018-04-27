using be4care.Helpers;
using be4care.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace be4care.Services
{
    class RestServices : IRestServices
    {
        private readonly  string url = Utils.Constant.url;
        private string me = "me?access_token=" + "YKuj1L0dWtMkU80qXfI1JwUlWWEt83eJ5og31pYov6f2oeZiaBY04Wgnijb9ESBs";


        public bool Inscription(string email, string password)
        {
            var client = new RestClient(url);
            var request = new RestRequest("users/", Method.POST);
            request.AddParameter("email", email);
            request.AddParameter("password", password);
            try
            {
                IRestResponse response = client.Execute(request);
                return GetAccessToken(email, password);
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
            var client = new RestClient(url);
            var request = new RestRequest("users/" + me, Method.GET);
            try
            {
                IRestResponse response = client.Execute(request);
                return JsonHelper.Deserialize<User>(response);
            }
            catch
            {
                // will , i'mnot sure  if this
                Console.WriteLine("error  getting user profile");
                return new User();
            }
        }

        public bool UpdateProfile(User user)
        {
            var client = new RestClient(url);
            var request = new RestRequest("users/" + me, Method.PATCH);
            request.Parameters.Clear();

            //request.AddParameter("email", user.email);
            request.AddParameter("name", user.name);
            request.AddParameter("lastName", user.lastName);
            request.AddParameter("phNumber", user.phNumber);
            request.AddParameter("bDate", user.bDate);
            request.AddParameter("sex", user.sex);
            request.AddParameter("pUrl", user.pUrl);
            request.AddParameter("username", user.username);
            try
            {
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.ToString());
                return  response.IsSuccessful;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}
