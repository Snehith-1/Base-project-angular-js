(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editRequestcompliancecontroller', editRequestcompliancecontroller);

    editRequestcompliancecontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService','cmnfunctionService'];

    function editRequestcompliancecontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editRequestcompliancecontroller';

        activate();


        function activate() {
            $scope.others_title = false;
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.closed = true;
            };
            
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.requestcompliance_gid = localStorage.getItem('requestcompliance_gid');
            var url = 'api/requestCompliance/Getrequestcompliance';

            var param = {
                requestcompliance_gid: $scope.requestcompliance_gid
            };
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtrequestref_noedit = resp.data.requestref_no;
                $scope.txtrequest_dateedit = resp.data.request_date;
              
                $scope.cborequest_typeedit = resp.data.requesttype_gid;
                $scope.txtothers_title = resp.data.others_title;
                $scope.txtremarksedit = resp.data.remarks;
                $scope.txtdocument_typeedit = resp.data.document_type;             
                $scope.filename_list = resp.data.upload_list;
                if (resp.data.request_type == 'Others') {
                    $scope.others_title = true;
                }
          
                else {
                    $scope.others_title = false;
                }
                unlockUI();
                
            });
           
            var url = 'api/requestCompliance/getrequesttype2compliance';

            SocketService.get(url).then(function (resp) {
                $scope.requesttype_list = resp.data.requesttype_list;

            });

            var url = 'api/requestCompliance/reqtempdelete';
            SocketService.get(url).then(function (resp) {  
           });
        }
        $scope.onselectedrequsttype = function () {
            var url = 'api/requestCompliance/Geteditrequesttype';
            var param = {
                requesttype_gid: $scope.cborequest_typeedit
            };
            console.log(param);
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.request_type == 'Others') {
                    $scope.others_title = true;
                }
                else {
                    $scope.others_title = false;
                }
            });
        }
        $scope.requestupdate = function () {
           
            var url = 'api/requestCompliance/editmandatory_check';
            var param = {
                requestcompliance_gid: $scope.requestcompliance_gid
            };
          
            SocketService.getparams(url, param).then(function (resp) {
               
                if(resp.data.status == false)
                {
                    Notify.alert(resp.data.message, 'warning')
                }
                else {

            var params = {
                requestcompliance_gid: $scope.requestcompliance_gid,
                requestref_no: $scope.txtrequestref_noedit,              
                requesttype_gid: $scope.cborequest_typeedit,
                others_title:$scope.txtothers_title,
                remarks:$scope.txtremarksedit,
            }
         
            var url = 'api/requestCompliance/updateRequestcompliance';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.requestCompliancesummary');
                    Notify.alert(resp.data.message,'success')
                }

                else {
                    Notify.alert(resp.data.message, 'warning')
                }
                activate();
            });
                }
                
            });
        }
        $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            $scope.lawfirm_gid = localStorage.getItem('requestcompliance_gid');
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_type', $scope.document_type);
            frm.append('requestcompliance_gid', $scope.requestcompliance_gid);
            frm.append('project_flag', "Default");

            $scope.uploadfrm = frm;
            var url = 'api/requestCompliance/Edituploaddocument';

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.upload_list;
                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.document_type = '';
                    //  Notify.alert('Document Uploaded Successfully..!!', 'success')

                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported')

                }

            });
        }
        $scope.document_cancelclick = function (val, data) {
            var params = { uploaddocument_gid: val };
            var url = 'api/requestCompliance/documentdelete';
            lockUI()
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.filename_list, function (value, key) {
                        if (value.uploaddocument_gid == val) {
                            $scope.filename_list.splice(key, 1);
                        }
                    });
                    Notify.alert('Document Deleted Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI()
                    //activate();
                }
                else {
                    Notify.alert('Internal Error Occurred', {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }
        $scope.requestback = function () {
            var url = 'api/requestCompliance/tempdelete';          
            SocketService.get(url).then(function (resp) {
                
            });

            $state.go("app.requestCompliancesummary");
        }
    }
})();
