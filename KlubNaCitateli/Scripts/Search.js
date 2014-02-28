$(document).ready(function () {

    var dataIn = '{' + '"search":"' + $("#TextBox1").val() + '"}';

    $.ajax({
            url: "../Services/SearchService.svc/GetBooks",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: dataIn,
            dataType: "json",
            success: function(data) {
                var obj = JSON.parse(data.d);
                if(obj.Error == '') {
                   
                }
            }

});