(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditRejectHoldRevokeHistoryController', MstCreditRejectHoldRevokeHistoryController);

    MstCreditRejectHoldRevokeHistoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function 
        MstCreditRejectHoldRevokeHistoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditRejectHoldRevokeHistoryController';

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtvertical_tag = resp.data.verticaltaggs_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txt_strategicbusiness_unit = resp.data.businessunit_name;
                $scope.txtprimayvalue_chain = resp.data.primaryvaluechain_name;
                $scope.txtsecondaryvalue_chain = resp.data.secondaryvaluechain_name;
                $scope.txtvernacular_language = resp.data.vernacular_language;
                $scope.txtApplfrom_SA = resp.data.sa_status;
                $scope.txtSAM_associateID = resp.data.sa_id;
                $scope.txtSAM_associatename = resp.data.sa_name;
                $scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtlandline_number = resp.data.landline_no;
                $scope.txtsocial_capital = resp.data.social_capital;
                $scope.txttrade_capital = resp.data.trade_capital;
                $scope.borrower_flag = resp.data.borrower_flag;
                $scope.borrower_type = resp.data.borrower_type;
                $scope.momapproval_flag = resp.data.momapproval_flag;
                $scope.txtcredit_group = resp.data.creditgroup_name;
            });

            var url = 'api/MstAdminApplication/GetCreditPendingHistoryLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditpendingapplhis_list = resp.data.creditpendingapplhis_list;
            });

            var url = 'api/MstAdminApplication/GetCreditHistoryLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.credithistorylog_list = resp.data.credithistorylog_list;
            });

            var url = 'api/MstAdminApplication/GetCreditManagerHistoryLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditmanagerhistorylog_list = resp.data.creditmanagerhistorylog_list;
            });

            //var url = 'api/MstAdminApplication/GetBusinessHoldHistoryLog';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.holdapplhis_list = resp.data.holdapplhis_list;
            //});
        }

        $scope.Back = function () {
            if (lspage == 'CreditRejectRevokeAppl') {
                $state.go('app.MstCreditRevokeSummary');
            }
            else if (lspage == 'CreditHoldRevokeAppl') {
                $state.go('app.MstCreditHoldRevokeSummary');
            }
            else if (lspage == 'CreditRevokedAppl') {
                $state.go('app.MstCreditRevokedApplSummary');
            }
            else {
                $state.go('app.MstCreditRevokedApplSummary');
            }
        }

        $scope.BusinessRemarks_View = function (creditrevokelog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/HistoryBusinessRemarksView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditrevokelog_gid: creditrevokelog_gid
                }
                var url = 'api/MstAdminApplication/GetCreditHistoryRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblcredit_remarks = resp.data.credit_remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.RevokedRemarks_View = function (creditrevokelog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/HistoryRevokedRemarksView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditrevokelog_gid: creditrevokelog_gid
                }
                var url = 'api/MstAdminApplication/GetCreditHistoryRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblrevoked_remarks = resp.data.reason;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.RejectHoldRemarks_View = function (appcreditapproval_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/RejectHoldRemarksView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    appcreditapproval_gid: appcreditapproval_gid
                }
                var url = 'api/MstAdminApplication/GetCreditPendingHistoryRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblcreditrejecthold_remarks = resp.data.creditrejecthold_remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.RejectedRemarks_View = function (creditmanagerrejectrevokereasonlog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/CreditManagerRejectRemarksView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditmanagerrejectrevokereasonlog_gid: creditmanagerrejectrevokereasonlog_gid
                }
                var url = 'api/MstAdminApplication/GetCreditManagerRejectedRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblcreditmanagerreject_remarks = resp.data.approval_remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.CreditRevokedRemarks_View = function (creditmanagerrevokelog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/CreditManagerRevokeRemarksView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditmanagerrevokelog_gid: creditmanagerrevokelog_gid
                }
                var url = 'api/MstAdminApplication/GetCreditManagerRejectedRevokeRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblcreditmanagerrejectrevoke_remarks = resp.data.reason;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
    }
})();
