(function () {
    'use strict';

    angular
        .module('angle')
        .controller('DocDigitalSignatureSummaryController', DocDigitalSignatureSummaryController);

    DocDigitalSignatureSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce','cmnfunctionService'];

    function DocDigitalSignatureSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'DocDigitalSignatureSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/idasMstDigitalSignature/GetDigitalSignatureList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.digitalsignaturelist = resp.data.digitalsignaturelist
            });
        }

        $scope.addSignature = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addSignatureContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {

                $scope.close = function () {
                    modalInstance.close('closed');
                };
                var url = 'api/idasMstDigitalSignature/GetEmployeeList';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist
                });


                $scope.upload = function (val, val1, name) {
                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        return false;
                    }
                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('employee_gid', $scope.cboemployee_name.employee_gid);
                    frm.append('employee_name', $scope.cboemployee_name.employee_name);
                    frm.append('project_flag', "Default"); 
                    $scope.uploadfrm = frm;
                }

                $scope.signaturesubmit = function () {
                    var url = 'api/idasMstDigitalSignature/SignatureUpload';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            activate();
                            modalInstance.close('closed');
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                        }
                        $("#digitaldocupload").val('');
                    });
                }
            }
        }

        //$scope.viewsignature = function (digitalsignature_gid) {

        //    var modalInstance = $modal.open({
        //        templateUrl: '/viewSignatureContent.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });

        //    ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
        //    function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {

        //        var params = {
        //            digitalsignature_gid: digitalsignature_gid
        //        }
        //        var url = 'api/idasMstDigitalSignature/GetSignatureView';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.employee_name = resp.data.employee_name;
        //            $scope.document_name = resp.data.document_name;
        //            $scope.document_path = resp.data.document_path;
        //        });

        //        $scope.close = function () {
        //            modalInstance.close('closed');
        //        };

        //        $scope.downloads = function (val1, val2) {
        //            var phyPath = val1;
        //            var relPath = phyPath.split("EMS");
        //            var relpath1 = relPath[1].replace("\\", "/");
        //            var hosts = window.location.host;
        //            var prefix = location.protocol + "//";
        //            var str = prefix.concat(hosts, relpath1);
        //            var link = document.createElement("a");
        //            var name = val2.split(".")
        //            link.download = val2;
        //            var uri = str;
        //            link.href = uri;

        //            link.click();
        //        }
        //    }
        //}

        $scope.deletesignature = function (digitalsignature_gid) {
            lockUI();
            var params = {
                digitalsignature_gid: digitalsignature_gid
            }
            var url = 'api/idasMstDigitalSignature/DeleteSignature';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    var url = 'api/idasMstDigitalSignature/GetDigitalSignatureList';
                    SocketService.get(url).then(function (resp) {
                        $scope.digitalsignaturelist = resp.data.digitalsignaturelist
                    });

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
                unlockUI();
            });
        }
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = val2;
            var uri = str;
            link.href = uri;

            link.click();
        }
    }
})();
