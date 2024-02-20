(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCourierMgmtsummaryController', MstCourierMgmtsummaryController);

    MstCourierMgmtsummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCourierMgmtsummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'MstCourierMgmtsummaryController';
        var params;
        activate();

        function activate() {           
            $scope.tab = {};

            var url = 'api/MstCourierManagement/GetCourierMgmt';
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
                    }
                    if (params.courier_type == "Courier Outward") {
                        $scope.courieroutward_List = resp.data.CourierMgmt;                       
                    }
                    if (params.courier_type == "Physical Inward") {
                        $scope.courierphysicalInward_List = resp.data.CourierMgmt;                        
                    }

                    if (params.courier_type == "Physical Outward") {
                        $scope.courierphysicaloutward_List = resp.data.CourierMgmt;                        
                    }
                }
                else {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
            });

            var url = "api/MstCourierManagement/CourierCount";
            SocketService.get(url).then(function (resp) {
                $scope.courier_inward = resp.data.courier_inward;
                $scope.courier_outward = resp.data.courier_outward;
                $scope.physical_inward = resp.data.physical_inward;
                $scope.physical_outward = resp.data.physical_outward;
            });

            var url = 'api/MstCourierManagement/GetACKNotification';
            SocketService.get(url).then(function (resp) {
                $scope.ack_status = resp.data.ack_status;
            });
        }

        $scope.tabcourierinward = function () {
            var url = 'api/MstCourierManagement/GetCourierMgmt';
            params = {
                courier_type: "Courier Inward"
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.courierInward_List = resp.data.CourierMgmt;                   
                }
                else {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
            });


        }
        $scope.tabcourieroutward = function () {
            var url = 'api/MstCourierManagement/GetCourierMgmt';
            params = {
                courier_type: "Courier Outward"
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.courieroutward_List = resp.data.CourierMgmt;                    
                }
                else {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
            });


        }

        $scope.tabphysicalinward = function () {
            var url = 'api/MstCourierManagement/GetCourierMgmt';
            params = {
                courier_type: "Physical Inward"
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.courierphysicalInward_List = resp.data.CourierMgmt;                    
                }
                else {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
            });
        }

        $scope.tabphysicaloutward = function () {
            var url = 'api/MstCourierManagement/GetCourierMgmt';
            params = {
                courier_type: "Physical Outward"
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.courierphysicaloutward_List = resp.data.CourierMgmt;                    
                }
                else {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
            });
        }     

        $scope.popupcourier = function () {
            $location.url('app/MstCourierCreation?courier_type=' + params.courier_type);            
        }

        $scope.editCI = function (courier_gid) {
            $location.url('app/MstCourierEdit?courier_gid=' + courier_gid + '&page=CI');               
        }

        $scope.editCO = function (courier_gid) {
            $location.url('app/MstCourierEdit?courier_gid=' + courier_gid + '&page=CO');  
        }

        $scope.editPI = function (courier_gid) {
            $location.url('app/MstCourierEdit?courier_gid=' + courier_gid + '&page=PI');    
        }

        $scope.editPO = function (courier_gid) {
            $location.url('app/MstCourierEdit?courier_gid=' + courier_gid + '&page=PO');  
        }

        $scope.ViewCI = function (courier_gid) {
            $location.url('app/MstCourierView?courier_gid=' + courier_gid + '&page=CI');            
        }

        $scope.ViewCO = function (courier_gid) {
            $location.url('app/MstCourierView?courier_gid=' + courier_gid + '&page=CO');
        }

        $scope.ViewPI = function (courier_gid) {
            $location.url('app/MstCourierView?courier_gid=' + courier_gid + '&page=PI');
        }

        $scope.ViewPO = function (courier_gid) {
            $location.url('app/MstCourierView?courier_gid=' + courier_gid + '&page=PO');
        }
    }
})();
