(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BRSActivityController', BRSActivityController);

        BRSActivityController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function BRSActivityController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'BRSActivityController';

        activate();

        function activate() { 
          
            var url = 'api/BRSMaster/GetBRSActivity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.BRSactivity_list = resp.data.BRSActivity_List;
                unlockUI();
            });
        }

        $scope.addbrsactivity = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addbrsactivity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        brsactivity_name: $scope.txtbrsactivity_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/BRSMaster/CreateBRSActivity';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    }); 

                    $modalInstance.close('closed');

                }
                
            }
        }

        $scope.editbrsactivity = function (brsactivity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbrsactivity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                 var params = {
                    brsactivity_gid: brsactivity_gid
                }
                 var url = 'api/BRSMaster/EditBRSActivity';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditbrsactivity_name = resp.data.brsactivity_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.brsactivity_gid = resp.data.brsactivity_gid;
                }); 
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                
               
                $scope.update = function () {

                    var url = 'api/BRSMaster/UpdateBRSActivity';
                    var params = {
                        brsactivity_name: $scope.txteditbrsactivity_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        brsactivity_gid: $scope.brsactivity_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    }); 

                }
                
            }
        }

        $scope.Status_update = function (brsactivity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusbrsactivity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    brsactivity_gid: brsactivity_gid
                }
                var url = 'api/BRSMaster/EditBRSActivity';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtbrsactivity_name = resp.data.brsactivity_name;
                   $scope.brsactivity_gid = resp.data.brsactivity_gid;
                    $scope.rbo_status = resp.data.status_log;
                });    
                var url = 'api/BRSMaster/GetBRSActivityActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.brsactivityinactive_list = resp.data.BRSActivity_List;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        brsactivity_name: $scope.txtbrsactivity_name,
                        remarks: $scope.txtremarks,
                        status_log:$scope.rbo_status,
                        brsactivity_gid:brsactivity_gid
                    }
                    var url = 'api/BRSMaster/BRSActivityStatusUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }activate();
                    }); 

                    $modalInstance.close('closed');

                }
                
            }
        }

        $scope.delete = function (brsactivity_gid) {
             var params = {
                brsactivity_gid: brsactivity_gid
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
                            lockUI();
                            var url = 'api/BRSMaster/BRSActivityDelete';
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    SweetAlert.swal('Deleted Successfully!');
                                    activate();
                                }
                                else {
                                    alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                    unlockUI;
                                }
                            });
                            }
                    });
        }
    }
})();


// JavaScript source code

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BrsMstCloseRemainingAmountController', BrsMstCloseRemainingAmountController);

    BrsMstCloseRemainingAmountController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function BrsMstCloseRemainingAmountController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        var vm = this;
        vm.title = 'BrsMstCloseRemainingAmountController';

        activate();

        function activate() {

            var url = 'api/CloseRemaindingAmount/GetRemaindingAmount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.Remaining_lits = resp.data.remaindingamount_list;
                unlockUI();
            });
            var url = 'api/CloseRemaindingAmount/GetRemainingAmountStatus';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.activeamount_count = resp.data.activeamount_count;
                unlockUI();
            });
        }

        $scope.addRemainingAmount = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addRemainingAmount.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        remaindingamount_name : $scope.txtremainding_name,
                        remaindingamount_amount: $scope.txtremainding_amount,
                        remarks: $scope.txtremarks
                    }
                    var url = 'api/CloseRemaindingAmount/CreateRemaindingAmount';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                    $modalInstance.close('closed');

                }

            }
        }
        $scope.Status_update = function (remaindingamount_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/status.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    remaindingamount_gid: remaindingamount_gid
                }
                var url = 'api/CloseRemaindingAmount/GetRemainingAmount';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.remaindingamount_gid = resp.data.remaindingamount_gid;
                    $scope.txtremainding_amount = resp.data.remaindingamount_amount;
                    $scope.txtremarks = resp.data.remarks;
                    $scope.rbo_status = resp.data.Status;


                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        remaindingamount_amount: $scope.txtremainding_amount,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        remaindingamount_gid: remaindingamount_gid
                    }
                    var url = 'api/CloseRemaindingAmount/InactiveRemaindingAmount';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    remaindingamount_gid: remaindingamount_gid
                }

                var url = 'api/CloseRemaindingAmount/RemaindingAmountInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.inactiveremaindingamount_list = resp.data.inactiveremaindingamount_list;
                    unlockUI();
                });

            }
        }
        $scope.process = function (remaindingamount_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ProcessRemainingAmount.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    remaindingamount_gid: remaindingamount_gid
                }
                var url = 'api/CloseRemaindingAmount/GetRemainingAmount';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtremainding_amount = resp.data.remaindingamount_amount;
                });    
                $scope.open1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    $scope.opened1 = true;
                };
                $scope.open2 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    $scope.opened2 = true;
                };

                //$scope.minDate = new Date();

                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.add_remainingamount = function () {

                    var params = {
                        remaindingamount_gid: remaindingamount_gid,
                        remainding_amount: $scope.txtremainding_amount,
                        start_date: $scope.txtfrom_date,
                        end_date: $scope.txtend_date,

                    }
                    var url = 'api/CloseRemaindingAmount/RemainingAmountClosed';
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


            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsMstRepaymentTemplateController', brsMstRepaymentTemplateController);

    brsMstRepaymentTemplateController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function brsMstRepaymentTemplateController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsMstRepaymentTemplateController';
         activate();

        function activate() {


            var url = 'api/RepaymentReconcillation/GetRepaymentTemplateSummary';
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
        }
        $scope.transac_template = function () {
            $state.go('app.brsMstTemplateSummary');
        }

        $scope.repayment_template = function () {
            $state.go('app.brsMstRepaymentTemplate');
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
            $scope.importexcel = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/importexcel.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
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
                           
                        }
                       
                        else {
                            var item = {
                                name: val[0].name,
                                file: val[0]
                            };
                            var frm = new FormData();
                            frm.append('fileupload', item.file);
                            frm.append('file_name', item.name);
                            frm.append('project_flag', "Default");
                           
                            $scope.uploadfrm = frm;
                        }
                    }

                    $scope.uploadexcel = function () {


                        if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                            Notify.alert('Kindly Select the Excel file', 'warning')
                        }
                        else {

                            var url = 'api/RepaymentReconcillation/BRSRepaymentExcelSample';
                            lockUI();
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $modalInstance.close('closed');

                                if (resp.data.status == true) {

                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                 
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
                    $scope.uploadexcelcancel = function () {
                        $("#fileimport").val('');
                    };


                }


            }
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

            $scope.process = function (repayreconcildoc_gid) {

                var params={
                    repayreconcildoc_gid: repayreconcildoc_gid
                    }
             
                var url = 'api/RepaymentReconcillation/BRSLmsprocess';
                lockUI();
                SocketService.post(url, params).then(function (resp) {                  

                    if (resp.data.status == true) {

                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();

                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                       
                    }

                });
              

            }

       
    }

  

})();

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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnbankconfigurationaddController', brsTrnbankconfigurationaddController);

    brsTrnbankconfigurationaddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function brsTrnbankconfigurationaddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnbankconfigurationaddController';
        activate();


        function activate() {

             var url = 'api/MstAppCreditUnderWriting/BankNameList';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.bankdtl_list = resp.data.bankdtl_list;
                });
        }
      
        $scope.submitconfiguration = function () {
            var lsacc_no = $scope.txtaccnorow + ',' + $scope.txtaccnocol;
            var lstrn_date = $scope.txttrndaterow + ',' + $scope.txttrndatecol;
            var lsvalue_date = $scope.txtvaldaterow + ',' + $scope.txtvaldatecol;
            var lspayment_date = $scope.txtpaydaterow + ',' + $scope.txtpaydatecol;
            var lstransact_particulars = $scope.txttranscparticularrow + ',' + $scope.txttranscparticularcol;
            var lsdebit_amt = $scope.txtdebamtrow + ',' + $scope.txtdebamtcol;
            var lscredit_amt = $scope.txtcreditamtrow + ',' + $scope.txtcreditamtcol;
            var lscr_dr = $scope.txtcrdrrow + ',' + $scope.txtcrdrcol;
            var lstransact_val = $scope.txttrnvalrow + ',' + $scope.txttrnvalcol;
            var lschq_no = $scope.txtchqnorow + ',' + $scope.txtchqnocol;
            var lsbranch_name = $scope.txtbranchrow + ',' + $scope.txtbranchcol;
            var lscustref_no = $scope.txtcustrefnorow + ',' + $scope.txtcustrefnocol;
            var lsbalance_amt = $scope.txtbalancerow + ',' + $scope.txtbalancecol;
            var lscr_dr = $scope.txtcrdrrow + ',' + $scope.txtcrdrcol;
            var lstransc_id = $scope.txttrnidrow + ',' + $scope.txttrnidcol;
            var lsknockoffby_fin = $scope.txtknockofffinrow + ',' + $scope.txttknockofffincol;

            var params = {
                brsbank_gid: $scope.txtBankName.bankdtl_gid,               
                datastart_row: $scope.txtdsrrow,
                acc_no: lsacc_no,                
                trn_date: lstrn_date ,              
                value_date: lsvalue_date,               
                payment_date: lspayment_date,                
                transact_particulars: lstransact_particulars,              
                debit_amt: lsdebit_amt,                
                credit_amt: lscredit_amt,                
                cr_dr: lscr_dr,                
                transact_val:lstransact_val,               
                chq_no: lschq_no,               
                branch_name:lsbranch_name,             
                custref_no: lscustref_no,               
                balance_amt: lsbalance_amt,
                transc_id: lstransc_id,
                knockoffby_finance:lsknockoffby_fin
               
            }


            var url = "api/ConfigurationReconcillation/Addbankconfiguration"
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.brsTrnbankconfiguration');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });

        }
        $scope.back = function () {
            $state.go('app.brsTrnbankconfiguration');
        }


        $scope.editbankfield = function (bank_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbank.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    bank_gid: bank_gid
                }
                var url = 'api/BankReconcillation/EditBank';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditbank_name = resp.data.bank_name;
                    $scope.txteditacc_no = resp.data.acc_no;
                    $scope.txteditcustref_no = resp.data.custref_no;
                    $scope.txteditbranch = resp.data.branch_name;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.Updatebank = function () {

                    var url = 'api/BankReconcillation/UpdateBank';
                    var params = {
                        bank_name: $scope.txteditbank_name,
                        acc_no: $scope.txteditacc_no,
                        custref_no: $scope.txteditcustref_no,
                        branch_name: $scope.txteditbranch,
                        bank_gid: bank_gid
                    }
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

            }
        }


        $scope.deleteconfiguration = function (bank_gid) {
            var params = {
                bank_gid: bank_gid
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
                    var url = 'api/ConfigurationReconcillation/DeleteConfigurationdata';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            });
        };
    }

})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnbankconfigurationController', brsTrnbankconfigurationController);

    brsTrnbankconfigurationController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnbankconfigurationController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnbankconfigurationController';
        activate();


        function activate() {

            var url = 'api/ConfigurationReconcillation/GetConfigurationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.configuration_summary = resp.data.configuration_summary;
                unlockUI();
            });
        }
        $scope.addconfiguration = function () {
            $state.go('app.brsTrnbankconfigurationadd');

        }

        $scope.editconfiguration = function (bankconfig_gid) {
         /*   $state.go('app.brsTrnbankconfigurationedit');*/
            //$location.url('app/brsTrnbankconfigurationedit?bankconfig_gid=' + bankconfig_gid);
            $location.url("app/brsTrnbankconfigurationedit?hash=" + cmnfunctionService.encryptURL("bankconfig_gid=" + bankconfig_gid));

        }


        //$scope.popupconfiguration = function () {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/mybankContent.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.bankSubmit = function () {
        //            var params = {
        //                bank_name: $scope.txtbank_name,
        //                acc_no: $scope.txtacc_no,
        //                custref_no: $scope.txtcustref_no,
        //                branch_name: $scope.txtbranch
        //            }

        //            var url = 'api/BankReconcillation/Addbank';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                else {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }

        //                activate();

        //            });
        //            $modalInstance.close('closed');

        //        }

        //    }
        //}

        $scope.editbankfield = function (bank_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbank.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    bank_gid: bank_gid
                }
                var url = 'api/BankReconcillation/EditBank';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditbank_name = resp.data.bank_name;
                    $scope.txteditacc_no = resp.data.acc_no;
                    $scope.txteditcustref_no = resp.data.custref_no;
                    $scope.txteditbranch = resp.data.branch_name;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.Updatebank = function () {

                    var url = 'api/BankReconcillation/UpdateBank';
                    var params = {
                        bank_name: $scope.txteditbank_name,
                        acc_no: $scope.txteditacc_no,
                        custref_no: $scope.txteditcustref_no,
                        branch_name: $scope.txteditbranch,
                        bank_gid: bank_gid
                    }
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

            }
        }


        $scope.deleteconfiguration = function (bankconfig_gid) {
            var params = {
                bankconfig_gid: bankconfig_gid
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
                    var url = 'api/ConfigurationReconcillation/DeleteConfigurationdata';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            });
        };
    }

})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnbankconfigurationeditController', brsTrnbankconfigurationeditController);

    brsTrnbankconfigurationeditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnbankconfigurationeditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
       /* var bankconfig_gid = $location.search().bankconfig_gid;*/
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var bankconfig_gid = searchObject.bankconfig_gid;

        vm.title = 'brsTrnbankconfigurationeditController';
        activate();


        function activate() {

            var url = 'api/MstAppCreditUnderWriting/BankNameList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.bankdtl_list = resp.data.bankdtl_list;
            });
            var param = {
                bankconfig_gid: bankconfig_gid
            }
            var url = 'api/ConfigurationReconcillation/Editbankconfiguration';
            SocketService.getparams(url,param).then(function (resp) {
               
                $scope.txteditaccnorow = resp.data.acc_norow;
                $scope.txteditaccnocol = resp.data.acc_nocol;
                $scope.txteditdsrrow = resp.data.datastart_row;
                $scope.txtedittrndaterow = resp.data.trn_daterow;
                $scope.txtedittrndatecol = resp.data.trn_datecol;
                $scope.txteditvaldaterow = resp.data.value_daterow;
                $scope.txteditvaldatecol = resp.data.value_datecol;
                $scope.txteditpaydaterow = resp.data.payment_daterow;
                $scope.txteditpaydatecol = resp.data.payment_datecol;
                $scope.txtedittranscparticularrow = resp.data.transact_particularsrow;
                $scope.txtedittranscparticularcol = resp.data.transact_particularscol;
                $scope.txteditdebamtrow = resp.data.debit_amtrow;
                $scope.txteditdebamtcol = resp.data.debit_amtcol;
                $scope.txteditcreditamtrow = resp.data.credit_amtrow;
                $scope.txteditcreditamtcol = resp.data.credit_amtcol;
                $scope.txteditcrdrrow = resp.data.cr_drrow;
                $scope.txteditcrdrcol = resp.data.cr_drcol;
                $scope.txtedittrnvalrow = resp.data.transact_valrow;
                $scope.txtedittrnvalcol = resp.data.transact_valcol;
                $scope.txteditchqnorow = resp.data.chq_norow;
                $scope.txteditchqnocol = resp.data.chq_nocol;
                $scope.txteditbranchrow = resp.data.branch_namerow;
                $scope.txteditbranchcol = resp.data.branch_namecol;
                $scope.txteditcustrefnorow = resp.data.custref_norow;
                $scope.txteditcustrefnocol = resp.data.custref_nocol;
                $scope.txteditbalancerow = resp.data.balance_amtrow;
                $scope.txteditbalancecol = resp.data.balance_amtcol;
                $scope.txtedittrnidrow = resp.data.transc_idrow;
                $scope.txedittrnidcol = resp.data.transc_idcol;
                $scope.txtBankName = resp.data.brsbank_gid;
                $scope.txteditknockofffinrow = resp.data.knockoffby_financerow;
                $scope.txteditknockofffincol = resp.data.knockoffby_financecol;

            });
            unlockUI();


        }

        $scope.updateconfiguration = function () {
            var lsbankdtl_name = '';
            var lsacc_no = $scope.txteditaccnorow + ',' + $scope.txteditaccnocol;
            var lstrn_date = $scope.txtedittrndaterow + ',' + $scope.txtedittrndatecol;
            var lsvalue_date = $scope.txteditvaldaterow + ',' + $scope.txteditvaldatecol;
            var lspayment_date = $scope.txteditpaydaterow + ',' + $scope.txteditpaydatecol;
            var lstransact_particulars = $scope.txtedittranscparticularrow + ',' + $scope.txtedittranscparticularcol;
            var lsdebit_amt = $scope.txteditdebamtrow + ',' + $scope.txteditdebamtcol;
            var lscredit_amt = $scope.txteditcreditamtrow + ',' + $scope.txteditcreditamtcol;
            var lscr_dr = $scope.txteditcrdrrow + ',' + $scope.txteditcrdrcol;
            var lstransact_val = $scope.txtedittrnvalrow + ',' + $scope.txtedittrnvalcol;
            var lschq_no = $scope.txteditchqnorow + ',' + $scope.txteditchqnocol;
            var lsbranch_name = $scope.txteditbranchrow + ',' + $scope.txteditbranchcol;
            var lscustref_no = $scope.txteditcustrefnorow + ',' + $scope.txteditcustrefnocol;
            var lsbalance_amt = $scope.txteditbalancerow + ',' + $scope.txteditbalancecol;
            var lscr_dr = $scope.txteditcrdrrow + ',' + $scope.txteditcrdrcol;
            var lstransc_id = $scope.txtedittrnidrow + ',' + $scope.txedittrnidcol;
            var lsknockofffin = $scope.txteditknockofffinrow + ',' + $scope.txteditknockofffincol;
            lsbankdtl_name = $('#bankdetailname :selected').text();
            lockUI();
            var url = 'api/ConfigurationReconcillation/Updatebankconfiguration';

            var params = {
                bankconfig_gid: bankconfig_gid,
                brsbank_gid: $scope.txtBankName,
                bank_name: lsbankdtl_name,
                datastart_row: $scope.txteditdsrrow,
                acc_no: lsacc_no,
                trn_date: lstrn_date,
                value_date: lsvalue_date,
                payment_date: lspayment_date,
                transact_particulars: lstransact_particulars,
                debit_amt: lsdebit_amt,
                credit_amt: lscredit_amt,
                cr_dr: lscr_dr,
                transact_val: lstransact_val,
                chq_no: lschq_no,
                branch_name: lsbranch_name,
                custref_no: lscustref_no,
                balance_amt: lsbalance_amt,
                transc_id: lstransc_id,
                knockoffby_finance: lsknockofffin

            }           
           
            SocketService.post(url,params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    
                    $state.go('app.brsTrnbankconfiguration');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });

        }
        $scope.back = function () {
            $state.go('app.brsTrnbankconfiguration');
        }


        $scope.editbankfield = function (bank_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbank.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    bank_gid: bank_gid
                }
                var url = 'api/BankReconcillation/EditBank';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditbank_name = resp.data.bank_name;
                    $scope.txteditacc_no = resp.data.acc_no;
                    $scope.txteditcustref_no = resp.data.custref_no;
                    $scope.txteditbranch = resp.data.branch_name;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.Updatebank = function () {

                    var url = 'api/BankReconcillation/UpdateBank';
                    var params = {
                        bank_name: $scope.txteditbank_name,
                        acc_no: $scope.txteditacc_no,
                        custref_no: $scope.txteditcustref_no,
                        branch_name: $scope.txteditbranch,
                        bank_gid: bank_gid
                    }
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

            }
        }


        $scope.deletebank = function (bank_gid) {
            var params = {
                bank_gid: bank_gid
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
                    var url = 'api/BankReconcillation/DeleteBank';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            });
        };
    }

})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnBankMatchedImportController', brsTrnBankMatchedImportController);

    brsTrnBankMatchedImportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnBankMatchedImportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnBankMatchedImportController';
        // console.log('test');
        activate();

        function activate() {
           
            var url = 'api/KotakReconcillation/GetBankTransactionMatchedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {

                    $scope.BankTransactionMatchedSummary = JSON.parse(resp.data.JSONdata);
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
            $scope.totalcount = parseInt($scope.bankmatched_count);
            if ($scope.totalcount < $scope.limitoffsetinc) {
                $scope.increment_flag = 'Y';
            }

            var params = {
                limitoffset_from: $scope.limitoffsetinc
            }
            var url = 'api/KotakReconcillation/GetBankTransactionMatchedSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                /*   if (resp.data.repayment_list != null) {*/
                if (resp.data.status == true) {
                    $scope.BankTransactionMatchedSummary = JSON.parse(resp.data.JSONdata);
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
            var url = 'api/KotakReconcillation/GetBankTransactionMatchedSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                /*   if (resp.data.repayment_list != null) {*/
                if (resp.data.status == true) {
                    $scope.BankTransactionMatchedSummary = JSON.parse(resp.data.JSONdata);
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
                        frm.append('brsbank_gid', lsbank_gid);
                        frm.append('bank_name', lsbank_name);
                        frm.append('project_flag', "RSK");
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnCreditClosedController', brsTrnCreditClosedController);

        brsTrnCreditClosedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnCreditClosedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnCreditClosedController';
       // console.log('test');
        activate();

        function activate() {
          
            var url = 'api/RepaymentReconcillation/GetCreditclosedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.CreditClosed_list = resp.data.CreditClosed_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                unlockUI();
            });
            
            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                $scope.creditmatch_count = resp.data.creditmatch_count;
                $scope.partialcredit_count = resp.data.partialcredit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.creditclose_count = resp.data.creditclose_count;
                unlockUI();
            });

            
        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }

        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');
        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnCreditPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnCreditUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnCreditUnMatchedAssigned');
        }
        $scope.close = function () {
            $state.go('app.brsTrnCreditClosed');
        }
        
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnCreditMatchedController', brsTrnCreditMatchedController);

    brsTrnCreditMatchedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnCreditMatchedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnCreditMatchedController';
       // console.log('test');
        activate();

        function activate() {
          
            var url = 'api/RepaymentReconcillation/GetCreditMatchedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditmatched_list = resp.data.creditmatched_list;
                unlockUI();

            });
            
            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                unlockUI();
            });

            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                $scope.creditmatch_count = resp.data.creditmatch_count;
                $scope.partialcredit_count = resp.data.partialcredit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.creditclose_count = resp.data.creditclose_count;
                unlockUI();
            });

            
        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }

        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');
        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnCreditPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnCreditUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnCreditUnMatchedAssigned');
        }
        $scope.close = function () {
            $state.go('app.brsTrnCreditClosed');
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/BrsTrnUnreconBankMatchedView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnCreditPartialMatchedController', brsTrnCreditPartialMatchedController);

        brsTrnCreditPartialMatchedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnCreditPartialMatchedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnCreditPartialMatchedController';
       // console.log('test');
        activate();

        function activate() {
          
            var url = 'api/RepaymentReconcillation/GetCreditPartialMatchedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditpartialmatched_list = resp.data.creditpartialmatched_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                unlockUI();
            });

            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                $scope.creditmatch_count = resp.data.creditmatch_count;
                $scope.partialcredit_count = resp.data.partialcredit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.creditclose_count = resp.data.creditclose_count;
                unlockUI();
            });


            
        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }

        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');
        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnCreditPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnCreditUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnCreditUnMatchedAssigned');
        }
         $scope.close = function () {
            $state.go('app.brsTrnCreditClosed');
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnPartialMatchedView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=CreditPartial'));
        }
        
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnCreditUnMatchedAssignedController', brsTrnCreditUnMatchedAssignedController);

        brsTrnCreditUnMatchedAssignedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnCreditUnMatchedAssignedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnCreditUnMatchedAssignedController';
       // console.log('test');
        activate();

        function activate() {
          
            var url = 'api/RepaymentReconcillation/GetCreditUnmatchedassignedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditunmatchedassigned_list = resp.data.creditunmatchedassigned_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                unlockUI();
            });

            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                $scope.creditmatch_count = resp.data.creditmatch_count;
                $scope.partialcredit_count = resp.data.partialcredit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.creditclose_count = resp.data.creditclose_count;
                unlockUI();
            });


            
        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }

        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');
        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnCreditPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnCreditUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnCreditUnMatchedAssigned');
        }

        $scope.close = function () {
            $state.go('app.brsTrnCreditClosed');
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnCreditUnMatchedUnAssignedController', brsTrnCreditUnMatchedUnAssignedController);

        brsTrnCreditUnMatchedUnAssignedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnCreditUnMatchedUnAssignedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnCreditUnMatchedUnAssignedController';
       // console.log('test');
        activate();

        function activate() {
          
            var url = 'api/RepaymentReconcillation/GetCreditUnmatchedUnassignedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditUnmatchedUnassigned_list = resp.data.creditUnmatchedUnassigned_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                unlockUI();
            });

            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                $scope.creditmatch_count = resp.data.creditmatch_count;
                $scope.partialcredit_count = resp.data.partialcredit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.creditclose_count = resp.data.creditclose_count;
                unlockUI();
            });


            
        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }

        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');
        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnCreditPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnCreditUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnCreditUnMatchedAssigned');
        }
        $scope.close = function () {
            $state.go('app.brsTrnCreditClosed');
        }
        
    }
})();

// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnDebitClosedController', brsTrnDebitClosedController);

        brsTrnDebitClosedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnDebitClosedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnDebitClosedController';
        // console.log('test');
        activate();

        function activate() {
            var url = 'api/RepaymentReconcillation/GetDebitClosedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.debitClosed_list = resp.data.debitClosed_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                unlockUI();
            });
          
            var url = "api/RepaymentReconcillation/GetDebitCount";
             SocketService.get(url).then(function (resp) {
                 $scope.debit_count = resp.data.debit_count;
                 $scope.debitmatch_count = resp.data.debitmatch_count;
                 $scope.partialdebit_count = resp.data.partialdebit_count;
                 $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                 $scope.unmatchassign_count = resp.data.unmatchassign_count;
                 $scope.debitclose_count = resp.data.debitclose_count;
                 unlockUI();
             });


        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }
        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');

        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnDebitPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnDebitUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnDebitUnMatchedAssigned');
        }
        $scope.close = function () {
            $state.go('app.brsTrnDebitClosed');
        }
    }
})();

// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnDebitMatchedController', brsTrnDebitMatchedController);

    brsTrnDebitMatchedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnDebitMatchedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnDebitMatchedController';
        // console.log('test');
        activate();

        function activate() {
            var url = 'api/RepaymentReconcillation/GetDebitMatchedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.Debitmatched_list = resp.data.Debitmatched_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                unlockUI();
            });

            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                $scope.debitmatch_count = resp.data.debitmatch_count;
                $scope.partialdebit_count = resp.data.partialdebit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.debitclose_count = resp.data.debitclose_count;
                unlockUI();
            });


        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }
        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');

        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnDebitPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnDebitUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnDebitUnMatchedAssigned');
        }
        $scope.close = function () {
            $state.go('app.brsTrnDebitClosed');
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/BrsTrnUnreconDebitBankMatchedView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();

// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnDebitPartialMatchedController', brsTrnDebitPartialMatchedController);

    brsTrnDebitPartialMatchedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnDebitPartialMatchedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnDebitPartialMatchedController';
        // console.log('test');
        activate();

        function activate() {
            var url = 'api/RepaymentReconcillation/GetDebitPartialMatchedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.debitpartialmatched_list = resp.data.debitpartialmatched_list;
                unlockUI();


           });
            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                unlockUI();
            });

            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                $scope.debitmatch_count = resp.data.debitmatch_count;
                $scope.partialdebit_count = resp.data.partialdebit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.debitclose_count = resp.data.debitclose_count;
                unlockUI();
            });

         }
         $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }
        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');

        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnDebitPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnDebitUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnDebitUnMatchedAssigned');
        }
        $scope.close = function () {
            $state.go('app.brsTrnDebitClosed');
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnPartialMatchedView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=DebitPartial'));
        }

    }
})();

// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnDebitUnMatchedAssignedController', brsTrnDebitUnMatchedAssignedController);

        brsTrnDebitUnMatchedAssignedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnDebitUnMatchedAssignedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnDebitUnMatchedAssignedController';
        // console.log('test');
        activate();

        function activate() {
            var url = 'api/RepaymentReconcillation/GetDebitUnmatchedassignedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.debitmatchedassigned_list = resp.data.debitmatchedassigned_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                unlockUI();
            });

            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                $scope.debitmatch_count = resp.data.debitmatch_count;
                $scope.partialdebit_count = resp.data.partialdebit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.debitclose_count = resp.data.debitclose_count;
                unlockUI();
            });

        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }
        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');

        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnDebitPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnDebitUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnDebitUnMatchedAssigned');
        }
        $scope.close = function () {
            $state.go('app.brsTrnDebitClosed');
        }
    }
})();

// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnDebitUnMatchedUnAssignedController', brsTrnDebitUnMatchedUnAssignedController);

        brsTrnDebitUnMatchedUnAssignedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnDebitUnMatchedUnAssignedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnDebitUnMatchedUnAssignedController';
        // console.log('test');
        activate();

        function activate() {
            var url = 'api/RepaymentReconcillation/GetDebitUnmatchedUnassignedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.debitUnmatchedUnassigned_list = resp.data.debitUnmatchedUnassigned_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                unlockUI();
            });

            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                $scope.debitmatch_count = resp.data.debitmatch_count;
                $scope.partialdebit_count = resp.data.partialdebit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.debitclose_count = resp.data.debitclose_count;
                unlockUI();
            });
        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }
        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');

        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnDebitPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnDebitUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnDebitUnMatchedAssigned');
        }
        $scope.close = function () {
            $state.go('app.brsTrnDebitClosed');
        }

    }
})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnMyUnReconAlloactedPendingSummaryController', brsTrnMyUnReconAlloactedPendingSummaryController);

    brsTrnMyUnReconAlloactedPendingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function brsTrnMyUnReconAlloactedPendingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnMyUnReconAlloactedPendingSummaryController';
        activate();
        function activate() {
            var url = 'api/MyUnreconciliationManagement/GetAllocatedPendingReportSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unreconallocaterpt_list = resp.data.unreconallocaterpt_list;
                unlockUI();
            });
            var url = "api/MyUnreconciliationManagement/GetMyunreConciliationSummaryCount";
            lockUI();
            SocketService.get(url).then(function (resp) {

                $scope.pendingrpt_count = resp.data.pendingrpt_count;
                $scope.closedrpt_count = resp.data.closedrpt_count;
                $scope.allocatependingrpt_count = resp.data.allocatependingrpt_count;

                unlockUI();
            });

        }
        $scope.pending_rpt = function () {
            $state.go('app.brsTrnMyUnReconciliationSummary');
        }

        $scope.closed_rpt = function () {
            $state.go('app.brsTrnMyUnReconClosedSummary');
        }
        $scope.alloacted_rpt = function () {
            $state.go('app.brsTrnMyUnReconAlloactedPendingSummary');
        }

        $scope.brsallocate_rpt = function () {
            lockUI();
            var url = 'api/MyUnreconciliationManagement/AllocatedPendingExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }
    }
})();
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnMyUnReconciliationClosedViewController', brsTrnMyUnReconciliationClosedViewController);

    brsTrnMyUnReconciliationClosedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function brsTrnMyUnReconciliationClosedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        vm.title = 'brsTrnMyUnReconciliationClosedViewController';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;

        activate();
        function activate() {


            var url = 'api/MyUnreconciliationManagement/GetMyUnreconReportView';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.banktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;

                $scope.knockoff_status = resp.data.knockoff_status;

                $scope.value_date = resp.data.value_date;
                $scope.payment_date = resp.data.payment_date;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.debit_amt = resp.data.debit_amt;
                $scope.credit_amt = resp.data.credit_amt;
                $scope.created_by = resp.data.created_by;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.bankrepaytransc_gid = resp.data.bankrepaytransc_gid;
                $scope.repay_transaction_date = resp.data.repay_transaction_date;
                $scope.principal = resp.data.principal;
                $scope.lblremainingamount = resp.data.remaining_amount;

                $scope.normal_interest = resp.data.normal_interest;
                $scope.forfeiture_waiver = resp.data.forfeiture_waiver;
                $scope.repay_remarks = resp.data.repay_remarks;
                $scope.repayment_type = resp.data.repayment_type;
                $scope.penal_interest = resp.data.penal_interest;
                $scope.instrument = resp.data.instrument;
                $scope.old_dpd = resp.data.old_dpd;
                $scope.dpd = resp.data.dpd;
                $scope.account_code = resp.data.account_code;
                $scope.interest_tds = resp.data.interest_tds;
                $scope.penal_interest_tds = resp.data.penal_interest_tds;
                $scope.repay_knockoff_status = resp.data.repay_knockoff_status;
                $scope.manualknockoff_remarks = resp.data.manualknockoff_remarks;
            });

            var url = 'api/MyUnreconciliationManagement/GetAssignedHistoryView';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedview_list = resp.data.assignedview_list;
            });

            var url = 'api/MyUnreconciliationManagement/GetTransferredHistoryView';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferview_list = resp.data.transferview_list;
            });
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
            
        }

        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Back = function () {
            $state.go('app.brsTrnMyUnReconClosedSummary');
        }
    }
})();
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnMyUnReconciliationPendingViewController', brsTrnMyUnReconciliationPendingViewController);

    brsTrnMyUnReconciliationPendingViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function brsTrnMyUnReconciliationPendingViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        vm.title = 'brsTrnMyUnReconciliationPendingViewController';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;

        activate();
        function activate() {


            var url = 'api/MyUnreconciliationManagement/GetMyUnreconReportView';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.banktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.lblremainingamount = resp.data.remaining_amount;

                $scope.knockoff_status = resp.data.knockoff_status;
               
                $scope.value_date = resp.data.value_date;
                $scope.payment_date = resp.data.payment_date;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.debit_amt = resp.data.debit_amt;
                $scope.credit_amt = resp.data.credit_amt;
                $scope.created_by = resp.data.created_by;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.bankrepaytransc_gid = resp.data.bankrepaytransc_gid;
                $scope.repay_transaction_date = resp.data.repay_transaction_date;
                $scope.principal = resp.data.principal;

                $scope.normal_interest = normal_interest;
                $scope.forfeiture_waiver = resp.data.forfeiture_waiver;
                $scope.repay_remarks = resp.data.repay_remarks;
                $scope.repayment_type = resp.data.repayment_type;
                $scope.penal_interest = resp.data.penal_interest;
                $scope.instrument = resp.data.instrument;
                $scope.old_dpd = resp.data.old_dpd;
                $scope.dpd = resp.data.dpd;
                $scope.account_code = resp.data.account_code;
                $scope.interest_tds = resp.data.interest_tds;
                $scope.penal_interest_tds = resp.data.penal_interest_tds;
                $scope.repay_knockoff_status = resp.data.repay_knockoff_status;
            });

            var url = 'api/MyUnreconciliationManagement/GetAssignedHistoryView';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedview_list = resp.data.assignedview_list;
            });

            var url = 'api/MyUnreconciliationManagement/GetTransferredHistoryView';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferview_list = resp.data.transferview_list;
            });

            var url = 'api/UnreconciliationManagement/GetSendBackHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.sendbackemployee_list = resp.data.sendbacklist;
            });

            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }

        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Back = function () {
            $state.go('app.brsTrnMyUnReconciliationSummary');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnMyUnReconciliationSummaryController', brsTrnMyUnReconciliationSummaryController);

    brsTrnMyUnReconciliationSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnMyUnReconciliationSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {

        var vm = this;
        vm.title = 'brsTrnMyUnReconciliationSummaryController';

        activate();

        function activate() {
            $scope.limit = 6000;
            $scope.totalDisplayed = 100;

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.MyUnreconciliation_list = resp.data.MyUnreconciliation_list;
              
                unlockUI();
            });

            var url = "api/MyUnreconciliationManagement/GetMyunreConciliationSummaryCount";
            lockUI();
            SocketService.get(url).then(function (resp) {
          
                $scope.pendingrpt_count = resp.data.pendingrpt_count;
                $scope.closedrpt_count = resp.data.closedrpt_count;
                $scope.allocatependingrpt_count = resp.data.allocatependingrpt_count;
             
                unlockUI();
            });
            var url = 'api/MyUnreconciliationManagement/GetBank';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.bankdtllist = resp.data.bankdtllist;

            });

        }
       
        $scope.pending_rpt = function () {
            $state.go('app.brsTrnMyUnReconciliationSummary');
        }

        $scope.closed_rpt = function () {
            $state.go('app.brsTrnMyUnReconClosedSummary');
        }
        $scope.alloacted_rpt = function () {
            $state.go('app.brsTrnMyUnReconAlloactedPendingSummary');
        }

        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnMyUnReconciliationPendingView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }

        $scope.onselectbank = function (bankname_gid) {
            lockUI();
            var params = {
                bankname_gid: $scope.cboBankName.bankname_gid
            }
            var url = 'api/MyUnreconciliationManagement/BankNameList';
            SocketService.getparams(url,params).then(function (resp) {               
                $scope.bankdtllist = resp.data.bankdtllist;
                var lsbank_gid = '';
                var lsbank_name = '';
                if ($scope.cboBankName != undefined || $scope.cboBankName != null) {
                    lsbank_gid = $scope.cboBankName.bankname_gid;
                    lsbank_name = $scope.cboBankName.bankname_name;
                }
            });
          
            unlockUI();
        }

        $scope.all = function () {
            $scope.bank_gid = "";
            $scope.cr_dr = "";
            $scope.amount_greater = "";
            $scope.amount_lesser = "";
            $scope.knockoff_status = "";
            $scope.trns_date = "";
           

            var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.MyUnreconciliation_list = resp.data.MyUnreconciliation_list;

                unlockUI();
            });
        }
        $scope.search = function () {
            lockUI();
            var lsbank_gid = '';
            var lsbank_name = '';
            if ($scope.cboBankName != undefined || $scope.cboBankName != null) {
                lsbank_gid = $scope.cboBankName.bankname_gid;
                lsbank_name = $scope.cboBankName.bankname_name;
            }
            if (($scope.single == false) && ($scope.cboBankName == "" || $scope.cboBankName == undefined)) {
                Notify.alert("Kindly Select Bank Name", "warning");
                unlockUI();
            }
            else {
                if ($scope.trn_date == undefined || $scope.trn_date == "") {
                    var trn_date = 'null';
                }
                else {
                    var trn_date1 = $scope.trn_date;

                    var trn_date = new Date(trn_date1.getTime() - (trn_date1.getTimezoneOffset() * 60000))
                        .toISOString()
                        .split("T")[0];
                }

                var params = {
                    bank_gid: lsbank_gid,
                    knockoff_status: $scope.knockoff_status,
                    amount_greater: $scope.txtgrt_amount,
                    amount_lesser: $scope.txtless_amount,
                    cr_dr: $scope.cr_dr,
                    trns_date: trn_date

                }

                var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationSummarySearch';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.MyUnreconciliation_list = resp.data.MyUnreconciliation_list;

                });
               
            }
        }


        $scope.viewPDF = function (val) {
            lockUI();
            var params = {
                servicerequest_gid: val
            }

            var url = 'api/OsdTrnTicketManagement/txtfile';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    unlockUI();
                }
                else {
                    //$modalInstance.close('closed');
                    alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    unlockUI();
                }
            });
        }
        $scope.export = function (val1, val2) {
           
            lockUI();
            var lsbank_gid = '';
            var lsbank_name = '';
            if ($scope.cboBankName != undefined || $scope.cboBankName != null) {
                lsbank_gid = $scope.cboBankName.bankname_gid;
                lsbank_name = $scope.cboBankName.bankname_name;
            }
            if (($scope.single == false) && ($scope.cboBankName == "" || $scope.cboBankName == undefined)) {
                Notify.alert("Kindly Select Bank Name", "warning");
                unlockUI();
            }
            else {
                if ($scope.trn_date == undefined || $scope.trn_date == "") {
                    var trn_date = 'null';
                }
                else {
                    var trn_date1 = $scope.trn_date;

                    var trn_date = new Date(trn_date1.getTime() - (trn_date1.getTimezoneOffset() * 60000))
                        .toISOString()
                        .split("T")[0];
                }
                
                var params = {
                    bank_gid: lsbank_gid,
                    knockoff_status: $scope.knockoff_status,
                    amount_greater: $scope.txtgrt_amount,
                    amount_lesser: $scope.txtless_amount,
                    cr_dr: $scope.cr_dr,
                    trn_date: trn_date
                    
                }
                var url = 'api/MyUnreconciliationManagement/UnreconPendingExport';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                        
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export !', 'success')
                        activate();

                    }

                }
                );
            }
        }

       
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnMyUnReconClosedSummaryController', brsTrnMyUnReconClosedSummaryController);

    brsTrnMyUnReconClosedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnMyUnReconClosedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {

        var vm = this;
        vm.title = 'brsTrnMyUnReconClosedSummaryController';

        activate();

        function activate() {
            $scope.limit = 6000;
            $scope.totalDisplayed = 100;

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationClosedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.MyUnreconciliationClose_list = resp.data.MyUnreconciliationClose_list;

                unlockUI();
            });

            var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationSummaryCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
               
                $scope.pendingrpt_count = resp.data.pendingrpt_count;
                $scope.closedrpt_count = resp.data.closedrpt_count;
                $scope.allocatependingrpt_count = resp.data.allocatependingrpt_count;
                unlockUI();
            });
            var url = 'api/MyUnreconciliationManagement/GetBank';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.bankdtllist = resp.data.bankdtllist;

            });
        }
        $scope.pending_rpt = function () {
            $state.go('app.brsTrnMyUnReconciliationSummary');
        }

        $scope.closed_rpt = function () {
            $state.go('app.brsTrnMyUnReconClosedSummary');
        }
        $scope.alloacted_rpt = function () {
            $state.go('app.brsTrnMyUnReconAlloactedPendingSummary');
        }


        $scope.onselectbank = function (bankname_gid) {
            lockUI();
            var params = {
                bankname_gid: $scope.cboBankName.bankname_gid
            }
            var url = 'api/MyUnreconciliationManagement/BankNameList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.bankdtllist = resp.data.bankdtllist;
                var lsbank_gid = '';
                var lsbank_name = '';
                if ($scope.cboBankName != undefined || $scope.cboBankName != null) {
                    lsbank_gid = $scope.cboBankName.bankname_gid;
                    lsbank_name = $scope.cboBankName.bankname_name;
                }
            });

            unlockUI();
        }

        $scope.all = function () {
            $scope.bank_gid = "";
            $scope.cr_dr = "";
            $scope.amount_greater = "";
            $scope.amount_lesser = "";
            $scope.knockoff_status = "";
            $scope.trns_date = "";


            var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationClosedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.MyUnreconciliationClose_list = resp.data.MyUnreconciliationClose_list;

                unlockUI();
            });
        }
        $scope.search = function () {
            lockUI();
            var lsbank_gid = '';
            var lsbank_name = '';
            if ($scope.cboBankName != undefined || $scope.cboBankName != null) {
                lsbank_gid = $scope.cboBankName.bankname_gid;
                lsbank_name = $scope.cboBankName.bankname_name;
            }
            if (($scope.single == false) && ($scope.cboBankName == "" || $scope.cboBankName == undefined)) {
                Notify.alert("Kindly Select Bank Name", "warning");
                unlockUI();
            }
            else {
                if ($scope.trn_date == undefined || $scope.trn_date == "") {
                    var trn_date = 'null';
                }
                else {
                    var trn_date1 = $scope.trn_date;

                    var trn_date = new Date(trn_date1.getTime() - (trn_date1.getTimezoneOffset() * 60000))
                        .toISOString()
                        .split("T")[0];
                }

                var params = {
                    bank_gid: lsbank_gid,
                    knockoff_status: $scope.knockoff_status,
                    amount_greater: $scope.txtgrt_amount,
                    amount_lesser: $scope.txtless_amount,
                    cr_dr: $scope.cr_dr,
                    trns_date: trn_date

                }

                var url = 'api/MyUnreconciliationManagement/GetMyunreConciliationClosedSummarySearch';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    $scope.MyUnreconciliationClose_list = resp.data.MyUnreconciliationClose_list;

                });

            }
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnMyUnReconciliationClosedView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }

        $scope.viewPDF = function (val) {
            lockUI();
            var params = {
                servicerequest_gid: val
            }

            var url = 'api/OsdTrnTicketManagement/txtfile';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    unlockUI();
                }
                else {
                    //$modalInstance.close('closed');
                    alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    unlockUI();
                }
            });
        }
        $scope.export = function (val1, val2) {

            lockUI();
            var lsbank_gid = '';
            var lsbank_name = '';
            if ($scope.cboBankName != undefined || $scope.cboBankName != null) {
                lsbank_gid = $scope.cboBankName.bankname_gid;
                lsbank_name = $scope.cboBankName.bankname_name;
            }
            if (($scope.single == false) && ($scope.cboBankName == "" || $scope.cboBankName == undefined)) {
                Notify.alert("Kindly Select Bank Name", "warning");
                unlockUI();
            }
            else {
                if ($scope.trn_date == undefined || $scope.trn_date == "") {
                    var trn_date = 'null';
                }
                else {
                    var trn_date1 = $scope.trn_date;

                    var trn_date = new Date(trn_date1.getTime() - (trn_date1.getTimezoneOffset() * 60000))
                        .toISOString()
                        .split("T")[0];
                }

                var params = {
                    bank_gid: lsbank_gid,
                    knockoff_status: $scope.knockoff_status,
                    amount_greater: $scope.txtgrt_amount,
                    amount_lesser: $scope.txtless_amount,
                    cr_dr: $scope.cr_dr,
                    trn_date: trn_date

                }
                var url = 'api/MyUnreconciliationManagement/UnreconClosedExport';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Export !', 'success')
                        activate();

                    }

                }
                );
            }
        }


    }
})();
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnPartialMatchedViewController', brsTrnPartialMatchedViewController);

    brsTrnPartialMatchedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnPartialMatchedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'brsTrnPartialMatchedViewController';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;

        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.banktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.lblchq_no = resp.data.chq_no;

            });
          
            var url = 'api/RepaymentReconcillation/GetLMSPartialhistory';
            var param = {
                banktransc_gid: banktransc_gid

            }
            lockUI();
            SocketService.getparams(url,param).then(function (resp) {
                $scope.repayment_lmshistory = resp.data.repayment_lmshistory;
                unlockUI();

            });
        }

       
        $scope.Back = function () {
            if (lspage == "CreditPartial") {
                $state.go('app.brsTrnCreditPartialMatched');
            }
            else if (lspage == "DebitPartial") {
                $state.go('app.brsTrnDebitPartialMatched');
            }
                     
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnReconcillationController', brsTrnReconcillationController);

        brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnReconcillationController';
       // console.log('test');
        activate();

        function activate() {
          
            var url = 'api/RepaymentReconcillation/GetreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cocillationcredit_list = resp.data.cocillationcredit_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.reconc_count = resp.data.reconc_count;
                $scope.unreconc_count = resp.data.unreconc_count;
                $scope.reconcredit_count = resp.data.reconcredit_count;
                $scope.recondebit_count = resp.data.recondebit_count;

                unlockUI();
            });

            
        }
        $scope.recon = function () {
            $state.go('app.brsTrnReconcillation');
        }

        $scope.unrecon = function () {
            $state.go('app.brsTrnUnReconcillation');
        }
        $scope.recondebit = function () {
            $state.go('app.brsTrnReconcillationdebit');
        }
        
    }
})();

// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnReconcillationdebitController', brsTrnReconcillationdebitController);

    brsTrnReconcillationdebitController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnReconcillationdebitController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnReconcillationdebitController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/RepaymentReconcillation/GetreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cocillationdebit_list = resp.data.cocillationdebit_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.reconc_count = resp.data.reconc_count;
                $scope.unreconc_count = resp.data.unreconc_count;

                unlockUI();
            });
            var url = "api/RepaymentReconcillation/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.reconc_count = resp.data.reconc_count;
                $scope.unreconc_count = resp.data.unreconc_count;
                $scope.reconcredit_count = resp.data.reconcredit_count;
                $scope.recondebit_count = resp.data.recondebit_count;

                unlockUI();
            });



        }
        $scope.recon = function () {
            $state.go('app.brsTrnReconcillation');
        }

        $scope.unrecon = function () {
            $state.go('app.brsTrnUnReconcillation');
        }
        $scope.recondebit = function () {
            $state.go('app.brsTrnReconcillationdebit');
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnRepaymentImportController', brsTrnRepaymentImportController);

    brsTrnRepaymentImportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnRepaymentImportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnRepaymentImportController';
        // console.log('test');
        activate();

        function activate() {
           

            var url = 'api/RepaymentReconcillation/GetRepaymentPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                /*   if (resp.data.repayment_list != null) {*/
                if (resp.data.status == true) {
                    $scope.RepaymentPending = JSON.parse(resp.data.JSONdata);
                    //$scope.limitoffset = parseInt(resp.data.offsetlimit);
                    //$scope.repayment_list = resp.data.repayment_list;
                    unlockUI();
                }
                else {
                    unlockUI();
                }

            });     

            var url = 'api/RepaymentReconcillation/GetRepaymentSummaryCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.repaymentpending_count = resp.data.repaymentpending_count;
                $scope.repaymentmatched_count = resp.data.repaymentmatched_count;


            });
        }
        $scope.repayment_pending = function () {
            $state.go('app.brsTrnRepaymentImport');
        }
        $scope.repayment_completed = function () {
            $state.go('app.brsTrnRepaymentMatchedImport');
        }

        $scope.incrementLimit = function () {

            $scope.limitoffset += 6000;

            $scope.limitoffsetinc = $scope.limitoffset;
            $scope.totalcount = parseInt($scope.repaymentpending_count);
            if ($scope.totalcount < $scope.limitoffsetinc) {
                $scope.increment_flag = 'Y';
            }

            var params = {
                limitoffset_from: $scope.limitoffsetinc
            }
            var url = 'api/RepaymentReconcillation/GetRepaymentPendingSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                /*   if (resp.data.repayment_list != null) {*/
                if (resp.data.status == true) {
                    $scope.RepaymentPending = JSON.parse(resp.data.JSONdata);
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
            if ($scope.limitoffsetdec =='0') {
                $scope.decrement_flag = 'Y';
            }
            var params = {
                limitoffset_from: $scope.limitoffsetdec
            }
            var url = 'api/RepaymentReconcillation/GetRepaymentPendingSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                /*   if (resp.data.repayment_list != null) {*/
                if (resp.data.status == true) {
                    $scope.RepaymentPending = JSON.parse(resp.data.JSONdata);
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
        $scope.deletetemplate = function (repayreconcildoc_gid) {
            var params = {
                repayreconcildoc_gid: repayreconcildoc_gid
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

                    var url = 'api/RepaymentReconcillation/DeleteRepaymentTemplatedata';
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
        $scope.deleterepaymentdata = function (bankrepaytransc_gid) {
            var params = {
                bankrepaytransc_gid: bankrepaytransc_gid
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

                    var url = 'api/RepaymentReconcillation/DeleteRepaymentdata';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting  Repayment Transaction !!!', {
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

        var limitStep = 6000;
        //$scope.incrementLimit = function () {
        //    $scope.limit += limitStep;
        //};
        //$scope.decrementLimit = function () {
        //    $scope.limit -= limitStep;
        //};

        $scope.importexcel = function () {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
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
                        frm.append('project_flag', "Default");
                        //frm.append('brsbank_gid', lsbank_gid);
                        //frm.append('bank_name', lsbank_name);
                        // frm.append('questionnarieanswer_gid', questionnarieanswer_gid);
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {


                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {

                        var url = 'api/RepaymentReconcillation/BRSRepaymentExcelSample';
                            lockUI();
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $modalInstance.close('closed');

                                if (resp.data.status == true) {
                                   
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    // var url = 'api/RepaymentReconcillation/GetRepaymentSummary';
                                    // lockUI();
                                    // SocketService.get(url).then(function (resp) {
                                    //     $scope.repayment_list = resp.data.repayment_list;
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
                                
                                // var url = 'api/RepaymentReconcillation/GetRepaymentSummary';
                                // lockUI();
                                // SocketService.get(url).then(function (resp) {
                                    //     $scope.repayment_list = resp.data.repayment_list;
                                    //     unlockUI();
                                    
                                    // });  
                                $("#fileimport").val('');
                                activate();

                            });                     
                      
                    }

                }
                $scope.uploadexcelcancel = function () {
                    $("#fileimport").val('');
                };


            }

          
        }
    }

    //Divya


    // $scope.Status_update = function (Questionnarie_gid) {
    //     var modalInstance = $modal.open({
    //         templateUrl: '/statusQuestionnarie.html',
    //         controller: ModalInstanceCtrl,
    //         backdrop: 'static',
    //         keyboard: false,
    //         size: 'md'
    //     });
    //     ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
    //     function ModalInstanceCtrl($scope, $modalInstance) {
    //          var params = {
    //             Questionnarie_gid: Questionnarie_gid
    //         }
    //         var url = 'api/FndQuestionnarieMaster/QuestionnarieEdit';
    //         SocketService.getparams(url, params).then(function (resp) {
    //             $scope.txteditQuestionnarie_code = resp.data.Questionnarie_code;
    //             $scope.txteditlms_code = resp.data.lms_code;
    //             $scope.txteditbureau_code = resp.data.bureau_code;
    //             $scope.txteditQuestionnarie = resp.data.Questionnarie_name;
    //             $scope.txteditremarks = resp.data.remarks;
    //             $scope.Questionnarie_gid = resp.data.Questionnarie_gid;
    //             $scope.rbo_status = resp.data.Status;
    //         });
    //         $scope.ok = function () {
    //             $modalInstance.close('closed');
    //         };
    //         $scope.update_status = function () {

    //             var params = {
    //                 Questionnarie_name: $scope.Questionnarie_name,
    //                 Questionnarie_gid: Questionnarie_gid,
    //                 remarks: $scope.txtremarks,
    //                 rbo_status: $scope.rbo_status

    //             }
    //             var url = 'api/FndQuestionnarieMaster/InactiveQuestionnarie';
    //             lockUI();
    //             SocketService.post(url, params).then(function (resp) {
    //                 unlockUI();
    //                 if (resp.data.status == true) {

    //                     Notify.alert(resp.data.message, {
    //                         status: 'success',
    //                         pos: 'top-center',
    //                         timeout: 3000
    //                     });

    //                 }
    //                 else {
    //                     Notify.alert(resp.data.message, {
    //                         status: 'warning',
    //                         pos: 'top-center',
    //                         timeout: 3000
    //                     });
    //                 }
    //                 activate();
    //             });

    //             $modalInstance.close('closed');

    //         }
    //         var param = {
    //             Questionnarie_gid: Questionnarie_gid
    //         }

    //         var url = 'api/FndQuestionnarieMaster/QuestionnarieInactiveLogview';
    //         lockUI();
    //         SocketService.getparams(url, param).then(function (resp) {
    //             $scope.statusQuestionnarieinactivelog_list = resp.data.Questionnarie_list;
    //             unlockUI();
    //         });
    //     }
    // }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnRepaymentMatchedImportController', brsTrnRepaymentMatchedImportController);

    brsTrnRepaymentMatchedImportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnRepaymentMatchedImportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnRepaymentMatchedImportController';
        // console.log('test');
        activate();

        function activate() {
          
            var url = 'api/RepaymentReconcillation/GetRepaymentMatchedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.RepaymentMatched = JSON.parse(resp.data.JSONdata);
                    //$scope.limitoffset = parseInt(resp.data.offsetlimit);
                    unlockUI();
                }
                else {
                    unlockUI();
                }

            });

            var url = 'api/RepaymentReconcillation/GetRepaymentSummaryCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.repaymentpending_count = resp.data.repaymentpending_count;
                $scope.repaymentmatched_count = resp.data.repaymentmatched_count;
               

            });
        }

        $scope.repayment_pending = function () {
            $state.go('app.brsTrnRepaymentImport');
        }
        $scope.repayment_completed = function () {
            $state.go('app.brsTrnRepaymentMatchedImport');
        }
       
        $scope.incrementLimit = function () {

            $scope.limitoffset += 6000;

            $scope.limitoffsetinc = $scope.limitoffset;
            $scope.totalcount = parseInt($scope.repaymentmatched_count);
            if ($scope.totalcount < $scope.limitoffsetinc) {
                $scope.increment_flag = 'Y';
            }

            var params = {
                limitoffset_from: $scope.limitoffsetinc
            }
            var url = 'api/RepaymentReconcillation/GetRepaymentMatchedSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                /*   if (resp.data.repayment_list != null) {*/
                if (resp.data.status == true) {
                    $scope.RepaymentMatched = JSON.parse(resp.data.JSONdata);
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
            var url = 'api/RepaymentReconcillation/GetRepaymentMatchedSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                /*   if (resp.data.repayment_list != null) {*/
                if (resp.data.status == true) {
                    $scope.RepaymentMatched = JSON.parse(resp.data.JSONdata);
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
        $scope.deletetemplate = function (repayreconcildoc_gid) {
            var params = {
                repayreconcildoc_gid: repayreconcildoc_gid
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

                    var url = 'api/RepaymentReconcillation/DeleteRepaymentTemplatedata';
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
        $scope.deleterepaymentdata = function (bankrepaytransc_gid) {
            var params = {
                bankrepaytransc_gid: bankrepaytransc_gid
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

                    var url = 'api/RepaymentReconcillation/DeleteRepaymentdata';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting  Repayment Transaction !!!', {
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

        //var limitStep = 6000;
        //$scope.incrementLimit = function () {
        //    $scope.limit += limitStep;
        //};
        //$scope.decrementLimit = function () {
        //    $scope.limit -= limitStep;
        //};

        $scope.importexcel = function () {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
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
                        frm.append('project_flag', "RSK");
                        //frm.append('brsbank_gid', lsbank_gid);
                        //frm.append('bank_name', lsbank_name);
                        // frm.append('questionnarieanswer_gid', questionnarieanswer_gid);
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {


                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {

                        var url = 'api/RepaymentReconcillation/BRSRepaymentExcelSample';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            $modalInstance.close('closed');

                            if (resp.data.status == true) {

                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                // var url = 'api/RepaymentReconcillation/GetRepaymentSummary';
                                // lockUI();
                                // SocketService.get(url).then(function (resp) {
                                //     $scope.repayment_list = resp.data.repayment_list;
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

                            // var url = 'api/RepaymentReconcillation/GetRepaymentSummary';
                            // lockUI();
                            // SocketService.get(url).then(function (resp) {
                            //     $scope.repayment_list = resp.data.repayment_list;
                            //     unlockUI();

                            // });  
                            $("#fileimport").val('');
                            activate();

                        });

                    }

                }
                $scope.uploadexcelcancel = function () {
                    $("#fileimport").val('');
                };


            }


        }
    }

    //Divya


    // $scope.Status_update = function (Questionnarie_gid) {
    //     var modalInstance = $modal.open({
    //         templateUrl: '/statusQuestionnarie.html',
    //         controller: ModalInstanceCtrl,
    //         backdrop: 'static',
    //         keyboard: false,
    //         size: 'md'
    //     });
    //     ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
    //     function ModalInstanceCtrl($scope, $modalInstance) {
    //          var params = {
    //             Questionnarie_gid: Questionnarie_gid
    //         }
    //         var url = 'api/FndQuestionnarieMaster/QuestionnarieEdit';
    //         SocketService.getparams(url, params).then(function (resp) {
    //             $scope.txteditQuestionnarie_code = resp.data.Questionnarie_code;
    //             $scope.txteditlms_code = resp.data.lms_code;
    //             $scope.txteditbureau_code = resp.data.bureau_code;
    //             $scope.txteditQuestionnarie = resp.data.Questionnarie_name;
    //             $scope.txteditremarks = resp.data.remarks;
    //             $scope.Questionnarie_gid = resp.data.Questionnarie_gid;
    //             $scope.rbo_status = resp.data.Status;
    //         });
    //         $scope.ok = function () {
    //             $modalInstance.close('closed');
    //         };
    //         $scope.update_status = function () {

    //             var params = {
    //                 Questionnarie_name: $scope.Questionnarie_name,
    //                 Questionnarie_gid: Questionnarie_gid,
    //                 remarks: $scope.txtremarks,
    //                 rbo_status: $scope.rbo_status

    //             }
    //             var url = 'api/FndQuestionnarieMaster/InactiveQuestionnarie';
    //             lockUI();
    //             SocketService.post(url, params).then(function (resp) {
    //                 unlockUI();
    //                 if (resp.data.status == true) {

    //                     Notify.alert(resp.data.message, {
    //                         status: 'success',
    //                         pos: 'top-center',
    //                         timeout: 3000
    //                     });

    //                 }
    //                 else {
    //                     Notify.alert(resp.data.message, {
    //                         status: 'warning',
    //                         pos: 'top-center',
    //                         timeout: 3000
    //                     });
    //                 }
    //                 activate();
    //             });

    //             $modalInstance.close('closed');

    //         }
    //         var param = {
    //             Questionnarie_gid: Questionnarie_gid
    //         }

    //         var url = 'api/FndQuestionnarieMaster/QuestionnarieInactiveLogview';
    //         lockUI();
    //         SocketService.getparams(url, param).then(function (resp) {
    //             $scope.statusQuestionnarieinactivelog_list = resp.data.Questionnarie_list;
    //             unlockUI();
    //         });
    //     }
    // }

})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnRepaymentReconcillationController', brsTrnRepaymentReconcillationController);

    brsTrnRepaymentReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnRepaymentReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnRepaymentReconcillationController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/RepaymentReconcillation/GetRepaymentReconcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.repayment_cocillation_list = resp.data.repayment_cocillation_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetRepaymentReconcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.repay_reconc_count = resp.data.repay_reconc_count;
                $scope.repay_unreconc_count = resp.data.repay_unreconc_count;

                unlockUI();
            });


        }
        $scope.repay_recon = function () {
            $state.go('app.brsTrnRepaymentReconcillation');
        }

        $scope.repay_unrecon = function () {
            $state.go('app.brsTrnRepaymentUnReconcillation');
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/BrsTrnUnreconRepaymentMatchedView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnRepaymentUnReconcillationController', brsTrnRepaymentUnReconcillationController);

    brsTrnRepaymentUnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnRepaymentUnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnRepaymentUnReconcillationController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/RepaymentReconcillation/GetRepaymentUnReconcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.repayment_unrecocillation_list = resp.data.repayment_unrecocillation_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetRepaymentReconcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.repay_reconc_count = resp.data.repay_reconc_count;
                $scope.repay_unreconc_count = resp.data.repay_unreconc_count;

                unlockUI();
            });


        }
        $scope.repay_recon = function () {
            $state.go('app.brsTrnRepaymentReconcillation');
        }

        $scope.repay_unrecon = function () {
            $state.go('app.brsTrnRepaymentUnReconcillation');
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BrsTrnUnreconAssignedSummaryController', BrsTrnUnreconAssignedSummaryController);

    BrsTrnUnreconAssignedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function BrsTrnUnreconAssignedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = 'BrsTrnUnreconAssignedSummaryController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetBrsUnReconciliationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.BrsUnreconciliation_list = resp.data.BrsUnreconciliation_list;
                unlockUI();
            });
            var url = 'api/UnreconciliationManagement/GetAllocatedCount';
            lockUI();
            SocketService.get(url).then(function (resp) {               
                $scope.unreconciliation_count = resp.data.unreconciliation_count;
                unlockUI();
            });

        }


        $scope.unreconciliationassignedsummary = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconciliationAssignedSummaryExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
        $scope.UnreconciliationView = function (banktransc_gid) {
            //    $location.url('app/brsTrnUnreconcillationTag?banktransc_gid=' + banktransc_gid);
            $location.url("app/BrsTrnUnreconTransactionDetails?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
       

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconCreditClosedManagement');
        }

        $scope.Manualmatch_Update = function (banktransc_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/manualknockoff.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.banktransc_gid = banktransc_gid;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_status = function () {

                    var params =
                    {
                        banktransc_gid: banktransc_gid,
                        // ticket_source: $scope.ticket_source,
                        manualknockoff_remarks: $scope.txtremarks
                    }
                    var url = 'api/UnreconciliationManagement/PostManualMatch';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                            $modalInstance.close('closed');
                            $state.go('app.brsTrnUnreconCreditSummaryManagement');

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();

                    });

                }


            }
        }

        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $state.go('app.brsTrnUnreconCreditSummaryManagement');


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
})();

// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BrsTrnUnreconBankMatchedViewController', BrsTrnUnreconBankMatchedViewController);

    BrsTrnUnreconBankMatchedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function BrsTrnUnreconBankMatchedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'BrsTrnUnreconBankMatchedViewController';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;

        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblbanktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.manualknockoff_remarks = resp.data.manualknockoff_remarks;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblrm_remarks = resp.data.rm_remarks;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.rmsendback_on = resp.data.rmsendback_on;
                $scope.sendback_reason = resp.data.sendback_reason;
                $scope.assignedrm_gid = resp.data.assignedrm_gid;
                $scope.assigned_rm = resp.data.assigned_rm;
                $scope.lblremainingamount = resp.data.remaining_amount;

            });
            var url = 'api/UnreconciliationManagement/GetReassignemployeeLog';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.reassignemployee_list = resp.data.reassignemployee_list;
                unlockUI();
            });
            var url = 'api/UnreconciliationManagement/GetAssignedHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;
            });

            var url = 'api/UnreconciliationManagement/GetTransferredHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlist = resp.data.transferlist;
            });
            var url = 'api/UnreconciliationManagement/GetUnReconciliationClosed';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationcredit_list = resp.data.BankAlertUnreconciliationcredit_list;
                // $scope.Manualknockoff_remarks = $scope.BankAlertUnreconciliationcredit_list.manualknockoff_remarks;
                unlockUI();
            });
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconBankTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }
        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Back = function () {
            $state.go('app.brsTrnCreditMatched');
            
         
        }
    }
})();
// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconcillationCompleteddebitManagementController', brsTrnUnReconcillationCompleteddebitManagementController);

    brsTrnUnReconcillationCompleteddebitManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconcillationCompleteddebitManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconcillationCompleteddebitManagementController';
        // console.log('test');
        activate();

        function activate() {

            //var url = 'api/KotakReconcillation/GetTransactionSummary';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    $scope.transaction_list = resp.data.transaction_list;
            //    unlockUI();

            //});
            var url = 'api/UnreconciliationManagement/GetUnReconciliationAssigned';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationdebit_list = resp.data.BankAlertUnreconciliationdebit_list;
                unlockUI();
            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconpending_count = resp.data.unreconpending_count;
                $scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                $scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                $scope.unreconcomp_count = resp.data.unreconcomp_count;

                unlockUI();
            });
        }
        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnReconcillationManagement');
        }
        $scope.view = function (banktransc_gid) {
           /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconcompdebit = function () {
            $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        }

        $scope.Transfer = function (banktransc_gid) {
        /*//    $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();

// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconcillationCompletedManagement', brsTrnUnReconcillationCompletedManagement);

    brsTrnUnReconcillationCompletedManagement.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconcillationCompletedManagement($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconcillationCompletedManagement';
        // console.log('test');
        activate();

        function activate() {

            //var url = 'api/KotakReconcillation/GetTransactionSummary';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    $scope.transaction_list = resp.data.transaction_list;
            //    unlockUI();

            //});
            var url = 'api/UnreconciliationManagement/GetUnReconciliationAssigned';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationcredit_list = resp.data.BankAlertUnreconciliationcredit_list;
                unlockUI();
            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconpending_count = resp.data.unreconpending_count;
                $scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                $scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                $scope.unreconcomp_count = resp.data.unreconcomp_count;

                unlockUI();
            });

        }
        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnReconcillationManagement');
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconcompdebit = function () {
            $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        }

        $scope.Transfer = function (banktransc_gid) {
            /*   $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();

// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconcillationController', brsTrnUnReconcillationController);

    brsTrnUnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnUnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconcillationController';
        // console.log('test');
        activate();

        function activate() {
            var url = 'api/RepaymentReconcillation/GetunreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.uncocillationcredit_list1 = resp.data.uncocillationcredit_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.reconc_count = resp.data.reconc_count;
                $scope.unreconc_count = resp.data.unreconc_count;
               
                unlockUI();
            });
            var url = "api/RepaymentReconcillation/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.reconc_count = resp.data.reconc_count;
                $scope.unreconc_count = resp.data.unreconc_count;
                $scope.unreconcredit_count = resp.data.unreconcredit_count;
                $scope.unrecondebit_count = resp.data.unrecondebit_count;

                unlockUI();
            });


        }
        $scope.recon = function () {
            $state.go('app.brsTrnReconcillation');
        }
        $scope.unrecon = function () {
            $state.go('app.brsTrnUnReconcillation');
        }

        $scope.unrecondebit = function () {
            $state.go('app.brsTrnUnReconcillationdebit');
        }

    }
})();

// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconcillationdebitController', brsTrnUnReconcillationdebitController);

    brsTrnUnReconcillationdebitController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function brsTrnUnReconcillationdebitController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconcillationdebitController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/RepaymentReconcillation/GetunreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.uncocillationdebit_list = resp.data.uncocillationdebit_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.reconc_count = resp.data.reconc_count;
                $scope.unreconc_count = resp.data.unreconc_count;
                $scope.unreconcredit_count = resp.data.unreconcredit_count;
                $scope.unrecondebit_count = resp.data.unrecondebit_count;

                unlockUI();
            });


        }
        $scope.recon = function () {
            $state.go('app.brsTrnReconcillation');
        }

        $scope.unrecon = function () {
            $state.go('app.brsTrnUnReconcillation');
        }
        $scope.recondebit = function () {
            $state.go('app.brsTrnReconcillationdebit');
        }

    }
})();

// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';


    angular
        .module('angle')
        .controller('brsTrnUnReconcillationPendingdebitManagementController', brsTrnUnReconcillationPendingdebitManagementController);

    brsTrnUnReconcillationPendingdebitManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconcillationPendingdebitManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconcillationPendingdebitManagementController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationpendingdebit_list = resp.data.unrecocillationpendingdebit_list;
                unlockUI();

            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconpending_count = resp.data.unreconpending_count;
                $scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                $scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                $scope.unreconcomp_count = resp.data.unreconcomp_count;

                unlockUI();
            });
        }
        $scope.Unreconciliationtag = function (banktransc_gid) {
        //    $location.url('app/brsTrnUnreconcillationTag?banktransc_gid=' + banktransc_gid);
             $location.url("app/brsTrnUnreconcillationTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
                   //}


        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnReconcillationManagement');
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconpendingdebit = function () {
            $state.go('app.brsTrnUnReconcillationPendingdebitManagement');
        }
        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();



                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.brsTrnUnReconcillationPendingdebitManagement');

            });
            activate();


        }


    }
})();

// JavaScript source code
// (function () {
//     'use strict';

//     angular
//         .module('angle')
//         .controller('brsTrnReconcillationController', brsTrnReconcillationController);
//     brsTrnReconcillationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService', 'cmnfunctionService'];

//     function brsTrnReconcillationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
//         var vm = this;
//         vm.title = 'brsTrnReconcillationController';

//         activate();
//         lockUI();
//         function activate() {


//         }

//     }
// })();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconcillationPendingManagement', brsTrnUnReconcillationPendingManagement);

    brsTrnUnReconcillationPendingManagement.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconcillationPendingManagement($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconcillationPendingManagement';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationpendingcredit_list = resp.data.unrecocillationpendingcredit_list;
                unlockUI();

            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconpending_count = resp.data.unreconpending_count;
                $scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                $scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                $scope.unreconcomp_count = resp.data.unreconcomp_count;

                unlockUI();
            });

        }
        $scope.Unreconciliationtag = function (banktransc_gid) {
        //    $location.url('app/brsTrnUnreconcillationTag?banktransc_gid=' + banktransc_gid);
            $location.url("app/brsTrnUnreconcillationTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
       

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnReconcillationManagement');
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconpendingdebit = function () {
            $state.go('app.brsTrnUnReconcillationPendingdebitManagement');
        }
        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,               
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                   


                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.brsTrnUnReconcillationManagement');

            });
            activate();
           

        }


    }
})();

// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnreconcillationTagController', brsTrnUnreconcillationTagController);

    brsTrnUnreconcillationTagController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnUnreconcillationTagController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'brsTrnUnreconcillationTagController';

        //var banktransc_gid = $location.search().banktransc_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;
        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.banktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblremainingamount = resp.data.remaining_amount;
                $scope.brstransactiondetails_flag = resp.data.brstransactiondetails_flag;
                $scope.brstransactiondetailsadvice_flag = resp.data.brstransactiondetailsadvice_flag;

                if ($scope.lblremainingamount == "0.00")
                {
                   
                    var modalInstance = $modal.open({
                        templateUrl: '/warningmessage.html',
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


                    }
                }

            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            //var url = 'api/employee/Employee';
            //SocketService.get(url).then(function (resp) {
            //    $scope.employee_list1 = resp.data.employee_list;
            //});
            var url = 'api/BRSMaster/GetBRSActivityStatus';
            SocketService.get(url).then(function (resp) {
                $scope.BRSActivity_List = resp.data.BRSActivity_List;
            });
            var url = 'api/UnreconciliationManagement/GetAdjustAdviceEmployeeWiseShow';
            SocketService.get(url).then(function (resp) {
                $scope.adjustadvicelist = resp.data.adjustadvicelist;
            });
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
            var url = 'api/UnreconciliationManagement/GetDepartmentName';
            SocketService.get(url).then(function (resp) {
                $scope.department_name = resp.data.department_name;
                $scope.employeename = resp.data.employee_name;

            });
            var url = 'api/UnreconciliationManagement/GetSamfinCustomerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;

            });

            
        }
        $scope.Back = function () {
            if (lspage == "Credit") {
                $location.url('app/brsTrnUnreconCreditSummaryManagement');
                //$location.url('app\brsTrnUnReconCreditSummaryManagement');
            }
            else if (lspage == "Debit") {
                /* $state.go('app.brsTrnUnReconDebitSummaryManagement');*/
                $location.url('app/brsTrnUnreconDebitSummaryManagement');
            }

        }
        $scope.Submit = function () {

            var params =
            {
                banktransc_gid: banktransc_gid,            
                assigned_to: $scope.cboemployee.employee_gid,
                assigned_toname: $scope.cboemployee.employee_name,
                assigned_remarks: $scope.txtremarks,
            }
            var url = 'api/UnreconciliationManagement/Post2Assign';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == "Credit") {
                        $state.go('app.brsTrnUnreconCreditSummaryManagement');
                    }
                    else if (lspage == "Debit") {
                        $state.go('app.brsTrnUnreconDebitSummaryManagement');
                    }
                   
                }
                else {
                   
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                unlockUI();
            });

        }
        $scope.addtransaction = function () {
            var lsbrsactivityname = '';
            var lsbrsactivitygid = '';
            var lssamfincustomer_name = '';
            var lssamfincustomer_gid = '';
            var lsadjustadvicename = '';
            var lsadjustadvicegid = '';
            if ($scope.cbosamfin != undefined || $scope.cbosamfin != null) {
                lssamfincustomer_gid = $scope.cbosamfin.samfincustomer_gid;
                lssamfincustomer_name = $scope.cbosamfin.samfincustomer_name;
            }

           
            lsbrsactivityname = $('#brsactivity :selected').text();
            lsbrsactivitygid = $scope.cbobrsactivity;
            lsadjustadvicename = $('#adjustadvice :selected').text();
            lsadjustadvicegid = $scope.cboaction;

            if ((($scope.cbobrsactivity == '' || $scope.cbobrsactivity == null) || ($scope.cboaction == '' || $scope.cboaction == null) || ($scope.txt_amount == '' || $scope.txt_amount == null) || ($scope.transactionremarks == '' || $scope.transactionremarks == null))) {
                Notify.alert('Kindly Fill Transaction Details', 'warning')
            }
            else {
                var params =
                {
                    banktransc_gid: banktransc_gid,
                    //assignby_gid: $scope.cboemployee1.employee_gid,
                    assignby_name: $scope.employeename,
                    activity_name: lsbrsactivityname,
                    activity_gid: lsbrsactivitygid,
                    samfincustomer_gid: lssamfincustomer_gid,
                    samfincustomer_name: lssamfincustomer_name,
                    action_name: lsadjustadvicename,
                    department_name: $scope.department_name,
                    amount: $scope.txt_amount,
                    transaction_remarks: $scope.transactionremarks,
                }
                var url = 'api/UnreconciliationManagement/PostUnreconTransactionDetails';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();

                    if (resp.data.status == true)
                    {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        //if (lspage == "Credit") {
                        //    $state.go('app.brsTrnUnreconCreditSummaryManagement');
                        //}
                        //else if (lspage == "Debit") {
                        //    $state.go('app.brsTrnUnreconDebitSummaryManagement');
                        //}
                        unrecontransaction_list();

                        activate();
                    }
                    else
                    {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.cbosamfin = '';
                    $scope.cbobrsactivity = '';
                    $scope.cboaction = '';
                    $scope.txt_amount = '';
                    $scope.transactionremarks = '';
                    activate();
                });
            }
        }
        function unrecontransaction_list()
        {
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }

        
        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.overallSubmit = function () {

            var params =
            {
                banktransc_gid: banktransc_gid,
                cbounreconciliation_status: $scope.cborm_status,
                /*brs_status: $scope.brs_status,*/
                updation_remarks: $scope.txtremarks

            }
            var url = 'api/UnreconciliationManagement/UnconPendingStatusUpdation';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/brsTrnUnreconCreditSummaryManagement');

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
        $scope.deletetransaction = function (unrecontransactiondetails_gid, banktransc_gid) {
            var params = {
                unrecontransactiondetails_gid: unrecontransactiondetails_gid,
                banktransc_gid: banktransc_gid

            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionDelete';
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
                unrecontransaction_list();
                activate();
            });
        }


        
        $scope.limit_amountChange = function () {
            var input = document.getElementById('limit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = '';
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_limitamt').innerHTML = lswords_creditamount;
            }
        } 
    }
    }) ();
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnreconcillationTagReassignController', brsTrnUnreconcillationTagReassignController);

    brsTrnUnreconcillationTagReassignController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnUnreconcillationTagReassignController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'brsTrnUnreconcillationTagReassignController';

        //var banktransc_gid = $location.search().banktransc_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;
        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.banktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.assigned_rm = resp.data.assigned_rm;
                $scope.rmsendback_on = resp.data.rmsendback_on;
                $scope.sendback_reason = resp.data.sendback_reason;
                $scope.assignedrm_gid = resp.data.assignedrm_gid;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblremainingamount = resp.data.remaining_amount;

            });
            var url = 'api/UnreconciliationManagement/GetReassignemployeeLog';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.reassignemployee_list = resp.data.reassignemployee_list;
                unlockUI();
            });
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
            var param =            {
                tagemployee_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetReassignEmployee';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/UnreconciliationManagement/GetAdjustAdviceEmployeeWiseShow';
            SocketService.get(url).then(function (resp) {
                $scope.adjustadvicelist = resp.data.adjustadvicelist;
            });
           
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list1 = resp.data.employee_list;
            });
            var url = 'api/BRSMaster/GetBRSActivityStatus';
            SocketService.get(url).then(function (resp) {
                $scope.BRSActivity_List = resp.data.BRSActivity_List;
            });
            var url = 'api/UnreconciliationManagement/GetDepartmentName';
            SocketService.get(url).then(function (resp) {
                $scope.department_name = resp.data.department_name;
                $scope.employeename = resp.data.employee_name;

            });
            var url = 'api/UnreconciliationManagement/GetAssignedHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assigned_list = resp.data.assignedlist;
            });
            var url = 'api/UnreconciliationManagement/GetSendBackHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.sendbackemployee_list = resp.data.sendbacklist;
            });
            var url = 'api/UnreconciliationManagement/GetSamfinCustomerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;

            });
        }
        $scope.Back = function () {
            if (lspage == "Credit") {
                $location.url('app/brsTrnUnreconCreditSummaryManagement');
                //$location.url('app\brsTrnUnReconCreditSummaryManagement');
            }
            else if (lspage == "Debit") {
                /* $state.go('app.brsTrnUnReconDebitSummaryManagement');*/
                $location.url('app/brsTrnUnreconDebitSummaryManagement');
            }

        }
        $scope.limit_amountChange = function () {
            var input = document.getElementById('limit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_limitamt').innerHTML = lswords_creditamount;
            }
        } 
        $scope.Submit = function () {

            var params =
            {
                banktransc_gid: banktransc_gid,            
                assigned_to: $scope.cboemployee.employee_gid,
                assigned_toname: $scope.cboemployee.employee_name,
                assigned_remarks: $scope.txtremarks,
            }
            var url = 'api/UnreconciliationManagement/Post2ReAssign';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == "Credit") {
                        $state.go('app.brsTrnUnreconCreditSummaryManagement');
                    }
                    else if (lspage == "Debit") {
                        $state.go('app.brsTrnUnreconDebitSummaryManagement');
                    }
                   
                }
                else {
                   
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                unlockUI();
            });

        }
        $scope.addtransaction = function () {
            var lsbrsactivityname = '';
            var lsbrsactivitygid = '';
            var lssamfincustomer_name = '';
            var lssamfincustomer_gid = '';
            var lsadjustadvicename = '';
            var lsadjustadvicegid = '';
            if ($scope.cbosamfin != undefined || $scope.cbosamfin != null) {
                lssamfincustomer_gid = $scope.cbosamfin.samfincustomer_gid;
                lssamfincustomer_name = $scope.cbosamfin.samfincustomer_name;
            }


            lsbrsactivityname = $('#brsactivity :selected').text();
            lsbrsactivitygid = $scope.cbobrsactivity;
            lsadjustadvicename = $('#adjustadvice :selected').text();
            lsadjustadvicegid = $scope.cboaction;
            if ((($scope.cbobrsactivity == '' || $scope.cbobrsactivity == null) || ($scope.cboaction == '' || $scope.cboaction == null) || ($scope.txt_amount == '' || $scope.txt_amount == null) || ($scope.transactionremarks == '' || $scope.transactionremarks == null))) {
                Notify.alert('Kindly Fill Transaction Details', 'warning')
            }
            else {
                var params =
                {
                    banktransc_gid: banktransc_gid,
                    //assignby_gid: $scope.cboemployee1.employee_gid,
                    assignby_name: $scope.employeename,
                    activity_name: lsbrsactivityname,
                    activity_gid: lsbrsactivitygid,
                    samfincustomer_gid: lssamfincustomer_gid,
                    samfincustomer_name: lssamfincustomer_name,
                    action_name: lsadjustadvicename,
                    department_name: $scope.department_name,
                    amount: $scope.txt_amount,
                    transaction_remarks: $scope.transactionremarks,
                }
                var url = 'api/UnreconciliationManagement/PostUnreconTransactionDetails';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        //if (lspage == "Credit") {
                        //    $state.go('app.brsTrnUnreconCreditSummaryManagement');
                        //}
                        //else if (lspage == "Debit") {
                        //    $state.go('app.brsTrnUnreconDebitSummaryManagement');
                        //}
                        unrecontransaction_list();
                        activate();
                    }
                    else {

                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.cbosamfin = '';
                    $scope.cbobrsactivity = '';
                    $scope.cboaction = '';
                    $scope.txt_amount = '';
                    $scope.transactionremarks = '';
                    unlockUI();
                    activate();

                });
            }
        }
        function unrecontransaction_list() {
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }
        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.deletetransaction = function (unrecontransactiondetails_gid, banktransc_gid) {
            var params = {
                unrecontransactiondetails_gid: unrecontransactiondetails_gid,
                banktransc_gid: banktransc_gid

            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionDelete';
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
                unrecontransaction_list();
                activate();

            });
        }

    }
    }) ();
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnreconcillationTransferController', brsTrnUnreconcillationTransferController);

    brsTrnUnreconcillationTransferController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnUnreconcillationTransferController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'brsTrnUnreconcillationTransferController';

         //var banktransc_gid = $location.search().banktransc_gid;

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;

        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.banktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.baselocation_name = resp.data.baselocation_name;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblremainingamount = resp.data.remaining_amount;


            });
            var url = 'api/UnreconciliationManagement/GetEmployee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list1 = resp.data.employee_list;
            });
            var url = 'api/BRSMaster/GetBRSActivityStatus';
            SocketService.get(url).then(function (resp) {
                $scope.BRSActivity_List = resp.data.BRSActivity_List;
            });
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
            var url = 'api/UnreconciliationManagement/GetDepartmentName';
            SocketService.get(url).then(function (resp) {
                $scope.department_name = resp.data.department_name;
                $scope.employeename = resp.data.employee_name;

            });
            var url = 'api/UnreconciliationManagement/GetSamfinCustomerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;

            });
        }

       

        $scope.Submit = function () {

            var params =
            {
                banktransc_gid: banktransc_gid,
                transfer_to: $scope.cboemployee.employee_gid,
                transfer_toname: $scope.cboemployee.employee_name,
                transfer_reason: $scope.txtreason,
            }
            var url = 'api/UnreconciliationManagement/Post2Transfer';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.brsTrnUnReconCreditAssignedManagement');

                }
                else {
                  
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                unlockUI();
            });

        }

        $scope.Back = function () {
            if (lspage == "Credit") {
                $state.go('app.brsTrnUnReconCreditAssignedManagement');
            }
            else if (lspage == "Debit") {
                $state.go('app.brsTrnUnReconDebitAssignedManagement');
            }

        }
        $scope.addtransaction = function () {
            var lsbrsactivityname = '';
            var lsbrsactivitygid = '';
            var lssamfincustomer_name = '';
            var lssamfincustomer_gid = '';
            var lsadjustadvicename = '';
            var lsadjustadvicegid = '';
            if ($scope.cbosamfin != undefined || $scope.cbosamfin != null) {
                lssamfincustomer_gid = $scope.cbosamfin.samfincustomer_gid;
                lssamfincustomer_name = $scope.cbosamfin.samfincustomer_name;
            }


            lsbrsactivityname = $('#brsactivity :selected').text();
            lsbrsactivitygid = $scope.cbobrsactivity;
            lsadjustadvicename = $('#adjustadvice :selected').text();
            lsadjustadvicegid = $scope.cboaction;

            if ((($scope.cbobrsactivity == '' || $scope.cbobrsactivity == null) || ($scope.cboaction == '' || $scope.cboaction == null) || ($scope.txt_amount == '' || $scope.txt_amount == null) || ($scope.transactionremarks == '' || $scope.transactionremarks == null))) {
                Notify.alert('Kindly Fill Transaction Details', 'warning')
            }
            else {
                var params =
                {
                    banktransc_gid: banktransc_gid,
                    //assignby_gid: $scope.cboemployee1.employee_gid,
                    assignby_name: $scope.employeename,
                    activity_name: lsbrsactivityname,
                    activity_gid: lsbrsactivitygid,
                    samfincustomer_gid: lssamfincustomer_gid,
                    samfincustomer_name: lssamfincustomer_name,
                    action_name: lsadjustadvicename,
                    department_name: $scope.department_name,
                    amount: $scope.txt_amount,
                    transaction_remarks: $scope.transactionremarks,
                }
                var url = 'api/UnreconciliationManagement/PostUnreconTransactionDetails';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        //if (lspage == "Credit") {
                        //    $state.go('app.brsTrnUnreconCreditSummaryManagement');
                        //}
                        //else if (lspage == "Debit") {
                        //    $state.go('app.brsTrnUnreconDebitSummaryManagement');
                        //}
                        unrecontransaction_list();
                    }
                    else {

                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.cbosamfin = '';
                    $scope.cbobrsactivity = '';
                    $scope.cboaction = '';
                    $scope.txt_amount = '';
                    $scope.transactionremarks = '';
                    unlockUI();
                    activate();

                });
            }
        }
        function unrecontransaction_list() {
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }
        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.deletetransaction = function (unrecontransactiondetails_gid, banktransc_gid) {
            var params = {
                unrecontransactiondetails_gid: unrecontransactiondetails_gid,
                banktransc_gid: banktransc_gid

            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionDelete';
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
                unrecontransaction_list();
                activate();

            });
        }
        $scope.limit_amountChange = function () {
            var input = document.getElementById('limit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_limitamt').innerHTML = lswords_creditamount;
            }
        } 
    }
})();



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconCreditAssignedManagementController', brsTrnUnReconCreditAssignedManagementController);

    brsTrnUnReconCreditAssignedManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconCreditAssignedManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconCreditAssignedManagementController';
        // console.log('test');
        activate();

        function activate() {

            // var url = 'api/UnreconciliationManagement/GetTransferreddisablestatus';
            // lockUI();
            // SocketService.get(url).then(function (resp) {
            //    $scope.brs_status = resp.data.brs_status;
            //    unlockUI();

            // });
            var url = 'api/UnreconciliationManagement/GetUnReconciliationAssigned';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationcredit_list = resp.data.BankAlertUnreconciliationcredit_list;
                unlockUI();
            });

            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;
                $scope.fin_count = resp.data.unreconfin_count;               

                //$scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                //$scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                //$scope.unreconpending_count = resp.data.unreconpending_count;
                //$scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                //$scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                //$scope.unreconcomp_count = resp.data.unreconcomp_count;

                unlockUI();
            });

        }
        $scope.unreconcreditassignedsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconCreditAssignedManagementExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
       $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }

        $scope.unreconCRreassigned = function () {
            $state.go('app.brsTrnUnreconCreditReassignpendingSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconCreditAssignedManagement');
        }
        $scope.finpending = function () {
            $state.go('app.brsTrnUnreconCreditFinancePendingManagement');
        }
        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconCreditClosedManagement');
        }
       
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid+ '&lspage=CreditAssign'));
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconcompdebit = function () {
            $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        }

        $scope.Transfer = function (banktransc_gid) {
            /*   $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Credit'));
        }
    }
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconCreditClosedManagementController', brsTrnUnReconCreditClosedManagementController);

    brsTrnUnReconCreditClosedManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconCreditClosedManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        var lspage = $location.search().lspage;
        vm.title = 'brsTrnUnReconCreditClosedManagementController';
        // console.log('test');
        activate();

        function activate() {

            //var url = 'api/KotakReconcillation/GetTransactionSummary';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    $scope.transaction_list = resp.data.transaction_list;
            //    unlockUI();

            //});
            var url = 'api/UnreconciliationManagement/GetUnReconciliationClosed';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationcredit_list = resp.data.BankAlertUnreconciliationcredit_list;
                unlockUI();
            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;
                $scope.fin_count = resp.data.unreconfin_count;               

                //$scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                //$scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                //$scope.unreconpending_count = resp.data.unreconpending_count;
                //$scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                //$scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                //$scope.unreconcomp_count = resp.data.unreconcomp_count;

                unlockUI();
            });

        }
        $scope.unreconcreditclosedsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconCreditClosedManagementExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
       $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.unreconCRreassigned = function () {
            $state.go('app.brsTrnUnreconCreditReassignpendingSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconCreditAssignedManagement');
        }
        $scope.finpending = function () {
            $state.go('app.brsTrnUnreconCreditFinancePendingManagement');
        }
        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconCreditClosedManagement');
        }
       
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=CreditClose'));
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconcompdebit = function () {
            $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        }

        $scope.requestback = function () {
            if (lspage == "reopenactivity") {
                $location.url('app/osdTrnReopenRequestSummary');
            }
            else if (lspage == "closeactivity") {
                $location.url('app/osdTrnCloseRequestSummary');
            }
            else if (lspage == "rejectedrequest") {
                $location.url('app/osdTrnRejectedRequestSummary');
            }
            else {
                $location.url('app/osdTrnServiceRequestSummary');
            }
        }

        $scope.Transfer = function (banktransc_gid) {
            /*   $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnreconCreditFinancePendingManagementController', brsTrnUnreconCreditFinancePendingManagementController);

    brsTrnUnreconCreditFinancePendingManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnUnreconCreditFinancePendingManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = 'brsTrnUnreconCreditFinancePendingManagementController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConcillationFinanceSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationfinpendingcredit_list = resp.data.unrecocillationfinpendingcredit_list;
                unlockUI();

            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;                
                $scope.fin_count = resp.data.unreconfin_count;                
                unlockUI();
            });

        }
        $scope.unreconcreditfinancependingsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconCreditFinancePendingManagementExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
        $scope.Unreconciliationtag = function (banktransc_gid) {
            //    $location.url('app/brsTrnUnreconcillationTag?banktransc_gid=' + banktransc_gid);
            $location.url("app/brsTrnUnreconcillationTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Credit'));
        }
        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.finpending = function () {
            $state.go('app.brsTrnUnreconCreditFinancePendingManagement');
        }

        $scope.unreconCRreassigned = function () {
            $state.go('app.brsTrnUnreconCreditReassignpendingSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconCreditAssignedManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconCreditClosedManagement');
        }

        $scope.Manualmatch_Update = function (banktransc_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/manualknockoff.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.banktransc_gid = banktransc_gid;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_status = function () {

                    var params =
                    {
                        banktransc_gid: banktransc_gid,
                        // ticket_source: $scope.ticket_source,
                        manualknockoff_remarks: $scope.txtremarks
                    }
                    var url = 'api/UnreconciliationManagement/PostManualMatch';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                            $modalInstance.close('closed');
                            $state.go('app.brsTrnUnreconCreditSummaryManagement');

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();

                    });

                }


            }
        }

        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $state.go('app.brsTrnUnreconCreditSummaryManagement');


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
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconCreditReassignSummaryManagementController', brsTrnUnReconCreditReassignSummaryManagementController);

    brsTrnUnReconCreditReassignSummaryManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconCreditReassignSummaryManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = 'brsTrnUnReconCreditReassignSummaryManagementController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConreassignpendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationpendingcredit_list = resp.data.unrecocillationpendingcredit_list;
                unlockUI();

            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;
                $scope.fin_count = resp.data.unreconfin_count;               

                //$scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                //$scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                //$scope.unreconpending_count = resp.data.unreconpending_count;
                //$scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                //$scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                //$scope.unreconcomp_count = resp.data.unreconcomp_count;
                unlockUI();
            });

        }
        $scope.unreconcredittreassignpendingsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconCreditReassignPendingSummaryManagementExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
        $scope.Unreconciliationtag = function (banktransc_gid) {
        //    $location.url('app/brsTrnUnreconcillationTag?banktransc_gid=' + banktransc_gid);
            $location.url("app/brsTrnUnreconcillationReassignTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Credit'));
        }
        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }

        $scope.unreconCRreassigned = function () {
            $state.go('app.brsTrnUnreconCreditReassignpendingSummaryManagement');
        }
        $scope.finpending = function () {
            $state.go('app.brsTrnUnreconCreditFinancePendingManagement');
        }
        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconCreditAssignedManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconCreditClosedManagement');
        }

        $scope.Manualmatch_Update = function (banktransc_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/manualknockoff.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.banktransc_gid = banktransc_gid;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_status = function () {

                    var params =
                    {
                        banktransc_gid: banktransc_gid,
                        // ticket_source: $scope.ticket_source,
                        manualknockoff_remarks: $scope.txtremarks
                    }
                    var url = 'api/UnreconciliationManagement/PostManualMatch';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                            $modalInstance.close('closed');
                            $state.go('app.brsTrnUnreconCreditSummaryManagement');

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();

                    });

                }

              
            }
        }

        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,               
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $state.go('app.brsTrnUnreconCreditSummaryManagement');


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
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconCreditSummaryManagementController', brsTrnUnReconCreditSummaryManagementController);

    brsTrnUnReconCreditSummaryManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconCreditSummaryManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = 'brsTrnUnReconCreditSummaryManagementController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationpendingcredit_list = resp.data.unrecocillationpendingcredit_list;
                unlockUI();

            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;
                $scope.fin_count = resp.data.unreconfin_count;               

                //$scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                //$scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                //$scope.unreconpending_count = resp.data.unreconpending_count;
                //$scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                //$scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                //$scope.unreconcomp_count = resp.data.unreconcomp_count;
                unlockUI();
            });

        }

        $scope.unreconcreditsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconCreditSummaryManagementExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }

        $scope.Unreconciliationtag = function (banktransc_gid) {
        //    $location.url('app/brsTrnUnreconcillationTag?banktransc_gid=' + banktransc_gid);
            $location.url("app/brsTrnUnreconcillationTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Credit'));
        }
        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.finpending = function () {
            $state.go('app.brsTrnUnreconCreditFinancePendingManagement');
        }

        $scope.unreconCRreassigned = function () {
            $state.go('app.brsTrnUnreconCreditReassignpendingSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconCreditAssignedManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconCreditClosedManagement');
        }

        $scope.Manualmatch_Update = function (banktransc_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/manualknockoff.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.banktransc_gid = banktransc_gid;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_status = function () {

                    var params =
                    {
                        banktransc_gid: banktransc_gid,
                        // ticket_source: $scope.ticket_source,
                        manualknockoff_remarks: $scope.txtremarks
                    }
                    var url = 'api/UnreconciliationManagement/PostManualMatch';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                            $modalInstance.close('closed');
                            $state.go('app.brsTrnUnreconCreditSummaryManagement');

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();

                    });

                }

              
            }
        }

        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,               
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $state.go('app.brsTrnUnreconCreditSummaryManagement');


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
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconDebitAssignedManagementController', brsTrnUnReconDebitAssignedManagementController);

    brsTrnUnReconDebitAssignedManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnUnReconDebitAssignedManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconDebitAssignedManagementController';
        // console.log('test');
        activate();

        function activate() {

            // var url = 'api/UnreconciliationManagement/GetTransferreddisablestatus';
            // lockUI();
            // SocketService.get(url).then(function (resp) {
            //    $scope.brs_status = resp.data.brs_status;
            //    unlockUI();

            // });
            var url = 'api/UnreconciliationManagement/GetUnReconciliationAssigned';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationdebit_list = resp.data.BankAlertUnreconciliationdebit_list;
                unlockUI();
            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;
                $scope.findebit_count = resp.data.unrecondebitfin_count; 

                unlockUI();
            });

        }

        $scope.unrecondebitassignedpendingsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconDebitAssignedManagementExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }
        $scope.findebitpending = function () {
            $state.go('app.brsTrnUnreconDebitFinancePendingManagement');
        }
        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }
        $scope.unreconDBreassigned = function () {
            $state.go('app.brsTrnUnreconDebitReassignpendingSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconDebitAssignedManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconDebitCloseManagement');
        }

        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=DebitAssign'));
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconcompdebit = function () {
            $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        }

        $scope.Transfer = function (banktransc_gid) {
            /*   $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Debit'));
        }
    }
})();

// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BrsTrnUnreconDebitBankMatchedViewController', BrsTrnUnreconDebitBankMatchedViewController);

    BrsTrnUnreconDebitBankMatchedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function BrsTrnUnreconDebitBankMatchedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'BrsTrnUnreconDebitBankMatchedViewController';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;

        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblbanktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.manualknockoff_remarks = resp.data.manualknockoff_remarks;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblrm_remarks = resp.data.rm_remarks;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.rmsendback_on = resp.data.rmsendback_on;
                $scope.sendback_reason = resp.data.sendback_reason;
                $scope.assignedrm_gid = resp.data.assignedrm_gid;
                $scope.assigned_rm = resp.data.assigned_rm;
                $scope.lblremainingamount = resp.data.remaining_amount;

            });
            var url = 'api/UnreconciliationManagement/GetReassignemployeeLog';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.reassignemployee_list = resp.data.reassignemployee_list;
                unlockUI();
            });
            var url = 'api/UnreconciliationManagement/GetAssignedHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;
            });

            var url = 'api/UnreconciliationManagement/GetTransferredHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlist = resp.data.transferlist;
            });
            var url = 'api/UnreconciliationManagement/GetUnReconciliationClosed';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationcredit_list = resp.data.BankAlertUnreconciliationcredit_list;
                // $scope.Manualknockoff_remarks = $scope.BankAlertUnreconciliationcredit_list.manualknockoff_remarks;
                unlockUI();
            });
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconBankTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }
        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Back = function () {
            $state.go('app.brsTrnDebitMatched');


        }
    }
})();



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconDebitClosedManagementController', brsTrnUnReconDebitClosedManagementController);

    brsTrnUnReconDebitClosedManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnUnReconDebitClosedManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconDebitClosedManagementController';
        // console.log('test');
        activate();

        function activate() {

            //var url = 'api/KotakReconcillation/GetTransactionSummary';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    $scope.transaction_list = resp.data.transaction_list;
            //    unlockUI();

            //});
            var url = 'api/UnreconciliationManagement/GetUnReconciliationClosed';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationdebit_list = resp.data.BankAlertUnreconciliationdebit_list;
                unlockUI();
            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.findebit_count = resp.data.unrecondebitfin_count;
                //$scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                //$scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                //$scope.unreconpending_count = resp.data.unreconpending_count;
                //$scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                //$scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                //$scope.unreconcomp_count = resp.data.unreconcomp_count;

                unlockUI();
            });

        }

        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconDebitAssignedManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconDebitClosedManagement');
        }

        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=DebitClose'));
        }

        //$scope.compunrecon = function () {
        //    $state.go('app.brsTrnUnReconcillationCompletedManagement');
        //}
        //$scope.unreconcompdebit = function () {
        //    $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        //}

        $scope.Transfer = function (banktransc_gid) {
            /*   $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconDebitCloseManagementController', brsTrnUnReconDebitCloseManagementController);

    brsTrnUnReconDebitCloseManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnUnReconDebitCloseManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        var lspage = $location.search().lspage;
        vm.title = 'brsTrnUnReconDebitCloseManagementController';
        // console.log('test');
        activate();

        function activate() {

            //var url = 'api/KotakReconcillation/GetTransactionSummary';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    $scope.transaction_list = resp.data.transaction_list;
            //    unlockUI();

            //});
            var url = 'api/UnreconciliationManagement/GetUnReconciliationClosed';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationdebit_list = resp.data.BankAlertUnreconciliationdebit_list;
                unlockUI();
            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;
                $scope.findebit_count = resp.data.unrecondebitfin_count;

                unlockUI();
            });

        }
        $scope.unrecondebitclosedsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconDebitClosedManagementExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }
        $scope.findebitpending = function () {
            $state.go('app.brsTrnUnreconDebitFinancePendingManagement');
        }
        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconDebitAssignedManagement');
        }
        $scope.unreconDBreassigned = function () {
            $state.go('app.brsTrnUnreconDebitReassignpendingSummaryManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconDebitCloseManagement');
        }

        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=DebitClose'));
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconcompdebit = function () {
            $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        }

        $scope.requestback = function () {
            if (lspage == "reopenactivity") {
                $location.url('app/osdTrnReopenRequestSummary');
            }
            else if (lspage == "closeactivity") {
                $location.url('app/osdTrnCloseRequestSummary');
            }
            else if (lspage == "rejectedrequest") {
                $location.url('app/osdTrnRejectedRequestSummary');
            }
            else {
                $location.url('app/osdTrnServiceRequestSummary');
            }
        }

        $scope.Transfer = function (banktransc_gid) {
            /*   $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnreconDebitFinancePendingManagementController', brsTrnUnreconDebitFinancePendingManagementController);

    brsTrnUnreconDebitFinancePendingManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnUnreconDebitFinancePendingManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnreconDebitFinancePendingManagementController';
        // console.log('test');

        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConcillationFinanceSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationfinpendingdebit_list = resp.data.unrecocillationfinpendingdebit_list;
                unlockUI();

            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;
                $scope.findebit_count = resp.data.unrecondebitfin_count;


                unlockUI();
            });

        }

        $scope.unrecondebitfinancependingsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconDebitFinancePendingManagementExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
        $scope.Unreconciliationtag = function (banktransc_gid) {

            $location.url("app/brsTrnUnreconcillationTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Debit'));
        }
        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.findebitpending = function () {
            $state.go('app.brsTrnUnreconDebitFinancePendingManagement');
        }
        $scope.unreconDBreassigned = function () {
            $state.go('app.brsTrnUnreconDebitReassignpendingSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconDebitAssignedManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconDebitCloseManagement');
        }

        $scope.Manualmatch_Update = function (banktransc_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/manualknockoff.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.banktransc_gid = banktransc_gid;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_status = function () {

                    var params =
                    {
                        banktransc_gid: banktransc_gid,
                        // ticket_source: $scope.ticket_source,
                        manualknockoff_remarks: $scope.txtremarks
                    }
                    var url = 'api/UnreconciliationManagement/PostManualMatch';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                            $modalInstance.close('closed');
                            $state.go('app.brsTrnUnreconDebitSummaryManagement');

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }


                    });
                    unlockUI();

                }
                $modalInstance.close('closed');

            }
        }

        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();



                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.brsTrnUnreconDebitSummaryManagement');

            });
            activate();


        }


    }
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconDebitReassignSummaryManagementController', brsTrnUnReconDebitReassignSummaryManagementController);

    brsTrnUnReconDebitReassignSummaryManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconDebitReassignSummaryManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = 'brsTrnUnReconDebitReassignSummaryManagementController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConreassignpendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationpendingdebit_list = resp.data.unrecocillationpendingdebit_list;
                unlockUI();

            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;
                $scope.findebit_count = resp.data.unrecondebitfin_count;
                //$scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                //$scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                //$scope.unreconpending_count = resp.data.unreconpending_count;
                //$scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                //$scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                //$scope.unreconcomp_count = resp.data.unreconcomp_count;
                unlockUI();
            });

        }
        $scope.unrecondebitreassignpendingsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconDebitReassignPendingManagementExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
        $scope.Unreconciliationtag = function (banktransc_gid) {
        //    $location.url('app/brsTrnUnreconcillationTag?banktransc_gid=' + banktransc_gid);
            $location.url("app/brsTrnUnreconcillationReassignTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Debit'));
        }
        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }
        $scope.findebitpending = function () {
            $state.go('app.brsTrnUnreconDebitFinancePendingManagement');
        }
        $scope.unreconDBreassigned = function () {
            $state.go('app.brsTrnUnreconDebitReassignpendingSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconDebitAssignedManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconDebitClosedManagement');
        }

        $scope.Manualmatch_Update = function (banktransc_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/manualknockoff.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.banktransc_gid = banktransc_gid;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_status = function () {

                    var params =
                    {
                        banktransc_gid: banktransc_gid,
                        // ticket_source: $scope.ticket_source,
                        manualknockoff_remarks: $scope.txtremarks
                    }
                    var url = 'api/UnreconciliationManagement/PostManualMatch';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                            $modalInstance.close('closed');
                            $state.go('app.brsTrnUnreconCreditSummaryManagement');

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();

                    });

                }

              
            }
        }

        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,               
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $state.go('app.brsTrnUnreconCreditSummaryManagement');


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
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnreconDebitSummaryManagementController', brsTrnUnreconDebitSummaryManagementController);

    brsTrnUnreconDebitSummaryManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnreconDebitSummaryManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnreconDebitSummaryManagementController';
        // console.log('test');
        
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConcillationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationpendingdebit_list = resp.data.unrecocillationpendingdebit_list;
                unlockUI();

            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;
                $scope.findebit_count = resp.data.unrecondebitfin_count;

                unlockUI();
            });

        }
        $scope.unrecondebitpendingsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconDebitPendingManagementExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
        $scope.Unreconciliationtag = function (banktransc_gid) {
     
            $location.url("app/brsTrnUnreconcillationTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Debit'));
        }
        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.findebitpending = function () {
          $state.go('app.brsTrnUnreconDebitFinancePendingManagement');
        }
        $scope.unreconDBreassigned = function () {
            $state.go('app.brsTrnUnreconDebitReassignpendingSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconDebitAssignedManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconDebitCloseManagement');
        }

        $scope.Manualmatch_Update = function (banktransc_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/manualknockoff.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
               
                $scope.banktransc_gid = banktransc_gid;
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
                $scope.update_status = function () {

                    var params =
                    {
                        banktransc_gid: banktransc_gid,
                       // ticket_source: $scope.ticket_source,
                        manualknockoff_remarks: $scope.txtremarks
                    }
                    var url = 'api/UnreconciliationManagement/PostManualMatch';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                            $modalInstance.close('closed');
                            $state.go('app.brsTrnUnreconDebitSummaryManagement');

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                       

                    });
                    unlockUI();
                    
                }
                $modalInstance.close('closed');
              
            }
        }

        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,               
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                   


                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.brsTrnUnreconDebitSummaryManagement');

            });
            activate();
           

        }


    }
})();

// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BrsTrnUnreconRepaymentMatchedViewController', BrsTrnUnreconRepaymentMatchedViewController);

    BrsTrnUnreconRepaymentMatchedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function BrsTrnUnreconRepaymentMatchedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'BrsTrnUnreconRepaymentMatchedViewController';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;

        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblbanktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.manualknockoff_remarks = resp.data.manualknockoff_remarks;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblrm_remarks = resp.data.rm_remarks;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.rmsendback_on = resp.data.rmsendback_on;
                $scope.sendback_reason = resp.data.sendback_reason;
                $scope.assignedrm_gid = resp.data.assignedrm_gid;
                $scope.assigned_rm = resp.data.assigned_rm;
                $scope.lblremainingamount = resp.data.remaining_amount;

            });
            var url = 'api/UnreconciliationManagement/GetReassignemployeeLog';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.reassignemployee_list = resp.data.reassignemployee_list;
                unlockUI();
            });
            var url = 'api/UnreconciliationManagement/GetAssignedHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;
            });

            var url = 'api/UnreconciliationManagement/GetTransferredHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlist = resp.data.transferlist;
            });
            var url = 'api/UnreconciliationManagement/GetUnReconciliationClosed';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationcredit_list = resp.data.BankAlertUnreconciliationcredit_list;
                // $scope.Manualknockoff_remarks = $scope.BankAlertUnreconciliationcredit_list.manualknockoff_remarks;
                unlockUI();
            });
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconBankTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }
        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Back = function () {
            $state.go('app.brsTrnRepaymentReconcillation');


        }
    }
})();
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnreconTagViewAssignedHistoryController', brsTrnUnreconTagViewAssignedHistoryController);

    brsTrnUnreconTagViewAssignedHistoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnUnreconTagViewAssignedHistoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'brsTrnUnreconTagViewAssignedHistoryController';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;

        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblbanktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.manualknockoff_remarks = resp.data.manualknockoff_remarks;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblrm_remarks = resp.data.rm_remarks;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.rmsendback_on = resp.data.rmsendback_on;
                $scope.sendback_reason = resp.data.sendback_reason;
                $scope.assignedrm_gid = resp.data.assignedrm_gid;
                $scope.assigned_rm = resp.data.assigned_rm;
                $scope.lblremainingamount = resp.data.remaining_amount;

            });
            var url = 'api/UnreconciliationManagement/GetReassignemployeeLog';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.reassignemployee_list = resp.data.reassignemployee_list;
                unlockUI();
            });
            var url = 'api/UnreconciliationManagement/GetAssignedHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;
            });

            var url = 'api/UnreconciliationManagement/GetTransferredHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlist = resp.data.transferlist;
            });
            var url = 'api/UnreconciliationManagement/GetUnReconciliationClosed';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationcredit_list = resp.data.BankAlertUnreconciliationcredit_list;
                // $scope.Manualknockoff_remarks = $scope.BankAlertUnreconciliationcredit_list.manualknockoff_remarks;
                unlockUI();
            });
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }

        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.Back = function () {
            if (lspage == "CreditAssign") {
                $state.go('app.brsTrnUnReconCreditAssignedManagement');
            }
            else if (lspage == "DebitAssign") {
                $state.go('app.brsTrnUnReconDebitAssignedManagement');
            }
            else if (lspage == "CreditClose") {
                $state.go('app.brsTrnUnReconCreditClosedManagement');
            }
            else if (lspage == "DebitClose") {
                $state.go('app.brsTrnUnReconDebitCloseManagement');
            }
          
        }
    }
})();
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BrsTrnUnreconTransactionDetailsController', BrsTrnUnreconTransactionDetailsController);

    BrsTrnUnreconTransactionDetailsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService', 'DownloaddocumentService'];

    function BrsTrnUnreconTransactionDetailsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService, DownloaddocumentService) {

        var vm = this;
        vm.title = 'BrsTrnUnreconTransactionDetailsController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;
      
        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblbanktransc_gid = banktransc_gid;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbltrn_date = resp.data.trn_date;
                $scope.lblrm_status = resp.data.rm_status;
                $scope.lblrm_remarks = resp.data.rm_remarks;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblremarks = resp.data.remarks;
                $scope.lblremainingamount = resp.data.remaining_amount;
                $scope.brstransactiondetails_flag = resp.data.brstransactiondetails_flag;
                $scope.brstransactiondetailsadvice_flag = resp.data.brstransactiondetailsadvice_flag;
                if ($scope.lblremainingamount == "0.00") {

                    var modalInstance = $modal.open({
                        templateUrl: '/warningmessage.html',
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


                    }
                }

            });

            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list1 = resp.data.employee_list;
            });
            var url = 'api/UnreconciliationManagement/GetAdjustAdviceEmployeeWiseShow';
            SocketService.get(url).then(function (resp) {
                $scope.adjustadvicelist = resp.data.adjustadvicelist;
            });
            var url = 'api/BRSMaster/GetBRSActivityStatus';
            SocketService.get(url).then(function (resp) {
                $scope.BRSActivity_List = resp.data.BRSActivity_List;
            });
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
            var param = {
                banktransc_gid: banktransc_gid,
            }

            var url = 'api/OsdTrnBankAlert/GetUnReconUploadedDoc';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.uploaddocument = resp.data.MdlDocDetails;
                $scope.filename = resp.data.filename;
                $scope.filepath = resp.data.filepath;

            });

            var url = 'api/UnreconciliationManagement/GetAssignedHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;
            });

            var url = 'api/UnreconciliationManagement/GetTransferredHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlist = resp.data.transferlist;
            });
            var url = 'api/UnreconciliationManagement/GetDepartmentName';
            SocketService.get(url).then(function (resp) {
                $scope.department_name = resp.data.department_name;
                $scope.employeename = resp.data.employee_name;

            });
            var url = 'api/UnreconciliationManagement/GetSamfinCustomerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.assignedlist1 = resp.data.assignedlist;

            });

        }

        $scope.UploadDocCancel = function (id) {
            var params = {
                fileupload_gid: id
            }
            var url = 'api/OsdTrnBankAlert/DeleteUnReconUploadedDoc';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var param = {
                        banktransc_gid: banktransc_gid

                    }
                    var url = 'api/OsdTrnBankAlert/GetUnReconUploadedDoc';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.uploaddocument = resp.data.MdlDocDetails;
                        $scope.filename = resp.data.filename;
                        $scope.filepath = resp.data.filepath;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.download = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.Udownloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }
        $scope.uploadattachment = function () {
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                lockUI();

                var frm = new FormData();
                for (var i = 0; i < fi.files.length; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);

                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                }
                frm.append('project_flag', "documentformatonly");
                frm.append('banktransc_gid', banktransc_gid);
                /* frm.append('document_name', $scope.test_document);*/
                /*frm.append('fileupload', item.file);*/
                frm.append('document_name', fname);
                //        $scope.uploadfrm = frm;
                $scope.uploadfrm = frm;

                var url = 'api/OsdTrnBankAlert/UnReconDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');

                    unlockUI();
                    if (resp.data.status == true) {
                        var param = {
                            banktransc_gid: banktransc_gid

                        }

                        var url = 'api/OsdTrnBankAlert/GetUnReconUploadedDoc';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.uploaddocument = resp.data.MdlDocDetails;
                            $scope.lblfilename = resp.data.filename;
                            $scope.lblfilepath = resp.data.filepath;
                        });

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
                Notify.alert('Please select a file.', 'warning')
            }
        }
        $scope.limit_amountChange = function () {
            var input = document.getElementById('limit_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_creditamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txt_amount = "";
            }
            else {
                $scope.txt_amount = output;
                document.getElementById('words_limitamt').innerHTML = lswords_creditamount;
            }
        } 
        $scope.Submit = function () {

            var params =
            {
                banktransc_gid: banktransc_gid,
                cbounreconciliation_status: $scope.cborm_status,
                /*brs_status: $scope.brs_status,*/
                updation_remarks: $scope.txtremarks

            }
            var url = 'api/UnreconciliationManagement/PostUnconciliationStatusUpdation';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();

                if (resp.data.status == true)
                {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/BrsTrnUnreconAssignedSummary');

                }
                else
                {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

        }
        $scope.remarks = function ( transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.Back = function () {
            $location.url("app/BrsTrnUnreconAssignedSummary");
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

        $scope.Sendbacktobrs = function () {
            var modalInstance = $modal.open({
                templateUrl: '/sentbacktobrs.html',
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
                $scope.submit = function () {
                    var params = {
                        sendback_reason: $scope.txtsendbackreason,
                        banktransc_gid: banktransc_gid
                    }
                    var url = 'api/UnreconciliationManagement/PostRMSendback';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            var url = 'api/UnreconciliationManagement/GetBrsUnReconciliationSummary';
                           /* lockUI();*/
                            SocketService.get(url).then(function (resp) {
                                $scope.BrsUnreconciliation_list = resp.data.BrsUnreconciliation_list;
                            //    unlockUI();
                            });
                        }
                        else
                        {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');
                    activate();
                    $location.url('app/BrsTrnUnreconAssignedSummary');

                }
            }
        }
        $scope.Assigned_Submit = function () {

            var params =
            {
                banktransc_gid: banktransc_gid,
                assigned_to: $scope.cboemployee.employee_gid,
                assigned_toname: $scope.cboemployee.employee_name,
                assigned_remarks: $scope.txtremarks,
            }
            var url = 'api/UnreconciliationManagement/Post2Assign';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    
                    //else if (lspage == "Debit") {
                    //    $state.go('app.BrsTrnUnreconAssignedSummary');
                    //}

                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.BrsTrnUnreconAssignedSummary');

            });

        }
        $scope.addtransaction = function () {
            var lsbrsactivityname = '';
            var lsbrsactivitygid = '';
            var lsadjustadvicename = '';
            var lsadjustadvicegid = '';
            var lssamfincustomer_name = '';
            var lssamfincustomer_gid = '';
            var lsadjustadvicename = '';
            var lsadjustadvicegid = '';
            if ($scope.cbosamfin != undefined || $scope.cbosamfin != null) {
                lssamfincustomer_gid = $scope.cbosamfin.samfincustomer_gid;
                lssamfincustomer_name = $scope.cbosamfin.samfincustomer_name;
            }


            lsbrsactivityname = $('#brsactivity :selected').text();
            lsbrsactivitygid = $scope.cbobrsactivity;

            lsadjustadvicename = $('#adjustadvice :selected').text();
            lsadjustadvicegid = $scope.cboaction;

            if ((($scope.cbobrsactivity == '' || $scope.cbobrsactivity == null) || ($scope.cboaction == '' || $scope.cboaction == null) || ($scope.txt_amount == '' || $scope.txt_amount == null) || ($scope.transactionremarks == '' || $scope.transactionremarks == null))) {
                Notify.alert('Kindly Fill Transaction Details', 'warning')
            }
            else {
                var params =
                {
                    banktransc_gid: banktransc_gid,
                    //assignby_gid: $scope.cboemployee1.employee_gid,
                    assignby_name: $scope.employeename,
                    activity_name: lsbrsactivityname,
                    activity_gid: lsbrsactivitygid,
                    samfincustomer_gid: lssamfincustomer_gid,
                    samfincustomer_name: lssamfincustomer_name,
                    action_name: lsadjustadvicename,
                    department_name: $scope.department_name,
                    amount: $scope.txt_amount,
                    transaction_remarks: $scope.transactionremarks,
                }
                var url = 'api/UnreconciliationManagement/PostUnreconTransactionDetails';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        //if (lspage == "Credit") {
                        //    $state.go('app.brsTrnUnreconCreditSummaryManagement');
                        //}
                        //else if (lspage == "Debit") {
                        //    $state.go('app.brsTrnUnreconDebitSummaryManagement');
                        //}
                        unrecontransaction_list();
                        activate();

                    }
                    else {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.cbosamfin = '';
                    $scope.cbobrsactivity = '';
                    $scope.cboaction = '';
                    $scope.txt_amount = '';
                    $scope.transactionremarks = '';
                    unlockUI();
                    activate();
                });
            }
        }
        function unrecontransaction_list() {
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
        }
        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.deletetransaction = function (unrecontransactiondetails_gid, banktransc_gid) {
            var params = {
                unrecontransactiondetails_gid: unrecontransactiondetails_gid,
                banktransc_gid: banktransc_gid

            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionDelete';
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
                unrecontransaction_list();
                activate();

            });
        }
    }
})();