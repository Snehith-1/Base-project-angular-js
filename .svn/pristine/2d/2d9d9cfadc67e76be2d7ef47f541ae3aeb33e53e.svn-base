/// <reference path="mstloantermperiodsummarycontroller.js" />
/// <reference path="mstloanpurposecontroller.js" />
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstProgramAddController', MstProgramAddController);

    MstProgramAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService','$anchorScroll'];

    function MstProgramAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService, $anchorScroll) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstProgramAddController';

        $scope.program_gid = $location.search().lsprogram_gid;
        var program_gid = $scope.program_gid;

        activate();

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

            var url = 'api/MstApplication360/ProgramTempClear';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });

            var url = 'api/MstApplication360/GetProgramDropDown';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
                $scope.vertical_list = resp.data.vertical_list;
                unlockUI();
            });
          

            var url = 'api/MstApplication360/GetLoanProduct';
            lockUI();
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

            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cboapprovedadd_list = resp.data.employeelist;
                unlockUI();
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

        $scope.agrloanproductname_change = function (cboloan_product) {
            var params = {
                loanproduct_gid: $scope.cboloan_product.loanproduct_gid,
            }
            var url = 'api/AgrMstApplication360/GetLoanSubProductDropdown';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.agrloansubproduct_data = resp.data.application_list;
            });
        }

        //$scope.entity_change = function (cboentity) {
        //    var params = {
        //        loanproduct_gid: $scope.cboentity.loanproduct_gid,
        //    }
        //    var url = 'api/MstApplication360/GetLoanSubProductDropdown';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        $scope.loansubproduct_data = resp.data.application_list;
        //    });
        //}

        $scope.entity_change = function (cboentity) {
           
            if ($scope.cboentity.entity_name == 'SAMFIN') {
                var url = 'api/MstApplication360/GetProgram2ProductSAMAGROList';
                SocketService.get(url).then(function (resp) {
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
            else if ($scope.cboentity.entity_name == 'SAMAGRO') {
                var url = 'api/MstApplication360/GetProgram2ProductSAMFINList';
                SocketService.get(url).then(function (resp) {
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

                //if (parseFloat($scope.txtprogram_limit) < parseFloat($scope.txtmax_limit)) {
                //        Notify.alert('Maximum Limit Per Transaction Should not be more than Overall Limit', 'warning');

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
            //$scope.mandatoryfields = false;
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

        $scope.product_add = function () {

            if (($scope.cboloan_product == '') || ($scope.cboloan_product == undefined) || ($scope.cboloan_subproduct == '') || ($scope.cboloan_subproduct == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;

                var params = {
                    loanproduct_gid: $scope.cboloan_product.loanproduct_gid,
                    loanproduct_name: $scope.cboloan_product.loanproduct_name,
                    loansubproduct_gid: $scope.cboloan_subproduct.loansubproduct_gid,
                    loansubproduct_name: $scope.cboloan_subproduct.loansubproduct_name,
                    entity_name: $scope.cboentity.entity_name
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
                    product_list();
                    $scope.cboloan_subproduct = '';
                    $scope.cboloan_product == '';
                    
                });
            }
        }
        function product_list() {
            var url = 'api/MstApplication360/GetProgram2ProductList';
            SocketService.get(url).then(function (resp) {
                $scope.loanproduct_list = resp.data.loanproduct_list;

            });
        }

        $scope.product_delete = function (program2loanproduct_gid) {
            var params =
                {
                    program2loanproduct_gid: program2loanproduct_gid
                }
            var url = 'api/MstApplication360/DeleteProgram2Product';
            lockUI();
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

                product_list();
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



                var url = 'api/MstApplication360/ProgramDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    $scope.upload_list = resp.data.upload_list;
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
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
            $scope.upload_list='';
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

        $scope.uploaddocumentcancel = function (tmp_documentGid) {
            lockUI();
            var params = {
                tmp_documentGid: tmp_documentGid
            }
            var url = 'api/MstApplication360/TmpDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
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
        $scope.Back = function ()
        {
            $location.url('app/MstProgram');
        }
        $scope.program_submit = function () {
            if (parseFloat($scope.txtprogram_limit) < parseFloat($scope.txtmax_limit)) {
                    Notify.alert('Maximum Limit Per Transaction Should not be more than Overall Limit', 'warning');

                }
                else {
                var params = {
                    entity_gid: $scope.cboentity.entity_gid,
                    entity_name: $scope.cboentity.entity_name,
                    program_name: $scope.txtprogram_name,
                    approved_date: $scope.txtapproved_date,
                    program_limit: $scope.txtprogram_limit,
                    maximum_limit: $scope.txtmax_limit,
                    lms_code: $scope.txtlms_code,
                    bureau_code: $scope.txtbureau_code,
                    program_description: $scope.txtdescription,             
                    verticalpro: $scope.cbovertical,
                    approvedby: $scope.cboapproved,

                }
            var url = 'api/MstApplication360/PostProgramAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, 'success')
                    $state.go('app.MstProgram');
                }
                else {
                    unlockUI();
                   
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                
            });
        }
        }    

        // $scope.addprogram = function () {
        //     var modalInstance = $modal.open({
        //         templateUrl: '/addprogram.html',
        //         controller: ModalInstanceCtrl,
        //         backdrop: 'static',
        //         keyboard: false,
        //         size: 'md'
        //     });
        //     ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //     function ModalInstanceCtrl($scope, $modalInstance) {

        //         $scope.ok = function () {
        //             $modalInstance.close('closed');
        //         };
        //         $scope.submit = function () {

        //             var params = {
        //                 program: $scope.txtprogram,
        //                 lms_code: $scope.txtlms_code,
        //                 bureau_code: $scope.txtbureau_code

        //             }
        //             var url = 'api/MstApplication360/CreateProgram';
        //             lockUI();
        //             SocketService.post(url, params).then(function (resp) {
        //                 unlockUI();
        //                 if (resp.data.status == true) {

        //                     Notify.alert(resp.data.message, {
        //                         status: 'success',
        //                         pos: 'top-center',
        //                         timeout: 3000
        //                     });
        //                     $modalInstance.close('closed');
        //                     activate();
        //                 }
        //                 else {
        //                     Notify.alert(resp.data.message, {
        //                         status: 'warning',
        //                         pos: 'top-center',
        //                         timeout: 3000
        //                     });
                            
        //                 }
        //             });

        //             $modalInstance.close('closed');

        //         }

        //     }
        // }

        //$scope.editprogram = function (program_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editprogram.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        var params = {
        //            program_gid: program_gid
        //        }
        //        var url = 'api/MstApplication360/EditProgram';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.txteditprogram = resp.data.program;
        //            $scope.txteditlms_code = resp.data.lms_code;
        //            $scope.txteditbureau_code = resp.data.bureau_code;
        //            $scope.program_gid = resp.data.program_gid;
        //        });


        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.update = function () {

        //            var url = 'api/MstApplication360/UpdateProgram';
        //            var params = {
        //                program: $scope.txteditprogram,
        //                lms_code: $scope.txteditlms_code,
        //                bureau_code: $scope.txteditbureau_code,
        //                program_gid: $scope.program_gid
        //            }
        //            SocketService.post(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();

        //                }
        //                else {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //            });
        //            $modalInstance.close('closed');
        //        }
        //    }
        //}

        $scope.Status_update = function (program_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusprogram.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    program_gid: program_gid
                }
                var url = 'api/MstApplication360/EditProgram';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.program_gid = resp.data.program_gid
                    $scope.txtprogram = resp.data.program;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        program_gid: program_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveProgram';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
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
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    program_gid: program_gid
                }

                var url = 'api/MstApplication360/ProgramInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.programinactivelog_list = resp.data.application_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (program_gid) {
            var params = {
                program_gid: program_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstApplication360/DeleteProgram';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Program!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };


        $scope.downloadall = function () {
            for (var i = 0; i < $scope.upload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);
            }
        }
    }
})();

