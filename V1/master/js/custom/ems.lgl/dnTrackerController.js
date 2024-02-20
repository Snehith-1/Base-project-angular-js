(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dnTrackercontroller', dnTrackercontroller);

    dnTrackercontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService','DownloaddocumentService'];

    function dnTrackercontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dnTrackercontroller';

        activate();


        function activate() {
            $scope.tab = {};
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "dn1tracker") {
                    $scope.tabdn1tracker = true;
                }
                else if (relpath1 == "dn2tracker") {
                    $scope.tabdn2tracker = true;
                }
                else if (relpath1 == "dn3tracker") {
                    $scope.tabdn3tracker = true;
                }

            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.tabdn1tracker = true;
                }
                else if ($scope.tab.activeTabId == 'dn1tracker') {
                    $scope.tabdn1tracker = true;

                }
                else if ($scope.tab.activeTabId == 'dn2tracker') {
                    $scope.tabdn2tracker = true;
                }
                else if ($scope.tab.activeTabId == 'dn3tracker') {
                    $scope.tabdn3tracker = true;
                }

            }
            lockUI();
            var url = "api/misDataimport/GetDN1list"
        
            SocketService.get(url).then(function (resp) {            
           
                $scope.DN1LIST = resp.data.mdlMisdataimport;
               
            });
            var url = "api/misDataimport/GetDN2list"
          
            SocketService.get(url).then(function (resp) {
                
                $scope.DN2LIST = resp.data.mdlMisdataimport;
            });
            var url = "api/misDataimport/GetDN3list"
          
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.DN3LIST = resp.data.mdlMisdataimport;
               
            });
            var url = "api/misDataimport/RecoveredCases"

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.RecoverCases = resp.data.mdlMisdataimport;
            });
            var url = "api/misDataimport/GetDNcount"
           
            SocketService.get(url).then(function (resp) {
              
                $scope.DN1_total_count = resp.data.dn1_total_count;
                $scope.DN1_today_count = resp.data.dn1_today_count;
                $scope.DN2_total_count = resp.data.dn2_total_count;
                $scope.DN2_today_count = resp.data.dn2_today_count;
                $scope.DN3_total_count = resp.data.dn3_total_count;
                $scope.DN3_today_count = resp.data.dn3_today_count;
                $scope.import_date = resp.data.import_date;
                $scope.process_date = resp.data.process_date;
                $scope.employee_name = resp.data.employee_name;
            });
         
        }
        $scope.dn1customerdetails = function (val) {
            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            $location.url('app/dnCustomer2loandetails?lstab=dn1tracker');
        }
        $scope.dn2customerdetails = function (val) {
            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            $location.url('app/dn2Customer2loandetails?lstab=dn2tracker');

        }
        $scope.dn3customerdetails = function (val) {
            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            $location.url('app/dn3Customer2loandetails?lstab=dn3tracker');

        }
        $scope.dn1ackstatus = function (urn) {

            var modalInstance = $modal.open({
                templateUrl: '/dn1ackstatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.isShowHide = function (param) {
                    if (param == "show") {

                        $scope.showval = true;
                        $scope.hideval = false;
                        $scope.showdiv = true;
                    }
                    else if (param == "hide") {

                        $scope.showval = false;
                        $scope.hideval = true;
                        $scope.showdiv = true;
                    }
                    else {
                        $scope.showval = false;
                        $scope.hideval = false;
                        $scope.showdiv = true;
                    }
                }          
                $scope.dn1ackstatusupdation = function () {
                    var params = {
                      urn:urn,
                      courier_status: $scope.dn1ackstatus,
                        delivered_date:$scope.txtdelivered_date,
                        returened_date:$scope.txtreturned_date
                    }
                    console.log(params);
                    var url = 'api/misDataimport/dn1ackstatusupdate';
                   
                    SocketService.post(url, params).then(function (resp) {
                       
                        if (resp.data.status == true) {
                         
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                          
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                           
                        }
                        activate();
                    });
                }
            }
        }
        $scope.dn2ackstatus = function (urn) {

            var modalInstance = $modal.open({
                templateUrl: '/dn2ackstatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.isShowHide = function (param) {
                    if (param == "show") {

                        $scope.showval = true;
                        $scope.hideval = false;
                        $scope.showdiv = true;
                    }
                    else if (param == "hide") {

                        $scope.showval = false;
                        $scope.hideval = true;
                        $scope.showdiv = true;
                    }
                    else {
                        $scope.showval = false;
                        $scope.hideval = false;
                        $scope.showdiv = true;
                    }
                }

                $scope.dn2ackstatusupdation = function () {

                    var params = {
                        urn: urn,
                        courier_status: $scope.dn1ackstatus,
                        dn2delivered_date: $scope.txtdelivered_date,
                        dn2returned_date: $scope.txtreturned_date
                    }
                    console.log(params);
                    var url = 'api/misDataimport/dn2ackstatusupdate';
                
                    SocketService.post(url, params).then(function (resp) {
                     
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        activate();
                    });
                }
            }
        }

        $scope.dn3ackstatus = function (urn) {

            var modalInstance = $modal.open({
                templateUrl: '/dn3ackstatus.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.isShowHide = function (param) {
                    if (param == "show") {

                        $scope.showval = true;
                        $scope.hideval = false;
                        $scope.showdiv = true;
                    }
                    else if (param == "hide") {

                        $scope.showval = false;
                        $scope.hideval = true;
                        $scope.showdiv = true;
                    }
                    else {
                        $scope.showval = false;
                        $scope.hideval = false;
                        $scope.showdiv = true;
                    }
                }

                $scope.dn3ackstatusupdation = function () {

                    var params = {
                        urn: urn,
                        courier_status: $scope.dn1ackstatus,
                        dn3delivered_date: $scope.txtdelivered_date,
                        dn2returned_date: $scope.txtreturned_date
                    }
                    console.log(params);
                    var url = 'api/misDataimport/dn3ackstatusupdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        activate();
                    });
                }
            }
        }

        $scope.dn1pdf = function (urn) {
           
          
            var params = {
                urn: urn

            };
            console.log(params);
            var url = 'api/misDataimport/DN1pdfcontent';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, "Report - DN1.pdf");
                    Notify.alert('DN1 Report Downloaded Successfully', 'success');
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                

                // var phyPath = resp.data;
                // var relPath = phyPath.split("EMS");
                // var relpath1 = relPath[1].replace("\\", "/");
                // var hosts = window.location.host;
                // var prefix = location.protocol + "//";
                // var str = prefix.concat(hosts, relpath1);
                // var link = document.createElement("a");
                // link.download = "Report - DN1";
                // var uri = str;
                // link.href = uri;
                // link.click();
                // Notify.alert('DN1 Report Downloaded Successfully', 'success')
                unlockUI();
            });
            
                }
               
        $scope.dn2pdf = function (urn) {

            var params = {
                urn: urn

            };
            console.log(params);
            var url = 'api/misDataimport/DN2pdfcontent';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, "Report - DN2.pdf");
                    unlockUI();
                    Notify.alert('DN2 Report Downloaded Successfully', 'success');
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                
            });
            
        }
        $scope.dn3pdf = function (urn) {

            var params = {
                urn: urn

            };
            console.log(params);
            var url = 'api/misDataimport/DN3pdfcontent';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, "Report - DN3.pdf");
                    unlockUI();
                    Notify.alert('DN3 Report Downloaded Successfully', 'success');
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
            });
            
        }

    }
})();