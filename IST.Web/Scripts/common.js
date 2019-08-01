

//------Delete Alert-------
var swalInit = swal.mixin({
    buttonsStyling: false,
    confirmButtonClass: 'btn btn-primary',
    cancelButtonClass: 'btn btn-light'
});

function message_show(title='Oops...', text='Something went wrong!', type='error') {
    swalInit({
        title: title,
        text: text,
        type: type,
        padding: 40
    });
}

function delete_confirm(url, paramData) {
    swalInit({
        title: 'Are you sure want to delete?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Confirm'
    }).then(function (result) {
        if (result.value) {
            ajaxCall(url, paramData, "renderRemoveItem");

        }
    });

}

function reset_password_confirm(url, paramData) {
    swalInit({
        title: 'Are you sure want to reset password?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Confirm'
    }).then(function (result) {
        if (result.value) {
            ajaxCall(url, paramData, "renderResetPassword");

        }
    });

}

//---Form Progress-----
$("form").on("submit", function (event) {
    if ($(this).valid()) {
        $.blockUI({
            message: '<i class="icon-spinner4 spinner"></i>',
            timeout: 20000000, //unblock after 20 seconds
            overlayCSS: {
                backgroundColor: '#1b2024',
                opacity: 0.8,
                zIndex: 1200,
                cursor: 'wait'
            },
            css: {
                border: 0,
                color: '#fff',
                padding: 0,
                zIndex: 1201,
                backgroundColor: 'transparent'
            }
        });
    }
});


// check & convert value //
function customTryParseInt(value, defaultValue) {
    defaultValue = (typeof defaultValue !== "undefined" && defaultValue !== null) ? defaultValue : 0;
    var returnValue = defaultValue;
    if (typeof value !== "undefined" && value !== null) {
        if (value.toString().length > 0) {
            if (!isNaN(value)) {
                returnValue = parseInt(value);
            }
        }
    }
    return returnValue;
}
function customTryParseFloat(value, defaultValue) {
    defaultValue = (typeof defaultValue !== "undefined" && defaultValue !== null) ? defaultValue : 0;
    var returnValue = defaultValue;
    if (typeof value !== "undefined" && value !== null) {
        if (value.toString().length > 0) {
            if (!isNaN(value)) {
                returnValue = parseFloat(value);
            }
        }
    }
    return returnValue;
}
// serialized row //
function rearrangeNameSuffix(selector) {
    var count = 0;
    $(selector).find('tr').each(function () {
        var suffix = $(this).find(':input:first').attr('name').match(/\d+/);
        $.each($(this).find(':input'), function (i, val) {
            // Replaced Name
            var oldN = $(this).attr('name');
            var newN = oldN.replace('[' + suffix + ']', '[' + count + ']');
            $(this).attr('name', newN);
        });
        count++;
    });
}
// attachment file add & remove //
function addAttachmentFileRow() {
    var rowNumber = $("#attachmentFileTBody").find("tr").length;
    $("#attachmentFileTBody").append('<tr>' +
        '<td>' +
        '<input class="col-md-12 form-control input-sm" id="FileLists_' + rowNumber + '__FileBase" name="FileLists[' + rowNumber + '].FileBase" type="file" accept="application/msword, application/vnd.openxmlformats-officedocument.wordprocessingml.document, application/vnd.ms-powerpoint, application/vnd.openxmlformats-officedocument.presentationml.slideshow, application/vnd.openxmlformats-officedocument.presentationml.presentation,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel,text/plain, application/pdf,image/*">' +
        '</td>' +
        '<td width="3%">' +
        '<span onclick="removeRow(this)" class="btn btn-sm text-danger"><i class="icon-cancel-circle2"></i></span>' +
        '</td>' +
        '</tr>');
}
function removeRow(obj) {
    $(obj).closest("tr").remove();
}