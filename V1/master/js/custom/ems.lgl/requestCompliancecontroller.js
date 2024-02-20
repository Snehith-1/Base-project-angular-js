(function () {
    'use strict';
    angular
    .module('angle')
    .controller('requestCompliancecontroller', requestCompliancecontroller);

    requestCompliancecontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', 'DownloaddocumentService','cmnfunctionService'];

    function requestCompliancecontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'requestCompliancecontroller';

        activate();


        function activate() {
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

               var date = new Date(),
              mnth = ("0" + (date.getMonth() + 1)).slice(-2),
              day = ("0" + date.getDate()).slice(-2);
               $scope.txtrequest_date=[date.getFullYear(), mnth, day].join("-");
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.others_title = false;
            var url = 'api/requestCompliance/tempdelete';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/requestCompliance/getrequesttype2compliance';

            SocketService.get(url).then(function (resp) {
                $scope.requesttype_list = resp.data.requesttype_list;
                console.log(resp.data.requesttype_list);
            });

           var url = 'api/requestCompliance/reqtempdelete';
           SocketService.get(url).then(function (resp) {  
          });
        }
        $scope.onselectedrequsttype=function()
        {
             if($scope.cborequest_type.request_type=='Others')
            {
                 $scope.others_title = true;
            }
            
            else
            {
                $scope.others_title = false;
            }
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
            
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_type',$scope.document_type);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;             
           
            var url = 'api/requestCompliance/Uploaddocument';

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.upload_list;

                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.document_type = '';    
                    Notify.alert('Document Uploaded Successfully..!!', 'success')
                  
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }

            });

        }
        $scope.document_cancelclick = function (val, data) {
            var params = { uploaddocument_gid: val };
            var url = 'api/requestCompliance/documentdelete';
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
                    activate();
                }
                else {
                    Notify.alert('Internal Error Occurred!', {
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
        $scope.requestSubmit = function () {
            if ($scope.cborequest_type.request_type == 'Others')
            {
                if(($scope.txttitle=='')||($scope.txttitle==undefined))
                {
                    Notify.alert('Kindly Enter Others Title', 'warning')
                }
                else {


                    var url = 'api/requestCompliance/mandatory_check';
                    SocketService.get(url).then(function (resp) {
                        console.log(resp.data.status)
                        if (resp.data.status == false) {
                            Notify.alert(resp.data.message, 'warning')
                        }
                        else {


                            var params = {

                                request_type: $scope.cborequest_type.request_type,
                                requesttype_gid: $scope.cborequest_type.requesttype_gid,
                                others_title: $scope.txttitle,
                                // deadline_date: $scope.txtdeadlinedate,
                                remarks: $scope.txtremarks,

                            }

                            var url = 'api/requestCompliance/requestcompliance';
                            lockUI()
                            SocketService.post(url, params).then(function (resp) {

                                if (resp.data.status == true) {
                                    unlockUI()

                                    $state.go('app.requestCompliancesummary');
                                    Notify.alert('Compliance Created Successfully', 'success')
                                }
                                else {
                                    unlockUI();
                                    Notify.alert('Error While Creating request compliance')
                                }
                                activate();
                            });
                        }

                    });
                }
            }
            else {

            
            var url = 'api/requestCompliance/mandatory_check';
            SocketService.get(url).then(function (resp) {
                console.log(resp.data.status)
                if(resp.data.status == false)
                {
                    Notify.alert(resp.data.message, 'warning')
                }
                else {


            var params = {
             
                request_type: $scope.cborequest_type.request_type,
                requesttype_gid: $scope.cborequest_type.requesttype_gid,
                others_title:$scope.txttitle,
               // deadline_date: $scope.txtdeadlinedate,
                remarks: $scope.txtremarks,
                
            }

            var url = 'api/requestCompliance/requestcompliance';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
          
                if (resp.data.status == true) {
                    unlockUI()
                  
                    $state.go('app.requestCompliancesummary');
                    Notify.alert('Compliance Created Successfully', 'success')
                }
                else {
                    unlockUI();
                    Notify.alert('Error While Creating request compliance')
                }
                activate();
            });
                }

            });
            }
        }
          
    }
})();