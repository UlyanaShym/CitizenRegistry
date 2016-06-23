using System;
using System.Linq;

namespace Citizens.Tests.Helpers
{
    public class VatIdBuilder
    {
        private static DateTime startDateCounting = new DateTime(1899, 12, 31);
        private static int birthDateVatIdLength = 5;
        private static int serialNumberVatIdLength = 3;
        static Random rnd = new Random();


        private static string CalculateBirthDateVatIdValue(DateTime birthDate)
        {
            return (birthDate - startDateCounting).TotalDays.ToString().PadRight(birthDateVatIdLength, '0');
        }

        public static string CalculateSerialNumberVatVatIdValue()
        {
            int serialNumberVatId = rnd.Next(1, 999);

            return serialNumberVatId.ToString().PadLeft(serialNumberVatIdLength, '0');
        }

        private static string CalculateGenderVatIdValue(Gender gender)
        {
            int randomValue = gender == Gender.Male ? 2 * rnd.Next(1, 4) + 1 : 2 * rnd.Next(1, 4);

            return randomValue.ToString();
        }
        private static string CalculateLastVatIdValue(string vatId)
        {
            var intList = vatId.Select(digit => int.Parse(digit.ToString())).ToList();
            var sum = intList[0] * (-1) + intList[1] * 5 + intList[2] * 7 + intList[3] * 9 + intList[4] * 4 + intList[5] * 6 +
                      intList[6] * 10 + intList[7] * 5 + intList[8] * 7;

            return ((sum % 11) % 10).ToString();
        }

        public static string GenerateVatId(DateTime birthDate, string serialNumber, Gender gender)
        {
            var vatId = CalculateBirthDateVatIdValue(birthDate.Date) + serialNumber + CalculateGenderVatIdValue(gender);
            vatId += CalculateLastVatIdValue(vatId);

            return vatId;
        }
    }
}
