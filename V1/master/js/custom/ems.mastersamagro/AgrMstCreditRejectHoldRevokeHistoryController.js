(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCreditRejectHoldRevokeHistoryController', AgrMstCreditRejectHoldRevokeHistoryController);

    AgrMstCreditRejectHoldRevokeHistoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function 
        AgrMstCreditRejectHoldRevokeHistoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCreditRejectHoldRevokeHistoryController';

        //$scope.application_gid = $location.search().application_gid;
        //var application_gid = $scope.application_gid;
        //$scope.lspage = $location.search().lspage;
        //var lspage = $scope.lspage;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.application_gid = searchObject.application_gid;
        var application_gid = $scope.application_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.employee_gid = searchObject.employee_gid;
        var employee_gid = $scope.employee_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
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

            var url = 'api/AgrAdminApplication/GetCreditPendingHistoryLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditpendingapplhis_list = resp.data.creditpendingapplhis_list;
            });

            var url = 'api/AgrAdminApplication/GetCreditHistoryLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.credithistorylog_list = resp.data.credithistorylog_list;
            });

            //var url = 'api/AgrMstAdminApplication/GetBusinessHoldHistoryLog';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.holdapplhis_list = resp.data.holdapplhis_list;
            //});
        }

        $scope.Back = function () {
            if (lspage == 'CreditRejectRevokeAppl') {
                $state.go('app.AgrMstCreditRevokeSummary');
            }
            else if (lspage == 'CreditHoldRevokeAppl') {
                $state.go('app.AgrMstCreditHoldRevokeSummary');
            }
            else if (lspage == 'CreditRevokedAppl') {
                $state.go('app.AgrMstCreditRevokedApplSummary');
            }
            else if (lspage  == 'CreditManagerRejectRevokeAppl'){
                $state.go('app.AgrMstCreditManagerRejectRevokeSummary');
            }
            else {
                $state.go('app.AgrMstCreditRevokedApplSummary');
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
                var url = 'api/AgrAdminApplication/GetCreditHistoryRemarksView';
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
                var url = 'api/AgrAdminApplication/GetCreditHistoryRemarksView';
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
                var url = 'api/AgrAdminApplication/GetCreditPendingHistoryRemarksView';
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
    }
})();
