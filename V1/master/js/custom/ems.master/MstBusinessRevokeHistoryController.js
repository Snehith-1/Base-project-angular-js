(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBusinessRevokeHistoryController', MstBusinessRevokeHistoryController);

    MstBusinessRevokeHistoryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function MstBusinessRevokeHistoryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBusinessRevokeHistoryController';
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

            var url = 'api/MstAdminApplication/GetBusinessPendingHistoryLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.pendingapplhis_list = resp.data.pendingapplhis_list;
            });

            var url = 'api/MstAdminApplication/GetBusinessHistoryLog';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.businesshistory_list = resp.data.businesshistory_list;
            });

            //var url = 'api/MstAdminApplication/GetBusinessHoldHistoryLog';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.holdapplhis_list = resp.data.holdapplhis_list;
            //});
        }

        $scope.Back = function () {
            if (lspage == 'RejectRevokeApplication') {
                $state.go('app.MstBusinessRevokeSummary');
            }
            else if (lspage == 'HoldRevokeApplication') {
                $state.go('app.MstBusinessHoldRevokeSummary');
            }
            else if (lspage == 'BusinessRevokedApplication') {
                $state.go('app.MstBusinessRevokedApplSummary');
            }
            else {

            }
        }

        $scope.BusinessRemarks_View = function (businessrejectrevokelog_gid) {
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
                    businessrejectrevokelog_gid: businessrejectrevokelog_gid
                }
                var url = 'api/MstAdminApplication/GetHistoryRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblbusiness_remarks = resp.data.business_remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.RevokedRemarks_View = function (businessrejectrevokelog_gid) {
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
                    businessrejectrevokelog_gid: businessrejectrevokelog_gid
                }
                var url = 'api/MstAdminApplication/GetHistoryRemarksView';
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

        $scope.RejectHoldRemarks_View = function (applicationapproval_gid) {
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
                    applicationapproval_gid: applicationapproval_gid
                }
                var url = 'api/MstAdminApplication/GetPendingHistoryRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblrejecthold_remarks = resp.data.rejecthold_remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
    }
})();
