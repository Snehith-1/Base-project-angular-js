(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDocumentTypeSummaryController', MstDocumentTypeSummaryController);

    MstDocumentTypeSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstDocumentTypeSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDocumentTypeSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();
        lockUI();
        function activate() {
            var url = 'api/MstApplication360/GetDocumentType';
            SocketService.get(url).then(function (resp) {
                $scope.documenttype_list = resp.data.documenttype;
                unlockUI();
            });
        }

        //Add

        $scope.adddoctype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/adddoctype.html',
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
                        documenttype_code: $scope.txtdocument_code,
                        documenttype_name: $scope.txtdocument_name,
                        description: $scope.txtdescription
                    }
                    lockUI();
                    var url = 'api/MstApplication360/PostDocumentType';
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
                        }
                    });
                }
            }
        }


        //Edit

        $scope.editdoctype = function (documenttypes_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editdoctype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    documenttypes_gid: documenttypes_gid
                }
                lockUI();
                var url = 'api/MstApplication360/GetDocumentEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.documentcodeedit = resp.data.documenttype_code;
                    $scope.txteditdocument_name = resp.data.documenttype_name;
                    $scope.txteditdescription = resp.data.description;
                   
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    lockUI();
                    var url = 'api/MstApplication360/DocumentUpdate';
                    var params = {
                        documenttypes_gid: documenttypes_gid,
                        documenttype_code: $scope.documentcodeedit,
                        documenttype_name: $scope.txteditdocument_name,
                        description: $scope.txteditdescription,
                    }
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
                        }
                    });
                }
            }
        }


        // Showoverpopup

        $scope.description = function (description) {
            var modalInstance = $modal.open({
                templateUrl: '/description.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.description = description;
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        //Status

        $scope.Status_update = function (documenttypes_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusdocumenttype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    documenttypes_gid: documenttypes_gid
                }
                lockUI();
                var url = 'api/MstApplication360/GetDocumentEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txteditdocument_name = resp.data.documenttype_name;
                    $scope.rbo_status = resp.data.Status;
                    
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        documenttypes_gid: documenttypes_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    lockUI();
                    var url = 'api/MstApplication360/InactiveDocumentType';
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
                        } activate();
                    });

                    $modalInstance.close('closed');
                }
                var params = {
                    documenttypes_gid: documenttypes_gid
                }
                lockUI();
                var url = 'api/MstApplication360/InactiveDocumentTypeHistory';              
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.documenttypeinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        //Delete

        $scope.delete = function (documenttypes_gid) {
            var params = {
                documenttypes_gid: documenttypes_gid
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
                    var url = 'api/MstApplication360/DeleteDocumentType';
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