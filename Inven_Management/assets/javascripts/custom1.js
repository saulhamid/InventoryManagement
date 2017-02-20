﻿$(function () {
    //valiDation();
    //InitDatePickers();
    //FormatDate();
     InitAccordions();
    //InitTabs();

    //InitButtons();
    InitDropDowns();
    //InitCustomDialog();

    //InitDialogs();
    //InitThemes();
    InitRequired();
    //InitHelp();
    InitDateTimePickers()
});
function InitAccordions() {
    $(".Accordion").accordion({
        collapsible: true, active:true 
    });
    $(".OpenAccordion").accordion({
        collapsible: false, active: 0, heightStyle: "content"
    });
    $(".CloseAccordion").accordion({
        collapsible: true, active: false, heightStyle: "content"
    });
}
  function tablelength( ) {
    var len = [[5, 10, 25, 50, 100, 150, 200, 500 - 1], [5, 10, 25, 50, 100, 150, 200, 500, "All"]];
    return len;
}
function CodeCheck(sender, T) {
    if ($(sender).val()=="") {
        return;
    }
    $(sender).parent().find('.NotMatch').remove();
    var url = "/Common/Common/EnumAlreadyExist?tableName=" + T + "&fieldName=Name&value=" + $(sender).val();
    $.ajax({
        type: "GET",
        url: url,
        error: function (xhr, status, error) {
            //"test"
        },
        success: function (response) {
            if (!response) {
                $(sender).val('');
                if (!$(sender).parent().find('.NotMatch').hasClass('NotMatch')) {
                    $(sender).parent().append('<p class="NotMatch" style="color:red;" >Value is not Matched</p>');
                }
            }
        }
    });
}
function customToollip() {
    //$('.country').attr('title', 'write here or Double click for sample');
    $(' .department,.designation,.section,.country, .division,.district, .bloodGroup, .degree, .employmentStatus, .employmentType, .gender, .immigrationType, .leaveApproveStatus, .leaveType, .leftType, .meritalStatus, .nationality, .religion, .salaryPayMode, .salutation, .skillQuality, .trainingCourse, .trainingPlace, .trainingStatus, .travelType, .languageE, .languageCompetency, .languageFluency').attr('title', 'write here or Double click for sample');
    $(' .department,.designation,.section,.country, .division,.district, .bloodGroup, .degree, .employmentStatus, .employmentType, .gender, .immigrationType, .leaveApproveStatus, .leaveType, .leftType, .meritalStatus, .nationality, .religion, .salaryPayMode, .salutation, .skillQuality, .trainingCourse, .trainingPlace, .trainingStatus, .travelType, .languageE, .languageCompetency, .languageFluency').attr('placeholder', 'write here');
    $('.customDatePicker,.customTimePicker').attr('placeholder', 'click here');
}
function InitDateTimePickers() {
    $(".customDatePicker").datepicker({
        changeYear: true, changeMonth: true, dateFormat: 'dd-M-yy', yearRange: '-90:+9', maxYear: '100'

    });

    $('.customTimePicker').prop("readonly", "readonly");
    $('.customDatePicker').prop("readonly", "readonly");

}

function InitDatePickers() {
    //var dt = FaizaSMS.WebUI.ViewModels.IdentityViewModel.SessionDate
    $(".DatePicker").prop("readonly", "readonly");
    $(".DatePicker:not(.OldDates)").datepicker({ changeYear: true, changeMonth: true, dateFormat: 'dd-M-yy', yearRange: '-100:+1' });
    $(".DatePicker.OldDates").datepicker({ changeYear: true, changeMonth: true, dateFormat: 'dd-M-yy', yearRange: '-100:+0', maxDate: '0', maxYear: '100' });
}
function FormatDate(input) {
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var dt = new Date(input);
    return [dt.getDate(), months[dt.getMonth()], dt.getFullYear()].join('-');
}
function ParseDate(input) {
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var parts = input.split('-');
    return new Date(parts[2], months.indexOf(parts[1]), parts[0]);
}

//function valiDation(sender) {
//    if (!$($("#frmEmployeeCreate .required")[0]).next('.RequiredField')) {

