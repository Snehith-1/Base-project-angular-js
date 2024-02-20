(function () {
    'use strict';
    
    angular
        .module('angle')
        .controller('SysMstEmployeeHRDocumentController', SysMstEmployeeHRDocumentController);

        SysMstEmployeeHRDocumentController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert','DownloaddocumentService'];

    function SysMstEmployeeHRDocumentController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog,SweetAlert,DownloaddocumentService ) {
        
        var vm = this;
        vm.title = 'SysMstEmployeeHRDocumentController';
        
        var lsemployee_gid = $location.search().employee_gid;
        $scope.employee_gid = lsemployee_gid;

        activate();
        function activate() {
            var url = 'api/SysMstHRDocument/UpdateExpiryDate';
            lockUI();
            var params = {
                employee_gid: $scope.employee_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
            }); 

            var url = 'api/SysMstHRDocument/GetSysHRDocumentDropDown';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrdocument_data = resp.data.hrdocument_list;
                unlockUI();
                
            }); 
            var url = 'api/ManageEmployee/GetHRDoclist';
            
            var params={
                employee_gid: $scope.employee_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
             
                    $scope.document_list = resp.data.hrdoc;
                    unlockUI();
            
            }); 
        }

        $scope.HRDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i < fi.files.length; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                    
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                }
                frm.append('document_name', $scope.cboHRDcument.hrdocument_name);
                frm.append('document_gid', $scope.cboHRDcument.hrdocument_gid);
                frm.append('employee_gid', $scope.employee_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/ManageEmployee/HRDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    
                    unlockUI();
                    if (resp.data.status == true) {
                        activate();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        activate();
                        Notify.alert(resp.data.message, {
                            
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    cboHRDcument = "";        
                    unlockUI();

                });
            }
            else {
                alert('Please select a file.')
            }
            activate();
            
        }


        $scope.download = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            } 
        }

        $scope.downloadallhr = function () {
            for (var i = 0; i < $scope.document_list.length; i++) {
                if ($scope.document_list[i].migration_flag == 'N') {
                    DownloaddocumentService.Downloaddocument($scope.document_list[i].hrdoc_path, $scope.document_list[i].hrdoc_name);
                }
                else {
                    DownloaddocumentService.OtherDownloaddocument($scope.document_list[i].hrdoc_path, $scope.document_list[i].hrdoc_name,"HRMigration");
                }
                
            }
        }

        $scope.proceedforesign = function (val1, val2, val3, migration_flag) {
            var params = {
                hrdoc_id: val1,
                file_name: val2,
                file_path: val3,
                migration_flag: migration_flag
            }
            var url = 'api/SysMstHRDocument/UploadDocumenttoDigio';
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
            });
        }
            
        $scope.esignstatusenquiry = function (val1, val2, val3) {
            var params = {
                hrdoc_id: val1,
                file_name: val2,
                file_path: val3 
            }
            var url = 'api/SysMstHRDocument/GetDocumentDetails';
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
            });
        }

        $scope.downloaddocfromdigio = function (val1, val2, val3) {
            var params = {
                hrdoc_id: val1,
                hrdoc_path: val2,
                hrdoc_name: val3
            }
            var url = 'api/SysMstHRDocument/DownloadDocfromDigio';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true)
                    $rootScope.$emit('downloadEvent', resp);
                else {
                    return resp;
                }
            });
        }

        $scope.deleteDocument = function (val) {
            var params = {
                hrdoc_id: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Document List ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/ManageEmployee/HRDocDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }
    } 
})();
