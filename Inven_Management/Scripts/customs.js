/// <reference path="jquery-1.8.2.js" />
$(document).on("click", "[type='checkbox']", function (e) {
    if (this.checked) {
        $(this).attr("value", "true");
    } else {
        $(this).attr("value", "false");
    }
});

$(function () {
    //popupvalidation("upfrom");
    //pageSubmit('upfrom');
    InitDropDowns();
})
function InitDropDowns() {
    var dropdownElements = $('select.Dropdown:not(.DropdownInited)');
    $.each(dropdownElements, function (index, element) {
        var dropdownEl = $(element);
        var url = dropdownEl.attr('data-url');
        if (!url) {
            alert("no url");
            return;
        }
        var selected = dropdownEl.attr('data-selected');
        var dataCache = dropdownEl.attr('data-cache') ? true : false;
        $.ajax({
            url: url,
            type: 'GET',
            cache: dataCache,
            success: function (jsonData, textStatus, XMLHttpRequest) {
                var Listitems = '<option value="">Select</option>';
                $.each(jsonData, function (i, item) {
                    if (selected && selected == item.Value) {
                        Listitems += "<option selected='selected' value='" + item.Value + "'>" + item.Text + "</option>";
                    }
                    else {
                        Listitems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    }
                });
                dropdownEl.html(Listitems).addClass("DropdownInited");
            }
        });
    });
}
function deletedData(sender, checkboxId, id) {
    var deletedIds = "";
    if (typeof id === 'undefined') {
        var length = $("#" + checkboxId + " tbody input:checkbox").length;
        for (var i = 0; i < length; i++) {
            if ($($("#" + checkboxId + " tbody input:checkbox")[i]).is(":checked")) {
                deletedIds += $($("#" + checkboxId + " tbody input:checkbox")[i]).attr("data-Id") + "~";
            }
        }
    }
    else {
        deletedIds = id + "~";
    }


    var url = $(sender).attr("data-url") + "?ids=" + deletedIds;
    if (deletedIds == "") {
        ShowResult("failure", "Select first to delete");
        return;
    }
    Ask("Are you sure to delete!", function () {
        $.getJSON(url, function (item, textStatus, jqXHR) {
            ShowResult("Success", item);
            var interval = setInterval(function () {
                window.location.reload(true); clearInterval(interval); 
            }, 4000);
        });
        location.reload(true);
    }, function () { })
}
function GoTo(sender) {
    window.location = $(sender).attr("data-url");
}
function CheckAll(sender) {

    var unchecked = $(sender).closest("tbody").find('input:checkbox:not(":checked")').length;
    if (unchecked == 0) {
        $(sender).closest("table").find(" thead input:checkbox").attr('checked', true);
    }
    else {
        $(sender).closest("table").find(" thead input:checkbox").attr('checked', false);
    }
}
function tablelength() {
    var len = [[5, 10, 25, 50, 100, 150, 200, 500 - 1], [5, 10, 25, 50, 100, 150, 200, 500, "All"]];
    return len;
}
function Ask(msg, yesCallback, noCallback) {
    var html = '' +
    '<div class="modal fade ask" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">' +
        '<div class="modal-dialog" style="margin-top:150px;width:400px;">' +
            '<div class="modal-content">' +
                '<div class="modal-header">' +
                    '<button type="button" class="close" onclick="CloseModal(this);" aria-hidden="true">&times;</button>' +
                    '<h2 class="modal-title" id="myModalLabel">HRM</h2>' +
                 '</div>' +
                 '<div class="modal-body Success">' +
                    msg +
                 '</div>' +
                 '<div class="modal-footer">' +
                    '<input type="button" id="btnAskYes"   value="Yes" class="btn btn-success" />' +
                    '<input type="button" id="btnAskNo" value="No" class="btn btn-success" />' +
                '</div>' +
            '</div>' +
        '</div>' +
    '</div>';

    var dialogWindow = $(html).appendTo('body');
    dialogWindow.modal({ backdrop: 'static' });

    $("#btnAskYes").on("click", function () {
        var win = $(this).closest(".modal");
        win.modal("hide");
        setTimeout(function () {
            win.next(".modal-backdrop").remove();
            win.remove();
            $("body").removeClass("modal-open");
        }, 500);
        setTimeout(yesCallback, 600);
    });
    $("#btnAskNo").on("click", function () {
        var win = $(this).closest(".modal");
        win.modal("hide");
        setTimeout(function () {
            win.next(".modal-backdrop").remove();
            win.remove();
            $("body").removeClass("modal-open");
        }, 500);
        setTimeout(noCallback, 600);
    });
}
function popupvalidation(sender) {
    //$("textarea").attr('maxlength', '200');
    //$("input:text").attr('maxlength', '50');
        //$("#"+sender+'.required').keyup(function () {
        //    if ($("#" + sender + '.required').val() != '') {
        //        $('button[type="submit"]').prop('disabled', false);
        //    }
        //});

    $("#" + sender + " .required").parent().append('<p class="RequiredField" style="color:red;" >Value is Required</p>');
    $("#" + sender + " .RequiredField").hide();
    // $("#" + sender + ".required").change(function () {
    $(".required").change(function () {
        if ($(this).val() == "") {
            $('button[type="submit"]').prop('disabled', true);

            $(this).parent().find('.RequiredField').show();

        }
        else {
            $(this).parent().find('.RequiredField').hide('slow');
            $(this).parent().find('button[type="button"]').attr('type', 'submit');

        }
    });
}
function pageSubmit(sender) {
    var a = 0;
    for (var i = 0; i < $('#' + sender + ' .required').length; i++) {
        if ($($('#' + sender + ' .required')[i]).val() == "") {
            $($('#' + sender + ' .required')[i]).parent().find('.RequiredField').show();
            a++;
        }
    }
    if (a <= 2) {
        $("#" + sender).submit();
    }
}
function ShowResult(status, msg, dataAction, dataUrl) {
    var html = '' +
    '<div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">' +
        '<div class="modal-dialog" style="margin-top:150px;width:500px;">' +
            '<div class="modal-content">' +
                '<div class="modal-header">' +
                    '<button type="button" class="close"  onclick="CloseModal(this)" aria-hidden="true">&times;</button>' +
                    '<h2 class="modal-title" id="myModalLabel">HRM</h2>' +
                 '</div>' +
                 '<div class="modal-body ' + status + '">' +
                    msg +
                 '</div>' +
                 '<div class="modal-footer">' +
                    '<input type="button" value="Ok" class="btn btn-info " onclick="CloseModal(this,\'' + dataAction + '\',\'' + dataUrl + '\')"/>' +
                '</div>' +
            '</div>' +
        '</div>' +
    '</div>';

    var dialogWindow = $(html).appendTo('body');
    dialogWindow.modal({ backdrop: 'static' });
}