//    }
//    $($("#frmEmployeeCreate .required")[0]).next(':not(.RequiredField)')
//    $("#" + sender + " .required").parent().append('<p class="RequiredField" style="color:red;" >Value is Required</p>');
//    $("#" + sender + " .RequiredField").hide();
//    // $("#" + sender + ".required").change(function () {
//    $(".required").change(function () {
//        if ($(this).val() == "") {
//            $(this).parent().find('.RequiredField').show();
//        }
//        else {
//            $(this).parent().find('.RequiredField').hide('slow');
//        }
//    });
//}
function valiDation(sender) {
        //$("textarea").attr('maxlength', '200');
        //$("input:text").attr('maxlength', '50');

    $("#" + sender + " .required").parent().append('<p class="RequiredField" style="color:red;" >Value is Required</p>');
    $("#" + sender + " .RequiredField").hide();
   // $("#" + sender + ".required").change(function () {
    $(".required").change(function () {
        if ($(this).val() == "") {
            $(this).parent().find('.RequiredField').show();
        }
        else {
            $(this).parent().find('.RequiredField').hide('slow');
        }
    });
}
function BackToList(sender) {
    var url = $(sender).attr('data-url');
    window.location = url;
}
function NumberCheck(sender) {
    var val = $(sender).val().replace('.', '');
    val = val.replace(',', '');
    if (isNaN(val)) {
        $(sender).val("0");
        ShowResult("failure", "Only numeric allowed!");
    }
}
function pageSubmit(sender) {
    var a = 0;
    for (var i = 0; i < $('#' + sender + ' .required').length; i++) {
        if ($($('#' + sender + ' .required')[i]).val() == "") {
            $($('#' + sender + ' .required')[i]).parent().find('.RequiredField').show();
            a++;
        }
    }
    if (a == 0) {
        $("#"+sender).submit();
    }
}
function pageSubmitJSON(sender) {
    var a = 0;
    for (var i = 0; i < $('#' + sender + ' .required').length; i++) {
        if ($($('#' + sender + ' .required')[i]).val() == "") {
            $($('#' + sender + ' .required')[i]).parent().find('.RequiredField').show();
            a++;
        }
    }
    return a;
}
function Division() {
    $("select.country").change(function () {
        var a = $(this);
        $(a).closest('div').parent().closest('div').parent('div').find('.district').html("<option></option>")
        var dropdownElements = a.closest('div').parent().next().find('.division');
        var url = a.closest('div').parent().next().find('.division').attr('data-url') + "?country=" + $(this).val();
        $.getJSON(url, function (item, textStatus, jqXHR) {
            var Listitems = '<option value="">Select</option>';
            $.each(item, function (i, data) {
                if (true) {

                }
                Listitems += "<option value='" + data.Value + "'>" + data.Text + "</option>";
            });
            dropdownElements.html(Listitems).addClass("DropdownInited");
        });
    });
}
function District() {
    $("select.division").change(function () {
        var a = $(this);
        var dropdownElements = a.closest('div').parent().next().find('.district');
        var url = a.closest('div').parent().next().find('.district').attr('data-url') + "?division=" + $(this).val();
        $.getJSON(url, function (item, textStatus, jqXHR) {
            var Listitems = '<option value="">Select</option>';
            $.each(item, function (i, data) {
                if (true) {

                }
                Listitems += "<option value='" + data.Value + "'>" + data.Text + "</option>";
            });
            dropdownElements.html(Listitems).addClass("DropdownInited");
        });
    });
}
function Country() {
    var county = $("select.country");
    $.each(county, function (a, b) {
        var selectedValue = $(b).attr('data-val');
        var url = $(b).attr('data-url');
        $.getJSON(url, function (item, textStatus, jqXHR) {
            var Listitems = '<option value="">Select</option>';
            $.each(item, function (i, data) {
                if (selectedValue == data.Value) {
                    Listitems += "<option selected='selected' value='" + data.Value + "'>" + data.Text + "</option>";
                    //
                        var selectedValue2 = $(b).closest('div').parent().next().find('.division').attr('data-val');
                        var url2 = $(b).closest('div').parent().next().find('.division').attr('data-url') + "?country=" + selectedValue;
                        $.getJSON(url2, function (item2, textStatus, jqXHR) {
                            var dListitems = '<option value="">Select</option>';
                            $.each(item2, function (i, division) {
                                if (selectedValue2 == division.Value) {
                                    dListitems += "<option selected='selected' value='" + division.Value + "'>" + division.Text + "</option>";
                                    //
                                    var selectedValue3 = $(b).closest('div').parent().closest('div').parent('div').find('.district').attr('data-val');
                                    var url3 = $(b).closest('div').parent().closest('div').parent('div').find('.district').attr('data-url') + "?division=" + selectedValue2;
                                    $.getJSON(url3, function (item3, textStatus, jqXHR) {
                                        var dListitem3 = '<option value="">Select</option>';
                                        $.each(item3, function (i, disct) {
                                            if (selectedValue3 == disct.Value) {
                                                dListitem3 += "<option selected='selected' value='" + disct.Value + "'>" + disct.Text + "</option>";

                                            }
                                            else {
                                                dListitem3 += "<option value='" + disct.Value + "'>" + disct.Text + "</option>";
                                            }
                                        });
                                        $(b).closest('div').parent().closest('div').parent('div').find('.district').html(dListitem3).addClass("DropdownInited");
                                    });
                                    //
                                }
                                else {
                                    dListitems += "<option value='" + division.Value + "'>" + division.Text + "</option>";
                                }
                            });
                            $(b).closest('div').parent().next().find('.division').html(dListitems).addClass("DropdownInited");
                        });
                    //
                }
                else {
                    Listitems += "<option value='" + data.Value + "'>" + data.Text + "</option>";
                }
            });
            $(b).html(Listitems).addClass("DropdownInited");
        });
    });
}


