using System;
using System.Collections.Generic;
using DataLayer.Models;
using DataLayer.Repository;
namespace BusinessLayer
{
    public class EqptBusiness
    {
        public IEnumerable<Eqpt_masterModel> Eqpt_details(string ssearch, string ssort, string ssortdir, int icol)
        {
            using (EqptEntities eqpt = new EqptEntities())
            {

                var lists = eqpt.equipment_master.Select(m => new Equipment_masterModel() { Tranx_Id = m.Tranx_Id, EquipmentPartId = m.EquipmentPartId, Description = m.Description, ParentEquipmentPartId = m.ParentEquipmentPartId, IsPhysical = m.IsPhysical ?? false, Compartment = m.Compartment, EquipmentType = m.EquipmentType }).ToList();
                if (!string.IsNullOrEmpty(ssearch))
                {
                    if (icol != 0)
                    {
                        switch (icol)
                        {
                            case 1:
                                lists = lists.Where(m => m.Tranx_Id.ToString() == ssearch).ToList();
                                break;
                            case 2:
                                lists = lists.Where(m => m.EquipmentPartId.ToUpper().Contains(ssearch.ToUpper())).ToList();
                                break;
                            case 3:
                                lists = lists.Where(m => m.Description.ToUpper().Contains(ssearch.ToUpper())).ToList();
                                break;
                            case 4:
                                lists = lists.Where(m => m.ParentEquipmentPartId.ToUpper().Contains(ssearch.ToUpper())).ToList();
                                break;
                            case 5:
                                lists = lists.Where(m => m.EquipmentType.ToUpper().Contains(ssearch.ToUpper())).ToList();
                                break;
                            case 6:
                                lists = lists.Where(m => m.Compartment.ToUpper().Contains(ssearch.ToUpper())).ToList();
                                break;
                            case 7:
                                if ("Yes".Contains(ssearch))
                                {
                                    lists = lists.Where(m => m.IsPhysical == true).ToList();
                                    break;
                                }
                                else if ("No".Contains(ssearch))
                                {
                                    lists = lists.Where(m => m.IsPhysical == false).ToList();
                                    break;
                                }
                                else
                                    break;
                        }
                    }
                    else
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
