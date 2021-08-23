using AccountingNote.DBSource;
using AccountingNote.ORM.DBModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    // 類別要盡量分門別類，DBSource為資料庫相關，Auth負責處理登入元件
    public class AuthManager
    {
        public static bool IsLogined()   //功能為單純檢查是否有登入
        {
            /// <summary>check is logined</summary> 
            /// <returns></returns>
            if (HttpContext.Current.Session["UserLoginInfo"] == null)   //封裝此行
                return false;
            else
                return true;
        }
    
        public static UserInfoModel GetCurrentUser()   //取得使用者相關的資訊
        {
            // 將原本直接讀取資料庫的程式，作封裝
            string account = HttpContext.Current.Session
                ["UserLoginInfo"] as string;

            if (account == null)
                return null;


            UserInfo userInfo = UserInfoManager.GetUserInfoByAccount_ORM(account);
            
            if (userInfo == null)
            {
                HttpContext.Current.Session
                    ["UserLoginInfo"] = null;
                return null;
            }
                
            UserInfoModel model = new UserInfoModel();
            model.ID = userInfo.ID;
            model.Account = userInfo.Account;
            model.Name = userInfo.Name;
            model.Email = userInfo.Email;

            return model;
        }

        ///<summary>登出鍵</summary>
        ///<return></return>
        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"]
                = null;
        }
    
        public static bool TryLogin(string account, string pwd, out string errorMsg)
        {
            // check 帳號密碼是否為空字串
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "Account / PWD is required";
                return false;
            }
            // 到DB查使用者資料
            UserInfo userInfo = UserInfoManager.GetUserInfoByAccount_ORM(account); //查詢資料庫是否有這筆資料

            // check null
            if (userInfo == null)
            {
                errorMsg = "Account doesn't exist";
                return false;
            }

            // check account / pwd
            if (string.Compare(userInfo.Account, account, true) == 0 &&
                string.Compare(userInfo.PWD, pwd, false) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = userInfo.Account; //現在的帳號寫進Session去

                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "Login fail. Please check Account / PWD.";
                return false;
            }
        }
    }
}
