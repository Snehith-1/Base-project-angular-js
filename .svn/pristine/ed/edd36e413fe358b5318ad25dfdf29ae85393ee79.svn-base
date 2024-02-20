(function ($) {
    $.fn.setMainPage = function (option) {

        var setting = $.extend({
            DataColumns: new Array(),
            DataColumnsOrder: [[0, 'asc']],
            FunLoadData: function () { },
            btnSearch: true,
            btnNew: false,
            btnNewFun: function () { },
            btnDelete: false,
            btnDeleteFun: function () { },
            btnDeleteAll: false,
            btnEdit: false,
            btnEditFun: function () { },
            headerString: '',
            rowNum: [[10, 25, 50, -1], ['10 Items', '25 Items', '50 Items', 'Show All']],
            FundCallback: function () { },
            valSearch: '',
            DataColumnsDefs: new Array(),
            responsive: true
        }, option);

        return this.each(function () {
            var formId = $(this);
            var tblName = 'tbl' + formId.attr('id');
            var btnAdd = tblName + 'btnAdd';
            var btnEdit = tblName + 'btnEdit';
            var btnRemove = tblName + 'btnRemove';
            var btnRemoveMenu = tblName + 'btnRemoveMenu';
            var btnChecked = tblName + 'btnChecked';
            var btnUnChecked = tblName + 'btnUnChecked';
            var btnRef = tblName + 'btnRef';
            var btnPages = tblName + 'btnPages';
            var txtSearch = tblName + 'txtSearch';
            var chk = tblName + 'chk';
            var _html = '';
            _html = '<div class="">';
            _html += '<div class="row">';
            if ($.trim(setting.headerString).length > 0)
                _html += '<div class="page-header col-lg-12"><h3>' + setting.headerString + '</h3></div>';

            _html += '<div class="col-xs-6">';
            _html += '<div class="form-group label-floating" style="padding-left: 12px;">';
            _html += '<div class="btn-group" style="margin-top: 0px;">';

            if (setting.btnNew)
                _html += '<div class="btn btn-primary btn-raised ' + btnAdd + '"><i class="fa fa-cloud"></i><spen class="hidden-xs hidden-sm"> New</spen></div>';

            if (setting.btnEdit) {
                if (setting.btnDelete && setting.btnDeleteAll === false) {
                    _html += '<div class="btn btn-danger btn-raised ' + btnEdit + '"><i class="fa fa-pencil"></i><spen class="hidden-xs hidden-sm"> Edit</spen></div>';
                }
            }

            if (setting.btnDeleteAll) {
                _html += '<div class="btn btn-danger btn-raised ' + btnRemove + '"><i class="fa fa-remove"></i><spen class="hidden-xs hidden-sm"> Delete</spen></div>';
                _html += '<div class="btn btn-danger btn-raised dropdown-toggle ' + btnRemoveMenu + '" data-toggle="dropdown">';
                _html += '<span class="caret"></span>';
                _html += '</div>';
                _html += '<ul class="dropdown-menu dropdown-menu-right">';
                _html += '<li><a href="#" class="danger ' + btnChecked + '"><i class="fa fa-check-square" style="min-width:20px;">&nbsp;</i>Select</a></li>';
                _html += '<li><a href="#" class="danger ' + btnUnChecked + '"><i class="fa fa-square-o" style="min-width:20px;">&nbsp;</i>Unselect</a></li>';
                _html += '</ul>';
            } else if (setting.btnDelete) {
                _html += '<div class="btn btn-danger btn-raised ' + btnRemove + '"><i class="fa fa-remove"></i><spen class="hidden-xs hidden-sm"> Delete</spen></div>';
            }
            _html += '</div>';
            _html += '</div>';
            _html += '</div>';

            //alert(setting.FunLoadData());
            _html += '<div class="col-xs-6 text-right">';
            if (setting.btnSearch) {
                _html += '<div class="form-group label-floating">';
                _html += '<div class="input-group">';
                _html += '<span class="input-group-addon hidden-xs hidden-sm" ><i class="fa fa-search"></i></span>';
                _html += '<label class="control-label hidden-xs hidden-sm" for="' + txtSearch + '">Specify the text to search.</label>';
                _html += '<input type="text" name="' + txtSearch + '" id="' + txtSearch + '" class="form-control"  />';


                //_html += '<span class="input-group-addon btn-raised hidden-xs">';
                //_html += '<span class="fa fa-search"></span>';
                //_html += '</span>';
                //_html += '<input type="text" name="' + txtSearch + '" id="' + txtSearch + '" class="form-control" placeholder="ระบุข้อความที่ต้องการค้นหา" />';
                //_html += '<div class="input-group-btn">';
                //_html += '<div class="btn btn-primary btn-raised ' + btnRef + '"><i class="fa fa-refresh"></i></div>';
                //_html += '</div>';
                _html += '<div class="input-group-btn">';
                //_html += '<div class="btn-group">';
                _html += '<div class="btn btn-primary btn-raised ' + btnRef + '"><i class="fa fa-refresh"></i></div>';
                _html += '<div class="btn btn-primary btn-raised btn-secondary btnList dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding-left: 8px;padding-right: 8px;">';
                _html += '<span class="caret"></span>';
                _html += '</div>';
                _html += '<ul class="dropdown-menu dropdown-menu-right ulPages" style="right: 12px;margin-top: -6px;border-radius: 0px 0px 2px 2px;">';
                $.each(setting.rowNum[0], function (key, val) {
                    if (val === -1) {
                        _html += '<li role="separator" class="divider"></li>';
                        _html += '<li value="' + val + '"><a href="#"><i class="fa fa-chevron-circle-right" style="min-width:20px;"></i>  ' + setting.rowNum[1][key] + '</a></li>';
                    } else {
                        _html += '<li value="' + val + '"><a href="#"><i class="fa fa-chevron-circle-right" style="min-width:20px;"></i>  ' + setting.rowNum[1][key] + '</a></li>';
                    }
                });
                _html += '</ul>';
                //_html += '</div>';
                _html += '</div>';
            } else {
                _html += '<div class="input-group" style="display:inline-block;">';
            }
            //_html += '<div class="input-group-btn">';
            //_html += '<div class="btn btn-primary ' + btnPages + '"><i class="glyphicon glyphicon-duplicate"></i></div>';
            //_html += '<div class="btn btn-primary btn-raised dropdown-toggle" data-toggle="dropdown">';
            //_html += '<i class="fa fa-reorder"></i> <span class="caret"></span>';
            //_html += '</div>';

            _html += '</div>';

            _html += '</div>';
            _html += '</div>';
            _html += '</div>';


            _html += '<div class="col-lg-12 col-xs-12">';
            _html += '<table id="' + tblName + '" class="display nowrap responsive" cellspacing="0" style="width:100%;">';
            _html += '<thead>';
            _html += '<tr>';
            $.each(setting.DataColumns, function (key, val) {
                _html += '<th>' + val.header + '</th>';
            });
            _html += '</tr>';
            _html += '</thead>';
            _html += '</table>';
            _html += '</div>';
            _html += '</div>';
            _html += '</div>';

            formId.empty().append(_html).find('#' + tblName).each(function (key, val) {
                //alert('s');
                var _colDefs = new Array();
                if (setting.btnDeleteAll) {
                    _colDefs.push({
                        render: function (row, type, val2, meta) {
                            _html = '<div class="checkbox-none-text checkbox-primary ">';
                            _html += '<input id="' + (chk + val2.key) + '" type="checkbox" class="' + chk + '" xData="' + val2.key + '">';
                            _html += '<label for="' + (chk + val2.key) + '" style="padding-left: 5px!important;font-size:100%!important;font-weight:100!important;color:#000!important;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
                            _html += row;
                            _html += '</label>';
                            _html += '</div>';
                            return _html;
                        },
                        orderable: true,
                        targets: 0
                    });
                }
                $.each(setting.DataColumnsDefs, function (k, v) {
                    _colDefs.push(v);
                });

                var tblEmp = $(val).DataTable({
                    processing: false,
                    responsive: setting.responsive,
                    //autoWidth: true,
                    //data: setting.FunLoadData,
                    //scrollY: 300,
                    //paging: false,
                    order: setting.DataColumnsOrder,
                    columns: setting.DataColumns,
                    //columnDefs: !setting.btnDeleteAll ? [{}] :
                    //    [{
                    //        render: function (row, type, val2, meta) {
                    //            _html = '<div class="checkbox-none-text checkbox-primary ">';
                    //            _html += '<input id="' + (chk + val2.key) + '" type="checkbox" class="' + chk + '" xData="' + val2.key + '">';
                    //            _html += '<label for="' + (chk + val2.key) + '" style="padding-left: 5px!important;font-size:100%!important;font-weight:100!important;color:#000!important;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
                    //            _html += row;
                    //            _html += '</label>';
                    //            _html += '</div>';
                    //            //_html = '<div class="checkbox">';
                    //            //_html += '<label>';
                    //            //_html += '<input type="checkbox" checked=""> ' + row;
                    //            //_html += '</label>';
                    //            //_html += '</div>';
                    //            return _html;
                    //        },
                    //        orderable: true,
                    //        targets: 0
                    //    }],
                    columnDefs: _colDefs,
                    processing: true,
                    initComplete: function (settings) {

                    },
                    rowCallback: function (row, data, index) {
                        if (index === 0)
                            tblEmp.responsive.recalc();
                    },
                    fnDrawCallback: function (settings) {

                    },
                    pageLength: -1
                });
                setting.FunLoadData(tblEmp, formId);

                formId.find('.ulPages > li:not(.divider)').off('click').on({
                    click: function (e) {
                        e.preventDefault();
                        var rowCount = parseInt($(this).val());
                        if (rowCount > -1) {
                            formId.find('.dataTables_paginate').show();
                            tblEmp.page.len(rowCount).draw();
                        } else {
                            formId.find('.dataTables_paginate').hide();
                            tblEmp.page.len(rowCount).draw();
                        }
                    }
                }).eq(0).click();

                if (setting.btnDelete === true && setting.btnDeleteAll === false) {
                    formId.find('#' + tblName + ' tbody').on('click', 'tr > td:not(:first-child)', function () {
                        var vTR = $(this).parent('tr');
                        if (vTR.hasClass('selected')) {
                            vTR.removeClass('selected');
                        } else {
                            tblEmp.$('tr.selected').removeClass('selected');
                            vTR.addClass('selected');
                        }
                    });
                } else if ((setting.btnEdit && setting.btnDeleteAll) || (setting.btnEdit && setting.btnDeleteAll == false)) {
                    formId.find('#' + tblName + ' tbody').on('click', 'tr > td:not(:first-child)', function () {
                        var vdata = tblEmp.row($(this).parent('tr')).data();
                        setting.btnEditFun(formId, vdata);
                    });
                }

                formId.on('input propertychange paste', '#' + txtSearch, function () {
                    tblEmp.search($(this).val()).draw();
                });

                var fsearch = $('#' + txtSearch).prop('value', setting.valSearch);
                tblEmp.search(fsearch.val()).draw();

                formId.find('.' + btnRef).off('click').on({
                    click: function () {
                        //tblEmp.clear().rows.add(setting.FunLoadData());
                        setting.FunLoadData(tblEmp, formId);
                    }
                });

                formId.find('.' + btnChecked).off('click').on({
                    click: function (e) {
                        e.preventDefault();
                        formId.find('.' + chk).prop('checked', true);
                    }
                });

                formId.find('.' + btnUnChecked).off('click').on({
                    click: function (e) {
                        e.preventDefault();
                        formId.find('.' + chk).prop('checked', false);
                    }
                });

                formId.find('.' + btnAdd).off('click').on({
                    click: function () {
                        setting.btnNewFun(formId);
                    }
                })

                formId.find('.' + btnRemove).off('click').on({
                    click: function () {
                        if (setting.btnDeleteAll) {
                            var vdata = new Array();
                            formId.find('#' + tblName + ' tbody .' + chk + ':checked').each(function (key, val) {
                                vdata.push(tblEmp.row($(val).parents('tr')).data().key);
                            });
                            setting.btnDeleteFun(formId, vdata);
                        } else {
                            var vdata = tblEmp.row(formId.find('#' + tblName + ' tbody tr[role=row].selected')).data().key;
                            setting.btnDeleteFun(formId, vdata);
                        }

                    }
                });

                formId.find('.' + btnEdit).off('click').on({
                    click: function () {
                        var vdata = tblEmp.row(formId.find('#' + tblName + ' tbody tr[role=row].selected')).data();
                        setting.btnEditFun(formId, vdata);
                    }
                });

                setting.FundCallback(formId);
            });
            //var tblEmp = formId.find('#' + tblName)
        })
    };

    $.fn.setEvenValidator = function (option) {
        var setting = $.extend({
            btnActive: new Array()
        }, option);

        return this.each(function () {
            var _thisF = $(this);
            $(this).on('success.field.fv err.field.fv', function (e) {
                _thisF.find('[data-fv-result="INVALID"]').removeAttr('style');
                $.each(setting.btnActive, function (key, val) {
                    if (_thisF.find('[data-fv-result="INVALID"]').length > 0) {
                        $(val).prop('disabled', true).addClass('disabled');
                    } else {
                        $(val).prop('disabled', false).removeClass('disabled');
                    }
                });
            });
        });
    }

    $.searchData = function (option) {
        var setting = $.extend({
            fromGrid: '',
            opt: 'get',
            callback: function () { }
        }, option);

        var fromThis = $(setting.fromGrid);
        var objName = 'tbl' + fromThis.attr('id');
        //var vDataTable = $('#' + objName).DataTable();
        //var vDataTable = new $.fn.dataTable.Api($('#' + objName));
        var vopt = $.trim(setting.opt).toUpperCase();
        if (vopt === 'GET') {
            var chk = $('.' + objName + 'chk:checked');
            var datakey = new Array();
            $.each(chk, function (key, val) {
                datakey.push($(val).attr('xData'));
            });
            setting.callback(fromThis, chk, datakey);
        }
    }

    $.fn.searchData = function (option) {
        var setting = $.extend({
            headerString: '',
            btnSearch: true,
            DataColumnsOrder: [[0, 'asc']],
            DataColumns: new Array(),
            rowNum: [[10, 25, 50, -1], ['10 Items', '25 Items', '50 Items', 'Show All']],
            FunLoadData: function () { },
            btnClickRowFun: function () { },
            FundCallback: function () { },
            multiselect: false,
            btnSelectAll: 'Selected',
            btnUnSelectAll: 'Unselected',
            rowClick: true,
            rowClickFun: function () { },
            rowDbClick: true,
            rowDbClickFun: function () { },
            DataColumnsDefs: new Array()
        }, option);

        return this.each(function () {
            var formId = $(this);
            var tblName = 'tbl' + formId.attr('id');
            var btnRef = tblName + 'btnRef';
            var btnPages = tblName + 'btnPages';
            var txtSearch = tblName + 'txtSearch';
            var btnClickRow = tblName + 'Click';
            var btnSelect = tblName + 'Selected';
            var btnUnSelect = tblName + 'Unselected';
            var chk = tblName + 'chk';

            var _html = '';
            _html = '<div class="">';
            _html += '<div class="row">';
            if ($.trim(setting.headerString).length > 0)
                _html += '<div class="page-header col-lg-12"><h3>' + setting.headerString + '</h3></div>';

            _html += '<div class="col-xs-6">';
            _html += '<div class="form-group label-floating" style="padding-left: 12px;">';
            _html += '<div class="btn-group" style="margin-top: 0px;">';
            if (setting.multiselect) {
                _html += '<div class="btn btn-warning btn-raised ' + btnSelect + '"><i class="fa fa-dot-circle-o"></i><spen class="hidden-xs hidden-sm"> ' + setting.btnSelectAll + '</spen></div>';
                _html += '<div class="btn btn-warning btn-raised ' + btnUnSelect + '"><i class="fa fa-circle-o"></i><spen class="hidden-xs hidden-sm"> ' + setting.btnUnSelectAll + '</spen></div>';
                //_html += '<div class="btn btn-danger dropdown-toggle ' + btnRemoveMenu + '" data-toggle="dropdown">';
                //_html += '<span class="caret"></span>';
                //_html += '</div>';
                //_html += '<ul class="dropdown-menu dropdown-menu-left">';
                //_html += '<li><a href="#" class="danger ' + btnChecked + '"><i class="fa fa-check-square">&nbsp;</i>เลือก</a></li>';
                //_html += '<li><a href="#" class="danger ' + btnUnChecked + '"><i class="fa fa-square-o">&nbsp;</i>ไม่เลือก</a></li>';
                //_html += '</ul>';
            }
            _html += '</div>';
            _html += '</div>';
            _html += '</div>';

            _html += '<div class="col-xs-6 text-right">';
            if (setting.btnSearch) {
                _html += '<div class="form-group label-floating">';
                _html += '<div class="input-group">';
                _html += '<span class="input-group-addon hidden-xs hidden-sm" ><i class="fa fa-search"></i></span>';
                _html += '<label class="control-label hidden-xs hidden-sm" for="' + txtSearch + '">Specify the text to search.</label>';
                _html += '<input type="text" name="' + txtSearch + '" id="' + txtSearch + '" class="form-control"  />';


                //_html += '<span class="input-group-addon btn-raised hidden-xs">';
                //_html += '<span class="fa fa-search"></span>';
                //_html += '</span>';
                //_html += '<input type="text" name="' + txtSearch + '" id="' + txtSearch + '" class="form-control" placeholder="ระบุข้อความที่ต้องการค้นหา" />';
                //_html += '<div class="input-group-btn">';
                //_html += '<div class="btn btn-primary btn-raised ' + btnRef + '"><i class="fa fa-refresh"></i></div>';
                //_html += '</div>';
                _html += '<div class="input-group-btn">';
                //_html += '<div class="btn-group">';
                _html += '<div class="btn btn-primary btn-raised ' + btnRef + '"><i class="fa fa-refresh"></i></div>';
                _html += '<div class="btn btn-primary btn-raised btn-secondary btnList dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="padding-left: 8px;padding-right: 8px;">';
                _html += '<span class="caret"></span>';
                _html += '</div>';
                _html += '<ul class="dropdown-menu dropdown-menu-right ulPages" style="right: 12px;margin-top: -6px;border-radius: 0px 0px 2px 2px;">';
                $.each(setting.rowNum[0], function (key, val) {
                    if (val === -1) {
                        _html += '<li role="separator" class="divider"></li>';
                        _html += '<li value="' + val + '"><a href="#"><i class="fa fa-chevron-circle-right" style="min-width:20px;"></i>  ' + setting.rowNum[1][key] + '</a></li>';
                    } else {
                        _html += '<li value="' + val + '"><a href="#"><i class="fa fa-chevron-circle-right" style="min-width:20px;"></i>  ' + setting.rowNum[1][key] + '</a></li>';
                    }
                });
                _html += '</ul>';
                //_html += '</div>';
                _html += '</div>';
            } else {
                _html += '<div class="input-group" style="display:inline-block;">';
            }
            //_html += '<div class="input-group-btn">';
            //_html += '<div class="btn btn-primary ' + btnPages + '"><i class="glyphicon glyphicon-duplicate"></i></div>';
            //_html += '<div class="btn btn-primary btn-raised dropdown-toggle" data-toggle="dropdown">';
            //_html += '<i class="fa fa-reorder"></i> <span class="caret"></span>';
            //_html += '</div>';

            _html += '</div>';

            _html += '</div>';
            _html += '</div>';
            _html += '</div>';


            _html += '<div class="col-lg-12 col-xs-12">';
            _html += '<table id="' + tblName + '" class="display nowrap responsive" cellspacing="0" style="width:100%;">';
            _html += '<thead>';
            _html += '<tr>';
            $.each(setting.DataColumns, function (key, val) {
                _html += '<th>' + val.header + '</th>';
            });
            _html += '</tr>';
            _html += '</thead>';
            _html += '</table>';
            _html += '</div>';
            _html += '</div>';
            _html += '</div>';

            formId.empty().append(_html).find('#' + tblName).each(function (key, val) {
                var _colDefs = new Array();
                if (setting.multiselect) {
                    _colDefs.push({
                        render: function (row, type, val2, meta) {
                            _html = '<div class="checkbox-none-text checkbox-primary ">';
                            var attrL = '';
                            for (var nam in val2) {
                                attrL += ' data-' + nam + '="' + $.trim(val2[nam]) + '"';
                            }
                            _html += '<input id="' + (chk + val2.key) + '" type="checkbox" class="' + chk + '" xData="' + $.trim(val2.key) + '"' + attrL + '>';
                            _html += '<label for="' + (chk + val2.key) + '" style="padding-left: 5px!important;font-size:100%!important;font-weight:100!important;color:#000!important;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
                            _html += row;
                            _html += '</label>';
                            _html += '</div>';
                            return _html;
                        },
                        orderable: true,
                        targets: 0
                    });
                }
                $.each(setting.DataColumnsDefs, function (k, v) {
                    _colDefs.push(v);
                });

                var tblEmp = $(val).DataTable({
                    processing: false,
                    responsive: true,
                    order: setting.DataColumnsOrder,
                    columns: setting.DataColumns,
                    //columnDefs: !setting.multiselect ? [{}] :
                    //    [{
                    //        render: function (row, type, val2, meta) {
                    //            _html = '<div class="checkbox-none-text checkbox-primary ">';

                    //            var attrL = '';
                    //            for (var nam in val2) {
                    //                attrL += ' data-' + nam + '="' + $.trim(val2[nam]) + '"';
                    //            }

                    //            _html += '<input id="' + (chk + val2.key) + '" type="checkbox" class="' + chk + '" xData="' + $.trim(val2.key) + '"' + attrL + '>';
                    //            _html += '<label for="' + (chk + val2.key) + '" style="padding-left: 5px!important;font-size:100%!important;font-weight:100!important;color:#000!important;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
                    //            _html += row;
                    //            _html += '</label>';
                    //            _html += '</div>';
                    //            return _html;
                    //        },
                    //        orderable: true,
                    //        targets: 0
                    //    }],
                    columnDefs: _colDefs,
                    initComplete: function (settings) {

                    },
                    rowCallback: function (row, data, index) {
                        if (index === 0)
                            tblEmp.responsive.recalc();
                    },
                    fnDrawCallback: function (settings) {

                    },
                    pageLength: -1
                });
                setting.FunLoadData(tblEmp, formId);

                formId.find('.ulPages > li:not(.divider)').off('click').on({
                    click: function (e) {
                        e.preventDefault();
                        var rowCount = parseInt($(this).val());
                        if (rowCount > -1) {
                            formId.find('.dataTables_paginate').show();
                            tblEmp.page.len(rowCount).draw();
                        } else {
                            formId.find('.dataTables_paginate').hide();
                            tblEmp.page.len(rowCount).draw();
                        }
                    }
                }).eq(0).click();

                if (setting.rowClick) {
                    formId.find('#' + tblName + ' tbody').on('click', 'tr > td:not(:first-child)', function () {
                        var vdata = tblEmp.row($(this).parent('tr')).data();
                        setting.rowClickFun(formId, vdata);
                    });
                }

                if (setting.rowDbClick) {
                    formId.find('#' + tblName + ' tbody').on('dblclick', 'tr > td:not(:first-child)', function () {
                        var vdata = tblEmp.row($(this).parent('tr')).data();
                        setting.rowDbClickFun(formId, vdata);
                    });
                }

                formId.on('input propertychange paste', '#' + txtSearch, function () {
                    tblEmp.search($(this).val()).draw();
                });

                formId.find('.' + btnSelect).off('click').on({
                    click: function (e) {
                        e.preventDefault();
                        formId.find('.' + chk).prop('checked', true);
                    }
                });

                formId.find('.' + btnUnSelect).off('click').on({
                    click: function (e) {
                        e.preventDefault();
                        formId.find('.' + chk).prop('checked', false);
                    }
                });

                formId.find('.' + btnRef).off('click').on({
                    click: function () {
                        //tblEmp.clear().rows.add(setting.FunLoadData());
                        setting.FunLoadData(tblEmp, formId);
                    }
                });

                //formId.find('.' + btnClickRow).off('click').on({
                //    click: function () {
                //        var vdata = tblEmp.row(formId.find('#' + tblName + ' tbody tr[role=row].selected')).data();
                //        setting.btnClickRowFun(formId, vdata);
                //    }
                //});

                //if (setting.multiselect === false) {
                formId.find('#' + tblName + ' tbody').on('click', 'tr > td:not(:first-child)', function () {
                    var vdata = tblEmp.row($(this).parent('tr')).data();
                    setting.btnClickRowFun(formId, vdata);
                });
                //}

                setting.FundCallback(formId);

                formId.data('table_tool', tblEmp);
            });
        });
    }
}(jQuery));

function chkForms(v) {
    $(v).submit().find('[data-fv-result="INVALID"]').removeAttr('style');
    if ($(v).find('[data-fv-result="INVALID"]').length > 0) {
        return false;
    } else {
        return true;
    }
}