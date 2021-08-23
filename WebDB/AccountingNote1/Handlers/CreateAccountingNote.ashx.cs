using AccountingNote.DBSource;
using AccountingNote.ORM.DBModel;
using AccountingNote1.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace AccountingNote1.Handlers
{
    /// <summary>
    /// CreateAccountingNote 的摘要描述
    /// </summary>
    public class CreateAccountingNote : IHttpHandler
        // 任務為取得Post的內容
    {
        public void ProcessRequest(HttpContext context)
        {
            //檢查輸入值為get還是post
            if(context.Request.HttpMethod != "POST")
            {
                this.ProcessError(context, "POST Only");
                return;
            }

            

            // 透過Post取得使用者輸入值
            string caption = context.Request.Form["Caption"];
            string amountText = context.Request.Form["Amount"];
            string actTypeText = context.Request.Form["ActType"];
            string body = context.Request.Form["Body"];

            // 先將id寫死 帳號 Admin
            string id = "4FCF6DBD-11D3-4EB1-8F74-4D08D287453C";
            // AJAX預設是無法取得Session的值
            
            // 必填檢查
            if(string.IsNullOrWhiteSpace(caption) ||
                string.IsNullOrWhiteSpace(amountText) ||
                string.IsNullOrWhiteSpace(actTypeText))
            {
                this.ProcessError(context, "caption, amount, actType is required");
                return;
            }

            // 轉型檢查
            int tempAmount, tempActType;
            if(!int.TryParse(amountText , out tempAmount) || 
                !int.TryParse(actTypeText, out tempActType))
            {
                this.ProcessError(context, "Amount, ActType should be an integer");
                return;
            }
            
            Accounting accounting = new Accounting()
            {
                UserID = id.ToGuid(),
                Caption = caption,
                Body = body,
                Amount = tempAmount,
                ActType = tempActType
            };
            // 建立流水帳
            // 檢查都完成後，將Post參數放入程式中，並將值傳進SQL
            AccountingManager.CreateAccounting(accounting);

            context.Response.ContentType = "text/plain";
            context.Response.Write("ok");
        }

        private void ProcessError(HttpContext context, string msg)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
            context.Response.End();
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