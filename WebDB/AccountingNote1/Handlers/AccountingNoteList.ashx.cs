using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using AccountingNote1.Models;

namespace AccountingNote1.Handlers
{
    /// <summary>
    /// AccountingNoteList 的摘要描述
    /// </summary>
    public class AccountingNoteList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // 取得傳進來是哪個使用者 透過Get來取
            string account = context.Request.QueryString["Account"];

            if (string.IsNullOrWhiteSpace(account))
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            var dr = UserInfoManager.GetUserInfoByAccount(account);  //透過帳號檢查使用者是否存在

            if (dr == null)
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            // 查詢這個使用者所有的流水帳
            string userID = dr["ID"].ToString();
            DataTable dataTable = AccountingManager.GetAccountingList(userID);  //透過使用者ID

            // 資料格式轉換成指定格式
            List<AccountnigNoteViewModel> list = new List<AccountnigNoteViewModel>();
            foreach (DataRow drAccounting in dataTable.Rows)
            {
                AccountnigNoteViewModel model = new AccountnigNoteViewModel()
                {
                    ID = drAccounting["ID"].ToString(),
                    Caption = drAccounting["Caption"].ToString(),
                    Amount = drAccounting.Field<int>("Amount"),
                    ActType =
                    (drAccounting.Field<int>("ActType") == 0) ? "支出" : "收入",
                    CreatDate = drAccounting.Field<DateTime>("CreateDate").ToString("yyyy-MM-dd")
                };

                list.Add(model);
        }
        // 序列化list這個容器即可
        string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);

        //序列化
        context.Response.ContentType = "application/json";
            context.Response.Write(jsonText);

            //context.Response.Write("Hello World");
        }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
}