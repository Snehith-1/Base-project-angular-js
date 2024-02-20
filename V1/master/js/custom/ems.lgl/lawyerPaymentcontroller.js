(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lawyerPaymentcontroller', lawyerPaymentcontroller);

    lawyerPaymentcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location','$route', 'ngTableParams'];

    function lawyerPaymentcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'lawyerPaymentcontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = "api/LawyerInvoice/GetPaymentSummary";
            SocketService.get(url).then(function (resp) {
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
        $scope.viewinvoice = function (caseref_gid) {
            localStorage.setItem('caseref_gid', caseref_gid)
            $state.go('app.LglTrnInvoiceSummary');
        }
    }
})();
