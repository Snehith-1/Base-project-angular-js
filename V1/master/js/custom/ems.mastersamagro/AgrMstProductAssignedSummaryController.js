(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstProductAssignedSummaryController', AgrMstProductAssignedSummaryController);

    AgrMstProductAssignedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$timeout'];

    function AgrMstProductAssignedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstProductAssignedSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrTrnProductApproval/GetAppProductAssignedAssignmentSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.productdescassiged_list = resp.data.applicationadd_list;
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

        $scope.VisitReport_add = function (val) {
            $location.url('app/AgrProductDescVisitReportAdd?application_gid=' + val + '&lstab=ProductDescAssigned');
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrApplicationCreationView?application_gid=' + val + '&lstab=ProductDescAssigned');
        }

        $scope.assigned_applications = function (val) {
            $location.url('app/AgrMstProductAssignedSummary');
        }

        $scope.pending_applications = function (val) {
            $location.url('app/AgrMstProductPendingAssignmentSummary');
        }

        $scope.reassignapplication = function (productdesk_gid, application_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/reassigncreditapproval.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();

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
                    if (resp.data.status == true) {
                        $scope.lblproductdesk_name = resp.data.productdesk_name;
                        $scope.ProductMemberGrouplist = resp.data.ProductMemberGroup;
                        $scope.ProductManagerGrouplist = resp.data.ProductManagerGroup;
                        unlockUI();
                    }
                });
                lockUI();
                var params1 = {
                    application_gid: application_gid
                }
                var url = 'api/AgrTrnProductApproval/GetAppProductAprovalinfo';
                SocketService.getparams(url, params1).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        $scope.cboproductdescmanager = resp.data.product_managergid;
                        $scope.approvedproductmanager = resp.data.productmanager_approvalflag === 'Y' ? true : false;

                        $scope.cboproductdescmember = resp.data.product_membergid;
                        $scope.approvedproductmember = resp.data.productmanager_approvalflag === 'Y' ? true : false;
                        $scope.txtremarks = resp.data.assign_remarks;
                    }
                    else {
                        unlockUI();
                    }
                });

                var url = "api/AgrTrnProductApproval/GetProductReassignedLog"
                SocketService.getparams(url, params1).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.reassignedloglist = resp.data.Productreassignedloglist;
                        angular.forEach($scope.reassignedloglist, function (value, key) {
                            if (value.product_membergid === "" || value.product_membergid === null) {
                                value.showproductdescmember_name = false;
                            }
                            else {
                                value.showproductdescmember_name = true;
                            }
                            if (value.product_managergid === "" || value.product_managergid === null) {
                                value.showproductdescmanager_name = false;
                            }
                            else {
                                value.showproductdescmanager_name = true;
                            }
                        });
                        if ($scope.reassignedloglist && $scope.reassignedloglist.length != 0)
                            $scope.showreassignedlist = true;
                        else
                            $scope.showreassignedlist = false;
                        unlockUI();
                    }
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.reassign_application = function () {
                    var lsproductdescmember_gid = '';
                    var lsproductdescmember_name = '';
                    var lsproductdescmanager_gid = '';
                    var lsproductdescmanager_name = '';

                    if ($scope.cboproductdescmember != undefined || $scope.cboproductdescmember != null) {
                        lsproductdescmember_gid = $scope.cboproductdescmember;
                        lsproductdescmember_name = $scope.ProductMemberGrouplist.find(function (a) { return a.employee_gid === $scope.cboproductdescmember.employee_name })
                    }
                    if ($scope.cboproductdescmanager != undefined || $scope.cboproductdescmanager != null) {
                        lsproductdescmanager_gid = $scope.cboproductdescmanager;
                        lsproductdescmanager_name = $scope.ProductManagerGrouplist.find(function (a) { return a.employee_gid === $scope.cboproductdescmanager.employee_name })
                    }

                    var params = {
                        application_gid: application_gid,
                        product_membergid: lsproductdescmember_gid,
                        product_membername: lsproductdescmember_name,
                        product_managergid: lsproductdescmanager_gid,
                        product_managername: lsproductdescmanager_name,
                        assign_remarks: $scope.txtremarks
                    }
                    var url = 'api/AgrTrnProductApproval/GetProductReassignUpdate';
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
                                status: 'error',
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
