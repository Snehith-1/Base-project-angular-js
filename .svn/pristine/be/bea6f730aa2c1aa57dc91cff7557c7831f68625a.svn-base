(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstUDCMakerViewController', MstUDCMakerViewController);

    MstUDCMakerViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstUDCMakerViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstUDCMakerViewController';

        $scope.udcmanagement2cheque_gid = $location.search().lsudcmanagement2cheque_gid;
        $scope.udcmanagement_gid = $location.search().lsudcmanagement_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        function activate(){
    
            var params={
                udcmanagement2cheque_gid : $scope.udcmanagement2cheque_gid
            }
            var url = 'api/UdcManagement/ChequeDetailsEdit';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtstakeholder_name = resp.data.stakeholder_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
                $scope.txtdesignation = resp.data.designation;
                $scope.txtaccountholder_name = resp.data.accountholder_name;
                $scope.txtaccount_number = resp.data.account_number;
                $scope.txtbank_name = resp.data.bank_name;
                $scope.txtcheque_no = resp.data.cheque_no;
                $scope.txtifsc_code = resp.data.ifsc_code;
                $scope.txtmicr = resp.data.micr;
                $scope.txtbranch_address = resp.data.branch_address;
                $scope.txtbranch_name = resp.data.branch_name;
                $scope.txtcity = resp.data.city;
                $scope.txtdistrict = resp.data.district;
                $scope.txtstate = resp.data.state;
                $scope.cbomergedbankingentity_name = resp.data.mergedbankingentity_name;
                $scope.txtcheque_type = resp.data.cheque_type;
                $scope.txtdate_chequetype = resp.data.date_chequetype;
                $scope.txtcts_enabled = resp.data.cts_enabled;
                $scope.txtdate_chequepresentation = resp.data.date_chequepresentation;
                $scope.txtstatus_chequepresentation = resp.data.status_chequepresentation;
                $scope.txtdate_chequeclearance = resp.data.date_chequeclearance;
                $scope.txtstatus_chequeclearance = resp.data.status_chequeclearance;
                $scope.txtspecial_condition = resp.data.special_condition;
                $scope.txtgeneral_remarks = resp.data.general_remarks;
            });

            var url = 'api/UdcManagement/ChequeDocumentList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.chequedocument_list = resp.data.chequedocument_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCadFlow/GetApplicationBasicView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
                console.log(resp.data.application_no);
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                console.log(resp.data.customer_name);
            });
        }

        $scope.Back = function () {
            var application_gid = $scope.application_gid;
            var lspage = $scope.lspage;
            if (lspage == 'makerfollowup') {
                $location.url('app/MstChequeMakerFollowDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'checkerfollowup') {
                $location.url('app/MstChequeMakerFollowDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'CompletedApprocal') {
                $location.url('app/MstChequeMakerFollowDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'makerpending') {
                $location.url('app/MstUDCMakerSummary?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'checkerpending') {
                $location.url('app/MstChequeCheckerDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'approvalpending') {
                $location.url('app/MstChequeApprovalDtls?lsudcmanagement_gid=' + $scope.udcmanagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

    }
})();