(function () {
    'use strict';

    angular
        .module('angle')
        .controller('misDataimportcontroller', misDataimportcontroller);

    misDataimportcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function misDataimportcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'misDataimportcontroller';

        activate();

        function activate() {
            lockUI();
            var url = "api/misDataimport/Getmisdata";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.imported_list = resp.data.imported_list;            
                $scope.show = true;
            });
        }
            $scope.openpanel=function() {
           
            $scope.show = false;
            $scope.options = true;
        }
        $scope.cancel = function () {
            
            $scope.show = true;
            $scope.options = false;
            $("#addupload").val('');

        }
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };

            
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);  
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
          
        }
        $scope.handleFile = function () {
            var url = 'api/misDataimport/mistempdataupload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.options = false;
                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });              
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                activate();
            });
        }
        $scope.viewloandetails = function (val) {
            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            $state.go('app.customer2misdata')
        }

        $scope.processdata = function (misdocumentimport_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/processdata.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    misdocumentimport_gid: misdocumentimport_gid
                }
                console.log(params);
                var url = 'api/misDataimport/Getimporteddata';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.file_name = resp.data.file_name;
                    $scope.imported_date = resp.data.imported_date

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.process = function () {
                    var params = {
                        misdocumentimport_gid: misdocumentimport_gid
                    }
                   
                    var url = 'api/misDataimport/processdata';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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

      
    }
})();
