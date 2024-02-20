
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addStudentcontroller', addStudentcontroller);

    addStudentcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function addStudentcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addStudentcontroller';

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
        }
        $scope.studentback = function (val) {
            $state.go('app.studentSummary');
        };
        $scope.studentSubmit = function () {
            var dob = new Date();
            dob.setFullYear($scope.dob.getFullYear());
            dob.setMonth($scope.dob.getMonth());
            dob.setDate($scope.dob.getDate());

            var params = {
                student_firstname: $scope.txtfirstname,
                student_lastname: $scope.txtlastname,
                student_emailid: $scope.txtemail,
                student_mobno: $scope.txtmobileno,
                student_code: $scope.txtstucode,
                student_dob: dob
            }
            //console.log(params);
            var url = 'api/student/addStudent';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Student Added Successfully..!!', 'success')
                    activate();


                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Adding Student!', 'warning')
                    activate();
                }
            });
            var url = 'api/student/studentSummary';
            SocketService.get(url).then(function (resp) {
                $scope.student_list = resp.data.student_list;
            });
            $state.go('app.studentSummary');
        }
    }
})();

