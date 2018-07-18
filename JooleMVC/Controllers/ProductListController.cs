using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JooleMVC.Models;
using Newtonsoft.Json;

namespace JooleMVC.Controllers
{
    public class ProductListController : Controller
    {
        public async Task<ActionResult> GetProductListJson(int SubCategory_ID)
        {
            ProductListVM ProductList = new ProductListVM();

            string apiUrl_User = "http://localhost:57022/WebAPI/GetUserDetailById?";
            apiUrl_User += "UserID=" + Session["UserID"].ToString();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl_User);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiUrl_User);
            var data_user = await response.Content.ReadAsStringAsync();
            UserModel User = JsonConvert.DeserializeObject<UserModel>(data_user);
            ProductList.User = User;



            string apiUrl = "http://localhost:57022";
            client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string SubID = SubCategory_ID.ToString();
            HttpResponseMessage response1 = await client.GetAsync("ProductList/GetProductsBySubCategory?SubCategory_ID=" + SubID);
            var data = await response1.Content.ReadAsStringAsync();
            List<ProductDetails> ldd = JsonConvert.DeserializeObject<List<ProductDetails>>(data);
            ProductList.ProductList = ldd;


            return View("ProductList", ProductList);
        }
    }
}
