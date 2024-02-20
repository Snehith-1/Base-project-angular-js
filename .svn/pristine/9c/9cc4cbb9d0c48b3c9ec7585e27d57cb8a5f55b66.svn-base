(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasCourierMgmtsummary', idasCourierMgmtsummary);

    idasCourierMgmtsummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasCourierMgmtsummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'idasCourierMgmtsummary';
        var params;
        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            $scope.total = 0;
            $scope.tab = {};

            var url = 'api/IdasCourierManagement/GetCourierMgmt';


            var lstaburl = window.location.href;
            var relPath = lstaburl.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "CI") {
                    $scope.tabci = true;
                    params = {
                        courier_type: "Courier Inward"
                    }

                }
                else if (relpath1 == "CO") {
                    $scope.tabco = true;
                    params = {
                        courier_type: "Courier Outward"
                    }

                }
                else if (relpath1 == "PI") {
                    $scope.tabpi = true;
                    params = {
                        courier_type: "Physical Inward"
                    }

                }
                else if (relpath1 == "PO") {
                    $scope.tabpo = true;
                    params = {
                        courier_type: "Physical Outward"
                    }

                }
            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.tabci = true;
                    params = {
                        courier_type: "Courier Inward"
                    }

                }
                else if ($scope.tab.activeTabId == 'CI') {
                    $scope.tabci = true;
                    params = {
                        courier_type: "Courier Inward"
                    }


                }
                else if ($scope.tab.activeTabId == 'CO') {
                    $scope.tabco = true;
                    params = {
                        courier_type: "Courier Outward"
                    }

                }
                else if ($scope.tab.activeTabId == 'PI') {
                    $scope.tabpi = true;
                    params = {
                        courier_type: "Physical Inward"
                    }

                }
                else if ($scope.tab.activeTabId == 'PO') {
                    $scope.tabpo = true;
                    params = {
                        courier_type: "Physical Outward"
                    }

                }
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    if (params.courier_type == "Courier Inward") {
                        $scope.courierInward_List = resp.data.CourierMgmt;
                        if ($scope.courierInward_List == null) {
                            $scope.total = 0;
                            $scope.totalDisplayed = 0;
                        }
                        else {
                            $scope.total = $scope.courierInward_List.length;
                            if ($scope.courierInward_List.length < 100) {
                                $scope.totalDisplayed = $scope.courierInward_List.length;
                            }
                        }
                    }
                    if (params.courier_type == "Courier Outward") {
                        $scope.courieroutward_List = resp.data.CourierMgmt;
                        if ($scope.courieroutward_List == null) {
                            $scope.total = 0;
                            $scope.totalDisplayed = 0;
                        }
                        else {
                            $scope.total = $scope.courieroutward_List.length;
                            if ($scope.courieroutward_List.length < 100) {
                                $scope.totalDisplayed = $scope.courieroutward_List.length;
                            }
                        }
                    }
                    if (params.courier_type == "Physical Inward") {
                        $scope.courierphysicalInward_List = resp.data.CourierMgmt;
                        if ($scope.courierphysicalInward_List == null) {
                            $scope.total = 0;
                            $scope.totalDisplayed = 0;
                        }
                        else {
                            $scope.total = $scope.courierphysicalInward_List.length;
                            if ($scope.courierphysicalInward_List.length < 100) {
                                $scope.totalDisplayed = $scope.courierphysicalInward_List.length;
                            }
                        }
                    }

                    if (params.courier_type == "Physical Outward") {
                        $scope.courierphysicaloutward_List = resp.data.CourierMgmt;
                        if ($scope.courierphysicaloutward_List == null) {
                            $scope.total = 0;
                            $scope.totalDisplayed = 0;
                        }
                        else {
                            $scope.total = $scope.courierphysicaloutward_List.length;
                            if ($scope.courierphysicaloutward_List.length < 100) {
                                $scope.totalDisplayed = $scope.courierphysicaloutward_List.length;
                            }
                        }
                    }



                }
                else {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
            });

            var url = "api/IdasCourierManagement/CourierCount";
            SocketService.get(url).then(function (resp) {

                $scope.courier_inward = resp.data.courier_inward;
                $scope.courier_outward = resp.data.courier_outward;
                $scope.physical_inward = resp.data.physical_inward;
                $scope.physical_outward = resp.data.physical_outward;


            });

            var url = 'api/IdasCourierManagement/GetACKNotification';
            SocketService.get(url).then(function (resp) {
                $scope.ack_status = resp.data.ack_status;
            });
        }

        $scope.tabcourierinward = function () {
            var url = 'api/IdasCourierManagement/GetCourierMgmt';
            params = {
                courier_type: "Courier Inward"
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.courierInward_List = resp.data.CourierMgmt;
                    if ($scope.courierInward_List == null) {
                        $scope.total = 0;
                        $scope.totalDisplayed = 0;
                    }
                    else {
                        $scope.total = $scope.courierInward_List.length;
                        if ($scope.courierInward_List.length < 100) {
                            $scope.totalDisplayed = $scope.courierInward_List.length;
                        }
                    }

                }
                else {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
            });


        }
        $scope.tabcourieroutward = function () {
            var url = 'api/IdasCourierManagement/GetCourierMgmt';
            params = {
                courier_type: "Courier Outward"
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.courieroutward_List = resp.data.CourierMgmt;
                    if ($scope.courieroutward_List == null) {
                        $scope.total = 0;
                        $scope.totalDisplayed = 0;
                    }
                    else {
                        $scope.total = $scope.courieroutward_List.length;
                        if ($scope.courieroutward_List.length < 100) {
                            $scope.totalDisplayed = $scope.courieroutward_List.length;
                        }
                    }

                }
                else {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
            });


        }

        $scope.tabphysicalinward = function () {
            var url = 'api/IdasCourierManagement/GetCourierMgmt';
            params = {
                courier_type: "Physical Inward"
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.courierphysicalInward_List = resp.data.CourierMgmt;
                    if ($scope.courierphysicalInward_List == null) {
                        $scope.total = 0;
                        $scope.totalDisplayed = 0;
                    }
                    else {
                        $scope.total = $scope.courierphysicalInward_List.length;
                        if ($scope.courierphysicalInward_List.length < 100) {
                            $scope.totalDisplayed = $scope.courierphysicalInward_List.length;
                        }
                    }

                }
                else {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
            });


        }
        $scope.tabphysicaloutward = function () {
            var url = 'api/IdasCourierManagement/GetCourierMgmt';
            params = {
                courier_type: "Physical Outward"
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.courierphysicaloutward_List = resp.data.CourierMgmt;
                    if ($scope.courierphysicaloutward_List == null) {
                        $scope.total = 0;
                        $scope.totalDisplayed = 0;
                    }
                    else {
                        $scope.total = $scope.courierphysicaloutward_List.length;
                        if ($scope.courierphysicaloutward_List.length < 100) {
                            $scope.totalDisplayed = $scope.courierphysicaloutward_List.length;
                        }
                    }

                }
                else {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
            });


        }

        $scope.loadMore = function (pagecount) {
            console.log('courier_test');
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
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };
        $scope.popupcourier = function () {
            localStorage.setItem('courier_type', params.courier_type);
            $state.go('app.idasCourierCreation');
        }
        $scope.editCI = function (courier_gid) {
            localStorage.setItem('courier_gid', courier_gid)
            localStorage.setItem('page', 'CI')
            $state.go('app.idasCourierEdit');
        }

        $scope.editCO = function (courier_gid) {
            localStorage.setItem('courier_gid', courier_gid)
            localStorage.setItem('page', 'CO')
            $state.go('app.idasCourierEdit');
        }
        $scope.editPI = function (courier_gid) {
            localStorage.setItem('courier_gid', courier_gid)
            localStorage.setItem('page', 'PI')
            $state.go('app.idasCourierEdit');
        }
        $scope.editPO = function (courier_gid) {
            localStorage.setItem('courier_gid', courier_gid)
            localStorage.setItem('page', 'PO')
            $state.go('app.idasCourierEdit');
        }

        $scope.ViewCI = function (courier_gid) {
            localStorage.setItem('courier_gid', courier_gid)
            localStorage.setItem('page', 'CI')
            $state.go('app.idasTrnCourierView');
        }
        $scope.ViewCO = function (courier_gid) {
            localStorage.setItem('courier_gid', courier_gid)
            localStorage.setItem('page', 'CO')
            $state.go('app.idasTrnCourierView');
        }
        $scope.ViewPI = function (courier_gid) {
            localStorage.setItem('courier_gid', courier_gid)
            localStorage.setItem('page', 'PI')
            $state.go('app.idasTrnCourierView');
        }
        $scope.ViewPO = function (courier_gid) {
            localStorage.setItem('courier_gid', courier_gid)
            localStorage.setItem('page', 'PO')
            $state.go('app.idasTrnCourierView');
        }
    }
})();
