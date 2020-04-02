using HBM.Data;
using HBM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Services
{
   public class DashboardService
    {
        HBMContext context = new HBMContext();
        public bool SavePicture(Picture picture)
        {
           
            context.Pictures.Add(picture);

            return context.SaveChanges() > 0;
        }

        public IEnumerable<Picture> GetPicturesByIDs(List<int> pictureIDs)
        {
            return pictureIDs.Select(x => context.Pictures.Find(x)).ToList();
        }
    }
}
