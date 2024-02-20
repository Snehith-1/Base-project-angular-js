(function () {
    'use strict';

    angular
        .module('angle')
        .controller('IdasMstDocListController', IdasMstDocListController);

    IdasMstDocListController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function IdasMstDocListController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        $scope.title = 'IdasMstDocListController';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/IdasMstDocList/GetDocumentList";
            SocketService.get(url).then(function (resp) {
               
                $scope.documentation_list = resp.data.IDASDocument;

                if ($scope.documentation_list != null){
                 $scope.total = $scope.documentation_list.length;
                }
                unlockUI();
            });
        }
        document.getElementById('pagecount').onkeyup = function () {
            if($scope.pagecount==null){
             var el = document.getElementById('loadmore');
             el.style.backgroundColor = '#DCDCDC';  
            }
            else{
             var el = document.getElementById('loadmore');
             el.style.backgroundColor = '#ffa';
            }
        };

        $scope.exportdocumentdata = function () {
            lockUI();
            var url = 'api/IdasMstDocList/ExportDocument';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    

                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");                 
                    // var relpath1 = relPath[1].replace("\\", "/");             
                    // var hosts = window.location.host;                  
                    // var prefix = location.protocol + "//";                 
                    // var str = prefix.concat(hosts, relpath1);              
                    // var link = document.createElement("a");               
                    // var name = resp.data.lsname.split('.');                 
                    // link.download = name[0];                 
                    // var uri = str; 
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();
                }

            });
        }

        $scope.addDocument = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/addDocumentModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {



                $scope.documentformSubmit = function () {

                    if ($scope.comments == undefined)
                    {
                        $scope.comments=""
                    }

                    var params = {
                        
                        document_name: $scope.documentListName,
                        display_order: $scope.display_order,
                        comments: $scope.comments
                       
                    }
                    lockUI();
                    var url = "api/IdasMstDocList/PostCreationDocList";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

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
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.loadMore = function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            
            $scope.totalDisplayed += Number;
            unlockUI();
        };
        $scope.editDocument=function(val)
        {
            var modalInstance = $modal.open({
                templateUrl: '/editDocumentModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    doclist_gid: val
                }
                var url = 'api/IdasMstDocList/GetEditDocList';
                SocketService.getparams(url, params).then(function (resp) {
                   
                    $scope.documentNameEdit = resp.data.document_name;
                    $scope.documentCodeEdit = resp.data.document_code;
                    $scope.commentsEdit = resp.data.comments;
                    $scope.displayOrderEdit = resp.data.display_order;
                });
                $scope.close = function () {
                    $modalInstance.close('closed');
                };

                $scope.documentformUpdate = function () {
                    lockUI();
                    var params = {
                        document_name: $scope.documentNameEdit,
                        comments: $scope.commentsEdit,
                        display_order:$scope.displayOrderEdit,
                        documentlist_gid: val
                    }
                    var url = "api/IdasMstDocList/PostUpdateDoc";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            unlockUI();
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }

                    });
                }
            }
        }
        $scope.deleteDocument = function (val) {
            var params = {
                doclist_gid: val
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
                    var url = "api/IdasMstDocList/PostDocDelete";
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
        $scope.commondocumentupload = function (val, val1, name) {
            for (var i in $scope.documentname) {
            }
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_title', $scope.txtdocument_title);
            frm.append('sanction_gid', sanction_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/IdasTrnSanctionDoc/CommonDocUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $scope.txtdocument_title = '';
                $("#commonupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                    var url = 'api/IdasTrnSanctionDoc/GetCommonDoc';
                    var params = {
                        sanction_gid: sanction_gid
                    };
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.commondocument = resp.data.uploaddocument;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }

            });

        }

        $scope.uploadTemplate = function(val){
            $location.url('app/idasMstDocTemplate?documentlist_gid=' + val);
        }
        //$scope.commondocumentupload = function (val, val1, name) {

        //    alert('1');
           
        //    var item = {
        //        name: val[0].name,
        //        file: val[0]
        //    };
        //    var frm = new FormData();
        //    frm.append('fileupload', item.file);
        //    frm.append('file_name', item.name);
        //    //frm.append('document_name', $scope.documentname);
        //    frm.append('document_path', $scope.document_path);
        //    frm.append('doclist_gid', doclist_gid);
        //    frm.append('project_flag', "Default");
        //    $scope.uploadfrm = frm;
        //    var url = 'api/IdasMstDocList/CommonDocUpload';
        //    lockUI();
        //    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

        //        $scope.txtdocument_title = '';
        //        $("#commonupload").val('');
        //        if (resp.data.status == true) {
        //            unlockUI();
        //            Notify.alert('Document Uploaded Successfully..!!', 'success')

        //            //var url = 'api/IdasTrnSanctionDoc/GetCommonDoc';
        //            //var params = {
        //            //    sanction_gid: sanction_gid
        //            //};
        //            //SocketService.getparams(url, params).then(function (resp) {

        //            //    $scope.commondocument = resp.data.uploaddocument;

        //            //});
        //        }
        //        else {
        //            unlockUI();
        //            Notify.alert('File Format Not Supported!')

        //        }

        //    });

        //}
    }
})();
