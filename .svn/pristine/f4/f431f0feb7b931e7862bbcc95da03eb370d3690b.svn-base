(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customer2misdataController', customer2misdataController);

    customer2misdataController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function customer2misdataController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editRegisterlawyercontroller';
        activate();
        function activate() {

            $scope.urn = localStorage.getItem('urn');
            var url = 'api/misDataimport/Getcutomer2misdata';

            var param = {
                urn: $scope.urn
            };
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.customer_name = resp.data.customer_name;
                $scope.urn = resp.data.urn;
                $scope.mdlMisdataimport = resp.data.mdlMisdataimport;

              
                console.log(resp.data.customer_name);
            });

        }
        $scope.requestback = function () {
            $state.go('app.misDataimport')
        }
        }
    })();
        