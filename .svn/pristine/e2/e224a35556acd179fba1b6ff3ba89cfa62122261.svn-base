(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lawfirmSummarycontroller', lawfirmSummarycontroller);

    lawfirmSummarycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function lawfirmSummarycontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'lawfirmSummarycontroller';

        activate();


        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/lawFirm/lawfirmdetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lawfirm_data = resp.data.lawfirm_list;
                $scope.total = $scope.lawfirm_data.length;
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
        $scope.popuplawfirm = function () {
           
            $state.go('app.addLawfirm');
        }

        $scope.edit = function (val) {
            $scope.lawfirm_gid = val;
            $scope.lawfirm_gid = localStorage.setItem('lawfirm_gid', val);
            $state.go('app.editLawfirm');
 
        }

        $scope.delete = function (lawfirm_gid) {
            var params = {
                lawfirm_gid: lawfirm_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/lawFirm/lawfirmDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred ', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully');
                }
            });
        };
   
        $scope.Viewfirm = function (val) {
            $scope.lawfirm_gid = val;
            $scope.lawfirm_gid = localStorage.setItem('lawfirm_gid', val);
            $state.go('app.viewLawfirm');

        }
        $scope.logincreation = function (lawfirm_gid) {
            var params = {
                lawfirm_gid: lawfirm_gid
            }
            var modalInstance = $modal.open({
                templateUrl: '/lawfirmLoginContent.html',
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
                var url = 'api/lawFirm/lawfirmView'
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.firm_refno = resp.data.firm_refno;
                    $scope.firm_name = resp.data.firm_name;
                });
                $scope.sendaccouncreate = function () {
                    var params = {
                        lawfirmuser_code: $scope.firm_refno,
                        lawfirmuser_password: $scope.lawfirm_userpassword,
                    
                        lawfirm_gid: lawfirm_gid
                    }
                    console.log(params);
                    var url = "api/lawFirm/lawfirmlogincreation";
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

        $scope.viewlogin = function (lawfirm_gid) {
            var params = {
                lawfirm_gid: lawfirm_gid
            }
            $scope.resetpassword = false;
            var modalInstance = $modal.open({
                templateUrl: '/ViewlawfirmLoginContent.html',
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
                $scope.resetpwd = function () {
                    $scope.resetpassword = true;
                    $scope.lawfirmruser_password = '';
                }
                var url = 'api/lawFirm/lawfirmView'
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.firm_refno = resp.data.firm_refno;
                    $scope.firm_name = resp.data.firm_name;
                    $scope.lawfirmuser_password = resp.data.lawfirmuser_password;
                    $scope.loginuserstatus = resp.data.lawfirmuser_status;
                    $scope.lawyerstatus_remarks = resp.data.block_remarks;
                    $scope.block_remarks = resp.data.block_remarks;
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

                            lawfirmuser_activation: $scope.loginuserstatus,
                            lawfirm_gid: lawfirm_gid,
                            blockremarks: lsblock_remarks,
                            lawfirmuser_password: $scope.lawfirmuser_password
                        }
                        console.log(params);
                        var url = "api/lawFirm/lawfirmactivationstatus";
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
                        if (($scope.txtblockremarks == '') || ($scope.txtblockremarks == undefined)) {
                            $scope.mandatoryfields = true;
                        }
                        else {
                         
                            $scope.mandatoryfields = false;
                            if ($scope.lawyerstatus_remarks == '') {
                                var lsblock_remarks = $scope.txtblockremarks
                            }
                            else {
                                lsblock_remarks = $scope.lawyerstatus_remarks
                            }
                            var params = {

                                lawfirmuser_activation: $scope.loginuserstatus,
                                lawfirm_gid: lawfirm_gid,
                                blockremarks: lsblock_remarks,
                                lawfirmuser_password: $scope.lawfirmuser_password
                            }

                            var url = "api/lawFirm/lawfirmactivationstatus";
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
        $scope.remarks = function () {
            $scope.mandatoryfields = false;
        }
    }
})();
