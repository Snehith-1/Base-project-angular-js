(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addAuthorcontroller', addAuthorcontroller);

    addAuthorcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function addAuthorcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addAuthorcontroller';

        activate();

        function activate() { }
        $scope.authorback = function (val) {
            $state.go('app.authorSummary');
        };

        $scope.authorSubmit = function () {
            var params = {
                author_firstname: $scope.txtfirstname,
                author_lastname: $scope.txtlastname,
                author_emailid: $scope.txtemail,
                author_mobno: $scope.txtmobileno,
                author_code: $scope.txtauthcode,
                author_address1: $scope.txtaddress1,
                acc_name: $scope.txtaccname,
                ifsc_code: $scope.txtifsccode,
                acc_no: $scope.txaccno,
                bank_name: $scope.txtbankname
            }
        console.log(params);
            var url = 'api/author/addAuthor';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Author Added Successfully..!!', 'success')
                    activate();
                    

                }
                else {
                    unlockUI();
                    Notify.alert('Author Code already Exist!', 'warning')
                    activate();
                }
            });
            var url = 'api/author/authorSummary';
            SocketService.get(url).then(function (resp) {
                $scope.author_list = resp.data.author_list;
            });
            $state.go('app.authorSummary');
        }
    }
})();
