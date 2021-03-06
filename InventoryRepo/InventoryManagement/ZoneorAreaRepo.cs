﻿using InventoryServices.InventoryManagement;
using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryRepo.InventoryManagement
{
  public class ZoneorAreaRepo
    {
        #region Declare
        ZoneorAreaDAL _dal = new ZoneorAreaDAL();
        #endregion Declare
        /******************************/
        #region Methods
        public IEnumerable<ZoneOrArea> GETAllZoneOrArea { get { return _dal.GETAllZoneOrArea; } }

        public ZoneOrArea GetSigle(int Id)
        {
            return _dal.GetSigle(Id);
        }
        public IEnumerable<ZoneOrArea> GETbySearch(int? Id, string name = null, string code = null)
        {
            return _dal.GETbySearch(Id, name, code);
        }
        public string[] SaveAndEdit(ZoneOrArea data)
        {
            return _dal.SaveAndEdit(data);
        }
        public string[] Delete(string[] Ids)
        {
            return _dal.Delete(Ids);
        }
        public IEnumerable<ZoneOrArea> Dropdown()
        {
            return _dal.Dropdown();
        }
        public IEnumerable<ZoneOrArea> Autocomplete()
        {
            return _dal.Autocomplete();
        }
        #endregion Method
  
    }
}
