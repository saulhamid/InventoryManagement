using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
   public class ProductDetailRepo
    {
        #region Declare
       ProductDetailDAL _dal = new ProductDetailDAL();
        #endregion Declare
        /******************************/
        #region Methods
        //public IEnumerable<ProductDetail> GETAllProductDetail { get { return _dal.GETAllProductDetail; } }
       public dynamic GETAllProductDetail(int Id)
       {
           return _dal.GETAllProductDetail();
       }

        public ProductDetail GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        //public IEnumerable<ProductDetail> GETbySearch(int? Id, string name = null, string code = null)
        //{
        //    return _dal.GETbySearch(Id, name, code);
        //}
        public string[] SaveAndEdit(ProductDetail data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        //public IEnumerable<ProductDetail> Dropdown()
        //{
        //    return _dal.Dropdown();
        //}
        //public IEnumerable<ProductDetail> Autocomplete()
        //{
        //    return _dal.Autocomplete();
        //}
        #endregion Method

    }
}
