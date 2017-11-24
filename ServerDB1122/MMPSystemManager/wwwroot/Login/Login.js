$(document).ready(function() {
    $("#btu").click(function(){
        var username=$("#username").val();
        var password = $("#password").val();
       // var c = { "LoginId": username, "LoginPasswd": password};   //登录
        //var c = {
        //  "admin": "6534", "AdminId": "854261", "AdminGrade": "2", "AdminName": "zhang", "AdminPasswd": "mima", "AdminContactPhone": "1234567879", "AdminContactEmail": "123456@163,com"
        //  , "AdminPicture": "c:/df", "AdminIdPict": "c:/df", "Remark": "c:/df"
        //};                //插入admin   或 更新
//          var c = {
 //             "userinfo": "12", "UserName": "zhang", "UserWechatName": "zhang", "UserId": "625449456", "UserContactPhone": "1234567879", "UserContactEmail": "123456@163,com"
//              , "UserFacepict": "c:/df", "UserPicTime": "c:/df", "Remark": "very"
//         };                //插入userinfo
    //   var c = { "admin": "100" };             //获取全部
        var c = { "Id": "12" };             //删除
        $.post("../Api/SystemApi/Delete",
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