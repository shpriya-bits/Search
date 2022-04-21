using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;
using DataLayer.Repository;
namespace BusinessLayer
{
    public class EqptBusiness
    {
        public IEnumerable<Eqpt_masterModel> Eqpt_details(string ssearch, string ssort, string ssortdir)
        {
            using (EqptEntities eqpt = new EqptEntities())
            {

                var lists = eqpt.equipment_master.Select(m => new Eqpt_masterModel() { Tranx_Id = m.Tranx_Id, EquipmentPartId = m.EquipmentPartId, Description = m.Description, ParentEquipmentPartId = m.ParentEquipmentPartId, IsPhysical = m.IsPhysical ?? false, Compartment = m.Compartment, EquipmentType = m.EquipmentType }).ToList();
                if (!string.IsNullOrEmpty(ssearch))
                {
                   lists = lists.Where(m => m.EquipmentPartId.ToUpper().Contains(ssearch.ToUpper()) || (m.Description != null && m.Description.ToUpper().Contains(ssearch.ToUpper())) || (m.ParentEquipmentPartId != null && m.ParentEquipmentPartId.ToUpper().Contains(ssearch.ToUpper()))).ToList();
                }

                //sort
                if (!(string.IsNullOrEmpty(ssort) && string.IsNullOrEmpty(ssortdir)))
                {
                    lists = lists.OrderBy(o => ssort + " " + ssortdir).ToList();
                }
                return lists;
            }
        }

    }
}
