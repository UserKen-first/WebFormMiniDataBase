<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryJQuery2.aspx.cs" Inherits="WebApplication1.TryJQuery2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            //$(".pp").click(function () {
            //    $(this).hide();
            //});

            $("#btn1").click(function () {
                // $(".pp").show('fast');
                $(".pp").show(1500)
                    //.css("color", "red")
                    //.css("font-size", "15px")

                var colorArr = [
                    "red", "blue", "green"
                ];

                var list = document.getElementsByClassName(".pp");

                for (var i = 0; i < 3; i++) {

                    // 0810 問題1 : (.pp)的屬性經過一次變更後，就固定了嗎
                    // 0810 問題2 : (.pp)迴圈時有辦法使其每圈的字體顏色都改變嗎

                    // 嘗試失敗區
                    //list.s1tyle = colorArr[i % 3];
                    //$(".pp").css(colorArr[i % 3]);

                    $(".pp").slideUp(200)
                        .css("color", "red")
                        .css("font-size", "25px")

                    for (var ii = 0; ii < 1; ii++) {
                        $(".pp").slideDown(1000)
                            .css("font-size", "50px")
                            .css("color", "blue")
                    }
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%-- HTTP Post為由表單送出的Request，表單的所有內容送到伺服器 --%>
        <div>
            <div>
                <p id="p" class="pp">If you click on me, I will disappear.</p>
                <p class="pp">Click me away!</p>
                <p class="pp" id="pp">Click me too!</p>

                <input type="text" id="text1" name="TextBox1" />
                <button type="button" id="btn1">ClickMe</button>
            </div>
        </div>
    </form>
</body>
</html>
