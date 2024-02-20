(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMLoanDetailsDtlsController', MstRMLoanDetailsDtlsController);

        MstRMLoanDetailsDtlsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstRMLoanDetailsDtlsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMLoanDetailsDtlsController';
        var application_gid = $location.search().application_gid;
        var customer_urn = $location.search().customer_urn;
        var customer_urn1 = $location.search().customer_urn1;
        var lspage = $location.search().lspage;
        var appcreditapproval_gid = $location.search().appcreditapproval_gid;
        var product_gid = $location.search().product_gid;
        var variety_gid = $location.search().variety_gid;
        var lspagetype = $location.search().lspagetype;
        $scope.lspagetype = $location.search().lspagetype;
        console.log($scope.lspagetype);


        activate();

        function activate() { 
           
            if (lspage != 'myapp' || lspage != 'StartScheduled' || lspage != 'PendingCADReview') {
                var params = {
                    customer_urn: customer_urn
                }
                var url = 'api/MstCADApplication/GetLoanDetailsUrnView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loandetails_list = resp.data.alldatamodified_List;
                    unlockUI();
                }); 
            }
            else if (lspage == 'myapp' || lspage == 'StartScheduled' || lspage == 'PendingCADReview')
            {
                var params = {
                    application_gid: application_gid
                }
                var url = 'api/MstApplicationView/GetLoanDetailsView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loandetails_list = resp.data.alldatamodified_List;
                    unlockUI();
                }); 
            }
           
        }
        
        $scope.Back = function () {
          
            if (lspage == 'RMCADUrnGroupingMain') {
                $location.url('app/MstRMCustomerDashboard?customer_urn=' + customer_urn + '&lspage=' + lspage);
            }
            else if (lspage == 'myapp')
            {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=myapp' + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
            }
            else if (lspage == 'CreditApproval') {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=myapp' + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
            }
            else if (lspage == 'StartScheduled') {
                $location.url('app/MstStartScheduledMeeting?application_gid=' + application_gid);
            }
            else if (lspage == 'PendingCADReview') {
                $location.url('app/MstCADPendingApplicationEdit?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
            }
            else if (lspage == 'CADAcceptanceCustomers') {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
            }
           
            else  {
                $location.url('app/MstPostCcActivitiesRMView?application_gid=' + application_gid + '&customer_urn=' + customer_urn + '&lspage=StartScheduled');
            }
        }
        $scope.close = function () {
            window.close();
        }
    }
})();

