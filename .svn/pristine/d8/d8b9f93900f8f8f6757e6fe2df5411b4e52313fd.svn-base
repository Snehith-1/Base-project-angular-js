(function ($) {

    //var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    function btnType(v) {
        if (v === BootstrapDialog.TYPE_INFO) {
            return 'btn-info';
        } else if (v === BootstrapDialog.TYPE_PRIMARY) {
            return 'btn-primary';
        } else if (v === BootstrapDialog.TYPE_SUCCESS) {
            return 'btn-success';
        } else if (v === BootstrapDialog.TYPE_WARNING) {
            return 'btn-warning';
        } else if (v === BootstrapDialog.TYPE_DANGER) {
            return 'btn-danger';
        } else {
            return 'btn-default';
        }
    }
    function textType(v) {
        if (v === BootstrapDialog.TYPE_INFO) {
            return 'text-info';
        } else if (v === BootstrapDialog.TYPE_PRIMARY) {
            return 'text-primary';
        } else if (v === BootstrapDialog.TYPE_SUCCESS) {
            return 'text-success';
        } else if (v === BootstrapDialog.TYPE_WARNING) {
            return 'text-warning';
        } else if (v === BootstrapDialog.TYPE_DANGER) {
            return 'text-danger';
        } else {
            return 'text-default';
        }
    }

    $.fn.setPageNumber = function (option) {
        var setting = $.extend({
            data: new Object(),
            callback: function () { }
        }, option);

        return this.each(function () {
            var maxPage = parseInt(setting.data.MaxPage);
            var thisPage = parseInt(setting.data.ThisPage);
            var maxShowPage = 5;
            var totalPage = Math.ceil(maxPage / maxShowPage);
            var allPage = new Array();
            for (var i = 1; i <= totalPage; i++) {
                var objP = new Object();
                var vMax = i * maxShowPage;
                if (i === totalPage) {
                    objP.min = maxPage - (maxShowPage - 1);
                    objP.max = maxPage
                } else {
                    objP.min = vMax - (maxShowPage - 1);
                    objP.max = vMax;
                }
                objP.min = objP.min < 1 ? 1 : objP.min;
                objP.max = objP.max > maxPage ? maxPage : objP.max;
                allPage.push(objP);
            }

            var _this = $(this).empty();

            if (totalPage === 1) {
                var fObj = allPage[0];
                for (var i = fObj.min; i <= fObj.max; i++) {
                    var ac = (i === thisPage) ? 'active' : '';
                    var _html = '<li class="' + ac + ' p' + i + '"><a href="#">' + i + '</a></li>';
                    _this.append(_html).find('.p' + i).data('data', i);
                }
            } else {
                var nSearch = $.grep(allPage, function (ele, index) {
                    return thisPage >= parseInt(ele.min)
                        && thisPage <= parseInt(ele.max)
                });
                var oneRow = new Object();
                if (nSearch.length > 1) {
                    oneRow = nSearch[1];
                } else {
                    oneRow = nSearch[0];
                }

                var _html = '<li class="back"><a href="#"><i class="fa fa-backward"></i></a></li>';
                _this.append(_html);
                _this.removeData('list').data('list', allPage);
                for (var i = 0; i < nSearch.length; i++) {
                    if (JSON.stringify(nSearch[i]) === JSON.stringify(oneRow)) {
                        _this.removeData('index').data('index', i);
                        break;
                    }
                }

                for (var i = oneRow.min; i <= oneRow.max; i++) {
                    var ac = (i === thisPage) ? 'active' : '';
                    _html = '<li class="' + ac + ' p' + i + ' page"><a href="#">' + i + '</a></li>';
                    _this.append(_html).find('.p' + i).data('data', i);
                }
                _html = '<li class="next"><a href="#"><i class="fa fa-forward"></i></a></li>';
                _this.append(_html);
            }
            _this.off('click').on('click', 'li', function () {
                if (!$(this).hasClass('active') && !$(this).hasClass('back') && !$(this).hasClass('next')) {
                    setting.data.ThisPage = $(this).data('data');
                    setting.callback();
                } else if ($(this).hasClass('back')) {
                    setPageClick(_this, 'back', thisPage);
                } else if ($(this).hasClass('next')) {
                    setPageClick(_this, 'next', thisPage);
                }
                return false;
            });

            function setPageClick(_ul, _back_next, _thispage) {
                var _index = parseInt(_ul.data('index'));
                var _list = _ul.data('list');
                if (_back_next === 'back') {
                    _index = _index === 0 ? _index : _index - 1;
                } else if (_back_next === 'next') {
                    _index = _index === _list.length - 1 ? _index : _index + 1;
                }
                _ul.removeData('index').data('index', _index);

                _ul.find('.page').remove();
                var pagenext = _ul.find('.next');
                for (var i = _list[_index].min; i <= _list[_index].max; i++) {
                    var ac = (i === _thispage) ? 'active' : '';
                    var _html = '<li class="' + ac + ' p' + i + ' page"><a href="#">' + i + '</a></li>';
                    $(_html).insertBefore(pagenext).parents('ul').find('.p' + i).data('data', i);
                }
            }
        });
    }

    $.bPopup = function (option) {
        var setting = $.extend({
            title: '',
            url: '',
            type: BootstrapDialog.TYPE_PRIMARY,
            closable: true,
            buttons: new Array(),
            data: new Object(),
            onshow: function () { },
            onshown: function () { },
            onhide: function () { },
            onhidden: function () { },
            size: BootstrapDialog.SIZE_WIDE,
            btnCancel: true
        }, option);

        //var popupstr = 'popup';
        //for (var i = 0; i < 5; i++)
        //    popupstr += possible.charAt(Math.floor(Math.random() * possible.length));

        var btnArray = new Array();
        $.each(setting.buttons, function (key, val) {
            var obj = new Object();
            if (val.id !== undefined)
                obj.id = val.id;
            obj.icon = val.icon;
            obj.label = val.label;
            obj.cssClass = val.cssClass === undefined ? btnType(setting.type) : btnType(val.cssClass);
            obj.action = val.action
            btnArray.push(obj);
        });

        var objD = new Object();
        objD.id = 'btn-close';
        objD.icon = 'fa fa-close';
        objD.label = '&nbsp;Cancel';
        objD.cssClass = btnType(setting.type);
        objD.action = function (k) {
            k.close();
        }
        if (setting.btnCancel)
            btnArray.push(objD);

        BootstrapDialog.show({
            //title: '<i class="fa fa-comments-o fa-1x">&nbsp;&nbsp;</i>' + setting.title,
            title: '<div style="font-size:180%;" class="' + textType(setting.type) + '">' + setting.title + '</div>',
            message: $('<div class="container bPopup" style="width:auto;"></div>').load(setting.url),
            type: setting.type,
            closable: setting.closable,
            buttons: btnArray,
            //cssClass: popupstr,
            data: setting.data,
            onshow: setting.onshow,
            onshown: setting.onshown,
            onhide: setting.onhide,
            onhidden: setting.onhidden,
            size: setting.size
        }).getModalBody().find('.bPopup').wrap('<div class="panel-body"></div>').parent('.panel-body').wrap('<div class="panel panel-primary"></div>').parents('.modal-content').css({ 'background-color': '#EEE' });
        //$('.bPopup').parents('.modal-content').css({
        //    'background-color': '#EEE!important'
        //})
    }

    $.bAlert = function (option) {
        var setting = $.extend({
            title: 'There is an error',
            message: '',
            type: BootstrapDialog.TYPE_DANGER,
            closable: false,
            buttonOK: function (k) {
                k.close();
            },
            size: BootstrapDialog.SIZE_NORMAL
        }, option);

        BootstrapDialog.show({
            title: '<div style="font-size:180%;" class="' + textType(setting.type) + '"><i class="fa fa-exclamation-circle fa-1x">&nbsp;&nbsp;</i>' + setting.title + '</div>',
            message: '<spen class="' + textType(setting.type) + '">' + setting.message + '</spen>',
            type: setting.type,
            closable: setting.closable,
            onshown: function (k) {
                $.material.init();
            },
            buttons: [{
                icon: 'fa fa-close',
                label: '&nbsp;OK',
                cssClass: btnType(setting.type),
                action: setting.buttonOK
            }],
            size: setting.size
        });

    }

    $.bConfirm = function (option) {
        var setting = $.extend({
            title: 'Confirm action',
            message: '',
            type: BootstrapDialog.TYPE_PRIMARY,
            closable: false,
            buttonOK: function (k) {

            },
            buttonCancel: function (k) {
                k.close();
            },
            size: BootstrapDialog.SIZE_NORMAL
        }, option);

        BootstrapDialog.show({
            title: '<div style="font-size:180%;" class="' + textType(setting.type) + '"><i class="fa fa-exclamation-circle fa-1x">&nbsp;&nbsp;</i>' + setting.title + '</div>',
            message: '<spen class="' + textType(setting.type) + '">' + setting.message + '</spen>',
            type: setting.type,
            closable: setting.closable,
            onshown: function (k) {
                $.material.init();
            },
            buttons: [{
                icon: 'fa fa-check',
                label: '&nbsp;OK',
                cssClass: btnType(setting.type),
                action: setting.buttonOK
            }, {
                icon: 'fa fa-close',
                label: '&nbsp;Cancel',
                cssClass: btnType(setting.type),
                action: setting.buttonCancel
            }],
            size: setting.size
        });
    }

    $.bConfirmCritical = function (option) {
        var setting = $.extend({
            title: 'Confirm action',
            message: '',
            type: BootstrapDialog.TYPE_DANGER,
            closable: false,
            buttonOK: function (k) {

            },
            buttonCancel: function (k) {
                k.close();
            },
            size: BootstrapDialog.SIZE_NORMAL
        }, option);

        BootstrapDialog.show({
            title: '<div style="font-size:180%;" class="' + textType(setting.type) + '"><i class="fa fa-exclamation-circle fa-2x">&nbsp;&nbsp;</i>' + setting.title + '</div>',
            message: '<spen class="' + textType(setting.type) + '">' + setting.message + '</spen>',
            type: setting.type,
            closable: setting.closable,
            onshown: function (k) {
                $.material.init();
            },
            buttons: [{
                icon: 'fa fa-check',
                label: '&nbsp;OK',
                cssClass: btnType(setting.type),
                action: setting.buttonOK
            }, {
                icon: 'fa fa-close',
                label: '&nbsp;Cancel',
                cssClass: btnType(setting.type),
                action: setting.buttonCancel
            }],
            size: setting.size
        });
    }

}(jQuery));