(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTenureController', MstHRLoanTenureController);

        MstHRLoanTenureController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanTenureController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTenureController';

        activate();

        function activate() {
           
            var url = 'api/MstHRLoanTenure/GetHRLoanTenure';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.tenure_data = resp.data.hrloantenure_list;
                $scope.typeofdocument_list = resp.data.typeofdocument_list;                
                unlockUI();
            });
        }
        $scope.addtenure = function () {
            $state.go('app.MstHRLoanTenureAdd');
        }
       

        $scope.viewtenure = function (hrloantenure_gid) {
            $location.url('app/MstHRLoanTenureView?hash=' + cmnfunctionService.encryptURL('lshrloantenure_gid=' + hrloantenure_gid));
        }

        $scope.edittenure = function (hrloantenure_gid) {
            $location.url('app/MstHRLoanTenureEdit?hash=' + cmnfunctionService.encryptURL('lshrloantenure_gid=' + hrloantenure_gid));
        }
       
        $scope.Status_update = function (hrloantenure_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statustenure.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    hrloantenure_gid: hrloantenure_gid
                }
                var url = 'api/MstHRLoanTenure/EditHRLoanTenure';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrloantenure_gid = resp.data.hrloantenure_gid
                    $scope.hrloantenure_name = resp.data.hrloantenure_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrloantenure_gid: hrloantenure_gid,
                        hrloantenure_name: $scope.hrloantenure_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanTenure/InactiveHRLoanTenure';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    hrloantenure_gid: hrloantenure_gid
                }

                var url = 'api/MstHRLoanTenure/InactiveHRLoanTenureHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.tenureinactivelog_data = resp.data.tenureinactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.Add_tenure = function (hrloantenure_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/addtenure.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.open1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    $scope.opened1 = true;
                };
            
                $scope.minDate = new Date();

                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };              
                

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.add_tenure = function () {
                    
                    var params = {
                        hrloantenure_gid: hrloantenure_gid, 
                        hrloantenure_name: $scope.txttenure_name,                        
                        hrloantenurestart_date: $scope.txttenurestart_date,                  
                         
                    }
                    var url = 'api/MstHRLoanTenure/CreateHRLoanTenureUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');
                

                }
                              

            }
        }


        $scope.delete = function (hrloantenure_gid,hrloantypeoffinancialassistance_gid) {
            var params = {
                hrloantenure_gid: hrloantenure_gid,
                hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid
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
                            lockUI();
                            var url = 'api/MstHRLoanTenure/DeleteHRLoanTenure';
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    SweetAlert.swal('Deleted Successfully!');
                                    activate();
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                    unlockUI();
                                }
                            });
                            }
                    });
        }
        }
})();

