using JooleMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JooleMVC.Controllers
{
    public class CompareController : Controller
    {
        // GET: Compare
        public ActionResult Index(int id)
        {
            return View();
        }

        public async Task<ActionResult> GetProductDetails(string ProductIDList)
        {
            string apiUrl_User = "http://localhost:57022/WebAPI/GetUserDetailById?";
            apiUrl_User += "UserID=" + Session["UserID"].ToString();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl_User);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiUrl_User);
            var data_user = await response.Content.ReadAsStringAsync();
            UserModel User = JsonConvert.DeserializeObject<UserModel>(data_user);

            string apiUrl = "http://localhost:57022/WebAPI/GetProductListForCompare?";
            foreach (string i in ProductIDList.Split(','))
            {
                apiUrl += "ProductIDList=" + i + "&";
            }

            client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            response = await client.GetAsync(apiUrl);
            var data = await response.Content.ReadAsStringAsync();
            List<ProductDetails> ldd = JsonConvert.DeserializeObject<List<ProductDetails>>(data);

            ProductDetailVM ProductList = new ProductDetailVM();
            ProductList.ProductList = ldd;
            ProductList.User = User;
            return View("ProductCompare", ProductList);
        }

        public async Task<ActionResult> ProductSummary(int inp)
        {
            ProductDetailVM pObj = new ProductDetailVM();
            string apiRegister = "http://localhost:57022/WebAPI/getProductPageValues?productID=";
            apiRegister += inp;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(apiRegister);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiRegister);

            //if (response.IsSuccessStatusCode)

            string data = (await response.Content.ReadAsStringAsync());
            if (data == null)
            {
                return View("ProductSummary");
            }
            else
            {
                char[] delimiterChars = { ',' };
                string[] words = data.Split(delimiterChars);
                pObj.data = words;
                return View("ProductSummary", pObj);
            }
        }
    }
}