(function () {
    'use strict';

    angular
        .module('angle')
        .controller('legalsrreportcontroller', legalsrreportcontroller);

    legalsrreportcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function legalsrreportcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'legalsrreportcontroller';

        activate();
        function activate() {
           

            var url = 'api/lglDashboard/Getlegalsrreport';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.legalSR_list = resp.data.legalSR_list;
              
            });


        }

        $scope.export = function () {
            var params = {       
            srref_no: $scope.srref_no,
            raised_by: $scope.raised_by,
            raised_date: $scope.raised_date,
            raised_by_department: $scope.raised_by_department,
            customer_urn: $scope.customer_urn,
            customer_name: $scope.customer_name,
            auth_status: $scope.auth_status,
            auth_date: $scope.auth_date,
            auth_remarks: $scope.auth_remarks,
            approval_status: $scope.approval_status
            }
            lockUI();
            var url = 'api/lglDashboard/GetSRexport';
            SocketService.post(url, params).then(function (resp) {
                console.log(resp.data.status)
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    unlockUI();
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    activate();

                }

            });
        }
        //$scope.complete = function (string ) {

        //    if (string.length >= 3) {
        //        $scope.message = "";
        //        var url = 'api/lglDashboard/GetCustomerName';
        //        //var url = 'api/customer/GetExploreCustomer';
        //        var params = {
        //            customername: string
                   
        //        }
        //        SocketService.getparams(url, params).then(function (resp) {
        //            if (resp.data.status == true) {
        //                $scope.message = "";
                      
        //                $scope.legalSR_list = resp.data.customers_list;
                      
        //            }
        //            else {
        //                $scope.message = "No Records";
        //            }


        //        });
        //    }
        //    else {
        //        $scope.customers_list = null;
        //        $scope.message = "Type atleast three character";
        //    }
        //}

        //$scope.fillTextbox = function (customer_gid, customer_name) {
        //    //console.log('string', customer_name, customer_gid);
        //    $scope.customer = customer_name;
        //    $scope.customer_gid = customer_gid;
        //    $scope.customers_list = null;



        //}

        $scope.all = function () {
            $scope.customername = "";
            $scope.month_date = "";
            $scope.year_date = "";

        activate();
    }

    $scope.search = function () {
        var params = {
            customername: $scope.customer,
            month_date: $scope.month_date,
            year_date: $scope.year_date
        }

        var url = 'api/lglDashboard/GetLegalreportsummary';
        lockUI();
        SocketService.post(url, params).then(function (resp) {
            unlockUI();
            $scope.legalSR_list = resp.data.Legalreportsummary;
        });
    }


       
    }
})();
