(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditFSAPandLViewController', AgrTrnCreditFSAPandLViewController);

    AgrTrnCreditFSAPandLViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnCreditFSAPandLViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'AgrTrnCreditFSAPandLViewController';
        var vm = this;
        vm.title = 'AgrTrnCreditFSAPandLViewController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.template_name = $location.search().template_name;
        var template_name = $scope.template_name;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        var lspagename = $location.search().lspagename;
        var lspagetype = $location.search().lspagetype;
        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            lockUI();
            var params = {
                credit_gid: institution_gid,
                template_name: template_name,
                application_gid: application_gid
            }           
            var url = "api/AgrTrnAppCreditUnderWriting/GetProfitLoss";
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.profitloss_list = resp.data.profitloss_list;
            });
            var params = {
                credit_gid: institution_gid,
                application_gid: application_gid,
                template_type : template_name
            }           
            var url = "api/AgrTrnAppCreditUnderWriting/GetBalanceSheetTemplate1List";
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.creditbalancesheettemplate1_list = resp.data.creditbalancesheettemplate1_list;
            });

            var params = {
                credit_gid: institution_gid,
                application_gid: application_gid,
                template_type : template_name
            }  
            var url = "api/AgrTrnAppCreditUnderWriting/GetBalanceSheetTemplate2List";
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.creditbalancesheettemplate2_list = resp.data.creditbalancesheettemplate2_list;
            });

            var params = {
                credit_gid: $location.search().institution_gid,
                template_name: $location.search().template_name,
                application_gid: $location.search().application_gid,
            }
            var url = "api/AgrTrnAppCreditUnderWriting/GetSummaryTemplate1View";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.summarytemplate1_list = resp.data.summarytemplate1_list;
            });
            var params = {
                credit_gid: $location.search().institution_gid,
                template_name: $location.search().template_name,
                application_gid: $location.search().application_gid,
            }
            var url = "api/AgrTrnAppCreditUnderWriting/GetSummaryTemplate2View";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.summarytemplate2_list = resp.data.summarytemplate2_list;
            });
            var params = {
                credit_gid: institution_gid,
                template_name: template_name,
                application_gid: application_gid
            }           
            var url = "api/AgrTrnAppCreditUnderWriting/GetProfitLossTemp2List";
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.profitlosstemp2_list = resp.data.profitlosstemp2_list;
            });
            

            if (template_name == 'BalanceSheetTemplate1'){
                $scope.balancetemplate1 = true;
            }
            else if (template_name == 'PLTemplate1') {
                $scope.PandLtemplate1 = true;
            }
            else if (template_name == 'SummaryTemplate1') {
                $scope.Summarytemplate1 = true;
            }
            else if (template_name == 'BalanceSheetTemplate2') {
                $scope.balancetemplate2 = true;
            }
            else if (template_name == 'SummaryTemplate2') {
                $scope.Summarytemplate2 = true;
            }
            else if (template_name == 'PLTemplate2') {
                $scope.PandLtemplate2 = true;
            }
            else {
                
            }

        }
        
        
        $scope.Back = function () {
            if(lspagename=='AgrTrnCreditFsaDetailAdd')
                $location.url('app/AgrTrnCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
            
            else if (lspagename=='AgrTrnCreditCompanyDtlView' || lspagename=='AgrTrnCcCommitteeInstitutionView' || lspagename=='AgrMstCcCommitteeInstitutionView')
                $location.url('app/'+ lspagename +'?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage +'&lspagetype='+ lspagetype);

            }
    
    }
})();