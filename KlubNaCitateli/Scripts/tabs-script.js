function GoToAbout(sender, args) {
    
   if (sender._postBackSettings.sourceElement.id == 'mainContent_Button3') {
        $("#div1").hide();
        $("#div2").show();
        $("#li1").css("background-color", "#E6E6FA");
        $("#li2").css("background-color", "#D3D3D3");
    }
    else if(sender._postBackSettings.sourceElement.id == 'mainContent_Button2'){

        $("#div2").hide();
        $("#div1").show();
        $("#li2").css("background-color", "#E6E6FA");
        $("#li1").css("background-color", "#D3D3D3");
    }
}



$(document).ready(function () {

    $("#div2").hide();
    $("#li1").css("background-color", "#D3D3D3");

    $("#li1").click(function () {
        $("#div2").hide();
        $("#div1").show();
        $("#li2").css("background-color", "#E6E6FA");
        $(this).css("background-color", "#D3D3D3");
    });
    $("#li2").click(function () {
        $("#div1").hide();
        $("#div2").show();
        $("#li1").css("background-color", "#E6E6FA");
        $(this).css("background-color", "#D3D3D3");
    });

    var availableTags = [
      "ActionScript",
      "AppleScript",
      "Asp",
      "BASIC",
      "C",
      "C++",
      "Clojure",
      "COBOL",
      "ColdFusion",
      "Erlang",
      "Fortran",
      "Groovy",
      "Haskell",
      "Java",
      "JavaScript",
      "Lisp",
      "Perl",
      "PHP",
      "Python",
      "Ruby",
      "Scala",
      "Scheme"
    ];

    $("#tags").autocomplete({
        source: availableTags
    });



   
    

   
});