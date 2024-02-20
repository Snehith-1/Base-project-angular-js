(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstLimitManagementViewController', MstLimitManagementViewController);

    MstLimitManagementViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstLimitManagementViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstLimitManagementViewController';

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.customer_urn = $location.search().customer_urn;
        var customer_urn = $scope.customer_urn;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
            
            if (lspage != 'myapp' && lspage != 'CreditApproval' && lspage != 'submittoapp' ) {
                var params = {
                    customer_urn: customer_urn
                }
                var url = 'api/MstAppCreditUnderWriting/GetLimitManagementDtlView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.applicationdtl_list = resp.data.applicationdtl_list;
                });

                var url = 'api/MstAppCreditUnderWriting/GetCadUrnCustomerDtlView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.customer_name = resp.data.customer_name;
                });
            }
            else {
                var params = {
                    customer_urn: customer_urn
                }
                var url = 'api/MstAppCreditUnderWriting/MyAppGetLimitManagementDtlView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.applicationdtl_list = resp.data.applicationdtl_list;
                    if ($scope.applicationdtl_list == null || $scope.applicationdtl_list == "" || $scope.applicationdtl_list == undefined) {
                        Notify.alert("This Application Not Having URN Number", {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });

                var url = 'api/MstAppCreditUnderWriting/GetMyAppCadUrnCustomerDtlView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.customer_name = resp.data.customer_name;
                });
              

            }
        }

        $scope.Back = function () {
            if (lspage == "MstRMCustomerSummary") {
                $state.go('app.MstRMCustomerSummary');
            }
            else if (lspage == "MstCadUrnGrouping") {
                $state.go('app.MstCadUrnAcceptedCustomers');
            }
            else if (lspage == "myapp") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CreditApproval") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {
                $location.url('app/MstRMCustomerSummary');
            }           
        }
                
    }
})();
