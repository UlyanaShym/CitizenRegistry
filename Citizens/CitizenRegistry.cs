using Humanizer;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using Citizens.Tests.Helpers;

namespace Citizens
{
    public class CitizenRegistry : ICitizenRegistry
    {
        private List<ICitizen> citizens = new List<ICitizen>();


        public void Register(ICitizen citizen)
        {
            if (String.IsNullOrEmpty(citizen.VatId))
            {
                string serialNumber = VatIdBuilder.CalculateSerialNumberVatVatIdValue();
                citizen.VatId = VatIdBuilder.GenerateVatId(citizen.BirthDate, serialNumber, citizen.Gender);
            }
            if(citizens.FirstOrDefault(c => c.VatId == citizen.VatId) != null)
            {
                throw new InvalidOperationException();
            }
            Citizen citizenCopy = citizen.Clone() as Citizen;
            citizens.Add(citizenCopy);
        }

        public ICitizen this[string id]
        {
            get
            {
                if (id == null)
                {
                    throw new ArgumentNullException();
                }
                return citizens.Find(s => s.VatId == id);
            }
        }

        public string Stats()
        {
            int menCount = 0;
            int womenCount = 0;

            foreach (var sitizen in citizens)
            {
                if (sitizen.Gender == Gender.Female)
                    womenCount++;
                else
                    menCount++;
            }
            string lastRegistrationDate = (womenCount > 0 || menCount > 0)
                ? ". Last registration was " + citizens.MaxBy(s => s.BirthDate).BirthDate.Humanize()
                : String.Empty;
            string manString = menCount > 0 ? "man" : "men";
            string womanString = womenCount > 0 ? "woman" : "women";

            return String.Format("{0} {1} and {2} {3}{4}", menCount, manString, womenCount, womanString, lastRegistrationDate);
        }

        public static Citizen GetCitizenByVatId(Citizen[] citizens, string id)
        {
            return citizens.FirstOrDefault(citizen => citizen.VatId == id);
        }
    }
}
