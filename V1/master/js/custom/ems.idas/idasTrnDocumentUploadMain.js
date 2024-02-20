(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnDocumentUploadMain', idasTrnDocumentUploadMain);

    idasTrnDocumentUploadMain.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnDocumentUploadMain($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        var vm = this;
        vm.title = 'idasTrnDocumentUploadMain';
        
        activate();
        function activate(){
            $scope.totalDisplayed=0;
            $scope.total = 0;
            $scope.DivFile=false;

            var url = 'api/IdasTrnDocumentUpload/FolderDtls';
            var params={
                parent_directorygid:'$'
            }
            SocketService.getparams(url,params).then(function (resp) {
               // $scope.DirectoryDtls_list = resp.data.DirectoryDtls;
                if(resp.data.status==true){
                    $scope.DirectoryDtls_list=resp.data.DirectoryDtls;
                    if ($scope.DirectoryDtls_list == null) {
                        $scope.total = 0;
                        $scope.totalDisplayed = 0;
                    }
                    else {
                        $scope.total = $scope.DirectoryDtls_list.length;
                        if ($scope.DirectoryDtls_list.length < 100) {
                            $scope.totalDisplayed = $scope.DirectoryDtls_list.length;
                        }
                    }
                   
                   }
                   else{
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                   }
              
            });
        }
        $scope.loadMore= function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
        
            var Number = parseInt(pagecount);
            // new code start
            if ($scope.DirectoryDtls_list != null) {
               
                if (pagecount < $scope.DirectoryDtls_list.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.DirectoryDtls_list.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.DirectoryDtls_list.length;
                        Notify.alert(" Total Summary " + $scope.DirectoryDtls_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.DirectoryDtls_list.length + " Records Only", "warning");
                    return;
                }
            }
           
            unlockUI();
        };
        document.getElementById('pagecount').onkeyup = function () {
            // console.log(document.getElementById('pagecount').value);
            if($scope.pagecount==null){
             var el = document.getElementById('loadmore');
             el.style.backgroundColor = '#DCDCDC';  
            }
            else{
             var el = document.getElementById('loadmore');
             el.style.backgroundColor = '#ffa';
            }
        };
        $scope.deleteDocument = function (gid) {
            var params = {
                fileupload_gid: gid
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
                   
                 var url = "api/IdasTrnDocumentUpload/Delete";
                  
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
        $scope.FileDetails = function () {
            var fi = document.getElementById('file');
           
            if(fi.value==""){
                Notify.alert('Kindly select the file');
                return;
            }
           
            
            if (fi.files.length > 0) {
               
                //document.getElementById('fp').innerHTML =
                //    'Total Files: <b>' + fi.files.length + '</b></br >';

                var frm = new FormData();
                
                frm.append('parent_directorygid', '$');
                frm.append('directory_type', 'File');
                frm.append('project_flag', "Default");
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                    
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fname, "");

                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        return false;
                    }
                    //document.getElementById('fp').innerHTML =
                    //    document.getElementById('fp').innerHTML + '<br /> ' + fname + 'Size ' + fsize;
                }

                var url =  'api/IdasTrnDocumentUpload/FileUpload';
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

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    unlockUI();
                });


            }
            else {
                alert('Please select a file.')
            }
        }

        // $scope.uploadallocation = function (val, val1, name) {
        //     var item = {
        //         name: val[0].name,
        //         file: val[0]
        //     };
        //     var frm = new FormData();
        //     frm.append('fileupload', item.file);
        //     frm.append('file_name', item.name);
        //     frm.append('parent_directorygid', '$');
        //     frm.append('directory_type', 'File');
        //      frm.append('project_flag', "Default");
        //     $scope.uploadfrm = frm;
        //     var url = 'api/IdasTrnDocumentUpload/FileUpload';
        //     lockUI();
        //     SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {


        //         $("#addupload").val('');
               
        //         if (resp.data.status == true) {
        //             unlockUI();
        //             Notify.alert('Document Uploaded Successfully..!!', 'success')

        //            activate();
        //         }
        //         else {
        //             unlockUI();
        //             Notify.alert('File Format Not Supported!')

        //         }

        //     });

        // }

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

        $scope.goChild=function(val){
            localStorage.setItem('parent_directorygid',val);
            $state.go('app.idasTrnDocumentUploadChild');

        }
        $scope.FileShow=function(){
            if($scope.DivFile==true){
                $scope.DivFile=false;
            }
            else{
                $scope.DivFile=true;
            }
        }
        $scope.PopupFolder=function(){
           
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.close = function () {
                    $modalInstance.close('closed');
                };
                 $scope.createFolder = function () {
                     var params = {
                        folder_name:$scope.folderName,
                        directory_type:'Folder',
                        parent_directorygid:'$'
                     }
                    
                     var url = 'api/IdasTrnDocumentUpload/CreateFolder';

                     SocketService.post(url, params).then(function (resp) {
                         if (resp.data.status == true) {
                            $modalInstance.close('closed');
                             Notify.alert(resp.data.message, 'success')
                             activate();

                         }
                         else {
                            Notify.alert('Error Occurred', 'warning')
                             activate();
                         }
                     });

                 }
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
                $scope.close = function () {
                    $modalInstance.close('closed');
                };
                $scope.renameFolder=Filerename;
                 $scope.renameSave = function () {
                     var params = {
                    
                        fileupload_gid:gid,
                        folder_name:$scope.renameFolder
                     }
                     if(type=="File"){
                        var url = 'api/IdasTrnDocumentUpload/RenameFile';
                     }
                     else{
                        var url = 'api/IdasTrnDocumentUpload/RenameFolder';
                     }
                    
                    

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
       
    }
})();
