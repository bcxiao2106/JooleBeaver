using JooleWebAPI.Unitofwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JooleWebAPI.DTO;
using System.Data.Entity;
using System.Web.Http;

namespace JooleWebAPI.Controllers.ProductListController
{
    public class ProductListController : Controller
    {

        private UnitOfWork _unitOfWork;

        public ProductListController()
        {
            this._unitOfWork = new UnitOfWork(new JooleContext());
        }

        // GET: ProductList
        public JsonResult GetProductsBySubCategory([FromUri] int SubCategory_ID)
        {
            var ProductListBySubCategory = (from pd in _unitOfWork.Products.GetAll()
                                            where pd.SubCategory_ID == SubCategory_ID
                                            select pd).ToList<tblProduct>();

            List<ProductDTO> ProductDTOList = new List<ProductDTO>();
            ProductDTOList = Mapper.Map<List<tblProduct>, List<ProductDTO>>(ProductListBySubCategory);

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

        public JsonResult GetPropertyMinMax([FromUri] int SubCategory_ID)
        {
            var TechSpecFilterList = (from ts in _unitOfWork.TechSpecFilter.GetAll()
                                      where ts.SubCategory_ID == SubCategory_ID
                                      select ts).ToList<tblTechSpecFilter>();
            List<TechSpecFilterDTO> TechSpecFilterDTOList = new List<TechSpecFilterDTO>();
            TechSpecFilterDTOList = Mapper.Map<List<tblTechSpecFilter>, List<TechSpecFilterDTO>>(TechSpecFilterList);
            foreach (TechSpecFilterDTO ts in TechSpecFilterDTOList)//add property list into product
            {
                int pid = ts.Property_ID;
                var PropertyName = (from pxp in _unitOfWork.ProductProperties.GetAll()
                                    join t in _unitOfWork.TechSpecFilter.GetAll()
                                    on pxp.Property_ID equals t.Property_ID
                                    where t.Property_ID == pid
                                    select pxp.Property_Name).ToList();
                ts.Property_Name = PropertyName[0];
            }
            _unitOfWork.Complete();
            return Json(TechSpecFilterDTOList, JsonRequestBehavior.AllowGet);
        }
    }
}