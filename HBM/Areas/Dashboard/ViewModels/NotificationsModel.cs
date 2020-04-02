using HBM.Entities;
using HBM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBM.Areas.Dashboard.ViewModels
{
    public class NotificationsListingModel
    {
        public IEnumerable<Notifications> Notifications { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }


    public class NotificationsActionModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}