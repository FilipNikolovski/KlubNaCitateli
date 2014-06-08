$(document).ready(function () {
    var name = $("#name"),
      email = $("#email"),
      password = $("#password"),
      allFields = $([]).add(name).add(email).add(password),
      tips = $(".validateTips");

    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Length of " + n + " must be between " +
          min + " and " + max + ".");
            return false;
        } else {
            return true;
        }
    }

    $("#dialog-form").dialog({
        autoOpen: false,
        height: 300,
        width: 350,
        modal: true,
        buttons: {
            "Add": function () {
                var bValid = true;
                allFields.removeClass("ui-state-error");

                bValid = bValid && checkLength(name, "username", 3, 16);
                bValid = bValid && checkLength(email, "email", 6, 80);
                bValid = bValid && checkLength(password, "password", 5, 16);

                if (bValid) {
                    $("#users tbody").append("<tr>" +
                          "<td>" + name.val() + "</td>" +
                          "<td>" + email.val() + "</td>" +
                          "<td>" + password.val() + "</td>" +
                        "</tr>");
                    $(this).dialog("close");
                }
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        },
        close: function () {
            allFields.val("").removeClass("ui-state-error");
        }
    });






});



