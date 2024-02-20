$(function () {
    var form_pageSubmit = $('#form_pageSubmit');

    $('.btnJob').on({
        click: function () {
            form_pageSubmit.prop('action', mvcPatch('Job/JobMain')).submit();
        }
    });

    $('.btnProfile').on({
        click: function () {
            $.bPopup({
                url: mvcPatch('Home/ProfileView'),
                title: 'Profile View',
                closable: true,
                btnCancel: true,
                size: BootstrapDialog.SIZE_NORMAL,
                onshown: function (k) {
                    var content = k.getModalBody();
                    content.find('.btnSearch').click();
                    $.material.init();
                },

                buttons: [
                    {
                        id: 'btn-logout',
                        icon: 'fa fa-lock',
                        label: '&nbsp;&nbsp;logout',
                        action: function (k) {
                            var content = k.getModalBody();
                        }
                    }]
            });
        }
    });

    $('.btnProject').on({
        click: function () {
            form_pageSubmit.prop('action', mvcPatch('ProjectManage/ProjectMain')).submit();
        }
    });

    $('.btnReports').on({
        click: function () {
            form_pageSubmit.prop('action', mvcPatch('Report/ReportMain')).submit();
        }
    });

    //$('.btnHome').on({
    //    click: function () {
    //        form_pageSubmit.prop('action', mvcPatch('Home/Home')).submit();
    //    }
    //});

    if (parseInt($('.hasUser').val()) === 0)
        form_pageSubmit.prop('action', mvcPatch('Home/LoginPage')).submit();

    setInterval(function () {
        $.reqDataCheckUser({
            url: mvcPatch('Home/chkUseLogin'),
            data: {},
            callback: function (data) {
                if (!data.success) {
                    form_pageSubmit.prop('action', mvcPatch('Home/LoginPage')).submit();
                }
            }
        });
    }, 5000);
});