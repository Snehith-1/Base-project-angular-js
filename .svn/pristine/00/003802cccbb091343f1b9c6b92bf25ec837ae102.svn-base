(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditMappingDetailsController', MstCreditMappingDetailsController);

    MstCreditMappingDetailsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditMappingDetailsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditMappingDetailsController';

        $scope.creditmapping_gid = $location.search().lscreditmapping_gid;
        var creditmapping_gid = $scope.creditmapping_gid;

        activate();

        function activate() {

            var params = {
                creditmapping_gid: creditmapping_gid
            }
            var url = "api/MstCreditMapping/GetCreditMappingLog"
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditmappingloglist = resp.data.creditmappingloglist;
                angular.forEach($scope.creditmappingloglist, function (value, key) {
                    if (value.credithead_gid === "" || value.credithead_gid === null) {
                        value.showcredit2credithead_name = false;
                    }
                    else {
                        value.showcredit2credithead_name = true;
                    }
                    if (value.nationalmanager_gid === "" || value.nationalmanager_gid === null) {
                        value.showcredit2nationalmanager_name = false;
                    }
                    else {
                        value.showcredit2nationalmanager_name = true;
                    }
                    if (value.regionalmanager_gid === "" || value.regionalmanager_gid === null) {
                        value.showcreditr2regionalmanager_name = false;
                    }
                    else {
                        value.showcreditr2regionalmanager_name = true;
                    }
                    if (value.creditmanager_gid === "" || value.creditmanager_gid === null) {
                        value.showcredit2creditmanager_name = false;
                    }
                    else {
                        value.showcredit2creditmanager_name = true;
                    }
                    if (value.creditgroup_name === "" || value.creditgroup_name === null) {
                        value.showcreditgroup_name = false;
                    }
                    else {
                        value.showcreditgroup_name = true;
                    }
                });
               
                unlockUI();
            });
        }

        $scope.addcreditgroup = function () {
            $location.url('app/MstCreditMappingAdd');
        }

        $scope.editcreditgroup = function (creditmapping_gid) {
            $location.url('app/MstCreditMappingEdit?lscreditmapping_gid=' + creditmapping_gid);
        }
     

        $scope.Back = function () {
        
            $location.url('app/MstCreditMappingSummary');
            }
     
    }
})();
