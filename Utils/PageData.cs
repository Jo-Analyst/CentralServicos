﻿using DataBase;
using System;

namespace Interface
{
    internal class PageData
    {
        static public double quantityRowsSelected { get; set; }

        static public double quantity;

        static public int SetPageQuantityUsers()
        {
            quantity = User.CountQuantityUsers();
            return CalculateNumberOfPage();
        }


        static public int SetPageQuantityServices(int userId = 0)
        {
            quantity = Service.CountQuantityServicesByUserId(userId);
            return CalculateNumberOfPage();
        }

        static public int SetPageQuantityServicesByDate(string year)
        {
            quantity = Service.CountQuantityServicesByYear(year);
            return CalculateNumberOfPage();
        }

        //static public int SetPageQuantityServicesAll()
        //{
        //    quantity = PaefiService.CountQuantityServicesAll();
        //    return CalculateNumberOfPage();
        //}

        //static public int SetPageQuantityServicesByPeriod(string month, string year)
        //{
        //    quantity = PaefiService.CountQuantityServicesByPeriod(month,
        //        year);
        //    return CalculateNumberOfPage();
        //}

        static public int SetPageQuantityUsersByName(string name)
        {
            quantity = User.CountQuantityUsersByName(name);
            return CalculateNumberOfPage();
        }

        ////static public int SetPageQuantityServices(int personId)
        ////{
        ////    quantity = Service.CountQuantityServices(personId);
        ////    return CalculacalculateNumberOfPage();
        ////}

        static private int CalculateNumberOfPage()
        {
            double result = quantity / quantityRowsSelected;
            return (int)Math.Ceiling(result);
        }
    }
}
