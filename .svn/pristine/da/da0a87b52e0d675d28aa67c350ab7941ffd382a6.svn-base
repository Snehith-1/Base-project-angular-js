(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTenureAddController', MstHRLoanTenureAddController);

        MstHRLoanTenureAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanTenureAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTenureAddController';

        activate();

        function activate() { 
            var url = 'api/MstHRLoanTenure/GetHRLoanTenureDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.typeofdocument_list = resp.data.typeofdocument_list;          

            });
        }
       
        $scope.ok = function () {
            $modalInstance.close('closed');
        };
      
        $scope.Back = function () {
            $state.go('app.MstHRLoanTenure');

        }
        $scope.submit = function () {
            var lshrloantypeoffinancialassistance_gid = '';
            var lshrloantypeoffinancialassistance_name = '';
            if ($scope.cbohrdocument != undefined || $scope.cbohrdocument != null) {
                lshrloantypeoffinancialassistance_gid = $scope.cbohrdocument.hrloantypeoffinancialassistance_gid;
                lshrloantypeoffinancialassistance_name = $scope.cbohrdocument.hrloantypeoffinancialassistance_name;
            }

            var params = {
                hrloantenure_name: $scope.txttenure_name,
                hrloantypeoffinancialassistance_gid: lshrloantypeoffinancialassistance_gid,
                hrloantypeoffinancialassistance_name: lshrloantypeoffinancialassistance_name,               
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,
                 
            }
            lockUI();
            var url = 'api/MstHRLoanTenure/CreateHRLoanTenure';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstHRLoanTenure');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                
            });
        }
    }
})();
