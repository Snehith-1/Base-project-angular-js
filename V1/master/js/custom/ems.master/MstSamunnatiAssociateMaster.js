(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AssociateMastercontroller', AssociateMastercontroller);

    AssociateMastercontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AssociateMastercontroller($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AssociateMastercontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/AssociateMaster/GetAssociateMaster';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.associate_master = resp.data.associatemaster_list;
                unlockUI();

                // new code start   
                if ($scope.associate_master == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.associate_master.length;
                    if ($scope.associate_master.length < 100) {
                        $scope.totalDisplayed = $scope.associate_master.length;
                    }
                }
                // new code endd
            });
        }
        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.associate_master != null) {

                if (pagecount < $scope.associate_master.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.associate_master.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.associate_master.length;
                        Notify.alert(" Total Summary " + $scope.associate_master.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.associate_master.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };
        // Add Code Starts
        $scope.popupassociatemaster = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.associatemasterSubmit = function () {
                    var params = {
                        name: $scope.name,
                        associate_code: $scope.associate_code,
                        rdbstatus: $scope.rdbstatus
                    }
                    var url = 'api/AssociateMaster/CreateAssociateMaster';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')
                            activate();

                        }
                        else {
                            Notify.alert(resp.data.message, 'warning')
                            activate();
                        }
                    });

                }
            }
        }
        // Add Code Ends

        // Edit Code Starts
        $scope.edit = function (associatemaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    associatemaster_gid: associatemaster_gid
                }
                var url = 'api/AssociateMaster/EditAssociateMaster';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.associatecodeedit = resp.data.associate_code;
                    $scope.nameedit = resp.data.name;
                    $scope.rdbstatusedit = resp.data.rdbstatus;
                    console.log(resp.data.rdbstatus);
                    $scope.associatemaster_gid = resp.data.associatemaster_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.associatemasterUpdate = function () {

                    var params = {
                        associate_code: $scope.associatecodeedit,
                        name: $scope.nameedit,
                        rdbstatus: $scope.rdbstatusedit,
                        associatemaster_gid: $scope.associatemaster_gid
                    }
                    var url = 'api/AssociateMaster/UpdateAssociateMaster';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')

                        }
                        else {
                            Notify.alert(resp.data.message, 'success')
                            activate();

                        }
                    });
                }
            }
        }
        // Edit Code Ends

        // Delete Code Starts
        $scope.delete = function (associatemaster_gid) {
            var params = {
                associatemaster_gid: associatemaster_gid
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
                    var url = 'api/AssociateMaster/DeleteAssociateMaster';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Associate Mater!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
        // Delete Code Ends
    }
})();
