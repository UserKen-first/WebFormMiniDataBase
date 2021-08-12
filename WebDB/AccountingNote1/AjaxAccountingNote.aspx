<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxAccountingNote.aspx.cs" Inherits="AccountingNote1.AjaxAccountingNote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>使用 AJAX 更新 AccountingNote</title>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script>
        // 取得介面上輸入的資料
        $(function () {
            $("#btnSave").click(function () {
                var id = $("#hfID").val();
                var actType = $("#ddlActType").val();
                var amount = $("#txtAmount").val();
                var caption = $("#txtCaption").val();
                var desc = $("#txtDesc").val();

                // 新增或修改模式
                if (id) {
                    //再把Ajax發出去
                    $.ajax({
                        //取得要連結的地方
                        url: "http://localhost:60072/Handlers/AccountingNoteHandler.ashx?ActionName=Update",
                        type: "POST",
                        data: {
                            "ID": id,
                            "Caption": caption,
                            "Amount": amount,
                            "ActType": actType,
                            "Body": desc
                        },
                        success: function (result) {
                            alert("更新成功");
                        }
                    });
                }
                else {
                    $.ajax({
                        url: "http://localhost:65087/Handlers/AccountingNoteHandler.ashx?ActionName=Create",
                        type: "POST",
                        data: {
                            "Caption": caption,
                            "Amount": amount,
                            "ActType": actType,
                            "Body": desc
                        },
                        success: function (result) {
                            alert("新增成功");
                        }
                    });
                }
            });

            $("#btnRead").click(function () {
                $.ajax({
                    url: "http://localhost:60072/Handlers/AccountingNoteHandler.ashx?ActionName=Query",
                    type: "POST",
                    data: {
                        "ID": 1023,
                    },
                    success: function (result) {
                        $("#hfID").val(result["ID"]);
                        $("#ddlActType").val(result["ActType"]);
                        $("#txtAmount").val(result["Amount"]);
                        $("#txtCaption").val(result["Caption"]);
                        $("#txtDesc").val(result["Body"]);
                    }
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%-- 透過input hidden將流水帳的ID藏起來 --%>
        <input type="hidden" id="hfID" value="10" />
        <button type="button" id="btnRead">Read Data</button>
        <table>
            <tr>
                <td colspan="2">
                    <h1>流水帳管理系統 - 後台</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <%-- 這裡放主要內容 --%>
                    Type:
                    <select id="ddlActType">
                        <option value="0">支出</option>
                        <option value="1">收入</option>
                    </select>
                    <br />
                    金額:<input type="number" id="txtAmount" /><br />
                    主題:<input type="text" id="txtCaption" /><br />
                    描述:<textarea id="txtDesc" rows="5" cols="60"></textarea><br />
                    <button type="button" id="btnSave">Save</button>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
