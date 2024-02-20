(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsMstTemplateSummaryController', brsMstTemplateSummaryController);

    brsMstTemplateSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function brsMstTemplateSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsMstTemplateSummaryController';
      activate();

        function activate() {
          
            
           

            var url = 'api/KotakReconcillation/GetUploadTemplateSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {

                if (resp.data.status == true) {
                    $scope.uploadtemplatelist = resp.data.uploadtemplatelist;

                    unlockUI();
                }
                else {
                    unlockUI();
                }               
            });
         var url = "api/RepaymentReconcillation/GetRepaymentTemplateCount";
            SocketService.get(url).then(function (resp) {
                $scope.transac_count = resp.data.transac_count;
                $scope.repayment_count = resp.data.repayment_count;
               
                unlockUI();
            });

        $scope.transac_template = function () {
            $state.go('app.brsMstTemplateSummary');
        }
          

        $scope.repayment_template = function () {
            $state.go('app.brsMstRepaymentTemplate');
        }
        
        $scope.transfer = function (banktransc_gid) {
            $location.url('app/brsTrnImportView?lsbanktransc_gid=' + banktransc_gid );
        }
        $scope.addQuestionTitle = function () {
            $state.go('app.FndAddQuestion');
        }
        $scope.Back = function () {
            activate();
            $state.go('app.FndMstQuestionBankMaster');
        }
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

            
                var url = 'api/MstAppCreditUnderWriting/BankNameList';
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
                    // else if (filePath.includes("ImportQuestionsMaster") == false) {
                    //     Notify.alert('File Name / Template Not Supported!', 'warning')
                    //     $modalInstance.close('closed');
                    // }
                    else {
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('brsbank_gid',lsbank_gid);
                        frm.append('bank_name',lsbank_name);
                        // frm.append('questionnarieanswer_gid', questionnarieanswer_gid);
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {
                    
                   
                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {

                        if ($scope.cboBankName.bankdtl_name == "KOTAK") {

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
                                    activate();
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
                            });
                        }
                        else if ($scope.cboBankName.bankdtl_name == "IDFC") {
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
                                    activate();
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
                            });
                        }
                        else if ($scope.cboBankName.bankdtl_name == "ICICI") {
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
                                    activate();
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
                            });
                        }
                        else if ($scope.cboBankName.bankdtl_name == "RBL") {
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
                                    activate();
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
                            });
                        }
                        else if ($scope.cboBankName.bankdtl_name == "AXIS") {
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
                                    activate();
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
                            });
                        }
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

