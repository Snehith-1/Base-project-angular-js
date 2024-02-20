(function () {
    'use strict';

    angular
        .module('angle')
        .controller('verticalcontroller', verticalcontroller);

    verticalcontroller.$inject = ['$rootScope', '$scope','$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function verticalcontroller($rootScope, $scope,$modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'verticalcontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            $scope.totalDisplayed=100;
            var url = 'api/vertical/vertical';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.vertical = resp.data.vertical_list;
                unlockUI();               
            });
     
        }

        $scope.editRule = function (vertical_gid) {
            $location.url('app/MstVerticalApplicantTypeRule?lsvertical_gid=' + vertical_gid);
        }
       
       $scope.delete = function (vertical_gid) {
                var params = {
                    vertical_gid: vertical_gid
                }
                var url = 'api/vertical/verticalDelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                            activate();
                        }

                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();
                }
            });
            };
        
        $scope.popupvertical = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {             
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var url = 'api/entity/Entity';
                SocketService.get(url).then(function (resp) {
                    $scope.entity_list = resp.data.entity_list;
                });
                $scope.verticalSubmit = function () {
                    var params = {
                        bureau_code: $scope.txtbureau_code,
                        vertical_name: $scope.txtvertical_name,
                        vertical_code:$scope.txtvertical_code,
                        lms_code: $scope.txtlms_code,
                        entity_gid: $scope.cboentity_type.entity_gid,
                        entity_name: $scope.cboentity_type.entity_name,
                        vertical_refno: $scope.txtvertical_refno,
                    }
                  
                    var url = 'api/vertical/createVertical';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')
                             activate();
                        
                          }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                            
                        }
                        activate();
                    });
                    $modalInstance.close('closed');
                  
                }
                
            }
        }

       

        $scope.edit = function (vertical_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                var params = {
                    vertical_gid: vertical_gid
                }
                var url = 'api/vertical/Getverticalupdate';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.bureau_codeedit = resp.data.bureau_codeedit;
                    $scope.lms_codeedit = resp.data.lms_codeedit;
                    $scope.vertical_codeedit = resp.data.verticalCodeedit;
                    $scope.verticalNameedit = resp.data.verticalNameedit;
                    $scope.vertical_gid = resp.data.vertical_gid;
                    $scope.cboentity_type = resp.data.entity_gid;
                    $scope.txtvertical_refno = resp.data.vertical_refno;
                     });
                var url = 'api/entity/Entity';
                SocketService.get(url).then(function (resp) {
                    $scope.entity_list = resp.data.entity_list;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.verticalUpdate = function () {
                    var entity_name = $('#entity_name :selected').text();
                    var params = {
                        bureau_codeedit: $scope.bureau_codeedit,
                        lms_codeedit: $scope.lms_codeedit,
                        verticalCodeedit:$scope.vertical_codeedit,
                        verticalNameedit: $scope.verticalNameedit,
                        vertical_gid: $scope.vertical_gid,
                        entity_gid: $scope.cboentity_type,
                        entity_name: entity_name,
                        vertical_refno: $scope.txtvertical_refno,
                    }
                    var url = 'api/vertical/verticalUpdate';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')

                        }
                        else {
                            Notify.alert(resp.data.message, 'success')
                            activate();

                        }
                    });
                }
            }

        }

        $scope.Status_update = function (vertical_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCompanyDocument.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    vertical_gid: vertical_gid
                }
            var url = 'api/vertical/Getverticalupdate';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.bureau_codeedit = resp.data.bureau_codeedit;
                $scope.lms_codeedit = resp.data.lms_codeedit;
                $scope.vertical_codeedit = resp.data.verticalCodeedit;
                $scope.lblvertical_name = resp.data.verticalNameedit;
                $scope.rbo_status = resp.data.status_log;
                $scope.txtvertical_refno = resp.data.vertical_refno;
            });
            var params = {
                vertical_gid: vertical_gid
            }
             var url = 'api/vertical/GetActiveLog';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        vertical_name: $scope.lblvertical_name,
                        remarks: $scope.txtremarks,
                        status_log: $scope.rbo_status,
                        vertical_gid: vertical_gid
                    }
                    var url = 'api/vertical/VerticalStatusUpdate';
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

        $scope.editTemplate = function (vertical_gid) {
            $location.url('app/MstTemplateSummary?lsvertical_gid=' + vertical_gid);
        }

        $scope.editSanctionTemplate = function (vertical_gid) {
            $location.url('app/MstSanctionTemplateSummary?lsvertical_gid=' + vertical_gid);
        }

        $scope.document_deferral = function (vertical_gid) {
            $location.url('app/MstDisbursementDeferralDocument?vertical_gid=' + vertical_gid);
        }
        $scope.disbursement_document = function (vertical_gid) {
            $location.url('app/MstVerticalDisbursementDocument?vertical_gid=' + vertical_gid);
        }
        $scope.disbursement_bankaccount = function (vertical_gid) {
            $location.url('app/MstDisbursementBankAccount?vertical_gid=' + vertical_gid);
        }
        $scope.disbursement_odbelow30 = function (vertical_gid) {
            $location.url('app/MstDisbursementODBelow30?vertical_gid=' + vertical_gid);
        }
        $scope.disbursement_odbelow90 = function (vertical_gid) {
            $location.url('app/MstDisbursementODBelow90?vertical_gid=' + vertical_gid);
        }
        $scope.penal_waiver = function (vertical_gid) {
            $location.url('app/MstPenalWaiver?vertical_gid=' + vertical_gid);
        }

        $scope.editContrateTemplate = function (vertical_gid) {
            $location.url('app/AgrMstContrateTemplateSummary?lsvertical_gid=' + vertical_gid);
        }
       }
})();