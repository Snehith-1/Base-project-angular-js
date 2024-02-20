(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAEntityTypeController', MstSAEntityTypeController);

        MstSAEntityTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSAEntityTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAEntityTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
                       
            var url = 'api/MstApplication360/GetSAEntityType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saentitytype_list = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addsaentitytype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsaentitytype.html',
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
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        satype_gid: $scope.satype.satype_gid,
                        satype_name: $scope.satype.satype_name,
                        saentitytype_name: $scope.txtsaentitytype_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateSAEntityType';
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
                $scope.titlename = function (string) {
                    if (string.length >= 255) {
                        $scope.message = "Allowed Only 255 Characters";
                    }
                    else {
                        $scope.message = "";
                    }
                }
                $scope.lmslength = function (string) {
                    if (string.length >= 30) {
                        $scope.lmsmessage = "Allowed Only 30 Characters";
                    }
                    else {
                        $scope.lmsmessage = "";
                    }
                }
                $scope.bureaulength = function (string) {
                    if (string.length >= 10) {
                        $scope.bureaumessage = "Allowed Only 10 Characters";
                    }
                    else {
                        $scope.bureaumessage = "";
                    }
                }
            }
        }

        $scope.editsaentitytype = function (saentitytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editsaentitytype.html',
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
                var params = {
                    saentitytype_gid: saentitytype_gid
                }
                var url = 'api/MstApplication360/EditSAEntityType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.satype_Name = resp.data.satype_name;
                    $scope.satype_Gid = resp.data.satype_gid;

                    $scope.txteditsaentitytype_name = resp.data.saentitytype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.saentitytype_gid = resp.data.saentitytype_gid;
                });
                $scope.titlename = function (string) {
                    if (string.length >= 255) {
                        $scope.message = "Allowed Only 255 Characters";
                    }
                    else {
                        $scope.message = "";
                    }
                }
                $scope.lmslength = function (string) {
                    if (string.length >= 30) {
                        $scope.lmsmessage = "Allowed Only 30 Characters";
                    }
                    else {
                        $scope.lmsmessage = "";
                    }
                }
                $scope.bureaulength = function (string) {
                    if (string.length >= 10) {
                        $scope.bureaumessage = "Allowed Only 10 Characters";
                    }
                    else {
                        $scope.bureaumessage = "";
                    }
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                                              
                $scope.update = function () {
                    var satype_name = $('#satype_Name :selected').text();
                    var url = 'api/MstApplication360/UpdateSAEntityType';
                    var params = {
                        saentitytype_name: $scope.txteditsaentitytype_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        saentitytype_gid: $scope.saentitytype_gid,
                        satype_name: satype_name,
                        satype_gid: $scope.satype_Gid
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

        $scope.Status_update = function (saentitytype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statussaentitytype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    saentitytype_gid: saentitytype_gid
                }               
                var url = 'api/MstApplication360/EditSAEntityType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.saentitytype_gid= resp.data.saentitytype_gid;
                    $scope.txtsaentitytype_name = resp.data.saentitytype_name;
                    $scope.rbo_status = resp.data.Status;
                });   
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        saentitytype_name: $scope.txtsaentitytype_name,
                        saentitytype_gid: $scope.saentitytype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/InactiveSAEntityType';
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

                var url = 'api/MstApplication360/SAEntityTypeInactiveLogview';

                var param = {
                    saentitytype_gid:saentitytype_gid
                }

                SocketService.getparams(url, param).then(function (resp) {
                    $scope.saentitytypeinactivelog_list = resp.data.application_list;
                });
            }
        }

        $scope.delete = function (saentitytype_gid) {
            var params = {
                saentitytype_gid: saentitytype_gid
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
                    var url = 'api/MstApplication360/DeleteSAEntityType';
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

