using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plenamente.Models.ViewModel
{
    public class ArchivoCumpView  /*Esto puede causar estrago con BaseViewModel*/
    {
        public List<ItemEstandar> ListIE { get; set; }
        public Cumplimiento CUMPL { get; set; }
        public ItemEstandar IEST { get; set; }
        [AllowHtml]
        public string CuAr_Content { get; set; }
        public string CuAr_Resource { get; set; }
        public string CuAr_Ext { get; set; }
    }
}