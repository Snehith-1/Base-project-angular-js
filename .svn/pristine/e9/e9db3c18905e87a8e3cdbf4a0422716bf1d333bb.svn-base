(function () {
    'use strict';

    angular
        .module('angle')
        .controller('caseAllocationViewcontroller', caseAllocationViewcontroller);

    caseAllocationViewcontroller.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route'];

    function caseAllocationViewcontroller($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'caseAllocationViewcontroller';

        activate();

        function activate() {
            lockUI();
            var allocationdtl_gid = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
            }

            var url = "api/allocationManagement/getExternalDetails";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
               
                $scope.customerdtl = resp.data.customerdtl;
                $scope.externalname = resp.data.externalname;
                $scope.AllocateExtRemarks = resp.data.requested_remarks;
                $scope.assigned_by = resp.data.assigned_by;
                $scope.assigned_date = resp.data.assigned_date;
                $scope.target_date = resp.data.target_date;
            });

            var url = "api/allocationManagement/getExternaldocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                    $scope.upload_list = resp.data.upload_list;
                    $scope.documentUpload = true;
                }
                else {

                    $scope.documentNotUpload = true;
                }
              

            });
            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.customerdetails = resp.data;
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.customerCollateral = resp.data.collateraldtl;
                $scope.customerguarantorlist = resp.data.Guarantorsdtl;
                $scope.customerPromotorlist = resp.data.Promoterdtl;
                angular.forEach($scope.sanctiondetails, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid,
                        allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
                    };
                    var url = 'api/allocationManagement/GetAllocateloanList';
                    SocketService.post(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanList;
                        value.expand = false;

                    });
                });
                unlockUI();
            });
           
        }

        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.back = function () {
            $state.go('app.caseAllocation');
        }
    }
})();
