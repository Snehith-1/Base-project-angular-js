(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDisbursementDocumentController', MstDisbursementDocumentController);

        MstDisbursementDocumentController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstDisbursementDocumentController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDisbursementDocumentController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            
         var url = 'api/MstApplication360/GetDisbursementDocument';
          lockUI();
         
         SocketService.get(url).then(function (resp) {
         $scope.DisbursementDocument_List = resp.data.DisbursementDocument_List;
         unlockUI();
         });
        }

        // Add
             
        $scope.adddisbursementdocument = function () {
                var modalInstance = $modal.open({
                templateUrl: '/addpopup.html',
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
                        disbursementdocument_name: $scope.txtdisbursementdocument_name,
                         lms_code: $scope.txtlms_code,
                         bureau_code: $scope.txtbureau_code,
                    }
                var url = 'api/MstApplication360/CreateDisbursementDocument';
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

        //Edit

        $scope.editdisbursementdocument = function (disbursementdocument_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editpopup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    disbursementdocument_gid: disbursementdocument_gid
                }
                var url = 'api/MstApplication360/EditDisbursementDocument';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditdisbursementdocument_name = resp.data.disbursementdocument_name;
                    $scope.txteditdisbursementdocument_id = resp.data.disbursementdocument_id;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    // $scope.txteditremarks = resp.data.remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstApplication360/UpdateDisbursementDocument';
                    var params = {
                        disbursementdocument_gid: disbursementdocument_gid,
                        disbursementdocument_name: $scope.txteditdisbursementdocument_name,
                        disbursementdocument_id: $scope.txteditdisbursementdocument_id,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        // remarks: $scope.txteditremarks
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


        //Delete
        
        $scope.delete = function (disbursementdocument_gid) {
            var params = {
                disbursementdocument_gid: disbursementdocument_gid
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
                   var url = 'api/MstApplication360/DeleteDisbursementDocument';
                   SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                               timeout: 3000
                             });
                        activate();
                        unlockUI;
                    }
                });
            }
        });
    }


       // status

        $scope.Status_update = function (disbursementdocument_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusdisbursementdocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                   
                var params = {
                    disbursementdocument_gid: disbursementdocument_gid
                }
                var url = 'api/MstApplication360/EditDisbursementDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.disbursementdocument_gid = resp.data.disbursementdocument_gid
                    $scope.txteditdisbursementdocument_name = resp.data.disbursementdocument_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        disbursementdocument_gid: disbursementdocument_gid,
                        disbursementdocument_name: $scope.txteditdisbursementdocument_name,
                        remarks: $scope.txtremarks,
                        Status: $scope.rbo_status
                    }
                    var url = 'api/MstApplication360/InactiveDisbursementDocument';
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
                        }
                        activate();

                    }
                    );

                    $modalInstance.close('closed');

                }
              
                var param = {
                    disbursementdocument_gid: disbursementdocument_gid
                }
                var url = 'api/MstApplication360/GetDisbursementDocumentInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                $scope.disbursementdocumentinactivelog_data = resp.data.DisbursementDocument_List;
                    unlockUI();
                });
            }
        }


    }

    

})();