(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingSourceofContactController', MstMarketingSourceofContactController);

    MstMarketingSourceofContactController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstMarketingSourceofContactController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingSourceofContactController';

        activate();

        function activate() {          
            var url = 'api/MstMarketingSourceOfContact/GetMarketingSourceofContact';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.sourceofcontact_data = resp.data.application_list;
                unlockUI();
            });
        }
        $scope.addsourceofcontact = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsource.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        marketingsourceofcontact_name: $scope.txtsource_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstMarketingSourceOfContact/CreateMarketingSourceofContact';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }
                
            }
        }
        $scope.editsourceofcontact = function (marketingsourceofcontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editsource.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    marketingsourceofcontact_gid: marketingsourceofcontact_gid
                }
                var url = 'api/MstMarketingSourceOfContact/EditMarketingSourceofContact';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditsource_name = resp.data.marketingsourceofcontact_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.marketingsourceofcontact_gid = resp.data.marketingsourceofcontact_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstMarketingSourceOfContact/UpdateMarketingSourceofContact';
                    var params = {
                        marketingsourceofcontact_name: $scope.txteditsource_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        marketingsourceofcontact_gid: $scope.marketingsourceofcontact_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            }
        }

        $scope.Status_update = function (marketingsourceofcontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statussource.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    marketingsourceofcontact_gid: marketingsourceofcontact_gid
                }            
                var url = 'api/MstMarketingSourceOfContact/EditMarketingSourceofContact';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingsourceofcontact_gid = resp.data.marketingsourceofcontact_gid
                    $scope.txtsource_name = resp.data.marketingsourceofcontact_name;
                    $scope.rbo_status = resp.data.Status;
                });    
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        marketingsourceofcontact_gid: marketingsourceofcontact_gid,
                        remarks: $scope.txtremarks,
                        rbo_status:$scope.rbo_status
                    
                    }
                    var url = 'api/MstMarketingSourceOfContact/InactiveMarketingSourceofContact';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }activate();
                    }); 

                    $modalInstance.close('closed');

                }
                var params = {
                    marketingsourceofcontact_gid: marketingsourceofcontact_gid
                }

                var url = 'api/MstMarketingSourceOfContact/InactiveMarketingSourceofcontactHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sourceofcontactinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                }); 
            }
        }

        $scope.delete = function (marketingsourceofcontact_gid) {
            var params = {
                marketingsourceofcontact_gid: marketingsourceofcontact_gid
            }
           
            
           SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#d64b3c',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false,
                        cancelButtonColor: 'Crimson',                       
                
                    }, function (isConfirm) {
                        if (isConfirm) {
                            var url = 'api/MstMarketingSourceOfContact/DeleteMarketingSourceofContact';
                            SocketService.getparams(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                SweetAlert.swal('Deleted Successfully!');
                                activate();
                                }
                                else if (resp.data.status == false) {
                                    SweetAlert.swal({
                                        title: 'Warning!',
                                        text: "Can't able to delete Source Of Contact because it is mapped to add Business Development call",
                                        timer: 5000,
                                       
                                        showCancelButton: false,
                                        showConfirmButton: false,
                                        
                                        backgroundcolor: '#d64b3c'
                                });
                                    activate();
                                    }

                                else {
                                Notify.alert(SweetAlert.swal(resp.data.message,{
                             
                                    type: 'warning',
      title: 'warning!',
      text: "Can't able to delete Source Of Contact because it is mapped to add Business Development call",
      timer: 2000,
      showCancelButton: false,
      showConfirmButton: false
                                }

                                ));
                                activate();
                                }
                            });
                        }
                    });
        }
    }
})();

