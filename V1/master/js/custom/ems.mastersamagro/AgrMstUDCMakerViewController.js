(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstUDCMakerViewController', AgrMstUDCMakerViewController);

    AgrMstUDCMakerViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstUDCMakerViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstUDCMakerViewController';

        $scope.UdcManagement2cheque_gid = $location.search().lsudcmanagement2cheque_gid;
        $scope.UdcManagement_gid = $location.search().lsudcmanagement_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        function activate() {

            var params = {
                UdcManagement2cheque_gid: $scope.UdcManagement2cheque_gid
            }
            var url = 'api/AgrUdcManagement/ChequeDetailsEdit';
            SocketService.getparams(url, params).then(function (resp) {
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

            var url = 'api/AgrUdcManagement/ChequeDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.chequedocument_list = resp.data.chequedocument_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
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
                $location.url('app/AgrMstChequeMakerFollowDtls?lsUdcManagement_gid=' + $scope.UdcManagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'checkerfollowup') {
                $location.url('app/AgrMstChequeMakerFollowDtls?lsUdcManagement_gid=' + $scope.UdcManagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'CompletedApprocal') {
                $location.url('app/AgrMstChequeMakerFollowDtls?lsUdcManagement_gid=' + $scope.UdcManagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'makerpending') {
                $location.url('app/AgrMstUDCMakerSummary?lsUdcManagement_gid=' + $scope.UdcManagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'checkerpending') {
                $location.url('app/AgrMstChequeCheckerDtls?lsUdcManagement_gid=' + $scope.UdcManagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == 'approvalpending') {
                $location.url('app/AgrMstChequeApprovalDtls?lsUdcManagement_gid=' + $scope.UdcManagement_gid + '&application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

            }
        }

        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

    }
})();