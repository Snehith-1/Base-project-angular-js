
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('studentSummarycontroller', studentSummarycontroller);

    studentSummarycontroller.$inject = ['$rootScope', '$scope', '$state', '$modal','SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function studentSummarycontroller($rootScope, $scope, $state, $modal,SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'studentSummarycontroller';

        activate();

        function activate() {
            var url = 'api/student/studentSummary';
            SocketService.get(url).then(function (resp) {
                $scope.student_list = resp.data.student_list;
            });
        }
        $scope.addstudent = function () {
            $state.go('app.addStudent');
        };
        $scope.edit = function (val) {
            $scope.student_gid = val;
            $scope.student_gid = localStorage.setItem('student_gid', val);
            $state.go('app.editStudent');
        };
        $scope.view = function (val) {
            $scope.student_gid = val;
            $scope.student_gid = localStorage.setItem('student_gid', val);
            $state.go('app.viewStudent');
        };
        $scope.delete = function (student_gid) {
            var params = {
                student_gid: student_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/student/studentDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Author!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });

                }

            });
        };


    }
})();

