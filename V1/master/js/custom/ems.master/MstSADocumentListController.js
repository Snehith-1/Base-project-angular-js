(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSADocumentListController', MstSADocumentListController);

        MstSADocumentListController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSADocumentListController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSADocumentListController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            
            var url = 'api/MstApplication360/GetSADocumentList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.sadocumentlist_list = resp.data.application_list;
                unlockUI();
            });
           
        }

        $scope.addsadocumentlist = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsadocumentlist.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                  
                var url = 'api/MstApplication360/SATypeList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.satype_list = resp.data.satype_list;
                    unlockUI();
                });
                var url = 'api/MstApplication360/GetSAEntityType';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.saentitytype_list = resp.data.application_list;
                    unlockUI();
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var lssaentitytype_gid = '';
                    var lssaentitytype_name = '';
                    if ($scope.cboconstitution != undefined || $scope.cboconstitution != null) {
                        lssaentitytype_gid = $scope.cboconstitution.saentitytype_gid;
                        lssaentitytype_name = $scope.cboconstitution.saentitytype_name;
                    }
                    var params = {
                        satype_gid: $scope.satype.satype_gid,
                        satype_name: $scope.satype.satype_name,
                        saentitytype_gid: lssaentitytype_gid,
                        saentitytype_name: lssaentitytype_name,
                        sadocumentlist_name: $scope.txtsadocumentlist_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateSADocumentList';
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

        $scope.editsadocumentlist = function (sadocumentlist_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editsadocumentlist.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/MstApplication360/SATypeList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.satype_list = resp.data.satype_list;
                    unlockUI();
                });
                var url = 'api/MstApplication360/GetSAEntityType';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.saentitytype_list = resp.data.application_list;
                    unlockUI();
                });
                var params = {
                    sadocumentlist_gid: sadocumentlist_gid
                }
                var url = 'api/MstApplication360/EditSADocumentList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.saentitytype_name = resp.data.saentitytype_name;
                    $scope.cboconstitution_name = resp.data.saentitytype_gid;
                    $scope.satype_Name = resp.data.satype_name;
                    $scope.satype_Gid = resp.data.satype_gid;
                    $scope.txteditsadocumentlist_name = resp.data.sadocumentlist_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.sadocumentlist_gid = resp.data.sadocumentlist_gid;
                }); 
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                                              
                $scope.update = function () {
                    var satype_name = $('#satype_Name :selected').text();
                    var saentitytype_name = $('#entitytype :selected').text();
                    var url = 'api/MstApplication360/UpdateSADocumentList';
                    var params = {
                        sadocumentlist_name: $scope.txteditsadocumentlist_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        sadocumentlist_gid: $scope.sadocumentlist_gid,
                       satype_name: satype_name,
                        satype_gid: $scope.satype_Gid,
                        saentitytype_name: saentitytype_name,
                        saentitytype_gid: $scope.saentitytype_Gid,
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

        $scope.Status_update = function (sadocumentlist_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statussadocumentlist.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sadocumentlist_gid: sadocumentlist_gid
                }               
                var url = 'api/MstApplication360/EditSADocumentList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sadocumentlist_gid= resp.data.sadocumentlist_gid;
                    $scope.txtsadocumentlist_name = resp.data.sadocumentlist_name;
                    $scope.rbo_status = resp.data.Status;
                });    
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        sadocumentlist_name: $scope.txtsadocumentlist_name,
                        sadocumentlist_gid: $scope.sadocumentlist_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/InactiveSADocumentList';
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
                var url = 'api/MstApplication360/SADocumentListInactiveLogview';

                var params = {
                    sadocumentlist_gid:sadocumentlist_gid
                }

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sadocumentlistinactivelog_list = resp.data.application_list;
                }); 
            }
        }

        $scope.delete = function (sadocumentlist_gid) {
             var params = {
                sadocumentlist_gid: sadocumentlist_gid
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
                    var url = 'api/MstApplication360/DeleteSADocumentList';
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
    }
})();

