(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnDocumentTaggingDocChildController', idasTrnDocumentTaggingDocChildController);

        idasTrnDocumentTaggingDocChildController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnDocumentTaggingDocChildController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        var vm = this;
        vm.title = 'idasTrnDocumentTaggingDocChildController';
        // var customer2sanction_gid=$location.search().customer2sanction_gid;
        // var customer_gid=$location.search().customer_gid;
        // var customerfileupload_gid=$location.search().customerfileupload_gid;
        activate();
        var customer2sanction_gid;
        var customer_gid;
        var customerfileupload_gid;
        var parent_directorygid;

        function activate(){
            parent_directorygid=localStorage.getItem('parent_directorygid');
             customer2sanction_gid=localStorage.getItem('customer2sanction_gid');
             customer_gid=localStorage.getItem('customer_gid');
             customerfileupload_gid=localStorage.getItem('customerfileupload_gid');
             $scope.lspage = localStorage.getItem('lspage');
            
           var params={
                customerfileupload_gid:customerfileupload_gid
            }

            var url="api/IdasTrnDocumentUpload/CustomerFilesBreadCrumb";
           
            
            SocketService.getparams(url,params).then(function (resp) {
                    $scope.BreadCrumb_list = resp.data.FolderDtls;
            });
            var params={
                parent_directorygid:customerfileupload_gid,
                customer2sanction_gid: customer2sanction_gid,
                customer_gid:customer_gid
            }
            var url = 'api/IdasTrnDocumentUpload/CustomerFolderDtls';
            SocketService.getparams(url,params).then(function (resp) {
                if(resp.data.status==true){
                    $scope.DocumentDtls_list = resp.data.DirectoryDtls;
                }
                else{
                    $scope.DocumentDtls_list = null;
                }
               
              
            });

            var url = 'api/IdasTrnDocumentUpload/GetCadTeamFlag';
          
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadteam_flag = resp.data.cadteam_flag;
            });

            var params = {
                customer_gid: customer_gid
            }
           
            var url = 'api/customer/Getcustomerdetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerName = resp.data.customerNameedit;
                $scope.Customerurn = resp.data.customer_urnedit;
            });
        }

        
        // $scope.goChild=function(val){
        //     console.log(val);
        //     $location.url('app/idasTrnDocumentTaggingDocChild?customer_gid='+customer_gid+'&?&customer2sanction_gid='+customer2sanction_gid+'&?&customerfileupload_gid='+val);
        // }

        $scope.goChild=function(val){
            localStorage.setItem('customer_gid',customer_gid);
            localStorage.setItem('customer2sanction_gid',customer2sanction_gid);
            localStorage.setItem('customerfileupload_gid',val);
            activate();

          //  $location.url('app/idasTrnDocumentTaggingDocChild?customer_gid='+customer_gid+'&?&customer2sanction_gid='+customer2sanction_gid+'&?&customerfileupload_gid='+val);
        }
        $scope.back=function()
        {
            if ($scope.lspage=="Sanction")
            {
                $location.url('app/idasTrnDocumentTaggingDocView?customer_gid='+customer_gid+'&?&customer2sanction_gid='+customer2sanction_gid);
            }
            if ($scope.lspage == "Customer")
            {
                $location.url('app/idasTrnDocumentTaggingView?customer_gid='+customer_gid+'&?&customer2sanction_gid='+'');
            }
            
        }

        /* $scope.back = function () {

            $location.url('app/idasTrnDocumentTaggingDocView?customer_gid='+customer_gid+'&?&customer2sanction_gid='+customer2sanction_gid);
        }
 */
        $scope.home = function () {
            if ($scope.lspage=="Sanction")
            {
                $location.url('app/idasTrnDocumentTaggingDocView?customer_gid='+customer_gid+'&?&customer2sanction_gid='+customer2sanction_gid);
            }
            if ($scope.lspage == "Customer")
            {
                $location.url('app/idasTrnDocumentTaggingView?customer_gid='+customer_gid+'&?&customer2sanction_gid='+'');
            }
           
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
                        parent_directorygid:customerfileupload_gid,
                        customer2sanction_gid:customer2sanction_gid,
                        customer_gid:customer_gid,
                        customerfileupload_gid:customerfileupload_gid
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
               
                $scope.close = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/IdasTrnDocumentUpload/GetDocumentLabellist';
                SocketService.get(url).then(function (resp) {
                    $scope.document_labellist = resp.data.CreditAdminDocumentLabelList;
                });
                $scope.uploadfile = function () {
                    var fi = document.getElementById('file');
                    
                    if (fi.files.length > 0) {
                        var frm = new FormData();
                        var documentlabel_name = $('#documentlabelname :selected').text();
                        frm.append('parent_directorygid', customerfileupload_gid);
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
                                $modalInstance.close('closed');
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                })
                                 activate();
                             }
                             $modalInstance.close('closed');
                            unlockUI();
                        });
        
        
                    }
                    else {
                        alert('Please select a file.')
                    }
                    $modalInstance.close('closed');
                }
               
            }
        } 
       
    }
})();
