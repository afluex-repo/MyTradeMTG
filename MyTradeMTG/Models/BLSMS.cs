using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;

namespace MyTradeMTG
{
    static public class BLSMS
    {
        static public void SendSMS(string Mobile, string Message, string TempId)
        {
            try
            {
                string SMSAPI = ConfigurationSettings.AppSettings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile);
                SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);
                SMSAPI = SMSAPI.Replace("[TempId]", TempId);
                //SMSAPI = SMSAPI.Replace("[Date]", DateTime.Now.ToString());
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI, false));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
            }
            catch (Exception ex)
            {
            }
        }
        static public string ForgetPassword(string Name, string Password)
        {

            string Message = ConfigurationSettings.AppSettings["ForgetPassword"].ToString();


            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Password]", Password);
            return Message;


        }
        static public string KycApprovel(string Name, string kycstatus)
        {

            string Message = ConfigurationSettings.AppSettings["KycApprovel"].ToString();


            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[KYCSTATUS]", kycstatus);
            return Message;


        }
        static public string PayoutStatus(string Name, string payoutstatus)
        {

            string Message = ConfigurationSettings.AppSettings["PayoutStatus"].ToString();


            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[PAYOUTSTATUS]", payoutstatus);
            return Message;


        }
        static public string Payout(string Name, string Payout,string payoutNo,string Date)
        {

            string Message = ConfigurationSettings.AppSettings["Payout"].ToString();
            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Payout]", Payout);
            Message = Message.Replace("[PayoutNo]", payoutNo);
            Message = Message.Replace("[PayoutDate]", Date);
            return Message;


        }
        static public string Topup(string Name, string Amount)
        {

            string Message = ConfigurationSettings.AppSettings["Topup"].ToString();


            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Amount]", Amount);
            return Message;


        }
        static public string Wallet(string Name, string Status)
        {

            string Message = ConfigurationSettings.AppSettings["Wallet"].ToString();


            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Status]", Status);
            return Message;


        }
        static public string AdminPinGeneration(string Name, string Password)
        {

            string Message = ConfigurationSettings.AppSettings["AdminPinGeneration"].ToString();


            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Password]", Password);
            return Message;


        }
        static public string OTP(string Name, string OTP,string USEDFOR)
        {

            string Message = ConfigurationSettings.AppSettings["OTP"].ToString();


            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[OTP]", OTP);
            Message = Message.Replace("[USEDFOR]", USEDFOR);
            return Message;


        }
        static public string IdActivated(string Name, string Package)
        {

            string Message = ConfigurationSettings.AppSettings["IdActivated"].ToString();


            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Package]", Package);
            return Message;


        }
        static public string ProfileInfoedited(string Name, string OtpVerify)
        {

            string Message = ConfigurationSettings.AppSettings["ProfileInfoedited"].ToString();
            
            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[OtpVerify]", OtpVerify);
            return Message;


        }

    }
}
