(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditFSAPandLViewController', MstCreditFSAPandLViewController);

    MstCreditFSAPandLViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditFSAPandLViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'MstCreditFSAPandLViewController';
        var vm = this;
        vm.title = 'MstCreditFsaDetailAddController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.template_name = $location.search().template_name;
        var template_name = $scope.template_name;
        $scope.lspage = $location.search().lspage;
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
            var url = "api/MstAppCreditUnderWriting/GetProfitLoss";
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.profitloss_list = resp.data.profitloss_list;
            });
            var params = {
                credit_gid: institution_gid,
                application_gid: application_gid,
                template_type : template_name
            }           
            var url = "api/MstAppCreditUnderWriting/GetBalanceSheetTemplate1List";
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.creditbalancesheettemplate1_list = resp.data.creditbalancesheettemplate1_list;
            });

            var params = {
                credit_gid: institution_gid,
                application_gid: application_gid,
                template_type : template_name
            }  
            var url = "api/MstAppCreditUnderWriting/GetBalanceSheetTemplate2List";
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.creditbalancesheettemplate2_list = resp.data.creditbalancesheettemplate2_list;
            });

            var params = {
                credit_gid: $location.search().institution_gid,
                template_name: $location.search().template_name,
                application_gid: $location.search().application_gid,
            }
            var url = "api/MstAppCreditUnderWriting/GetSummaryTemplate1View";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.summarytemplate1_list = resp.data.summarytemplate1_list;
            });
            var params = {
                credit_gid: $location.search().institution_gid,
                template_name: $location.search().template_name,
                application_gid: $location.search().application_gid,
            }
            var url = "api/MstAppCreditUnderWriting/GetSummaryTemplate2View";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.summarytemplate2_list = resp.data.summarytemplate2_list;
            });
            var params = {
                credit_gid: institution_gid,
                template_name: template_name,
                application_gid: application_gid
            }           
            var url = "api/MstAppCreditUnderWriting/GetProfitLossTemp2List";
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
            if(lspagename=='MstCreditFsaDetailAdd')
                $location.url('app/MstCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage);
            
            else if (lspagename=='MstCreditCompanyDtlView')
                $location.url('app/MstCreditCompanyDtlView?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage +'&lspagetype='+ lspagetype);
            
            else if (lspagename=='MstCcCommitteeInstitutionView')
                $location.url('app/MstCcCommitteeInstitutionView?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + $scope.lspage +'&lspagetype='+ lspagetype);

            }
    
    }
})();