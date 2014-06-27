function GoToAbout(sender, args) {
    
   if (sender._postBackSettings.sourceElement.id == 'mainContent_Button3') {
        $("#div1").hide();
        $("#div2").show();
        $("#li1").css("background-color", "#FFF8DC");
        $("#li2").css("background-color", "#D3D3D3");
    }
    else if(sender._postBackSettings.sourceElement.id == 'mainContent_Button2'){

        $("#div2").hide();
        $("#div1").show();
        $("#li2").css("background-color", "#FFF8DC");
        $("#li1").css("background-color", "#D3D3D3");
    }
}



$(document).ready(function () {


    $("#div2").hide();
    $("#li1").css("background-color", "#D3D3D3");


   
});