(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationViewcontroller', allocationViewcontroller);

    allocationViewcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location','SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function allocationViewcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationViewcontroller';

        var completed_flag=$location.search().completed_flag;
        var allocationdtl_gid=$location.search().allocationdtl_gid;
        var customer_gid=$location.search().allocation_customer_gid;
        var MyAllocation=$location.search().MyAllocation;

      

        activate();

        function activate() {
            lockUI(); 
            
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
           
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.state_name = resp.data.state_name;
                $scope.district_name = resp.data.district_name;
                $scope.assigned_RM = resp.data.assigned_RM;
                $scope.customername = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.ZonalRMname = resp.data.ZonalRMname;
                $scope.zonal_name = resp.data.zonal_name;
            });

            

            var url = "api/visitReport/GetAllocationLogDetail";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.scheduleList = resp.data.schedulelogdtl;
                $scope.calllogdtlList = resp.data.calllogdtl;

            });
            var paramsCustomerGID = {
                customer_gid: customer_gid
            }
            var url = 'api/raiseLegalSR/GetDemandNoticedtl';

            SocketService.getparams(url, paramsCustomerGID).then(function (resp) {
                $scope.demandnotice_list = resp.data.demandnotice_list;
                $scope.demand_status = resp.data.demand_status;

            });

            if (completed_flag == 'Y') {
                var url = "api/customerManagement/HistoryEscrowSummary";
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.escrowlist = resp.data.escrowSummary;
                        $scope.historyescrow = 'Y';
                         
                    }
                });

                var url = "api/allocationManagement/getAllocationdocument";
                SocketService.getparams(url, params).then(function (resp) {
                    if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                        $scope.upload_list = resp.data.upload_list;
                        $scope.documentUpload = true;
                    }
                    else {

                        $scope.documentNotUpload = true;
                    }
                });
                
            }
            else {
                var url = "api/customerManagement/EscrowSummary";
                SocketService.getparams(url, paramsCustomerGID).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.escrowlist = resp.data.escrowSummary;
                    }
                });
                var uploadparams = {
                    allocationdtl_gid: allocationdtl_gid,
                    customer_gid: customer_gid
                }
                var url = "api/customerManagement/GetTrnSanctionDocumentUpload";
                SocketService.getparams(url, uploadparams).then(function (resp) {
                    if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                        $scope.upload_list = resp.data.upload_list;
                        $scope.documentUpload = true;
                    }
                    else {

                        $scope.documentNotUpload = true;
                    }
                });

            }

            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerdetails = resp.data;
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.customerCollateral = resp.data.collateraldtl;
                $scope.holdallocationlist = resp.data.holdallocation;
                $scope.U2CMovedallocation = resp.data.U2CMovedallocation;
                $scope.customerguarantorlist = resp.data.Guarantorsdtl;
                $scope.customerPromotorlist = resp.data.Promoterdtl;
                angular.forEach($scope.sanctiondetails, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid,
                        allocationdtl_gid: allocationdtl_gid
                    };
                    var url = 'api/allocationManagement/GetAllocateloanList';
                    SocketService.post(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanList;
                        value.expand = false;

                    });
                });
            });

         
            if (MyAllocation == "Y") {
                $scope.MyAllocationView = true;
            }
            else {
                $scope.AllocationSummary = true;
            }

            unlockUI();
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.escrowInfoView = function (escrow_gid, historyescrow) {
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
                if (historyescrow == 'Y')
                {
                    console.log('historyescrow ==');
                    var url = "api/customerManagement/HistoryEscrowView";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.escrowview = resp.data;
                    });
                }
                else {
                    var url = "api/customerManagement/EscrowView";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.escrowview = resp.data;
                    });
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
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
            
            frm.append('document_name', $scope.documentname);
            frm.append('document_type', $scope.txtdocument_type);
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
             
        }

        $scope.documentRMUpload = function () {

            if ($scope.uploadfrm != undefined) {
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

        $scope.SubmitPrevisitRMdocument = function () {
            if ($scope.documentuploadflag == "Y")
            {
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/visitReport/GetPreVisitRMUpload";
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.rmVisitReport');
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
                Notify.alert('Atleast Upload One Document..!', {
                    status: 'info',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
        }

        $scope.uploadcancel = function (tmp_documentGid) {
            var allocationupload = {
                tmp_documentGid: tmp_documentGid
            }

            var url = 'api/allocationManagement/AllocateUploadcancel';
            SocketService.getparams(url, allocationupload).then(function (resp) {
                $scope.uploadallocation_list = resp.data.upload_list;
                if (resp.data.upload_list == null) {
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

        //$scope.back = function () {
        //    $state.go('app.caseAllocation');
        //}
    }
})();
