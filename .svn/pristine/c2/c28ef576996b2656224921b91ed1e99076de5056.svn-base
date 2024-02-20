(function () {
    'use strict';

    angular
        .module('angle')
        .controller('checkerApprovalSummaryController', checkerApprovalSummaryController);

    checkerApprovalSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function checkerApprovalSummaryController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'checkerApprovalSummaryController';
        var i = 0;
        var j = 0;
        var deferralGidList = [];
        //$scope.loandata = [];
        var user_code;
        activate();
        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/deferral/CheckerApprovalSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deferral_data = resp.data.deferralSummaryDtls;
                // new code start  
                unlockUI();
                if ($scope.deferral_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.deferral_data.length;
                    if ($scope.deferral_data.length < 100) {
                        $scope.totalDisplayed = $scope.deferral_data.length;
                    }
                }
                    
            });

            var url = 'api/deferral/UserCode';
            SocketService.get(url).then(function (resp) {
                user_code = resp.data.user_code;
                //console.log($scope.UploadDocumentname);
                if (user_code == 'S0537' || user_code == 'S0562' || user_code == 'S0616' || user_code == 'S0448') {
                    //console.log('test');
                    $scope.user_status = "Y";
                }

            });

        }

        $scope.checkall = function (selected) {
            //console.log(selected);
            angular.forEach($scope.deferral_data, function (val) {
                val.checked = selected;
            });
        }

        $scope.bulkVerifyChecker = function () {
            angular.forEach($scope.deferral_data, function (val) {

                if (val.checked == true) {
                    var deferral_gid = val.deferral_gid;
                    deferralGidList.push(deferral_gid);
                    i = i + 1;
                }
            });
           
            if (i == 0) {
                Notify.alert('Select Atleast One Deferral!', 'warning');
                return false;
            }

            if ($scope.deferral_status == "PushBack") {
                //console.log($scope.deferral_status);
                var modalInstance = $modal.open({
                    templateUrl: '/updatecheckerremarks.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                    
                    $scope.RemarksUpdate = function () {

                        var params = {
                            deferral_gid: deferralGidList,
                            deferral_status: 'PushBack',
                            checker_remarks: $scope.checker_remarks
                        }
                        //console.log(params);
                        if ($scope.checker_remarks != undefined) {
                            //console.log('1');
                          
                            $modalInstance.close('closed');
                            var url = 'api/deferral/CheckerBulkVerify';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    //activate();
                                    $scope.deferral_status = '';
                                    Notify.alert('Checker Verification done Successfully!', 'success');
                                    
                                }
                                else {
                                    Notify.alert('Error While Checker Verification', 'warning');

                                }
                             
                                activate();
                            });
                            deferralGidList = [];
                            i = 0;
                            $('input[type="checkbox"]:checked').prop('checked', false);
                            activate();
                            $scope.deferral_status = '';
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert('Update Checker Remarks', 'warning');
                          
                            deferralGidList = [];
                            i = 0;
                            $('input[type="checkbox"]:checked').prop('checked', false);
                            activate();
                        }
                                             
                    }
                }
                $scope.deferral_status = '';
            }
            else if ($scope.deferral_status == "Close") {
                //console.log($scope.deferral_status);
                var modalInstance = $modal.open({
                    templateUrl: '/updateremarks.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    $scope.Update = function () {

                        var params = {
                            deferral_gid: deferralGidList,
                            deferral_status: 'Close',
                            approval_remarks: $scope.approval_remarks,
                            customer_remarks: $scope.customerremarks
                        }
                        //console.log(params);
                        if (($scope.approval_remarks != undefined) || ($scope.customerremarks != undefined)) {
                            //console.log('1');

                            $modalInstance.close('closed');
                            var url = 'api/deferral/CheckerBulkVerify';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    //activate();
                                    $scope.deferral_status = '';
                                    Notify.alert('Checker Verification done Successfully!', 'success');

                                }
                                else {
                                    Notify.alert('Error While Checker Verification', 'warning');

                                }

                                activate();
                            });
                            deferralGidList = [];
                            i = 0;
                            $('input[type="checkbox"]:checked').prop('checked', false);
                            activate();
                            $scope.deferral_status = '';
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert('Update Remarks', 'warning');

                            deferralGidList = [];
                            i = 0;
                            $('input[type="checkbox"]:checked').prop('checked', false);
                            activate();
                        }

                    }
                }
                $scope.deferral_status = '';
            }
            else {
               
                //console.log(i);
                var params = {
                    deferral_gid: deferralGidList,
                    deferral_status: $scope.deferral_status
                }
                //console.log(params);
                
                    var url = 'api/deferral/CheckerBulkVerify';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert('Checker Verification done Successfully!', 'success');
                            $scope.deferral_status = '';
                            activate();
                        }
                        else {
                            Notify.alert('Error While Checker Verification', 'warning');
                            $scope.deferral_status = '';
                        }
                        activate();
                    });
                    deferralGidList = [];
                    i = 0;                   
                    $('input[type="checkbox"]:checked').prop('checked', false);
                    activate();
                    $scope.deferral_status = '';
            }
           
        }


        $scope.export = function () {

            if ($scope.deferral_data==null) {
                Notify.alert('No Records to Export !', 'warning');
                return;
            }
            angular.forEach($scope.deferral_data, function (val) {

                if (val.checked == true) {
                    var deferral_gid = val.deferral_gid;
                    deferralGidList.push(deferral_gid);
                }
            });

            var params = {
                deferral_gid: deferralGidList
            }
            //console.log(params);
            lockUI();
            var url = 'api/deferral/CheckerExcelExport';

            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // //console.log(str);
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


        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.deferral_data != null) {

                if (pagecount < $scope.deferral_data.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.deferral_data.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.deferral_data.length;
                        Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.deferral_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

        $scope.verifyChecker = function (val) {
            $scope.deferral_gid = val;
            $scope.deferral_gid = localStorage.setItem('deferral_gid', val);
            $state.go('app.checkerApprovalView');
        }


    }
})();