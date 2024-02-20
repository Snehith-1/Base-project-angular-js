(function () {
    'use strict';
    angular
           .module('angle')
           .controller('idasTrnDocumentTaggingViewController', idasTrnDocumentTaggingViewController);

    idasTrnDocumentTaggingViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnDocumentTaggingViewController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, $sce, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnDocumentTaggingViewController';
        var customer_gid = $location.search().customer_gid;
        activate();

        function activate() {
            var params = {
                customer_gid: customer_gid
            }

            var url = 'api/customer/Getcustomerupdatedetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerName = resp.data.customerNameedit;
                $scope.Customerurn = resp.data.customer_urnedit;
                $scope.mobileno = resp.data.mobileNoedit;
                $scope.contactno = resp.data.contactnoedit;
                $scope.clusterManager = resp.data.cluster_manager_name;
                $scope.relationshipmgmt = resp.data.relationshipmgmt_name;
                $scope.creditManager = resp.data.creditmanager_name;
                $scope.zonalHeadName = resp.data.zonal_name;
                $scope.businessHeadName = resp.data.businesshead_name;
                $scope.ContactPerson = resp.data.contactPersonedit;
                $scope.Address1 = resp.data.addressline1edit;
                $scope.Address2 = resp.data.addressline2edit;
                $scope.VerticalCode = resp.data.vertical_code;
                $scope.Constitution = resp.data.constitution_nameedit;
                $scope.emailaddress = resp.data.emailedit;
                $scope.country = resp.data.countryedit;
            });
            var url = 'api/IdasTrnDocumentUpload/sanction2customer';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.sanction_list = resp.data.sanction2customer_list;

            });

            var url = 'api/IdasTrnDocumentUpload/GetCadTeamFlag';

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadteam_flag = resp.data.cadteam_flag;
            });

            var params = {
                parent_directorygid: '$',
                customer2sanction_gid: '',
                customer_gid: customer_gid
            }
            var url = 'api/IdasTrnDocumentUpload/CustomerFolderDtls';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.DocumentDtls_list = resp.data.DirectoryDtls;

            });
            var params = {
                customer_gid: customer_gid
            }
            var url = 'api/IdasTrnDocumentUpload/WorkItemArchivalCustomerSummary';

            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.Documentcustomer_list = resp.data.MdlWorkItem;

            });

            var params = {
                parent_directorygid: '$',
                customer_gid: customer_gid
            }
            var url = 'api/IdasTrnDocumentUpload/CreditFolderDtls';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditDocumentDtls_list = resp.data.DirectoryDtls;

            });

            var url = 'api/IdasTrnDocumentUpload/GetCreditTeamFlag';

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditteam_flag = resp.data.creditteam_flag;
            });

            var params = {
                parent_directorygid: '$',
                customer_gid: customer_gid
            }

            var url = 'api/IdasTrnDocumentUpload/CreditOperationsFolderDtls';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditOperationsDocumentDtls_list = resp.data.DirectoryDtls;

            });

            var url = 'api/IdasTrnDocumentUpload/GetCreditOperationsTeamFlag';

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditoperationteam_flag = resp.data.creditoperationteam_flag;
            });
        }


        $scope.goChild = function (val) {
            localStorage.setItem('customer_gid', customer_gid);
            localStorage.setItem('customer2sanction_gid', '');
            localStorage.setItem('customerfileupload_gid', val);
            localStorage.setItem('parent_directorygid', val);
            localStorage.setItem('lspage', 'Customer');
            $state.go('app.idasTrnDocumentTaggingDocChild');

            //  $location.url('app/idasTrnDocumentTaggingDocChild?customer_gid='+customer_gid+'&?&customer2sanction_gid='+customer2sanction_gid+'&?&customerfileupload_gid='+val);
        }

        $scope.back = function () {
            $state.go('app.idasTrnDocumentTagging');
        }

        $scope.viewcustomersancdocument = function (customer2sanction_gid) {
            $location.url('app/idasTrnDocumentTaggingDocView?customer2sanction_gid=' + customer2sanction_gid + '&?&customer_gid=' + customer_gid);
        }
        $scope.RenameFolder = function (gid, type, Filerename) {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent1.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.folderrename = function (string1) {
                    if (string1.length > 128) {
                        $scope.message1 = "Allowed only  128 Characters";
                    }
                    else {
                        $scope.message1 = ""
                    }
                }
                $scope.close = function () {
                    $modalInstance.close('closed');
                };
                $scope.renameFolder = Filerename;
                $scope.renameSave = function () {
                    var params = {

                        customerfileupload_gid: gid,
                        folder_name: $scope.renameFolder,
                        type: type
                    }

                    var url = 'api/IdasTrnDocumentUpload/RenameCustomerFile';

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

        $scope.deleteDocument = function (gid) {
            var params = {
                customerfileupload_gid: gid
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

                    var url = "api/IdasTrnDocumentUpload/FileDelete";

                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            SweetAlert.swal(resp.data.message);
                            // Notify.alert(resp.data.message, {
                            //     status: 'danger',
                            //     pos: 'top-center',
                            //     timeout: 3000
                            // });
                            unlockUI();
                        }
                    });

                }

            });
        }

        $scope.PopupFolder = function () {

            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.foldername = function (string) {
                    if (string.length > 128) {
                        $scope.message = "Allowed only  128 Characters";
                    }
                    else {
                        $scope.message = ""
                    }
                }

                $scope.createFolder = function () {

                    var params = {
                        folder_name: $scope.folderName,
                        directory_type: 'Folder',
                        parent_directorygid: '$',
                        customer2sanction_gid: '',
                        customer_gid: customer_gid
                    }

                    var url = 'api/IdasTrnDocumentUpload/CreateCustomerFolder';

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

        $scope.downloadsdocument = function (val1, val2) {

            //var phyPath = val1;

            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;

            //link.click();

            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.FileShow = function () {
            if ($scope.DivFile == true) {
                $scope.DivFile = false;
            }
            else {
                $scope.DivFile = true;
            }
        }

        $scope.popFileShow = function () {

            var modalInstance = $modal.open({
                templateUrl: '/Documentuploadcontent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/IdasTrnDocumentUpload/GetDocumentLabellist';
                SocketService.get(url).then(function (resp) {
                    $scope.document_labellist = resp.data.CreditAdminDocumentLabelList;
                });
                $scope.uploadfile = function () {
                    var fi = document.getElementById('file');
                    
                    if (fi.files.length > 0) {
                        var frm = new FormData();

                        var documentlabel_name = $('#documentlabelname :selected').text();
                        frm.append('parent_directorygid', '$');
                        frm.append('directory_type', 'File');
                        frm.append('customer_gid', customer_gid);
                        frm.append('customer2sanction_gid', '');
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

                        var url = 'api/IdasTrnDocumentUpload/CustomerFileUpload';
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
                                unlockUI();
                                $modalInstance.close('closed');
                                activate();

                            }
                            else {
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                })
                                activate();
                            }
                            unlockUI();
                        });


                    }
                    else {
                        alert('Please select a file.')
                    }
                    $modalInstance.close('closed');
                }
                $scope.close = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

        // Credit Document File Upload 

        $scope.CreditpopFileShow = function () {

            var modalInstance = $modal.open({
                templateUrl: '/Documentuploadcontent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/IdasTrnDocumentUpload/GetDocumentLabellist';
                SocketService.get(url).then(function (resp) {
                    $scope.document_labellist = resp.data.CreditUnderwritingDocumentLabelList;
                });

                // credit Document Upload

                $scope.uploadfile = function () {
                    var fi = document.getElementById('file');
                    if (fi.value == "") {
                        Notify.alert('Kindly select the file','warning');
                        return;
                    }
                    
                    if (fi.files.length > 0) {
                        var frm = new FormData();

                        var documentlabel_name = $('#documentlabelname :selected').text();

                        frm.append('parent_directorygid', '$');
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

                        var url = 'api/IdasTrnDocumentUpload/CreditFileUpload';
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

        $scope.CreditFolder = function () {

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
                        parent_directorygid: '$',
                        customer_gid: customer_gid
                    }

                    var url = 'api/IdasTrnDocumentUpload/CreateCreditFolder';

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

        $scope.RenameCreditFolder = function (gid, type, Filerename) {
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
                        creditfileupload_gid: gid,
                        folder_name: $scope.renameFolder,
                        type: type
                    }
                    var url = 'api/IdasTrnDocumentUpload/RenameCreditFile';

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

        $scope.DeleteCreditDocument = function (gid) {
            var params = {
                creditfileupload_gid: gid
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

                    var url = "api/IdasTrnDocumentUpload/CreditFileDelete";

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

        $scope.downloadcreditdocument = function (val1, val2) {

            //var phyPath = val1;

            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;

            //link.click();

            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.goCreditChild = function (val) {
            localStorage.setItem('customer_gid', customer_gid);
            localStorage.setItem('creditfileupload_gid', val);
            localStorage.setItem('parent_directorygid', val);

            $state.go('app.idasTrnDocumentTaggingCreditChild');
        }

        // Credit Operations Document File Upload 

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

                // Credit Operations Document Upload

                $scope.uploadfile = function () {
                    var fi = document.getElementById('file');
                    if (fi.value == "") {
                        Notify.alert('Kindly select the file','warning');
                        return;
                    }
                    
                    if (fi.files.length > 0) {
                        var frm = new FormData();

                        var documentlabel_name = $('#documentlabelname :selected').text();

                        frm.append('parent_directorygid', '$');
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

        // Credit Operations Folder Creation

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
                        parent_directorygid: '$',
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
                            Notify.alert(resp.data.message, 'warning')
                            activate();
                        }
                    });

                }
                $scope.close = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        // Credit Operations Folder Rename

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

        // Delete Credit Operations Document

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

        // Download Credit Operations Document

        $scope.downloadcreditoperationsdocument = function (val1, val2) {

            //var phyPath = val1;

            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;

            //link.click();

            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.goCreditOperationsChild = function (val) {
            localStorage.setItem('customer_gid', customer_gid);
            localStorage.setItem('creditoperationsfileupload_gid', val);
            localStorage.setItem('parent_directorygid', val);

            $state.go('app.idasTrnDocumentTaggingCreditOperationsChild');
        }
    }
})();
