(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnDocumentTaggingCreditOperationsChildController', idasTrnDocumentTaggingCreditOperationsChildController);

    idasTrnDocumentTaggingCreditOperationsChildController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnDocumentTaggingCreditOperationsChildController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        var vm = this;
        vm.title = 'idasTrnDocumentTaggingCreditOperationsChildController';

        activate();
        var customer_gid;
        var creditoperationsfileupload_gid;
        var parent_directorygid;

        function activate() {
            parent_directorygid = localStorage.getItem('parent_directorygid');
            customer_gid = localStorage.getItem('customer_gid');
            creditoperationsfileupload_gid = localStorage.getItem('creditoperationsfileupload_gid');

            var params = {
                creditoperationsfileupload_gid: creditoperationsfileupload_gid
            }

            var url = "api/IdasTrnDocumentUpload/CreditOperationsFilesBreadCrumb";


            SocketService.getparams(url, params).then(function (resp) {
                $scope.BreadCrumb_list = resp.data.FolderDtls;
            });

            var params = {
                parent_directorygid: creditoperationsfileupload_gid,
                customer_gid: customer_gid
            }
            var url = 'api/IdasTrnDocumentUpload/CreditOperationsFolderDtls';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditOperationsDocumentDtls_list = resp.data.DirectoryDtls;

            });

            var params = {
                customer_gid: customer_gid
            }
            var url = 'api/customer/Getcustomerdetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerName = resp.data.customerNameedit;
                $scope.Customerurn = resp.data.customer_urnedit;
            });

            var url = 'api/IdasTrnDocumentUpload/GetCreditOperationsTeamFlag';

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoperationteam_flag = resp.data.creditoperationteam_flag;
            });
        }

        $scope.goCreditOperationsChild = function (val) {
            localStorage.setItem('customer_gid', customer_gid);
            localStorage.setItem('creditoperationsfileupload_gid', val);

            activate();
        }

        $scope.home = function () {
            $location.url('app/idasTrnDocumentTaggingView?customer_gid=' + customer_gid);
        }

        // Credit Document File Upload 

        $scope.CreditOperationspopFileShow = function () {

            var modalInstance = $modal.open({
                templateUrl: '/Documentuploadcontent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/IdasTrnDocumentUpload/GetDocumentLabellist';
                SocketService.get(url).then(function (resp) {
                    $scope.document_labellist = resp.data.CreditOperationsDocumentLabelList;
                });

                // Credit Document Upload

                $scope.uploadfile = function () {
                    var fi = document.getElementById('file');
                    if (fi.value == "") {
                        Notify.alert('Kindly select the file','warning');
                        return;
                    }
                    
                    if (fi.files.length > 0) {
                        var frm = new FormData();

                        var documentlabel_name = $('#documentlabelname :selected').text();

                        frm.append('parent_directorygid', creditoperationsfileupload_gid);
                        frm.append('directory_type', 'File');
                        frm.append('customer_gid', customer_gid);
                        frm.append('documentlabel_gid', $scope.documentlabel_gid.documentlabel_gid);
                        frm.append('documentlabel_name', documentlabel_name);
                        frm.append('remarks', $scope.remarks);
                        frm.append('project_flag', "Default");
                        for (var i = 0; i <= fi.files.length - 1; i++) {
                            frm.append(fi.files[i].name, fi.files[i]);
                           
                            $scope.uploadfrm = frm;
                            var fname = fi.files.item(i).name;
                            var fsize = fi.files.item(i).size;
                            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fname, "");

                            if (IsValidExtension == false) {
                                alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                return false;
                            }
                        }

                        var url = 'api/IdasTrnDocumentUpload/CreditOperationsFileUpload';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $("#file").val('');
                                activate();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            unlockUI();
                        });
                    }
                    else {
                        alert('Please select a file.')
                    }
                }

                $scope.close = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        // Credit Folder Creation

        $scope.CreditOperationsFolder = function () {

            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.createFolder = function () {

                    var params = {
                        folder_name: $scope.folderName,
                        directory_type: 'Folder',
                        parent_directorygid: creditoperationsfileupload_gid,
                        customer_gid: customer_gid
                    }

                    var url = 'api/IdasTrnDocumentUpload/CreateCreditOperationsFolder';

                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert('Error Occurred', 'warning')
                            activate();
                        }
                    });

                }
                $scope.close = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        // Credit Folder Rename

        $scope.RenameCreditOperationsFolder = function (gid, type, Filerename) {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent1.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.close = function () {
                    $modalInstance.close('closed');
                };
                $scope.renameFolder = Filerename;
                $scope.renameSave = function () {
                    var params = {
                        creditoperationsfileupload_gid: gid,
                        folder_name: $scope.renameFolder,
                        type: type
                    }
                    var url = 'api/IdasTrnDocumentUpload/RenameCreditOperationsFile';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning')
                            activate();
                        }
                    });

                }
            }
        }

        // Delete Credit Document

        $scope.DeleteCreditOperationsDocument = function (gid) {
            var params = {
                creditoperationsfileupload_gid: gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete this Folder/File ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();

                    var url = "api/IdasTrnDocumentUpload/CreditOperationsFileDelete";

                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            SweetAlert.swal(resp.data.message);
                            unlockUI();
                        }
                    });
                }
            });
        }

        // Download Credit Document

        $scope.downloadcreditoperationsdocument = function (val1, val2) {

            // var phyPath = val1;

            // var relPath = phyPath.split("EMS");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = location.protocol + "//";
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // var name = val2.split(".")
            // link.download = val2;
            // var uri = str;
            // link.href = uri;

            // link.click();

            DownloaddocumentService.Downloaddocument(val1, val2);

        }
    }
})();