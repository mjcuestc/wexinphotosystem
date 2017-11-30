$(document).ready(function () {

    var package = { "userinfo": "100" };
    $.post("../../Api/SystemApi/Getall",
        package,
        function (data_return) {

            var data = data_return;
            var User_table = document.getElementById("table_t");
            for (var i = 0; i < data.length; i++) {
                var row = User_table.insertRow(User_table.rows.length);

                var c1 = row.insertCell(0);
                c1.innerHTML = data[i].UserName;
                var c2 = row.insertCell(1);
                c2.innerHTML = data[i].UserContactPhone;
                var c3 = row.insertCell(2);
                c3.innerHTML = data[i].UserWechatName;
                var c3 = row.insertCell(3);
                c3.innerHTML = data[i].UserContactEmail;
                var c3 = row.insertCell(4);
                c3.innerHTML = '<div class="delete"><button type="button" class="delete_btu" onclick="delete_fun(this)"/>ɾ��</div><div class="admin_detail"><button type="button" class="delete_btu" onclick="detail_fun(this)"/>����</div>';
            }

        })
});

function delete_fun(user_info) {


    var delete_id = user_info.parentNode.parentNode.parentNode.childNodes[0].textContent;
    //admin_info.parentNode.parentNode.parentNode.remove(admin_info.parentNode.parentNode); //IE��֧��


    //�����Խ��
    var userAgent = navigator.userAgent;
    var isOpera = userAgent.indexOf("Opera") > -1; //�ж��Ƿ�Opera�����
    var isFF = userAgent.indexOf("Firefox") > -1; //�ж��Ƿ�Firefox�����
    var isSafari = userAgent.indexOf("Safari") > -1 && userAgent.indexOf("Chrome") == -1; //�ж��Ƿ�Safari�����
    var isChrome = userAgent.indexOf("Chrome") > -1 && userAgent.indexOf("Safari") > -1; //�ж�Chrome�����
    var isIE = userAgent.indexOf("Windows NT") > -1 && userAgent.indexOf("Gecko") > -1 && !isOpera & !isChrome;  //�ж��Ƿ�IE�����
    var isEdge = userAgent.indexOf("Edge") > -1; //�ж��Ƿ�IE��Edge�����

    if (isIE) {
        user_info.parentNode.parentNode.parentNode.removeNode(user_info.parentNode.parentNode);
    } else {
        user_info.parentNode.parentNode.parentNode.remove(user_info.parentNode.parentNode);
    }

    var package = { "Id": delete_id };
    $.post("../../Api/SystemApi/Delete",
        package,
        function (data_return) {

            var data = data_return;

            if (data == true) {
                alert("ɾ���ɹ�");
            }
            else {
                alert("ɾ��ʧ��");
            }
        })
}


function detail_fun(user_info) {
    var detail_id = user_info.parentNode.parentNode.parentNode.childNodes[0].textContent;
    window.location.href = './user_detail/user_detail.html?id=' + detail_id;
}

