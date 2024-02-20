(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocation360controller', allocation360controller);

    allocation360controller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function allocation360controller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocation360controller';

        activate();

        function activate() {
            lockUI();

            $scope.Allocated = localStorage.getItem('Allocated');
            $scope.ZonalAllocationBack = localStorage.getItem('ZonalAllocationBack');
           
            var allocationdtl_gid = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
            }
             
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.state_name = resp.data.state_name;
                $scope.district_name = resp.data.district_name;
                $scope.assigned_RM = resp.data.assigned_RM;
                $scope.customername = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.ZonalRMname = resp.data.ZonalRMname;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.zonal_name = resp.data.zonal_name;
              
            });

            var url = "api/allocationManagement/getAllocationdocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                    $scope.upload_list = resp.data.upload_list;

                }
                else {


                }
            });

            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
              
                $scope.ppa_name = resp.data.ppa_name;
                $scope.customerdetails = resp.data;
                $scope.customerCollateral = resp.data.collateraldtl;
                $scope.holdallocationlist = resp.data.holdallocation;
                $scope.U2CMovedallocation = resp.data.U2CMovedallocation;
                $scope.customerguarantorlist = resp.data.Guarantorsdtl;
                $scope.customerPromotorlist = resp.data.Promoterdtl;
                $scope.sanctiondetails = resp.data.loandtl;
                angular.forEach($scope.sanctiondetails, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid,
                        allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
                    };

                    var url = 'api/allocationManagement/GetAllocateloanList';
                    SocketService.post(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanList;
                        value.expand = false;
                    });
                });
            });
            var customer_gid = {
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }
            var url = 'api/raiseLegalSR/GetDemandNoticedtl';

            SocketService.getparams(url, customer_gid).then(function (resp) {
                $scope.demandnotice_list = resp.data.demandnotice_list;
                $scope.demand_status = resp.data.demand_status;

            });
            var url = "api/visitReport/GetAllocationLogDetail";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.scheduleList = resp.data.schedulelogdtl;
                $scope.calllogdtlList = resp.data.calllogdtl;
                
            });

            var url = "api/customerManagement/EscrowSummary";
            SocketService.getparams(url, customer_gid).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowlist = resp.data.escrowSummary;
                }
            });
            unlockUI();
        }

        $scope.allocationAllocate = function () {
            localStorage.setItem('PendingSummary', 'N');
            localStorage.setItem('ZonalPendingSummary', 'N');
            var params = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid'),
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }
            var url = "api/allocationManagement/getRMAllocationSubmit";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    if (localStorage.getItem('ZonalAllocationBack') == "Y")
                    {
                        $state.go('app.allocationZonalRM');
                    }
                    else {
                        $state.go('app.caseAllocation');
                    }
                   
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

        $scope.scheduleLoghistory = function (schedulelog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/SchedulehistoryModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    schedulelog_gid: schedulelog_gid
                }
                var url = "api/visitReport/GetScheduleLogHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.scheduleList = resp.data.schedulelogdtl;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.escrowInfoView = function (escrow_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EscrowInfoModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    escrow_gid: escrow_gid
                }
                var url = "api/customerManagement/EscrowView";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.escrowview = resp.data;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.cancel = function () {
            $state.go('app.caseAllocation');
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

            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_type', $scope.txtdocument_type);
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
        }

        $scope.documentUpload = function () {

            if ($scope.uploadfrm != "") {
                var url = 'api/allocationManagement/AllocateUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                  
                    $scope.uploadallocation_list = resp.data.upload_list;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_type = "";

                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.documentuploadflag = "Y";
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.documentuploadflag = "N";
                    }
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }

        }

        $scope.uploadcancel = function (tmp_documentGid) {
            var allocationupload = {
                tmp_documentGid: tmp_documentGid
            }

            var url = 'api/allocationManagement/AllocateUploadcancel';
            SocketService.getparams(url, allocationupload).then(function (resp) {
                $scope.uploadallocation_list = resp.data.upload_list;
                if (resp.data.upload_list == null)
                {
                    $scope.documentuploadflag = "N";
                }
                else {
                 
                    $scope.documentuploadflag = "Y";
                }
            });
        }

        $scope.cancelupload = function () {
            $scope.uploadfrm = "";
            $scope.txtdocument_type = "";
            $("#addExternalupload").val('');
        }

        $scope.allocationSave = function () {
            localStorage.setItem('PendingSummary', 'Y')
            var params = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid'),
                customer_gid: localStorage.getItem('allocation_customer_gid'),
                allocationSubmit: 'N'
            }
            var url = "api/allocationManagement/getRMAllocationSave";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (localStorage.getItem('ZonalAllocationBack') == "Y") {
                        $state.go('app.allocationZonalRM');
                    }
                    else {
                        $state.go('app.caseAllocation');
                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });

        }

        $scope.allocationSubmit = function () {
            if ($scope.documentuploadflag == "Y")
            {
                localStorage.setItem('PendingSummary', 'N');
                localStorage.setItem('ZonalPendingSummary', 'N');
                var params = {
                    allocationdtl_gid: localStorage.getItem('allocationdtl_gid'),
                    customer_gid: localStorage.getItem('allocation_customer_gid'),
                    allocationSubmit: 'Y'
                }
                var url = "api/allocationManagement/getRMAllocationSave";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        if (localStorage.getItem('ZonalAllocationBack') == "Y") {
                            $state.go('app.allocationZonalRM');
                        }
                        else {
                            $state.go('app.caseAllocation');
                        }
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
            else {
                alert('Atleast Upload One Document');
                return;
            }
            
        }
    }
})();
