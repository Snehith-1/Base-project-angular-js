(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationReport', allocationReport);

    allocationReport.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function allocationReport($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationReport';
        var page;
        activate();

        function activate() {
            page = localStorage.getItem('page');

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var url = "api/zonalMapping/getzonalMappingdtl";
            SocketService.get(url).then(function (resp) {
                $scope.zonalMappingList = resp.data.zonalMapping;
            });

            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;

            });
            lockUI();
            $scope.totalDisplayed = 100;
            var url = 'api/allocationManagement/GetAllocationReport';
            SocketService.get(url).then(function (resp) {
                $scope.allocationdtl_list = resp.data.allocationdtl;
                unlockUI();
                if ($scope.allocationdtl_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.allocationdtl_list.length;
                    if ($scope.allocationdtl_list.length < 100) {
                        $scope.totalDisplayed = $scope.allocationdtl_list.length;
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
            if ($scope.allocationdtl_list != null) {

                if (pagecount < $scope.allocationdtl_list.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.allocationdtl_list.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.allocationdtl_list.length;
                        Notify.alert(" Total Summary " + $scope.allocationdtl_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.allocationdtl_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.complete = function (string) {

            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }
                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;
        }

        $scope.search = function () {
            var params = {
                customer_gid: $scope.customer_gid,
                zonalmapping_gid: $scope.zonalmapping_gid,
                zonalrisk_manager: $scope.zonalrisk_manager,
                risk_manager: $scope.risk_manager,
            }

            var url = 'api/allocationManagement/GetAllocationReportSummaryreport';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                $scope.allocationdtl_list = resp.data.allocationdtl;
            });
        }

        $scope.refresh = function () {
            $scope.customer_gid = "";
            $scope.customer = "";
            $scope.zonalmapping_gid = "";
            $scope.zonalrisk_manager = "";
            $scope.risk_manager = "";
            activate();
        }

        $scope.export = function () {

            lockUI();
            var url = 'api/allocationManagement/GetAllocationReportExcel';

            SocketService.get(url).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.excel_path, resp.data.excel_name);
                    //var phyPath = resp.data.excel_path;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.excel_name.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('tier1format_gid', tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }
    }
})();
