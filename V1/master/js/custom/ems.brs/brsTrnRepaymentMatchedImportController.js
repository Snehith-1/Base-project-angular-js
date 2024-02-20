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
