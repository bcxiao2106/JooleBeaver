using JooleWebAPI.Unitofwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using JooleWebAPI.DTO;
using System.Data.Entity;

namespace JooleWebAPI.Controllers.ProductListViewController
{
    public class ProductListViewController : Controller
    {

        private UnitOfWork _unitOfWork;

        public ProductListViewController()
        {
            this._unitOfWork = new UnitOfWork(new JooleContext());
        }

        // GET: ProductListView
        public JsonResult GetProductListSubCategory_ID(int SubCategory_ID)
        {
            DbSet<tblProduct> Products = _unitOfWork.ProductList.GetProductList();
            List<ProductDTO> ProductDTOList = new List<ProductDTO>();
            foreach (tblProduct tc in Products)
            {
                if (tc.SubCategory_ID == SubCategory_ID)
                {
                    ProductDTO cd = Mapper.Map<tblProduct, ProductDTO>(tc);
                    ProductDTOList.Add(cd);
                }
            }
            return Json(ProductDTOList, JsonRequestBehavior.AllowGet);
        }
    }
}