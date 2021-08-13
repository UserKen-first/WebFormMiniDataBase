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
                        url: "http://localhost:60072/Handlers/AccountingNoteHandler.ashx?ActionName=Create",
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

            /* $("#btnReadData").click(function () {*/
            /*$(".btnReadData").bind("click", function ({*/
            $(document).on("click", ".btnReadData", function () {   //(事件觸發方式, 事件觸發者, 觸發後的回應)
                var td = $(this).closest("td");           // 給一個選擇器  找到最接近自己的td
                var hf = td.find("input:hidden.hfRowID");     // 透過找到的td裡面的值 ("標籤: 種類.ID")

                var rowID = hf.val();                     // rowID存取hidden Value => 流水帳id

                $.ajax({
                    url: "http://localhost:60072/Handlers/AccountingNoteHandler.ashx?ActionName=Query",
                    type: "POST",
                    data: {
                        "ID": rowID,
                    },
                    success: function (result) {
                        $("#hfID").val(result["ID"]);
                        $("#ddlActType").val(result["ActType"]);
                        $("#txtAmount").val(result["Amount"]);
                        $("#txtCaption").val(result["Caption"]);
                        $("#txtDesc").val(result["Body"]);

                        $("#divEditor").show(1000);
                    }
                });
            });

            $("#btnCreate").click(function () {
                $("#hfID").val('');
                $("#ddlActType").val('');
                $("#txtAmount").val('');
                $("#txtCaption").val('');
                $("#txtDesc").val('');

                $("#divEditor").show(1000);
            });

            $("#btnCancel").click(function () {
                $("#hfID").val('');
                $("#ddlActType").val('');
                $("#txtAmount").val('');
                $("#txtCaption").val('');
                $("#txtDesc").val('');

                $("#divEditor").hide(1000);
            });

            $("#btnDelete").click(function () {
                $("#hfID").val('');
                $("#ddlActType").val('');
                $("#txtAmount").val('');
                $("#txtCaption").val('');
                $("#txtDesc").val('');

                $("#divEditor").show(1000);
            });

            $("#divEditor").hide();

                $.ajax({
                    url: "http://localhost:60072/Handlers/AccountingNoteHandler.ashx?ActionName=List",
                    type: "GET",
                    data: {},
                    success: function (result) {
                        var table =
                            '<table border="1">';
                        table += '<tr> <th>Caption</th> <th>Amount</th> <th>ActType</th> <th>CreateDate</th > <th>Act</th> </tr>';

                        for (var i = 0; i < result.length; i++) {
                            var obj = result[i];

                            // 字串補差點的寫法
                            var htmlText =
                                `<tr>
                                <td>${obj.Caption}</td>
                                <td>${obj.Amount}</td>
                                <td>${obj.ActType}</td>
                                <td>${obj.CreateDate}</td>
                                <td>
                                    <input type="hidden" class="hfRowID" value="${obj.ID}" />
                                    <button type="button" class="btnReadData">
                                    <img src="Img/search.png" width="20" height="20" />
                                    </button>
                                </td>
                            </tr>`;
                            table += htmlText;
                        }

                        table += "</table>";
                        $("#divAccountingList").append(table);
                    }
                });
            });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%-- 透過input hidden將流水帳的ID藏起來 --%>
        <input type="hidden" id="hfID" value="10" />
        <div id="divEditor">
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
                        <button type="button" id="btnCancel">Cancel</button>
                        <button type="button" id="btnDelete">Delete</button>
                    </td>
                </tr>
            </table>
        </div>
        <button type="button" id="btnCreate">Create</button>
        <div id="divAccountingList"></div>
    </form>
</body>
</html>
