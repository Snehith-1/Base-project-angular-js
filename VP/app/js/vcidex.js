/// JavaScripts for Vcidex


// Disable Back button

window.onpopstate = function (e) { window.history.forward(1); }

// Loading Spinner

function lockUI() {
    $.blockUI({
        message: '<h5><div style="text-align: center; color: white"><i class="fa fa-spinner fa-spin fa-4x"></i></div></h5>' +
            '<div style="font-size:21px;color: white"><b>Just a Moment...</b></div>'
    });
}
function unlockUI() {
    $.unblockUI();
}