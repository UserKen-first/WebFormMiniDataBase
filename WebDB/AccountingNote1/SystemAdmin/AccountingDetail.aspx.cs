using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class AccountingDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (this.Session["UserLoginInfo"] == null)
            if(!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            string account = this.Session["UserLoginInfo"] as string;
            var currentUser = AuthManager.GetCurrentUser();

            if (currentUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }
            if (!this.IsPostBack)
            {
                //check is create made or edit mode
                if (this.Request.QueryString["ID"] == null)
                {
                    this.btnDel.Visible = false;
                }
                else
                {
                    this.btnDel.Visible = true;
                    
                    string idText = this.Request.QueryString["ID"];  //由網址頁取得ID
                    int id;
                    if (int.TryParse(idText, out id))  //確認能否轉型成數字
                    {
                        var drAccounting = AccountingManager.GetAccounting(id, currentUser.ID);  //再轉型成內容
                        
                        if (drAccounting == null) //點了超連結進來
                        {
                            this.Itmsg.Text = "Data doesn't exit";
                            this.btnsave.Visible = false;
                            this.btnDel.Visible = false;
                        }
                        else
                        {
                            this.ddlActType.SelectedValue = drAccounting["ActType"].ToString();
                            this.TxtAmount.Text = drAccounting["Amount"].ToString();
                            this.TxtCap.Text = drAccounting["caption"].ToString();
                            this.TxtDesc.Text = drAccounting["Body"].ToString();
                        }
                    }
                    else
                    {
                        this.Itmsg.Text = "ID is Required!";
                        this.btnsave.Visible = false;
                        this.btnDel.Visible = false;
                    }
                }
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))  //如果沒檢查到錯誤就不會執行
            {
                // 假設檢查失敗就執行下列
                this.Itmsg.Text = string.Join("<br/>", msgList); //執行字串結合
                return; //停掉程式
            }
            //取值

            UserInfoModel currentUser = AuthManager.GetCurrentUser();

            //string account = this.Session["UserLoginInfo"] as string;
            //var dr = UserInfoManager.GetUserInfoByAccount(account);

            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            //通過即一個一個取得輸入值
            string userID = currentUser.ID;
            string actTypeText = this.ddlActType.SelectedValue; //需要注意轉型
            string amountText = this.TxtAmount.Text;    //需要注意轉型
            string caption = this.TxtCap.Text;
            string body = this.TxtDesc.Text;

            int amount = Convert.ToInt32(amountText);
            int actType = Convert.ToInt32(actTypeText);

            string idText = this.Request.QueryString["ID"];  //由網址頁取得ID

            if (string.IsNullOrWhiteSpace(idText))
            {
                //新增模式
                // Execute 'Insert into db'
                AccountingManager.CreateAccounting(userID, caption, amount, actType, body);
            }
            else
            {
                //編輯修改模式
                int id;
                if (int.TryParse(idText, out id))
                {
                    // Execute 'update db'
                    // 如果是修改模式 如何拿到使用者id?  ==> 使用Session取得現在登入的是誰，再取得其userID
                    AccountingManager.UpdateAccounting(id, userID,  caption,  amount,  actType,  body);
                }
            }
            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }
        private bool CheckInput(out List<string> errorMsgList)  //out字串 回傳錯誤訊息 //回傳值為布林，代表通過或不通過
        {
            List<string> msgList = new List<string>();   //字串清單存放提示文字
            
            // Type檢查
            if (this.ddlActType.SelectedValue != "0"
                && this.ddlActType.SelectedValue != "1")
            {
                msgList.Add("Type must be 0 or 1");
            }
            //Amount檢查是否為空字串
            if (string.IsNullOrWhiteSpace(this.TxtAmount.Text))
            {
                msgList.Add("Amount is required");
            }
            else
            {
                //如果不是空字串就轉型成整數
                int tempInt;
                if(!int.TryParse(this.TxtAmount.Text, out tempInt))
                {
                    msgList.Add("Amount must be a number."); //如果轉換失敗就出現提示文字
                }
            
                if(tempInt < 0 || tempInt > 1000000)
                {
                    msgList.Add("Amount is out of range");
                }
            }
            errorMsgList = msgList;  //因為使用了out所以一定要初始化

            if (msgList.Count == 0)  //沒有任何錯誤訊息
                return true;
            else
                return false;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            string idtext = this.Request.QueryString["ID"];
           
            if (string.IsNullOrWhiteSpace(idtext))
                return;
            
            // if 我用string id 刪除，容易透過client端修改造成程式問題
            int id;   //確認方法的指定型別 
            if(int.TryParse(idtext, out id))
            {
                AccountingManager.DeleteAccout(id);
            }
                Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }
    }
}