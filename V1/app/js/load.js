function lockUI() {

    $.blockUI({
        message: '<h5><div style="text-align: center; color: #4daf4d"><i class="fa fa-spinner fa-spin fa-4x"></i></div></h5>' +
            '<div style="font-size:21px;color: white"><b>Just a Moment...</b></div>'
    });
}
function unlockUI() {
    //$.uiUnlock();
    $.unblockUI();
}