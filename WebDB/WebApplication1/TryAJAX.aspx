<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryAJAX.aspx.cs" Inherits="WebApplication1.TryAJAX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script>
        $(function () {
            // check hidden field

            var domWeatherData = $("#hfWeatherData").val();
            // 做狀態保留
            if (domWeatherData) {                          //如果有值的情況下
                try {
                    var wData = JSON.parse(domWeatherData) //把一個JSON字串轉換成JavaScript的數值或是物件

                    $("#spanLoc").text(wData["Name"]);
                    $("#spanTemp").text(wData["T"]);
                    $("#spanPop").text(wData["Pop"]);      //.text方法為純粹塞字進去
                }
                catch {
                    $("#spanLoc").text("-");
                    $("#spanTemp").text("-");
                    $("#spanPop").text("-");
                }
            }
            // button click
            $("#btn1").click(function () {

                var acc = $("#text1").val();
                var pwd = $("#pwd1").val();
                var url = "WeatherDataHandler.ashx?account=" + acc;   //JS輸入QS的值裝進變數url中

                $.ajax({                                  //由瀏覽器送出QueryString變數給伺服器 //由Get傳參數給Server
                    url: url,                             //url的值不寫死，動態輸入
                    type: "Post",                         //由post將表單元素取出後，放入data再輸出至頁面
                    data: {
                        "Password": pwd                   //data的coulumn Name由後端輸入
                    },
                    success: function (result) {          //在此的result還是一個物件

                        var txt = JSON.stringify(result)  //將JSON取到的內容做字串化
                        //$("#div1").html(txt);
                        $("#hfWeatherData").val(txt);     //用val的方法將txt的值丟進Hidden field
                        
                        $("#spanLoc").text(result["Name"]);
                        $("#spanTemp").text(result["T"]);
                        $("#spanPop").text(result["Pop"]);
                    }
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%-- 透過AJAX與Handlers溝通，取得值再輸出到頁面 --%>
        Account：<input type="text" id="text1" />
        PassWord：<input type="password" id="pwd1" />
        
        <%-- Client端的Button不會觸發後端Server的事件 --%>
        <button type="button" id="btn1">Click Me</button> 
        <%-- Client端事件 --%>
        <div id="div1">
            <%-- 不是直接打開伺服器的檔案，透過瀏覽器把伺服器上的資料下載下來
                到了本機打開，再放到瀏覽器上--%>
            --
        </div>
        <asp:HiddenField ID="hfWeatherData" runat="server" value=""/>
        <%-- HiddenField用來做伺服器端的狀態保留 --%>
        <table border="1">
            <tr>
                <th>地點 </th>
                <td>
                     <span id="spanLoc">=</span>
                </td>
            </tr>
            <tr>
                <th>溫度</th>
                <td>
                    <span id="spanTemp">=</span>
                </td>
            </tr>
            <tr>
                <th>降雨量</th>
                <td>
                    <span id="spanPop">=</span>
                </td>
            </tr>
        </table>
        <asp:Literal ID="ltlMsg" runat="server">1</asp:Literal>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <%-- 伺服器的Button事件不會影響到瀏覽器的資訊 --%>
    </form>
</body>
</html>
