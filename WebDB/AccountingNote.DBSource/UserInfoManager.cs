using AccountingNote.ORM.DBModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class UserInfoManager
    {
        //public static string GetConnectionString()
        //{
        //    //連線時請再次確認連接的Web.config位置
        //    //<connectionStrings>< add name = "DefaultConnection"connectionString ="Data Source=.\SQLEXPRESS;Initial Catalog=AccountingNote
        //    //;Integrated Security = True; "/></ connectionStrings >
        //    //請注意Data Source的位置，有時會有一樣名稱的資料表，需確認資料庫位置!!!
        //    string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    return val; //回傳設定檔的值
        //}
        public static DataRow GetUserInfoByAccount(string account) //帶參數進來
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                @"SELECT [ID], [Account], [PWD], [Name], [Email]
                    FROM UserInfo
                    WHERE [Account] = @account
                ";

            using (SqlConnection connection = new SqlConnection(connectionString)) // 開啟連線
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection)) // 建立物件
                {

                    command.Parameters.AddWithValue("@account", account); // 參數化查詢

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();

                        if (dt.Rows.Count == 0) // 如果查不到id資料的話回傳null
                            return null;

                        DataRow dr = dt.Rows[0]; // id為主鍵且不允許重複，因此查第一筆即可
                        return dr;
                    }
                    catch (Exception ex)
                    {
                        logger.WriteLog(ex);
                        //Console.WriteLine(ex.ToString());
                        return null;
                    }
                }
            }
        }

        public static UserInfo GetUserInfoByAccount_ORM(string account)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query = (from item in context.UserInfoes
                                 where item.Account == account
                                 select item);
                    var obj = query.FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
                return null;
            }
        }
    }
}
