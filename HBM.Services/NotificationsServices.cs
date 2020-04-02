using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
    public class NotificationsServices
    {
        HBMContext context = new HBMContext();
        public IEnumerable<Notifications> GetAllNotifications()
        {

            return context.Notifications.ToList();
        }
        public IEnumerable<Notifications> SearchNotifications(string searchTerm, int page, int recordSize)
        {
            var notifications = context.Notifications.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                notifications = notifications.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var skip = (page - 1) * recordSize;

            return notifications.OrderBy(x => x.Date).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchNotificationsCount(string searchTerm)
        {
            var notifications = context.Notifications.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                notifications = notifications.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return notifications.Count();
        }

        public Notifications GetNotificationsByID(int ID)
        {
            return context.Notifications.Find(ID);
        }


        public bool SaveNotifications(Notifications notifications)
        {
            context.Notifications.Add(notifications);

            return context.SaveChanges() > 0;
        }

        public bool UpdateNotifications(Notifications notifications)
        {
            context.Entry(notifications).State = System.Data.Entity.EntityState.Modified;

            var exitingBudgets = context.Notifications.Find(notifications.ID);

            context.Entry(exitingBudgets).CurrentValues.SetValues(notifications);

            return context.SaveChanges() > 0;
        }

        public bool DeleteNotifications(Notifications notifications)
        {
            context.Entry(notifications).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }


    }
}
