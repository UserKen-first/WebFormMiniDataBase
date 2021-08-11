<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryJQuery.aspx.cs" Inherits="WebApplication1.TryJQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script>
        // JQuery查回來的資料為一個集合
        $(document).ready(function () {

            //$(".pp").click(function () {
            //        $(this).hide();
            //    });

            //    $("#p").click(function () {
            //        $(".pp").show();
            //    });


            $("#text1").change(function () {
                alert($(this).val());
            });

            //$("#btn1").click(function () {
            //    $("#text1").val('');
            //});

            $("#btn1").on("click" , function () {
                alert("alert from button click");
            });

            $("#btn1").mouseenter(function () {
                alert("You entered pq!");
            });

            
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p id="p">If you click on me, I will disappear.</p>
            <p class="pp">Click me away!</p>
            <p class="pp" id="pp">Click me too!</p>

            <input type="text" id="text1" />
            <button type="button" id="btn1">ClickMe</button>
        </div>

        <script>
            // 透過選擇區域再執行行為
            var array = document.getElementByClassName(".pp");

            for (item of array) {
                item.onclick = function () {
                    item.style["display"] = "none";
                }
            }

        </script>

    </form>
</body>
</html>
