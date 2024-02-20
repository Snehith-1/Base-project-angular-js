(function () {
    'use strict';

    angular
        .module('angle')
        .controller('invoiceViewcontroller', invoiceViewcontroller);

    invoiceViewcontroller.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route'];

    function invoiceViewcontroller($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'invoiceViewcontroller';

        activate();

        function activate() {

            var params = {
                lawyerinvoicedtl_gid: localStorage.getItem('lawyerinvoicedtl_gid')
            }
            var url = "api/LawyerInvoice/getinvoicedetails";
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp);
                $scope.invoicedetail = resp.data;
                $scope.filename_list = resp.data.uploaddocument
            });
        }


        $scope.downloads = function (val1, val2) {
            
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            console.log(str);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.cancel = function () {
            $state.go('app.invoiceSummary');
        }
    }
})();
