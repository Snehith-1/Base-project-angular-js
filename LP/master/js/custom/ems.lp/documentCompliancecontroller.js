(function () {
    'use strict';

    angular
        .module('angle')
        .controller('documentCompliancecontroller', documentCompliancecontroller);

    documentCompliancecontroller.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route'];

    function documentCompliancecontroller($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'documentCompliancecontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/documentCompliance/Compliancemanagementsummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.requestcompliance_data = resp.data.requestcompliance_list;
                $scope.total = $scope.requestcompliance_data.length;
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
        $scope.View = function (val,val1) {
            localStorage.setItem('requestcompliance_gid', val);
            localStorage.setItem('requestcompliance2lawyerdtl_gid', val1);
            $state.go('app.documentComplianceView');
        }

    }
})();
