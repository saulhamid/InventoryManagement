using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
   public class UOMRepo
   {
       #region Declare
       UOMDAL _dal = new UOMDAL();
       #endregion Declare
       /******************************/
       #region Methods
       public IEnumerable<UOM> GETAllUOM { get { return _dal.GETAllUOM; } }

       public UOM GetSigle(int Id) {
           return _dal.GetSigle(Id);
       }
       public IEnumerable<UOM> GETbySearch(int? Id, string name=null, string code=null)
       {
           return  _dal.GETbySearch(Id, name, code);
       }
       public string[] SaveAndEdit(UOM data)
       {
           return _dal.SaveAndEdit(data);
       }
       public string[] Delete(string[] Ids)
       {
           return _dal.Delete(Ids);
       }
       public dynamic Dropdown()
       {
           return _dal.Dropdown();
       }
       public IEnumerable<UOM> Autocomplete(string term)
       {
           return _dal.Autocomplete(term);
       }
       #endregion Method
   }
}
