(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sopdocumentmanagement', sopdocumentmanagement);

    sopdocumentmanagement.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function sopdocumentmanagement($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        $scope.title = 'sopdocumentmanagement';

        activate();

        function activate() {
         
            $scope.totalDisplayed = 100;
            lockUI();
            var url = "api/MstSOPDocumentUpload/GetSOPDocumentSummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sopdepartment_list = resp.data.sopdepartment_list;
                $scope.total = $scope.sopdepartment_list.length;
            });
            var url = "api/MstSOPDocumentUpload/GetITflag";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.department_flag = resp.data.department_flag;
                console.log(resp.data.department_flag)
            });
        }
        document.getElementById('pagecount').onkeyup = function () {
            if ($scope.pagecount == null) {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#DCDCDC';
            }
            else {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#ffa';
            }
        };
        $scope.loadMore = function (pagecount) {
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayed += Number;
            unlockUI();
        };
        $scope.viewdocument = function (sopdepartment_gid)
        {
            $location.url('app/MstDocumentDownload?sopdepartment_gid=' + sopdepartment_gid);
        }
        $scope.popFileShow = function () {

            var modalInstance = $modal.open({
                templateUrl: '/Documentuploadcontent.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.warning = false;
                var url = 'api/MstSOPDocumentUpload/GetSOPdepartment_list';
                SocketService.get(url).then(function (resp) {
                    $scope.sopdepartment_list = resp.data.sopdepartment_list;
                });
                $scope.uploadfile = function (val, val1, name) {
                    $scope.warning = false;
                    $scope.msg = '';
                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");
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
                    frm.append('sopdepartment_name', $scope.cbosop_department.sopdepartment_name);
                    frm.append('sopdepartment_gid', $scope.cbosop_department.sopdepartment_gid);
                    frm.append('sopdocument_name', $scope.txtsop_name);
                    frm.append('sop_code', $scope.txtsop_code);
                    frm.append('sop_versionno', $scope.txtsop_versionno);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
                  
                }
          
                $scope.submit=function()
                {
                    
                    if ($scope.uploadfrm == undefined || $scope.uploadfrm == null)
                    {
                        $scope.warning = true;
                        $scope.msg = "Kindly upload the document";
                    }
                    else {
                        $scope.warning = false;
                    var url = 'api/MstSOPDocumentUpload/PostSOPDocument';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.sopdepartment_list = resp.data.sopdepartment_list;
                            $("#addupload").val('');
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
                            Notify.alert('Error Occurred', 'warning')
                            activate();
                        }
                        unlockUI();
                    });
                    $modalInstance.close('closed');
                    }
                  
                }
                $modalInstance.close('closed');
                $scope.close = function () {
                    $modalInstance.close('closed');
                };
            }
        }
    }
})();
