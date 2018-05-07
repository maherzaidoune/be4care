using be4care.Helpers;
using be4care.Models;
using be4care.Utils;
using Firebase.Storage;
using Flurl.Http;
using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace be4care.Services
{
    class RestServices : IRestServices
    {
        public string random()
        {
            Random random = new Random();
            return random.Next(999999999).ToString() + ".png";
        }

        public bool Inscription(string data, string pass)
        {
            try
            {
                var result = Constant.url.PostJsonAsync(new { email = data, password = pass }).Result.IsSuccessStatusCode;
                if (result)
                {
                    if (GetAccessToken(data, pass))
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
                Settings.AuthToken = Constant.urlLogin.PostJsonAsync(new { email = data, password = pass }).ReceiveJson<Login>().Result.id;
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
                return (Constant.urlme + "/exists" + token).GetAsync().Result.IsSuccessStatusCode;
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
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return (Constant.urlgetDocuments + token).GetJsonAsync<IList<Document>>();
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

        public Task<IList<Doctor>> GetDoctorsAsync()
        {
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return (Constant.urlgetDoctors + token).GetJsonAsync<IList<Doctor>>();

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

        public async Task<string> Upload(User user)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = true
            });


            if (file == null)
                return null;

            Console.WriteLine(file.Path);
            var stream = File.Open(file.Path, FileMode.Open);


            var task = await new FirebaseStorage("be4care-2f7ae.appspot.com")
            .Child(user.email)
            .Child("Documents")
            .Child(random())
            .PutAsync(stream);
            try
            {
                var downloadUrl = task;
                Console.WriteLine(downloadUrl);
                return downloadUrl;

            }
            catch (Exception e)
            {
                Console.WriteLine("restServoces error add document" + e.StackTrace);
                return null;
            }


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
                var result = (Constant.urlme + token).PatchJsonAsync
                    (new {
                        name = user.name,
                        lastName = user.lastName,
                        phNumber = user.phNumber,
                        bDate = user.bDate.ToUniversalTime(),
                        sex = user.sex,
                        pUrl = user.pUrl,
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


        public async Task<bool> AddDoctor(Doctor d)
        {
            var doc = new Doctor
            {
                adress = d.adress,
                phNumber = d.phNumber,
                email = d.email,
                healthStruct = d.healthStruct,
                star = d.star,
                specialite = d.specialite,
                note = d.note
            };

            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                var result = await (Constant.urlgetDoctors + token).PostJsonAsync(doc);
                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine("error adding  Doctor : " + e.StackTrace);
                return false;
            }
        }

        public Task<IList<HealthStruct>> GetHealthStructs()
        {
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return (Constant.urlgetHealthStruct + token).GetJsonAsync<IList<HealthStruct>>();
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

        public async Task<bool> AddHealthStruct(HealthStruct s)
        {

           var str = new HealthStruct
           {
                fullName = s.fullName,
                adress =s.adress,
                 phNumber = s.phNumber,
                 email = s.email,
                 star = s.star,
            };
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                var result = await (Constant.urlgetHealthStruct + token).PostJsonAsync(str);
                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine("error adding  Healthstruct : " + e.StackTrace);
                return false;
            }
        }

        public Task<string> Analyse(string Url)
        {
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return (Constant.urlAnalyse + token).PostJsonAsync(new { url = Url }).ReceiveString();
            }catch(Exception e)
            {
                Console.WriteLine("Error analysing + " + e.StackTrace);
                return null;
            }
        }

        public bool addDocument(Document d)
        {

            var doc = new Document
            {
                url = d.url,
                star = d.star,
                date =d.date,
                dr = d.dr,
                type  = d.type,
                HStructure = d.HStructure,
                place =d.place,
                note = d.note,
            };
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return  (Constant.urlgetDocuments + token).PostJsonAsync(doc).Result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine("Eroor adding doc "+e.StackTrace);
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return (Constant.urlDisconnect + token).PostJsonAsync(new { }).Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return (Constant.urlme + token).DeleteAsync().Result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public Task<IList<Doctor>> GetAllDoctors()
        {
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return (Constant.urlAllDoctors + token).GetJsonAsync<IList<Doctor>>();
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

        public Task<IList<HealthStruct>> GetAllHstruct()
        {
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                return (Constant.urlAllHstruct + token).GetJsonAsync<IList<HealthStruct>>();
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

        public async Task<bool> AddDoctorFromDB(string id)
        {
            try
            {
                var token = "?access_token=" + Settings.AuthToken;
                var result = await (Constant.urlAddDocFromDb + token).PostStringAsync(id);
                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine("error adding  Healthstruct : " + e.StackTrace);
                return false;
            }
        }
    }
    
}
