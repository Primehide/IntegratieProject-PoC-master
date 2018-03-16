using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain.Alert;
using Domain.Entiteit;
using Domain.Posts;

namespace BL
{
    public class AccountMgr : IAccountMgr
    {
        IAccountRepo repo;
        private UnitOfWorkManager uowManager;

        public AccountMgr()
        {

        }

        public AccountMgr(UnitOfWorkManager uofMgr)
        {
            uowManager = uofMgr;
            repo = new AccountRepo(uowManager.UnitOfWork);
        }

        public void genereerAlerts()
        {
            initNonExistingRepo(true);
            EntiteitMgr entiteitMgr = new EntiteitMgr(uowManager);
            List<Alert> Alerts = getAlleAlerts();
            Entiteit e;

            foreach (var Alert in Alerts)
            {
                e = Alert.Entiteit;
                if(entiteitMgr.berekenTrends(Alert.MinTrendWaarde, e, Alert.Type, Alert.voorwaarde))
                {
                    Alert.Triggered = true;
                    UpdateAlert(Alert);
                }
            }
            uowManager.Save();
        }

        public List<Alert> getAlleAlerts()
        {
            initNonExistingRepo();
            return repo.getAlleAlerts();
        }

        public void UpdateAlert(Alert alert)
        {
            initNonExistingRepo();
            repo.UpdateAlert(alert);
        }

        public void initNonExistingRepo(bool withUnitOfWork = false)
        {
            // Als we een repo met UoW willen gebruiken en als er nog geen uowManager bestaat:
            // Dan maken we de uowManager aan en gebruiken we de context daaruit om de repo aan te maken.

            if (withUnitOfWork)
            {
                if (uowManager == null)
                {
                    uowManager = new UnitOfWorkManager();
                    repo = new AccountRepo(uowManager.UnitOfWork);
                }
            }
            // Als we niet met UoW willen werken, dan maken we een repo aan als die nog niet bestaat.
            else
            {
                repo = (repo == null) ? new AccountRepo() : repo;
            }
        }

        public List<Alert> getAllertOnAccountId(int accountId)
        {
            return getAlleAlerts().Where(x => x.User.AccountId == accountId).ToList();
        }
    }
}
