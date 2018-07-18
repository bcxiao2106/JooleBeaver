using JooleMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JooleMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Logintest");
        }

        // GET: Register
        public HomeController()
        {
        }
        public ActionResult login()
        {
            return View("Logintest");
        }
        public ActionResult register()
        {
            return View("register");
        }
        [HttpPost]
        public async Task<ActionResult> Submit(UserModel User, HttpPostedFileBase User_Image)
        {
            try
            {
                var client = new HttpClient();
                if (User_Image != null)
                {
                    if (User_Image.ContentLength > 0)
                    {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(User_Image.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(User_Image.ContentLength);
                            User.User_Image_SRC = imageData;
                            //string ss = JsonConvert.SerializeObject(userModel);
                            var content = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
                            HttpResponseMessage postResponse = await client.PostAsync("http://localhost:57022/api/tblUsers", content);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            return View("Logintest");
        }
        public async Task<ActionResult> valid()
        {
            string apiRegister = "http://localhost:57022/WebAPI/validateLogin?userName=";
            apiRegister += Request.Form["User_Name"];
            apiRegister += "&password=";
            apiRegister += Request.Form["User_Password"];
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(apiRegister);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiRegister);

            //if (response.IsSuccessStatusCode)

            string data = (await response.Content.ReadAsStringAsync());
            if (data == "Not Found!")
            {
                ViewData["Not Found!"] = data;
                return View("Logintest");
            }
            else
            {
                char[] delimiterChars = { '{', ',', '}', '=' };
                string[] words = data.Split(delimiterChars);
                string UserID = words.ElementAt(2).Trim();
                string UserName = words.ElementAt(4).Trim();
                string CredentialID = words.ElementAt(6).Trim();

                //create session variables
                Session["UserID"] = UserID;
                Session["UserName"] = UserName;
                Session["CredentialID"] = CredentialID;

                if (CredentialID.Equals("2"))//Consumer
                {
                    return RedirectToAction("SearchProduct", "Project");
                }
                else//Customer
                {
                    return View("Logintest");
                }
            }
        }
        public async Task<ActionResult> showImage()
        {
            string apiRegister = "http://localhost:57022/WebAPI/GetImageSrc?userName=";
            apiRegister += "abcde";
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(apiRegister);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiRegister);

            //if (response.IsSuccessStatusCode)

            byte[] data = Encoding.Unicode.GetBytes(await response.Content.ReadAsStringAsync());
            MemoryStream ms = new MemoryStream(data);
            Image src = Image.FromStream(ms);
            Session["src"] = src;
            return View("showImage");
        }
    }
}