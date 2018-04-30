using be4care.Helpers;
using be4care.Models;
using be4care.Utils;
using Flurl.Http;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace be4care.Services
{
    class RestServices : IRestServices
    {
        public bool Inscription(string data,string pass)
        {
            try
            {
                var result = Constant.url.PostJsonAsync(new {email= data,password = pass  }).Result.IsSuccessStatusCode;
                if (result)
                {
                    if(GetAccessToken(data, pass))
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (FlurlHttpTimeoutException)
            {
                Console.WriteLine("Request timed out.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        public bool GetAccessToken(string data, string pass)
        {

            try
            {
                Settings.AuthToken = Constant.urlLogin.PostJsonAsync(new { email = data,password = pass }).ReceiveJson<Login>().Result.id;
                return true;
            }
            catch (FlurlHttpTimeoutException)
            {
                Console.WriteLine("Request timed out.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool Init()
        {
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return (Constant.urlme + "exists/" + token).GetAsync().Result.IsSuccessStatusCode;
            }
            catch (FlurlHttpTimeoutException)
            {
                Console.WriteLine("Request timed out.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
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
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return (Constant.urlme + token).GetJsonAsync<User>().Result;
            }
            catch (FlurlHttpTimeoutException)
            {
                Console.WriteLine("Request timed out.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool UpdateProfile(User user)
        {
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                var result =  (Constant.urlme + token).PatchJsonAsync
                    (new {
                        name = user.name,
                        lastName = user.lastName,
                        phNumber = user.phNumber,
                        bDate = user.bDate.ToUniversalTime(),
                        sex  = user.sex, 
                        pUrl = user.pUrl,
                        username = user.username
                        }).
                    Result;
                return result.IsSuccessStatusCode;
            }
            catch (FlurlHttpTimeoutException)
            {
                Console.WriteLine("Request timed out.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
    
}
