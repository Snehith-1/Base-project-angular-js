(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstProductPendingAssignmentSummaryController', AgrMstProductPendingAssignmentSummaryController);

    AgrMstProductPendingAssignmentSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$timeout'];

    function AgrMstProductPendingAssignmentSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstProductPendingAssignmentSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrTrnProductApproval/GetAppProductPendingAssignmentSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.productdescassignment_list = resp.data.applicationadd_list;
                }
                else {
                    unlockUI();
                } 
            });
            lockUI();
            var url = 'api/AgrTrnProductApproval/AssignProductApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.pending_count = resp.data.pending_count;
                $scope.assigned_count = resp.data.assigned_count;
                $scope.lstotalcount = resp.data.lstotalcount;
                $scope.submittedtocc_count = resp.data.submittedtocc_count;
                $scope.ccapproved_count = resp.data.ccapproved_count;
            });
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrApplicationCreationView?application_gid=' + val + '&lstab=ProductDescPendingAssignment');
        }

        $scope.assigned_applications = function (val) {
            $location.url('app/AgrMstProductAssignedSummary');
        }

        $scope.pending_applications = function (val) {
            $location.url('app/AgrMstProductPendingAssignmentSummary');
        }
       
        $scope.assign = function (productdesk_gid, application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/assign.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

               var param = {
                    application_gid: application_gid
                };

                var url = 'api/AgrTrnApplicationApproval/Getapplicationdetails';
                SocketService.getparams(url, param).then(function (resp) {            
                    $scope.shortclosing_reason = resp.data.shortclosing_reason;
                    $scope.expired_flag = resp.data.expired_flag;
                });
            $scope.amendmentshow = false;
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, param).then(function (resp) {             
                $scope.lblamendment_remarks = resp.data.amendment_remarks;
                unlockUI();

                if ($scope.lblamendment_remarks == null || $scope.lblamendment_remarks == '' || $scope.lblamendment_remarks == undefined) {
                    $scope.amendmentshow = false;
                }
                else {
                    $scope.amendmentshow = true;
                }
            });
                var params = {
                    productdesk_gid: productdesk_gid
                }
                var url = 'api/AgrTrnProductApproval/GetProductApprovalManagerMember';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblproductdesk_name = resp.data.productdesk_name;
                    $scope.ProductMemberGrouplist = resp.data.ProductMemberGroup;
                    $scope.ProductManagerGrouplist = resp.data.ProductManagerGroup;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.assign_application = function () {
                    var lsproductdescmember_gid = '';
                    var lsproductdescmember_name = '';
                    var lsproductdescmanager_gid = '';
                    var lsproductdescmanager_name = '';
                     
                    if ($scope.cboproductdescmember != undefined || $scope.cboproductdescmember != null) {
                        lsproductdescmember_gid = $scope.cboproductdescmember.employee_gid;
                        lsproductdescmember_name = $scope.cboproductdescmember.employee_name;
                    }
                    if ($scope.cboproductdescmanager != undefined || $scope.cboproductdescmanager != null) {
                        lsproductdescmanager_gid = $scope.cboproductdescmanager.employee_gid;
                        lsproductdescmanager_name = $scope.cboproductdescmanager.employee_name;
                    }

                    var params = {
                        application_gid: application_gid,
                        productdesk_gid: productdesk_gid,
                        productdesk_name: $scope.lblproductdesk_name,
                        product_membergid: lsproductdescmember_gid,
                        product_membername: lsproductdescmember_name,
                        product_managergid: lsproductdescmanager_gid,
                        product_managername: lsproductdescmanager_name,
                        assign_remarks: $scope.txtremarks
                    }
                    var url = 'api/AgrTrnProductApproval/PostProductAssign';
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
            }
        }
       
    }
})();
