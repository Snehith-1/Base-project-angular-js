(function () {
    'use strict';

    angular
        .module('angle')
        .controller('invoiceSummarycontroller', invoiceSummarycontroller);

    invoiceSummarycontroller.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route'];

    function invoiceSummarycontroller($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route) {
        var vm = this;
        vm.title = 'invoiceSummarycontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var params = {
                lawyeruser_gid: localStorage.getItem('lawyeruser_gid')
            }
            var url = "api/LawyerInvoice/getinvoicedtlList";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.invoicelist = resp.data.invoicedtl;
                $scope.total = $scope.invoicelist.length;
            });

        }
        document.getElementById('pagecount').onkeyup = function () {
            // console.log(document.getElementById('pagecount').value);
            if ($scope.pagecount == null) {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#DCDCDC';
            }
            else {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#ffa';
            }
        };
        $scope.loadMore = function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);

            $scope.totalDisplayed += Number;
            unlockUI();
        };
        $scope.createInvoice = function () {
            $state.go('app.invoiceCreate');
        }
        $scope.editinvoice = function (lawyerinvoicedtl_gid)
        {
            console.log(lawyerinvoicedtl_gid);
            localStorage.setItem('lawyerinvoicedtl_gid', lawyerinvoicedtl_gid);
            $state.go('app.lpMstInvoiceEdit');
        }
        $scope.viewinvoice = function (lawyerinvoicedtl_gid) {
            localStorage.setItem('lawyerinvoicedtl_gid', lawyerinvoicedtl_gid);
            $state.go('app.invoiceView');
        }
    }
})();
