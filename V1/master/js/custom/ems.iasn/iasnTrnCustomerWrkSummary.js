(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnCustomerWrkSummary', iasnTrnCustomerWrkSummary);

    iasnTrnCustomerWrkSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','$sce'];

    function iasnTrnCustomerWrkSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        var lscustomer_gid;
        var lstype;
        var lsCustomerName;
        var SanctionRefNo;
        vm.title = 'iasnTrnCustomerWrkSummary';

        activate();

        function activate() {
            lscustomer_gid = localStorage.getItem('customer_gid')
            lstype = localStorage.getItem('type')
            // lsCustomerName= localStorage.getItem('CustomerName')
            // SanctionRefNo = localStorage.getItem('SanctionRefNo')
            
            var params=
                {
                    customer_gid:lscustomer_gid,
                    archival_type: lstype
                }
            var url = 'api/IasnTrnWorkItem/WorkItemArchivalSummary';
            SocketService.post(url, params).then(function (resp) {
                $scope.TaggedWI_List = resp.data.MdlWorkItem;
                $scope.workitemref_no = resp.data.workitemref_no;
                $scope.updatedby_on = resp.data.updatedby_on;            

            });
         

        }
        $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
        $scope.back = function()
        {
            $state.go('app.iasnTrnArchivalSummary')
        }

        $scope.EmployeeProfile = function (emp_gid) {
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params = {
                employee_gid: emp_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.user_code = resp.data.user_code;
                    $scope.user_name = resp.data.user_name;
                    $scope.user_photo = resp.data.user_photo;
                    $scope.user_designation = resp.data.user_designation;
                    $scope.user_department = resp.data.user_department;
                    $scope.user_mobileno =resp.data.user_mobileno;
                }
                else {
                    $scope.user_code = "-";
                    $scope.user_name = "-";
                    $scope.user_photo = "N";
                    $scope.user_designation = "-";
                    $scope.user_department = "-";
                }
            });

        }

    }
})();
