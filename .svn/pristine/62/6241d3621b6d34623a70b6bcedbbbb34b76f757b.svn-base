(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstProgramEditController', MstProgramEditController);

    MstProgramEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService','$anchorScroll'];

    function MstProgramEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService, $anchorScroll) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstProgramEditController';

        $scope.program_gid = $location.search().lsprogram_gid;
        var program_gid = $scope.program_gid;

        activate();
        lockUI();
        function activate() {
            $scope.amount_validation = true;

            vm.open1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/MstApplication360/GetEntityDropDown';

            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
                //$scope.cbovertical_editlist = resp.data.vertical_list;
                unlockUI();
            });

          

            //var url = 'api/MstApplication360/GetEntity';

            //SocketService.get(url).then(function (resp) {
            //    $scope.entity_list = resp.data.application_list;
            //    unlockUI();
            //});

            var params = {
                program_gid: program_gid
            };
            var url = 'api/MstApplication360/EditProgram';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprogram_refno = resp.data.program_refno;
                $scope.approved_date = resp.data.approved_date;

                $scope.txtdescription = resp.data.program_description;
                $scope.txtprogram_limit = resp.data.program_limit;
                $scope.txtmax_limit = resp.data.maximum_limit;
                $scope.cboentity = resp.data.entity_gid;
                $scope.txtprogram_name = resp.data.program;
                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;
                $scope.program_gid = resp.data.program_gid;

                $scope.cboapproved_editlist = resp.data.approvedby_list;
                $scope.approvedby = resp.data.approvedby;
                $scope.cboapproved_edit = [];
                if (resp.data.approvedby != null) {
                    var count = resp.data.approvedby.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.cboapproved_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.approvedby[i].employee_gid);
                        $scope.cboapproved_edit.push($scope.cboapproved_editlist[indexs]);
                        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                    }
                }

                $scope.cbovertical_editlist = resp.data.programvertical_list;
                //$scope.verticalpro = resp.data.verticalpro;
                $scope.cbovertical_edit = [];
                if (resp.data.verticalpro != null) {
                    var count = resp.data.verticalpro.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.cbovertical_editlist.map(function (x) { return x.vertical_gid; }).indexOf(resp.data.verticalpro[i].vertical_gid);
                        $scope.cbovertical_edit.push($scope.cbovertical_editlist[indexs]);
                        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                    }
                }

                if (resp.data.entity_name == 'SAMFIN') {
                    $scope.Product_Samfin = true;
                    $scope.Product_Samagro = false;
                }
                else if (resp.data.entity_name == 'SAMAGRO') {
                    $scope.Product_Samagro = true;
                    $scope.Product_Samfin = false;

                }
                else {
                    //$scope.Product_Samfin = true;
                    //$scope.Product_Samagro = false;

                }

               

            });



            var url = 'api/MstApplication360/ProgramTempClear';

            SocketService.get(url).then(function (resp) {
                unlockUI();
            });

            


            

            var url = 'api/MstApplication360/GetLoanProduct';

            SocketService.get(url).then(function (resp) {
                $scope.loanproduct_data = resp.data.application_list;
                unlockUI();
            });

            var url = 'api/AgrMstApplication360/GetLoanProduct';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.agrloanproduct_data = resp.data.application_list;
                unlockUI();
            });
            var param = {
                program_gid: program_gid
            };
            var url = 'api/MstApplication360/GetProgram2ProductEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.loanproduct_list = resp.data.loanproduct_list;
                unlockUI();
            });


            var param = {
                program_gid: program_gid
            };
            var url = 'api/MstApplication360/GetProgramDocumentEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.upload_list = resp.data.uploadprogramdocumentlist;
                unlockUI();
            });
        }

        //$scope.entity_change = function () {
        //    var entity_Name = $('#Entity :selected').text();
        //    if (entity_Name == 'SAMFIN') {
        //        $scope.Product_Samfin = true;
        //        $scope.Product_Samagro = false;
        //    }
        //    else if (entity_Name == 'SAMAGRO') {
        //        $scope.Product_Samagro = true;
        //        $scope.Product_Samfin = false;

        //    }
        //    else {
        //        $scope.Product_Samfin = true;
        //        $scope.Product_Samagro = false;

        //    }
        //}

        $scope.entity_change = function () {
            var entity_Name = $('#Entity :selected').text();
            if (entity_Name == 'SAMFIN') {
                var params = {

                    program_gid: program_gid
                }
                var url = 'api/MstApplication360/GetProductSAMAGROTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.samagroloanproduct_list = resp.data.loanproduct_list;
                    if ($scope.samagroloanproduct_list == null) {
                        $scope.Product_Samfin = true;
                        $scope.Product_Samagro = false;
                    }
                    else {
                        $scope.Product_Samagro = true;
                        $scope.Product_Samfin = false;
                        $scope.cboentity = '';
                       
                        Notify.alert('Kindly Delete the product for Samagro', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.hash('SamagroProduct');
                        $anchorScroll();
                    }
                });

            }
            else if (entity_Name == 'SAMAGRO') {
                var params = {

                    program_gid: program_gid
                }
                var url = 'api/MstApplication360/GetProductSAMFINTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.samfinloanproduct_list = resp.data.loanproduct_list;
                    if ($scope.samfinloanproduct_list == null) {

                        $scope.Product_Samagro = true;
                        $scope.Product_Samfin = false;


                    }
                    else {
                        $scope.Product_Samfin = true;
                        $scope.Product_Samagro = false;
                        $scope.cboentity = '';
                        Notify.alert('Kindly Delete the product for Samfin', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        $location.hash('SamfinProduct');
                        $anchorScroll();
                    }
                });



            }
            else {
                //$scope.Product_Samfin = true;
                //$scope.Product_Samagro = false;

            }


        }

        $scope.agrloanproductname_change = function (cboloan_product) {
            var params = {
                loanproduct_gid: $scope.cboloan_product.loanproduct_gid,
            }
            var url = 'api/AgrMstApplication360/GetLoanSubProductDropdown';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.agrloansubproduct_data = resp.data.application_list;
            });
        }
        $scope.loanproductname_change = function (cboloan_product) {
            var params = {
                loanproduct_gid: $scope.cboloan_product.loanproduct_gid,
            }
            var url = 'api/MstApplication360/GetLoanSubProductDropdown';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loansubproduct_data = resp.data.application_list;
            });
        }

       

        $scope.txtmaxlimit_change = function () {
            var input = document.getElementById('txtInput2').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lswords_documentlimit = cmnfunctionService.fnConvertNumbertoWord(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdocument_limit = "";

            }
            else {
                document.getElementById('maxlimitamount_words').innerHTML = lswords_documentlimit;
                //$scope.txtdocument_limit = amount;

                //if ((($scope.txtSanctionAmount.replace(/[\s,]+/g, '').trim()) - ($scope.totaldocumentlimitamount.replace(/[\s,]+/g, '').trim())) < ($scope.txtdocument_limit.replace(/[\s,]+/g, '').trim())) {
                //    $scope.panel1 = false;
                //}
                //else {
                //    $scope.panel1 = true;
                //}

                var txtmax_limit = parseInt(str);
                var lsprogram_limit = parseInt($scope.txtprogram_limit);
                var lsmaximum_limit = parseInt($scope.txtmax_limit);
                if ((txtmax_limit) > (lsprogram_limit - lsmaximum_limit)) {
                    $scope.amount_validation = false;
                }
                else {
                    $scope.amount_validation = true;
                }
                var lsamount = (lsprogram_limit - lsmaximum_limit);
                $scope.txtremaining = (lsamount - txtmax_limit);
            }
            $scope.mandatoryfields = false;
        }

        $scope.txtprogramlimit_change = function () {
            var input = document.getElementById('txtInput1').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lswords_documentlimit = cmnfunctionService.fnConvertNumbertoWord(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdocument_limit = "";

            }
            else {
                document.getElementById('documentlimitamount_words').innerHTML = lswords_documentlimit;
                $scope.txtdocument_limit = amount;

                if ((($scope.txtSanctionAmount.replace(/[\s,]+/g, '').trim()) - ($scope.totaldocumentlimitamount.replace(/[\s,]+/g, '').trim())) < ($scope.txtdocument_limit.replace(/[\s,]+/g, '').trim())) {
                    $scope.panel1 = false;
                }
                else {
                    $scope.panel1 = true;
                }
            }
            $scope.mandatoryfields = false;
        }

        $scope.back = function () {
            $state.go('app.MstProgram');
        };

        $scope.update = function () {
            if (parseFloat($scope.txtprogram_limit) < parseFloat($scope.txtmax_limit)) {
                Notify.alert('Maximum Limit Per Transaction Should not be more than Overall Limit', 'warning');
            }
            else {
                var entity_Name = $('#Entity :selected').text();

                lockUI();

                var params = {
                    entity_gid: $scope.cboentity,
                    entity_name: entity_Name,
                    program: $scope.txtprogram_name,
                    approved_date: $scope.approved_date,
                    program_limit: $scope.txtprogram_limit,
                    maximum_limit: $scope.txtmax_limit,
                    lms_code: $scope.txtlms_code,
                    bureau_code: $scope.txtbureau_code,
                    program_description: $scope.txtdescription,
                    approvedby: $scope.cboapproved_edit,
                    verticalpro: $scope.cbovertical_edit,
                    program_gid: program_gid,                 
                }
                var url = 'api/MstApplication360/UpdateProgram';
                SocketService.post(url, params).then(function (resp) {

                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.MstProgram');
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }

        $scope.product_add = function () {

            if (($scope.cboloan_product == '') || ($scope.cboloan_product == undefined) || ($scope.cboloan_subproduct == '') || ($scope.cboloan_subproduct == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;
                var entity_Name = $('#Entity :selected').text();

                var params = {
                    loanproduct_gid: $scope.cboloan_product.loanproduct_gid,
                    loanproduct_name: $scope.cboloan_product.loanproduct_name,
                    loansubproduct_gid: $scope.cboloan_subproduct.loansubproduct_gid,
                    loansubproduct_name: $scope.cboloan_subproduct.loansubproduct_name,
                    entity_name: entity_Name
                }
                var url = 'api/MstApplication360/PostProductDetails';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.cboloan_subproduct = '';
                        $scope.cboloan_product = '';
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var params = {

                        program_gid: program_gid
                    }
                    lockUI();
                    var url = 'api/MstApplication360/GetProgram2ProductEditTempList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.loanproduct_list = resp.data.loanproduct_list;
                        unlockUI();
                    });
                    $scope.cboloan_subproduct = '';
                    $scope.cboloan_product == '';

                });
            }
        }

        $scope.product_editdelete = function (program2loanproduct_gid) {
            var params =
                {
                    program2loanproduct_gid: program2loanproduct_gid,
                    program_gid: program_gid,
                }
            lockUI();
            var url = 'api/MstApplication360/DeleteProgram2Product';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {

                    program_gid: program_gid
                }
                lockUI();
                var url = 'api/MstApplication360/GetProgram2ProductEditTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loanproduct_list = resp.data.loanproduct_list;
                    unlockUI();
                });
            });

        }

        $scope.ProgramDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                frm.append('project_flag', "Default");
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i])
                   
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                }




                var url = 'api/MstApplication360/PrograEditDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    unlockUI();
                    $("#file").val('');

                    var param = {
                        program_gid: $scope.program_gid
                    };
                    var url = 'api/MstApplication360/GetProgramDocumentTempEditList';
                    SocketService.getparams(url, param).then(function (resp) {

                        $scope.upload_list = resp.data.uploadprogramdocumentlist;
                    });

                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });
            }
            else {
                alert('Please select a file.')
            }
            $scope.upload_list = '';
        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.uploaddocumentcancel = function (programdocument_gid) {
            lockUI();
            var params = {
                program_gid: program_gid,
                programdocument_gid: programdocument_gid
            }
            var url = 'api/MstApplication360/GetProgramDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                var param = {
                    program_gid: program_gid
                };
                var url = 'api/MstApplication360/GetProgramDocumentTempEditList';
                SocketService.getparams(url, param).then(function (resp) {

                    $scope.upload_list = resp.data.uploadprogramdocumentlist;
                });
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.upload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);
            }
        }

    }
})();