//if (selectedValue != null) {
//    var selectedValue = $(b).closest('div').parent().next().find('.division').attr('data-val');
//    var url = $(b).closest('div').parent().next().find('.division').attr('data-url') + "?country=" + selectedValue;
//    $.getJSON(url, function (item, textStatus, jqXHR) {
//        var Listitems = '<option></option>';
//        $.each(item, function (i, data) {
//            if (selectedValue == data.Value) {
//                Listitems += "<option selected='selected' value='" + data.Value + "'>" + data.Text + "</option>";
//            }
//            else {
//                Listitems += "<option value='" + data.Value + "'>" + data.Text + "</option>";
//            }
//        });
//        $(b).closest('div').parent().next().find('.division').html(Listitems).addClass("DropdownInited");
//    });
//}



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

function InitCascadingDropDowns() {
    var dependentElements = $('select.Cascading:not(.DropdownInited)');
    $.each(dependentElements, function (index, element) {
        var dependentEl = $(element);
        var parentEl = $('#' + dependentEl.attr('data-parent')); // element.dataset.parent;
        var url = dependentEl.attr('data-url');
        var selected = dependentEl.attr('data-selected');
        var dataCache = dependentEl.attr('data-cache') ? true : false;
        var loadDropDownItems = function () {
            if (!parentEl.val()) {
                if (selected) {
                    setTimeout(loadDropDownItems, 300);
                }
                return;
            }
            $.ajax({
                url: url + parentEl.val(),
                type: 'GET',
                cache: dataCache,
                success: function (jsonData, textStatus, XMLHttpRequest) {
                    var Listitems = '<option></option>';
                    $.each(jsonData, function (i, item) {
                        if (selected && selected == item.Value) {
                            Listitems += "<option selected='selected' value='" + item.Value + "'>" + item.Text + "</option>";
                        }
                        else {
                            Listitems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                        }
                    });
                    dependentEl.html(Listitems).addClass("DropdownInited");
                }
            });
        };
        parentEl.change(loadDropDownItems);
        if (selected) {
            loadDropDownItems();
        }
    });
}
function fnUpdate(submitBtn) {
    var $form = $(submitBtn).parents('form');
    var tt = "";
    $.ajax({
        type: "POST",
        url: $form.attr('action'),
        data: $form.serialize(),
        error: function (xhr, status, error) {
            "test"
        },
        success: function (response) {
           // $(".dialog-alert").dialog('open');
            $("#dialog-msg").append('' + response[1]);
            $(".ui-dialog").addClass('' + response[0]);

        }
    });
    return false;// if it's a link to prevent post
}
function InitRequired() {
    //$("[data-val-required]").css({
    //    $(".required").css({
    //    borderRight: "4px solid #f20"
    //});
}
var url = "";
function fromWriteColor(value)
{
    $("." + value + " input:text,." + value + " textarea,." + value + " input:file,." + value + " select").addClass("wrightColor");
}
function fromReadColor(value) {
    $("." + value + " input:text,." + value + " textarea,." + value + " input:file,." + value + " select").addClass("readOnlyColor");
}
function fnReadOnly(value) {
    if ($("." + value).hasClass("readOnly")) {
        $("." + value).removeClass("readOnly");
        $("." + value + " input:text,." + value + " textarea").attr("readOnly", false);
        $("." + value + " input:file,." + value + " select,." + value + "  input:Checkbox,." + value + " .customDatePicker,." + value + " .customTimePicker").attr("disabled", false);
        $("." + value + " input:text,." + value + " textarea,." + value + " input:file,." + value + " select").addClass("wrightColor");
        $("." + value + " input:text,." + value + " textarea,." + value + " input:file,." + value + " select").removeClass("readOnlyColor");
        //$("." + value).attr("readOnly", false);
        //$("." + value).addClass("wrightColor");
        //$("." + value).removeClass("readOnlyColor");
        $("." + value + " .customTimePicker").attr('style', "cursor:pointer !important");
    }
    else {
        $("." + value + " input:file,." + value + " select,." + value + " input:Checkbox,." + value + " .customDatePicker,." + value + " .customTimePicker").attr("disabled", true);
        $("." + value).addClass("readOnly");
        $("." + value + " input:text,." + value + " textarea").attr("readOnly", true)
        $("." + value + " input:text,." + value + " textarea,." + value + " input:file,." + value + " select").removeClass("wrightColor");
        $("." + value + " input:text,." + value + " textarea,." + value + " input:file,." + value + " select").addClass("readOnlyColor");
        $("." + value + " .customTimePicker").attr('style', "cursor:not-allowed !important");
    }
}

