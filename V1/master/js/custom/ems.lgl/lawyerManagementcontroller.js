(function () {
    'use strict';
    angular
        .module('angle')
        .controller('lawyerManagementcontroller', lawyerManagementcontroller);
    lawyerManagementcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];
    function lawyerManagementcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'lawyerManagementcontroller';
        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/registerLawyer/lawyerdetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lawyer_data = resp.data.lawyer_list;
              
                $scope.total = $scope.lawyer_data.length;
            });
        }
        document.getElementById('pagecount').onkeyup = function () {
            // console.log(document.getElementById('pagecount').value);
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
        $scope.popuplawyer = function () {
            $state.go('app.registerLawyer');
        }

        $scope.edit = function (val) {
            $scope.lawyerregister_gid = val;
            $scope.lawyerregister_gid = localStorage.setItem('lawyerregister_gid', val);
            $state.go('app.editRegisterlawyer');
        }
       
        $scope.delete = function (lawyerregister_gid) {
            var params = {
                lawyerregister_gid: lawyerregister_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/registerLawyer/lawyerDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal(resp.data.message);
                        }
                        else {
                            SweetAlert.swal(resp.data.message);
                            activate();
                        }
                    });
                }
            });
        };

        $scope.Viewfirm = function (val) {
            $scope.lawyerregister_gid = val;
            $scope.lawyerregister_gid = localStorage.setItem('lawyerregister_gid', val);
            $state.go('app.viewLawyer');
        }

        $scope.logincreation = function (lawyerregister_gid) {
            var params = {
                lawyerregister_gid: lawyerregister_gid
            }
            var modalInstance = $modal.open({
                templateUrl: '/lawyerLoginContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.close = function () {
                    $modalInstance.close('closed');
                }
                var url = 'api/registerLawyer/lawyerView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lawyerref_no = resp.data.lawyerref_no;
                    $scope.lawyer_name = resp.data.lawyer_name;
                    $scope.email_address = resp.data.email_address;
                });
                $scope.sendaccouncreate = function () {
                    var params = {
                        lawyeruser_code: $scope.lawyerref_no,
                        lawyer_userpassword: $scope.lawyer_userpassword,
                        email_address: $scope.email_address,
                        lawyerregister_gid: lawyerregister_gid
                    }
                  
                    var url = "api/registerLawyer/lawyerlogincreation";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        console.log(resp.data.status);
                        if (resp.data.status == true) {

                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
            }
        }

        $scope.viewlogin = function (lawyerregister_gid) {
            var params = {
                lawyerregister_gid: lawyerregister_gid
            }
            $scope.resetpassword = false;
            var modalInstance = $modal.open({
                templateUrl: '/ViewlawyerLoginContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.mandatorycheck = function () {
                   
                    $scope.mandatoryfields = false;
                }
                $scope.close = function () {
                    $modalInstance.close('closed');
                }
                $scope.resetpwd=function()
                {
                    $scope.resetpassword = true;
                    $scope.lawyeruser_password = '';
                }
                var url = 'api/registerLawyer/lawyerView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lawyerref_no = resp.data.lawyerref_no;
                    $scope.lawyer_name = resp.data.lawyer_name;
                    $scope.lawyeruser_password = resp.data.lawyeruser_password;
                    $scope.loginuserstatus = resp.data.lawyeruser_status;
                    $scope.lawyerstatus_remarks = resp.data.block_remarks;
                    $scope.block_remarks = resp.data.block_remarks;
                    console.log(resp.data.block_remarks)
                });
                $scope.sendaccounstatus = function () {
                    if ($scope.loginuserstatus != 'Block') {

                        if ($scope.lawyerstatus_remarks == '') {
                            var lsblock_remarks = $scope.txtblockremarks
                        }
                        else {
                            lsblock_remarks = $scope.lawyerstatus_remarks
                        }
                        var params = {

                            lawyeruser_activation: $scope.loginuserstatus,
                            lawyerregister_gid: lawyerregister_gid,
                            blockremarks: lsblock_remarks,
                            lawyeruser_password: $scope.lawyeruser_password
                        }

                        var url = "api/registerLawyer/lawyeractivationstatus";
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                activate();
                            }
                            else {
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                            }
                        });
                    }
                    else {
                        if (($scope.txtblockremarks == '') || ($scope.txtblockremarks == undefined))
                        {
                            $scope.mandatoryfields = true;
                        }
                        else {
                            console.log($scope.txtblockremarks);
                            $scope.mandatoryfields = false;
                            if ($scope.lawyerstatus_remarks == '') {
                                var lsblock_remarks = $scope.txtblockremarks
                            }
                            else {
                                lsblock_remarks = $scope.lawyerstatus_remarks
                            }
                            var params = {

                                lawyeruser_activation: $scope.loginuserstatus,
                                lawyerregister_gid: lawyerregister_gid,
                                blockremarks: lsblock_remarks,
                                lawyeruser_password: $scope.lawyeruser_password
                            }

                            var url = "api/registerLawyer/lawyeractivationstatus";
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    $modalInstance.close('closed');
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    unlockUI();
                                    activate();
                                }
                                else {
                                    $modalInstance.close('closed');
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    unlockUI();
                                }
                            });
                        }
                    }
                }
            }
        }
        $scope.remarks=function()
        {
            $scope.mandatoryfields = false;
        }
    }
})();
