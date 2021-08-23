using AccountingNote.DBSource;
using AccountingNote.ORM.DBModel;
using AccountingNote1.Extensions;
using AccountingNote1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccountingNote1.Handlers
{
    /// <summary>
    /// AccountingNoteHandler 的摘要描述
    /// </summary>
    public class AccountingNoteHandler : IHttpHandler
    {
        // 當成一個進入點
        public void ProcessRequest(HttpContext context)
        {
            // 使用者的參數進來 決定要用甚麼東西(增、刪、修)
            // 使用get方法，取得ActionName
            string actionName = context.Request.QueryString["ActionName"];
            if (string.IsNullOrEmpty(actionName))
            {
                this.ProcessError(context, "ActionName can't be null.");
                return;
            }

            // 取得ActionName確認是否為Create
            if (actionName == "Create")
            {
                string caption = context.Request.Form["Caption"];
                string amountText = context.Request.Form["Amount"];
                string actTypeText = context.Request.Form["ActType"];
                string body = context.Request.Form["Body"];

                string id = "4FCF6DBD-11D3-4EB1-8F74-4D08D287453C";

                // 必填檢查
                if (string.IsNullOrWhiteSpace(caption) ||
                    string.IsNullOrWhiteSpace(amountText) ||
                    string.IsNullOrWhiteSpace(actTypeText))
                {
                    this.ProcessError(context, "caption, amount, actType is required");
                    return;
                }

                // 轉型檢查
                int tempAmount, tempActType;
                if (!int.TryParse(amountText, out tempAmount) ||
                    !int.TryParse(actTypeText, out tempActType))
                {
                    this.ProcessError(context, "Amount, ActType should be an integer");
                    return;
                }
                try
                {
                    // 建立流水帳
                    Accounting accounting = new Accounting()
                    {
                        UserID = id.ToGuid(),
                        Caption = caption,
                        Body = body,
                        Amount = tempAmount,
                        ActType = tempActType
                    };
                    // 檢查都完成後，將Post參數放入程式中，並將值傳進SQL
                    AccountingManager.CreateAccounting(accounting);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Creaete Succeed");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 503;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("ERROR HAPPENED");
                }
            }

            // 取得ActionName確認是否為Update
            else if (actionName == "Update")
            {
                string caption = context.Request.Form["Caption"];
                string amountText = context.Request.Form["Amount"];
                string actTypeText = context.Request.Form["ActType"];
                string body = context.Request.Form["Body"];
                string idText = context.Request.Form["ID"];

                string UserId = "4FCF6DBD-11D3-4EB1-8F74-4D08D287453C";

                // 必填檢查
                if (string.IsNullOrWhiteSpace(caption) ||
                    string.IsNullOrWhiteSpace(amountText) ||
                    string.IsNullOrWhiteSpace(actTypeText) ||
                    string.IsNullOrWhiteSpace(idText))
                {
                    this.ProcessError(context, "caption, amount, actType and ID is required");
                    return;
                }

                // 轉型檢查
                int tempAmount, tempActType, tempID;
                if (!int.TryParse(amountText, out tempAmount) ||
                    !int.TryParse(actTypeText, out tempActType) ||
                    !int.TryParse(idText, out tempID))
                {
                    this.ProcessError(context, "Amount, ActType and ID should be an integer");
                    return;
                }
                
                Accounting accounting = new Accounting()
                {
                    UserID = UserId.ToGuid(),
                    ID = tempID,
                    ActType = tempActType,
                    Amount = tempAmount,
                    Caption = caption,
                    Body = body
                };
                AccountingManager.UpdateAccounting(accounting);
                context.Response.ContentType = "text/plain";
                context.Response.Write("Update Succeed");
            }

            else if (actionName == "Delete")
            {
                string idText = context.Request.Form["ID"];
                int id;
                int.TryParse(idText, out id);

                AccountingManager.DeleteAccounting_ORM(id);
                context.Response.ContentType = "text/plain";
                context.Response.Write("Delete Succeed");
            }

            else if (actionName == "Query")
            {
                string idText = context.Request.Form["ID"];
                int id;
                int.TryParse(idText, out id);

                Guid userGUID = new Guid("4FCF6DBD-11D3-4EB1-8F74-4D08D287453C");
                var accounting = AccountingManager.GetAccounting(id, userGUID);

                if (accounting == null)
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("No Data:" + idText);
                    context.Response.End();
                    return;
                }
                AccountnigNoteViewModel model = new AccountnigNoteViewModel()
                {
                    ID = accounting.ID.ToString(),
                    Caption = accounting.Caption,
                    Body = accounting.Body,
                    Amount = accounting.Amount,
                    ActType = (accounting.ActType == 0) ? "支出" : "收入",
                    CreateDate = accounting.CreateDate.ToString("yyyy-MM-dd")
                };

                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }

            else if (actionName == "List")
            {
                Guid userGUID = new Guid("4FCF6DBD-11D3-4EB1-8F74-4D08D287453C");


                //DataTable dataTable = AccountingManager.GetAccountingList(userID);  //透過使用者ID

                // 資料格式轉換成指定格式

                List<Accounting> sourceList = AccountingManager.GetAccountingList(userGUID);

                List<AccountnigNoteViewModel> list = sourceList.Select(obj => new AccountnigNoteViewModel()
                {
                    ID = obj.ID.ToString(),
                    Caption = obj.Caption,
                    Amount = obj.Amount,
                    ActType = (obj.ActType == 0) ? "支出" : "收入",
                    CreateDate = obj.CreateDate.ToString("yyyy-MM-dd")
                }).ToList();


                // 序列化list這個容器即可
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);

                //序列化
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);
            }
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