function SelectAllForDelete() {
    $(".chkAll").on("click", function () {
        if ($(this).is(":checked")) {
            $(this).closest("table").find("tbody input:Checkbox").attr("checked", true);
        }
        else {
            $(this).closest("table").find("tbody input:Checkbox").attr("checked", false);
        }
    });
    $(".paginate_enabled_next,.paginate_enabled_previous,.paginate_disabled_next,.paginate_disabled_previous").on("click", function () {
        $(this).parents().find("table th .chkAll").attr("checked", false);
    });
}
function deletedOne(sender) {
    var url = $(sender).attr("data-url") + "~";
    //alert(url);
    Ask("Are you sure to delete!", function () {
        $.getJSON(url, function (item, textStatus, jqXHR) {
            ShowResult("Success", item);
            var interval = setInterval(function () { window.location.reload(true); clearInterval(interval); }, 2000);
        });
    }, function () { })
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
            var interval = setInterval(function () { window.location.reload(true); clearInterval(interval); }, 2000);
        });
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
function CheckPromotionDate(sender) {
    var url = "/HRM/Home/CheckPromotionDate?employeeId=" + $("#promotionVM_EmployeeId").val() + "&date=" + $(sender).val();
    $.ajax({
        type: "GET",
        url: url,
        error: function (xhr, status, error) {
            //"test"
        },
        success: function (response) {
            if (!response) {
                $(sender).val('');
                ShowResult("failure", "Promotion date can't be perior to join date/  last promotion date");
            }
        }
    });
}
function CheckTransferDate(sender) {
    var url = "/HRM/Home/CheckTransferDate?employeeId=" + $("#transferVM_EmployeeId").val() + "&date=" + $(sender).val();
    $.ajax({
        type: "GET",
        url: url,
        error: function (xhr, status, error) {
            //"test"
        },
        success: function (response) {
            if (!response) {
                $(sender).val('');
                ShowResult("failure", "Transfer date can't be perior to join date/  last transfer  date");
            }
        }
    });
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
function CloseModal(el, dataAction,dataUrl) {
    //if (dataAction == "formsubmit") {
    //    $("#" + dataUrl).submit();
    //}
    //else if (dataAction == "refreshparent") {
    //    window.parent.location = window.parent.location;
    //}
    //else if (dataAction == "refreshself") {
    //    window.location = window.location;
    //}
    //else if (dataAction == "redirect") {
    //    window.location = dataUrl;
    //}
    //else if (dataAction == "function") {
    //    eval(dataUrl);
    //}

     if (dataAction == "refreshparent") {
        window.parent.location = window.parent.location;
     }
    else if (dataAction == "redirect") {
        window.location = dataUrl;
    }
    var win = $(el).closest(".modal");
    win.modal("hide");
    setTimeout(function () {
        win.next(".modal-backdrop").remove();
        win.remove();
        $("body").removeClass("modal-open");
    }, 500);

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
function InitButtons() {
    $('.AddRow:not(.AddRowInited)').on("click", function () {
        var url = $(this).attr('data-url');
        var container = $(this).attr('data-container');
        $.ajax({
            url: url,
            type: 'POST',
            cache: false,
            success: function (html) {
                $("#" + container).append(html);
            }
        });
        return false;
    }).addClass("AddRowInited");

    $('.RemoveRow:not(.RemoveRowInited)').on("click", function () {
        $(this).parents("div.row:first").remove();
        return false;
    }).addClass("RemoveRowInited");
}

function CheckImageSize(sender) {
    if (sender.files[0].size > (1024*100)) {
        ShowResult('failure', 'Image size can be maximum 100kb');
        $(sender).val('');
    }
}
function CheckFileSize(sender) {
    if (sender.files[0].size > (1024*512)) {
        ShowResult('failure', 'File size can be maximum 512kb');
        $(sender).val('');
    }
}
function FileDelete(sender)
{
    Ask("Are you sure to delete this file!", function () {
        var url = "/Config/DropDown/FileDelete?filepath=" + $(sender).attr('data-url') + "&table=" + $(sender).attr('data-table') + "&field=" + $(sender).attr('data-field') + "&id=" + $(sender).attr('data-id');
        $.ajax({
            type: "GET",
            url: url,
            error: function (xhr, status, error) {
                //"test"
            },
            success: function (response) {
                if (response) {
                    $(sender).closest("div").parent().remove();
                    ShowResult("Success", "File deleted!");
                }
                else {
                    ShowResult("failure", "File deleted failed!");
                }
            }
        })
    }, function () { })

}
//// salary structure start
function FixedCheck(sender) {
    if ($(sender).is(":checked")) {
        $(sender).closest('.row').find(".PortionSalaryTypeId option:selected").prop("selected", false);
        $(sender).closest('.row').find(".PortionSalaryTypeId").attr('disabled', true);
    }
    else {
        $(sender).closest('.row').find(".PortionSalaryTypeId").attr('disabled', false);
        $(sender).closest('.row').find(".Portion").trigger('change');
    }
}

function SalaryTypePortion(sender) {
    $(sender).closest('div').find('lable').hide();
    if (!isNaN($(sender).val())) {
        if (!$(sender).closest('.row').find(".IsFixed").is(":checked")) {
            if (parseFloat($(sender).val()) > 100) {
                $(sender).val("100.00");
            }
            else if (parseFloat($(sender).val()) < 0) {
                $(sender).val("0.00");
            }
        }
    }
    else {
        $(sender).val("0.00");
        $(sender).closest('div').append('<lable class="" style="color:red">Only numecir value is allowed!<lable>');
    }
}

function CheckNumeric(value) {
    var a = $(value).val();
    if (!$.isNumeric(a)) {
        $(value).val("")
        ShowResult("failure", "Please Enter Numeric data");

        return;
    }
}
$('.ccProject').click(function () {
    var departments = "";
    $('.department').html("");
     

    var oParam = { "projectId": "" };
    oParam.projectId = $('.ccProject').val();

    var url1 = "/Config/DropDown/DerparmentByProject";
    departments += "<option value='0'>Select</option>";

    $.getJSON(url1, oParam, function (data) {
        $.each(data, function (i, state) {
            departments += "<option value='" + state.value + "'>" + state.text + "</option>";
        });
        $('.ccDepartment').html(departments);
    });
});
//// salary structure end

//function fnReadOnly(value) {
//    if ($("." + value).hasClass("readOnly")) {
//        $("." + value).removeClass("readOnly");
//        $("." + value + " .text-box").attr("readOnly", false);
//        $("." + value + " .text-box").addClass("wrightColor");
//        $("." + value + " .text-box").removeClass("readOnlyColor");
//    }
//    else {
//        $("." + value).addClass("readOnly");
//        $("." + value + " .text-box").attr("readOnly", true)
//        $("." + value + " .text-box").removeClass("wrightColor");
//        $("." + value + " .text-box").addClass("readOnlyColor");
//    }
//}
//function fnReadOnly(value) {
//    if ($("." + value).hasClass("readOnly")) {
//        $("." + value).removeClass("readOnly");
//        $("." + value + " .text-box").attr("readOnly", false);
//        $("." + value + " .text-box").addClass("wrightColor");
//        $("." + value + " .text-box").removeClass("readOnlyColor");
//    }
//    else {
//        $("." + value).addClass("readOnly");
//        $("." + value + " .text-box").attr("readOnly", true)
//        $("." + value + " .text-box").removeClass("wrightColor");
//        $("." + value + " .text-box").addClass("readOnlyColor");
//    }
//}

//function InitCustomDialog() {
    //$.ajaxSetup({ cache: false });
    //$(".dialog-alert").dialog({
    //    autoOpen: false,
    //    resizable: false,
    //    //height: 170,
    //    title: "Symphony Office",
    //    cache: false,
    //    width: 350,
    //    show: { effect: 'drop', direction: "up" },
    //    modal: true,
    //    draggable: true,
    //    open: function (event, ui) {
    //        $(".ui-dialog-titlebar-close").hide();
    //    },
    //    buttons: {
    //        "OK": function () {
    //            $(this).dialog("close");

    //        },
    //        "Cancel": function () {
    //            $(this).dialog("close");
    //        }
    //    }
    //});
    //$(".dialog-create").dialog({
    //    title: 'Create',
    //    autoOpen: false,
    //    resizable: false,
    //    cache: false,
    //    width: 400,
    //    show: { effect: 'drop', direction: "up" },
    //    modal: true,
    //    draggable: true,
    //    open: function (event, ui) {
    //        $(".ui-dialog-titlebar-close").hide();
    //        $(this).load(url);
    //        $(".ui-dialog").removeClass('Success');
    //        $(".ui-dialog").removeClass('Fail');
    //    }
    //});
    //$(".dialog-edit").dialog({
    //    title: 'Update',
    //    autoOpen: false,
    //    resizable: false,
    //    cache: false,
    //    width: 400,
    //    show: { effect: 'drop', direction: "up" },
    //    modal: true,
    //    draggable: true,
    //    open: function (event, ui) {
    //        $(".ui-dialog-titlebar-close").hide();
    //        $(this).load(url);
    //        $(".ui-dialog").removeClass('Success');
    //        $(".ui-dialog").removeClass('Fail');
    //    }
    //});
    //$(".dialog-confirm").dialog({
    //    autoOpen: false,
    //    resizable: false,
    //    cache: false,
    //    height: 170,
    //    width: 350,
    //    show: { effect: 'drop', direction: "up" },
    //    modal: true,
    //    draggable: true,
    //    open: function (event, ui) {
    //        $(".ui-dialog-titlebar-close").hide();

    //    },
    //    buttons: {
    //        "OK": function () {
    //            $(this).dialog("close");
    //            window.location.href = url;
    //        },
    //        "Cancel": function () {
    //            $(this).dialog("close");
    //        }
    //    }
    //});

    //$(".dialog-detail").dialog({
    //    title: 'View User',
    //    autoOpen: false,
    //    resizable: false,
    //    cache: false,
    //    width: 400,
    //    show: { effect: 'drop', direction: "up" },
    //    modal: true,
    //    draggable: true,
    //    open: function (event, ui) {
    //        $(".ui-dialog-titlebar-close").hide();
    //        $(this).load(url);
    //    },
    //    buttons: {
    //        "Close": function () {
    //            $(this).dialog("close");
    //        }
    //    }
    //});

    //$(".lnkCreate").live("click", function (e) {
    //    url = $(this).attr('href');
    //    $(".dialog-create").dialog('open');

    //    return false;
    //});

    //$(".lnkEdit").live("click", function (e) {
    //    url = $(this).attr('href');
    //    $(".dialog-edit").dialog('open');

    //    return false;
    //});

    //$(".lnkDelete").live("click", function (e) {
    //    url = $(this).attr('href');
    //    $(".dialog-confirm").dialog('open');

    //    return false;
    //});

    //$(".lnkDetail").live("click", function (e) {
    //    url = $(this).attr('href');
    //    $(".dialog-detail").dialog('open');

    //    return false;
    //});

    //$(".btncancel").live("click", function (e) {
    //    $(".dialog-edit").dialog("close");
    //    $(".dialog-create").dialog("close");
    //    return false;
    //});
//}
///
///////////-----------------------Pharmaraw--------------------///////////////

/////<input type="text" name="textWeight" id="txtWeight" maxlength="5" onkeyup="this.value = minmax(this.value, 0, 100)"/>
function minmax(value, min, max) {
    var a = $(value).val();
    if (!$.isNumeric(a)) {
        $(value).val("")
        ShowResult("failure", "Please Enter Numeric data");
        return;
    }
    else {
        if (parseInt(a) < min || isNaN(parseInt(a))) {
            ShowResult("failure", "Please Enter more then")
            return;
        }
        else if (parseInt(a) > max) {
            ShowResult("failure", "Please Enter greater then")
            return;
        }
        else return value;
    }
}