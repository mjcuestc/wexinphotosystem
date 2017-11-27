$(document).ready(function() {
    $("#btu").click(function(){
        var username=$("#username").val();
        var password = $("#password").val();
        //var c = { "LoginId": username, "LoginPasswd": password};   //登录
        //var c = {
        //  "admin": "54", "AdminId": "65874231", "AdminGrade": "1", "AdminName": "li", "AdminPasswd": "ok", "AdminContactPhone": "1234567879", "AdminContactEmail": "123456@163,com"
        //  , "AdminPicture": "c:/df", "AdminIdPict": "c:/df", "Remark": "c:/df"
        //};                //插入admin   或 更新
         // var c = {
         //     "userinfo": "12", "UserName": "wang", "UserWechatName": "wechat", "UserId": "84645121", "UserContactPhone": "9684451", "UserContactEmail": "123456@163,com"
         //     , "UserFacepict": "c:/df", "UserPicTime": "c:/df", "Remark": "very"
         //};                //插入userinfo
        //var c = { "admin": "100" };             //获取全部
        //var c = { "admin": "1234" };             //退出  
        var c = { "adminid": "1234", "adminname": "wang" };            //查询
    //    var c = { "Id": "12" };             //删除
        $.post("../Api/SystemApi/Search",
            c,
            function (data) {
                
                alert(data);
                window.location.href='index.html';
             
            }
        )
    })
});

/*

*/