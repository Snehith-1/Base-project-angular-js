(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnServiceRequestAdd', osdTrnServiceRequestAdd);

    osdTrnServiceRequestAdd.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];


    function osdTrnServiceRequestAdd($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnServiceRequestAdd';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var lspage = searchObject.lspage;
        activate();

        function activate() {
           

            var url = 'api/OsdTrnServiceRequest/GetEmployees';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
                unlockUI();
            });

            var url = 'api/OsdTrnServiceRequest/tempdelete';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/OsdMstDepartmentManagement/GetActivaterequestdept';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deptmasterlist = resp.data.deptlist;

                unlockUI();
            });
        }
     

        $scope.titlename = function (string) {
            if (string.length >= 150) {
                $scope.message = "Maximum 150 characters Length";
            }
            else {
                $scope.message = ""
            }
        }
        $scope.onselectdept = function (department_gid) {
            var params = {
                department_gid: department_gid.department_gid
            }         
            var url = 'api/OsdMstDepartmentManagement/GetDeptActivityList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.activity_list = resp.data.activitydtl;
            });
        }

        $scope.submitserviceRequest = function () {
            // var editor = new FroalaEditor('div#froala-editor', {}, function () { })
            // $scope.viewFroala = editor.html.get();
            // var remarks = $($scope.viewFroala).text();
            // console.log('s' + $scope.viewFroala);
                      
            // if ((editor.core.isEmpty())) {
            //     Notify.alert('Enter Request Description', {
            //         status: 'warning',
            //         pos: 'top-center',
            //         timeout: 3000
            //     });
            // }
            // else {
                var params = {
                    activitymaster_gid: $scope.cboactivity.activitymaster_gid,
                    activity_name: $scope.cboactivity.activity_name,
                    request_title: $scope.request_title,
                    request_description: $scope.request_description,
                    // request_description: $scope.viewFroala,
                    tagmemberdtl: $scope.tagmember_list,
                    department_gid: $scope.cbodepartment.department_gid,
                    department_name: $scope.cbodepartment.department_name
                }

                lockUI();
                var url = "api/OsdTrnServiceRequest/PostServiceRequest"
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.osdTrnServiceRequestSummary');
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
            // }
              

            
          
        }


        $scope.ServiceRequestDocumentUpload = function () {
                       
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                //document.getElementById('fp').innerHTML =
                //    'Total Files: <b>' + fi.files + '</b></br >';
                
                var frm = new FormData();
                //frm.append('allocationdtl_gid', 'RSK001')
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                  /*  frm.append('project_flag', "Default");*/
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    //document.getElementById('fp').innerHTML =
                    //    document.getElementById('fp').innerHTML + '<br /> ' + fname;
                        
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fname, "Default");

                        if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                return false;
                            }
                }
                frm.append('project_flag', "Default");
                lockUI(); 
                var url = 'api/OsdTrnServiceRequest/RequestDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    $scope.upload_list = resp.data.upload_list;
                    $scope.lblfilename = resp.data.filename;
                    $scope.lblfilepath = resp.data.filepath;
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
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    unlockUI();
                });
            }
            else {
                Notify.alert('Please select a file.', 'info')
            }
        }

        $scope.uploaddocumentcancel = function (tmp_documentGid) {
            lockUI();
            var params = {
                tmp_documentGid: tmp_documentGid
            }
            var url = 'api/OsdTrnServiceRequest/GettmpDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
                $scope.lblfilename = resp.data.filename;
                $scope.lblfilepath = resp.data.filepath;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
        }

        //$scope.downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadall = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }

       

        // Back Code Starts
        $scope.cancel = function () {
            if (lspage == "myrequest") {
                $location.url('app/osdTrnServiceRequestSummary');
            }
            else if (lspage == "taggedrequest") {
                $location.url('app/osdTrnTaggedRequestSummary');
            }
            else if (lspage == "forwardrequest") {
                $location.url('app/osdTrnForwardTransferSummary');
            }
            else if (lspage == "reopenactivity") {
                $location.url('app/osdTrnReopenRequestSummary');
            }
            else if (lspage == "rejectrequest") {
                $location.url('app/osdTrnReopenRequestSummary');
            }
            else if (lspage == "closerequest") {
                $location.url('app/osdTrnCloseRequestSummary');
            }
        }
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
      
  
    }
})();
