(function () {
    'use strict';
    angular
           .module('angle')
           .controller('idasTrnDocumentTaggingDocViewController', idasTrnDocumentTaggingDocViewController);

    idasTrnDocumentTaggingDocViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnDocumentTaggingDocViewController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, $sce, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnDocumentTaggingDocViewController';
        var customer2sanction_gid=$location.search().customer2sanction_gid;
        var customer_gid=$location.search().customer_gid;
        
        activate();
      
        function activate() {
            var params = {
                customer2sanction_gid: customer2sanction_gid
            }
           var url = 'api/IdasTrnDocumentUpload/GetDocumentsofSanction';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.document_list = resp.data.MOM_DocumentList;
            });

            var params = {
                customer_gid: customer_gid
            }
           
            var url = 'api/customer/Getcustomerdetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerName = resp.data.customerNameedit;
                $scope.Customerurn = resp.data.customer_urnedit;
            });

            var params = {
                sanction_gid: customer2sanction_gid
            }
           
            var url = 'api/IdasMstSanction/SanctionDtlsEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionref_no = resp.data.sanction_refno;
                $scope.sanction_date = resp.data.sanction_date;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.entity  = resp.data.entity ;
                $scope.buyerconfirmation_letter = resp.data.status_ofBAL;
                $scope.colanding_status = resp.data.colanding_status;
                $scope.sanction_type = resp.data.sanction_type;
                $scope.ccapproved_by = resp.data.ccapproved_by;
                $scope.ccapproved_date = resp.data.ccapprovedDate;
                $scope.facility_type = resp.data.facility_type;
                $scope.esdeclaration_status = resp.data.esdeclaration_status;
               
            });

            var params = {
                sanction_gid: customer2sanction_gid
            }

            var url = 'api/IdasMstSanction/GetBuyerinfoEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.buyer_list = resp.data.buyer_list;

            });

            var params = {
                parent_directorygid:'$',
                customer2sanction_gid: customer2sanction_gid,
                customer_gid:customer_gid
            }
           var url = 'api/IdasTrnDocumentUpload/CustomerFolderDtls';
          
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.DocumentDtls_list = resp.data.DirectoryDtls;
               
            });
           
            var url = 'api/IdasTrnDocumentUpload/GetCadTeamFlag';
          
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadteam_flag = resp.data.cadteam_flag;
            });
                    
           /*  var params={
                customerfileupload_gid:$scope.parent_directorygid
            }

            var url="api/IdasTrnDocumentUpload/CustomerFilesBreadCrumb";
           
            
            SocketService.getparams(url,params).then(function (resp) {
                $scope.BreadCrumb_list = resp.data.FolderDtls;
            }); */

            var params = {
                customer_gid: customer_gid,
                customer2sanction_gid: customer2sanction_gid,
            }
            var url = 'api/IdasTrnDocumentUpload/WorkItemArchivalSpecificSummary';

            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.Documentcustomer_list = resp.data.MdlWorkItem;

            });
            
        }

          
        $scope.goChild=function(val){
            localStorage.setItem('customer_gid',customer_gid);
            localStorage.setItem('customer2sanction_gid',customer2sanction_gid);
            localStorage.setItem('customerfileupload_gid',val);
            localStorage.setItem('parent_directorygid',val);
            localStorage.setItem('lspage','Sanction');
            $state.go('app.idasTrnDocumentTaggingDocChild');

          //  $location.url('app/idasTrnDocumentTaggingDocChild?customer_gid='+customer_gid+'&?&customer2sanction_gid='+customer2sanction_gid+'&?&customerfileupload_gid='+val);
        }
       
        $scope.back = function () {
            $location.url('app/idasTrnDocumentTaggingView?customer_gid='+customer_gid);
        }

      
      
        $scope.RenameFolder=function(gid,type,Filerename){
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
                $scope.renameFolder=Filerename;
                 $scope.renameSave = function () {
                     var params = {
                    
                        customerfileupload_gid:gid,
                        folder_name:$scope.renameFolder,
                        type:type
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
        
        $scope.PopupFolder=function(){
           
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
                        folder_name:$scope.folderName,
                        directory_type:'Folder',
                        parent_directorygid:'$',
                        customer2sanction_gid: customer2sanction_gid,
                        customer_gid:customer_gid
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
        $scope.FileShow=function(){
            if($scope.DivFile==true){
                $scope.DivFile=false;
            }
            else{
                $scope.DivFile=true;
            }
        }

        $scope.popFileShow=function(){
           
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
                        frm.append('customer_gid',customer_gid);
                        frm.append('customer2sanction_gid',customer2sanction_gid);
                        frm.append('documentlabel_gid',$scope.documentlabel_gid.documentlabel_gid);
                        frm.append('documentlabel_name',documentlabel_name);
                        frm.append('remarks',$scope.remarks);
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
                                });
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

    }
})();
