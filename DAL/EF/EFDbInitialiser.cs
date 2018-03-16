using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entiteit;
using Domain.Account;
using Domain.Alert;

namespace DAL.EF
{
    internal class EFDbInitialiser : DropCreateDatabaseIfModelChanges<EFContext>
    {
        protected override void Seed(EFContext context)
        {
            base.Seed(context);
            Persoon p = new Persoon()
            {
                Voornaam = "Imade",
                Achternaam = "Annouri",
            };

            Persoon p2 = new Persoon()
            {
                Voornaam = "Annick",
                Achternaam = "De Ridder"
            };

            Account a1 = new Account()
            {
                Alerts = new List<Domain.Alert.Alert>(),
                Naam = "Sander"
            };

            Account a2 = new Account()
            {
                Alerts = new List<Domain.Alert.Alert>(),
                Naam = "Stijn"
            };

            //zal WEL getriggered worden
            Alert al1 = new Alert()
            {
                MinTrendWaarde = 3,
                Type = TrendType.STIJGEND,
                voorwaarde = Voorwaarde.AANTALVERMELDINGEN,
                Entiteit = p,
                PlatformType = PlatformType.EMAIL
            };

            //zal NIET getriggered worden
            Alert al2 = new Alert()
            {
                MinTrendWaarde = 30,
                Type = TrendType.DALEND,
                voorwaarde = Voorwaarde.AANTALVERMELDINGEN,
                Entiteit = p,
                PlatformType = PlatformType.WEB
            };

            Alert al3 = new Alert()
            {
                MinTrendWaarde = 0,
                Type = TrendType.STERKOPWAARDS,
                voorwaarde = Voorwaarde.AANTALVERMELDINGEN,
                Entiteit = p,
                PlatformType = PlatformType.ANDROID
            };

            //alerts toevoegen aan accounts
            a1.Alerts.Add(al1);
            a1.Alerts.Add(al2);
            a2.Alerts.Add(al3);

            //entiteiten toevoegen
            context.Entiteiten.Add(p);
            context.Entiteiten.Add(p2);

            //accounts toevoegen
            context.Accounts.Add(a1);
            context.Accounts.Add(a2);

            context.SaveChanges();
        }
    }
}
