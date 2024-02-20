(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDisbursementDeferralDocumentController', MstDisbursementDeferralDocumentController);

    MstDisbursementDeferralDocumentController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstDisbursementDeferralDocumentController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDisbursementDeferralDocumentController';
        $scope.vertical_gid = $location.search().vertical_gid;
        var vertical_gid = $scope.vertical_gid;

        activate();
        function activate() {

            var params = {
                vertical_gid: vertical_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbDocumentDeferralSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.disbdocumentdeferral_list = resp.data.disbdocumentdeferral_list;
                unlockUI();
            });

            var url = 'api/MstCreditOpsApplication/GetDisbDoDefApprovalConfigSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.disbdocdefapprovalconfig_list = resp.data.disbdocdefapprovalconfig_list;
                unlockUI();
            });

            $scope.calenderopen16 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                $scope.open16 = true;
            };
            $scope.formats = ['dd-MM-yyyy'];
            $scope.format = $scope.formats[0];
            $scope.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

        }   

        $scope.documentdeferral_submit = function () {
            var params = {
                vertical_gid: vertical_gid,
                wef_date: $scope.txtwef_date,
                customer_type: $scope.cbocustomer_type
            }
            var url = 'api/MstCreditOpsApplication/PostDisbDocumentDeferral';
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
                    $scope.txtwef_date = '';
                    $scope.cbocustomer_type = '';
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.txtwef_date = '';
                    $scope.cbocustomer_type = '';
                }
                $scope.txtwef_date = '';
                $scope.cbocustomer_type = '';
            });
           
        }

        $scope.approval_config = function (disbursementdocdeferral_gid) {
            var disbursementdocdeferral_gid = disbursementdocdeferral_gid;
            var modalInstance = $modal.open({
                templateUrl: '/ApprovalConfiglAdd.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {               
                var params = {
                    disbursementdocdeferral_gid: disbursementdocdeferral_gid
                }
                var url = 'api/MstCreditOpsApplication/GetDeviationApprovalGroupName';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.deviationgroup_list = resp.data.deviationgroup_list;
                    unlockUI();
                });                

                $scope.group_change = function (cbodeviationgroup_name) {
                    var lsgroup_gid = '';
                    lsgroup_gid = $scope.cbodeviationgroup_name.deviationapprovalgroup_gid;
                    var params = {
                        deviationapprovalgroup_gid: lsgroup_gid
                    }
                    var url = 'api/MstCreditOpsApplication/GetDeviationApprovalSubGroupName';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.deviationsubgroup_list = resp.data.deviationsubgroup_list;
                        unlockUI();
                    });
                }

                $scope.subgroup_change = function (cbosubgroup_name) {
                    var lssubgroup_gid = '';
                    lssubgroup_gid = $scope.cbosubgroup_name.subgroup_gid;
                    var params = {
                        deviationapprovalgroup_gid: lssubgroup_gid
                    }
                    var url = 'api/MstCreditOpsApplication/GetDeviationApprovalManagerName';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.deviationmanager_list = resp.data.deviationmanager_list;
                    });

                    var url = 'api/MstCreditOpsApplication/GetDeviationApprovalMemberName';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.deviationmember_list = resp.data.deviationmember_list;
                    });                   
                }

                $scope.documentdeferral_submit = function () {
                    var lsdeviationapprovalgroup_gid = '';
                    var lsdeviationapprovalgroup_name = '';
                    var lssubgroup_gid = '';
                    var lssubgroup_name = '';
                    var lsmanager_gid = '';
                    var lsmanager_name = '';
                    var lsmember_gid = '';
                    var lsmember_name = '';

                    if ($scope.cbodeviationgroup_name != undefined || $scope.cbodeviationgroup_name != null) {
                        lsdeviationapprovalgroup_gid = $scope.cbodeviationgroup_name.deviationapprovalgroup_gid;
                        lsdeviationapprovalgroup_name = $scope.cbodeviationgroup_name.deviationapprovalgroup_name;
                    }
                    if ($scope.cbosubgroup_name != undefined || $scope.cbosubgroup_name != null) {
                        lssubgroup_gid = $scope.cbosubgroup_name.subgroup_gid;
                        lssubgroup_name = $scope.cbosubgroup_name.subgroup_name;
                    }
                    if ($scope.cbomanager_name != undefined || $scope.cbomanager_name != null) {
                        lsmanager_gid = $scope.cbomanager_name.employee_gid;
                        lsmanager_name = $scope.cbomanager_name.employee_name;
                    }
                    if ($scope.cbomember_name != undefined || $scope.cbomember_name != null) {
                        lsmember_gid = $scope.cbomember_name.employee_gid;
                        lsmember_name = $scope.cbomember_name.employee_name;
                    }
                    var params = {
                        disbursementdocdeferral_gid: disbursementdocdeferral_gid,
                        vertical_gid: vertical_gid,
                        group_gid: lsdeviationapprovalgroup_gid,
                        group_name: lsdeviationapprovalgroup_name,
                        subgroup_gid: lssubgroup_gid,
                        subgroup_name: lssubgroup_name,
                        manager_gid: lsmanager_gid,
                        manager_name: lsmanager_name,
                        member_gid: lsmember_gid,
                        member_name: lsmember_name
                    }
                    var url = 'api/MstCreditOpsApplication/PostDisbDocDefApprovalConfig';
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Back = function () {
            $location.url('app/vertical');
        }

        $scope.Status_update = function (disbursementdocdeferral_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusdeviationapprovalgroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    disbursementdocdeferral_gid: disbursementdocdeferral_gid
                }
                var url = 'api/MstCreditOpsApplication/GetDisbDeferralDocView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.disbursementdocdeferral_gid = resp.data.disbursementdocdeferral_gid
                    $scope.lblwef_date = resp.data.wef_date;
                    $scope.lblcustomer_type = resp.data.customer_type;
                    $scope.rbodocdeferral_status = resp.data.disbursementdocdeferral_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        disbursementdocdeferral_gid: disbursementdocdeferral_gid,
                        remarks: $scope.txtremarks,
                        disbursementdocdeferral_status: $scope.rbodocdeferral_status,
                        wef_date: $scope.lblwef_date,
                        customer_type: $scope.lblcustomer_type,
                        vertical_gid: vertical_gid
                    }
                    var url = 'api/MstCreditOpsApplication/PostDisbDeferralDocInactive';
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
                    disbursementdocdeferral_gid: disbursementdocdeferral_gid
                }

                var url = 'api/MstCreditOpsApplication/GetDisbDeferralDocInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.disbdefdoclog_list = resp.data.disbdefdoclog_list;
                    unlockUI();
                });

            }
        }

        $scope.approvalconfig_edit = function (disbdocdeferralapprovalconfig_gid) {
            var disbdocdeferralapprovalconfig_gid = disbdocdeferralapprovalconfig_gid;
            var modalInstance = $modal.open({
                templateUrl: '/ApprovalConfiglEdit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    disbdocdeferralapprovalconfig_gid: disbdocdeferralapprovalconfig_gid
                }
                var url = 'api/MstCreditOpsApplication/GetDisbDeferralDocEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.disbursementdocdeferral_gid = resp.data.disbursementdocdeferral_gid;
                    var params = {
                        disbursementdocdeferral_gid: $scope.disbursementdocdeferral_gid
                    }
                    var url = 'api/MstCreditOpsApplication/GetDeviationApprovalGroupName';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.deviationgroup_list = resp.data.deviationgroup_list;
                        unlockUI();
                    });
                    $scope.vertical_gid = resp.data.vertical_gid;
                    $scope.cboeditdeviationgroup_name = resp.data.group_gid;  
                    $scope.cboeditsubgroup_name = resp.data.subgroup_gid;
                    var params = {
                        deviationapprovalgroup_gid: $scope.cboeditsubgroup_name
                    }
                    var url = 'api/MstCreditOpsApplication/GetDeviationApprovalSubGroupName';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.deviationsubgroup_list = resp.data.deviationsubgroup_list;
                        unlockUI();
                    });
                    var url = 'api/MstCreditOpsApplication/GetDeviationApprovalManagerName';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.deviationmanager_list = resp.data.deviationmanager_list;
                    });    
                    var url = 'api/MstCreditOpsApplication/GetDeviationApprovalMemberName';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.deviationmember_list = resp.data.deviationmember_list;
                    });
                    $scope.cboeditmanager_name = resp.data.manager_gid;
                    $scope.cboeditmember_name = resp.data.member_gid;                    
                });

               
                $scope.group_change = function (cboeditdeviationgroup_name) {
                    var lsgroup_gid = '';
                    lsgroup_gid = cboeditdeviationgroup_name;
                    var params = {
                        deviationapprovalgroup_gid: lsgroup_gid
                    }
                    var url = 'api/MstCreditOpsApplication/GetDeviationApprovalSubGroupName';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.deviationsubgroup_list = resp.data.deviationsubgroup_list;
                        unlockUI();
                    });
                }

                $scope.subgroup_change = function (cboeditsubgroup_name) {
                    var lssubgroup_gid = '';
                    lssubgroup_gid = cboeditsubgroup_name;
                    var params = {
                        deviationapprovalgroup_gid: lssubgroup_gid
                    }
                    var url = 'api/MstCreditOpsApplication/GetDeviationApprovalManagerName';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.deviationmanager_list = resp.data.deviationmanager_list;
                    });

                    var url = 'api/MstCreditOpsApplication/GetDeviationApprovalMemberName';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.deviationmember_list = resp.data.deviationmember_list;
                    });
                }

                $scope.documentdeferral_update = function () {
                    var deviationapprovalgroup_name = $('#DeviationApprovalGroupName :selected').text();
                    var subgroup_name = $('#SubGroupName :selected').text();
                    var manager_name = $('#ManagerName :selected').text();
                    var member_name = $('#MemberName :selected').text();
                   
                    var params = {
                        disbdocdeferralapprovalconfig_gid: disbdocdeferralapprovalconfig_gid,
                        vertical_gid: vertical_gid,
                        group_gid: $scope.cboeditdeviationgroup_name,
                        group_name: deviationapprovalgroup_name,
                        subgroup_gid: $scope.cboeditsubgroup_name, 
                        subgroup_name: subgroup_name,
                        manager_gid: $scope.cboeditmanager_name,
                        manager_name: manager_name,
                        member_gid: $scope.cboeditmember_name,
                        member_name: member_name
                    }
                    var url = 'api/MstCreditOpsApplication/PosDisbDeferralDocUpdate';
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

    }
})();