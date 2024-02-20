(function () {
    'use strict';

    angular
        .module('angle')
        .controller('authorSummarycontroller', authorSummarycontroller);

    authorSummarycontroller.$inject = ['$rootScope', '$scope', '$state', 'SweetAlert', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function authorSummarycontroller($rootScope, $scope, $state,SweetAlert, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'authorSummarycontroller';

        activate();

        function activate() {
            var url = 'api/author/authorSummary';
            SocketService.get(url).then(function (resp) {
                $scope.author_list = resp.data.author_list;
            });
            console.log($scope.author_list);
        }
        $scope.addauthor = function () {
            $state.go('app.addAuthor');
        };

        $scope.edit = function (val) {
            $scope.author_gid = val;
            $scope.author_gid = localStorage.setItem('author_gid', val);
            $state.go('app.editAuthor');
        };
        $scope.view = function (val) {
            $scope.author_gid = val;
            $scope.author_gid = localStorage.setItem('author_gid', val);
            $state.go('app.viewAuthor');
        };
        $scope.delete = function (author_gid) {
            var params = {
                author_gid: author_gid
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
                    var url = 'api/author/authorDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert('you can not delete this author because he had created the Course', {
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
