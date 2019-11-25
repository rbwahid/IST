
// basic function for ajax call get data //

function ajaxCall(url, paramData, callback, method, obj) {
    method = method == null ? "POST" : method;
    $.ajax({
        type: method,
        url: url,
        data: JSON.stringify(paramData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //callback(data);
            if (callback == 'renderRemoveItem') {
                renderRemoveItem(response);
            }
            else if (callback == 'renderResetPassword') {
                renderResetPassword(response);
            }
            else if (callback == 'renderCompanyEntryLoad') {
                renderCompanyEntryLoad(response);
            }
            else if (callback == 'renderCompanyProjectEntryLoad') {
                renderCompanyProjectEntryLoad(response);
            }
            else if (callback == 'renderPositionEntryLoad') {
                renderPositionEntryLoad(response);
            }
            else if (callback == 'renderTicketsByProjectLoad') {
                renderTicketsByProjectLoad(response);
            } else if (callback == 'renderIssueLabelEntryLoad') {
                renderIssueLabelEntryLoad(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + "! please try again", '<i class="fa fa-exclamation-circle" aria-hidden="true"> Alert</i>');
        }
    });
}

// for Modal...
function ajaxCallModal(url, paramData, callback, method, obj) {
    method = method == null ? "POST" : method;
    $.ajax({
        type: method,
        url: url,
        data: JSON.stringify(paramData),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            //callback(data);
            if (callback == 'renderUserDetailsEntryLoad') {
                console.log(response);
                renderUserDetailsEntryLoad(response);
            }
            else if (callback == 'renderCreateTicketAssignLoad') {
                renderCreateTicketAssignLoad(response);
            }
            else if (callback == 'renderProjectDetailsEntryLoad') {
                renderProjectDetailsEntryLoad(response);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + "! please try again", '<i class="fa fa-exclamation-circle" aria-hidden="true"> Alert</i>');
        }
    });
}

// render method //

function renderRemoveItem(data) {
    swalInit({
        title: 'Deleted!',
        text: 'Your data has been deleted.',
        type: 'success'
    }).then(function (result) {
        //if (result.value) {
            location.reload();
        //}
    });
}

function renderResetPassword(data) {
    swalInit({
        title: 'Password Changed!',
        text: 'Your password has been changed.',
        type: 'success'
    }).then(function (result) {
        //if (result.value) {
        location.reload();
        //}
    });
}
