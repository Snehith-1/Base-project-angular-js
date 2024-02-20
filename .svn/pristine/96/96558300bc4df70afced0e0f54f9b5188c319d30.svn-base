(function () {
    'use strict';

    angular
        .module('angle')
        .controller('LglTrnInvoiceSummary', LglTrnInvoiceSummary);

    LglTrnInvoiceSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function LglTrnInvoiceSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'LglTrnInvoiceSummary';

        activate();

        function activate() {
            var params = {
                caseref_gid: localStorage.getItem('caseref_gid')
        }

        var url = "api/LawyerInvoice/GetinvoiceListSummary";
        SocketService.getparams(url,params).then(function (resp) {
            $scope.invoicelist = resp.data.invoicedtl;
            $scope.caseref_no = resp.data.caseref_no;
            $scope.case_type = resp.data.case_type;
        });
        
    }

        $scope.viewinvoice = function (lawyerinvoicedtl_gid) {
            localStorage.setItem('lawyerinvoicedtl_gid', lawyerinvoicedtl_gid)
        $state.go('app.lawyerPaymentView');
        }
        $scope.back=function()
        {
            $state.go('app.lawyerPayment')
        }
}

})();
