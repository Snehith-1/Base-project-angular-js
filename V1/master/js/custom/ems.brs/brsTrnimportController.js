(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnimportController', brsTrnimportController);

    brsTrnimportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnimportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnimportController';
       // console.log('test');
        activate();

        function activate() {
           
            var url = 'api/KotakReconcillation/GetTransactionSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {

                    $scope.BankTransactionSummary = JSON.parse(resp.data.JSONdata);
                    //$scope.transaction_list = resp.data.transaction_list;
                    unlockUI();
                }
                else {
                    unlockUI();
                }
            });
            var url = 'api/KotakReconcillation/GetBankSummaryCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.bankpending_count = resp.data.bankpending_count;
                $scope.bankmatched_count = resp.data.bankmatched_count;


            });
        }
        $scope.bank_pending = function () {
            $state.go('app.brsTrnimport');
        }
        $scope.bank_matched = function () {
            $state.go('app.brsTrnBankMatchedImport');
        }

        $scope.incrementLimit = function () {

            $scope.limitoffset += 6000;

            $scope.limitoffsetinc = $scope.limitoffset;
            $scope.totalcount = parseInt($scope.bankpending_count);
            if ($scope.totalcount < $scope.limitoffsetinc) {
                $scope.increment_flag = 'Y';
            }

            var params = {
                limitoffset_from: $scope.limitoffsetinc
            }
            var url = 'api/KotakReconcillation/GetTransactionSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                /*   if (resp.data.repayment_list != null) {*/
                if (resp.data.status == true) {
                    $scope.BankTransactionSummary = JSON.parse(resp.data.JSONdata);
                    //$scope.repayment_list = resp.data.repayment_list;
                    unlockUI();
                }
                else {
                    unlockUI();
                }

            });

        };
        $scope.decrementLimit = function () {
            $scope.limitoffset -= 6000;
            $scope.limitoffsetdec = $scope.limitoffset;
            if ($scope.limitoffsetdec == '0') {
                $scope.decrement_flag = 'Y';
            }
            var params = {
                limitoffset_from: $scope.limitoffsetdec
            }
            var url = 'api/KotakReconcillation/GetTransactionSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                /*   if (resp.data.repayment_list != null) {*/
                if (resp.data.status == true) {
                    $scope.BankTransactionSummary = JSON.parse(resp.data.JSONdata);
                    //$scope.repayment_list = resp.data.repayment_list;
                    unlockUI();
                }
                else {
                    unlockUI();
                }

            });
        };



        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.deletetemplate = function (reconcildoc_gid) {
            var params = {
                reconcildoc_gid: reconcildoc_gid
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

                    var url = 'api/KotakReconcillation/DeleteTemplatedata';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Transaction !!!', {
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
        $scope.deleteTranactdata = function (banktransc_gid) {
            var params = {
                banktransc_gid: banktransc_gid
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

                    var url = 'api/KotakReconcillation/DeleteTransactiondata';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Transaction !!!', {
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

        $scope.showPopover = function (Questionnarie_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    Questionnarie_gid: Questionnarie_gid
                }
                var url = 'api/FndQuestionnarieMaster/QuestionnarieEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditQuestionnarie_code = resp.data.Questionnarie_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditQuestionnarie = resp.data.Questionnarie_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.Questionnarie_gid = resp.data.Questionnarie_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };



            }
        }

       

        $scope.importexcel = function () {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];

            function ModalInstanceCtrl($scope, $modalInstance) {
               

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            
                var url = 'api/KotakReconcillation/BankNameList';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.bankdtl_list = resp.data.bankdtl_list;
                });


                $scope.excelupload = function (val, val1, name) {
                    var lsbank_gid = '';
                    var lsbank_name = '';
                    if ($scope.cboBankName != undefined || $scope.cboBankName != null) {
                        lsbank_gid = $scope.cboBankName.bankdtl_gid;
                        lsbank_name = $scope.cboBankName.bankdtl_name;
                    }
                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    
                    else {
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('brsbank_gid',lsbank_gid);
                        frm.append('bank_name', lsbank_name);
                        frm.append('project_flag', "Default");
                        // frm.append('questionnarieanswer_gid', questionnarieanswer_gid);
                        $scope.uploadfrm = frm;
                    }
                }
                var limitStep = 6000;
                $scope.incrementLimit = function () {
                    $scope.limit += limitStep;
                };
                $scope.decrementLimit = function () {
                    $scope.limit -= limitStep;
                };

               

                $scope.uploadexcel = function () {
                   
                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {

                        if ($scope.cboBankName.bankdtl_name == "Kotak Mahindra Bank") {

                            var url = 'api/KotakReconcillation/BRSExcelSample';
                            lockUI();
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $modalInstance.close('closed');

                                if (resp.data.status == true) {
                                    //  defaultdynamic();
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                                                                                
                                    // var url = 'api/KotakReconcillation/GetTransactionSummary';
                                    // lockUI();
                                    // SocketService.get(url).then(function (resp) {
                                    //     $scope.transaction_list = resp.data.transaction_list;
                                    //     unlockUI();

                                    // });
                                    // activate();

                                }
                                else {
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                }
                                $("#fileimport").val('');
                                // var url = 'api/KotakReconcillation/GetTransactionSummary';
                                // lockUI();
                                // SocketService.get(url).then(function (resp) {
                                //     $scope.transaction_list = resp.data.transaction_list;
                                //     unlockUI();

                                // });
                                activate();
                            });
                        }
                        else if ($scope.cboBankName.bankdtl_name == "IDFC First Bank") {
                            var url = 'api/IdfcReconcillation/BRSExcelSample';
                            lockUI();
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $modalInstance.close('closed');

                                if (resp.data.status == true) {
                                    //  defaultdynamic();
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                   
                                    // activate();


                                }
                                else {
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                }
                                $("#fileimport").val('');
                              
                                activate();
                            });
                        }
                        
                        else if ($scope.cboBankName.bankdtl_name == "ICICI Bank") {
                            var url = 'api/IciciReconcillation/BRSExcelSample';
                            lockUI();
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $modalInstance.close('closed');

                                if (resp.data.status == true) {
                                    //  defaultdynamic();
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                
                                    // activate();
                                }
                                else {
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                }
                                $("#fileimport").val('');
                                activate();
                            });
                        }
                        else if ($scope.cboBankName.bankdtl_name == "RBL Bank") {
                            var url = 'api/RblReconcillation/BRSExcelSample';
                            lockUI();
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $modalInstance.close('closed');

                                if (resp.data.status == true) {
                                    //  defaultdynamic();
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                  
                                    // activate();
                                }
                                else {
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                }
                                $("#fileimport").val('');
                                activate();
                            });
                        }
                        else if ($scope.cboBankName.bankdtl_name == "State Bank India") {
                            var url = 'api/SbiReconcillation/BRSExcelSample';
                            lockUI();
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $modalInstance.close('closed');

                                if (resp.data.status == true) {
                                    //  defaultdynamic();
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });

                                    // activate();
                                }
                                else {
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                }
                                $("#fileimport").val('');

                                activate();
                            });
                        }
                        else if ($scope.cboBankName.bankdtl_name == "Axis Bank") {
                            var url = 'api/AxisReconcillation/BRSExcelSample';
                            lockUI();
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $modalInstance.close('closed');

                                if (resp.data.status == true) {
                                    //  defaultdynamic();
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                   
                                    // activate();
                                    //  $location.url('app/AtmTrnAudit360View?auditcreation_gid=' + val1 + '&checklistmaster_gid=' + val2 + '&sampleimport_gid=' + val3)

                                }
                                else {
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                }
                                $("#fileimport").val('');
                                activate();
                            });
                        }
                    }

                }

                $scope.uploadexcelcancel = function () {
                    $("#fileimport").val('');
                };


            }


        }

    }

     
    
})();
