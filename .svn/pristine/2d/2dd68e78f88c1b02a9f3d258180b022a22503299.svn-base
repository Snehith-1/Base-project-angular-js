(function () {
    'use strict';

    angular
        .module('vcx')
        .controller('ReadytoRelease', ReadytoRelease);

    ReadytoRelease.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];

    function ReadytoRelease($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'ReadytoRelease';
        $scope.tabledata = [];
        activate();

        function activate() {
            var url = apiManage.apiList['readytoRelease'].api;
            SocketService.get(url).then(function (resp) {
               
                angular.forEach(resp.data.tabledata, function (val) {                    
                    val.checked = '';
                    $scope.tabledata.push(val);
                    $scope.releasedata = resp.data.tabledata;
                    $scope.total = $scope.releasedata.length;
                });
                //$scope.tabledata = resp.data.tabledata;
            });

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
        }

      
        $scope.PopupreleaseDetails = function (val) {
            $scope.issue_id = val;
            var doc = document.getElementById('popupreleasedetails');
            doc.style.display = 'block';
        }

        $scope.checkall = function (selected) {
            angular.forEach($scope.tabledata, function (val) {
                val.checked = selected;
            });
        }

        $scope.document_cancelclick = function (val) {
            var val = { tmpuatdocument_gid: val }
            var url = apiManage.apiList['documentcancel'].api;
            SocketService.post(url, val).then(function (resp) {
                if (resp.data.status == true) {
                    var url = apiManage.apiList['tmpuatdocument'].api;
                    SocketService.get(url).then(function (resp) {
                        $scope.filename_list = resp.data.filename_list;
                    });
                   
                    Notify.alert('Document Deleted Successfully..!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert('Error Occured..!', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.back = function () {
            $state.go('app.releaseManagement');
        }

        // Document Upload //
        $scope.upload = function (val, val1, name) {

            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            $scope.uploadfrm = frm;
            var url = apiManage.apiList['uploaddocument'].api;
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;
                $("#addupload").val('');
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert('Document Uploaded Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert('File Format Not Supported!', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });

        }

        $scope.MakeRelease = function () {
            console.log($scope.release_date);
            var date = new Date();
            date.setFullYear($scope.release_date.getFullYear());
            date.setMonth($scope.release_date.getMonth());
            date.setDate($scope.release_date.getDate());

            var issueGidList = [];
                angular.forEach($scope.tabledata, function (val) {
                    if (val.checked == true) {
                        var issueGid = val.issuetracker_gid;
                        issueGidList.push(issueGid);
                    }
                });
                var params = {
                    issueGid: issueGidList,
                    StatusRemarks: $scope.ReleaseRemarks,
                    releaseStatus: $scope.releaseStatus,
                    TargetIssuDate: date,
                    DoneBy: $scope.DoneBy,
                    change_description: $scope.change_description,
                    impacted_module: $scope.impacted_module,
                    impacted_system: $scope.impacted_system,
                    reasonsfor_change: $scope.reasonsfor_change,
                    alternative_way: $scope.alternative_way,
                    resources: $scope.resources
                }
                 
                if (issueGidList == "")
                {
                    Notify.alert('Atleast Select One Issue to Create Release Plan!', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return
                }
                else {
                    lockUI();
                    var url = apiManage.apiList['MakeRelease'].api;
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.message == 'success') {
                            Notify.alert('Issues in \'Ready to Release\' Mode', {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert('Error Occured.', {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $state.go('app.releaseManagement');
                    });
                }
        }
    }

})();