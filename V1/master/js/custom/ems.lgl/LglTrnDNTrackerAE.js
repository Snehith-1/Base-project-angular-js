﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dnTrackerAEcontroller', dnTrackerAEcontroller);

    dnTrackerAEcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService', 'DownloaddocumentService'];

    function dnTrackerAEcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dnTrackerAEcontroller';
        activate();
        function activate() {          
            $scope.totalDisplayedpending = 100;
            $scope.totalDisplayedgenerated = 100;
            $scope.totalDisplayedskipped = 100;
            $scope.totalDisplayedexclusion = 100;
            $scope.totalDisplayedlegalsr = 100;
            $scope.total = 0;
            $scope.tab = {};
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "pending") {
                    $scope.tabpending = true;
                }
                else if (relpath1 == "generated") {
                    $scope.tabgenerated = true;
                }
                else if (relpath1 == "skipped") {
                    $scope.tabskipped = true;
                }
                else if (relpath1 == "exclusion") {
                    $scope.tabexclusion = true;
                }
                else if (relpath1 == "legalsr") {
                    $scope.tablegalsr = true;
                }
            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.tabpending = true;
                }
                else if ($scope.tab.activeTabId == 'pending') {
                    $scope.tabpending = true;

                }
                else if ($scope.tab.activeTabId == 'generated') {
                    $scope.tabgenerated = true;
                }
                else if ($scope.tab.activeTabId == 'skipped') {
                    $scope.tabskipped = true;
                }
                else if ($scope.tab.activeTabId == 'exclusion') {
                    $scope.tabexclusion = true;
                }
                else if ($scope.tab.activeTabId == 'legalsr') {
                    $scope.tablegalsr = true;
                }
            }
            //Get ADM updated Information
            var url = "api/misDataimport/GetDNcount"
            lockUI();
            SocketService.get(url).then(function (resp) {              
                $scope.import_date = resp.data.import_date;
                $scope.process_date = resp.data.process_date;
                $scope.employee_name = resp.data.employee_name;
            });
        
            var url = "api/LglTrnDNTrackerAE/getAEPendingList"
            SocketService.get(url).then(function (resp) {
              
                $scope.DNpending_list = resp.data.DNpending_list;
                if ($scope.DNpending_list == null) {
                    $scope.pending_total = 0;
                    $scope.totalDisplayedpending = 0;
                }
                else {
                    $scope.pending_total = $scope.DNpending_list.length;
                    if ($scope.DNpending_list.length < 100) {
                        $scope.totalDisplayedpending = $scope.DNpending_list.length;
                    }

                }
            });
            var url = "api/LglTrnDNTrackerAE/GetAE_Count"
            unlockUI();
            SocketService.get(url).then(function (resp) {
                $scope.lblpending_count = resp.data.lblpending_count;
                $scope.lblgenerated_count = resp.data.lblgenerated_count;
                console.log(resp.data.lblexclusion_count)
                $scope.lblskipped_count = resp.data.lblskipped_count;
                $scope.lblexclusion_count = resp.data.lblexclusion_count;
                $scope.lbllegalsr_count = resp.data.lbllegalsr_count;
            });

        }
        $scope.dnskip = function (val) {
            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            $location.url('app/LglTrnDNTrackerSkipped?lstab=AE');
        }
        $scope.dngenerated_history = function (val) {
            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            $location.url('app/LglTrnDNTrackerHistory?lstab=AE');
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
        //-------Pending List----------//
        $scope.pending = function () {
            lockUI();
            var url = "api/LglTrnDNTrackerAE/getAEPendingList"
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.DNpending_list = resp.data.DNpending_list;
                if ($scope.DNpending_list == null) {
                    $scope.pending_total = 0;
                    $scope.totalDisplayedpending = 0;
                }
                else {
                    $scope.pending_total = $scope.DNpending_list.length;
                    if ($scope.DNpending_list.length < 100) {
                        $scope.totalDisplayedpending = $scope.DNpending_list.length;
                    }

                }
            });
        }
        //-------Generated List----------//
        $scope.generated = function () {
            lockUI();
            var url = "api/LglTrnDNTrackerAE/getAEGeneratedList"

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.DNgenerated_list = resp.data.DNgenerated_list;
                if ($scope.DNgenerated_list == null) {
                    $scope.generated_total = 0;
                    $scope.totalDisplayedgenerated = 0;
                }
                else {
                    $scope.generated_total = $scope.DNgenerated_list.length;
                    if ($scope.DNgenerated_list.length < 100) {
                        $scope.totalDisplayedgenerated = $scope.DNgenerated_list.length;
                    }
                }

            });
        }
        function generated_list() {
            var url = "api/LglTrnDNTrackerAE/getAEGeneratedList"

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.DNgenerated_list = resp.data.DNgenerated_list;
                if ($scope.DNgenerated_list == null) {
                    $scope.generated_total = 0;
                    $scope.totalDisplayedgenerated = 0;
                }
                else {
                    $scope.generated_total = $scope.DNgenerated_list.length;
                    if ($scope.DNgenerated_list.length < 100) {
                        $scope.totalDisplayedgenerated = $scope.DNgenerated_list.length;
                    }
                }

            });
        }
        function exclusion_list() {
            var url = "api/LglTrnDNTrackerAE/getAEExclusionList"

            SocketService.get(url).then(function (resp) {
                $scope.DNexclusion_list = resp.data.DNexclusion_list;
                if ($scope.DNexclusion_list == null) {
                    $scope.exclusion_total = 0;
                    $scope.totalDisplayedexclusion = 0;
                }
                else {
                    $scope.exclusion_total = $scope.DNexclusion_list.length;
                    if ($scope.DNexclusion_list.length < 100) {
                        $scope.totalDisplayedexclusion = $scope.DNexclusion_list.length;
                    }

                }
            });
        }
        //--------Skipped List----------//
        $scope.skipped = function () {
            lockUI();
            var url = "api/LglTrnDNTrackerAE/getAESkippedList"

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.DNskipped_list = resp.data.DNskipped_list;
                if ($scope.DNskipped_list == null) {
                    $scope.skipped_total = 0;
                    $scope.totalDisplayedskipped = 0;
                }
                else {
                    $scope.skipped_total = $scope.DNskipped_list.length;
                    if ($scope.DNskipped_list.length < 100) {
                        $scope.totalDisplayedskipped = $scope.DNskipped_list.length;
                    }

                }
            });
        }
        //--------Exclusion List----------//
        $scope.exclusion = function () {
            lockUI();
            var url = "api/LglTrnDNTrackerAE/getAEExclusionList"

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.DNexclusion_list = resp.data.DNexclusion_list;
                if ($scope.DNexclusion_list == null) {
                    $scope.exclusion_total = 0;
                    $scope.totalDisplayedexclusion = 0;
                }
                else {
                    $scope.exclusion_total = $scope.DNexclusion_list.length;
                    if ($scope.DNexclusion_list.length < 100) {
                        $scope.totalDisplayedexclusion = $scope.DNexclusion_list.length;
                    }

                }
            });

        }
        //--------Legal SR List----------//
        $scope.legalsrlist = function () {
            lockUI();
            var url = 'api/LglTrnDNTrackerAE/getAELegalSRList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.DNlegalsr_list = resp.data.DNlegalsr_list;
                if ($scope.DNlegalsr_list == null) {
                    $scope.legalsr_total = 0;
                    $scope.totalDisplayedlegalsr = 0;
                }
                else {
                    $scope.legalsr_total = $scope.DNlegalsr_list.length;
                    if ($scope.DNlegalsr_list.length < 100) {
                        $scope.totalDisplayedlegalsr = $scope.DNlegalsr_list.length;
                    }

                }
            });
        }
        $scope.popupDNskip = function (urn, Customer_name) {
            var modalInstance = $modal.open({
                templateUrl: '/skipdn.html',
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
            var d = new Date();
            $scope.txtskipvalid_date = (new Date(d.setDate(d.getDate() + 15)));
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var d = new Date();
                $scope.txtskipvalid_date = (new Date(d.setDate(d.getDate() + 15)));
                $scope.customer_urn = urn;
                $scope.customer_name = Customer_name;
                $scope.confirmSkip = function () {
                    lockUI();
                    var params = {
                        urn: urn,
                        skip_reason: $scope.txtskip_reason,
                        valid_date: $scope.txtskipvalid_date
                    }
                    console.log(params)
                    var url = "api/misDataimport/DNskip"

                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            activate();
                            Notify.alert('DN Skipped Successfully', 'success')
                        }
                        else {
                            Notify.alert('Error Occurred While Updating DN Skip ')
                        }
                        $modalInstance.close('closed');
                    });
                }

            }
        }
        //------------DN1 Generation----//
        $scope.dn1generatepage = function (val) {
            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            $location.url('app/LglTrnDNTrackerAEGenerate?lstab=pending');
        }
        //------------DN2 Generation----//
        $scope.dn2generatepage = function (val) {

            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            $location.url('app/LglTrnDNTrackerAE2Generate?lstab=pending');

        }
        //------------DN3 Generation----//
        $scope.dn3generatepage = function (val) {
            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            $location.url('app/LglTrnDNTrackerAE3Generate?lstab=pending');

        }
        $scope.dngeneratedinfo = function (val) {
            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            localStorage.setItem('MyZonalAllocationHistory', 'N');
            $location.url('app/LglTrnDN2GeneratedAE?lstab=dn2tracker');
        }
        $scope.dnackstatus = function (urn) {
            $scope.warningmsg = false;
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
                $scope.warningmsg = false;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.changewarningmsg = function () {
                    $scope.warningmsg = false;
                }
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
                    if ($scope.dn1ackstatus == 'Delivered') {
                        if (($scope.txtdelivered_date == '') || ($scope.txtdelivered_date == undefined)) {
                            $scope.warningmsg = true;
                        }
                        else {
                            $scope.warningmsg = false;
                            var params = {
                                urn: urn,
                                courier_status: $scope.dn1ackstatus,
                                delivered_date: $scope.txtdelivered_date,
                                returened_date: $scope.txtreturned_date
                            }

                            var url = 'api/misDataimport/dn1ackstatusupdate';

                            SocketService.post(url, params).then(function (resp) {

                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                    activate();
                                    generated_list();
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'Warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });

                                }
                                activate();
                                generated_list();
                            });
                        }
                    }
                    else {
                        $scope.warningmsg = false;
                        var params = {
                            urn: urn,
                            courier_status: $scope.dn1ackstatus,
                            delivered_date: $scope.txtdelivered_date,
                            returened_date: $scope.txtreturned_date
                        }

                        var url = 'api/misDataimport/dn1ackstatusupdate';

                        SocketService.post(url, params).then(function (resp) {

                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                                activate();
                                generated_list();
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'Warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });

                            }

                        });
                    }
                    activate();
                }
            }
        }
        $scope.dn2ackstatus = function (urn) {
            console.log('dn2');
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
            console.log('dn3');
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
        $scope.exclusioncustomer = function (urn, Customer_name) {
            console.log('test', urn);
            var modalInstance = $modal.open({
                templateUrl: '/exclusionpopup.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = urn;
                $scope.customername = Customer_name;
                var params = {
                    customer_urn: urn
                }
                var url = "api/LglTrnDNTrackerVertical/GetExclusionCustomerHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.exclusioncustomerHistory = resp.data.exclusionhistory;
                    if (resp.data.exclusionhistory == null) {
                        $scope.Nohistoryexclusion = true;
                    }
                    else {
                        $scope.historyexclusion = true;
                    }
                });
                $scope.confirmExclusioncustomer = function () {
                    var params = {
                        customer_urn: urn,
                        exclusion_reason: $scope.txtexclusion_reason
                    }
                    var url = "api/LglTrnDNTrackerVertical/GetExclusionCustomer";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            })
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }

        $scope.activateexclusion = function (urn, Customer_name) {

            var modalInstance = $modal.open({
                templateUrl: '/activateconfirmation.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = urn;
                $scope.customername = Customer_name;
                $scope.confirmActivation = function () {
                    lockUI();
                    var params = {
                        customer_urn: urn,
                        exclusion_reason: $scope.txtactivated_reason
                    }
                    var url = "api/LglTrnDNTrackerVertical/GetActivationCustomer";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
                            exclusion_list();
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
        $scope.exclusionHistory = function (urn, Customer_name) {

            var modalInstance = $modal.open({
                templateUrl: '/exclusionHistoryPopup.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = urn;
                $scope.customername = Customer_name;
                var params = {
                    customer_urn: urn
                }
                var url = "api/LglTrnDNTrackerVertical/GetExclusionCustomerHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.exclusioncustomerHistory = resp.data.exclusionhistory;

                });
            }
        }

        $scope.legalsr = function (urn, Customer_name) {
            var modalInstance = $modal.open({
                templateUrl: '/legalsr.html',
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
                $scope.customer_urn = urn;
                $scope.customer_name = Customer_name;
                $scope.confirm = function () {
                    lockUI();
                    var params = {
                        customer_urn: urn,
                        customer_name: Customer_name,
                        remarks: $scope.txtremarks
                    }
                    console.log(params)
                    var url = "api/LglTrnDNTrackerVertical/raiselegalsr"

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            Notify.alert('Legal SR Raised Successfully', 'success')
                        }
                        else {
                            Notify.alert('Error Occurred ')
                        }
                        $modalInstance.close('closed');
                    });
                }

            }
        }

        $scope.raisesr = function (templegalsr_gid, customer_gid) {
            $scope.templegalsr_gid = localStorage.setItem('templegalsr_gid', templegalsr_gid);
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            $location.url('app/raiseSR2authentication?lstab=dntracker');
        }
        $scope.view = function (legalsr_gid, legalsr_customergid) {
            $scope.templegalsr_gid = localStorage.setItem('templegalsr_gid', templegalsr_gid);
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            $state.go('app.viewlegalSR');
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
                    Notify.alert('DN1 Report Downloaded Successfully', 'success')
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                
                //var phyPath = resp.data;
                //console.log(resp.data);
                //var relPath = phyPath.split("EMS");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //link.download = "Report - DN1";
                //var uri = str;
                //link.href = uri;
                //link.click();
                // DownloaddocumentService.Downloaddocument(resp.data, "Report - DN1.pdf");
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
                    Notify.alert('DN2 Report Downloaded Successfully', 'success')
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                //var phyPath = resp.data;
                //var relPath = phyPath.split("EMS");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //link.download = "Report - DN2";
                //var uri = str;
                //link.href = uri;
                //link.click();
                // DownloaddocumentService.Downloaddocument(resp.data, "Report - DN2.pdf");
                // Notify.alert('DN2 Report Downloaded Successfully', 'success')
                unlockUI();
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
                    Notify.alert('DN3 Report Downloaded Successfully', 'success')
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                
                //var phyPath = resp.data;
                //var relPath = phyPath.split("EMS");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                //var link = document.createElement("a");
                //link.download = "Report - DN3";
                //var uri = str;
                //link.href = uri;
                //link.click();
                //   DownloaddocumentService.Downloaddocument(resp.data, "Report - DN3.pdf");
                // Notify.alert('DN3 Report Downloaded Successfully', 'success')
                unlockUI();
            });

        }
    }
})();