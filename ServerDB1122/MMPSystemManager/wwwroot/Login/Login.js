$(document).ready(function() {
    $("#btu").click(function(){
        var username=$("#username").val();
        var password = $("#password").val();
        //var c = { "LoginId": username, "LoginPasswd": password};   //��¼
        //var c = {
        //  "admin": "54", "AdminId": "65874231", "AdminGrade": "1", "AdminName": "li", "AdminPasswd": "ok", "AdminContactPhone": "1234567879", "AdminContactEmail": "123456@163,com"
        //  , "AdminPicture": "c:/df", "AdminIdPict": "c:/df", "Remark": "c:/df"
        //};                //����admin   �� ����
         // var c = {
         //     "userinfo": "12", "UserName": "wang", "UserWechatName": "wechat", "UserId": "84645121", "UserContactPhone": "9684451", "UserContactEmail": "123456@163,com"
         //     , "UserFacepict": "c:/df", "UserPicTime": "c:/df", "Remark": "very"
         //};                //����userinfo
        //var c = { "admin": "100" };             //��ȡȫ��
        //var c = { "admin": "1234" };             //�˳�  
        var c = { "adminid": "1234", "adminname": "wang" };            //��ѯ
    //    var c = { "Id": "12" };             //ɾ��
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