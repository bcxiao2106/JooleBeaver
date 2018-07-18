using AutoMapper;
using JooleWebAPI.DTO;
using JooleWebAPI.Unitofwork;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Mvc;

namespace JooleWebAPI.Controllers
{
    public class WebAPIController : Controller
    {
        private UnitOfWork _unitOfWork;

        //by Andrew
        public WebAPIController()
        {
            this._unitOfWork = new UnitOfWork(new JooleContext());
        }

        public JsonResult Get()
        {
            IEnumerable<tblUser> Users = this._unitOfWork.Users.GetAll();
            _unitOfWork.Complete();
            return Json(Users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStateList()//Return State List
        {
            DbSet<tblState> States = _unitOfWork.Projects.GetStatesList();
            List<StateDTO> StateDTOList = new List<StateDTO>();
            foreach (tblState ts in States)
            {
                StateDTO sd = Mapper.Map<tblState, StateDTO>(ts);
                StateDTOList.Add(sd);
            }
            _unitOfWork.Complete();
            return Json(StateDTOList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityList()//Return City List
        {
            DbSet<tblCity> Cities = _unitOfWork.Projects.GetCityList();
            List<CityDTO> CityDTOList = new List<CityDTO>();
            foreach (tblCity tc in Cities)
            {
                CityDTO cd = Mapper.Map<tblCity, CityDTO>(tc);
                CityDTOList.Add(cd);
            }
            _unitOfWork.Complete();
            return Json(CityDTOList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategoryList()
        {
            var Categories = _unitOfWork.Categories.GetAll().ToList<tblCategory>();
            List<CategoryDTO> CategoryDTOList = new List<CategoryDTO>();
            CategoryDTOList = Mapper.Map<List<tblCategory>, List<CategoryDTO>>(Categories);
            _unitOfWork.Complete();
            return Json(CategoryDTOList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubCateById(int CateID, string Keyword)
        {
            List<string> SubCategories = (from sc in _unitOfWork.SubCategories.GetAll()
                                          where sc.Category_ID == CateID && sc.SubCategory_Name.ToLower().Contains(Keyword.ToLower())
                                        select sc.SubCategory_Name).ToList<string>();
            _unitOfWork.Complete();
            return Json(SubCategories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductListForCompare([FromUri] int[] ProductIDList)
        {
            var ProductList = (from pd in _unitOfWork.Products.GetAll()
                       where ProductIDList.Contains(pd.Product_ID)
                       select pd).ToList<tblProduct>();
            List<ProductDTO> ProductDTOList = new List<ProductDTO>();
            
            ProductDTOList = Mapper.Map<List<tblProduct>, List<ProductDTO>>(ProductList);
            foreach (ProductDTO pd in ProductDTOList)//add property list into product
            {
                int pid = pd.Product_ID;
                List<ProductPropertyDTO> ProductPropertyList = new List<ProductPropertyDTO>();
                ProductPropertyList = (from pp in _unitOfWork.ProductProperties.GetAll()
                                       join ppv in _unitOfWork.ProductPropertyValues.GetAll()
                                       on pp.Property_ID equals ppv.Property_ID
                                       where ppv.Product_ID == pid
                                       select new ProductPropertyDTO { Product_ID = ppv.Product_ID, Property_ID = ppv.Property_ID, Property_Name = pp.Property_Name, Value = ppv.Value, IsType = pp.IsType, IsTechSpec = pp.IsTechSpec }).ToList();
                pd.PropertyList = ProductPropertyList;
            }
            _unitOfWork.Complete();
            return Json(ProductDTOList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProjectListByUserId(int UserID)
        {
            var res = from pd in _unitOfWork.Projects.GetAll()
                      where pd.User_ID == UserID
                      select new { Project_ID = pd.Project_ID, Project_Name = pd.Project_Name };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public int AddProdToProject([FromUri]int ProjectId, int ProductId, int Quantity)
        {
            var ExistedItem = _unitOfWork.ProdToProjRelations.GetAll().SingleOrDefault(x => x.Product_ID == ProductId && x.Project_ID == ProjectId);
            if(ExistedItem == null)//add new
            {
                tblProjToProd ptp = new tblProjToProd { Product_ID = ProductId, Project_ID = ProjectId, Quantity = Quantity };
                _unitOfWork.ProdToProjRelations.Add(ptp);
            }
            else
            {
                ExistedItem.Quantity = ExistedItem.Quantity + Quantity;
            }
            
            int res = _unitOfWork.Complete();
            return res;
        }

        public JsonResult GetUserDetailById(int UserID)
        {
            var res = _unitOfWork.Users.GetAll().SingleOrDefault(x => x.User_ID == UserID);
            UserDTO User = Mapper.Map<tblUser, UserDTO>(res);
            return Json(User, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserImageByID(int UserID)
        {
            var res = _unitOfWork.Users.GetAll().SingleOrDefault(x => x.User_ID == UserID);
            UserDTO User = Mapper.Map<tblUser, UserDTO>(res);
            string ImageString = System.Convert.ToBase64String(User.User_Image_SRC);
            return Json(ImageString, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProjectList(int UserID)
        {
            var Projects = _unitOfWork.Projects.Find(x => x.User_ID == UserID).ToList<tblProject>();
            List<ProjectDTO> ProjectsList = Mapper.Map<List<tblProject>, List<ProjectDTO>>(Projects);
            return Json(ProjectsList, JsonRequestBehavior.AllowGet);
        }

        //by Rohin
        public JsonResult validateUserName(string userName)
        {
            return Json((_unitOfWork.Users.userNameCheck(userName) ? "Not Available!" : "Available").ToString(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult validateUserEmail(string emailID)
        {
            return Json((_unitOfWork.Users.emailCheck(emailID) ? "Not Available!" : "Available").ToString(), JsonRequestBehavior.AllowGet);
        }

        public string validateLogin(string userName, string password)
        {
            return _unitOfWork.Users.loginCheck(userName, password);
        }
        public List<byte[]> GetImageSrc(string userName)
        {
            return _unitOfWork.Users.GetImageSRC(userName);
        }

        public string getProductDescription(int productID)
        {
            string tmp = _unitOfWork.Users.GetProductDescription(productID), ret = null;
            char[] delimiterChars = { '{', ',', '}', '=' };
            string[] words = tmp.Split(delimiterChars);
            bool skip = false;
            foreach (var x in words)
            {
                if (skip == false)
                {
                    ret += x;
                    ret += ',';
                    skip = true;
                }
                else
                {
                    skip = false;
                }
            }
            return ret;
        }
        public string getType(int productID)
        {
            string tmp = _unitOfWork.Users.GetType(productID), ret = null;
            char[] delimiterChars = { '{', ',', '}', '=' };
            string[] words = tmp.Split(delimiterChars);
            /*bool skip = false;
            foreach (var x in words)
            {
                if (skip == false)
                {
                    ret += x;
                    ret += ',';
                    skip = true;
                }
                else
                {
                    skip = false;
                }
            }*/
            bool skip = false; int count = 0;
            foreach (var x in words)
            {
                if (count == 5)
                {
                    skip = false;
                    count = 0;
                }
                if (skip == false)
                {
                    ret += x;
                    ret += ',';
                    skip = true;
                }
                else
                {
                    skip = false;
                }
                count += 1;
            }
            /*foreach (var x in words)
            {
                ret += x;
                ret += ',';
            }*/
            return ret;
        }
        public string getTechSpecs(int productID)
        {
            string tmp = _unitOfWork.Users.GetTechSpecs(productID), ret = null;
            char[] delimiterChars = { '{', ',', '}', '=' };
            string[] words = tmp.Split(delimiterChars);
            bool skip = false; int count = 0;
            foreach (var x in words)
            {
                if (count == 5)
                {
                    skip = false;
                    count = 0;
                }
                if (skip == false)
                {
                    ret += x;
                    ret += ',';
                    skip = true;
                }
                else
                {
                    skip = false;
                }
                count += 1;
            }
            return ret;
        }
        public string getSeriesInfo(int productID)
        {
            string tmp = _unitOfWork.Users.GetSeriesInfo(productID), ret = null;
            char[] delimiterChars = { '{', ',', '}', '=' };
            string[] words = tmp.Split(delimiterChars);
            foreach (var x in words)
            {
                ret += x;
                ret += ',';
            }
            return ret;
        }
        public string getProductDoc(int productID)
        {
            string tmp = _unitOfWork.Users.GetProductDoc(productID), ret = null;
            char[] delimiterChars = { '{', ',', '}', '=' };
            string[] words = tmp.Split(delimiterChars);
            foreach (var x in words)
            {
                ret += x;
                ret += ',';
            }
            return ret;
        }
        public string getSalesRep(int productID)
        {
            string tmp = _unitOfWork.Users.GetSalesRep(productID), ret = null;
            char[] delimiterChars = { '{', ',', '}', '=' };
            string[] words = tmp.Split(delimiterChars);
            bool skip = false;
            foreach (var x in words)
            {
                if (skip == false)
                {
                    ret += x;
                    ret += ',';
                    skip = true;
                }
                else
                {
                    skip = false;
                }
            }
            return ret;
        }
        public string getManufacturer(int productID)
        {
            string tmp = _unitOfWork.Users.GetManufacturer(productID), ret = null;
            char[] delimiterChars = { '{', ',', '}', '=' };
            string[] words = tmp.Split(delimiterChars);
            bool skip = false;
            foreach (var x in words)
            {
                if (skip == false)
                {
                    ret += x;
                    ret += ',';
                    skip = true;
                }
                else
                {
                    skip = false;
                }
            }
            return ret;
        }
        public string getProductSummary(int productID)
        {
            var ret = getProductDescription(productID);
            ret += getType(productID);
            ret += getTechSpecs(productID);
            return ret;
        }
        public string getProductDetails(int productID)
        {
            return getSeriesInfo(productID);
        }
        public string getProductDocumentation(int productID)
        {
            return getProductDoc(productID);
        }
        public string getContact(int productID)
        {
            var ret = getSalesRep(productID);
            ret += getManufacturer(productID);
            return ret;
        }
        public string getProductPageValues(int productID)
        {
            var ret = getProductSummary(productID);
            ret += getProductDetails(productID);
            ret += getProductDocumentation(productID);
            ret += getContact(productID);
            return ret;
        }

    }
}