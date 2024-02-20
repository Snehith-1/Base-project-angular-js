(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTenureViewController', MstHRLoanTenureViewController);

    MstHRLoanTenureViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanTenureViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTenureViewController';
        $scope.hrloantenure_gid = cmnfunctionService.decryptURL($location.search().hash).lshrloantenure_gid;
        var hrloantenure_gid = $scope.hrloantenure_gid;

        activate();

        function activate() {

            var params = {
                hrloantenure_gid: hrloantenure_gid
            }
            var url = 'api/MstHRLoanTenure/EditHRLoanTenure';
            SocketService.getparams(url, params).then(function (resp) {                
                $scope.lblhrdocument = resp.data.hrloantypeoffinancialassistance_name;
                $scope.lbltenure = resp.data.hrloantenure_name;
                $scope.lbltenurestartdate = resp.data.hrloantenurestart_date;               
                $scope.lbllms_code = resp.data.lms_code;
                $scope.lblbureau_code = resp.data.bureau_code;               
                unlockUI();
            });

            var url = 'api/MstHRLoanTenure/Gettenurelog';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tenurelog_details = resp.data.hrloantenure_list;                              
                unlockUI();
            });
        }
       
        $scope.Back = function () {
            $state.go('app.MstHRLoanTenure');

        }
        }
})();
