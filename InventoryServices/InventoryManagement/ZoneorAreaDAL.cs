﻿using InventoryViewModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryServices.InventoryManagement
{
   public class ZoneorAreaDAL
    {
        #region Declare

        InventoryEntities _context = new InventoryEntities();
        #endregion Declare
        #region Method
        public IEnumerable<ZoneOrArea> GETAllZoneOrArea { get { return _context.ZoneOrAreas.Where(m => m.IsArchive == false).AsEnumerable(); } }

        //public ZoneOrArea GETZoneOrArea { get { return _context.ZoneOrAreas.SingleOrDefault(); } }
        #region sigle method

        public ZoneOrArea GetSigle(int Id)
        {
            var result = _context.ZoneOrAreas.FirstOrDefault(m => m.Id == Id && m.IsArchive == false);
            return result;
        }
        public IEnumerable<ZoneOrArea> GETbySearch(int? Id, string name, string code)
        {
            var result = _context.ZoneOrAreas.Where(t => t.Code == code || t.Name == name && t.IsArchive == false).ToList();
            return result;
        }

        #endregion sigle method
        #region Save and Edit
        public string[] SaveAndEdit(ZoneOrArea data)
        {
            string[] result = new string[6];

            try
            {
                if (data == null) throw new ArgumentNullException("The expected data not found For Insert");

                if (data.Id == null || data.Id == 0)
                {

                    bool duplicateCode = _context.ZoneOrAreas.Any(m => m.IsArchive == false && m.Code == data.Code);
                    if (duplicateCode == true)
                    {
                        result[1] = "Your Code is already Exit";
                        throw new ArgumentNullException("Your Code is already Exit");
                    }
                    bool duplicateName = _context.ZoneOrAreas.Any(m => m.IsArchive == false && m.Code == data.Code);
                    if (duplicateName == true)
                    {
                        result[1] = "Your Name is already Exit";
                        throw new ArgumentNullException("Your Name is already Exit");
                    }

                    data.IsActive = data.IsActive == false ? false : true;
                    data.IsArchive = false;
                    data.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                    data.CreatedAt = DateTime.Now.ToString();
                    data.CreatedFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.ZoneOrAreas.Add(data);
                    result[1] = "ZoneOrArea Data Save";
                    result[0] = "Successfully";
                }
                else
                {
                    var duplicateCode = _context.ZoneOrAreas.Where(m => m.IsArchive == false && m.Code == data.Code && m.Id != data.Id);
                    if (duplicateCode.Count() > 0)
                    {
                        result[1] = "Your Name is already Exit";
                        throw new ArgumentNullException("Your Code is already Exit");
                    }
                    var duplicateName = _context.ZoneOrAreas.Where(m => m.IsArchive == false && m.Name == data.Name && m.Id != data.Id);
                    if (duplicateName.Count() > 0)
                    {
                        result[1] = "Your Name is already Exit";
                        throw new ArgumentNullException("Your Code is already Exit");
                    }
                    var edit = _context.ZoneOrAreas.Find(data.Id);
                    if (edit == null) throw new ArgumentNullException("The expected data not found for Update");
                    data.IsActive = data.IsActive == false ? false : true;
                    data.IsArchive = false;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString();
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.Entry(edit).CurrentValues.SetValues(data);
                    result[1] = "ZoneOrArea Data Update";
                    result[0] = "Successfully";
                }
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                result[0] = "Fail";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation(
                              "Class: {0}, Property: {1}, Error: {2}",
                           result[1] = " -- " + validationErrors.Entry.Entity.GetType().FullName,
                             result[1] += " -- " + validationError.PropertyName,
                            result[1] += validationError.ErrorMessage);
                    }
                }

                //result[2] = ex.Message.ToString();
                result[0] = "Fail";
            }

            return result;
        }
        #endregion Save and Edit

        #region Delete
        public string[] Delete(string[] Ids)
        {
            string[] result = new string[3];
            try
            {
                for (var i = 0; i < Ids.Length; i++)
                {
                    var data = _context.ZoneOrAreas.Find(Convert.ToInt32(Ids[i]));
                    data.IsArchive = true;
                    data.LastUpdateBy = Thread.CurrentPrincipal.Identity.Name; //Commons.CurrentUserName.UserName;
                    data.LastUpdateAt = DateTime.Now.ToString();
                    data.LastUpdateFrom = Commons.GetIpAddress.GetLocalIPAddress();
                    _context.SaveChanges();
                }
                result[1] = "ZoneOrArea Data Delete";
            }
            catch (Exception ex)
            {
                result[2] = ex.Message.ToString();
            }
            finally
            {
                result[0] = "Successfully";
            }
            return result;
        }
        #endregion Delete

        public IEnumerable<ZoneOrArea> Dropdown()
        {
            IEnumerable<ZoneOrArea> ZoneOrAreas = from ZoneOrArea in _context.ZoneOrAreas
                                    where ZoneOrArea.IsArchive == false && ZoneOrArea.IsActive == true
                                    orderby ZoneOrArea.Name
                                    select new ZoneOrArea() { Id = ZoneOrArea.Id, Name = ZoneOrArea.Name };
            return ZoneOrAreas.ToList();
        }
        public IEnumerable<ZoneOrArea> Autocomplete()
        {
            IEnumerable<ZoneOrArea> ZoneOrAreas = from ZoneOrArea in _context.ZoneOrAreas
                                    where ZoneOrArea.IsArchive == false && ZoneOrArea.IsActive == true
                                    orderby ZoneOrArea.Name
                                    select new ZoneOrArea() { Id = ZoneOrArea.Id, Name = ZoneOrArea.Name };
            return ZoneOrAreas.ToList();
        }
        #endregion Method
    }
}
