using JooleMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JooleMVC.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View("CreateProject");
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject(Project ProjectModel)
        {
            try
            {
                ProjectModel.Project_ID = 1;
                ProjectModel.User_ID = Int32.Parse(Session["userID"].ToString());
                var client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(ProjectModel), Encoding.UTF8, "application/json");
                HttpResponseMessage postResponse = await client.PostAsync("http://localhost:57022/api/tblProjects", content);
            }
            catch(Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            //return View("CreateProject", ProjectModel);
            return RedirectToAction("ListProjectAsync", "Project", ProjectModel);
        }

        public ActionResult SearchProduct()
        {
            return View("SearchProduct");
        }

        public async Task<ActionResult> ListProjectAsync()
        {
            string apiUrl = "http://localhost:57022/WebAPI/GetProjectList?UserID=" + Session["UserID"].ToString();
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            var data = await response.Content.ReadAsStringAsync();
            List<Project> Projects = JsonConvert.DeserializeObject<List<Project>>(data);
            ProjectListVM ProjectsModel = new ProjectListVM();
            ProjectsModel.Projects = Projects;
            return View("MyProject", ProjectsModel);
        }
    }
}