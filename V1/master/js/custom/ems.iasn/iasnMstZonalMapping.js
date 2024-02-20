
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstZonalMapping', iasnMstZonalMapping);

        iasnMstZonalMapping.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnMstZonalMapping($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'iasnMstZonalMapping';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/IasnMstZone/ZoneSummary";
            SocketService.get(url).then(function (resp) {
                $scope.ZoneSummary = resp.data.MdlZoneSummary;
                unlockUI();
                if ($scope.ZoneSummary == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.ZoneSummary.length;
                    if ($scope.ZoneSummary.length < 100) {
                        $scope.totalDisplayed = $scope.ZoneSummary.length;
                    }
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
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        };


        $scope.addZone = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/addZone.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {

                var url = "api/IasnMstZone/Employee";
                SocketService.get(url).then(function (resp) {
                   $scope.employee_list=resp.data.employee_list;
                });

                $scope.rdb_acks = '';

                $scope.Add = function () {
                    var params = {
                        zone_name: $scope.zoneName,
                        employee_gid: $scope.rmlist,
                        employee_name: $('#rmlist :selected').text(),
                        acknowledgement_flag: $scope.rdb_acks
                    }
                    lockUI();
                    var url = "api/IasnMstZone/PostRMName";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            modalInstance.close('closed');
                            activate();
                        }
                        var params = {
                            zone_name: $scope.zoneName
                        }
                        var url = 'api/IasnMstZone/RMStatusSummary';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.rmstatus_list = resp.data.MdlRMStatusSummary;
                            if ($scope.rmstatus_list != null) {
                                $scope.rmlist_status = true;
                            }
                            else {
                                $scope.rmlist_status = false;
                            }
                        });
                        var url = "api/IasnMstZone/Employee";
                        SocketService.get(url).then(function (resp) {
                            $scope.employee_list = resp.data.employee_list;
                        });
                        $scope.rmlist = '';
                        $scope.rdb_acks = '';
                    });
                }
                $scope.Submit = function () {
                    var params = {
                        zone_name: $scope.zoneName
                    }
                    lockUI();
                    var url = "api/IasnMstZone/CreateZone";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            modalInstance.close('closed');
                            activate();
                        }
                    });
                }

                $scope.deleteRM = function (val3) {
                    var zone_name = $scope.zoneName;
                    var params = {
                        employee_gid: val3,
                        zone_name : zone_name
                    };
                    var url = 'api/IasnMstZone/RM_Delete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = 'api/IasnMstZone/RMStatusSummary';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.rmstatus_list = resp.data.MdlRMStatusSummary;
                                if ($scope.rmstatus_list != null) {
                                    $scope.rmlist_status = true;
                                }
                                else {
                                    $scope.rmlist_status = false;
                                }
                            });
                            var url = "api/IasnMstZone/Employee";
                            SocketService.get(url).then(function (resp) {
                                $scope.employee_list = resp.data.employee_list;
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                    activate();
                };
            }
        }

        $scope.editZone=function(val,val1)
        {
            var modalInstance = $modal.open({
                templateUrl: '/editZone.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
               
                var params = {
                    zone_gid: val
                }
                var url = 'api/IasnMstZone/EditZone';
                SocketService.getparams(url, params).then(function (resp) {
                   $scope.zoneNameEdit=resp.data.zone_name;
                    $scope.zoneref_code = resp.data.zoneref_no;
                    $scope.rm_listEdit = '';
                    $scope.rdb_acksedit = '';
                });
                var url = "api/IasnMstZone/Employee";
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });
                var params = {
                    zone_name: val1
                }
                var url = 'api/IasnMstZone/RMStatusSummary';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rmstatus_list = resp.data.MdlRMStatusSummary;
                });
               

                $scope.Update = function () {
                    lockUI();
                    var params = {
                        zone_gid: val,
                        zone_name: val1,
                        employee_gid: $scope.rm_listEdit,
                        employee_name: $('#rm_listEdit :selected').text(),
                        acknowledgement_flag: $scope.rdb_acksedit                      
                    }
                    var url = "api/IasnMstZone/UpdateZone";
                    SocketService.post(url, params).then(function (resp) {
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
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                           
                        }
                        var params = {
                            zone_name: $scope.zoneNameEdit
                        }
                        var url = 'api/IasnMstZone/RMStatusSummary';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.rmstatus_list = resp.data.MdlRMStatusSummary;
                        });
                        var url = "api/IasnMstZone/Employee";
                        SocketService.get(url).then(function (resp) {
                            $scope.employee_list = resp.data.employee_list;
                        });
                        $scope.rm_listEdit = '';
                        $scope.rdb_acksedit = '';
                    });
              
                }

                $scope.deleteRM = function (val3) {
                    var zone_name = val1
                    var params = {
                        employee_gid: val3,
                        zone_name: zone_name
                    };
                    var url = 'api/IasnMstZone/RM_Delete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = 'api/IasnMstZone/RMStatusSummary';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.rmstatus_list = resp.data.MdlRMStatusSummary;
                            });
                            var url = "api/IasnMstZone/Employee";
                            SocketService.get(url).then(function (resp) {
                                $scope.employee_list = resp.data.employee_list;
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
        
        $scope.showPopover = function (zone_gid, zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showpopupModal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            lockUI();
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    zone_gid: zone_gid
                }
                var url = 'api/IasnMstZone/EditZone';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.zone_name = zone_name;
                    $scope.MdlRmList = resp.data.MdlRmList;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.deleteZone = function (val) {
            var params = {
                zone_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Zone ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/IasnMstZone/Zone_Delete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            alert(resp.data.message, {
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
    }
})();
