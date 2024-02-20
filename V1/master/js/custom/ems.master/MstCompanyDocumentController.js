(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCompanyDocumentController', MstCompanyDocumentController);

    MstCompanyDocumentController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstCompanyDocumentController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCompanyDocumentController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            var url = 'api/MstApplication360/GetCompanyDocument';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.companydocument_data = resp.data.application_list;
                $scope.document_list = resp.data.document_list;
                $scope.documentseverity_list = resp.data.documentseverity_list;
                unlockUI();
            });
        }
        $scope.addCompanyDocument = function () {
            $state.go('app.MstCompanyDocumentAdd');
        }
        //$scope.editCompanyDocument = function () {
        //    $state.go('app.MstCompanyDocumentEdit');
        //}
        //$scope.viewCompanyDocument = function () {
        //    $state.go('app.MstCompanyDocumentView');
        //}

        $scope.viewCompanyDocument = function (companydocument_gid) {
            $location.url('app/MstCompanyDocumentView?lscompanydocument_gid=' + companydocument_gid);
        }

        $scope.editcompanydocument = function (companydocument_gid) {
            $location.url('app/MstCompanyDocumentEdit?lscompanydocument_gid=' + companydocument_gid);
        }
        //$scope.addCompanyDocument = function () {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/addCompanyDocument.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        var url = 'api/MstApplication360/GetCompanyDropDown';
        //        SocketService.get(url).then(function (resp) {
        //            $scope.document_list = resp.data.document_list;

        //        });
        //        $scope.submit = function () {
        //            var lsdocumenttypes_gid = '';
        //            var lsdocumenttype_name = '';
        //            if ($scope.cbodocument != undefined || $scope.cbodocument != null) {
        //                lsdocumenttypes_gid = $scope.cbodocument.documenttypes_gid;
        //                lsdocumenttype_name = $scope.cbodocument.documenttype_name;
        //            }
        //            var params = {
        //                companydocument_name: $scope.txtCompanyDocument_name,
        //                documenttypes_gid: lsdocumenttypes_gid,
        //                documenttype_name: lsdocumenttype_name,
        //                lms_code: $scope.txtlms_code,
        //                bureau_code: $scope.txtbureau_code

        //            }
        //            var url = 'api/MstApplication360/CreateCompanyDocument';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    $modalInstance.close('closed');
        //                    activate();
        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    $modalInstance.close('closed');
        //                    activate();
        //                }
        //            });

        //            // $modalInstance.close('closed');

        //        }
                
        //    }
        //}

        //$scope.editCompanyDocument = function (companydocument_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editCompanyDocument.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        var params = {
        //            companydocument_gid: companydocument_gid
        //        }
        //        var url = 'api/MstApplication360/EditCompanyDocument';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.txteditCompanyDocument_name = resp.data.companydocument_name;
        //            $scope.txteditlms_code = resp.data.lms_code;
        //            $scope.txteditbureau_code = resp.data.bureau_code;
        //            $scope.companydocument_gid = resp.data.companydocument_gid;
        //            $scope.cbodocumentedit = resp.data.documenttypes_gid;
        //            $scope.documenttype_list = resp.data.documenttype_list;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.update = function () {
        //            var documenttype_name = $('#documentedit :selected').text();
        //            var url = 'api/MstApplication360/UpdateCompanyDocument';
        //            var params = {
        //                companydocument_name: $scope.txteditCompanyDocument_name,
        //                lms_code: $scope.txteditlms_code,
        //                bureau_code: $scope.txteditbureau_code,
        //                companydocument_gid: $scope.companydocument_gid,
        //                documenttypes_gid: $scope.cbodocumentedit,
        //                documenttype_name: documenttype_name
        //            }
        //            SocketService.post(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();

        //                }
        //                else {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();
        //                }
        //            });

        //        }
                
        //    }
        //}

        $scope.Status_update = function (companydocument_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCompanyDocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    companydocument_gid: companydocument_gid
                }
                var url = 'api/MstApplication360/EditCompanyDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.companydocument_gid = resp.data.companydocument_gid
                    $scope.companydocument_name = resp.data.companydocument_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        companydocument_gid: companydocument_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveCompanyDocument';
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
                    companydocument_gid: companydocument_gid
                }

                var url = 'api/MstApplication360/InactiveCompanyDocumentHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.companydocumentinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (companydocument_gid) {
            var params = {
                companydocument_gid: companydocument_gid
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
                            var url = 'api/MstApplication360/DeleteCompanyDocument';
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

        $scope.excelreport = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportMstCompanyReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')

                }

            });
        }

        }
})();

