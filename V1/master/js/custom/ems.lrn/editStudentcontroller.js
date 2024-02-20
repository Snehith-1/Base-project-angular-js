
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editStudentcontroller', editStudentcontroller);

    editStudentcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function editStudentcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editStudentcontroller';

        activate();

        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.student_gid = localStorage.getItem('student_gid');
            var url = 'api/student/studentUpdatedetails';
            var param = {
                student_gid: $scope.student_gid
            };


            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtfirstname = resp.data.student_firstnameedit;
                $scope.txtlastname = resp.data.student_lastnameedit;
                $scope.txtstucode = resp.data.student_codeedit;
                $scope.txtemail = resp.data.student_emailidedit;
                $scope.txtmobileno = resp.data.student_mobnoedit;
                $scope.dobedit = resp.data.student_dobedit;
                unlockUI();
                console.log(resp.data);
            });
        }

        $scope.editstudentback = function () {
            $state.go('app.studentSummary');
        }

        $scope.editstudentupdate = function () {
            var dobedit = new Date();
            dobedit.setFullYear($scope.dobedit.getFullYear());
            dobedit.setMonth($scope.dobedit.getMonth());
            dobedit.setDate($scope.dobedit.getDate());

            var params = {
                student_gid: $scope.student_gid,
                student_firstnameedit: $scope.txtfirstname,
                student_lastnameedit: $scope.txtlastname,
                student_codeedit: $scope.txtstucode,
                student_emailidedit: $scope.txtemail,
                student_mobnoedit: $scope.txtmobileno,
                student_dobedit: dobedit
            }

            var url = 'api/student/updateStudent';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.studentSummary');
                    Notify.alert('Student Updated Successfully..!!', 'success')
                }

                else {
                    Notify.alert('Error Occurred While Updating Student !')
                }
                activate();
            });
        }
    }
})();

