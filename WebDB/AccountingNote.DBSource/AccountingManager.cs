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
    public class AccountingManager
    {
        public static DataTable GetAccountingList(string userID) //取得流水帳清單
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT
                            ID,
                            Caption,
                            Amount,
                            ActType,
                            CreateDate
                            FROM Accounting
                            WHERE UserID = @userID
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", userID));

            try
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// 查詢流水帳清單
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<Accounting> GetAccountingList(Guid userID)
        {
            try
            {
                // Guid.TryParse(userID, out Guid tempGUID); // 判斷是否為GUID
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Accountings
                         where item.UserID == userID
                         select item);
                    var list = query.ToList(); 
                                                //將資料由DataTable轉成List => IEnumberable
                                               // 不能回傳 query 因為只有這裡使用 ORM
                                               // 回到網站後就沒有地方使用 ORM 了，變成"物件"的東西就跟 ORM 沒關係了
                    return list;
                }
            }
            catch(Exception ex)
            {
                logger.WriteLog(ex);
                return null;
            }
        }
        public static Accounting GetAccounting(int id, Guid userID)  //查詢單筆
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.Accountings
                         where item.UserID == userID && item.ID == id
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
        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            // <<<<< check input >>>>>
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000.");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1.");
            // <<<<< check input >>>>>

            // 檢查傳進來的body參數
            string bodyColumnSQL = "";
            string bodyValueSQL = "";
            if (!string.IsNullOrWhiteSpace(body))
            {
                bodyColumnSQL = ", Body";
                bodyValueSQL = ", @Body";
            }

            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
            $@" INSERT INTO [dbo].[Accounting]
                (
                    UserID
                    ,Caption
                    ,Amount
                    ,ActType
                    ,CreateDate
                    {bodyColumnSQL}
                )
                    VALUES
                (
                    @userID
                    ,@caption
                    ,@amount
                    ,@actType
                    ,@createDate
                    {bodyValueSQL}
                ) ";

            // connect db & execute
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@userID", userID);
                    comm.Parameters.AddWithValue("@caption", caption);
                    comm.Parameters.AddWithValue("@amount", amount);
                    comm.Parameters.AddWithValue("@actType", actType);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now);

                    if (!string.IsNullOrWhiteSpace(body))
                        comm.Parameters.AddWithValue("@body", body);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        logger.WriteLog(ex);
                    }
                }
            }
        }
        public static void CreateAccounting(Accounting accounting) //外部只要讀物件就好
        {

            if (accounting.Amount < 0 || accounting.Amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000.");

            if (accounting.ActType < 0 || accounting.ActType > 1)
                throw new ArgumentException("ActType must be 0 or 1.");
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    accounting.CreateDate = DateTime.Now;
                    context.Accountings.Add(accounting);
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
                return;
            }
        }
        public static bool UpdateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        {
            // <<<<< check input >>>>>
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000.");

            if (actType < 0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1.");
            // <<<<< check input >>>>>

            // 檢查傳進來的body參數
            string bodyColumnSQL = "";
            string bodyValueSQL = "";
            if (!string.IsNullOrWhiteSpace(body))
            {
                bodyColumnSQL = ", Body";
                bodyValueSQL = " @body";
            }

            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@"UPDATE [Accounting]
                   SET
                       UserID = @userID
                       ,Caption = @caption
                       ,Amount = @amount
                       ,ActType = @actType
                       ,CreateDate = @createDate
                       {bodyColumnSQL} = {bodyValueSQL}
                    WHERE
                        ID = @id  ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@userID", userID));
            paramList.Add(new SqlParameter("@caption", caption));
            paramList.Add(new SqlParameter("@amount", amount));
            paramList.Add(new SqlParameter("@actType", actType));
            paramList.Add(new SqlParameter("@createDate", DateTime.Now));
            if (!string.IsNullOrWhiteSpace(body))
                paramList.Add(new SqlParameter("@body", body));
            paramList.Add(new SqlParameter("@id", ID));
            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, paramList);

                if (effectRows == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex); //利用logger存ex而不要用console
                return false;
            }
        }
        public static bool UpdateAccounting(Accounting accounting)
        {
            if (accounting.Amount < 0 || accounting.Amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1,000,000.");

            if (accounting.ActType < 0 || accounting.ActType > 1)
                throw new ArgumentException("ActType must be 0 or 1.");
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbobject =
                        context.Accountings.Where(obj => obj.ID
                        == accounting.ID).FirstOrDefault();

                    if (dbobject != null)
                    {
                        dbobject.Caption = accounting.Caption;
                        dbobject.Body = accounting.Body;
                        dbobject.Amount = accounting.Amount;
                        dbobject.ActType = accounting.ActType;

                        context.SaveChanges();

                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
                return false;
            }
        }
        public static void DeleteAccout(int ID)
        {
            string connectionString = DBHelper.GetConnectionString();
            string dbCommandString =
                 @" DELETE
	                    [Accounting]
	                WHERE
	                    ID = @id
                ";
            List<SqlParameter> paramList = new List<SqlParameter>();  //參數往外挪
            paramList.Add(new SqlParameter("@id", ID)); //comm.Parameters.AddWithValue("@id", id);

            try
            {
                DBHelper.ModifyData(connectionString, dbCommandString, paramList);
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
            }
        }
        public static void DeleteAccounting_ORM(int ID)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var dbobject =
                        context.Accountings.Where(obj => obj.ID
                        == ID).FirstOrDefault();
                    if(dbobject != null)
                    {
                        context.Accountings.Remove(dbobject);
                        context.SaveChanges();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
            }
        }
    }
}
