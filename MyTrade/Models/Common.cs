﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTrade.Models
{
    public class Common
    {
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string ReferBy { get; set; }
        public string Result { get; set; }
        public string DisplayName { get; set; }
        public string AddedOn { get; set; }
        public string Fk_UserId { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public static string GenerateRandom()
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                s = string.Concat(s, r.Next(10).ToString());
            }
            return s;
        }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            DateTime Dt;

            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }

        }
        public static List<SelectListItem> AssociateStatus()
        {
            List<SelectListItem> AssociateStatus = new List<SelectListItem>();
            AssociateStatus.Add(new SelectListItem { Text = "All", Value = null });
            AssociateStatus.Add(new SelectListItem { Text = "Active", Value = "P" });
            AssociateStatus.Add(new SelectListItem { Text = "Inactive", Value = "T" });
            AssociateStatus.Add(new SelectListItem { Text = "Blocked", Value = "B" });
            return AssociateStatus;
        }
        public static List<SelectListItem> LegType()
        {
            List<SelectListItem> Leg = new List<SelectListItem>();
            Leg.Add(new SelectListItem { Text = "All", Value = null });
            Leg.Add(new SelectListItem { Text = "Left", Value = "L" });
            Leg.Add(new SelectListItem { Text = "Right", Value = "R" });

            return Leg;
        }
        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberName", para);

            return ds;
        }
        public DataSet GetStateCity()
        {
            SqlParameter[] para = { 
                                      new SqlParameter("@PinCode", PinCode),
                                    
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);

            return ds;
        }
        public int GenerateRandomNo()
        {
            int _min = 0000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}