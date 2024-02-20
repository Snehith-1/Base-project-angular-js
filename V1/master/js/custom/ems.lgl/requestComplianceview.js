(function () {
    'use strict';

    angular
        .module('angle')
        .controller('requestComplianceviewcontroller', requestComplianceviewcontroller);

    requestComplianceviewcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', 'DownloaddocumentService'];

    function requestComplianceviewcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'requestComplianceview360controller';

        activate();


        function activate() {

            lockUI();
            var url = "api/requestCompliance/requestcomplianceview"
            var param = {
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid')
            };

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.requestref_no = resp.data.requestref_no;
                $scope.requestcompliance_gid = resp.data.requestcompliance_gid;
                $scope.request_type = resp.data.request_type;
             //   $scope.deadline_date = resp.data.deadline_date;
                $scope.request_date = resp.data.request_date;             
                $scope.requested_by = resp.data.requested_by;
                $scope.designation_name = resp.data.designation_name;
                $scope.department_name = resp.data.department_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.employee_photo = resp.data.employee_photo;
                $scope.txtremarks = resp.data.remarks;
                $scope.list = resp.data.document_list;
                $scope.rejected_remarks = resp.data.rejected_remarks;
                $scope.completed_remarks = resp.data.completed_remarks;
                $scope.updated_date = resp.data.updated_date;
                $scope.updated_by = resp.data.updated_by;
                $scope.request_status = resp.data.request_status;
                if (resp.data.correctedfile_name!='---') 
                {
                    $scope.updated_download = true;
                }               
            });
            var url = "api/requestCompliance/querieslist"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.querieslog_list = resp.data.querydetails;
                unlockUI();
            });
            var url = "api/requestCompliance/getcorrecteddocument"

            SocketService.getparams(url, param).then(function (resp) {
                $scope.file_list = resp.data.Managecomplianuploaddoc_list;
                unlockUI();
            });
        }
        $scope.requestback = function () {
            $state.go("app.requestCompliancesummary");
        }
        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //console.log(str);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = name[0];
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadsdocument = function (val1, val2) {
            //var phyPath = val1;
            //console.log(val1);
            //console.log(val2);
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split('.');
            //link.download = name[0];
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };

          
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_type', $scope.document_type);
            frm.append('requestcompliance_gid', $scope.requestcompliance_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            console.log($scope.requestcompliance_gid);
            var url = 'api/requestCompliance/additionaldocupload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.upload_list;

                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.document_type = '';
                    Notify.alert('Document Uploaded Successfully', 'success')
                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported')

                }
                activate();
            });
          
        }
        $scope.delete_doc = function (val) {
            var params = { uploaddocument_gid: val };
            var url = 'api/requestCompliance/documentdelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.filename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.filename_list.splice(key, 1);
                        }
                    });
                    Notify.alert('Document Deleted Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert('Internal Error Occurred', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                activate();
            });
        }
        $scope.updateddoc_downloads = function (val1,val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            console.log(str);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.query = function (requestcompliance_gid) {

            var params = {
                requestcompliance_gid: requestcompliance_gid
            }
            console.log(requestcompliance_gid);
            var modalInstance = $modal.open({
                templateUrl: '/requestcompliancequery.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            //$scope.requestcompliance_gid = requestcompliance_gid;

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = "api/requestCompliance/compliancemanagement360"
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.requestref_no = resp.data.requestref_no;
                        $scope.request_type = resp.data.request_type;
                 
                    }
                });
                $scope.querysubmit = function () {
                    var lawyer = $('#lawyer :selected').text();
                    var params = {
                        firm_refno: $scope.firm_refno,
                        firm_name: $scope.firm_name,
                        lawyer: lawyer,
                        remarks: $scope.remarks,
                        lawfirm_gid: $scope.lawfirm_gid,
                        lawyerregister_gid: $scope.lawyer
                    }

                    var url = 'api/requestCompliance/querydetails';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Query Details Added Successfully', 'success')
                            activate();

                        }
                        else {
                            Notify.alert('Error Occurred While Query Details', 'warning')
                            activate();
                        }
                    });
                }
            }
        }
        $scope.sendclick = function () {
            var params = {
                requestcompliance_gid: localStorage.getItem('requestcompliance_gid'),
                queries: $scope.txtqueries
            }
         
            lockUI();
            var url = "api/requestCompliance/sendqueries";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/requestCompliance/querieslist"
                    var param = {
                        requestcompliance_gid: localStorage.getItem('requestcompliance_gid')
                    };
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.querieslog_list = resp.data.querydetails;
                        unlockUI();
                    });
                    Notify.alert('Queries sent Successfully', 'success');
                    $scope.txtqueries = "";
                }
                else {
                    Notify.alert('Error Occurred!', 'warning');
                }
            });
        }

        $scope.cancelclick = function () {
            $scope.txtqueries = "";
        }
    }

})();
