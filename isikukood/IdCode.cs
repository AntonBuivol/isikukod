﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isikukood
{
    public class IdCode
    {
        private readonly string _idCode;

        public IdCode(string idCode)
        {
            _idCode = idCode;
        }

        private bool IsValidLength()
        {
            return _idCode.Length == 11;
        }

        private bool ContainsOnlyNumbers()
        {
            // return _idCode.All(Char.IsDigit);

            for (int i = 0; i < _idCode.Length; i++)
            {
                if (!Char.IsDigit(_idCode[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private int GetGenderNumber()
        {
            return Convert.ToInt32(_idCode.Substring(0, 1));
        }

        private bool IsValidGenderNumber()
        {
            int genderNumber = GetGenderNumber();
            return genderNumber > 0 && genderNumber < 7;
        }

        private int Get2DigitYear()
        {
            return Convert.ToInt32(_idCode.Substring(1, 2));
        }

        public int GetFullYear()
        {
            int genderNumber = GetGenderNumber();
            // 1, 2 => 18xx
            // 3, 4 => 19xx
            // 5, 6 => 20xx
            return 1800 + (genderNumber - 1) / 2 * 100 + Get2DigitYear();
        }

        private int GetMonth()
        {
            return Convert.ToInt32(_idCode.Substring(3, 2));
        }

        private bool IsValidMonth()
        {
            int month = GetMonth();
            return month > 0 && month < 13;
        }

        private static bool IsLeapYear(int year)
        {
            return year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
        }
        private int GetDay()
        {
            return Convert.ToInt32(_idCode.Substring(5, 2));
        }

        private bool IsValidDay()
        {
            int day = GetDay();
            int month = GetMonth();
            int maxDays = 31;
            if (new List<int> { 4, 6, 9, 11 }.Contains(month))
            {
                maxDays = 30;
            }
            if (month == 2)
            {
                if (IsLeapYear(GetFullYear()))
                {
                    maxDays = 29;
                }
                else
                {
                    maxDays = 28;
                }
            }
            return 0 < day && day <= maxDays;
        }

        private int CalculateControlNumberWithWeights(int[] weights)
        {
            int total = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                total += Convert.ToInt32(_idCode.Substring(i, 1)) * weights[i];
            }
            return total;
        }

        private bool IsValidControlNumber()
        {
            int controlNumber = Convert.ToInt32(_idCode[^1..]);
            int[] weights = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            int total = CalculateControlNumberWithWeights(weights);
            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }
            // second round
            int[] weights2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };
            total = CalculateControlNumberWithWeights(weights2);
            if (total % 11 < 10)
            {
                return total % 11 == controlNumber;
            }
            // third round, control number has to be 0
            return controlNumber == 0;
        }

        public bool IsValid()
        {
            return IsValidLength() && ContainsOnlyNumbers()
                && IsValidGenderNumber() && IsValidMonth()
                && IsValidDay()
                && IsValidControlNumber();
        }

        public DateOnly GetBirthDate()
        {
            int day = GetDay();
            int month = GetMonth();
            int year = GetFullYear();
            return new DateOnly(year, month, day);
        }


        public void Age(IdCode id)
        {
            DateOnly BirthDate = id.GetBirthDate();
            DateTime DateNow = DateTime.Today;
            int age = DateNow.Year - BirthDate.Year;
            Console.WriteLine(age + " aastad vana");
        }
        public string Gender(string id)
        {
            string genedrnum = id.Substring(0,1);
            int num = Convert.ToInt32(genedrnum);
            string gender = "";

            if(num % 2 == 0)
            {
                gender = "Naine";
            }
            else
            {
                gender = "Mees";
            }
            Console.WriteLine(gender);
            return gender;
        }
        public static string BirthPlace(string id)
        {
            string placenum = id.Substring(7,3);
            int num = Convert.ToInt32(placenum);
            string place = "";
            if (001<=num && num<=010)
            {
                place = "Kuressaare haigla";
            }
            else if (011 <= num && num <= 019)
            {
                place = "Tartu Ülikooli Naistekliinik";
            }
            else if (021 <= num && num <= 150)
            {
                place = "Ida-Tallinna keskhaigla, Pelgulinna sünnitusmaja (Tallinn)";
            }
            else if (151 <= num && num <= 160)
            {
                place = "Keila haigla";
            }
            else if (161 <= num && num <= 220)
            {
                place = "Rapla haigla, Loksa haigla, Hiiumaa haigla (Kärdla)";
            }
            else if (221 <= num && num <= 270)
            {
                place = "Ida-Viru keskhaigla (Kohtla-Järve, endine Jõhvi)";
            }
            else if (271 <= num && num <= 370)
            {
                place = "Maarjamõisa kliinikum (Tartu), Jõgeva haigla";
            }
            else if (371 <= num && num <= 420)
            {
                place = "Narva haigla";
            }
            else if (421 <= num && num <= 470)
            {
                place = "Pärnu haigla";
            }
            else if (471 <= num && num <= 490)
            {
                place = "Haapsalu haigla";
            }
            else if (491 <= num && num <= 520)
            {
                place = "Järvamaa haigla (Paide)";
            }
            else if (521 <= num && num <= 570)
            {
                place = "Rakvere haigla, Tapa haigla";
            }
            else if (571 <= num && num <= 600)
            {
                place = "Valga haigla ";
            }
            else if (601 <= num && num <= 650)
            {
                place = "Viljandi haigla";
            }
            else if (651 <= num && num <= 700)
            {
                place = "Lõuna-Eesti haigla (Võru), Põlva haigla ";
            }
            else
            {
                place= "Mitte Eestist";
            }

            Console.WriteLine(place);

            return place;
        }
    }
}
