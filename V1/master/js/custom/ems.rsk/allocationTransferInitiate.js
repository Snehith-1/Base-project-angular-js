(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationTransferInitiate', allocationTransferInitiate);

    allocationTransferInitiate.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function allocationTransferInitiate($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationTransferInitiate';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/allocationTransfer/getallocateddetails";
            SocketService.get(url).then(function (resp) {
                $scope.allocationlist = resp.data.mappingdtl;
                if ($scope.allocationlist == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.allocationlist.length;
                    if ($scope.allocationlist.length < 100) {
                        $scope.totalDisplayed = $scope.allocationlist.length;
                    }
                }
                unlockUI();
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
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayed += Number;
            unlockUI();
        };

        $scope.AllocationtransferRM = function (allocationdtl_gid, customer_gid) {

            var params = {
                allocationdtl_gid: allocationdtl_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/AllocationRMTransfer.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = 'api/newServiceTicket/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                var url = "api/allocationManagement/gettransferDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.transferFrom = resp.data.transferred_from;
                    $scope.assignedRM_name = resp.data.assignedRM_name;
                    $scope.customerdtl = resp.data.customerdtl;
                    $scope.location = resp.data.location;
                    $scope.state_gid = resp.data.state_gid;
                    $scope.zonalRM_name = resp.data.zonalRM_name;
                    $scope.zonalRM_gid = resp.data.zonalRM_gid;
                    $scope.employee_assignedRM = resp.data.assignedRM;
                    $scope.zonal_gid = resp.data.zonal_gid;
                });
 
                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                // Within Zonal Event...//

                $scope.WithinZone = function (zonal_gid) {

                    var params = {
                        zonalmapping_gid: zonal_gid
                    }
                    var url = "api/rmMapping/GetZonalStateDtls";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.statedtllist = resp.data.statedtl;
                    });

                    $scope.zonalmapping_gid = "";
                    $scope.zonalrisk_managername = "";
                    $scope.crosszone_employeeGid = "";
                    $scope.withincrossstate_gid = "";
                    $scope.withincrossdistrict_gid = "";
                    $scope.withincrossstate_name = "";
                    $scope.withincrossdistrict_name = "";
                    $scope.CrossZone_employeename = "";
                    $scope.withinZonediv = true;
                    $scope.CrossZonediv = false;
                }

                $scope.withinStatechange = function (withincrossstate_gid) {
                 
                    lockUI();
                    var params = {
                        state_gid: withincrossstate_gid
                    }

                    var url = "api/allocationTransfer/getallocatedistritdtl";

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.distrtictwithindtl = resp.data.statedtl;

                    });
                    unlockUI();
                }

                $scope.crossStatechange = function (withincrossstate_gid) {
                   
                    lockUI();
                    var params = {
                        state_gid: withincrossstate_gid
                    }

                    var url = "api/allocationTransfer/getallocatedistritdtl";

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.distrtictcrossdtl = resp.data.statedtl;
                        console.log(resp);
                    });
                    unlockUI();
                }

                $scope.withinDistrictchange = function (withincrossdistrict_gid) {
                    lockUI();
                    var params = {
                        district_gid: withincrossdistrict_gid
                    }

                    var url = "api/allocationTransfer/getdistrictallocateRM";

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.withinZone_employeeGid = resp.data.assigned_RMGid;
                        $scope.withinZone_employeename = resp.data.assigned_RMname;

                        //if (resp.data.crosszone_employeeGid == null) {
                        //    alert("RM is not Mapping for the Selected District");
                        //    return;
                        //}
                        //else {
                           
                        //}
                        
                    });
                    unlockUI();
                }

                //$scope.withinzoneDistrict = function (district_gid) {
                //    lockUI();
                //    var params = {
                //        district_gid: district_gid
                //    }
                //    var url = "api/allocationTransfer/getdistrictallocateRM";

                //    SocketService.getparams(url, params).then(function (resp) {
                //        console.log(resp);
                //    });
                //    unlockUI();
                //    $scope.withinZonediv = true;
                //}

                // Cross Zonal Event...//

                $scope.CrossDistrictchange = function (withincrossdistrict_gid) {
                    lockUI();
                    var params = {
                        district_gid: withincrossdistrict_gid
                    }

                    var url = "api/allocationTransfer/getdistrictallocateRM";

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.crosszone_employeeGid = resp.data.assigned_RMGid;
                        $scope.CrossZone_employeename = resp.data.assigned_RMname;
                        //if (resp.data.crosszone_employeeGid == null) {
                        //    alert("RM is not Mapping for the Selected District");
                        //    return;
                        //}
                        //else {
                           
                        //}

                    });
                    unlockUI();
                }

                $scope.CrossZone = function (state_gid) {
                    lockUI();
                    var url = "api/zonalMapping/getzonaldtl";
                    SocketService.get(url).then(function (resp) {
                        $scope.zonalMappinglist = resp.data.zonalMapping;
                    });
                    $scope.withinZonediv = false;
                    $scope.CrossZonediv = true;
                    $scope.state_gid = "";
                    $scope.withinZone_employeeGid = "";
                    $scope.withinZone_employeename = "";
                    $scope.withincrossstate_gid = "";
                    $scope.withincrossdistrict_gid = "";
                    $scope.withincrossstate_name = "";
                    $scope.withincrossdistrict_name = "";
                    unlockUI();
                }

                $scope.zonalRMname = function (zonalmapping_gid) {
                    lockUI();
                    var params = {
                        zonalmapping_gid: zonalmapping_gid
                    }
                    var url = "api/zonalMapping/getviewzonalmappingdtl";

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.zonalrisk_managername = resp.data.zonalrisk_managername;
                        $scope.zonalrisk_managerGid = resp.data.zonalrisk_managerGid;
                        $scope.assignedcrossRMlist = resp.data.assignedRMlist;
                    });

                    var url = "api/rmMapping/GetZonalStateDtls";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.statedtllist = resp.data.statedtl;
                    });

                    unlockUI();
                }

                $scope.withzoneRMchange = function (allocationdtl_gid, withinZone_employeeGid) {
                    lockUI();
                    var params = {
                        assigned_RMGid: withinZone_employeeGid
                    }
                    var url = "api/zonalMapping/getRMstatedistrict";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.state_gid == null) {
                            alert("State and district are not Mapping for the Selected RM");
                            return;
                        }
                        else {
                            $scope.withincrossstate_gid = resp.data.state_gid;
                            $scope.withincrossdistrict_gid = resp.data.district_gid;
                            $scope.withincrossstate_name = resp.data.state_name;
                            $scope.withincrossdistrict_name = resp.data.district_name;
                        }
                    });
                    unlockUI();
                }



                $scope.submitAllocationtransfer = function () {

                    if ($scope.cbotransferstatus == "WithinZone") {
                        var withincrossdistrict_name = $('#withindistrict_name :selected').text();
                        var transfer_to = $scope.withinZone_employeeGid;
                        var transferto_name = $scope.withinZone_employeename;
                        var zonalmapping_gid = $scope.zonal_gid;

                        if ($scope.state_gid == undefined) {
                            alert("Select state");
                            return;
                        }
                        if ($scope.withinZone_employeeGid == undefined) {

                            alert("Select Transfer RM");
                            return;
                        }
                    }
                    else {
                        var transfer_to = $scope.crosszone_employeeGid;
                        var transferto_name = $scope.CrossZone_employeename;
                        var zonalmapping_gid = $scope.zonalmapping_gid
                        var withincrossdistrict_name = $('#crossdistrict_name :selected').text();
                        if ($scope.zonalmapping_gid == undefined) {
                            alert("Select Zonal");
                            return;
                        }
                        if ($scope.crosszone_employeeGid == undefined) {
                            alert("Select Transfer RM");
                            return;
                        }
                    }
                    lockUI();
                    var withincrossstate_name = $('#withincrossstate_name :selected').text();
                    var transferTo = {
                        allocationdtl_gid: allocationdtl_gid,
                        customer_gid: customer_gid,
                        transfer_from: $scope.transferFrom,
                        transfer_to: transfer_to,
                        transferto_name: transferto_name,
                        transferFrom_zonalgid: $scope.zonal_gid,
                        transferTo_zonalgid: zonalmapping_gid,
                        transferto_stategid: $scope.withincrossstate_gid,
                        transferto_statename: withincrossstate_name,
                        transferto_districtgid: $scope.withincrossdistrict_gid,
                        transferto_districtname: withincrossdistrict_name,
                        zonal_approvalfrom: $scope.zonalRM_gid,
                        zonal_approvalfromname: $scope.zonalRM_name,
                        zonal_approvalto: $scope.zonalrisk_managerGid,
                        zonal_approvaltoname: $scope.zonalrisk_managername,
                        transfer_remarks: $scope.txttransferremarks,
                        transfer_zonal: $scope.cbotransferstatus,
                    }

                    var url = "api/allocationTransfer/posttransferAllcoation";
                    SocketService.post(url, transferTo).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            $state.go('app.allocationTransfer');
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
                }
            }
        }


    }
})();
