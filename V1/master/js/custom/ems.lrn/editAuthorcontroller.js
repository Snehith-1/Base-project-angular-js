(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editAuthorcontroller', editAuthorcontroller);

    editAuthorcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function editAuthorcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editAuthorcontroller';

        activate();

        function activate() {

            $scope.author_gid = localStorage.getItem('author_gid');
            var url = 'api/author/authorUpdatedetails';
            var param = {
                author_gid: $scope.author_gid
            };

          
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtfirstname = resp.data.author_firstnameedit;
                $scope.txtlastname = resp.data.author_lastnameedit;
                $scope.txtauthcode = resp.data.author_codeedit;
                $scope.txtemail = resp.data.author_emailidedit;
                $scope.txtmobileno = resp.data.author_mobnoedit;
                $scope.txtaddress1 = resp.data.author_address1edit;
                $scope.txtaccname = resp.data.acc_nameedit;
                $scope.txaccno = resp.data.acc_noedit;
                $scope.txtifsccode = resp.data.ifsc_codeedit;
                $scope.txtbankname = resp.data.bank_nameedit;
                unlockUI();
                console.log(resp.data);
            });
        }

        $scope.editauthorback = function () {
            $state.go('app.authorSummary');
        }

        $scope.editauthorupdate = function () {

            var params = {
                author_gid: $scope.author_gid,
                author_firstnameedit: $scope.txtfirstname,
                author_lastnameedit: $scope.txtlastname,
                author_codeedit: $scope.txtauthcode,
                author_emailidedit: $scope.txtemail,
                author_mobnoedit: $scope.txtmobileno,          
                author_address1edit: $scope.txtaddress1,
                acc_nameedit: $scope.txtaccname,
                acc_noedit: $scope.txaccno,
                ifsc_codeedit: $scope.txtifsccode,
                bank_nameedit: $scope.txtbankname

            }

            var url = 'api/author/updateAuthor';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.authorSummary');
                    Notify.alert('Author Updated Successfully..!!', 'success')
                }

                else {
                    Notify.alert('Error Occurred While Updating Author !')
                }
                activate();
            });
        }
    }
})();
