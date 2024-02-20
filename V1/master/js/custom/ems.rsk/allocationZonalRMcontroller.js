(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationZonalRMcontroller', allocationZonalRMcontroller);

    allocationZonalRMcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function allocationZonalRMcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationZonalRMcontroller';

        activate();

        function activate() {
            $scope.cboQualifiedStatus = "Fresh";
            lockUI();
            localStorage.setItem('RSK_RM', 'N');
            localStorage.setItem('CC', 'N');
            localStorage.setItem('AgrCC', 'N');
            localStorage.setItem('AgrSuprCC', 'N');
            var url = "api/zonalAllocation/GetZonalAllocationLogDetail";
            SocketService.get(url).then(function (resp) {
                $scope.todayactivity = resp.data.todayactivity;
                $scope.zonalpendingcount = resp.data.zonalallocationcount;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.monthlyactivity = resp.data.monthlyactivity;
                $scope.count_currentallo = resp.data.count_current;
                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.count_completedallo = resp.data.count_completed;
                $scope.count_external = resp.data.count_external;
                $scope.count_breached = resp.data.count_breached;
                $scope.count_reportcancel = resp.data.count_reportchanges;
                $scope.count_qualified = resp.data.count_qualified;
                $scope.ADM_updatedby = resp.data.ADM_updatedby;
                $scope.ADM_updateddate = resp.data.ADM_updateddate;
                unlockUI();
            });
        }
        

        $scope.Fresh = function () {
            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            var url = "api/zonalAllocation/GetZoanlFreshAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.qualifiedallocationList = resp.data.qualifiedallocation;
                if ($scope.qualifiedallocationList == null) {
                    $scope.totalQualified = 0;
                    $scope.totalQualifiedDisplayed = 0;
                }
                else {
                    $scope.totalQualified = $scope.qualifiedallocationList.length;
                    if ($scope.qualifiedallocationList.length < 100) {
                        $scope.totalQualifiedDisplayed = $scope.qualifiedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.ReVisit = function () {
            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            var url = "api/zonalAllocation/GetZonalReVisitAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.qualifiedallocationList = resp.data.qualifiedallocation;
                if ($scope.qualifiedallocationList == null) {
                    $scope.totalQualified = 0;
                    $scope.totalQualifiedDisplayed = 0;
                }
                else {
                    $scope.totalQualified = $scope.qualifiedallocationList.length;
                    if ($scope.qualifiedallocationList.length < 100) {
                        $scope.totalQualifiedDisplayed = $scope.qualifiedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.allcase = function () {
            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            var url = "api/zonalAllocation/GetZonalQualifiedAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.qualifiedallocationList = resp.data.qualifiedallocation;
                if ($scope.qualifiedallocationList == null) {
                    $scope.totalQualified = 0;
                    $scope.totalQualifiedDisplayed = 0;
                }
                else {
                    $scope.totalQualified = $scope.qualifiedallocationList.length;
                    if ($scope.qualifiedallocationList.length < 100) {
                        $scope.totalQualifiedDisplayed = $scope.qualifiedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.qualified = function () {
            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            var url = "api/zonalAllocation/GetZoanlFreshAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.qualifiedallocationList = resp.data.qualifiedallocation;
                if ($scope.qualifiedallocationList == null) {
                    $scope.totalQualified = 0;
                    $scope.totalQualifiedDisplayed = 0;
                }
                else {
                    $scope.totalQualified = $scope.qualifiedallocationList.length;
                    if ($scope.qualifiedallocationList.length < 100) {
                        $scope.totalQualifiedDisplayed = $scope.qualifiedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadQualifiedMore = function (pageQualifiedcount) {

            if (pageQualifiedcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageQualifiedcount);
            if ($scope.qualifiedallocationList != null) {
                if ($scope.totalQualifiedDisplayed < $scope.qualifiedallocationList.length) {
                    $scope.totalQualifiedDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.qualifiedallocationList.length + " Records Only", "warning");
                    return;
                }
            }
        };

        $scope.current = function () {
            lockUI();
            $scope.totalCurrentDisplayed = 100;
            var url = "api/zonalAllocation/GetZonalCurrentAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.count_currentallo = resp.data.count_currentallo;
                $scope.currentallocationList = resp.data.allocationdtl;
                if ($scope.currentallocationList == null) {
                    $scope.totalCurrent = 0;
                    $scope.totalCurrentDisplayed = 0;
                }
                else {
                    $scope.totalCurrent = $scope.currentallocationList.length;
                    if ($scope.currentallocationList.length < 100) {
                        $scope.totalCurrentDisplayed = $scope.currentallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadCurrentMore = function (pageCurrentcount) {

            if (pageCurrentcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageCurrentcount);
            if ($scope.totalCurrentDisplayed < $scope.currentallocationList.length) {
                $scope.totalCurrentDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.currentallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.upcoming = function () {
            lockUI();
            $scope.totalUpcomingDisplayed = 100;
            var url = "api/zonalAllocation/GetZonalUpcomingAllocation";
            SocketService.get(url).then(function (resp) {

                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.upcomingallocationList = resp.data.allocationdtl;
                if ($scope.upcomingallocationList == null) {
                    $scope.totalUpcoming = 0;
                    $scope.totalUpcomingDisplayed = 0;
                }
                else {
                    $scope.totalUpcoming = $scope.upcomingallocationList.length;
                    if ($scope.upcomingallocationList.length < 100) {
                        $scope.totalUpcomingDisplayed = $scope.upcomingallocationList.length;
                    }
                }
                unlockUI();
            });
        }
        
        $scope.movetocurrentallocation = function (allocationdtl_gid, customer_urn, customername) {

            var modalInstance = $modal.open({
                templateUrl: '/movetocurrentallocationpopup.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;
                $scope.confirmmoveAllocation = function () {
                    var params = {
                        allocationdtl_gid: allocationdtl_gid,
                        allocationmove_reason: $scope.txtmove_reason,
                    }
                    var url = "api/allocationManagement/GetMovetoCurrentAllocation";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $state.go('app.pageredirect');
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

        $scope.loadUpcomingMore = function (pageUpcomingcount) {

            if (pageUpcomingcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageUpcomingcount);
            if ($scope.totalUpcomingDisplayed < $scope.upcomingallocationList.length) {
                $scope.totalUpcomingDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.upcomingallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.breached = function () {
            lockUI();
            $scope.totalBreachedDisplayed = 100;
            var url = "api/zonalAllocation/GetZonalBreachedAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.count_breached = resp.data.count_breached;
                $scope.breachedallocationList = resp.data.breacheddtl;
                if ($scope.breachedallocationList == null) {
                    $scope.totalBreached = 0;
                    $scope.totalBreachedDisplayed = 0;
                }
                else {
                    $scope.totalBreached = $scope.breachedallocationList.length;
                    if ($scope.breachedallocationList.length < 100) {
                        $scope.totalBreachedDisplayed = $scope.breachedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadBreachedMore = function (pageBreachedcount) {

            if (pageBreachedcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageBreachedcount);
            if ($scope.breachedallocationList != null) {
                if ($scope.totalBreachedDisplayed < $scope.breachedallocationList.length) {
                    $scope.totalBreachedDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.breachedallocationList.length + " Records Only", "warning");
                    return;
                }
            }
            else {

            }
            unlockUI();
        };

        $scope.completed = function () {
            lockUI();
            $scope.totalCompletedDisplayed = 100;
            var url = "api/zonalAllocation/GetZonalcompletedAlloSummary";
            SocketService.get(url).then(function (resp) {
                $scope.count_completedallo = resp.data.count_completedallo;
                $scope.completedallocationList = resp.data.allocationdtl;
                if ($scope.completedallocationList == null) {
                    $scope.totalCompleted = 0;
                    $scope.totalCompletedDisplayed = 0;
                }
                else {
                    $scope.totalCompleted = $scope.completedallocationList.length;
                    if ($scope.completedallocationList.length < 100) {
                        $scope.totalCompletedDisplayed = $scope.completedallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadCompletedMore = function (pageCompletedcount) {

            if (pageCompletedcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageCompletedcount);
            if ($scope.totalCompletedDisplayed < $scope.completedallocationList.length) {
                $scope.totalCompletedDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.completedallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.external = function () {
            lockUI();
            $scope.totalExternalDisplayed = 100;
            var url = "api/zonalAllocation/GetZonalExternalAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.count_external = resp.data.count_external;
                $scope.externalallocationList = resp.data.allocationdtl;
                console.log($scope.externalallocationList);
                if ($scope.externalallocationList == null) {
                    $scope.totalExternal = 0;
                    $scope.totalExternalDisplayed = 0;
                }
                else {
                    $scope.totalExternal = $scope.externalallocationList.length;
                    if ($scope.externalallocationList.length < 100) {
                        $scope.totalExternalDisplayed = $scope.externalallocationList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadExternalMore = function (pageExternalcount) {

            if (pageExternalcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageExternalcount);
            if ($scope.externalallocationList != null) {
                if ($scope.totalExternalDisplayed < $scope.externalallocationList.length) {
                    $scope.totalExternalDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.externalallocationList.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.reportcancel = function () {
            lockUI();
            $scope.totalReportDisplayed = 100;
            var url = "api/zonalAllocation/GetVisitCancelChanges";
            SocketService.get(url).then(function (resp) {
                $scope.count_reportcancel = resp.data.count_reportcancel;
                $scope.reportcancelList = resp.data.allocationdtl;
                if ($scope.reportcancelList == null) {
                    $scope.totalReport = 0;
                    $scope.totalReportDisplayed = 0;
                }
                else {
                    $scope.totalReport = $scope.reportcancelList.length;
                    if ($scope.reportcancelList.length < 100) {
                        $scope.totalReportDisplayed = $scope.reportcancelList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadReportMore = function (pageReportcount) {

            if (pageReportcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageReportcount);
            if ($scope.reportcancelList != null) {
                if ($scope.totalReportDisplayed < $scope.reportcancelList.length) {
                    $scope.totalReportDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.reportcancelList.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.all = function () {
            $scope.cboQualifiedStatus = undefined;
        }

        $scope.holdAllocation = function (allocationdtl_gid, customer_urn, customername) {

            var modalInstance = $modal.open({
                templateUrl: '/holdAllocationpopup.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;
                $scope.confirmHoldAllocation = function () {
                    var params = {
                        allocationdtl_gid: allocationdtl_gid,
                        allocationhold_reason: $scope.txthold_reason,
                    }
                    var url = "api/allocationManagement/GetHoldAllocation";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $state.go('app.pageredirect');
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



        $scope.exclusioncustomer = function (customer_urn, customername, qualified_status) {

            var modalInstance = $modal.open({
                templateUrl: '/exclusionpopup.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;
                $scope.customer_status = qualified_status;

                var params = {
                    customer_urn: customer_urn
                }
                var url = "api/zonalAllocation/GetExclusionCustomerHistory";
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
                        customer_urn: customer_urn,
                        exclusion_reason: $scope.txtexclusion_reason
                    }
                    var url = "api/zonalAllocation/GetExclusionCustomer";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $state.go('app.pageredirect');
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

        $scope.createDirectAllocation = function () {
            var url = "api/allocationManagement/tmpAllocatedocumentclear";
            SocketService.get(url).then(function (resp) {
            });
            $state.go('app.allocationZonalCreateDirect');
        }

        //$scope.createAllocation = function (customer_gid, customername) {
        //    var url = "api/allocationManagement/tmpAllocatedocumentclear";
        //    SocketService.get(url).then(function (resp) {
        //    });
        //    localStorage.setItem('allocation_customer_gid', customer_gid);
        //    $state.go('app.allocationZonalCreate');
        //}

        $scope.historyallocation = function (customer_gid) {
            localStorage.setItem('MyZonalAllocationHistory', 'Y')
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $state.go('app.allocationHistorydetails');
        }

        $scope.viewZonalallocation = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $state.go('app.zonalAllocation360');
        }

        $scope.viewallocation = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('ZonalAllocationBack', 'Y');
            localStorage.setItem('MyAllocation', 'N');
            localStorage.setItem('Allocated', 'N');
            $state.go('app.zonalAllocation360');
        }

        $scope.viewallocateddetails = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('ZonalAllocationBack', 'Y');
            localStorage.setItem('MyAllocation', 'N');
            localStorage.setItem('Allocated', 'Y');
            $state.go('app.zonalAllocation360');
        }

        $scope.exteralAllocate = function (allocationdtl_gid) {
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/AllocateExternal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = "api/allocationManagement/tmpExternaldocumentclear"
                SocketService.get(url).then(function (resp) {

                });
                var url = 'api/allocationManagement/getExternalNamelist';
                SocketService.get(url).then(function (resp) {
                    $scope.externaldtl = resp.data.externaldtl;
                });

                var url = "api/allocationManagement/gettransferDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerdtl = resp.data.customerdtl;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                $scope.uploadallocation = function (val, val1, name) {
                    
                    var frm = new FormData();
                    for (var i = 0; i < val.length; i++) {
                        var item = {
                            name: val[i].name,
                            file: val[i]
                        };
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
        
                                if (IsValidExtension == false) {
                                    Notify.alert("File format is not supported..!", {
                                        status: 'danger',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    return false;
                                }
                    }
                    // var item = {
                    //     name: val[0].name,
                    //     file: val[0]
                    // };
                    // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                    // if (IsValidExtension == false) {
                    //     alert("File format is not supported..!", {
                    //         status: 'danger',
                    //         pos: 'top-center',
                    //         timeout: 3000
                    //     });
                    //     return false;
                    // }
                    
                    // frm.append('fileupload', item.file);
                    // frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    frm.append('project_flag', "documentformatonly");
                    $scope.uploadfrm = frm;

                    var url = 'api/allocationManagement/ExternalUpload';

                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        console.log(resp);
                        $scope.uploadallocation_list = resp.data.upload_list;
                        $("#addExternalupload").val('');

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert('File Format Not Supported!', {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                }

                $scope.uploadcancel = function (tmp_documentGid) {
                    var allocationupload = {
                        tmp_documentGid: tmp_documentGid
                    }
                    var url = 'api/allocationManagement/ExternalUploadcancel';
                    SocketService.getparams(url, allocationupload).then(function (resp) {
                        $scope.uploadallocation_list = resp.data.upload_list;
                    });
                }

                $scope.submitAllocateExternal = function () {
                    lockUI();
                    var targetdate = new Date();
                    targetdate.setFullYear($scope.targetdate.getFullYear());
                    targetdate.setMonth($scope.targetdate.getMonth());
                    targetdate.setDate($scope.targetdate.getDate());

                    var transferTo = {
                        allocationdtl_gid: allocationdtl_gid,
                        external_usergid: $scope.external_usergid,
                        external_allocateRemarks: $scope.AllocationExtRemarks,
                        target_date: targetdate,
                    }

                    var url = "api/allocationManagement/postAllocationExternal";
                    SocketService.post(url, transferTo).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.AllocationExtRemarks = "";
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $state.go('app.pageredirect');

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

        //$scope.showPopover = function (assigned_RM, id) {
        //    console.log(id);
        //    $("[data-toggle=popover]").popover({
        //        html: true,
        //        content: function () {
        //            return $('#popover-content').html();
        //        }
        //    });

        //    var params = {
        //        assigned_RM: assigned_RM,
        //        qualified_status: 'Fresh'
        //    }

        //    var url = 'api/RskDashboard/GetRMAllocateCountdtl';
        //    SocketService.getparams(url, params).then(function (resp) {

        //        if (resp.data.status == true) {

        //            $scope.zonalpendingcount[id][assigned_RM] = resp.data.customerstatusdtl;
        //            console.log('resp',resp.data.customerstatusdtl);
        //            console.log('id',$scope.zonalpendingcount[id][assigned_RM]);
        //        }
        //        else {
        //            $scope.zonalpendingcount[id][assigned_RM] = "No Record";
        //            $("[data-toggle=popover]").popover({
        //                html: false,
        //                content: function () {
        //                    return $('#popover-content').html();
        //                }
        //            });
        //        }
        //    });
        //};

        //$scope.showRevisitPopover = function (assigned_RM, data) {

        //    $("[data-toggle=revisitpopover]").popover({
        //        html: true,
        //        content: function () {
        //            return $('#popover-Revisitcontent').html();
        //        }
        //    });

        //    var params = {
        //        assigned_RM: assigned_RM,
        //        qualified_status: 'Re-Visit'
        //    }

        //    var url = 'api/RskDashboard/GetRMAllocateCountdtl';
        //    SocketService.getparams(url, params).then(function (resp) {

        //        if (resp.data.status == true) {
        //            $scope.customerrevisitdtl = resp.data.customerstatusdtl;
        //            console.log('YES', $scope.customerrevisitdtl);
        //        }
        //        else {
        //            $scope.customerrevisitdtl = 'No Record';
        //            console.log('NO', $scope.customerrevisitdtl);
        //            $("[data-toggle=revisitpopover]").popover({
        //                html: false,
        //                content: function () {
        //                    return $('#popover-Revisitcontent').html();
        //                }
        //            });
        //        }
        //    });
        //};

        $scope.externalAssigned = function (allocationdtl_gid) {
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/AssignedExternal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = "api/allocationManagement/getExternalDetails";
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.customerdtl = resp.data.customerdtl;
                    $scope.externalname = resp.data.externalname;
                    $scope.AllocateExtRemarks = resp.data.requested_remarks;
                    $scope.assigned_by = resp.data.assigned_by;
                    $scope.assigned_date = resp.data.assigned_date;
                    $scope.target_date = resp.data.target_date;
                });

                var url = "api/allocationManagement/getExternaldocument";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.upload_list = resp.data.upload_list;
                });
                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
            }
        }

        $scope.createAllocation = function (customer_urn, qualified_status) {
            var params = {
                customer_urn: customer_urn
            }
            lockUI();
            var url = "api/allocationManagement/GetCustomerGid";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    //var url = "api/allocationManagement/tmpAllocatedocumentclear";
                    //SocketService.get(url).then(function (resp) {
                    //});
                    localStorage.setItem('allocation_customer_gid', resp.data.customer_gid);
                    localStorage.setItem('qualified_status', qualified_status)
                    $state.go('app.allocationZonalCreate');
                    unlockUI();
                }
                else {
                    unlockUI();
                    var modalInstance = $modal.open({
                        templateUrl: '/warningpopup.html',
                        controller: ModalInstanceCtrl,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };
                    }
                }
            });
        }

        $scope.generetePDF = function (allocationdtl_gid) {

            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            };
            var url = 'api/zonalAllocation/visitReportpdfcontent';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var filepath = resp.data.relpath1;
                    var filename = resp.data.file_name;
                            DownloaddocumentService.Downloaddocument(relpath1, filename);
                        }
                        else {
                            unlockUI();
                            Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                unlockUI();
                    });
                }
            
    }
})();
