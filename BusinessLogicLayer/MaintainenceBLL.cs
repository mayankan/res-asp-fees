using CommunicationLayer;
using Data_Link_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class MaintainenceBLL
    {
        //Instantiating the Rainbow SQL server Entities.
        RAINBOWEntities dbcontext = new RAINBOWEntities();
        public string updateNote(MaintainenceCL homeNote)
        {
            Maintainence query = (from x in dbcontext.Maintainences select x).FirstOrDefault();
            query.HomeNote = homeNote.homeNote;
            dbcontext.SaveChanges();
            return query.HomeNote;
        }
        public bool updateOffline(MaintainenceCL offlineStatus)
        {
            Maintainence query = (from x in dbcontext.Maintainences select x).FirstOrDefault();
            query.isOffline = offlineStatus.isOffline;
            dbcontext.SaveChanges();
            return query.isOffline;
        }
        public MaintainenceCL getStatus()
        {
            Maintainence query = (from x in dbcontext.Maintainences select x).FirstOrDefault();
            MaintainenceCL returnStatus = new MaintainenceCL()
            {
                homeNote = query.HomeNote,
                isOffline = query.isOffline,
            };
            return returnStatus;
        }
    }
}
