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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationCreatecontroller', allocationCreatecontroller);

    allocationCreatecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function allocationCreatecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationCreatecontroller';

        activate();

        function activate() {
            lockUI();
            $scope.cbocustomergid = localStorage.getItem('allocation_customer_gid');
            if (localStorage.getItem('qualified_status') == 'Fresh')
            {
                $scope.div_visitinfo = false;
            }
            else {
                $scope.div_visitinfo = true;
            }
            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });

            var params = {
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }

            var url = "api/allocationManagement/getcreateallocatedtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customername = resp.data.customername;
                $scope.zonal_gid = resp.data.zonal_gid;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.state_gid = resp.data.state_gid;
                $scope.state_name = resp.data.state_name;
                $scope.district_gid = resp.data.district_gid;
                $scope.district_name = resp.data.district_name;
                $scope.ZonalRM_gid = resp.data.ZonalRM_gid;
                $scope.ZonalRMname = resp.data.ZonalRMname;
                $scope.assignedRM_gid = resp.data.assignedRM_gid;
                $scope.allocateRMname = resp.data.assigned_RM;
                $scope.customerlastvisitdtl = resp.data.customerlastvisit;
                
            });

            var url = "api/customer/Getcustomerdetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerdetails = resp.data;
            });
            var url = "api/customerManagement/Getsanctionloandetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiondetails = resp.data.sanctionloanList;
                $scope.sanctionDocument = resp.data.upload_list;
                var previstdocumentflag;
                if (resp.data.upload_list == null)
                {
                    $scope.previstdocumentflag = 'N';
                }
                else
                {
                    $scope.previstdocumentflag = 'Y';
                }
 
                angular.forEach($scope.sanctiondetails, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid
                    };
                    var url = 'api/customerManagement/GetloanListDetails';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanList;
                        value.expand = false;

                    });
                });
            });

            var url = 'api/raiseLegalSR/GetDemandNoticedtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.demandnotice_list = resp.data.demandnotice_list;
                $scope.demand_status = resp.data.demand_status;

            });
          
            var url = "api/customerManagement/getcustomerPromoter";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ppa_name = resp.data.PPAname;
                $scope.customerPromotorlist = resp.data.customerPromoter;
            });

            var url = "api/customerManagement/getcustomerGuarantors";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerguarantorlist = resp.data.customerGuarantors;
            });

            var url = "api/customerManagement/getCollateraldetail";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerCollateral = resp.data.customerCollateral;
            });
            var url = "api/customerManagement/EscrowSummary";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowlist = resp.data.escrowSummary;
                }
            });

            unlockUI();
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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

        $scope.onchangestate = function (cbostatename) {
            lockUI();
            var params = {
                state_gid: cbostatename
            }
            var url = "api/rmMapping/getdistrictdtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.districtdtl = resp.data.statedtl;
            });
            unlockUI();
        }

        $scope.onchangedistrict = function (cbodistrictgid) {
            lockUI();
            var params = {
                district_gid: cbodistrictgid
            }
            var url = "api/allocationManagement/getallocateRM";
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp.data.assigned_RM);
                if (resp.data.assigned_RM == "") {
                    $scope.allocateRMname = "";
                    $scope.assignedRM_gid = "";
                    $scope.ZonalRM_gid = "";
                    $scope.ZonalRMname = "";
                    alert('RM is Not Mapping for the Selected District');
                }
                else {
                    $scope.allocateRMname = resp.data.assigned_RM;
                    $scope.assignedRM_gid = resp.data.assignedRM_gid;
                    $scope.ZonalRM_gid = resp.data.ZonalRM_gid;
                    $scope.ZonalRMname = resp.data.ZonalRMname;
                }
            });
            unlockUI();
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
            });
        }

        $scope.cancelupload = function () {
            $scope.uploadfrm = "";
            $scope.txtdocument_type = "";
            $("#addExternalupload").val('');
        }

        $scope.AllocationSubmit = function () {
            if ($scope.previstdocumentflag == 'Y')
            {
                var params = {
                    customer_gid: localStorage.getItem('allocation_customer_gid'),
                    qualified_status: localStorage.getItem('qualified_status'),
                    customername: $scope.customername,
                    zonal_gid: $scope.zonal_gid,
                    state_gid: $scope.state_gid,
                    state_name: $scope.state_name,
                    district_gid: $scope.district_gid,
                    district_name: $scope.district_name,
                    assignedRM_gid: $scope.assignedRM_gid,
                    assigned_RM: $scope.allocateRMname,
                    ZonalRM_gid: $scope.ZonalRM_gid,
                    ZonalRMname: $scope.ZonalRMname,
                    Manual_Allocation: 'N'
                }
                lockUI();
                var url = "api/allocationManagement/PostCreateAllocation";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.caseAllocation');
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
            else
            {
                Notify.alert('Atleast Upload One Document to Allocate..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
           
        }

        $scope.cancel = function () {
            $state.go('app.caseAllocation');
        }
 
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationCreateDirect', allocationCreateDirect);

    allocationCreateDirect.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function allocationCreateDirect($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationCreateDirect';

        activate();

        function activate() {
            var url = "api/allocationManagement/getCustomerAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.customerdtl = resp.data.customerdtl;
            });
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
                console.log('string', customer_name);
                $scope.customer = customer_name;
                $scope.customer_gid = customer_gid;
                $scope.customer_list = null;


                var params = {
                    customer_gid: customer_gid
                }

                var url = "api/allocationManagement/GetRiskCustomerList";
                SocketService.getparams(url, params).then(function (resp) {
                    if(resp.data.status == false)
                    {
                        $scope.customer='';
                        alert(resp.data.message);
                    }
                    else {
                        var url = 'api/loan/customer_getheads';

                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.mdlheadsofcustomer = resp.data;
                            $scope.zonalHead = resp.data.zonalGid;
                            $scope.businessHead = resp.data.businessHeadGid;
                            $scope.clustermanager = resp.data.clustermanagerGid;
                            $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                            $scope.creditmgmt_name = resp.data.creditmanager_gid;
                            $scope.vertical_code = resp.data.vertical_code;
                            $scope.vertical_code = true;
                            $scope.sanctiondtl = resp.data.sanctiondtl;

                        });
                        $scope.onchangecustomer = true;

                        var url = "api/allocationManagement/getcreateallocatedtls";
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.zonal_gid = resp.data.zonal_gid;
                            $scope.zonal_name = resp.data.zonal_name;
                            $scope.state_gid = resp.data.state_gid;
                            $scope.state_name = resp.data.state_name;
                            $scope.district_gid = resp.data.district_gid;
                            $scope.district_name = resp.data.district_name;
                            $scope.ZonalRM_gid = resp.data.ZonalRM_gid;
                            $scope.ZonalRMname = resp.data.ZonalRMname;
                            $scope.assignedRM_gid = resp.data.assignedRM_gid;
                            $scope.allocateRMname = resp.data.assigned_RM;
                            $scope.qualified_status = resp.data.qualified_status;
                            console.log($scope.qualified_status);
                        });
                        var url = "api/customer/Getcustomerdetails";
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.customerdetails = resp.data;
                        });
                        var url = "api/customerManagement/Getsanctionloandetails";
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.sanctiondetails = resp.data.sanctionloanList;
                            $scope.sanctionDocument = resp.data.upload_list;
                            var previstdocumentflag;
                            if (resp.data.upload_list == null) {
                                $scope.previstdocumentflag = 'N';
                            }
                            else {
                                $scope.previstdocumentflag = 'Y';
                            }
                            angular.forEach($scope.sanctiondetails, function (value, key) {
                                var params = {
                                    sanction_gid: value.sanction_gid
                                };
                                var url = 'api/customerManagement/GetloanListDetails';
                                SocketService.getparams(url, params).then(function (resp) {
                                    value.loandetails = resp.data.loanList;
                                    value.expand = false;

                                });
                            });
                        });
                        var url = 'api/raiseLegalSR/GetDemandNoticedtl';

                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.demandnotice_list = resp.data.demandnotice_list;
                            $scope.demand_status = resp.data.demand_status;

                        });

                        var url = "api/customerManagement/getcustomerPromoter";
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.ppa_name = resp.data.PPAname;
                            $scope.customerPromotorlist = resp.data.customerPromoter;
                        });

                        var url = "api/customerManagement/getcustomerGuarantors";
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.customerguarantorlist = resp.data.customerGuarantors;
                        });
                        var url = "api/customerManagement/EscrowSummary";
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                $scope.escrowlist = resp.data.escrowSummary;
                            }
                        });
                        var url = "api/customerManagement/getCollateraldetail";
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.customerCollateral = resp.data.customerCollateral;
                        });
                    }
                });
            }
        }

        
        $scope.loandetails = function (sanction_gid, id) {
          
            var params = {
                sanction_gid: sanction_gid
            };
            var url = 'api/customerManagement/GetloanListDetails';
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp.data.loanList);
                //$scope.loanList = resp.data.loanList;
               
            });
            
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

            if ($scope.uploadfrm != "")
            {
                var url = 'api/allocationManagement/AllocateUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    console.log(resp);
                    $scope.uploadallocation_list = resp.data.upload_list;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_type = "";

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
            else
            {
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
            });
        }

        $scope.cancelupload = function () {
            $scope.uploadfrm = "";
            $scope.txtdocument_type = "";
            $("#addExternalupload").val('');
        }

        $scope.cancel = function () {
            $state.go('app.caseAllocation');
        }
      

        $scope.AllocationSubmit = function () {
            if ($scope.previstdocumentflag == 'Y')
            {
                var params = {
                    customer_gid: $scope.customer_gid,
                    qualified_status: $scope.qualified_status,
                    zonal_gid: $scope.zonal_gid,
                    state_gid: $scope.state_gid,
                    state_name: $scope.state_name,
                    district_gid: $scope.district_gid,
                    district_name: $scope.district_name,
                    assignedRM_gid: $scope.assignedRM_gid,
                    assigned_RM: $scope.allocateRMname,
                    ZonalRM_gid: $scope.ZonalRM_gid,
                    ZonalRMname: $scope.ZonalRMname,
                    customername: $scope.customer,
                    Manual_Allocation:'Y'
                }
                lockUI();
                var url = "api/allocationManagement/PostCreateAllocation";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.caseAllocation');
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
            else {
                Notify.alert('Atleast Upload One Document to Allocate..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationHistorycontroller', allocationHistorycontroller);

    allocationHistorycontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$modal', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService'];

    function allocationHistorycontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $modal, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationHistorycontroller';
        activate();

        function activate() {
            lockUI();
            $scope.MyZonalAllocationHistory = localStorage.getItem('MyZonalAllocationHistory');

            var customer_gid = {
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }
            var url = "api/allocationManagement/getAllocationHistory";
            SocketService.getparams(url, customer_gid).then(function (resp) {
                if (resp.data.overallhistoryallocationdtl == null) {
                    if (localStorage.getItem('MyZonalAllocationHistory') == "Y") {
                        $state.go('app.allocationZonalRM');
                    }
                    else {
                        $state.go('app.caseAllocation');
                    }
                    alert('No History for this selected Customer');
                    return

                }
                else {
                    $scope.allocationHistoryList = resp.data.overallhistoryallocationdtl;
                    $scope.customername = resp.data.overallhistoryallocationdtl[0].customername;
                    $scope.customer_urn = resp.data.overallhistoryallocationdtl[0].customer_urn;
                }

            });
            unlockUI();
        }


        $scope.ViewCancelReason = function (allocationdtl_gid, customername, customer_urn, allocate_external) {

            var modalInstance = $modal.open({
                templateUrl: '/reportCancelModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customername = customername;
                $scope.customer_urn = customer_urn;
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                if (allocate_external == 'Y') {
                    var url = "api/VisitReportCancel/GetExternalVisitCancelLog";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.visistreportcancel = resp.data.visistreportcancel;
                    });
                }
                else {
                    var url = "api/VisitReportCancel/GetVisitCancelLog";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.visistreportcancel = resp.data.visistreportcancel;

                    });
                }

                //var url = "api/allocationManagement/GetViewCancelReason";
                //SocketService.getparams(url, params).then(function (resp) {

                //    $scope.cancel_remarks = resp.data.cancel_remarks;
                //    $scope.created_date = resp.data.created_date;
                //    $scope.created_by = resp.data.created_by;
                //});
                $scope.ok = function () {
                    $modalInstance.close('closed');
                    activate();
                };


            }
        }

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

        $scope.historyback = function () {
            if (localStorage.getItem('MyZonalAllocationHistory') == "Y") {
                $state.go('app.allocationZonalRM');
            }
            else {
                $state.go('app.caseAllocation');
            }
        }

        $scope.observationviewreport = function (allocationdtl_gid, observation_reportgid) {

            if (observation_reportgid !== "") {
                localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
                localStorage.setItem('observation_reportgid', observation_reportgid);
                //$state.go('app.ZRMObservationReportView');
                $location.url('app/ZRMObservationReportView?allocationdtl_gid=' + allocationdtl_gid + '&?&observation_reportgid=' + observation_reportgid);
            }
            else {
                Notify.alert('Observation Report  Not Found..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                
            }
        }

        $scope.generetePDF = function (allocationdtl_gid) {

            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            };
            var url = 'api/zonalAllocation/visitReportpdfcontent';
            SocketService.getparams(url, params).then(function (resp) {


                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, filename);

                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }

            });


        }

        $scope.viewallocation = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $state.go('app.allocationHistoryView');
        }

        $scope.genereteATRPDF = function (observation_reportgid) {

            if (observation_reportgid !== "") {
            var params = {
                observation_reportgid: observation_reportgid

            };
            var url = 'api/ObservationReport/ATRReportpdfcontent';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, filename);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                unlockUI();
            });
        }
    else {
                Notify.alert('ATR PDF  Not Found..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                
    }

        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationHistoryView', allocationHistoryView);

    allocationHistoryView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function allocationHistoryView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationHistoryView';
        activate();

        function activate() {
            lockUI();
            var allocationdtl_gid = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
            }
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.zonal_name = resp.data.zonal_name;
                $scope.state_name = resp.data.state_name;
                $scope.district_name = resp.data.district_name;
                $scope.assigned_RM = resp.data.assigned_RM;
                $scope.customername = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.ZonalRMname = resp.data.ZonalRMname;
                $scope.clientName = resp.data.customername;
            });

            var url = "api/visitReport/GetAllocationLogDetail";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.scheduleList = resp.data.schedulelogdtl;
                $scope.calllogdtlList = resp.data.calllogdtl;

            });

            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
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
                        allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
                    };

                    var url = 'api/allocationManagement/GetAllocateloanList';
                    SocketService.post(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanList;
                        value.expand = false;
                    });
                });
            });

            var url = "api/customerManagement/HistoryEscrowSummary";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowlist = resp.data.escrowSummary;
                }
            });

            var url = "api/allocationManagement/getAllocationdocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                    $scope.upload_list = resp.data.upload_list;
                    $scope.documentUpload = true;
                }
                else {

                    $scope.documentNotUpload = true;
                }
            });

            var url = "api/visitReport/getvisitreportdtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
               
                $scope.visitreport_generateGid = resp.data.visitreport_generateGid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.txtbusiness_vintage = resp.data.business_vintage,
                $scope.cbotypeof_loan = resp.data.typeof_loanvertical,
                $scope.txtbusiness_sector = resp.data.business_sector,
                $scope.txtregister_address = resp.data.registeredoffice_address,
                $scope.cboriskcode = resp.data.risk_code,
                $scope.txtactual_address = resp.data.present_address,
                $scope.txtcontact_dtl1 = resp.data.contact_details1,
                $scope.txtcontact_dtl2 = resp.data.contact_details2,
                $scope.cborisk_reviewtype = resp.data.typeof_riskreview;
                $scope.txtlattitude = resp.data.visit_latitude;
                $scope.txtlongitude = resp.data.visit_longitude;
                //$scope.firstdisb_date = resp.data.relationship_startedfrom
                $scope.txtbusiness_client = resp.data.clientbusiness_vintage,
                $scope.txtprimary_chain = resp.data.primarysecondary_valuechain,
                $scope.cbogenetic_code = resp.data.geneticcode_complied,
                $scope.cboRMD_gid = resp.data.RMD_visitedGid,
                $scope.RMD_visitedname = resp.data.RMD_visitedname;
                $scope.txtPPA_name = resp.data.PPA_name;
                $scope.cbovisit_done = resp.data.visit_done,
                $scope.txtpurposeof_loan = resp.data.purpose_ofloan,
                $scope.txtrequestedloan_byclient = resp.data.requestedamount_byclient,
                $scope.txtsantionloan_bycredit = resp.data.sanctionedamount_byclient;
                $scope.txtdisbursement_amount = resp.data.disbursement_amount,
                $scope.txttotalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.cborepayment_track = resp.data.repayment_track,
                $scope.cbobasic_records = resp.data.basicrecords_maintain,
                $scope.txtturnover_lastfy = resp.data.turnover_lastFY,
                $scope.txtpresent_fysales = resp.data.presentFY_sales,
                $scope.txtdeferral_pendency = resp.data.deferral_pendency,
                $scope.txtadditional_funding = resp.data.adequacy_additionalfunding,
                $scope.txtcbototal_groups = resp.data.total_noofGroups,
                $scope.txtCBOgroup_funded = resp.data.CBOfunded_noofGroups,
                $scope.txtRMDvisit_groupcount = resp.data.RMD_visitgroups,
                $scope.txtassetverification_comment = resp.data.assetverification_createdoutofloan,
                $scope.txtsecurity_details = resp.data.assetverification_securitydtls,
                $scope.txtassetverification_mortagged = resp.data.assetverification_mortgaged,
                $scope.txtROCcreation = resp.data.assetverification_ROCcreation,
                $scope.txtbasicrecord_remarks = resp.data.basicrecords_remarks,
                $scope.txtpurpose_funding = resp.data.purposeof_funding,
                $scope.txt_utilisationdtls = resp.data.utilisation_details,
                $scope.txtadequacyloan_samunnati = resp.data.adequacy_loanamount,
                $scope.txtadequacyloan_impactassessment = resp.data.adequacy_impactassessment,
                $scope.txtportfolio_noofmember = resp.data.portfolio_noofmembers,
                $scope.txtportfolio_activemembers = resp.data.portfolio_activemembers,
                $scope.txtportfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.txtportfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.txtportfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.txtportfolio_overdueAmount = resp.data.overdue_amount,
                $scope.txtportfolio_fundingoverdue = resp.data.overdueaccount_funding,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtrepayment_trackremarks = resp.data.repayment_trackremarks,
                //$scope.txtloan_clientdate = resp.data.loan_clientdate,
                $scope.txtoverdue = resp.data.overdue,
                $scope.txtborrower_commitment = resp.data.borrower_commitment,
                $scope.txtpending_documentation = resp.data.pending_documentation,
                //$scope.txtasset_verification = resp.data.asset_verification,
                $scope.txtbriefdtls_client = resp.data.briefdtls_client,
                $scope.txtenduse_loan = resp.data.enduse_loan,
                //$scope.txtadequacy_loan = resp.data.adequacy_loan,
                $scope.txtoverall_remarks = resp.data.overall_remarks,
                $scope.txtPDD_compliance = resp.data.PDD_compliance,
                $scope.txtbriefrpt_financials = resp.data.briefrpt_financials,
                $scope.txtbriefrpt_process = resp.data.briefrpt_process,
                $scope.txtbriefrpt_customer = resp.data.briefrpt_customer,
                 $scope.txtvaluechain_mapanalysis = resp.data.valuechain_mapanalysis,
                $scope.txtcompetitorbusiness_segment = resp.data.competitorbusiness_segment;
                $scope.txtbriefrpt_learnings = resp.data.briefrpt_learnings,
                $scope.txtbriefrpt_valuechain = resp.data.briefrpt_valuechain,
                $scope.editvisittype = resp.data.editvisittype;
                if (resp.data.RM_name != null) {
                    $scope.relationship_managername = resp.data.RM_name
                }
                if (resp.data.constitution != null) {
                    $scope.constitution = resp.data.constitution
                }
                if (resp.data.credit_managername != null) {
                    $scope.credit_managername = resp.data.credit_managername;
                }
                if (resp.data.visit_date != null) {
                    var p = resp.data.visit_date.split(/\D/g)
                    $scope.visitdate = [p[2], p[1], p[0]].join("-");
                }
               
                if (resp.data.dealing_withsince != null) {
                    var p = resp.data.dealing_withsince.split(/\D/g)
                    $scope.txtincorporated_date = [p[2], p[1], p[0]].join("-");
                }
               
                if (resp.data.disbursement_date != null) {
                    var p = resp.data.disbursement_date.split(/\D/g)
                    $scope.txtdisbursement_date = [p[2], p[1], p[0]].join("-");
                }
               
            });

            var url = "api/visitReport/getvisitReportDocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreportdocument = resp.data.visitreportdocument;
            });

            var url = "api/visitReport/getvisitReportPhoto";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreportphoto = resp.data.visitreportphoto;
                unlockUI();
            });
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
                var url = "api/customerManagement/HistoryEscrowView";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.escrowview = resp.data;
                });

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

        $scope.downloads = function (val1, val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationManagementcontroller', allocationManagementcontroller);

    allocationManagementcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function allocationManagementcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationManagementcontroller';

        activate();

        function activate() {
            var url = "api/allocationManagement/getallocationSummary";
            SocketService.get(url).then(function (resp) {
                $scope.allocationList = resp.data.mappingdtl;
            });
        }

        $scope.createAllocation = function () {
            $state.go('app.allocationCreate');
        }

        $scope.viewallocation = function (allocationdtl_gid, customer_gid) {
            console.log(allocationdtl_gid, customer_gid);
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid)
            $state.go('app.allocationView');
        }
    }
})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationTransfercontroller', allocationTransfercontroller);

    allocationTransfercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function allocationTransfercontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationTransfercontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/allocationTransfer/gettransferSummary";
            SocketService.get(url).then(function (resp) {
                $scope.allocationtransferlist = resp.data.allocationtransferdtl;
                $scope.count_OverallTransfer = resp.data.count_OverallTransfer;
                $scope.count_pendingApproval = resp.data.count_mypendingApproval;
                $scope.count_Approved = resp.data.count_myApproved;
                $scope.count_rejected = resp.data.count_myrejected;
                if ($scope.allocationtransferlist == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.allocationtransferlist.length;
                    if ($scope.allocationtransferlist.length < 100) {
                        $scope.totalDisplayed = $scope.allocationtransferlist.length;
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

        $scope.initiateTransfer = function () {
            $state.go('app.allocationTransferInitiate');
        }

        $scope.TransferAllocationView = function (allocationdtl_gid, customer_gid,allocation_transfergid){
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('allocation_transfergid',allocation_transfergid);
            localStorage.setItem('MyApprovalPage', 'N');
            $state.go('app.transferApproval360');
        }
    }
})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationZonalCreate', allocationZonalCreate);

    allocationZonalCreate.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function allocationZonalCreate($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationZonalCreate';

        activate();
        function activate() {
            lockUI();
            $scope.cbocustomergid = localStorage.getItem('allocation_customer_gid');

            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });

            var params = {
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }

            var url = "api/allocationManagement/getcreateallocatedtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customername = resp.data.customername;
                $scope.zonal_gid = resp.data.zonal_gid;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.state_gid = resp.data.state_gid;
                $scope.state_name = resp.data.state_name;
                $scope.district_gid = resp.data.district_gid;
                $scope.district_name = resp.data.district_name;
                $scope.ZonalRM_gid = resp.data.ZonalRM_gid;
                $scope.ZonalRMname = resp.data.ZonalRMname;
                $scope.assignedRM_gid = resp.data.assignedRM_gid;
                $scope.allocateRMname = resp.data.assigned_RM;
                $scope.customerlastvisitdtl = resp.data.customerlastvisit;
            });

            var url = "api/customer/Getcustomerdetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerdetails = resp.data;
            });
            var url = "api/customerManagement/Getsanctionloandetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiondetails = resp.data.sanctionloanList;
                $scope.sanctionDocument = resp.data.upload_list;
                var previstdocumentflag;
                if (resp.data.upload_list == null) {
                    $scope.previstdocumentflag = 'N';
                }
                else {
                    $scope.previstdocumentflag = 'Y';
                }
                angular.forEach($scope.sanctiondetails, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid
                    };
                    var url = 'api/customerManagement/GetloanListDetails';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanList;
                        value.expand = false;

                    });
                });
            });

            var url = 'api/raiseLegalSR/GetDemandNoticedtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.demandnotice_list = resp.data.demandnotice_list;
                $scope.demand_status = resp.data.demand_status;

            });
            var url = "api/customerManagement/getcustomerPromoter";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerPromotorlist = resp.data.customerPromoter;
            });

            var url = "api/customerManagement/getcustomerGuarantors";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerguarantorlist = resp.data.customerGuarantors;
            });

            var url = "api/customerManagement/getCollateraldetail";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerCollateral = resp.data.customerCollateral;
            });
            var url = "api/customerManagement/EscrowSummary";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowlist = resp.data.escrowSummary;
                }
            });
            unlockUI();
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
        $scope.onchangestate = function (cbostatename) {
            lockUI();
            var params = {
                state_gid: cbostatename
            }
            var url = "api/rmMapping/getdistrictdtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.districtdtl = resp.data.statedtl;
            });
            unlockUI();
        }

        $scope.downloads = function (val1, val2) {
          DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.onchangedistrict = function (cbodistrictgid) {
            lockUI();
            var params = {
                district_gid: cbodistrictgid
            }
            var url = "api/allocationManagement/getallocateRM";
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp.data.assigned_RM);
                if (resp.data.assigned_RM == "") {
                    $scope.allocateRMname = "";
                    $scope.assignedRM_gid = "";
                    $scope.ZonalRM_gid = "";
                    $scope.ZonalRMname = "";
                    alert('RM is Not Mapping for the Selected District');
                }
                else {
                    $scope.allocateRMname = resp.data.assigned_RM;
                    $scope.assignedRM_gid = resp.data.assignedRM_gid;
                    $scope.ZonalRM_gid = resp.data.ZonalRM_gid;
                    $scope.ZonalRMname = resp.data.ZonalRMname;
                }
            });
            unlockUI();
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
                    console.log(resp);
                    $scope.uploadallocation_list = resp.data.upload_list;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_type = "";

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
            });
        }

        $scope.cancelupload = function () {
            $scope.uploadfrm = "";
            $scope.txtdocument_type = "";
            $("#addExternalupload").val('');
        }

        $scope.AllocationSubmit = function () {
            if ($scope.previstdocumentflag == 'Y')
            {
                lockUI();
                var params = {
                    customer_gid: localStorage.getItem('allocation_customer_gid'),
                    qualified_status: localStorage.getItem('qualified_status'),
                    zonal_gid: $scope.zonal_gid,
                    state_gid: $scope.state_gid,
                    state_name: $scope.state_name,
                    district_gid: $scope.district_gid,
                    district_name: $scope.district_name,
                    assignedRM_gid: $scope.assignedRM_gid,
                    assigned_RM: $scope.allocateRMname,
                    ZonalRM_gid: $scope.ZonalRM_gid,
                    ZonalRMname: $scope.ZonalRMname
                }
                var url = "api/allocationManagement/PostCreateAllocation";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.allocationZonalRM');
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
            else {
                Notify.alert('Atleast Upload One Document to Allocate..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            
        }

        $scope.cancel = function () {
            $state.go('app.allocationZonalRM');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationZonalCreateDirect', allocationZonalCreateDirect);

    allocationZonalCreateDirect.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function allocationZonalCreateDirect($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationZonalCreateDirect';

        activate();
        function activate() {
            var url = "api/zonalAllocation/getZonalCustomerAllocation";
            SocketService.get(url).then(function (resp) {
                $scope.customerdtl = resp.data.customerdtl;

            });

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
                console.log('string', customer_name);
                $scope.customer = customer_name;
                $scope.customer_gid = customer_gid;
                $scope.customer_list = null;


                var params = {
                    customer_gid: customer_gid
                }


                var url = 'api/loan/customer_getheads';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.mdlheadsofcustomer = resp.data;
                    $scope.zonalHead = resp.data.zonalGid;
                    $scope.businessHead = resp.data.businessHeadGid;
                    $scope.clustermanager = resp.data.clustermanagerGid;
                    $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                    $scope.creditmgmt_name = resp.data.creditmanager_gid;
                    $scope.vertical_code = resp.data.vertical_code;
                    $scope.vertical_code = true;
                    $scope.sanctiondtl = resp.data.sanctiondtl;

                });
                $scope.onchangecustomer = true;

                var url = "api/allocationManagement/getcreateallocatedtls";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.zonal_gid = resp.data.zonal_gid;
                    $scope.zonal_name = resp.data.zonal_name;
                    $scope.state_gid = resp.data.state_gid;
                    $scope.state_name = resp.data.state_name;
                    $scope.district_gid = resp.data.district_gid;
                    $scope.district_name = resp.data.district_name;
                    $scope.ZonalRM_gid = resp.data.ZonalRM_gid;
                    $scope.ZonalRMname = resp.data.ZonalRMname;
                    $scope.assignedRM_gid = resp.data.assignedRM_gid;
                    $scope.allocateRMname = resp.data.assigned_RM;
                    $scope.qualified_status = resp.data.qualified_status;
                    console.log($scope.qualified_status);
                });

                var url = "api/customer/Getcustomerdetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerdetails = resp.data;
                });
                var url = "api/customerManagement/Getsanctionloandetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sanctiondetails = resp.data.sanctionloanList;
                    $scope.sanctionDocument = resp.data.upload_list;
                    var previstdocumentflag;
                    if (resp.data.upload_list == null) {
                        $scope.previstdocumentflag = 'N';
                    }
                    else {
                        $scope.previstdocumentflag = 'Y';
                    }
                    angular.forEach($scope.sanctiondetails, function (value, key) {
                        var params = {
                            sanction_gid: value.sanction_gid
                        };
                        var url = 'api/customerManagement/GetloanListDetails';
                        SocketService.getparams(url, params).then(function (resp) {
                            value.loandetails = resp.data.loanList;
                            value.expand = false;

                        });
                    });
                });

                var url = 'api/raiseLegalSR/GetDemandNoticedtl';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.demandnotice_list = resp.data.demandnotice_list;
                    $scope.demand_status = resp.data.demand_status;

                });

                var url = "api/customerManagement/getcustomerPromoter";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.ppa_name = resp.data.PPAname;
                    $scope.customerPromotorlist = resp.data.customerPromoter;
                });

                var url = "api/customerManagement/getcustomerGuarantors";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerguarantorlist = resp.data.customerGuarantors;
                });
                var url = "api/customerManagement/EscrowSummary";
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.escrowlist = resp.data.escrowSummary;
                    }
                });
                var url = "api/customerManagement/getCollateraldetail";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerCollateral = resp.data.customerCollateral;
                });
            }
        }

    

        $scope.loandetails = function (sanction_gid, id) {

            var params = {
                sanction_gid: sanction_gid
            };
            var url = 'api/customerManagement/GetloanListDetails';
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp.data.loanList);
                //$scope.loanList = resp.data.loanList;

            });

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
                    console.log(resp);
                    $scope.uploadallocation_list = resp.data.upload_list;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_type = "";

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
            });
        }

        $scope.cancelupload = function () {
            $scope.uploadfrm = "";
            $scope.txtdocument_type = "";
            $("#addExternalupload").val('');
        }

        $scope.cancel = function () {
            $state.go('app.allocationZonalRM');
        }


        $scope.AllocationSubmit = function () {
            if ($scope.previstdocumentflag == 'Y') {
                var params = {
                    customer_gid: $scope.customer_gid,
                    qualified_status: $scope.qualified_status,
                    zonal_gid: $scope.zonal_gid,
                    state_gid: $scope.state_gid,
                    state_name: $scope.state_name,
                    district_gid: $scope.district_gid,
                    district_name: $scope.district_name,
                    assignedRM_gid: $scope.assignedRM_gid,
                    assigned_RM: $scope.allocateRMname,
                    ZonalRM_gid: $scope.ZonalRM_gid,
                    ZonalRMname: $scope.ZonalRMname
                }

                var url = "api/allocationManagement/PostCreateAllocation";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.allocationZonalRM');
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
            else {
                Notify.alert('Atleast Upload One Document to Allocate..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
           
        }
    }
})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('caseAllocation', caseAllocation);

    caseAllocation.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService','cmnfunctionService'];

    function caseAllocation($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'caseAllocation';

        activate();

        function activate() {
            lockUI();
            var url = "api/allocationManagement/GetOverallZonalPendingCount";
            SocketService.get(url).then(function (resp) {
                $scope.count_qualified = resp.data.count_qualified;
                $scope.count_currentallo = resp.data.count_current;
                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.count_completedallo = resp.data.count_completed;
                $scope.count_external = resp.data.count_external;
                $scope.count_breached = resp.data.count_breached;
                $scope.ADM_updatedby = resp.data.ADM_updatedby;
                $scope.ADM_updateddate = resp.data.ADM_updateddate;
                $scope.count_reportcancel = resp.data.count_reportchanges;
                $scope.count_unmatchedqualified = "500";

                var num1 = parseInt(resp.data.count_qualified);
                var num2 = parseInt($scope.count_unmatchedqualified);
                $scope.count_overall = num1 + num2;
                $scope.overallzonalcountdtl = resp.data.overallzonalcount;
                angular.forEach($scope.overallzonalcountdtl, function (value, key) {
                    var params = {
                        zonalmapping_gid: value.zonalmapping_gid
                    };

                    var url = 'api/allocationManagement/GetAllocationPendingCount';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.zonalwisecountdtl = resp.data.zonalwisecount;

                        value.expand = false;
                    });
                });
                unlockUI();
            });
        }

        $scope.showPopover = function (assigned_RM) {

            var params = {
                assigned_RM: assigned_RM
            }

            var url = 'api/RskDashboard/GetRMAllocateCountdtl';
            SocketService.getparams(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    $scope.customerstatusdtl = resp.data.customerstatusdtl;
                }
                else {
                    $scope.customerstatusdtl = 'No Record';
                    console.log('NO', $scope.customerstatusdtl);
                    $("[data-toggle=popover]").popover({
                        html: false,
                        content: function () {
                            return $('#popover-content').html();
                        }
                    });
                }
            });

        };

        $scope.Fresh = function () {
            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            $scope.cboQualifiedStatus = "Fresh";
            var url = "api/allocationManagement/GetQualifiedFreshAllocation";
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

        $scope.Revisit = function () {
            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            var url = "api/allocationManagement/GetQualifiedReVisitAllocation";
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

        $scope.all = function () {
            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            var url = "api/allocationManagement/GetQualifiedAllocationSummary";
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

        $scope.allunmatch = function () {
            lockUI();
            $scope.cbounmatchStatus = undefined;
            unlockUI();
        }
        $scope.unmatchedqualified = function () {
            lockUI();
            $scope.totalUnmatchedDisplayed = 100;
            var url = "api/allocationManagement/GetQualifiedUnmatched";
            SocketService.get(url).then(function (resp) {
                $scope.count_unmatchedqualified = resp.data.count_unmatchedqualified;
                $scope.unmatchedallocationList = resp.data.qualifiedallocation;
                if ($scope.unmatchedallocationList == null) {
                    $scope.totalUnmatched = 0;
                    $scope.totalUnmatchedDisplayed = 0;
                }
                else {
                    $scope.totalUnmatched = $scope.unmatchedallocationList.length;
                    if ($scope.unmatchedallocationList.length < 100) {
                        $scope.totalUnmatchedDisplayed = $scope.unmatchedallocationList.length;
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


        $scope.loadUnmatchedMore = function (pageUnmatchedcount) {

            if (pageUnmatchedcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageUnmatchedcount);
            if ($scope.totalUnmatchedDisplayed < $scope.unmatchedallocationList.length) {
                $scope.totalUnmatchedDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.unmatchedallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.qualified = function () {

            lockUI();
            $scope.totalQualifiedDisplayed = 100;
            var url = "api/allocationManagement/GetQualifiedFreshAllocation";
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

        $scope.current = function () {
            lockUI();
            $scope.totalCurrentDisplayed = 100;
            var url = "api/allocationManagement/GetCurrentAllocateSummary";
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
            var url = "api/allocationManagement/GetUpcomingAllocation";
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
            var url = "api/allocationManagement/GetBreachedAllocationSummary";
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
            var url = "api/allocationManagement/getcompletedAllocationSummary";
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
            var url = "api/allocationManagement/getExternalAllocationSummary";
            SocketService.get(url).then(function (resp) {
                $scope.count_external = resp.data.count_external;
                $scope.externalallocationList = resp.data.allocationdtl;
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
            if ($scope.totalExternalDisplayed < $scope.externalallocationList.length) {
                $scope.totalExternalDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.externalallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.reportcancel = function () {
            lockUI();
            $scope.totalReportDisplayed = 100;
            var url = "api/allocationManagement/GetCaseAllocaCancelChanges";
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
            if ($scope.totalReportDisplayed < $scope.reportcancelList.length) {
                $scope.totalReportDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.reportcancelList.length + " Records Only", "warning");
                return;
            }
        };

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

        $scope.lasvisitdate = function (customer_gid, customername, customer_urn) {
            var modalInstance = $modal.open({
                templateUrl: '/lastVisitDate.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.customername = customername;
                $scope.customer_urn = customer_urn;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                $scope.submitlastvisit = function () {
                    lockUI();

                    var visitdate = new Date();
                    visitdate.setFullYear($scope.visitdate.getFullYear());
                    visitdate.setMonth($scope.visitdate.getMonth());
                    visitdate.setDate($scope.visitdate.getDate());

                    var params = {
                        customer_gid: customer_gid,
                        lastvisit_date: visitdate
                    }

                    var url = "api/allocationManagement/postlastVisitDate";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.visitdate = "";
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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

        $scope.transferRM = function (allocationdtl_gid) {
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/RMTransfer.html',
                controller: ModalInstanceCtrl,
                size: 'md'
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
                    $scope.customerdtl = resp.data.customerdtl;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submittransfer = function () {
                    lockUI();
                    var transferTo = {
                        allocation_Gid: allocationdtl_gid,
                        transferred_to: $scope.employee_id
                    }
                    var url = "api/allocationManagement/postAllocationTransfer";
                    SocketService.post(url, transferTo).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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
                    //     Notify.alert("File format is not supported..!", {
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
            console.log(qualified_status)
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
                    $state.go('app.allocationCreate');
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



        $scope.createDirectAllocation = function () {
            var url = "api/allocationManagement/tmpAllocatedocumentclear";
            SocketService.get(url).then(function (resp) {
            });
            $state.go('app.allocationCreateDirect');
        }

        $scope.viewallocation = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('MyAllocation', 'N');
            localStorage.setItem('ZonalAllocationBack', 'N');
            localStorage.setItem('Allocated', 'N');
            $state.go('app.allocation360');
        }

        $scope.viewallocateddetails = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('MyAllocation', 'N');
            localStorage.setItem('Allocated', 'Y');
            localStorage.setItem('ZonalAllocationBack', 'N');
            $state.go('app.allocation360');
        }

        $scope.historyallocation = function (customer_gid) {
            localStorage.setItem('MyZonalAllocationHistory', 'N');
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $state.go('app.allocationHistorydetails');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customer2EscrowCreate', customer2EscrowCreate);

    customer2EscrowCreate.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function customer2EscrowCreate($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        var vm = this;
        vm.title = 'customer2EscrowCreate';
        var customer_gid;
        activate();

        function activate() {
            customer_gid = localStorage.getItem('customer_gid');
            $scope.customer_name = localStorage.getItem('customer_name');
            $scope.urn = localStorage.getItem('urn');
            $scope.customer_code = localStorage.getItem('customer_code');
          
            vm.calenderDisbursement = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opendisbursement = true;
            };
            vm.calenderTransaction = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openTransaction = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];


            var params = {
                customer_gid:customer_gid
            }
            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.sanctiondtl = resp.data.sanctiondtl;
                console.log(resp.data.sanctiondtl);

            });
        }

        $scope.escrowSubmit = function () {
          
            var params = {
                sanction_gid: $scope.cbosanction.sanction_Gid,
                customer_gid: customer_gid,
                disbursement_date:$scope.txtdisbursementDate,
                transaction_date: $scope.transactionDate,
                transactionref_no: $scope.transactionRefNo,
                escrow_account_no: $scope.escrow_accountno,
                dealer_name: $scope.dealername,
                master_account_no: $scope.master_accountno,
                amount: $scope.amount,
                beneficiary_customer_account_name: $scope.beneficiarycustomer_accountname,
                sender_customer_account_name: $scope.sendercustomer_accountname,
                sender_customer_account_no: $scope.sendercustomer_accountno,
                remittance_info: $scope.remittance,
                sender_branch_IFSC: $scope.sendbranch_ifsc,
                reference: $scope.reference,
                credit_time: $scope.creditTime,
                remarks: $scope.remarks
            };

            var url = "api/customerManagement/escrowCreate";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, 'success')
                    $state.go('app.Customer2EscrowSummary');

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });
            
        }
        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-IN');
            $scope.amount = output;

        }
        $scope.sanctionrefnochange = function (sanction_Gid) {
            var params = {
                sanction_gid: $scope.cbosanction.sanction_Gid
            }
            var url = 'api/loan/GetSanctionDate';
            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.sanctionDate = resp.data.sanctiondate;
                $scope.Sanction_Date = resp.data.Sanction_Date;
                $scope.facilitytype = resp.data.facility_type;
                $scope.facilitytype_gid = resp.data.facilitytype_gid;
            });
        }

        $scope.back=function()
        {
            $state.go('app.Customer2EscrowSummary');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customer2EscrowSummary', customer2EscrowSummary);

    customer2EscrowSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'SweetAlert'];

    function customer2EscrowSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, SweetAlert) {
        var vm = this;
        vm.title = 'customer2EscrowSummary';
        var customer_gid;
       
        activate();

        function activate() {
            $scope.IsCreate = false;
            $scope.IsView = false;
            customer_gid = localStorage.getItem('customer_gid');
            var params = {
                customer_gid: localStorage.getItem('customer_gid')
            }

            var url = "api/customerManagement/EscrowSummary";
            SocketService.getparams(url, params).then(function (resp) {
                if(resp.data.status==true)
                {
                    $scope.escrowlist = resp.data.escrowSummary;
                   
                }

            });

            var url = 'api/customer/Getcustomerdetails';

            lockUI();
            SocketService.getparams(url,params).then(function (resp) {

                if(resp.data.status==true)
                {
                    $scope.customerdetails = resp.data;
                    $scope.urn=resp.data.customer_urnedit;
                    $scope.customername = resp.data.customerNameedit;
                    $scope.customercode = resp.data. customerCodeedit;
                }
               
               
            });
            unlockUI();



            vm.calenderDisbursement = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opendisbursement = true;
            };
            vm.calenderTransaction = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openTransaction = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];


            var params = {
                customer_gid: customer_gid
            }
            var url = 'api/loan/customer_getheads';

            SocketService.getparams(url, params).then(function (resp) {

                $scope.sanctiondtl = resp.data.sanctiondtl;
               
            });
        }

        $scope.sanctionrefnochange = function (sanction_Gid) {
            if ($scope.cbosanction.sanction_Gid == undefined)
            {

            }
            else {
                var params = {
                    sanction_gid: $scope.cbosanction.sanction_Gid
                }
                var url = 'api/loan/GetSanctionDate';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.sanctionDate = resp.data.sanctiondate;
                    $scope.Sanction_Date = resp.data.Sanction_Date;
                    $scope.facilitytype = resp.data.facility_type;
                    $scope.facilitytype_gid = resp.data.facilitytype_gid;
                });
            }
             
           
           
        }

        $scope.escrowCreate=function()
        {
           $scope. IsCreate = true;
            //localStorage.setItem('customer_gid', customer_gid);
            //localStorage.setItem('urn', $scope.urn);
            //localStorage.setItem('customer_name', $scope.customername);
            //localStorage.setItem('customer_code', $scope.customercode);

            //$state.go('app.Customer2EscrowCreate');

        }
        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-IN');
            $scope.amount = output;

        }

        $scope.escrowSubmit = function () {

            var params = {
                sanction_gid: $scope.cbosanction.sanction_Gid,
                customer_gid: customer_gid,
                disbursement_date: $scope.txtdisbursementDate,
                transaction_date: $scope.transactionDate,
                transactionref_no: $scope.transactionRefNo,
                escrow_account_no: $scope.escrow_accountno,
                dealer_name: $scope.dealername,
                master_account_no: $scope.master_accountno,
                amount: $scope.amount,
                beneficiary_customer_account_name: $scope.beneficiarycustomer_accountname,
                sender_customer_account_name: $scope.sendercustomer_accountname,
                sender_customer_account_no: $scope.sendercustomer_accountno,
                remittance_info: $scope.remittance,
                sender_branch_IFSC: $scope.sendbranch_ifsc,
                reference: $scope.reference,
                credit_time: $scope.creditTime,
                remarks: $scope.remarks
            };

            var url = "api/customerManagement/escrowCreate";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.remarks = null;
                    $scope.cbosanction = '---Select---';
                    $scope.Sanction_Date = null;
                    $scope.facilitytype = null;
                    $scope.txtdisbursementDate = null;
                    $scope.transactionRefNo = null;
                    $scope.transactionDate = null;
                    $scope.dealername = '';
                    $scope.escrow_accountno = '';
                    $scope.master_accountno = '';
                    $scope.sendercustomer_accountname = '';
                    $scope.sendercustomer_accountno = '';
                    $scope.sendbranch_ifsc = '';
                    $scope.beneficiarycustomer_accountname = '';
                    $scope.reference = null;
                    $scope.remittance = '';
                    $scope.amount = '';
                    $scope.creditTime = '';
                    
                    Notify.alert(resp.data.message, 'success');
                    activate();
                   
                    //$state.go('app.Customer2EscrowSummary');
                   
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message)
                }
                activate();
            });

        }
        $scope.close=function()
        {
            $scope.IsCreate = false;
            this.remarks = null;
            this.cbosanction ='---Select---';
            this.Sanction_Date = null;
            this.facilitytype = null;
            this.txtdisbursementDate = null;
            this.transactionRefNo = null;
            this.transactionDate = null;
            this.dealername = '';
            this.escrow_accountno = '';
            this.master_accountno = '';
            this.sendercustomer_accountname = '';
            this.sendercustomer_accountno = '';
            this.sendbranch_ifsc = '';
            this.beneficiarycustomer_accountname = '';
            this.reference = null;
            this.remittance = '';
            this.amount = '';
            this.creditTime = '';
        }

        $scope.escrowView = function (escrow_gid)
        {
           
            $scope.IsView = true;
            var params = {
                escrow_gid: escrow_gid
            };
            var url = "api/customerManagement/EscrowView";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowview = resp.data;

                }


            });
           // localStorage.setItem('escrow_gid', escrow_gid);
            //$state.go('app.customer2EscrowView');
        }

        $scope.ViewClose=function()
        {
            $scope.IsView = false;
        }
        $scope.escrowDelete = function (val) {
            var params = {
                escrow_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Escrow ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/customerManagement/EscrowDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                            activate();
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

            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customer2EscrowView', customer2EscrowView);

    customer2EscrowView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams'];


    function customer2EscrowView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams) {

        $scope.title = 'customer2EscrowView';
        var escrow_gid;
        activate();

        function activate() {
            escrow_gid = localStorage.getItem("escrow_gid", escrow_gid);
            console.log(escrow_gid);
            var params = {
                escrow_gid: escrow_gid
            };
            var url = "api/customerManagement/EscrowView";
            SocketService.getparams(url, params).then(function (resp) {
                if(resp.data.status==true)
                {
                    $scope.escrowview = resp.data;
                   
                }
                

            });
        }

        $scope.back=function()
        {
            $state.go('app.Customer2EscrowSummary');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerManagement360controller', customerManagement360controller);

    customerManagement360controller.$inject = ['$rootScope', '$scope', '$state','$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams','SweetAlert'];

    function customerManagement360controller($rootScope, $scope, $state,$modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, SweetAlert) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerManagement360controller';

        activate();

        function activate() {
            $scope.MyCustomer = localStorage.getItem('MyCustomer');
            var params = {
                customer_gid: localStorage.getItem('customer_gid')
            }
            
            var url = "api/customer/Getcustomerdetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerdetails = resp.data;
              
            });

            var url = "api/customerManagement/getcustomerPromoter";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerPromotorlist = resp.data.customerPromoter;
               
            });
           
            var url = "api/customerManagement/getcustomerGuarantors";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerguarantorlist = resp.data.customerGuarantors;
            });

            var url = "api/customerManagement/getCollateraldetail";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerCollateral = resp.data.customerCollateral;
            });
 
        }
      
        $scope.addgurantor = function () {
            var modalInstance = $modal.open({
                templateUrl: '/GuarantorsAddModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.customerGuarantorsSubmit = function () {

                    var params = {
                        guarantors_name: $scope.txtGuarantorsName,
                        guarantor_age: $scope.txtAge,
                        networth: $scope.txtNetWorth,
                        basisofNW: $scope.txtBasisofNW,
                        customer_gid: localStorage.getItem('customer_gid')
                    }
                    var url = "api/customerManagement/postcustomerGuarantors";
                    SocketService.post(url, params).then(function (resp) {
                        console.log(resp);
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
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                }
            }
        }

        $scope.addpromotor = function () {
           
            var modalInstance = $modal.open({
                templateUrl: '/promotorAddModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.customerPromoterSubmit = function () {

                    var params = {
                        promoter_name: $scope.txtPromoterName,
                        designation: $scope.txtDesignation,
                        promoter_age: $scope.txtAge,
                        mobile: $scope.txtmobile,
                        customer_gid: localStorage.getItem('customer_gid')
                    }
                    
                    var url = "api/customerManagement/postcustomerPromoter";
                    SocketService.post(url, params).then(function (resp) {
                        
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            activate();
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
            }
        }

      

     

        $scope.deleteguarantor = function (customer2guarantor_gid) {
            var params = {
                customer2guarantor_gid: customer2guarantor_gid,
               
            }

            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = "api/customerManagement/postGuarantorsdetail";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        }

        $scope.deletepromotor = function (customer2promotor_gid) {
            var params = {
                customer2promotor_gid: customer2promotor_gid
            }

            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = "api/customerManagement/postPromoterdetail";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        }

        $scope.editpromotor = function (customer2promotor_gid) {
           
            var modalInstance = $modal.open({
                templateUrl: '/promotorModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer2promotor_gid: customer2promotor_gid
                }
                var url = "api/customerManagement/getPromoterdetail"
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtPromoterNameedit = resp.data.promoter_name
                    $scope.txtDesignationedit = resp.data.designation
                    $scope.txtAgeedit = resp.data.promoter_age
                    $scope.txtmobileedit = resp.data.mobile
                    
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.updatepromotor = function () {
                    var params = {
                        customer2promotor_gid: customer2promotor_gid,
                        promoter_name: $scope.txtPromoterNameedit,
                        designation: $scope.txtDesignationedit,
                        promoter_age: $scope.txtAgeedit,
                        mobile: $scope.txtmobileedit
                    }
                    console.log(params);
                    var url = "api/customerManagement/postcustomerPromoterEdit";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
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
            }
        }

        $scope.editguarantor = function (customer2guarantor_gid) {
           
            var modalInstance = $modal.open({
                templateUrl: '/gurantorModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer2guarantor_gid: customer2guarantor_gid
                }
                var url = "api/customerManagement/getGuarantorsdetail"
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtGuarantorsNameedit = resp.data.guarantors_name
                    $scope.txtAgeedit = resp.data.guarantor_age
                    $scope.txtNetWorthedit = resp.data.networth
                    $scope.txtBasisofNWedit = resp.data.basisofNW
                    console.log(resp);
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.updateguarantor = function () {
                    var params = {
                        customer2guarantor_gid: customer2guarantor_gid,
                        guarantors_name: $scope.txtGuarantorsNameedit,
                        guarantor_age: $scope.txtAgeedit,
                        networth: $scope.txtNetWorthedit,
                        basisofNW: $scope.txtBasisofNWedit
                    }
                    console.log(params);
                    var url = "api/customerManagement/postcustomerGuarantorsEdit";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
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
            }
        }

        $scope.back = function () {
            $state.go('app.customerManagement');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerSummaryManagementcontroller', customerSummaryManagementcontroller);

    customerSummaryManagementcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function customerSummaryManagementcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerSummaryManagementcontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/customerManagement/GetCustomerRMDetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customer_data = resp.data.customer_list;
                if ($scope.customer_data == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.customer_data.length;
                    if ($scope.customer_data.length < 100) {
                        $scope.totalDisplayed = $scope.customer_data.length;
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

        $scope.assignRM = function (customer_gid) {
            var params = {
                customer_gid: customer_gid
            }
            
            var modalInstance = $modal.open({
                templateUrl: '/assign_RMModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = 'api/newServiceTicket/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                var url = "api/customerManagement/getAssignRMdetail";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.districtdtl = resp.data.districtdtl;
                    $scope.state_name = resp.data.state_name;
                    $scope.cboassignedRM_gid = resp.data.assigned_RM;
                    $scope.ZonalRM_gid = resp.data.zonal_riskmanager;
                    $scope.cbodistrict_gid = resp.data.district_gid;
                    $scope.zonal_name = resp.data.zonal_name;
                    $scope.zonal_gid = resp.data.zonal_gid;
                    $scope.customer_name = resp.data.customer_name;
                    $scope.addressline1 = resp.data.addressline1;
                    $scope.addressline2 = resp.data.addressline2;
                    $scope.cboPPA_gid = resp.data.ppa_gid;
                });

                unlockUI();

                $scope.onchangedistrict = function (cbodistrictgid) {
                    console.log(cbodistrictgid);
                    lockUI();
                    var params = {
                        district_gid: cbodistrictgid
                    }
                    var url = "api/allocationManagement/getallocateRM";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.assigned_RM == "") {
                            $scope.cboassignedRM_gid = "";
                            $scope.ZonalRM_gid = "";
                            alert('RM is Not Mapping for the Selected District');
                        }
                        else {
                            $scope.cboassignedRM_gid = resp.data.assignedRM_gid;
                            $scope.ZonalRM_gid = resp.data.ZonalRM_gid;
                        }
                    });
                    unlockUI();
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submitmapping = function () {
                    lockUI();
                    var PPA_name = $('#ppa_name :selected').text();
                    var district_name = $('#cbodistrictname :selected').text();
                    var updatedtl = {
                        customer_gid: customer_gid,
                        district_gid: $scope.cbodistrict_gid,
                        district_name : district_name,
                        assignedRM_gid: $scope.cboassignedRM_gid,
                        ZonalRM_gid: $scope.ZonalRM_gid,
                        zonal_gid: $scope.zonal_gid,
                        PPA_gid: $scope.cboPPA_gid,
                        PPA_name: PPA_name
                    }
                   
                    var url = "api/rmMapping/postcustomerMappingdetails";
                    SocketService.post(url, updatedtl).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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

        $scope.EscrowCreate = function (customer_gid) {
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            $state.go('app.Customer2EscrowSummary')
        }

        $scope.customer360click = function (customer_gid) {
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            localStorage.setItem('MyCustomer', 'N');
            $state.go('app.customerManagement360');
        }

      
    }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dasRemitterBuyerscontroller', dasRemitterBuyerscontroller);

    dasRemitterBuyerscontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function dasRemitterBuyerscontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dasRemitterBuyerscontroller';

        activate();

        function activate() {
            var params = {
                customer_gid: val
            };
            $scope.remitterbuyersummary = function () {
                var url = "api/DASTracker/getremitterbuyers";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.remitterbuyerslist = resp.data.remitterbuyers;
                });
            }

        }

        $scope.selfbuyer = function () {
            $scope.showselfbuyer = true;
            $scope.showacknowledgedbuyer = false;
            $scope.showunacknowledgedbuyer = false;
            $scope.selfcustomername = customername;
        }

        $scope.acknowledgedbuyer = function () {
            $scope.showselfbuyer = false;
            $scope.showacknowledgedbuyer = true;
            $scope.showunacknowledgedbuyer = false;
            var url = "api/DASTracker/getacknowledgedbuyers";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.acknowledgedbuyers = resp.data.acknowledgedbuyers;
            });
        }

        $scope.unacknowledgedbuyer = function () {
            $scope.showselfbuyer = false;
            $scope.showunacknowledgedbuyer = true;
            $scope.showacknowledgedbuyer = false;
        }

        $scope.addremitterbuyer = function () {

            if ($scope.cboremitterbuyer == "Self") {
                var params = {
                    customer_gid: val,
                    remitter_status: $scope.cboremitterbuyer,
                    remitter_self: val
                }
            }
            else if ($scope.cboremitterbuyer == "Acknowledged buyer") {
                console.log($scope.acknowledgedbuyers_gid);
                if ($scope.acknowledgedbuyers_gid != undefined) {
                    var params = {
                        customer_gid: val,
                        remitter_status: $scope.cboremitterbuyer,
                        remitter_ackbuyersgid: $scope.acknowledgedbuyers_gid,
                        remitter_ackbuyers: $('#ackbuyersname :selected').text()
                    }
                }
                else {
                    alert('Select Acknowledged Buyer');
                    return;
                }

            }
            else {
                if ($scope.txtunacknowbuyers != "") {
                    var params = {
                        customer_gid: val,
                        remitter_status: $scope.cboremitterbuyer,
                        remitter_unackbuyers: $scope.txtunacknowbuyers,
                    }
                }
                else {
                    alert('Enter Unacknowledged Buyer');
                    return;
                }
            }

            var url = "api/DASTracker/addremitterbuyers";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $('#ackbuyersname').val(0);
                    $scope.acknowledgedbuyers_gid = "";
                    $scope.txtunacknowbuyers = "";
                    console.log($scope.acknowledgedbuyers_gid = "");
                    $scope.remitterbuyersummary();
                    unlockUI();
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

        $scope.deleteAcknowledgedbuyer = function (remitterbuyers_gid) {
            lockUI();
            var params = {
                remitterbuyers_gid: remitterbuyers_gid
            }
            console.log(params);
            var url = "api/DASTracker/deleteremitterbuyers"
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.remitterbuyersummary();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
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
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dasTrackercontroller', dasTrackercontroller);

    dasTrackercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', '$modal'];

    function dasTrackercontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, $modal) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dasTrackercontroller';

        activate();

        function activate() {
            var url = 'api/customer/customerdetail';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customer_data = resp.data.customer_list;
                unlockUI();
            });
        }



        //  Acknowledged buyers .............//

        $scope.ackbuyers = function (val) {
            var params = {
                customer_gid: val,
            };
            var modalInstance = $modal.open({
                templateUrl: '/AcknowledgedBuyerContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.acknowledgedbuyerssummary = function () {
                    var url = "api/DASTracker/getacknowledgedbuyers";
                    SocketService.getparams(url, params).then(function (resp) {
                        console.log(resp);
                        $scope.acknowledgedbuyers = resp.data.acknowledgedbuyers;
                    });

                }
                $scope.acknowledgedbuyerssummary();

                // Add Acknowledged buyers  ....//

                $scope.addacknowledgedbuyers = function () {
                    var addack = {
                        customer_gid: val,
                        acknowledged_buyers: $scope.txtackbuyers
                    };
                    console.log(addack);
                    var url = "api/DASTracker/addacknowledgedbuyers";
                    lockUI();
                    SocketService.post(url, addack).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtackbuyers = "";
                            $scope.acknowledgedbuyerssummary();
                            unlockUI();
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

                // Delete Acknowledged buyers  ....//

                $scope.deleteAcknowledgedbuyer = function (acknowledgedbuyers_gid) {
                    lockUI();
                    var deleteackbuyers = {
                        acknowledgedbuyers_gid: acknowledgedbuyers_gid
                    }
                    console.log(deleteackbuyers);
                    var url = "api/DASTracker/deleteacknowledgedbuyers";
                    SocketService.getparams(url, deleteackbuyers).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.acknowledgedbuyerssummary();
                            unlockUI();
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

                // Edit Acknowledged buyers  ....//
                $scope.editAcknowledgedbuyer = function (acknowledgedbuyers_gid) {
                    lockUI();
                    var editackbuyers = {
                        acknowledgedbuyers_gid: acknowledgedbuyers_gid,
                        acknowledged_buyers: $scope.txteditackbuyers
                    }
                    var url = "api/DASTracker/updateacknowledgedbuyers";
                    SocketService.post(url, editackbuyers).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.acknowledgedbuyerssummary();
                            unlockUI();
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


        //  Remitters .............//


        $scope.remitterbuyers = function (val, customername) {

            $state.go('app.dasRemitterBuyers');
        
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('documentationcontroller', documentationcontroller);

    documentationcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function documentationcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'documentationcontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/documentation/getdocumentationdtlList";
            SocketService.get(url).then(function (resp) {
                $scope.documentation_list = resp.data.documentationdtl;
                if ($scope.documentation_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.documentation_list.length;
                    if ($scope.documentation_list.length < 100) {
                        $scope.totalDisplayed = $scope.documentation_list.length;
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
            if ($scope.documentation_list != null)
            {
                if ($scope.totalDisplayed < $scope.documentation_list.length) {
                    $scope.totalDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.documentation_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.addDocument = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/addDocumentModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.documentformSubmit = function () {

                    var params = {
                        //documentation_refno: $scope.txtdocumentrefno,
                        documentation_name: $scope.txtdocumentationname,
                        documentation_type: $scope.txtdocumentationtype
                    }
                    lockUI();
                    var url = "api/documentation/postdocumentationdtls";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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

        $scope.editdocument = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/EditDocumentModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    customer2document_gid: val
                }
                var url = 'api/documentation/getdocumentationdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditdocumentrefno = resp.data.documentation_refno;
                    $scope.txteditdocumentationname = resp.data.documentation_name;
                    $scope.txteditdocumentationtype = resp.data.documentation_type;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.documentformUpdate = function () {
                    lockUI();
                    var params = {
                        documentation_name: $scope.txteditdocumentationname,
                        documentation_type: $scope.txteditdocumentationtype,
                        customer2document_gid: val
                    }
                    var url = "api/documentation/postdocumentationupdate";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            unlockUI();
                            activate();
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

        $scope.deleteDocumentation = function (val) {
            var params = {
                customer2document_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/documentation/documentationdelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
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

            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('documentCheckListController', documentCheckListController);

    documentCheckListController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function documentCheckListController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'documentCheckListController';

        activate();

        function activate() {
            $scope.photouploaded = false;
            $scope.upload = true;
            $scope.IsVisible = false;
            $scope.rskdocumentlist = false;
            $scope.customer2sanction_gid = localStorage.getItem('customer2sanction_gid');
            var params =
                {
                    customer2sanction_gid: $scope.customer2sanction_gid
                }
            var url = "api/sanction/GetIdasSanctionDocumentList";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.IsVisible = true;
                    $scope.idassanctiondocumentlist = resp.data.idassanctiondocument;
                }
            });

            var url = "api/sanction/GetRskSanctionDocumentList";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.rsksanctiondocumentlist = resp.data.rsksanctiondocument;
            });

            var url = "api/documentation/getrskdocumentationdtlList";

            SocketService.getparams(url, params).then(function (resp) {
                $scope.documentationlist = resp.data.documentationdtl;
            });

            var url = 'api/sanction/GetSanctionDtls';
            var params = {
                sanction_gid: $scope.customer2sanction_gid
            };

            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefno = resp.data.sanction_refno;
                $scope.SanctionDate = resp.data.sanction_date;
                $scope.SanctionAmount = resp.data.sanction_amount;
                $scope.FacilityType = resp.data.facility_type;
                $scope.customerName = resp.data.customername;
                $scope.Customerurn = resp.data.customer_urn;
                $scope.collateral_security = resp.data.collateral_security;
                $scope.zonalHeadName = resp.data.zonal_name;
                $scope.businessHeadName = resp.data.businesshead_name;
                $scope.clusterManager = resp.data.cluster_manager_name;
                $scope.creditManager = resp.data.creditmanager_name;
                $scope.relationshipmgmt = resp.data.relationshipmgmt_name;
                $scope.customercode = resp.data.customercode;
                $scope.verticalCode = resp.data.vertical_code;
                $scope.contactperson = resp.data.contactperson;
                $scope.mobileno = resp.data.mobileno;
                $scope.addressline1 = resp.data.addressline1;
                $scope.addressline2 = resp.data.addressline2;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.riskmanager = resp.data.riskmanager;
            });
        }

        $scope.back = function () {
            $state.go('app.sanctionManagement')
        }
        $scope.uploadidasdocument_sub = function (val, val1, name, document_gid) {
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

            //     if (IsValidExtension == false) {
            //         Notify.alert("File format is not supported..!", {
            //             status: 'danger',
            //             pos: 'top-center',
            //             timeout: 3000
            //         });
            //         return false;
            //     }

            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('document_gid', document_gid);
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            var url = "api/sanction/postUploadidasSanctionDocument"

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.document_type = '';
                    activate();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
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
        $scope.uploadidasdocument = function (val, val1, name, sanctiondocument_gid) {
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

            //     if (IsValidExtension == false) {
            //         Notify.alert("File format is not supported..!", {
            //             status: 'danger',
            //             pos: 'top-center',
            //             timeout: 3000
            //         });
            //         return false;
            //     }

            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('sanctiondocument_gid', localStorage.getItem('sanctiondocument_gid'));
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            var url = "api/sanction/postUploadSanctionDocument"

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.file_name = resp.data.file_name;
                    $scope.photouploaded = true;
                    $scope.upload = false;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
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
        $scope.uploaddocument = function (val, val1, name) {
            var frm = new FormData(); //docchecklist

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

            //     if (IsValidExtension == false) {
            //         Notify.alert("File format is not supported..!", {
            //             status: 'danger',
            //             pos: 'top-center',
            //             timeout: 3000
            //         });
            //         return false;
            //     }

            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('customer2sanction_gid', localStorage.getItem('customer2sanction_gid'));
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            var url = "api/sanction/postUploadSanctionDocument"

            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $("#addupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.Sanctiondocupload_list = resp.data.Sanctiondoc_upload;
                    $scope.photouploaded = true;
                    $scope.upload = false;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
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



        $scope.documentadd = function () {
            if ($scope.txtremarks == "" || $scope.txtremarks == undefined) {
                Notify.alert('Enter Remarks..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                return;
            }
            else {
                if ($scope.Sanctiondocupload_list.length > 0) {
                    lockUI();
                    $scope.customer2sanction_gid = localStorage.getItem('customer2sanction_gid');
                    var params = {
                        customer2document_gid: $scope.cbodocumentationname.customer2document_gid,
                        customer2sanction_gid: $scope.customer2sanction_gid,
                        customer_gid: $scope.customer_gid,
                        document_remarks: $scope.txtremarks,
                    }
                    var url = "api/sanction/postsanctiondocument";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            // $scope.Sanctiondoc_upload_list = resp.data.Sanctiondoc_upload;
                            $scope.txtremarks = "";
                            $("#addrskupload").val('');
                            $scope.test_document = "";
                            $scope.file_name = undefined;
                            $scope.cbodocumentationname = "";
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                            activate();
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
                else {
                    Notify.alert('Kindly Upload Document..!', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return;
 
                }

            }


        }


        $scope.docdelete = function (document_gid) {
            var params = {
                document_gid: document_gid
            };

            lockUI();
            var url = "api/sanction/UploadDocDelete";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
            unlockUI();
        }



        $scope.downloads = function (val1, val2) { 
            DownloaddocumentService.Downloaddocument(val1, val2); 
        }




    }

})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('exclusionCustomerList', exclusionCustomerList);

    exclusionCustomerList.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function exclusionCustomerList($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'exclusionCustomerList';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/ExclusionList/GetExclusionSummary";
            SocketService.get(url).then(function (resp) {
                $scope.exclusioncustomerList = resp.data.exclusioncustomer;
                if ($scope.exclusioncustomerList == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.exclusioncustomerList.length;
                    if ($scope.exclusioncustomerList.length < 100) {
                        $scope.totalDisplayed = $scope.exclusioncustomerList.length;
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
            if ($scope.exclusioncustomerList != null) {
                if ($scope.totalDisplayed < $scope.exclusioncustomerList.length) {
                    $scope.totalDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.exclusioncustomerList.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.export = function () {

            lockUI();
            var url = 'api/ExclusionList/GetExclusionExport';

            SocketService.get(url).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.excel_path, resp.data.excel_name);
                    // var phyPath = resp.data.excel_path;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.excel_name.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

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

        $scope.exclusionHistory = function (customer_urn, customername, qualified_status) {

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
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;
                $scope.customer_status = qualified_status;
                var params = {
                    customer_urn: customer_urn
                }
                var url = "api/zonalAllocation/GetExclusionCustomerHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.exclusioncustomerHistory = resp.data.exclusionhistory;

                });
            }
        }

        $scope.activateexclusion = function (customer_urn, customername, qualified_status) {

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
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;
                $scope.customer_status = qualified_status;
                $scope.confirmActivation = function () {
                    lockUI();
                    var params = {
                        customer_urn: customer_urn,
                        exclusion_reason: $scope.txtactivated_reason
                    }
                    var url = "api/zonalAllocation/GetActivationCustomer";
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('exclusionZonalCustomerList', exclusionZonalCustomerList);

    exclusionZonalCustomerList.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function exclusionZonalCustomerList($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'exclusionZonalCustomerList';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/zonalAllocation/GetExclusionZonalSummary";
            SocketService.get(url).then(function (resp) {
                $scope.exclusioncustomerList = resp.data.exclusioncustomer;
                if ($scope.exclusioncustomerList == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.exclusioncustomerList.length;
                    if ($scope.exclusioncustomerList.length < 100) {
                        $scope.totalDisplayed = $scope.exclusioncustomerList.length;
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
        $scope.export = function () {
            
            lockUI();
            var url = 'api/ExclusionList/GetExclusionZonalExport';

            SocketService.get(url).then(function (resp) {
               
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.excel_path, resp.data.excel_name);
                    // var phyPath = resp.data.excel_path;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.excel_name.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

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

        $scope.exclusionHistory = function (customer_urn, customername, qualified_status) {

            var modalInstance = $modal.open({
                templateUrl: '/exclusionHistoryPopup.html',
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
                $scope.customer_status = qualified_status;
                var params = {
                    customer_urn: customer_urn
                }
                var url = "api/zonalAllocation/GetExclusionCustomerHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.exclusioncustomerHistory = resp.data.exclusionhistory;

                });
            }
        }

        $scope.activateexclusion = function (customer_urn, customername, qualified_status) {

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
                $scope.customer_urn = customer_urn;
                $scope.customername = customername;
                $scope.customer_status = qualified_status;
                $scope.confirmActivation = function () {
                    lockUI();
                    var params = {
                        customer_urn: customer_urn,
                        exclusion_reason: $scope.txtactivated_reason
                    }
                    var url = "api/zonalAllocation/GetActivationCustomer";
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('externalRegisterAddcontroller', externalRegisterAddcontroller);

    externalRegisterAddcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function externalRegisterAddcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'externalRegisterAddcontroller';

        activate();

        function activate() {
            $scope.photouploaded = false;
            $scope.upload = true;
            var url = "api/externalVendor/tmpexternalphotoclear";
            SocketService.get(url).then(function (resp) {
                
            });

            var url = "api/rmMapping/getstatedtls";
            SocketService.get(url).then(function (resp) {
                $scope.statedtl = resp.data.statedtl;

            });
        }

        $scope.onchangestate = function (cbostategid) {
            lockUI();
            var params = {
                state_gid: cbostategid
            }
            var url = "api/rmMapping/getdistrictdtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.districtdtl = resp.data.statedtl;
            });
            unlockUI();
        }

        $scope.upload = function (val, val1, name) {

            var frm = new FormData();

            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "photoformatonly");

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
            // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "photoformatonly");

            //     if (IsValidExtension == false) {
            //         Notify.alert("File format is not supported..!", {
            //             status: 'danger',
            //             pos: 'top-center',
            //             timeout: 3000
            //         });
            //         $("#addupload").val('');

            //         return false;

            //     }
            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('customer_gid', localStorage.getItem('customer_gid'))
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            var url = "api/externalVendor/ExternalphotoUpload";
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.Rskexternalvendordoc;
                $scope.photouploaded = true;
                $scope.upload = false;

                
            });
            $("#addupload").val('');
        }

        $scope.registrationsubmit = function () {
            lockUI();
            var params = {
                external_vendorcode: $scope.txtexternal_vendorCode,
                external_vendorname: $scope.txtexternal_vendorName,
                contact_person: $scope.txtcontact_person,
                contact_emailid: $scope.txtemail_ID,
                contact_number: $scope.txtcontact_Number,
                address_line1: $scope.txtaddress_line1,
                address_line2: $scope.txtaddress_line2,
                state_gid: $scope.cbostategid,
                district_gid: $scope.cbodistrictgid,
                country_name: $scope.txtcountry,
                postal_code: $scope.txtpostalCode
            }

            var url = "api/externalVendor/postexternalRegistration";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    //var url = "api/externalVendor/externalphoto";
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.externalRegister');
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

        $scope.cancel = function () {
            $state.go('app.externalRegister');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('externalRegistercontroller', externalRegistercontroller);

    externalRegistercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function externalRegistercontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'externalRegistercontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            lockUI();
            var url = "api/externalVendor/getexternalRegistersummary";
            SocketService.get(url).then(function (resp) {
                $scope.externalVendorList = resp.data.externalvendordtl;
                if ($scope.externalVendorList == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.externalVendorList.length;
                    if ($scope.externalVendorList.length < 100) {
                        $scope.totalDisplayed = $scope.externalVendorList.length;
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

        $scope.externalRegister = function () {
            $state.go('app.externalRegisterAdd');
        }

        $scope.viewExternalRegister = function (externalregister_gid) {
            localStorage.setItem('externalregister_gid', externalregister_gid);
            $state.go('app.externalRegisterView');
        }

        $scope.editExternalRegister = function (externalregister_gid) {
            localStorage.setItem('externalregister_gid', externalregister_gid);
            $state.go('app.externalRegisterEdit');
        }

        $scope.logincreation = function (externalregister_gid) {

            var params = {
                externalregister_gid: externalregister_gid
            }


            var modalInstance = $modal.open({
                templateUrl: '/lawyerLoginContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                var url = 'api/externalVendor/getexternallogindtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.external_Vendorname = resp.data.external_Vendorname;
                    $scope.external_vendorCode = resp.data.external_vendorCode;
                });

                $scope.sendaccouncreate = function () {
                    var params = {
                        external_vendorCode: $scope.external_vendorCode,
                        external_vendorPassword: $scope.external_vendorpassword,
                        externalregister_gid: externalregister_gid
                    }
                    var url = "api/externalVendor/postExternalLogin";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        console.log(resp.data.status);
                        if (resp.data.status == true) {

                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }

                    });
                }
            }
        }


        $scope.viewlogin = function (externalregister_gid) {

            var params = {
                externalregister_gid: externalregister_gid
            }


            var modalInstance = $modal.open({
                templateUrl: '/ViewlawyerLoginContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.close = function () {
                    $modalInstance.close('closed');
                }

                var url = 'api/externalVendor/getexternallogindtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.external_Vendorname = resp.data.external_Vendorname;
                    $scope.external_vendorCode = resp.data.external_vendorCode;
                    $scope.external_vendorPassword = resp.data.external_vendorPassword;
                    $scope.loginuserstatus = resp.data.external_activeStatus;
                });

                $scope.sendaccounstatus = function () {
                    var params = {
                        external_activeStatus: $scope.loginuserstatus,
                        externalregister_gid: externalregister_gid
                    }
                    var url = "api/externalVendor/postExternalLoginStatus";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            activate();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('externalregisterViewEdit', externalregisterViewEdit);

    externalregisterViewEdit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function externalregisterViewEdit($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'externalregisterViewEdit';

        activate();

        function activate() {
            lockUI();
            var params = {
                externalregister_gid: localStorage.getItem('externalregister_gid')
            }
            var url = "api/externalVendor/getexternalRegisterdtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtexternal_vendorcode = resp.data.external_vendorcode;
                $scope.txtexternal_vendorName = resp.data.external_vendorname;
                $scope.txtcontact_person = resp.data.contact_person;
                $scope.txtemail_ID = resp.data.contact_emailid;
                $scope.txtcontact_Number = resp.data.contact_number;
                $scope.txtaddress_line1 = resp.data.address_line1;
                $scope.txtaddress_line2 = resp.data.address_line2;
                $scope.state_name = resp.data.state_name;
                $scope.cbostategid = resp.data.state_gid;
                $scope.cbodistrict_gid = resp.data.district_gid;
                $scope.district_name = resp.data.district_name;
                $scope.txtcountry = resp.data.country_name;
                $scope.txtpostalCode = resp.data.postal_code;
                
                if (resp.data.photo_path != "N") {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.photo_path;
                    var str = str.split("StoryboardAPI");
                    var relpath1 = str[1].replace("\\", "/");
                    $scope.photo_path = url.concat(relpath1); 
                }
                else {
                    $scope.photo_path = resp.data.photo_path;
                }
                var state_gid = {
                    state_gid: resp.data.state_gid
                }
                var url = "api/rmMapping/getdistrictdtls";
                SocketService.getparams(url, state_gid).then(function (resp) {
                    $scope.districtdtl = resp.data.statedtl;
                });

            });

            var url = "api/rmMapping/getstatedtls";
            SocketService.get(url).then(function (resp) {
                $scope.statedtl = resp.data.statedtl;
            });
            unlockUI();
        }

        $scope.onchangestate = function (cbostategid) {
            lockUI();
            var params = {
                state_gid: cbostategid
            }
            var url = "api/rmMapping/getdistrictdtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.districtdtl = resp.data.statedtl;
            });
            unlockUI();

        }

        $scope.registrationUpdate = function () {
            var params = {
                external_vendorcode: $scope.txtexternal_vendorcode,
                external_vendorname: $scope.txtexternal_vendorName,
                contact_person: $scope.txtcontact_person,
                contact_emailid: $scope.txtemail_ID,
                contact_number: $scope.txtcontact_Number,
                address_line1: $scope.txtaddress_line1,
                address_line2: $scope.txtaddress_line2,
                state_gid: $scope.cbostategid,
                district_gid: $scope.cbodistrict_gid,
                country_name: $scope.txtcountry,
                postal_code: $scope.txtpostalCode,
                externalregister_gid: localStorage.getItem('externalregister_gid')
            }

            var url = "api/externalVendor/updateexternalRegistration";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.externalRegister');
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

        $scope.cancel = function () {
            $state.go('app.externalRegister');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myZonalCustomercontroller', myZonalCustomercontroller);

    myZonalCustomercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function myZonalCustomercontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myZonalCustomercontroller';

        activate();

        function activate() {
            var url = 'api/MyCustomer/GetZonalCustomerRMDetail';
            lockUI();
            SocketService.get(url).then(function (resp) {

                $scope.customer_data = resp.data.customer_list;
                unlockUI();
            });

        }
        $scope.assignRM = function (customer_gid) {
            var params = {
                customer_gid: customer_gid
            }

            var modalInstance = $modal.open({
                templateUrl: '/assign_RMModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = 'api/newServiceTicket/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                var url = "api/customerManagement/getAssignRMdetail";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.districtdtl = resp.data.districtdtl;
                    $scope.state_name = resp.data.state_name;
                    $scope.cboassignedRM_gid = resp.data.assigned_RM;
                    $scope.ZonalRM_gid = resp.data.zonal_riskmanager;
                    $scope.cbodistrict_gid = resp.data.district_gid;
                    $scope.zonal_name = resp.data.zonal_name;
                    $scope.zonal_gid = resp.data.zonal_gid;
                    $scope.customer_name = resp.data.customer_name;
                    $scope.addressline1 = resp.data.addressline1;
                    $scope.addressline2 = resp.data.addressline2;
                    $scope.cboPPA_gid = resp.data.ppa_gid;
                });

                unlockUI();

                $scope.onchangedistrict = function (cbodistrictgid) {
                    console.log(cbodistrictgid);
                    lockUI();
                    var params = {
                        district_gid: cbodistrictgid
                    }
                    var url = "api/allocationManagement/getallocateRM";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.assigned_RM == "") {
                            $scope.cboassignedRM_gid = "";
                            $scope.ZonalRM_gid = "";
                            alert('RM is Not Mapping for the Selected District');
                        }
                        else {
                            $scope.cboassignedRM_gid = resp.data.assignedRM_gid;
                            $scope.ZonalRM_gid = resp.data.ZonalRM_gid;
                        }
                    });
                    unlockUI();
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submitmapping = function () {
                    lockUI();
                    var PPA_name = $('#ppa_name :selected').text();
                    var district_name = $('#cbodistrictname :selected').text();
                    var updatedtl = {
                        customer_gid: customer_gid,
                        district_gid: $scope.cbodistrict_gid,
                        district_name: district_name,
                        assignedRM_gid: $scope.cboassignedRM_gid,
                        ZonalRM_gid: $scope.ZonalRM_gid,
                        zonal_gid: $scope.zonal_gid,
                        PPA_gid: $scope.cboPPA_gid,
                        PPA_name: PPA_name
                    }
                    var url = "api/rmMapping/postcustomerMappingdetails";
                    SocketService.post(url, updatedtl).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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

        $scope.customer360click = function (customer_gid) {
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            localStorage.setItem('MyCustomer', 'Y');
            $state.go('app.customerManagement360');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('observationReportApproval', observationReportApproval);

    observationReportApproval.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function observationReportApproval($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'observationReportApproval';

        activate();

        function activate() {
            var params = {
                observation_reportgid: localStorage.getItem('observation_reportgid')
            }
            var url = "api/ObservationReport/GetViewObservationdtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.dateof_RMDvisit = resp.data.dateof_RMDvisit;
                $scope.report_pertainingto = resp.data.report_pertainingto;
                $scope.vertical = resp.data.vertical;
                $scope.disbursement_amount = resp.data.disbursement_amount;
                $scope.approving_authority = resp.data.approving_authority;
                $scope.loansanction_date = resp.data.loansanction_date;
                $scope.relationship_manager_name = resp.data.relationship_manager_name;
                $scope.PPA_name = resp.data.PPA_name;
                $scope.RMDvisit_officialname = resp.data.RMDvisit_officialname;
                $scope.loandisbursement_date = resp.data.loandisbursement_date;
                $scope.people_accompaniedRMD = resp.data.people_accompaniedRMD;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.outstanding_amount = resp.data.outstanding_amount;
                $scope.current_DPD = resp.data.current_DPD;
                $scope.contact_details1 = resp.data.contact_details1;
                $scope.contact_details2 = resp.data.contact_details2;
                $scope.observation_flag = resp.data.observation_flag;
                $scope.cboriskcode = resp.data.risk_code;
                //$scope.riskcode_classification = resp.data.riskcode_classification;
            });

            var url = "api/ObservationReport/GetViewObservationCriticalDtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.criticalobservation = resp.data.criticalobservation;
            });
        }

        $scope.ObservationRemarks = function (critical_observationgid, criteria, RMD_observations, actionable_recommended, relationship_manager_remarks) {

            var modalInstance = $modal.open({
                templateUrl: '/criticalobservationModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtcriteria = criteria;
                $scope.txtRMD_observations = RMD_observations;
                $scope.txtactionable_recommend = actionable_recommended;
                $scope.txtrelationshipmangerremarks = relationship_manager_remarks;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.observationremarkssubmit = function () {
                    var params = {
                        critical_observationgid: critical_observationgid,
                        relationshipmanager_remarks: $scope.txtrelationshipmangerremarks,
                    }
                    var url = "api/ObservationReport/PostObservationCriticalRemarks"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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

        $scope.ObservationReportSubmit = function () {
            var params = {
                observation_reportgid: localStorage.getItem('observation_reportgid')
            }
            lockUI();
            var url = "api/ObservationReport/PostObservationRemarksSubmit";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.observationReportSummary');
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
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('observationReportSummary', observationReportSummary);

    observationReportSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function observationReportSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'observationReportSummary';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/ObservationReport/GetObservationReportSummary";
            SocketService.get(url).then(function (resp) {
                $scope.observationreport = resp.data.observationreport;
                $scope.count_pending = resp.data.count_pending;
                $scope.count_approved = resp.data.count_approved;
                if ($scope.observationreport == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.observationreport.length;
                    if ($scope.observationreport.length < 100) {
                        $scope.totalDisplayed = $scope.observationreport.length;
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
            if ($scope.observationreport != null) {
                if ($scope.totalDisplayed < $scope.observationreport.length) {
                    $scope.totalDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.observationreport.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.viewobservationReport = function (observation_reportgid) {
            localStorage.setItem('observation_reportgid', observation_reportgid);
            $state.go('app.observationReportApproval');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('reloadController', reloadController);

    reloadController.$inject = ['$state', 'ScopeValueService'];

    function reloadController($state, ScopeValueService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'reloadController';

        activate();

        function activate() {

            $state.go(ScopeValueService.get("dataldCtrl").current);
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmAllocationcontroller', rmAllocationcontroller);

    rmAllocationcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function rmAllocationcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmAllocationcontroller';

        activate();

        function activate() {

            var url = "api/allocationManagement/getRMallocateddetails";
            SocketService.get(url).then(function (resp) {
                $scope.allocatedList = resp.data.mappingdtl;
                console.log(resp);
            });
        }

        $scope.viewallocation = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('MyAllocation', 'Y');
            $state.go('app.allocationView');
        }

        $scope.genereteallocation = function (allocationdtl_gid, customer_gid)
        {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $state.go('app.visitReportGenerate');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmAllocationTransfer', rmAllocationTransfer);

    rmAllocationTransfer.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function rmAllocationTransfer($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmAllocationTransfer';

        activate();

        function activate() {
            lockUI();
            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = "api/rmMapping/getstatedtls";
            SocketService.get(url).then(function (resp) {
                $scope.statedtl = resp.data.statedtl;

            });

            var params = {
                assignedRM_gid: localStorage.getItem('assignedRM_gid')
            }
            var url = "api/allocationManagement/getassignedAllocation";

            SocketService.getparams(url, params).then(function (resp) {
                $scope.RMallocation = resp.data.mappingdtl;
            });
            unlockUI();
        }

        $scope.onchangestate = function (cbostatename) {
            lockUI();
            var params = {
                state_gid: cbostatename.state_gid
            }
            var url = "api/rmMapping/getdistrictdtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.districtdtl = resp.data.statedtl;
            });
            unlockUI();
        }

        $scope.onchangedistrict = function (cbodistrictgid) {
            lockUI();
            var params = {
                district_gid: cbodistrictgid.district_gid
            }
            console.log(params);
            var url = "api/allocationManagement/getallocateRM";
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp.data.assigned_RM);
                if (resp.data.assigned_RM == "") {
                    alert('RM is Not Mapping for the Selected District');
                }
                else {
                    $scope.allocateRMname = resp.data.assigned_RM;
                    $scope.assignedRM_gid = resp.data.assignedRM_gid;
                }
            });
            unlockUI();
        }


        $scope.checkall = function (selected) {
            //console.log(selected);
            angular.forEach($scope.RMallocation, function (val) {
                val.checked = selected;
            });
        }

        $scope.AllocationTransfer = function () {
            var allocationGidList = [];

            angular.forEach($scope.RMallocation, function (val) {

                if (val.checked == true) {
                    var allocationdtl_gid = val.allocationdtl_gid;
                    allocationGidList.push(allocationdtl_gid);
                }
            });

            if (Array.isArray(allocationGidList) && allocationGidList.length == 0)
            {
                alert('Select Atleast one Customer');
            }
            else {
                var params = {
                    allocationdtl_gid: allocationGidList,
                    transferred_to: $scope.cboemployeename.employee_id
                }

                var url = 'api/allocationManagement/postAllocationTransfer';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.rmTransfer');
                        activate();
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
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmMappingcontroller', rmMappingcontroller);

    rmMappingcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function rmMappingcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmMappingcontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/rmMapping/getmappingsummary";
            SocketService.get(url).then(function (resp) {
                $scope.mappingdtlList = resp.data.mappingdtl;
                if ($scope.mappingdtlList == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.mappingdtlList.length;
                    if ($scope.mappingdtlList.length < 100) {
                        $scope.totalDisplayed = $scope.mappingdtlList.length;
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

        $scope.addpincodeMapping = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/addMaping.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = "api/rmMapping/getstatedtls";
                SocketService.get(url).then(function (resp) {
                    $scope.statedtl = resp.data.statedtl;
                });

                var url = 'api/newServiceTicket/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                unlockUI();
                $scope.onselectedchange = function (state_gid) {
                    var params = {
                        state_gid: state_gid
                    }
                    var url = "api/rmMapping/getdistrictdtls";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.districtdtl = resp.data.statedtl;
                    });
                }


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submitmapping = function () {
                    var state_name = $('#cbostatename :selected').text();
                    var district_name = $('#cbodistrictname :selected').text();
                    var params = {
                        state_gid: $scope.cbostate_gid,
                        district_gid: $scope.cbodistrict_gid,
                        state_name: state_name,
                        district_name: district_name,
                        assigned_RM: $scope.cboemployeegid
                    }
                    lockUI();
                    var url = "api/rmMapping/postmappingdetails";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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


        $scope.editmappingdtl = function (val) {
            var params = {
                RMmapping_gid: val
            }

            var modalInstance = $modal.open({
                templateUrl: '/EditMaping.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();
                var url = 'api/newServiceTicket/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });
              
                var url = "api/rmMapping/getmappingdtl";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.district_name = resp.data.district_name;
                    $scope.state_name = resp.data.state_name;
                    $scope.employee_id = resp.data.assigned_RM;
                    $scope.ZonalRMname = resp.data.ZonalRMname;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.updatemapping = function () {
                    lockUI();
                    var updatedtl = {
                        RMmapping_gid: val,
                        assigned_RM: $scope.employee_id,
                        //ZonalRM_gid: $scope.zonal_employeegid
                    }
                    var url = "api/rmMapping/updatemappingdetails";
                    SocketService.post(url, updatedtl).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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


        $scope.deletemappingdtl = function (val) {
            var params = {
                RMmapping_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = "api/rmMapping/deletemappingdtl";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                }

            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmObservationReport', rmObservationReport);

    rmObservationReport.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function rmObservationReport($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmObservationReport';
        var allocationdtl_gid = $location.search().allocationdtl_gid;
        var observation_reportgid = $location.search().observation_reportgid;
        //console.log(observation_reportgid);
        activate();

        function activate() {

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            if (observation_reportgid != '' && observation_reportgid != "undefined") {
                $scope.subbutton = "N";
            }
            else {
                $scope.subbutton = "Y";
            }

            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            var url = "api/ObservationReport/GetViewObservationReportDtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.RMD_visitname = resp.data.RMD_visitname;
                $scope.RMD_visitGid = resp.data.RMD_visitGid;
                $scope.relationship_manager_gid = resp.data.relationship_manager_gid;
                $scope.relationship_manager_name = resp.data.relationship_manager_name;
                $scope.visit_date = resp.data.visit_date;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.disbursement_amount = resp.data.disbursement_amount;
                $scope.txtoutstanding_amount = resp.data.totalloan_outstanding;
                $scope.cboriskcode = resp.data.risk_code;
                //$scope.riskcode_classification = resp.data.riskcode_classification;
                if (resp.data.PPA_name != "")
                {
                    $scope.txtPPA_name = resp.data.PPA_name;
                }
                $scope.disbursement_date = resp.data.disbursement_date;
                $scope.txtcontact_details1 = resp.data.contact_details1;
                $scope.txtcontact_details2 = resp.data.contact_details2;
            });

            if (observation_reportgid != '' && observation_reportgid != "undefined") {
                var params = {
                    observation_reportgid: observation_reportgid
                }
                var url = "api/ObservationReport/GetViewObservationdtl";
                SocketService.getparams(url, params).then(function (resp) {
                   
                    $scope.dateof_RMDvisit = resp.data.dateof_RMDvisit;
                    $scope.txtreportpertaining = resp.data.report_pertainingto;
                    $scope.vertical = resp.data.vertical;
                    $scope.cboapproval_authority = resp.data.approving_authority;
                    $scope.txtsanction_date = resp.data.loansanction_date;
                    $scope.txtPPA_name = resp.data.PPA_name;
                    $scope.RMD_visitname = resp.data.RMDvisit_officialname;
                    $scope.loandisbursement_date = resp.data.loandisbursement_date;
                    $scope.txtpeopleaccompanied_RMD = resp.data.people_accompaniedRMD;
                    $scope.outstanding_amount = resp.data.outstanding_amount;
                    $scope.txtcurrent_DPD = resp.data.current_DPD;
                    $scope.txtcontact_details1 = resp.data.contact_details1;
                    $scope.txtcontact_details2 = resp.data.contact_details2;
                    $scope.observation_flag = resp.data.observation_flag;
                });

                var url = "api/ObservationReport/GetViewObservationCriticalDtl";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.criticalobservation = resp.data.criticalobservation;
                    $scope.trn_status = "Y";
                });
                unlockUI();
            }
            else {
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/ObservationReport/GetTmpCriticaldtl";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.criticalobservation = resp.data.criticalobservation;
                    $scope.trn_status = "N";
                    unlockUI();
                });
            }
    
        }


        $scope.observations = function (customer_name, customer_urn) {

            var modalInstance = $modal.open({
                templateUrl: '/criticalobservationModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.criticalobservationsubmit = function () {
                    var params = {
                        allocationdtl_gid: allocationdtl_gid,
                        observation_reportgid:observation_reportgid,
                        criteria: $scope.txtcriteria,
                        RMD_observations: $scope.txtRMD_observations,
                        actionable_recommended: $scope.txtactionable_recommend
                    }
                    lockUI();
                    var url = "api/ObservationReport/PostObservationCritical"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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

        $scope.trndeleteCriticalObservation = function (critical_observationgid)
        {
            var params = {
                critical_observationgid: critical_observationgid
            }
            var url = "api/ObservationReport/GetDeleteTrnCriticalObser"
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();
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


        $scope.deleteCriticalObservation = function (tmpcritical_observationgid) {
            var params = {
                tmpcritical_observationgid: tmpcritical_observationgid
            }
            var url = "api/ObservationReport/GetDeleteCriticalObser"
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();
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

        $scope.outstandingamountchange = function () {
            var input = document.getElementById('outstanding_amount').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtoutstanding_amount = "";
            }
            else {
                $scope.txtoutstanding_amount = output;
            }
        }

        $scope.observationreportSubmit = function () {

            var visitdate = $scope.visit_date;
            var visit_date = visitdate.split("-").reverse().join("-");

            var disbursementdate = $scope.disbursement_date;
            var disbursement_date = disbursementdate.split("-").reverse().join("-");

            var params = {
                observation_reportgid: observation_reportgid,
                customer_name: $scope.customer_name,
                customer_urn: $scope.customer_urn,
                risk_code: $scope.cboriskcode,
                //riskcode_classification: $scope.riskcode_classification,
                dateof_RMDvisit: visit_date,
                allocationdtl_gid: allocationdtl_gid,
                report_pertainingto: $scope.txtreportpertaining,
                vertical: $scope.vertical_code,
                disbursement_amount: $scope.disbursement_amount,
                approving_authority: $scope.cboapproval_authority,
                loansanction_date: $scope.txtsanction_date,
                relationship_manager_gid: $scope.relationship_manager_gid,
                relationship_manager_name: $scope.relationship_manager_name,
                PPA_name: $scope.txtPPA_name,
                RMDvisit_officialname: $scope.RMD_visitname,
                loandisbursement_date: disbursement_date,
                people_accompaniedRMD: $scope.txtpeopleaccompanied_RMD,
                sanction_amount: $scope.sanction_amount,
                outstanding_amount: $scope.txtoutstanding_amount,
                current_DPD: $scope.txtcurrent_DPD,
                contact_details1: $scope.txtcontact_details1,
                contact_details2: $scope.txtcontact_details2
            }
            lockUI();
            var url = "api/ObservationReport/PostObservationReport"
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.rmVisitReport');
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
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmObservationReportView', rmObservationReportView);

    rmObservationReportView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function rmObservationReportView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmObservationReportView';
        var allocationdtl_gid=$location.search().allocationdtl_gid;
        var observation_reportgid=$location.search().observation_reportgid;

       
        activate();

        function activate() {
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            var params = {
                observation_reportgid: observation_reportgid
            }
            var url = "api/ObservationReport/GetViewObservationdtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.dateof_RMDvisit = resp.data.dateof_RMDvisit;
                $scope.report_pertainingto = resp.data.report_pertainingto;
                $scope.vertical = resp.data.vertical;
                $scope.disbursement_amount = resp.data.disbursement_amount;
                $scope.approving_authority = resp.data.approving_authority;
                $scope.loansanction_date = resp.data.loansanction_date;
                $scope.relationship_manager_name = resp.data.relationship_manager_name;
                $scope.PPA_name = resp.data.PPA_name;
                $scope.RMDvisit_officialname = resp.data.RMDvisit_officialname;
                $scope.loandisbursement_date = resp.data.loandisbursement_date;
                $scope.people_accompaniedRMD = resp.data.people_accompaniedRMD;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.outstanding_amount = resp.data.outstanding_amount;
                $scope.current_DPD = resp.data.current_DPD;
                $scope.contact_details1 = resp.data.contact_details1;
                $scope.contact_details2 = resp.data.contact_details2;
                $scope.observation_flag = resp.data.observation_flag;
                $scope.txtATR_date = resp.data.atr_completiondate;
                $scope.riskcode = resp.data.risk_code;
                $scope.cboriskcode = resp.data.risk_code;
                $scope.observationriskcode = resp.data.risk_code;
              
            });

            var url = "api/ObservationReport/tmpTier1documentclear"
            SocketService.get(url).then(function (resp) {

            });

            var url = "api/ObservationReport/GetViewObservationCriticalDtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.criticalobservation = resp.data.criticalobservation;
            });

            var url = "api/ObservationReport/GetTier1FormatDtl";
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtobservations = resp.data.tier1_observations;
                if (resp.data.tier1_code == null || resp.data.tier1_code == "") {
                }
                else {
                    $scope.cboriskcode = resp.data.tier1_code;
                }
                $scope.txtrationale_justification = resp.data.tier1_justification;
                $scope.txtprocess_gap = resp.data.tier1_processgap;
                $scope.txtcode_changereason = resp.data.tier1code_changereason;
                $scope.tier1code_changeflag = resp.data.tier1code_changeflag;
                $scope.txtimprovement_recommendation = resp.data.tier1_processrecommendation;
                $scope.txtmanagement_comments = resp.data.tier1_managementcomments;
                $scope.txtcheifheadreverts_actionplan = resp.data.tier1_reverts_actionplan;
                //$scope.txtATR_date = resp.data.tier1_atrdate;
                $scope.tier1format_gid = resp.data.tier1format_gid;
                $scope.tier1_approvalstatus = resp.data.tier1_approvalstatus;
                $scope.tier1approvallog = resp.data.tier1approvallog;
                $scope.uploaddocument_list = resp.data.tier1doc;
                $scope.tier1rejectlog = resp.data.tier1rejectlog;
            
                if ($scope.tier1code_changeflag == 'Y') {
                    $scope.disablecodechangereasonshow = true;
                }
                else {
                    $scope.disablecodechangereasonshow = false;
                }

                if (resp.data.tier1approvallog == null) {
                    $scope.nohistoryapproval = true;
                }
                else {
                    $scope.historyapproval = true;
                }
                if (resp.data.tier1rejectlog == null) {
                    $scope.rejecthistory = false;
                }
                else {
                    $scope.rejecthistory = true;
                }

                if (($scope.tier1format_gid != null) && ($scope.tier1_approvalstatus == 'Approved')) {
                    $scope.observation_flag = "T";
                    $scope.doc = true;
                }
                else if (($scope.tier1format_gid != null) && ($scope.tier1_approvalstatus == 'Rejected')) {
                    $scope.observation_flag = "Y";
                    $scope.btnupdated = true;
                    $scope.doc = false;
                }
                else if (($scope.tier1format_gid != null) && ($scope.tier1_approvalstatus == 'Pending')) {
                    $scope.observation_flag = "T";
                    $scope.doc = true;
                }
                else {

                }

                if ($scope.observation_flag == 'Y' && $scope.tier1_approvalstatus == 'Rejected') {
                    $scope.btnupdate = true;
                }
                else if ($scope.observation_flag == 'Y') {
                    $scope.btnsubmit = true;

                }
                else {

                }

            });
        }
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.riskcodechange = function (cboriskcode) {
            if ($scope.riskcode == cboriskcode) {
                $scope.codechangereasonshow = false;

            }
            else {
                $scope.codechangereasonshow = true;

            }
        }

        $scope.tier1formatsubmit = function () {
            var lscode_changereason = $scope.txtcode_changereason;

            if ($scope.riskcode == $scope.cboriskcode) {
                //var lscode_changereason = '-';
                var date = $scope.txtATR_date;
                var ATR_date = date.split("-").reverse().join("-");

                var params = {
                    observation_reportgid: observation_reportgid,
                    allocationdtl_gid: allocationdtl_gid,
                    customer_name: $scope.customer_name,
                    customer_urn: $scope.customer_urn,
                    tier1_observations: $scope.txtobservations,
                    tier1_code: $scope.cboriskcode,
                    tier1code_changereason: lscode_changereason,
                    tier1code_changeflag: 'N',
                    tier1_justification: $scope.txtrationale_justification,
                    tier1_processgap: $scope.txtprocess_gap,
                    tier1_processrecommendation: $scope.txtimprovement_recommendation,
                    tier1_managementcomments: $scope.txtmanagement_comments,
                    tier1_reverts_actionplan: $scope.txtcheifheadreverts_actionplan,
                    tier1_atrdate: ATR_date
                    
                }

                lockUI();
                var url = "api/ObservationReport/PostTier1Format";
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.rmVisitReport');
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
            else {
                console.log($scope.riskcode == $scope.cboriskcode);
                if (lscode_changereason == "" || lscode_changereason == undefined) {
                    Notify.alert('Kindly Enter Risk Code - Change Reason', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    var date = $scope.txtATR_date;
                    var ATR_date = date.split("-").reverse().join("-");

                    var params = {
                        observation_reportgid:observation_reportgid,
                        allocationdtl_gid: allocationdtl_gid,
                        customer_name: $scope.customer_name,
                        customer_urn: $scope.customer_urn,
                        tier1_observations: $scope.txtobservations,
                        tier1_code: $scope.cboriskcode,
                        tier1code_changereason: lscode_changereason,
                        tier1code_changeflag: 'Y',
                        tier1_justification: $scope.txtrationale_justification,
                        tier1_processgap: $scope.txtprocess_gap,
                        tier1_processrecommendation: $scope.txtimprovement_recommendation,
                        tier1_managementcomments: $scope.txtmanagement_comments,
                        tier1_reverts_actionplan: $scope.txtcheifheadreverts_actionplan,
                        tier1_atrdate: ATR_date
                      
                    }

                    lockUI();
                    var url = "api/ObservationReport/PostTier1Format";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $state.go('app.rmVisitReport');
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

        $scope.tier1formatUpdate = function () {
            var date = $scope.txtATR_date;
            var ATR_date = date.split("-").reverse().join("-");
            var params = {
                tier1format_gid: $scope.tier1format_gid,
                tier1_observations: $scope.txtobservations,
                tier1_code: $scope.cboriskcode,
                tier1code_changereason: $scope.txtcode_changereason,
                tier1_justification: $scope.txtrationale_justification,
                tier1_processgap: $scope.txtprocess_gap,
                tier1_processrecommendation: $scope.txtimprovement_recommendation,
                tier1_managementcomments: $scope.txtmanagement_comments,
                tier1_reverts_actionplan: $scope.txtcheifheadreverts_actionplan,
                tier1_atrdate: ATR_date,
                tier1_rejectedremarks: $scope.txttier1_rejectremarks
            }

            lockUI();
            var url = "api/ObservationReport/PostUpdateTier1Format";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.rmVisitReport');
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

        $scope.uploaddocument = function (val, val1, name) {
            if ($scope.txtdocument_title == undefined || $scope.txtdocument_title == "") {
                alert('Enter the Document Title to Upload Document');
                $("#addExternalupload").val('');
                return false;
            }
            else {

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
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/TierMeeting/PostTier1Upload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.uploaddocument_list = resp.data.tier1document;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = "";
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.uploadflag = 'Y';
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.uploadflag = 'N';
                    }
                });
            }
        }

        $scope.uploadcancel = function (tmp_documentGid) {
            var tier1upload = {
                tmp_documentGid: tmp_documentGid
            }
            var url = 'api/TierMeeting/GetTier1UploadCancel';
            SocketService.getparams(url, tier1upload).then(function (resp) {
                $scope.uploaddocument_list = resp.data.upload_list;
            });
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmScheduleLogDetails', rmScheduleLogDetails);

    rmScheduleLogDetails.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$modal', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function rmScheduleLogDetails($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $modal, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmScheduleLogDetails';
        var  allocationdtl_gid=$location.search().allocationdtl_gid;
      
        activate();

        function activate() {

            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            var url = "api/visitReport/GetAllocationLogDetail";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.allocated_date = resp.data.allocated_date;
                $scope.scheduleList = resp.data.schedulelogdtl;
                $scope.calllogdtlList = resp.data.calllogdtl;
            });
        }

        $scope.callLogDetails = function (customer_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/callLogDetailsModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.callLogsubmit = function () {
                    var params = {
                        allocationdtl_gid: allocationdtl_gid,
                        customer_gid: customer_gid,
                        dialed_number: $scope.txtdialed_number,
                        call_response: $scope.txtcall_response,
                        call_remarks: $scope.txtcall_remarks
                    }

                    var url = "api/visitReport/PostCallLog"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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

        $scope.callLogDetailsedit = function (calllog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/callLogEditDetails.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    calllog_gid: calllog_gid
                }
                var url = 'api/visitReport/GetCallLogDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtdialed_number = resp.data.dialed_number;
                    $scope.txtcall_response = resp.data.call_response;
                    $scope.txtcall_remarks = resp.data.call_remarks;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.editcallLogsubmit = function () {

                    var params = {
                        calllog_gid: calllog_gid,
                        dialed_number: $scope.txtdialed_number,
                        call_response: $scope.txtcall_response,
                        call_remarks: $scope.txtcall_remarks
                    }

                    var url = 'api/visitReport/patchCallLogUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'Warning')
                        }
                        activate();
                    });
                }
            }

        }

        $scope.scheduleLogDetails = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/schedule.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                vm.mytime = new Date();
                vm.hstep = 1;
                vm.mstep = 15;
                vm.ismeridian = false;
                vm.calender1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    vm.open1 = true;
                };
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


                $scope.scheduleappointment = function () {
                    var scheduletime, selectedtime;
                    if ($scope.txtappointment_time == undefined) {
                        var today = new Date();
                        scheduletime = today.getHours() + ":" + today.getMinutes();
                    }
                    else {
                        selectedtime = $scope.txtappointment_time;
                        scheduletime = selectedtime.getHours() + ":" + selectedtime.getMinutes();
                    }
                    var appointment_date = new Date();
                    appointment_date.setFullYear($scope.txtappointmentdate.getFullYear());
                    appointment_date.setMonth($scope.txtappointmentdate.getMonth());
                    appointment_date.setDate($scope.txtappointmentdate.getDate());
                    var params = {
                        allocationdtl_gid:allocationdtl_gid,
                        customer_gid: customer_gid,
                        appointment_date: appointment_date,
                        appointment_time: scheduletime,
                        appointment_status: $scope.txtappointmentstatus,
                        appointment_remarks: $scope.txtremarks,
                    }

                    var url = "api/visitReport/PostScheduleLog"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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


        $scope.scheduleLogDetailsEdit = function (schedulelog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Editschedule.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                vm.mytime = new Date();
                vm.hstep = 1;
                vm.mstep = 15;
                vm.ismeridian = false;
                vm.calender1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    vm.open1 = true;
                };
                var params = {
                    schedulelog_gid: schedulelog_gid
                }

                var url = 'api/visitReport/GetScheduleLogDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtappointmentdate = new Date(resp.data.appointment_Date);
                    $scope.txtappointment_time = new Date(resp.data.appointment_Time);
                    $scope.txtappointmentstatus = resp.data.appointment_status;
                    $scope.txtremarks = resp.data.appointment_remarks;
                });
                
                var url = "api/visitReport/GetScheduleLogHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.scheduleList = resp.data.schedulelogdtl;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.scheduleappointmentedit = function () {

                    var params = {
                        schedulelog_gid: schedulelog_gid,
                        appointment_date: $scope.txtappointmentdate,
                        appointment_time: $scope.txtappointment_time,
                        appointment_status: $scope.txtappointmentstatus,
                        appointment_remarks: $scope.txtremarks,
                    }
                    
                    var url = 'api/visitReport/patchScheduleLogUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                       
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            var url = "api/visitReport/GetScheduleLogHistory";
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.scheduleList = resp.data.schedulelogdtl;
                            });
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $location.url('app/rmScheduleLogDetails?allocationdtl_gid='+allocationdtl_gid);
                           // $state.go('app.pageredirect');
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

        $scope.ScheduleStatus = function (schedulelog_gid, schedule_status) {

            var modalInstance = $modal.open({
                templateUrl: '/ScheduleStatusModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
 
                $scope.cboschedule_status = schedule_status;
                $scope.ScheduleStatusUpdate = function () {
                    lockUI();

                    var params = {
                        schedule_status: $scope.cboschedule_status,
                        schedulelog_gid: schedulelog_gid,
                    }

                    var url = "api/visitReport/PostScheduleStatus";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $location.url('app/rmScheduleLogDetails?allocationdtl_gid='+allocationdtl_gid);
                            activate();
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmTransfercontroller', rmTransfercontroller);

    rmTransfercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function rmTransfercontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmTransfercontroller';

        activate();

        function activate() {
            var url = "api/allocationManagement/getRMallocationList";
            SocketService.get(url).then(function (resp) {
                $scope.RMallocation = resp.data.mappingdtl;
            });

        }
        $scope.transferRMdtl = function (assignedRM_gid) {
            localStorage.setItem('assignedRM_gid', assignedRM_gid);
            $state.go('app.rmAllocationTransfer');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmVisitReportcontroller', rmVisitReportcontroller);

    rmVisitReportcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function rmVisitReportcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmVisitReportcontroller';

        activate();

        function activate() {
            lockUI();

            localStorage.setItem('RSK_RM', 'Y');
            localStorage.setItem('CC', 'N');
            localStorage.setItem('AgrCC', 'N');
            localStorage.setItem('AgrSuprCC', 'N');
            var url = "api/visitReport/GetRMTodayActivity";
            SocketService.get(url).then(function (resp) {
                $scope.todayactivity = resp.data.todayactivity;
                $scope.monthlyactivity = resp.data.monthlyactivity;
                $scope.count_current = resp.data.count_current;
                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.count_completed = resp.data.count_completed;
                $scope.count_exclusion = resp.data.count_external;
                $scope.overallRMdistrictdtl = resp.data.mydistrictdtl;
                //angular.forEach($scope.overallRMdistrictdtl, function (value, key) {
                //    var params = {
                //        state_gid: value.state_gid,
                //        district_gid: value.district_gid
                //    };
                //    var url = 'api/visitReport/GetRMCustomerDetails';
                //    SocketService.getparams(url, params).then(function (resp) {
                //        value.mycustomerdtl = resp.data.mycustomerdtl;
                //        value.expand = false;
                //    });
                //});
                unlockUI();
            });
        }

        $scope.myschedule = function () {
            lockUI();
            $scope.MySchedule = true;
            localStorage.setItem('RSK_RM', 'Y');
            var url = "api/visitReport/GetRMTodayActivity";
            SocketService.get(url).then(function (resp) {
                $scope.todayactivity = resp.data.todayactivity;
                $scope.monthlyactivity = resp.data.monthlyactivity;
                $scope.count_current = resp.data.count_current;
                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.count_completed = resp.data.count_completed;
                unlockUI();
            });
        }

        $scope.current = function () {
            $scope.Current = true;
            lockUI();
            $scope.totalCurrentDisplayed = 100;
            var url = "api/allocationManagement/GetRMcurrentallocateddtl";
            SocketService.get(url).then(function (resp) {
                $scope.count_current = resp.data.count_current;
                $scope.allocatedList = resp.data.rmallocation;
                if ($scope.allocatedList == null) {
                    $scope.totalCurrent = 0;
                    $scope.totalCurrentDisplayed = 0;
                }
                else {
                    $scope.totalCurrent = $scope.allocatedList.length;
                    if ($scope.allocatedList.length < 100) {
                        $scope.totalCurrentDisplayed = $scope.allocatedList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.upcoming = function () {
            $scope.Upcoming = true;
            lockUI();
            $scope.totalUpcomingDisplayed = 100;
            var url = "api/allocationManagement/GetRMupcomingallocateddtl";
            SocketService.get(url).then(function (resp) {
                $scope.count_upcoming = resp.data.count_upcoming;
                $scope.upcomingallocationList = resp.data.rmallocation;
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

        $scope.exclusion = function () {
            $scope.Upcoming = true;
            lockUI();
            $scope.totalExclusionDisplayed = 100;
            var url = "api/allocationManagement/GetRMExclusiondetails";
            SocketService.get(url).then(function (resp) {
                $scope.count_exclusion = resp.data.count_exclusion;
                $scope.exclusionAllocationlist = resp.data.exclusionAllocation;
                if ($scope.exclusionAllocationlist == null) {
                    $scope.totalExclusion = 0;
                    $scope.totalExclusionDisplayed = 0;
                }
                else {
                    $scope.totalExclusion = $scope.exclusionAllocationlist.length;
                    if ($scope.exclusionAllocationlist.length < 100) {
                        $scope.totalExclusionDisplayed = $scope.exclusionAllocationlist.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.completed = function () {
            $scope.Completed = true;
            lockUI();
            var url = "api/allocationManagement/getRMCompleteddetails";
            SocketService.get(url).then(function (resp) {
                $scope.count_completed = resp.data.count_completed;
                $scope.completedallocationList = resp.data.rmallocation;
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

        $scope.loadCurrentMore = function (pageCurrentcount) {
            if (pageCurrentcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageCurrentcount);
            if ($scope.totalCurrentDisplayed < $scope.allocatedList.length) {
                $scope.totalCurrentDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.allocatedList.length + " Records Only", "warning");
                return;
            }
        };

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

        $scope.loadExclusionMore = function (pageExclusioncount) {
            if (pageExclusioncount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageExclusioncount);
            if ($scope.totalUpcomingDisplayed < $scope.exclusionAllocationlist.length) {
                $scope.totalUpcomingDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.exclusionAllocationlist.length + " Records Only", "warning");
                return;
            }
        };

        $scope.loadCompletedMore = function (pageCompletedcount) {
            if (pageCompletedcount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageCompletedcount);
            if ($scope.totalUpcomingDisplayed < $scope.completedallocationList.length) {
                $scope.totalUpcomingDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.completedallocationList.length + " Records Only", "warning");
                return;
            }
        };

        $scope.schedulecurrentlog = function (allocationdtl_gid, customer_gid, customername) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // $state.go('app.rmScheduleLogDetails');
            $location.url('app/rmScheduleLogDetails?allocationdtl_gid='+allocationdtl_gid);
        }
    
        $scope.scheduleupcominglog = function (allocationdtl_gid) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // $state.go('app.rmScheduleLogDetails');
            $location.url('app/rmScheduleLogDetails?allocationdtl_gid=' + allocationdtl_gid);
        }

        $scope.schedulecompletelog = function (allocationdtl_gid) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // $state.go('app.rmScheduleLogView');
            $location.url('app/rmScheduleLogView?allocationdtl_gid='+allocationdtl_gid);
        }

        $scope.observationreport = function (allocationdtl_gid, observation_reportgid) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // $state.go('app.rmObservationReport');
            //$location.url('app/rmObservationReport?allocationdtl_gid='+allocationdtl_gid);
            $location.url('app/rmObservationReport?allocationdtl_gid=' + allocationdtl_gid + '&?&observation_reportgid=' + observation_reportgid);
        }

        $scope.exclusioncustomer = function (allocationdtl_gid, customer_urn, customername, allocation_status) {

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
                $scope.customer_status = allocation_status;

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
                        allocationdtl_gid: allocationdtl_gid,
                        exclusion_reason: $scope.txtexclusion_reason
                    }
                    var url = "api/ExclusionList/GetExclusionAllocation";
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

        $scope.observationviewreport = function (allocationdtl_gid, observation_reportgid) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // localStorage.setItem('observation_reportgid', observation_reportgid);
            // $state.go('app.rmObservationReportView');

            $location.url('app/rmObservationReportView?allocationdtl_gid='+allocationdtl_gid+'&?&observation_reportgid='+observation_reportgid);
        }

        $scope.reportCanceldtl = function (allocationdtl_gid, customer_gid, customername) {

            var modalInstance = $modal.open({
                templateUrl: '/reportCancelModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customername = customername;
                $scope.proceedReportCancel = function () {
                    lockUI();

                    var params = {
                        cancel_reason: $scope.txtcancel_reason,
                        allocationdtl_gid: allocationdtl_gid,
                        customer_gid: customer_gid
                    }
                    var url = "api/VisitReportCancel/PostCancelReport";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
                            // localStorage.setItem('allocation_customer_gid', customer_gid);

                           // $state.go('app.visitReportCancel');
                            $location.url('app/visitReportCancel?allocationdtl_gid='+allocationdtl_gid+'&allocation_customer_gid='+customer_gid);
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

        $scope.visitStatus = function (allocationdtl_gid, customername, customer_urn, visit_status) {

            var modalInstance = $modal.open({
                templateUrl: '/visitStatusModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.customername = customername;
                $scope.customer_urn = customer_urn;
                $scope.txtVisitStatus = visit_status;
                $scope.VisitStatusUpdate = function () {
                    lockUI();

                    var params = {
                        visit_status: $scope.txtVisitStatus,
                        allocationdtl_gid: allocationdtl_gid,
                    }

                    var url = "api/VisitReportCancel/PostVisitStatus";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
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

        $scope.viewallocationDtl = function (allocationdtl_gid, customer_gid, allocation_status) {

            if (allocation_status == 'Allocated') {
               var lscompleted_flag= 'N';
            }
            else {
                var lscompleted_flag= 'Y';
               
            }
            var url = "api/allocationManagement/tmpAllocatedocumentclear";
            SocketService.get(url).then(function (resp) {
            });
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // localStorage.setItem('allocation_customer_gid', customer_gid);
            // localStorage.setItem('MyAllocation', 'Y');
            var url='app/allocationView?allocationdtl_gid='+allocationdtl_gid+'&?&allocation_customer_gid='+customer_gid+'&?&MyAllocation=Y&?&completed_flag='+lscompleted_flag;
            $location.url(url);
            console.log('url',url);
            //$state.go('app.allocationView');
        }

        $scope.genereteallocation = function (allocationdtl_gid, customer_gid) {
            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            var url = "api/visitReport/GetScheduleInfo";
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.info_flag == "Y") {
                    // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
                    // localStorage.setItem('allocation_customer_gid', customer_gid);
                    // $state.go('app.visitReportGenerate');
                    $location.url('app/visitReportGenerate?allocationdtl_gid='+allocationdtl_gid+'&allocation_customer_gid='+customer_gid);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                unlockUI();
            });

          
        }

        $scope.Viewgenereteddtl = function (allocationdtl_gid) {
            // localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            // localStorage.setItem('allocation_customer_gid', customer_gid);
            // $state.go('app.visitReportdetailView');
            $location.url('app/visitReportdetailView?allocationdtl_gid='+allocationdtl_gid);
        }

        $scope.generetePDF = function (allocationdtl_gid) {

            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            };
            var url = 'api/zonalAllocation/visitReportpdfcontent';
            SocketService.getparams(url, params).then(function (resp) {       
                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, filename);
            }
            else {
                unlockUI();
                Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                unlockUI();
            });
         
            
        }
        $scope.genereteATRPDF = function (observation_reportgid) {


            var params = {
                observation_reportgid: observation_reportgid

            };
            var url = 'api/ObservationReport/ATRReportpdfcontent';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var filename = resp.data.file_name;
                    var filepath = resp.data.file_path;
                    DownloaddocumentService.Downloaddocument(filepath, filename);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
          
            });

        }
      
    }
})();

               
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rskDashboardcontroller', rskDashboardcontroller);

    rskDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'Colors', 'ChartData', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$timeout', 'SweetAlert', '$cookies'];

    function rskDashboardcontroller($rootScope, $scope, $state, AuthenticationService, Colors, ChartData, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $timeout, SweetAlert, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rskDashboardcontroller';

        activate();

        function activate() {
            vm.barOptions = {
                scaleBeginAtZero: true,
                scaleShowGridLines: true,
                scaleGridLineColor: 'rgba(0,0,0,.05)',
                scaleGridLineWidth: 1,
                barShowStroke: true,
                barStrokeWidth: 2,
                barValueSpacing: 5,
                barDatasetSpacing: 1,
                animationSteps: 100,
                animationEasing: 'easeInOutSine',
            };
            
            var url = "api/RskDashboard/GetAllocationDtl"
            SocketService.get(url).then(function (resp) {
                var last6monthallocation = resp.data.allocationvisitgraphdtl;
                console.log(resp);
                if (last6monthallocation == null) {
                    vm.barData = {
                        labels: [],
                        datasets: [
                          {
                              fillColor: Colors.byName('success'),
                              strokeColor: Colors.byName('success'),
                              highlightFill: Colors.byName('info'),
                              highlightStroke: Colors.byName('info'),
                              data: []
                          },
                          {
                              fillColor: Colors.byName('warning'),
                              strokeColor: Colors.byName('warning'),
                              highlightFill: Colors.byName('primary'),
                              highlightStroke: Colors.byName('primary'),
                              data: []
                          }
                        ]
                    };
                }
                else {

                    vm.barData = {
                        labels: [last6monthallocation[4].monthname, last6monthallocation[3].monthname, last6monthallocation[2].monthname, last6monthallocation[1].monthname, last6monthallocation[0].monthname],
                        datasets: [
                          {
                              fillColor: Colors.byName('success'),
                              strokeColor: Colors.byName('success'),
                              highlightFill: Colors.byName('info'),
                              highlightStroke: Colors.byName('info'),
                              data: [last6monthallocation[4].countAllocated, last6monthallocation[3].countAllocated, last6monthallocation[2].countAllocated, last6monthallocation[1].countAllocated, last6monthallocation[0].countAllocated]
                          },
                          {
                              fillColor: Colors.byName('warning'),
                              strokeColor: Colors.byName('warning'),
                              highlightFill: Colors.byName('primary'),
                              highlightStroke: Colors.byName('primary'),
                              data: [last6monthallocation[4].countCompleted, last6monthallocation[3].countCompleted, last6monthallocation[2].countCompleted, last6monthallocation[1].countCompleted, last6monthallocation[0].countCompleted]
                          }
                        ]
                    };
                }

            });


            var url = 'api/RskDashboard/GetRskPrivilege';
            var user_gid = localStorage.getItem('user_gid');
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var sanction = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKSANMAN");
                var customerManagement = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKSANCUS");
                var zonalCustomer = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKSANMYC");
               
                var caseAllocation = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCMAN");
                var zonalAllocation = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCZAC");
                var visitManagement = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCRMA");
                var transferApproval = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCTAP");
                var allocationTransfer = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCACT");
                var observationReportApproval = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCORA");
                var tierreport = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCTRT");
                var tier2approval = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCTIA");
                var tier3preparation = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCTI3");
                var allocationreport = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCART");

                if (sanction != -1) {
                    $scope.sanction_show = 'Y';
                }
                else {
                    $scope.sanction_show = 'N';
                }
                if (customerManagement != -1) {
                    $scope.customerManagement_show = 'Y';
                }
                else {
                    $scope.customerManagement_show = 'N';
                }
                if (zonalCustomer != -1) {
                    $scope.zonalCustomer_show = 'Y';
                }
                else
                {
                    $scope.zonalCustomer_show = 'N';
                }
                if (caseAllocation != -1) {
                    $scope.caseAllocation_show = 'Y';
                }
                else {
                    $scope.caseAllocation_show = 'N';
                }
                if (zonalAllocation != -1) {
                    $scope.zonalAllocation_show = 'Y';
                }
                else {
                    $scope.zonalAllocation_show = 'N';
                }
                if (visitManagement != -1) {
                    $scope.visitManagement_show = 'Y';
                }
                else {
                    $scope.visitManagement_show = 'N';
                }
                if (transferApproval != -1) {
                    $scope.transferApproval_show = 'Y';
                }
                else {
                    $scope.transferApproval_show = 'N';
                }
                if (allocationTransfer != -1) {
                    $scope.allocationTransfer_show = 'Y';
                }
                else {
                    $scope.allocationTransfer_show = 'N';
                }
                if (observationReportApproval != -1) {
                    $scope.observationApproval_show = 'Y';
                }
                else {
                    $scope.observationApproval_show = 'N';
                }
                if (tierreport != -1) {
                    $scope.tierreport_show = 'Y';
                }
                else {
                    $scope.tierreport_show = 'N';
                }
                if (tier2approval != -1) {
                    $scope.tier2approval_show = 'Y';
                }
                else {
                    $scope.tier2approval_show = 'N';
                }
                if (tier3preparation != -1) {
                    $scope.tier3preparation_show = 'Y';
                }
                else {
                    $scope.tier3preparation_show = 'N';
                }
                if (allocationreport != -1) {
                    $scope.allocationreport_show = 'Y';
                }
                else {
                    $scope.allocationreport_show = 'N';
                }
            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sanctionAddcontroller', sanctionAddcontroller);

    sanctionAddcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function sanctionAddcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sanctionAddcontroller';

        activate();

        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open4 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });

            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {

                $scope.employee_list = resp.data.employee_list;
            });

            var url = "api/documentation/getdocumentationdtlList";
            SocketService.get(url).then(function (resp) {
                $scope.documentationdtl = resp.data.documentationdtl;
            });
        }


        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {
               
                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtSanctionAmount = output;
            //console.log(output);
        }

        $scope.sanctionlimitchange = function () {
            var input = document.getElementById('txt_SanctionLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtSanctionLimit = output;

        }

        $scope.revisedlimitchange = function () {
            var input = document.getElementById('txt_RevisedLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtRevisiedLimit = output;

        }

        $scope.existinglimitchange = function () {
            var input = document.getElementById('txt_ExistingLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtexistingLimit = output;

        }

        $scope.sanctionformSubmit = function () {
            var input = $scope.txtSanctionAmount;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {
                var str = input.replace(',', '');
                input = str;
            }
           
            lockUI();
            var params = {
                customer_gid: $scope.cbocustomergid.customer_gid,
                sanction_refno: $scope.txtsanctionRefNo,
                sanction_date: $scope.txtSanctionDate,
                sanction_amount: input,
                sanction_limit: $scope.txtSanctionLimit,
                sanction_validity: $scope.txtValidity,
                expiry_date: $scope.txtExpiryDate,
                review_date: $scope.txtReviewDate,
                approval_authority: $scope.cboapproval_authority,
                natureof_proposal: $scope.cbonature_proposal,
                constitution: $scope.cboconstitution,
                authorized_signatory: $scope.cboauthorizedsignatory.employee_gid,
                tenure_months: $scope.txt_tenuremonths,
                revisied_limit: $scope.txtRevisiedLimit,
                existing_limit: $scope.txtexistingLimit,
                escrow_account: $scope.cboEscrowAccount,
                specific_conditions: $scope.txtspecificcondition,
                facility_type: $scope.cbofacilitytype,
                documentationname: $scope.cbodocumentationname
            }

            var url = "api/sanction/postsanctiondetails";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.sanctionManagement');
                    activate();
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

        $scope.cancel = function () {
            $state.go('app.sanctionManagement');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sanctionEditcontroller', sanctionEditcontroller);

    sanctionEditcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function sanctionEditcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sanctionEditcontroller';

        activate();

        function activate() {
            $scope.documentationEdit = [];

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open4 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            lockUI();
            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });

            var url = "api/documentation/getdocumentationdtlList";
            SocketService.get(url).then(function (resp) {
                $scope.documentationdtl = resp.data.documentationdtl;
            });

            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var params = {
                customer2sanction_gid: localStorage.getItem('customer2sanction_gid')
            }
        
            var url = 'api/sanction/getsanctiondetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_gid = resp.data.customer_gid;
                $scope.txteditsanctionRefNo = resp.data.sanction_refno;
                $scope.txteditSanctionDate = resp.data.sanctionDate;
                $scope.txteditSanctionAmount = resp.data.sanction_amount;
                $scope.txteditSanctionLimit = resp.data.sanction_limit;
                $scope.txtEditValidity = resp.data.sanction_validity;
                $scope.txtEditExpiryDate = resp.data.expiry_date;
                $scope.txtEditReviewDate = resp.data.review_date;
                $scope.cboeditapproval_authority = resp.data.approval_authority;
                $scope.cboEditnature_proposal = resp.data.natureof_proposal;
                $scope.cboEditconstitution = resp.data.constitution;
                $scope.cboEditauthorizedsignatory = resp.data.authorized_signatory;
                $scope.txtEditRevisiedLimit = resp.data.revisied_limit;
                $scope.txtEditexistingLimit = resp.data.existing_limit;
                $scope.cboEditEscrowAccount = resp.data.escrow_account;
                $scope.txtEditspecificcondition = resp.data.specific_conditions;
                $scope.cboEditfacilitytype = resp.data.facility_type;
                $scope.documentationEdit = resp.data.documentationname;
                $scope.txtEdittenure_months = resp.data.tenure_months;
                console.log(resp);
                
                $scope.documentationEditData = [];
                if (resp.data.documentationname!=null)
                {
                    var count = resp.data.documentationname.length;
                    for (var i = 0; i < count; i++) {

                        var indexs = $scope.documentationdtl.map(function (x) { return x.customer2document_gid; }).indexOf(resp.data.documentationname[i].customer2document_gid); 
                        $scope.documentationEditData.push($scope.documentationdtl[indexs]);
                    }
                }
               
            });


            unlockUI();
        }

        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txteditSanctionAmount = output;
           
        }

        $scope.sanctionlimitchange = function () {
            var input = document.getElementById('txtSanctionLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txteditSanctionLimit = output;

        }

        $scope.revisedlimitchange = function () {
            var input = document.getElementById('txtRevisedLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtEditRevisiedLimit = output;

        }

        $scope.existinglimitchange = function () {
            var input = document.getElementById('txtExistingLimit').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            $scope.txtEditexistingLimit = output;

        }

        $scope.sanctionformUpdate = function () {
            var input = $scope.txteditSanctionAmount;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {
                var str = input.replace(',', '');
                input = str;
            }
            lockUI();
            var params = {
                customer2sanction_gid: localStorage.getItem('customer2sanction_gid'),
                customer_gid: $scope.customer_gid,
                sanction_refno: $scope.txteditsanctionRefNo,
                sanction_date: $scope.txteditSanctionDate,
                sanction_amount: input,
                sanction_limit: $scope.txteditSanctionLimit,
                sanction_validity: $scope.txtEditValidity,
                expiry_date: $scope.txtEditExpiryDate,
                review_date: $scope.txtEditReviewDate,
                approval_authority: $scope.cboeditapproval_authority,
                natureof_proposal: $scope.cboEditnature_proposal,
                constitution: $scope.cboEditconstitution,
                authorized_signatory: $scope.cboEditauthorizedsignatory,
                revisied_limit: $scope.txtEditRevisiedLimit,
                tenure_months: $scope.txtEdittenure_months,
                existing_limit: $scope.txtEditexistingLimit,
                escrow_account: $scope.cboEditEscrowAccount,
                specific_conditions: $scope.txtEditspecificcondition,
                facility_type: $scope.cboEditfacilitytype,
                documentationname: $scope.documentationEditData
            }

            var url = "api/sanction/postsanctionupdate";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.sanctionManagement');
                    activate();
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

        $scope.cancel = function () {
            $state.go('app.sanctionManagement');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sanctionManagementcontroller', sanctionManagementcontroller);

    sanctionManagementcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function sanctionManagementcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sanctionManagementcontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/sanction/getsanctiondtlList";
            SocketService.get(url).then(function (resp) {
                $scope.sanctionlist = resp.data.sanctiondetails;
                if ($scope.sanctionlist == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.sanctionlist.length;
                    if ($scope.sanctionlist.length < 100) {
                        $scope.totalDisplayed = $scope.sanctionlist.length;
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
            if ($scope.sanctionlist != null) {
                if ($scope.totalDisplayed < $scope.sanctionlist.length) {
                    $scope.totalDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.sanctionlist.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.selectedFile = null;
        $scope.msg = "";

        $scope.importExcel = function () {
            $scope.test = true;
            $scope.btnimport = true;
        }

        $scope.uploadcancel = function () {
            lockUI();
            $scope.test = false;
            $scope.btnimport = false;
            unlockUI();
        }

        $scope.documetnchecklistsanction = function (customer2sanction_gid)
        {
            localStorage.setItem('customer2sanction_gid', customer2sanction_gid)
            $state.go('app.documentCheckList')
        }
        $scope.handleFile = function (val, val1, name) {

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
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;

            var url = "api/sanction/postexcelupload";
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;
                console.log(resp);
                $("#addupload").val('');

                if (resp.data.status == true) {

                    Notify.alert('Document Uploaded Successfully..!!', {
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

        $scope.loadFile = function (files) {

            $scope.$apply(function () {

                $scope.selectedFile = files[0];

            })

        }
        $scope.save = function (params) {
           
            lockUI();

            var url = "api/sanction/postexcelupload";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $("#excelImport").val('');
                    activate();
                    unlockUI();
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

        $scope.addSanction = function () {
            $state.go('app.sanctionAdd');
        }

        $scope.editsanction = function (val) {
            localStorage.setItem('customer2sanction_gid', val);
            $state.go('app.sanctionEdit');
        }

        $scope.viewsanction = function (val) {
            localStorage.setItem('customer2sanction_gid', val);
            $state.go('app.sanctionView');
        }

        $scope.deletesanction = function (val) {
            var params = {
                customer2sanction_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = "api/sanction/getsanctiondelete";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
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

            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sanctionViewcontroller', sanctionViewcontroller);

    sanctionViewcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function sanctionViewcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sanctionViewcontroller';

        activate();

        function activate() {
            lockUI();
            var params = {
                customer2sanction_gid: localStorage.getItem('customer2sanction_gid')
            }

            var url = 'api/sanction/getsanctiondetails';
            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.customer_name = resp.data.customer_name;
                $scope.txteditsanctionRefNo = resp.data.sanction_refno;
                $scope.txteditSanctionDate = resp.data.sanction_date;
                $scope.txteditSanctionAmount = resp.data.sanction_amount;
                $scope.txteditSanctionLimit = resp.data.sanction_limit;
                $scope.txtEditValidity = resp.data.sanction_validity;
                $scope.txtEditExpiryDate = resp.data.expiry_date;
                $scope.txtEditReviewDate = resp.data.review_date;
                $scope.cboeditapproval_authority = resp.data.approval_authority;
                $scope.cboEditnature_proposal = resp.data.natureof_proposal;
                $scope.cboEditconstitution = resp.data.constitution;
                $scope.cboEditauthorizedsignatory = resp.data.authorizedsignatoryname;
                $scope.txtEditRevisiedLimit = resp.data.revisied_limit;
                $scope.txtEditexistingLimit = resp.data.existing_limit;
                $scope.cboEditEscrowAccount = resp.data.escrow_account;
                $scope.txtEditspecificcondition = resp.data.specific_conditions;
                $scope.cboEditfacilitytype = resp.data.facility_type;
                $scope.documentationname = resp.data.documentationname;
                $scope.customer_urn = resp.data.customer_urn;
            });
            unlockUI();
        }

        $scope.back = function () {
            $state.go('app.sanctionManagement');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier1Approvalcontroller', tier1Approvalcontroller);

    tier1Approvalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function tier1Approvalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier1Approvalcontroller';

        activate();

        function activate() {
            var params = {
                tier1format_gid: localStorage.getItem('tier1format_gid')
            }
            lockUI();
            var url = "api/TierMeeting/GetTier1ApprovalDtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.tier1_observations = resp.data.tier1_observations;
                $scope.tier1_code = resp.data.tier1_code;
                $scope.tier1_justification = resp.data.tier1_justification;
                $scope.tier1_processgap = resp.data.tier1_processgap;
                $scope.tier1_processrecommendation = resp.data.tier1_processrecommendation;
                $scope.tier1_managementcomments = resp.data.tier1_managementcomments;
                $scope.tier1_reverts_actionplan = resp.data.tier1_reverts_actionplan;
                $scope.tier1_atrdate = resp.data.tier1_atrdate;
                $scope.tier1approvallog = resp.data.tier1approvallog;
                $scope.txtcode_changereason = resp.data.tier1code_changereason;
                $scope.tier1code_changeflag = resp.data.tier1code_changeflag;
                $scope.tier3_status = resp.data.tier3_status;
                $scope.uploaddocument_list = resp.data.tier1doc;
                $scope.tier1rejectlog = resp.data.tier1rejectlog;
                $scope.tier1_approvalstatus = resp.data.tier1_approvalstatus;
                if ($scope.tier1code_changeflag == 'Y') {
                    $scope.disablecodechangereasonshow = true;
                }
                else {
                    $scope.disablecodechangereasonshow = false;
                }
                if ($scope.tier1_approvalstatus == "Approved") {
                    $scope.editdisable = true;
                }
                else {
                    $scope.editenable = true;
                }
                if (resp.data.tier1approvallog == null) {
                    $scope.nohistoryapproval = true;
                }
                else {
                    $scope.historyapproval = true;
                }
                if (resp.data.tier1rejectlog == null) {
                    $scope.rejecthistory = false;
                }
                else {
                    $scope.rejecthistory = true;
                }
                unlockUI();
            });
        }

        $scope.viewcustomerdtl = function () {
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }

        $scope.tier1approve = function () {
            var params = {
                tier1format_gid: localStorage.getItem('tier1format_gid'),
                approval_remarks: $scope.txttier1_approvalremarks
            }
            lockUI();
            var url = "api/TierMeeting/PostTier1Approved";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.tier1Summary');
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

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.tier1reject = function () {
            var params = {
                tier1format_gid: localStorage.getItem('tier1format_gid'),
                approval_remarks: $scope.txttier1_approvalremarks
            }
            lockUI();
            var url = "api/TierMeeting/PostTier1Rejected";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.tier1Summary');
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
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier1Summarycontroller', tier1Summarycontroller);

    tier1Summarycontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function tier1Summarycontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier1Summarycontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = "api/TierMeeting/GetTier1formatlist";
            SocketService.get(url).then(function (resp) {
                $scope.count_pending = resp.data.count_pending;
                $scope.count_approved = resp.data.count_approved;
                $scope.count_rejected = resp.data.count_rejected;
                $scope.tier1format = resp.data.tier1format;
                if ($scope.tier1format == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.tier1format.length;
                    if ($scope.tier1format.length < 100) {
                        $scope.totalDisplayed = $scope.tier1format.length;
                    }
                }
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
        $scope.viewtier1format = function (tier1format_gid, allocationdtl_gid) {
            localStorage.setItem('tier1format_gid', tier1format_gid);
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            $state.go('app.tier1Approval');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier2Approval', tier2Approval);

    tier2Approval.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function tier2Approval($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier2Approval';

        activate();

        function activate() {
            var params = {
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid')
            }
            var url = 'api/TierMeeting/GetTier2ViewDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.zonal_name = resp.data.zonal_name;
                $scope.cbomonth = resp.data.tier2_monthname;
                $scope.vertical = resp.data.vertical;
                $scope.cbovertical_gid = resp.data.vertical_gid;
                $scope.cboemployeegid = resp.data.headRMD_gid;
                $scope.headRMD_name = resp.data.headRMD_name;
                $scope.txttier2_remarks = resp.data.tier2_remarks;
                $scope.tier2_approval_status = resp.data.tier2_approval_status;
                console.log($scope.tier2_approval_status);
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.uploaddocument_list = resp.data.tier2document;
                $scope.tier2approvallog = resp.data.tier2approvallog;
                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                $scope.tier3_status = resp.data.tier3_status;
                if ($scope.tier2_approval_status == 'Approved') {
                    $scope.editdisable = true;
                }
                else {
                    $scope.editenable = true;
                }
                if (resp.data.tier2approvallog == null) {
                    $scope.nohistoryapproval = true;
                }
                else {
                    $scope.historyapproval = true;
                }
                unlockUI();
            });
        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('tier1format_gid', tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }

        $scope.tier2Approve = function () {
            var params = {
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid'),
                approval_remarks: $scope.txttier2_approvalremarks
            }
            lockUI();
            var url = "api/TierMeeting/PostTier2Approved";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.tier2ApprovalSummary');
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


        $scope.tier2Reject = function () {
            var params = {
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid'),
                approval_remarks: $scope.txttier2_approvalremarks
            }
            lockUI();
            var url = "api/TierMeeting/PostTier2Rejected";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.tier2ApprovalSummary');
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


        $scope.riskcodechange = function (allocationdtl_gid, customer_name, customer_urn, tier2_code) {

            var modalInstance = $modal.open({
                templateUrl: '/riskcodechangeModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier2_code;
                $scope.ok = function () {
                    $modalInstance.close('closed');

                };

                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTierColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;

                });
            }
        }

        $scope.downloads = function (val1, val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier2ApprovalSummary', tier2ApprovalSummary);

    tier2ApprovalSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams'];

    function tier2ApprovalSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier2ApprovalSummary';

        activate();

        function activate() {
            lockUI();
            var url = 'api/TierMeeting/GetTier2ApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.tier2preparation_list = resp.data.tier2preparation;
                $scope.count_pending = resp.data.count_pending;
                $scope.count_approved = resp.data.count_approved;
                $scope.count_rejected = resp.data.count_rejected;
                if ($scope.tier2preparation_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.tier2preparation_list.length;
                    if ($scope.tier2preparation_list.length < 100) {
                        $scope.totalDisplayed = $scope.tier2preparation_list.length;
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
            if ($scope.totalDisplayed < $scope.tier2preparation_list.length) {
                $scope.totalDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.tier2preparation_list.length + " Records Only", "warning");
                return;
            }
        };


        $scope.viewtier2dtl = function (tier2preparation_gid) {
            localStorage.setItem('tier2preparation_gid', tier2preparation_gid)
            $state.go('app.tier2Approval');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier2Createcontroller', tier2Createcontroller);

    tier2Createcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function tier2Createcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier2Createcontroller';

        activate();

        function activate() {
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/TierMeeting/GetTier2Monthdtl';
            SocketService.get(url).then(function (resp) {
                $scope.monthname_list = resp.data.monthname;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.zonalmapping_gid = resp.data.zonalmapping_gid;
                unlockUI();
            });

        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid',allocationdtl_gid);
            localStorage.setItem('tier1format_gid',tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }

        $scope.monthchange = function (cbomonth) {
            var params = {
                month: cbomonth,
                tier2: 'Y',
                zonalmapping_gid: $scope.zonalmapping_gid
            }
            var url = 'api/TierMeeting/GetVertical';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_list = resp.data.tiervertical;

            });
        }

        $scope.verticalchange = function (cbovertical_gid) {
            var params = {
                vertical_gid: cbovertical_gid,
                month: $scope.cbomonth,
                tier2_flag: 'Y'
            }
            lockUI();
            var url = 'api/TierMeeting/GetVerticalAllocationdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                $scope.tier2dtl = true;
                unlockUI();
            });
        }

        $scope.riskcodechange = function (allocationdtl_gid, tier1format_gid, customer_name, customer_urn, tier1_code) {

            var modalInstance = $modal.open({
                templateUrl: '/riskcodechangeModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.txtcode_changereason = "";
                $scope.cboriskcode = tier1_code;
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTier2ColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;
                    $scope.tmptier_stage = resp.data.tier_stage;
                    $scope.tmptier_code = resp.data.tier_code;
                    $scope.tmptiercode_changereason = resp.data.tiercode_changereason;
                    $scope.tmptier2_codechange = resp.data.tmptier2_codechange
                    if (resp.data.tmptier2_codechange == null || resp.data.tmptier2_codechange == undefined) {
                        $scope.tmpcodevisible = true;
                        $scope.tmpdata = false;
                    }
                    else {
                        $scope.tmpcodevisible = false;
                        $scope.tmpdata = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cboriskcodechange = function (cboriskcode) {
                    
                    if (tier1_code == cboriskcode) {
                        $scope.codechangereasonshow = false;

                    }
                    else {
                        $scope.codechangereasonshow = true;
                        $scope.txtcode_changereason = "";
                    }
                }

                $scope.riskcodechangecancel = function (tmptier2_codechange) {
                    var params = {
                        tmptier2_codechange: tmptier2_codechange
                    }
                    lockUI();
                    var url = "api/TierMeeting/GetTier2ColorDelete"
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $scope.tmpdata = false;
                            $scope.tmpcodevisible = true;
                            var params = {
                                allocationdtl_gid: allocationdtl_gid
                            }
                            var url = "api/TierMeeting/GetTier2ColorDetails";
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.tiercodedtl = resp.data.tiercodedtl;
                                $scope.cboriskcode = tier1_code;

                            });
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

                $scope.riskcodechangesubmit = function () {
                    var params = {
                        tier1format_gid: tier1format_gid,
                        allocationdtl_gid: allocationdtl_gid,
                        tier2_code: $scope.cboriskcode,
                        tier2code_changereason: $scope.txtcode_changereason
                    }
                    lockUI();
                    var url = "api/TierMeeting/PostTier2codeChange"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtcode_changereason = '';
                            $scope.codechangereasonshow = false;
                            $scope.tmpcodevisible = false;
                            $scope.tmpdata = true;
                            var params = {
                                allocationdtl_gid: allocationdtl_gid
                            }
                            var url = "api/TierMeeting/GetTier2ColorDetails";
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.tiercodedtl = resp.data.tiercodedtl;
                                if (resp.data.tier_code != "") {
                                  
                                    $scope.tmptier_stage = resp.data.tier_stage;
                                    $scope.tmptier_code = resp.data.tier_code;
                                    $scope.tmptiercode_changereason = resp.data.tiercode_changereason;
                                    $scope.tmptier2_codechange = resp.data.tmptier2_codechange
                                }
                                else {
                                    $scope.tmpdata = false;
                                }

                            });
                            unlockUI();

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

        $scope.uploaddocument = function (val, val1, name) {
            if ($scope.txtdocument_title == undefined || $scope.txtdocument_title == "") {
                alert('Enter the Document Title to Upload Document');
                $("#addExternalupload").val('');
                return false;
            }
            else {
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
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/TierMeeting/PostTier2Upload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.uploaddocument_list = resp.data.tier2document;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = "";
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.uploadflag = 'Y';
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.uploadflag = 'N';
                    }
                });
            }
        }

        $scope.uploadcancel = function (tmp_documentGid) {
            var tier2upload = {
                tmp_documentGid: tmp_documentGid
            }
            var url = 'api/TierMeeting/GetTier2UploadCancel';
            SocketService.getparams(url, tier2upload).then(function (resp) {
                $scope.uploaddocument_list = resp.data.upload_list;
            });
        }

        $scope.submittier2 = function () {
            lockUI();
            if ($scope.uploadflag != 'Y') {
                Notify.alert('Atleast Upload One Document to Submit..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
            }
            else {
                var vertical_name = $('#cboverticalname :selected').text();
                var headRMD_name = $('#cboemployeename :selected').text();
                var tier2dtl = {
                    zonalmapping_gid: $scope.zonalmapping_gid,
                    zonal_name: $scope.zonal_name,
                    tier2_month: $scope.cbomonth,
                    vertical_gid: $scope.cbovertical_gid,
                    vertical: vertical_name,
                    headRMD_gid: $scope.cboemployeegid,
                    headRMD_name: headRMD_name,
                    tier2_remarks: $scope.txttier2_remarks,
                }

                var url = "api/TierMeeting/PostTier2Preparation";
                SocketService.post(url, tier2dtl).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.txttier2_remarks = "";
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();

                        $state.go('app.tier2Preparation');

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
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier2Preparation', tier2Preparation);

    tier2Preparation.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function tier2Preparation($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier2Preparation';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/TierMeeting/GetTier2Summary';
            SocketService.get(url).then(function (resp) {
                $scope.tier2preparation_list = resp.data.tier2preparation;
                $scope.count_pending = resp.data.count_pending;
                $scope.count_approved = resp.data.count_approved;
                $scope.count_rejected = resp.data.count_rejected;
                if ($scope.tier2preparation_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.tier2preparation_list.length;
                    if ($scope.tier2preparation_list.length < 100) {
                        $scope.totalDisplayed = $scope.tier2preparation_list.length;
                    }
                }
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

        $scope.createtier2preparation = function () {
            $state.go('app.tier2Create');
        }


       
        $scope.viewtier2details = function (tier2preparation_gid) {
            localStorage.setItem('tier2preparation_gid', tier2preparation_gid);
            $state.go('app.tier2PreparationView');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier2PreparationView', tier2PreparationView);

    tier2PreparationView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function tier2PreparationView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier2PreparationView';

        activate();

        function activate() {
            lockUI();
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/TierMeeting/GetTier2Monthdtl';
            SocketService.get(url).then(function (resp) {
                $scope.monthname_list = resp.data.monthname;
            });

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var params = {
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid')
            }
            var url = 'api/TierMeeting/GetTier2ViewDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.zonal_name = resp.data.zonal_name;
                $scope.cbomonth = resp.data.tier2_month;
                $scope.monthname = resp.data.tier2_monthname;
                $scope.vertical = resp.data.vertical;
                $scope.cbovertical_gid = resp.data.vertical_gid;
                $scope.cboemployeegid = resp.data.headRMD_gid;
                $scope.headRMD_name = resp.data.headRMD_name;
                $scope.txttier2_remarks = resp.data.tier2_remarks;
                $scope.tier2_approval_status = resp.data.tier2_approval_status;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.uploaddocument_list = resp.data.tier2document;
                $scope.tier2approvallog = resp.data.tier2approvallog;
                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                $scope.zonalmapping_gid = resp.data.zonalmapping_gid;
                if (resp.data.tier2approvallog == null) {
                    //$scope.nohistoryapproval = true;
                }
                else {
                    $scope.historyapproval = true;
                }

                if ($scope.tier2_approval_status == "Pending" || $scope.tier2_approval_status == "Rejected") {
                    $scope.edittier2dtl = true;
                    $scope.viewtier2dtl = false;
                }
                else {

                    $scope.edittier2dtl = false;
                    $scope.viewtier2dtl = true;
                }
                unlockUI();
            });

        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('tier1format_gid', tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }


        $scope.riskcodehistory = function (allocationdtl_gid, customer_name, customer_urn, tier2_code) {

            var modalInstance = $modal.open({
                templateUrl: '/riskcodeHistoryModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier2_code;
                $scope.ok = function () {
                    $modalInstance.close('closed');

                };

                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTierColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;

                });
            }
        }

        $scope.riskcodechange = function (allocationdtl_gid, tierallocation_gid, tier2preparation_gid, customer_name, customer_urn, tier2_code) {

            var modalInstance = $modal.open({
                templateUrl: '/riskcodechangeModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier2_code;
                $scope.ok = function () {
                    $scope.txtcode_changereason = "";
                    $modalInstance.close('closed');

                };

                $scope.cboriskcodechange = function (cboriskcode) {
                    if (tier2_code == cboriskcode) {
                        $scope.codechangereasonshow = false;

                    }
                    else {
                        $scope.txtcode_changereason = "";
                        $scope.codechangereasonshow = true;
                       
                    }
                }
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTierColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;

                });

                $scope.riskcodechangecancel = function (tmptier2_codechange) {
                    var params = {
                        tmptier2_codechange: tmptier2_codechange
                    }
                    lockUI();
                    var url = "api/TierMeeting/GetTier2ColorDelete"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtcode_changereason = "";
                            unlockUI();
                            var params = {
                                allocationdtl_gid: allocationdtl_gid
                            }
                            var url = "api/TierMeeting/GetTierColorDetails";
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.tiercodedtl = resp.data.tiercodedtl;

                            });
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

                $scope.riskcodechangesubmit = function () {
                    var params = {
                        tierallocation_gid: tierallocation_gid,
                        allocationdtl_gid: allocationdtl_gid,
                        tier2preparation_gid: tier2preparation_gid,
                        tier_code: $scope.cboriskcode,
                        tiercode_changereason: $scope.txtcode_changereason,
                        tier3_flag: "N"
                    }
                    
                    lockUI();
                    var url = "api/TierMeeting/PostTierColorUpdate"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            $modalInstance.close('closed');
                            activate();
                            unlockUI();
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

        $scope.verticalchange = function (cbovertical_gid) {
            var params = {
                vertical_gid: cbovertical_gid,
                month: $scope.cbomonth,
                tier2_flag: 'Y'
            }

            var url = 'api/TierMeeting/GetVerticalAllocationdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                $scope.tier2dtl = true;
            });
        }

        $scope.uploaddocument = function (val, val1, name) {
            if ($scope.txtdocument_title == undefined || $scope.txtdocument_title == "") {
                alert('Enter the Document Title to Upload Document');
                $("#addExternalupload").val('');
                return false;
            }
            else {
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
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('tier2preparartion_gid', localStorage.getItem('tier2preparation_gid'));
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/TierMeeting/PostTier2TrnUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.uploaddocument_list = resp.data.tier2document;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = '';
                    unlockUI();
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

        }

        $scope.uploadcancel = function (tier2document_gid) {
            var tier2upload = {
                tier2document_gid: tier2document_gid,
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid')
            }
            var url = 'api/TierMeeting/GetTier2TrnUploadCancel';
            SocketService.getparams(url, tier2upload).then(function (resp) {
                $scope.uploaddocument_list = resp.data.tier2document;
            });
        }
        $scope.monthchange = function (cbomonth) {
            var params = {
                month: cbomonth,
                tier2: 'Y',
                zonalmapping_gid: $scope.zonalmapping_gid
            }

            var url = 'api/TierMeeting/GetVertical';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_list = resp.data.tiervertical;

            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.updatetier2 = function () {
            lockUI();
            var vertical_name = $('#cboverticalname :selected').text();
            var headRMD_name = $('#cboemployeename :selected').text();
            var tier2dtl = {
                tier2preparation_gid: localStorage.getItem('tier2preparation_gid'),
                tier2_month: $scope.cbomonth,
                vertical_gid: $scope.cbovertical_gid,
                vertical: vertical_name,
                headRMD_gid: $scope.cboemployeegid,
                headRMD_name: headRMD_name,
                tier2_remarks: $scope.txttier2_remarks,
            }

            var url = "api/TierMeeting/PostUpdateTier2";
            SocketService.post(url, tier2dtl).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.tier2Preparation');

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
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier3Createcontroller', tier3Createcontroller);

    tier3Createcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function tier3Createcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier3Createcontroller';

        activate();

        function activate() {
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            lockUI();
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/TierMeeting/GetTier3Monthdtl';
            SocketService.get(url).then(function (resp) {
                $scope.monthname_list = resp.data.monthname;
                unlockUI();
            });
        }

        $scope.monthchange = function (cbomonth) {
            var params = {
                month: cbomonth,
                tier2: 'N',
                zonalmapping_gid:""
            }

            var url = 'api/TierMeeting/GetVertical';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_list = resp.data.tiervertical;
            });
        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('tier1format_gid', tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }

        $scope.verticalchange = function (cbovertical_gid) {
            var params = {
                vertical_gid: cbovertical_gid,
                month: $scope.cbomonth,
                tier2_flag: 'N'
            }
            lockUI();
            var url = 'api/TierMeeting/GetVerticalAllocationdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                $scope.tier2dtl = true;
                unlockUI();
            });
        }

        $scope.uploaddocument = function (val, val1, name) {
            if ($scope.txtdocument_title == undefined || $scope.txtdocument_title == "") {
                alert('Enter the Document Title to Upload Document');
                $("#addExternalupload").val('');
                return false;
            }
            else {
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
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/TierMeeting/PostTier3Upload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.uploaddocument_list = resp.data.tier3document;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = "";
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.uploadflag = 'Y';
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.uploadflag = 'N';
                    }
                });
            }
        }

        $scope.uploadcancel = function (tmp_documentGid) {
            var tier3upload = {
                tmp_documentGid: tmp_documentGid
            }
            var url = 'api/TierMeeting/GetTier3UploadCancel';
            SocketService.getparams(url, tier3upload).then(function (resp) {
                $scope.uploaddocument_list = resp.data.upload_list;
            });
        }

        $scope.submittier3 = function () {
            lockUI();
            if ($scope.uploadflag != 'Y') {
                Notify.alert('Atleast Upload One Document to Submit..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
            }
            else {
                var vertical_name = $('#cboverticalname :selected').text();
                var tier3dtl = {
                    MLRC_date: $scope.MLRCdate,
                    tier3_month: $scope.cbomonth,
                    vertical_gid: $scope.cbovertical_gid,
                    vertical: vertical_name,
                    follow_up: $scope.txttier3_followup,
                }
                var url = "api/TierMeeting/PostTier3Preparation";
                SocketService.post(url, tier3dtl).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        $state.go('app.tier3Preparation');

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

        $scope.riskcodechange = function (allocationdtl_gid, tierallocation_gid, customer_name, customer_urn, tier3_code) {
            
            var modalInstance = $modal.open({
                templateUrl: '/riskcodechangeModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtcode_changereason = "";
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier3_code;
                $scope.txtcode_changereason = "";
                $scope.ok = function () {
                    $modalInstance.close('closed');

                };
                $scope.cboriskcodechange = function (cboriskcode) {
                    if (tier3_code == cboriskcode) {
                        $scope.codechangereasonshow = false;
                        $scope.txtcode_changereason = "";
                    }
                    else {
                        $scope.codechangereasonshow = true;
                        $scope.txtcode_changereason = "";
                    }
                }

                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTierColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;

                });

                $scope.riskcodechangesubmit = function () {
                    var params = {
                        tierallocation_gid: tierallocation_gid,
                        allocationdtl_gid: allocationdtl_gid,
                        tier_code: $scope.cboriskcode,
                        tiercode_changereason: $scope.txtcode_changereason,
                        tier3_flag: "Y"
                    }
                    lockUI();
                    var url = "api/TierMeeting/PostTierColorUpdate"
                    SocketService.post(url, params).then(function (resp) {
                        console.log(resp.data.status);
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtcode_changereason = "";
                            $modalInstance.close('closed');
                            var params = {
                                vertical_gid: $scope.cbovertical_gid,
                                month: $scope.cbomonth,
                                tier2_flag: 'N'
                            }
                            lockUI();
                            var url = 'api/TierMeeting/GetVerticalAllocationdtl';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.tierallocationdtl = [];
                                $scope.tierallocationdtl = resp.data.tierallocationdtl;
                                $scope.tier2dtl = true;
                                unlockUI();
                            });
                            unlockUI();
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier3Preparation', tier3Preparation);

    tier3Preparation.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function tier3Preparation($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier3Preparation';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = 'api/TierMeeting/GetTier3Summary';
            SocketService.get(url).then(function (resp) {
                $scope.tier3preparation_list = resp.data.tier3preparation;
                $scope.count_pending = resp.data.count_pending;
                $scope.count_completed = resp.data.count_completed;
                if ($scope.tier3preparation_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.tier3preparation_list.length;
                    if ($scope.tier3preparation_list.length < 100) {
                        $scope.totalDisplayed = $scope.tier3preparation_list.length;
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
            if ($scope.totalDisplayed < $scope.tier3preparation_list.length) {
                $scope.totalDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.tier3preparation_list.length + " Records Only", "warning");
                return;
            }
        };

        $scope.createtier3preparation = function () {
            $state.go('app.tier3Create');
        }

        $scope.viewtier3completed = function (tier3preparation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/completetier3.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }
                $scope.completetier3 = function () {
                    lockUI();
                   
                    var tier3dtl = {
                        tier3preparation_gid: tier3preparation_gid,
                        completed_remarks: $scope.txttier3_completedremarks,
                    }
                    var url = "api/TierMeeting/PostTier3Complete";
                    SocketService.post(url, tier3dtl).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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

        $scope.viewtier3details = function (tier3preparation_gid) {
            localStorage.setItem('tier3preparation_gid', tier3preparation_gid);
            $state.go('app.tier3PreparationView');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier3PreparationView', tier3PreparationView);

    tier3PreparationView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function tier3PreparationView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier3PreparationView';

        activate();

        function activate() {
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            lockUI();

            var url = 'api/TierMeeting/GetTier3Monthdtl';
            SocketService.get(url).then(function (resp) {
                $scope.monthname_list = resp.data.monthname;
            });

            var url = 'api/Vertical/Vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var params = {
                tier3preparation_gid: localStorage.getItem('tier3preparation_gid')
            }
            var url = 'api/TierMeeting/GetTier3ViewDtl';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.mlrc_date = resp.data.MLRC_date;
                $scope.txtMLRC_date = resp.data.MLRC_Date;
                $scope.cbomonth = resp.data.tier3_month;
                $scope.monthname = resp.data.tier3_monthname;
                $scope.txttier3_followup = resp.data.follow_up;
                $scope.tier3_status = resp.data.tier3_status;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.uploaddocument_list = resp.data.tier3document;
                $scope.completed_date = resp.data.completed_date;
                $scope.completed_by = resp.data.completed_by;
                $scope.completed_flag = resp.data.completed_flag;
                $scope.completed_remarks = resp.data.completed_remarks;
                $scope.vertical = resp.data.vertical;
                $scope.cbovertical_gid = resp.data.vertical_gid;
                $scope.tierallocationdtl = resp.data.tierallocationdtl

                if ($scope.completed_flag == "N") {
                    $scope.edittier3dtl = true;
                    $scope.viewtier3dtl = false;
                }
                else {

                    $scope.edittier3dtl = false;
                    $scope.viewtier3dtl = true;
                }
                unlockUI();
            });
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.viewcustomerdtl = function (allocationdtl_gid, tier1format_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('tier1format_gid', tier1format_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/tierCustomer360";
            window.open(URL, '_blank');
        }

        $scope.riskcodechange = function (allocationdtl_gid, tierallocation_gid, customer_name, customer_urn, tier3_code) {
            var modalInstance = $modal.open({
                templateUrl: '/riskcodechangeModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier3_code;
                $scope.txtcode_changereason = "";
                $scope.ok = function () {
                    $modalInstance.close('closed');

                };
                $scope.cboriskcodechange = function (cboriskcode) {
                    if (tier3_code == cboriskcode) {
                        $scope.codechangereasonshow = false;

                    }
                    else {
                        $scope.codechangereasonshow = true;
                        $scope.txtcode_changereason = "";
                    }
                }

                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTierColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;

                });

                $scope.riskcodechangesubmit = function () {
                    var params = {
                        tierallocation_gid: tierallocation_gid,
                        allocationdtl_gid: allocationdtl_gid,
                        tier_code: $scope.cboriskcode,
                        tiercode_changereason: $scope.txtcode_changereason,
                        tier3_flag: "Y"
                    }
                    lockUI();
                    var url = "api/TierMeeting/PostTierColorUpdate"
                    SocketService.post(url, params).then(function (resp) {
                        console.log(resp.data.status);
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            $modalInstance.close('closed');
                            activate();
                            unlockUI();
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


        $scope.riskcodehistory = function (allocationdtl_gid, customer_name, customer_urn, tier3_code) {

            var modalInstance = $modal.open({
                templateUrl: '/riskcodeHistoryModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.customer_name = customer_name;
                $scope.customer_urn = customer_urn;
                $scope.cboriskcode = tier3_code;
                $scope.ok = function () {
                    $modalInstance.close('closed');

                };

                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/TierMeeting/GetTierColorDetails";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.tiercodedtl = resp.data.tiercodedtl;

                });
            }
        }

        $scope.monthchange = function (cbomonth) {
            var params = {
                month: cbomonth,
                tier2: 'N',
                zonalmapping_gid: ""
            }

            var url = 'api/TierMeeting/GetVertical';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_list = resp.data.tiervertical;

            });
        }

        $scope.uploaddocument = function (val, val1, name) {
            if ($scope.txtdocument_title == undefined || $scope.txtdocument_title == "") {
                alert('Enter the Document Title to Upload Document');
                $("#addExternalupload").val('');
                return false;
            }
            else {
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
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('tier3preparation_gid', localStorage.getItem('tier3preparation_gid'));
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/TierMeeting/PostTrnTier3Upload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.uploaddocument_list = resp.data.tier3document;
                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = "";
                    unlockUI();
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
        }

        $scope.uploadcancel = function (tier3document_gid) {
            var tier3upload = {
                tier3document_gid: tier3document_gid,
                tier3preparation_gid: localStorage.getItem('tier3preparation_gid')
            }
            var url = 'api/TierMeeting/GetTier3TrnUploadCancel';
            SocketService.getparams(url, tier3upload).then(function (resp) {
                $scope.uploaddocument_list = resp.data.tier3document;
            });
        }

        $scope.updatetier3 = function () {
            lockUI();
            var vertical_name = $('#cboverticalname :selected').text();
            var tier3dtl = {
                tier3preparation_gid: localStorage.getItem('tier3preparation_gid'),
                tier3_month: $scope.cbomonth,
                vertical_gid: $scope.cbovertical_gid,
                vertical: vertical_name,
                MLRC_date: $scope.txtMLRC_date,
                follow_up: $scope.txttier3_followup,
            }
            console.log(tier3dtl);
            var url = "api/TierMeeting/PostUpdateTier3";
            SocketService.post(url, tier3dtl).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();

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
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TierCustomer360controller', TierCustomer360controller);

    TierCustomer360controller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function TierCustomer360controller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TierCustomer360controller';

        activate();

        function activate() {
            lockUI();
            var allocationdtl_gid = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
            }
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.zonal_name = resp.data.zonal_name;
                $scope.state_name = resp.data.state_name;
                $scope.district_name = resp.data.district_name;
                $scope.assigned_RM = resp.data.assigned_RM;
                $scope.customername = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.ZonalRMname = resp.data.ZonalRMname;
                $scope.clientName = resp.data.customername;
            });

            var url = "api/visitReport/GetAllocationLogDetail";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.scheduleList = resp.data.schedulelogdtl;
                $scope.calllogdtlList = resp.data.calllogdtl;

            });

            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.customerdetails = resp.data;
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.customerCollateral = resp.data.collateraldtl;
                $scope.holdallocationlist = resp.data.holdallocation;
                $scope.customerguarantorlist = resp.data.Guarantorsdtl;
                console.log('Gurantor',resp);
                $scope.customerPromotorlist = resp.data.Promoterdtl;
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

            var url = "api/customerManagement/HistoryEscrowSummary";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowlist = resp.data.escrowSummary;
                }
            });

            var url = "api/allocationManagement/getAllocationdocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                    $scope.upload_list = resp.data.upload_list;
                    $scope.documentUpload = true;
                }
                else {

                    $scope.documentNotUpload = true;
                }
            });

            var url = "api/visitReport/getvisitreportdtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {

                $scope.visitreport_generateGid = resp.data.visitreport_generateGid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.txtbusiness_vintage = resp.data.business_vintage,
                $scope.cbotypeof_loan = resp.data.typeof_loanvertical,
                $scope.txtbusiness_sector = resp.data.business_sector,
                $scope.txtregister_address = resp.data.registeredoffice_address,
                $scope.cboriskcode = resp.data.risk_code,
                $scope.txtactual_address = resp.data.present_address,
                $scope.txtcontact_dtl1 = resp.data.contact_details1,
                $scope.txtcontact_dtl2 = resp.data.contact_details2,
                $scope.cborisk_reviewtype = resp.data.typeof_riskreview;
                $scope.txtlattitude = resp.data.visit_latitude;
                $scope.txtlongitude = resp.data.visit_longitude;
                $scope.txtbusiness_client = resp.data.clientbusiness_vintage,
                $scope.txtprimary_chain = resp.data.primarysecondary_valuechain,
                $scope.cbogenetic_code = resp.data.geneticcode_complied,
                $scope.cboRMD_gid = resp.data.RMD_visitedGid,
                $scope.RMD_visitedname = resp.data.RMD_visitedname;
                $scope.txtPPA_name = resp.data.PPA_name;
                $scope.cbovisit_done = resp.data.visit_done,
                $scope.txtpurposeof_loan = resp.data.purpose_ofloan,
                $scope.txtrequestedloan_byclient = resp.data.requestedamount_byclient,
                $scope.txtsantionloan_bycredit = resp.data.sanctionedamount_byclient;
                $scope.txtdisbursement_amount = resp.data.disbursement_amount,
                $scope.txttotalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.cborepayment_track = resp.data.repayment_track,
                $scope.cbobasic_records = resp.data.basicrecords_maintain,
                $scope.txtturnover_lastfy = resp.data.turnover_lastFY,
                $scope.txtpresent_fysales = resp.data.presentFY_sales,
                $scope.txtdeferral_pendency = resp.data.deferral_pendency,
                $scope.txtadditional_funding = resp.data.adequacy_additionalfunding,
                $scope.txtcbototal_groups = resp.data.total_noofGroups,
                $scope.txtCBOgroup_funded = resp.data.CBOfunded_noofGroups,
                $scope.txtRMDvisit_groupcount = resp.data.RMD_visitgroups,
                $scope.txtassetverification_comment = resp.data.assetverification_createdoutofloan,
                $scope.txtsecurity_details = resp.data.assetverification_securitydtls,
                $scope.txtassetverification_mortagged = resp.data.assetverification_mortgaged,
                $scope.txtROCcreation = resp.data.assetverification_ROCcreation,
                $scope.txtbasicrecord_remarks = resp.data.basicrecords_remarks,
                $scope.txtpurpose_funding = resp.data.purposeof_funding,
                $scope.txt_utilisationdtls = resp.data.utilisation_details,
                $scope.txtadequacyloan_samunnati = resp.data.adequacy_loanamount,
                $scope.txtadequacyloan_impactassessment = resp.data.adequacy_impactassessment,
                $scope.txtportfolio_noofmember = resp.data.portfolio_noofmembers,
                $scope.txtportfolio_activemembers = resp.data.portfolio_activemembers,
                $scope.txtportfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.txtportfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.txtportfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.txtportfolio_overdueAmount = resp.data.overdue_amount,
                $scope.txtportfolio_fundingoverdue = resp.data.overdueaccount_funding,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtrepayment_trackremarks = resp.data.repayment_trackremarks,
                //$scope.txtloan_clientdate = resp.data.loan_clientdate,
                $scope.txtoverdue = resp.data.overdue,
                $scope.txtborrower_commitment = resp.data.borrower_commitment,
                $scope.txtpending_documentation = resp.data.pending_documentation,
                //$scope.txtasset_verification = resp.data.asset_verification,
                $scope.txtbriefdtls_client = resp.data.briefdtls_client,
                $scope.txtenduse_loan = resp.data.enduse_loan,
                //$scope.txtadequacy_loan = resp.data.adequacy_loan,
                $scope.txtoverall_remarks = resp.data.overall_remarks,
                $scope.txtPDD_compliance = resp.data.PDD_compliance,
                $scope.txtbriefrpt_financials = resp.data.briefrpt_financials,
                $scope.txtbriefrpt_process = resp.data.briefrpt_process,
                $scope.txtbriefrpt_customer = resp.data.briefrpt_customer,
                 $scope.txtvaluechain_mapanalysis = resp.data.valuechain_mapanalysis,
                $scope.txtcompetitorbusiness_segment = resp.data.competitorbusiness_segment;
                $scope.txtbriefrpt_learnings = resp.data.briefrpt_learnings,
                $scope.txtbriefrpt_valuechain = resp.data.briefrpt_valuechain,
                $scope.editvisittype = resp.data.editvisittype;
                if (resp.data.RM_name != null) {
                    $scope.relationship_managername = resp.data.RM_name
                }
                if (resp.data.constitution != null) {
                    $scope.constitution = resp.data.constitution
                }
                if (resp.data.credit_managername != null) {
                    $scope.credit_managername = resp.data.credit_managername;
                }
                if (resp.data.visit_date != null) {
                    var p = resp.data.visit_date.split(/\D/g)
                    $scope.visitdate = [p[2], p[1], p[0]].join("-");
                }

                if (resp.data.dealing_withsince != null) {
                    var p = resp.data.dealing_withsince.split(/\D/g)
                    $scope.txtincorporated_date = [p[2], p[1], p[0]].join("-");
                }

                if (resp.data.disbursement_date != null) {
                    var p = resp.data.disbursement_date.split(/\D/g)
                    $scope.txtdisbursement_date = [p[2], p[1], p[0]].join("-");
                }

            });

            var url = "api/visitReport/getvisitReportDocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreportdocument = resp.data.visitreportdocument;
            });

            var url = "api/visitReport/getvisitReportPhoto";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreportphoto = resp.data.visitreportphoto;
            });
 
            var url = "api/TierMeeting/GetViewTierObservationdtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                //$scope.customer_urn = resp.data.customer_urn;
                $scope.dateof_RMDvisit = resp.data.dateof_RMDvisit;
                $scope.report_pertainingto = resp.data.report_pertainingto;
                $scope.vertical = resp.data.vertical;
                $scope.disbursement_amount = resp.data.disbursement_amount;
                $scope.approving_authority = resp.data.approving_authority;
                $scope.loansanction_date = resp.data.loansanction_date;
                $scope.relationship_manager_name = resp.data.relationship_manager_name;
                $scope.PPA_name = resp.data.PPA_name;
                $scope.RMDvisit_officialname = resp.data.RMDvisit_officialname;
                $scope.loandisbursement_date = resp.data.loandisbursement_date;
                $scope.people_accompaniedRMD = resp.data.people_accompaniedRMD;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.outstanding_amount = resp.data.outstanding_amount;
                $scope.current_DPD = resp.data.current_DPD;
                $scope.contact_details1 = resp.data.contact_details1;
                $scope.contact_details2 = resp.data.contact_details2;
                $scope.observation_flag = resp.data.observation_flag;
                $scope.cboriskcode = resp.data.risk_code;
                $scope.criticalobservation = resp.data.criticalTierobservation;
                $scope.tiercodedtl = resp.data.tierReportdtl;
              
                unlockUI();
            });
            var tier1format_gid = {
                tier1format_gid: localStorage.getItem('tier1format_gid')
            }
            var url = "api/TierMeeting/GetTier1Format360Dtl";
            SocketService.getparams(url, tier1format_gid).then(function (resp) {
                $scope.txtobservations = resp.data.tier1_observations;
                if (resp.data.tier1_code == null || resp.data.tier1_code == "") {
                }
                else {
                    $scope.cboriskcode = resp.data.tier1_code;
                }
               
                $scope.txtrationale_justification = resp.data.tier1_justification;
                $scope.txtprocess_gap = resp.data.tier1_processgap;
                $scope.txtcode_changereason = resp.data.tier1code_changereason;
                $scope.tier1code_changeflag = resp.data.tier1code_changeflag;
                $scope.txtimprovement_recommendation = resp.data.tier1_processrecommendation;
                $scope.txtmanagement_comments = resp.data.tier1_managementcomments;
                $scope.txtcheifheadreverts_actionplan = resp.data.tier1_reverts_actionplan;
                $scope.txtATR_date = resp.data.tier1_atrdate;
                $scope.tier1format_gid = resp.data.tier1format_gid;
                $scope.tier1_approvalstatus = resp.data.tier1_approvalstatus;
                $scope.tier1approvallog = resp.data.tier1approvallog;
                $scope.uploaddocument_list = resp.data.tier1doc;
                $scope.tier1rejectlog = resp.data.tier1rejectlog;

                if ($scope.tier1code_changeflag == 'Y') {
                    $scope.disablecodechangereasonshow = true;
                }
                else {
                    $scope.disablecodechangereasonshow = false;
                }

                if (resp.data.tier1approvallog == null) {
                    $scope.nohistoryapproval = true;
                }
                else {
                    $scope.historyapproval = true;
                }
                if (resp.data.tier1rejectlog == null) {
                    $scope.rejecthistory = false;
                }
                else {
                    $scope.rejecthistory = true;
                }
            });
          
            var url = 'api/TierMeeting/GetTier2Report360Dtl';
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                console.log(resp);
                $scope.tier2zonal_name = resp.data.zonal_name;
                $scope.tier2_monthname = resp.data.tier2_monthname;
                $scope.vertical = resp.data.vertical;
                $scope.headRMD_name = resp.data.headRMD_name;
                $scope.txttier2_remarks = resp.data.tier2_remarks;
                $scope.tier2_approval_status = resp.data.tier2_approval_status;
                $scope.tier2_submitteddate = resp.data.created_date;
                $scope.tier2_submittedby = resp.data.created_by;
                $scope.uploaddocument2_list = resp.data.tier2document;
                $scope.tier2approvallog = resp.data.tier2approvallog;
                $scope.tier2_approveddate = resp.data.tier2_approveddate;

                if (resp.data.tier2approvallog == null) {
                    $scope.tier2nohistoryapproval = true;
                }
                else {
                    $scope.tier2historyapproval = true;
                }
                unlockUI();
            });

            var url = 'api/TierMeeting/GetTier3Report360Dtl';
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {

                $scope.mlrc_date = resp.data.MLRC_date;
                $scope.monthname = resp.data.tier3_month;
                $scope.txttier3_followup = resp.data.follow_up;
                $scope.tier3_status = resp.data.tier3_status;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.uploaddocument3_list = resp.data.tier3document;
                $scope.completed_date = resp.data.completed_date;
                $scope.completed_by = resp.data.completed_by;
                $scope.completed_flag = resp.data.completed_flag;
                $scope.completed_remarks = resp.data.completed_remarks;
                $scope.vertical = resp.data.vertical;
                unlockUI();
            });
        }

        $scope.close = function () {
            console.log('close');
            window.close();
        }

        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
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
                var url = "api/customerManagement/HistoryEscrowView";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.escrowview = resp.data;
                });

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

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tierReportcontroller', tierReportcontroller);

    tierReportcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function tierReportcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tierReportcontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = 'api/TierMeeting/GetTier3CompletedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.tier3preparation_list = resp.data.tier3preparation;
                if ($scope.tier3preparation_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.tier3preparation_list.length;
                    if ($scope.tier3preparation_list.length < 100) {
                        $scope.totalDisplayed = $scope.tier3preparation_list.length;
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
            if ($scope.totalDisplayed < $scope.tier3preparation_list.length)
            {
                $scope.totalDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.tier3preparation_list.length + " Records Only", "warning");
                return;
            }
        };

        $scope.viewtier3details = function (tier3preparation_gid) {
            localStorage.setItem('tier3preparation_gid', tier3preparation_gid);
            $state.go('app.tierReportView');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('transferApproval360controller', transferApproval360controller);

    transferApproval360controller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function transferApproval360controller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'transferApproval360controller';

        activate();

        function activate() {
            lockUI();

            $scope.MyApprovalPage = localStorage.getItem('MyApprovalPage');

            var allocationdtl_gid = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
            }
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.state_name = resp.data.transferFrom_statename;
                $scope.district_name = resp.data.transferFrom_districtname;
                $scope.assigned_RM = resp.data.transferfrom_assignedRM;
                $scope.customername = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.ZonalRMname = resp.data.transferFrom_ZonalRMname;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.zonal_name = resp.data.zonal_name;
            });

            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.customerdetails = resp.data;
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.customerCollateral = resp.data.collateraldtl;
                $scope.customerguarantorlist = resp.data.Guarantorsdtl;
                $scope.customerPromotorlist = resp.data.Promoterdtl;
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

            var allocation_transfergid = {
                allocation_transfergid: localStorage.getItem('allocation_transfergid')
            }
            var url = "api/allocationTransfer/getviewtransapprovaldtl";
            SocketService.getparams(url, allocation_transfergid).then(function (resp) {
                $scope.transapprovaldtl = resp.data;
            });

            var url = "api/allocationManagement/getAllocationdocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                    $scope.upload_list = resp.data.upload_list;
                }
                else {
                }
            });

            var customer_gid = {
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }
            var url = 'api/raiseLegalSR/GetDemandNoticedtl';

            SocketService.getparams(url, customer_gid).then(function (resp) {
                $scope.demandnotice_list = resp.data.demandnotice_list;
                $scope.demand_status = resp.data.demand_status;

            });

            if (localStorage.getItem('MyAllocation') == "Y") {
                $scope.MyAllocationView = true;
            }
            else {
                $scope.AllocationSummary = true;
            }

            unlockUI();
        }


        $scope.tranferApprove = function () {
            lockUI();
            var params = {
                transferapproval_gid: localStorage.getItem('transferapproval_gid'),
                approval_Remarks: $scope.textapprovalRemarks
            }
            console.log(params)
            if (localStorage.getItem('TransferFromApproval') == "Y") {
                var url = "api/allocationTransfer/posttransferFromApprove";
            }
            else {
                if (localStorage.getItem('ZonalApprovalToFlag') == "Y") {
                    var url = "api/allocationTransfer/postransferToApprove";
                }
                else {
                    var url = "api/allocationTransfer/posttransferFromApprove";
                }

            }

            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.transferApproval');
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


        $scope.tranferReject = function () {
            lockUI();
            var params = {
                transferapproval_gid: localStorage.getItem('transferapproval_gid'),
                approval_Remarks: $scope.textapprovalRemarks
            }

            if (localStorage.getItem('TransferFromApproval') == "Y") {
                var url = "api/allocationTransfer/posttransferFromReject";
            }
            else {
                if (localStorage.getItem('ZonalApprovalToFlag') == "Y") {
                    var url = "api/allocationTransfer/posttransferToReject";
                }
                else {
                    var url = "api/allocationTransfer/posttransferFromReject";
                }
            }
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.transferApproval');
                    activate();

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.transferApproval');
                    activate();
                }
            });

        }

        $scope.downloads = function (val1, val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('transferApprovalcontroller', transferApprovalcontroller);

    transferApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location','SweetAlert', '$route', 'ngTableParams'];

    function transferApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'transferApprovalcontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayedCrossZonal = 100;
            $scope.totalDisplayedWithinZonal = 100;
            $scope.totalDisplayedHistory = 100;
          
            var url = "api/allocationTransfer/gettransferApprovalSummary";
            SocketService.get(url).then(function (resp) {
                $scope.allocationtransferList = resp.data.allocationtransferdtl;
                if ($scope.allocationtransferList == null) {
                    $scope.totalCrossZonal = 0;
                    $scope.totalDisplayedCrossZonal = 0;
                }
                else {
                    $scope.totalCrossZonal = $scope.allocationtransferList.length;
                    if ($scope.allocationtransferList.length < 100) {
                        $scope.totalDisplayedCrossZonal = $scope.allocationtransferList.length;
                    }
                }
                $scope.zonalapproval = resp.data.zonalapproval;
                if ($scope.zonalapproval == null) {
                    $scope.totalWithinZonal = 0;
                    $scope.totalDisplayedWithinZonal = 0;
                }
                else {
                    $scope.totalWithinZonal = $scope.zonalapproval.length;
                    if ($scope.zonalapproval.length < 100) {
                        $scope.totalDisplayedWithinZonal = $scope.zonalapproval.length;
                    }
                }
                $scope.count_myapproval = resp.data.count_myapproval;
                $scope.count_mypendingApproval = resp.data.count_mypendingApproval;
                $scope.count_myApproved = resp.data.count_myApproved;
                $scope.count_myrejected = resp.data.count_myrejected;
                $scope.count_mywithinzonalApproval = resp.data.count_mywithinzonalApproval;
                $scope.count_mycrosszonalApproval = resp.data.count_mycrosszonalApproval;
            });
            unlockUI();
        }

        $scope.approvalhistoryclick = function () {
            lockUI();
            var url = "api/allocationTransfer/getApprovalHistorySummary";
            SocketService.get(url).then(function (resp) {
                $scope.allocationHistoryList = resp.data.allocationtransferdtl;
                if ($scope.allocationHistoryList == null) {
                    $scope.totalHistory = 0;
                    $scope.totalDisplayedHistory = 0;
                }
                else {
                    $scope.totalHistory = $scope.allocationHistoryList.length;
                    if ($scope.allocationHistoryList.length < 100) {
                        $scope.totalDisplayedHistory = $scope.allocationHistoryList.length;
                    }
                }
                unlockUI();
            });
        }

        $scope.loadMoreWithin = function (pageWithincount) {
            if (pageWithincount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageWithincount);
            $scope.totalDisplayedWithinZonal += Number;
            unlockUI();
        };

        $scope.loadMoreCross = function (pageCrosscount) {
            if (pageCrosscount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pageCrosscount);
            $scope.totalDisplayedCrossZonal += Number;
            unlockUI();
        };

        $scope.loadMoreHistory = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayedHistory += Number;
            unlockUI();
        };

        $scope.TransferFromAllocationView = function (allocationdtl_gid, customer_gid, transferapproval_gid, allocation_transfergid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('transferapproval_gid', transferapproval_gid);
            localStorage.setItem('allocation_transfergid', allocation_transfergid);
            localStorage.setItem('TransferFromApproval', 'Y');
            localStorage.setItem('MyApprovalPage', 'Y');
            $state.go('app.transferApproval360');
        }

        $scope.TransferToAllocationView = function (allocationdtl_gid, customer_gid, transferapproval_gid, zonalapprovalto_Flag, allocation_transfergid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('transferapproval_gid', transferapproval_gid);
            localStorage.setItem('ZonalApprovalToFlag', zonalapprovalto_Flag);
            localStorage.setItem('allocation_transfergid', allocation_transfergid);
            localStorage.setItem('TransferFromApproval', 'N');
            localStorage.setItem('MyApprovalPage', 'Y');
            $state.go('app.transferApproval360');
        }

        $scope.TransferApprovalHistory = function (allocationdtl_gid, customer_gid, allocation_transfergid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('allocation_transfergid', allocation_transfergid);
            $state.go('app.transferApprovalHistory');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('visitReportCancelcontroller', visitReportCancelcontroller);

    visitReportCancelcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService','cmnfunctionService'];

    function visitReportCancelcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'visitReportCancelcontroller';
        var allocationdtl_gid = $location.search().allocationdtl_gid;
        var customer_gid = $location.search().allocation_customer_gid;
        
        activate();

        function activate() {
            lockUI();

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open4 = true;
            };
            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open5 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, params).then(function (resp) {

                $scope.clientName = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;

                $scope.relationship_managername = resp.data.relationship_managername;
                $scope.credit_managername = resp.data.credit_managername;
                $scope.creditmanager_gid = resp.data.creditmanager_gid;
                $scope.relationship_managerGid = resp.data.relationship_managerGid;
                $scope.assigned_RMD = resp.data.assigned_RM;
                $scope.assignedRMD_gid = resp.data.assignedRM_gid;

            });


            var url = "api/visitReport/getvisitreportdtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreport_generateGid = resp.data.visitreport_generateGid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.visitdate = resp.data.visit_date;
                $scope.txtlattitude = resp.data.visit_latitude;
                $scope.txtlongitude = resp.data.visit_longitude;
                if (resp.data.constitution != null) {
                    $scope.txtconstitution = resp.data.constitution
                }
                if (resp.data.dealing_withsince != "0001-01-01T00:00:00") {
                    $scope.txtfirstdisb_date = resp.data.dealing_withsince
                }
                $scope.cboriskcode = resp.data.risk_code,
                $scope.cborisk_reviewtype = resp.data.typeof_riskreview;
                $scope.txtbusiness_vintage = resp.data.business_vintage,
                $scope.cbotypeof_loan = resp.data.typeof_loanvertical,
                $scope.txtbusiness_sector = resp.data.business_sector,
                $scope.txtregister_address = resp.data.registeredoffice_address,
                $scope.txtactual_address = resp.data.present_address,
                $scope.txtcontact_dtl1 = resp.data.contact_details1,
                $scope.txtcontact_dtl2 = resp.data.contact_details2,
                //$scope.firstdisb_date = resp.data.relationship_startedfrom
                $scope.txtbusiness_client = resp.data.clientbusiness_vintage,
                $scope.txtprimary_chain = resp.data.primarysecondary_valuechain,
                $scope.cbogenetic_code = resp.data.geneticcode_complied,
                $scope.cboRMD_gid = resp.data.RMD_visitedGid,
                $scope.RMD_visitedname = resp.data.RMD_visitedname;
                if (resp.data.RM_name != null) {
                    $scope.relationship_managername = resp.data.RM_name
                }
                if ($scope.txtPPA_name == "") {
                    $scope.txtPPA_name = resp.data.PPA_name;
                }

                if (resp.data.credit_managername != null) {
                    $scope.credit_managername = resp.data.credit_managername;
                }
                $scope.cbovisit_done = resp.data.visit_done,
                $scope.txtpurposeof_loan = resp.data.purpose_ofloan,
                $scope.txtrequestedloan_byclient = resp.data.requestedamount_byclient;
                $scope.requestedloan_byclient = resp.data.requestedamount_byclient;
                if (resp.data.disbursement_date != "0001-01-01T00:00:00") {
                    $scope.txtdisbursement_date = resp.data.disbursement_date
                }
                $scope.changedisbursement_amount = resp.data.disbursement_amount,
                $scope.txttotalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.totalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.cborepayment_track = resp.data.repayment_track,
                $scope.cbobasic_records = resp.data.basicrecords_maintain,
                $scope.txtturnover_lastfy = resp.data.turnover_lastFY,
                $scope.turnover_lastfy = resp.data.turnover_lastFY,
                $scope.txtpresent_fysales = resp.data.presentFY_sales,
                $scope.present_fysales = resp.data.presentFY_sales,
                $scope.txtdeferral_pendency = resp.data.deferral_pendency,
                $scope.txtcbototal_groups = resp.data.total_noofGroups,
                $scope.txtCBOgroup_funded = resp.data.CBOfunded_noofGroups,
                $scope.txtRMDvisit_groupcount = resp.data.RMD_visitgroups,
                $scope.txtassetverification_comment = resp.data.assetverification_createdoutofloan,
                $scope.txtsecurity_details = resp.data.assetverification_securitydtls,
                $scope.txtassetverification_mortagged = resp.data.assetverification_mortgaged,
                $scope.txtROCcreation = resp.data.assetverification_ROCcreation,
                $scope.txtpurpose_funding = resp.data.purposeof_funding,
                $scope.txt_utilisationdtls = resp.data.utilisation_details,
                $scope.txtadequacyloan_samunnati = resp.data.adequacy_loanamount,
                $scope.txtadequacyloan_impactassessment = resp.data.adequacy_impactassessment,
                $scope.txtadditional_funding = resp.data.adequacy_additionalfunding,
                $scope.txtbasicrecord_remarks = resp.data.basicrecords_remarks,
                $scope.txtportfolio_noofmember = resp.data.portfolio_noofmembers,
                $scope.txtportfolio_activemembers = resp.data.portfolio_activemembers,
                $scope.txtportfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.portfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.txtportfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.portfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.txtportfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.portfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.txtportfolio_overdueAmount = resp.data.overdue_amount,
                $scope.portfolio_overdueAmount = resp.data.overdue_amount,
                $scope.txtportfolio_fundingoverdue = resp.data.overdueaccount_funding,
                $scope.portfolio_fundingoverdue = resp.data.overdueaccount_funding,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                //$scope.txtloan_clientdate = resp.data.loan_clientdate,
                $scope.txtoverdue = resp.data.overdue,
                $scope.txtborrower_commitment = resp.data.borrower_commitment,
                $scope.txtpending_documentation = resp.data.pending_documentation,
                //$scope.txtasset_verification = resp.data.asset_verification,
                $scope.txtbriefdtls_client = resp.data.briefdtls_client,
                $scope.txtenduse_loan = resp.data.enduse_loan,
                //$scope.txtadequacy_loan = resp.data.adequacy_loan,
                $scope.txtoverall_remarks = resp.data.overall_remarks,
                $scope.txtPDD_compliance = resp.data.PDD_compliance,
                $scope.txtbriefrpt_financials = resp.data.briefrpt_financials,
                $scope.txtbriefrpt_process = resp.data.briefrpt_process,
                $scope.txtvaluechain_mapanalysis = resp.data.valuechain_mapanalysis,
                $scope.txtcompetitorbusiness_segment = resp.data.competitorbusiness_segment,
                $scope.txtbriefrpt_customer = resp.data.briefrpt_customer,
                $scope.txtbriefrpt_learnings = resp.data.briefrpt_learnings,
                $scope.txtbriefrpt_valuechain = resp.data.briefrpt_valuechain,
                $scope.visittypedtl = resp.data.visittype;
                $scope.txtrepayment_borrowings = resp.data.repayment_trackremarks,
                $scope.editvisittype = resp.data.editvisittype;
                $scope.cbovisit_done = [];
                if (resp.data.editvisittype != null) {
                    var count = resp.data.editvisittype.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.visittypedtl.map(function (x) { return x.vistdone_gid; }).indexOf(resp.data.editvisittype[i].vistdone_gid); 
                        $scope.cbovisit_done.push($scope.visittypedtl[indexs]);
                    }
                }
                if (resp.data.requestedamount_byclient != "" && resp.data.requestedamount_byclient !=null) {
                    
                    var str = resp.data.requestedamount_byclient.replace(/,/g, '');
                    $scope.txtrequestedloan_byclient = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_requestedloan').innerHTML = inWords(str);
                }
                if (resp.data.disbursement_amount != "" && resp.data.disbursement_amount != null) {
                    var str = resp.data.disbursement_amount.replace(/,/g, '');
                    var str = str.split('.')[0];
                    $scope.txtdisbursement_amount = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_disbursementamount').innerHTML = inWords(str);
                }
                if (resp.data.totalloan_outstanding != "" && resp.data.totalloan_outstanding !=null) {
                    var str = resp.data.totalloan_outstanding.replace(/,/g, '');
                    $scope.txttotalloan_oustanding = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_totalloan').innerHTML = inWords(str);
                }
                if (resp.data.presentFY_sales != "" && resp.data.presentFY_sales!=null) {
                    var str = resp.data.presentFY_sales.replace(/,/g, '');
                    $scope.txtpresent_fysales = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_presentFY').innerHTML = inWords(str);
                }
                if (resp.data.turnover_lastFY != "" && resp.data.turnover_lastFY!=null) {
                    var str = resp.data.turnover_lastFY.replace(/,/g, '');
                    $scope.txtturnover_lastfy = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_turnoverFY').innerHTML = inWords(str);
                }
                if (resp.data.total_disbursementamount != "" && resp.data.total_disbursementamount!=null) {
                    var str = resp.data.total_disbursementamount.replace(/,/g, '');
                    $scope.txtportfoliototal_loandisbursement = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_totalamount').innerHTML = inWords(str);
                }

                if (resp.data.outstanding_ondate != "" && resp.data.outstanding_ondate != null) {
                    var str = resp.data.outstanding_ondate.replace(/,/g, '');
                    $scope.txtportfolio_outstandingdate = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_outstandingdate').innerHTML = inWords(str);
                }
                if (resp.data.overdue_beneficiary != "" && resp.data.overdue_beneficiary != null) {
                    var str = resp.data.overdue_beneficiary.replace(/,/g, '');
                    $scope.txtportfolio_overduebeneficary = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_overduebeneficary').innerHTML = inWords(str);
                }
                if (resp.data.overdue_amount != "" && resp.data.overdue_amount != null) {
                    var str = resp.data.overdue_amount.replace(/,/g, '');
                    $scope.txtportfolio_overdueAmount = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_overdueAmount').innerHTML = inWords(str);
                }
                if (resp.data.overdueaccount_funding != "" && resp.data.overdueaccount_funding != null) {
                    var str = resp.data.overdueaccount_funding.replace(/,/g, '');
                    $scope.txtportfolio_fundingoverdue = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_fundingoverdue').innerHTML = inWords(str);
                }
            });

            var customer_gid = {
                customer_gid:customer_gid
            }
            var url = "api/visitReport/GetSanctionTenurePeriod";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.txtsantionloan_bycredit = resp.data.totalsanction_amount;
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

            var url = "api/visitReport/getvisitReportDocument";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreportdocument = resp.data.visitreportdocument;
            });
            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = "api/visitReport/getvisitReportPhoto";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreportphoto = resp.data.visitreportphoto;
                unlockUI();
            });


        }

        // Basic Details
        $scope.business_vintage = function (string) {
            if (string.length >= 64) {
                $scope.messagebusiness_vintage = "Allowed Only 64 Characters";
            }
            else {
                $scope.messagebusiness_vintage = ""
            }
        }
        $scope.business_sector = function (string) {
            if (string.length >= 128) {
                $scope.messagebusiness_sector = "Allowed Only 128 Characters";
            }
            else {
                $scope.messagebusiness_sector = ""
            }
        }
        $scope.register_address = function (string) {
            if (string.length >= 256) {
                $scope.messageregister_address = "Allowed Only 256 Characters";
            }
            else {
                $scope.messageregister_address = ""
            }
        }
        $scope.actual_address = function (string) {
            if (string.length >= 256) {
                $scope.messageactual_address = "Allowed Only 256 Characters";
            }
            else {
                $scope.messageactual_address = ""
            }
        }
        $scope.contact_dtl1 = function (string) {
            if (string.length >= 256) {
                $scope.messagecontact_dtl1 = "Allowed Only 256 Characters";
            }
            else {
                $scope.messagecontact_dtl1 = ""
            }
        }
        $scope.contact_dtl2 = function (string) {
            if (string.length >= 256) {
                $scope.messagecontact_dtl2 = "Allowed Only 256 Characters";
            }
            else {
                $scope.messagecontact_dtl2 = ""
            }
        }
        $scope.lattitude = function (string) {
            if (string.length >= 32) {
                $scope.message_lattitude = "Allowed Only 32 Characters";
            }
            else {
                $scope.message_lattitude = ""
            }
        }
        $scope.longitude = function (string) {
            if (string.length >= 32) {
                $scope.message_longitude = "Allowed Only 32 Characters";
            }
            else {
                $scope.message_longitude = ""
            }
        }

        // Visit Details
        $scope.primarychain = function (string) {
            if (string.length >= 128) {
                $scope.message = "Allowed Only 128 Characters";
            }
            else {
                $scope.message = ""
            }
        }
        $scope.purposeof_loan = function (string) {
            if (string.length >= 128) {
                $scope.message_loan = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_loan = ""
            }
        }
        $scope.overdue = function (string) {
            if (string.length >= 128) {
                $scope.message_overdue = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_overdue = ""
            }
        }
        $scope.repayment_borrowings = function (string) {
            if (string.length >= 128) {
                $scope.message_borrowings = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_borrowings = ""
            }
        }
        $scope.basicrecord_remarks = function (string) {
            if (string.length >= 128) {
                $scope.message_basicrecordremarks = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_basicrecordremarks = ""
            }
        }
        $scope.deferral_pendency = function (string) {
            if (string.length >= 128) {
                $scope.messagedeferral_pendency = "Allowed Only 128 Characters";
            }
            else {
                $scope.messagedeferral_pendency = ""
            }
        }
        $scope.cbototal_groups = function (string) {
            if (string.length >= 64) {
                $scope.messagecbototal_groups = "Allowed Only 64 Characters";
            }
            else {
                $scope.messagecbototal_groups = ""
            }
        }
        $scope.CBOgroup_funded = function (string) {
            if (string.length >= 64) {
                $scope.messageCBOgroup_funded = "Allowed Only 64 Characters";
            }
            else {
                $scope.messageCBOgroup_funded = ""
            }
        }
        $scope.RMDvisit_groupcount = function (string) {
            if (string.length >= 64) {
                $scope.messageRMDvisit_groupcount = "Allowed Only 64 Characters";
            }
            else {
                $scope.messageRMDvisit_groupcount = ""
            }
        }
        $scope.borrower_commitment = function (string) {
            if (string.length >= 1024) {
                $scope.messageborrower_commitment = "Allowed Only 1024 Characters";
            }
            else {
                $scope.messageborrower_commitment = ""
            }
        }
        $scope.pending_documentation = function (string) {
            if (string.length >= 1024) {
                $scope.messagepending_documentation = "Allowed Only 1024 Characters";
            }
            else {
                $scope.messagepending_documentation = ""
            }
        }

        // Asset Verification
        $scope.purpose_funding = function (string) {
            if (string.length >= 512) {
                $scope.message_funding = "Allowed Only 512 Characters";
            }
            else {
                $scope.message_funding = ""
            }
        }
        $scope.utilisationdtls = function (string) {
            if (string.length >= 512) {
                $scope.message_utilisationdtls = "Allowed Only 512 Characters";
            }
            else {
                $scope.message_utilisationdtls = ""
            }
        }
        $scope.adequacyloan_samunnati = function (string) {
            if (string.length >= 256) {
                $scope.message_samunnati = "Allowed Only 256 Characters";
            }
            else {
                $scope.message_samunnati = ""
            }
        }
        $scope.adequacyloan_impactassessment = function (string) {
            if (string.length >= 256) {
                $scope.message_impactassessment = "Allowed Only 256 Characters";
            }
            else {
                $scope.message_impactassessment = ""
            }
        }
        $scope.additional_funding = function (string) {
            if (string.length >= 1024) {
                $scope.message_additionalfunding = "Allowed Only 1024 Characters";
            }
            else {
                $scope.message_additionalfunding = ""
            }
        }
        
        // Numeric to Word - Indian Standard...//

        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }

        $scope.disbursementchange = function () {
            var input = document.getElementById('disbursement_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_disbursementamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdisbursement_amount = $scope.changedisbursement_amount;

            }
            else {
                $scope.txtdisbursement_amount = output;
                document.getElementById('words_disbursementamount').innerHTML = lswords_disbursementamount;
            }

        }

        $scope.requestedloanamountChange = function () {
            var input = document.getElementById('requestloan_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_requestedloan = inWords(str);

            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtrequestedloan_byclient = $scope.requestedloan_byclient;
            }
            else {
                $scope.txtrequestedloan_byclient = output;
                document.getElementById('words_requestedloan').innerHTML = lswords_requestedloan;
            }
        }

        $scope.totalloanoustandingChange = function () {
            var input = document.getElementById('totalloan_oustanding').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalloan = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txttotalloan_oustanding = $scope.totalloan_oustanding;
            }
            else {
                $scope.txttotalloan_oustanding = output;
                document.getElementById('words_totalloan').innerHTML = lswords_totalloan;
            }
        }

        $scope.turnover_lastfyChange = function () {
            var input = document.getElementById('turnover_lastfy').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_turnoverFY = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtturnover_lastfy = $scope.turnover_lastfy;
            }
            else {
                $scope.txtturnover_lastfy = output;
                document.getElementById('words_turnoverFY').innerHTML = lswords_turnoverFY;
            }
        }

        $scope.present_fysalesChange = function () {
            var input = document.getElementById('present_fysales').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_presentFY = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtpresent_fysales = $scope.present_fysales;
            }
            else {
                $scope.txtpresent_fysales = output;
                document.getElementById('words_presentFY').innerHTML = lswords_presentFY;
            }
        }

        $scope.portfoliototal_loandisbursementChange = function () {
            var input = document.getElementById('portfoliototal_loandisbursement').value;
             var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfoliototal_loandisbursement = $scope.portfoliototal_loandisbursement;
            }
            else {
                $scope.txtportfoliototal_loandisbursement = output;
                document.getElementById('words_totalamount').innerHTML = lswords_totalamount;
            }
        }

        $scope.portfolio_outstandingdateChange = function () {
            var input = document.getElementById('portfolio_outstandingdate').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_outstandingdate = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_outstandingdate = "";
            }
            else {
                $scope.txtportfolio_outstandingdate = output;
                document.getElementById('words_outstandingdate').innerHTML = lswords_outstandingdate;
            }
        }

        $scope.portfolio_overduebeneficaryChange = function () {
            var input = document.getElementById('portfolio_overduebeneficary').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_overduebeneficary = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_overduebeneficary = "";
            }
            else {
                $scope.txtportfolio_overduebeneficary = output;
                document.getElementById('words_overduebeneficary').innerHTML = lswords_overduebeneficary;
            }
        }

        $scope.portfolio_overdueAmountChange = function () {
            var input = document.getElementById('portfolio_overdueAmount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_overdueAmount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_overdueAmount = "";
            }
            else {
                $scope.txtportfolio_overdueAmount = output;
                document.getElementById('words_overdueAmount').innerHTML = lswords_overdueAmount;
            }
        }

        $scope.portfolio_fundingoverdueChange = function () {
            var input = document.getElementById('portfolio_fundingoverdue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_fundingoverdue = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_fundingoverdue = "";
            }
            else {
                $scope.txtportfolio_fundingoverdue = output;
                document.getElementById('words_fundingoverdue').innerHTML = lswords_fundingoverdue;
            }
        }

        $scope.VisitreportComplete = function () {
            var input = $scope.txtdisbursement_amount;

            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid,
                customer_gid:customer_gid,
                customer_name: $scope.clientName,
                visit_date: $scope.visitdate,
                visitDate: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                constitution: $scope.txtconstitution,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
                typeof_riskreview: $scope.cborisk_reviewtype,
                business_sector: $scope.txtbusiness_sector,
                registeredoffice_address: $scope.txtregister_address,
                present_address: $scope.txtactual_address,
                contact_details1: $scope.txtcontact_dtl1,
                contact_details2: $scope.txtcontact_dtl2,
                relationship_Startedfrom: $scope.txtfirstdisb_date,
                clientbusiness_vintage: $scope.txtbusiness_client,
                primarysecondary_valuechain: $scope.txtprimary_chain,
                basicrecords_remarks: $scope.txtbasicrecord_remarks,
                geneticcode_complied: $scope.cbogenetic_code,
                RMD_visitedGid: $scope.assignedRMD_gid,
                RMD_visitedname: $scope.assigned_RMD,
                RM_name: $scope.relationship_managername,
                PPA_name: $scope.txtPPA_name,
                credit_managername: $scope.credit_managername,
                visittype: $scope.cbovisit_done,
                purpose_ofloan: $scope.txtpurposeof_loan,
                requestedamount_byclient: $scope.txtrequestedloan_byclient,
                sanctionedamount_byclient: $scope.txtsantionloan_bycredit,
                disbursement_Date: $scope.txtdisbursement_date,
                disbursement_amount: input,
                totalloan_outstanding: $scope.txttotalloan_oustanding,
                repayment_track: $scope.cborepayment_track,
                repayment_trackremarks: $scope.txtrepayment_borrowings,
                basicrecords_maintain: $scope.cbobasic_records,
                turnover_lastFY: $scope.txtturnover_lastfy,
                presentFY_sales: $scope.txtpresent_fysales,
                deferral_pendency: $scope.txtdeferral_pendency,
                total_noofGroups: $scope.txtcbototal_groups,
                CBOfunded_noofGroups: $scope.txtCBOgroup_funded,
                RMD_visitgroups: $scope.txtRMDvisit_groupcount,
                assetverification_createdoutofloan: $scope.txtassetverification_comment,
                assetverification_securitydtls: $scope.txtsecurity_details,
                assetverification_mortgaged: $scope.txtassetverification_mortagged,
                assetverification_ROCcreation: $scope.txtROCcreation,
                purposeof_funding: $scope.txtpurpose_funding,
                utilisation_details: $scope.txt_utilisationdtls,
                adequacy_loanamount: $scope.txtadequacyloan_samunnati,
                adequacy_impactassessment: $scope.txtadequacyloan_impactassessment,
                adequacy_additionalfunding: $scope.txtadditional_funding,
                portfolio_noofmembers: $scope.txtportfolio_noofmember,
                portfolio_activemembers: $scope.txtportfolio_activemembers,
                total_disbursementamount: $scope.txtportfoliototal_loandisbursement,
                outstanding_ondate: $scope.txtportfolio_outstandingdate,
                overdue_beneficiary: $scope.txtportfolio_overduebeneficary,
                overdue_amount: $scope.txtportfolio_overdueAmount,
                overdueaccount_funding: $scope.txtportfolio_fundingoverdue,
                sanctioned_limit: $scope.txtsanctioned_limit,
                tenure_period: $scope.txttenure_period,
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                briefdtls_client: $scope.txtbriefdtls_client,
                overall_remarks: $scope.txtoverall_remarks,
                PDD_compliance: $scope.txtPDD_compliance,
                briefrpt_financials: $scope.txtbriefrpt_financials,
                briefrpt_process: $scope.txtbriefrpt_process,
                briefrpt_customer: $scope.txtbriefrpt_customer,
                briefrpt_learnings: $scope.txtbriefrpt_learnings,
                briefrpt_valuechain: $scope.txtbriefrpt_valuechain,
                valuechain_mapanalysis: $scope.txtvaluechain_mapanalysis,
                competitorbusiness_segment: $scope.txtcompetitorbusiness_segment,
                report_status: 'Completed'
            }
            var url = "api/VisitReportCancel/PostCancelReportSubmit"
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.rmVisitReport');
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
            unlockUI();
        }

        $scope.VisitreportbasicdtlSave = function (val) {
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,
                visit_date: $scope.visitdate,
                visitDate: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                customer_gid: customer_gid,
                customer_name: $scope.clientName,
                constitution: $scope.txtconstitution,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
                typeof_riskreview: $scope.cborisk_reviewtype,
                business_sector: $scope.txtbusiness_sector,
                registeredoffice_address: $scope.txtregister_address,
                present_address: $scope.txtactual_address,
                contact_details1: $scope.txtcontact_dtl1,
                contact_details2: $scope.txtcontact_dtl2,
                relationship_Startedfrom: $scope.txtfirstdisb_date,
                clientbusiness_vintage: $scope.txtbusiness_client,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.VisitreportvisitdtlSave = function (val) {
         
            var input = $scope.txtdisbursement_amount;
            
            var RMD_visitedname = $('#cboRMD_name :selected').text();
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,
                primarysecondary_valuechain: $scope.txtprimary_chain,
                geneticcode_complied: $scope.cbogenetic_code,
                RMD_visitedGid: $scope.assignedRMD_gid,
                RMD_visitedname: $scope.assigned_RMD,
                RM_name: $scope.relationship_managername,
                PPA_name: $scope.txtPPA_name,
                credit_managername: $scope.credit_managername,
                visittype: $scope.cbovisit_done,
                purpose_ofloan: $scope.txtpurposeof_loan,
                requestedamount_byclient: $scope.txtrequestedloan_byclient,
                sanctionedamount_byclient: $scope.txtsantionloan_bycredit,
                disbursement_Date: $scope.txtdisbursement_date,
                disbursement_amount: input,
                totalloan_outstanding: $scope.txttotalloan_oustanding,
                repayment_track: $scope.cborepayment_track,
                repayment_trackremarks: $scope.txtrepayment_borrowings,
                basicrecords_maintain: $scope.cbobasic_records,
                basicrecords_remarks: $scope.txtbasicrecord_remarks,
                turnover_lastFY: $scope.txtturnover_lastfy,
                presentFY_sales: $scope.txtpresent_fysales,
                deferral_pendency: $scope.txtdeferral_pendency,
                total_noofGroups: $scope.txtcbototal_groups,
                CBOfunded_noofGroups: $scope.txtCBOgroup_funded,
                RMD_visitgroups: $scope.txtRMDvisit_groupcount,    
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.VisitreportassetdtlSave = function (val) {
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,
                assetverification_createdoutofloan: $scope.txtassetverification_comment,
                assetverification_securitydtls: $scope.txtsecurity_details,
                assetverification_mortgaged: $scope.txtassetverification_mortagged,
                assetverification_ROCcreation: $scope.txtROCcreation,
                purposeof_funding: $scope.txtpurpose_funding,
                utilisation_details: $scope.txt_utilisationdtls,
                adequacy_loanamount: $scope.txtadequacyloan_samunnati,
                adequacy_impactassessment: $scope.txtadequacyloan_impactassessment,
                adequacy_additionalfunding: $scope.txtadditional_funding,
                sanctioned_limit: $scope.txtsanctioned_limit,
                tenure_period: $scope.txttenure_period,
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                briefdtls_client: $scope.txtbriefdtls_client,
                overall_remarks: $scope.txtoverall_remarks,
                PDD_compliance: $scope.txtPDD_compliance,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }
        

        $scope.VisitreportportfoliodtlSave = function (val) {
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,              
                portfolio_noofmembers: $scope.txtportfolio_noofmember,
                portfolio_activemembers: $scope.txtportfolio_activemembers,
                total_disbursementamount: $scope.txtportfoliototal_loandisbursement,
                outstanding_ondate: $scope.txtportfolio_outstandingdate,
                overdue_beneficiary: $scope.txtportfolio_overduebeneficary,
                overdue_amount: $scope.txtportfolio_overdueAmount,
                overdueaccount_funding: $scope.txtportfolio_fundingoverdue,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        
        $scope.VisitreportbriefdtlSave = function (val) {
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,              
                briefrpt_financials: $scope.txtbriefrpt_financials,
                briefrpt_process: $scope.txtbriefrpt_process,
                briefrpt_customer: $scope.txtbriefrpt_customer,
                briefrpt_learnings: $scope.txtbriefrpt_learnings,
                briefrpt_valuechain: $scope.txtbriefrpt_valuechain,
                valuechain_mapanalysis: $scope.txtvaluechain_mapanalysis,
                competitorbusiness_segment: $scope.txtcompetitorbusiness_segment,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.VisitreportSave = function () {
            var input = $scope.txtdisbursement_amount;
            //var arr = input.split(',');
            //var i;
            //for (i = 0; i < arr.length; i++) {
            //    var str = input.replace(',', '');
            //    input = str;
            //}
            var RMD_visitedname = $('#cboRMD_name :selected').text();
           
            var params = {
                allocationdtl_gid:allocationdtl_gid,
                visit_date: $scope.visitdate,
                visitDate: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                customer_gid: customer_gid,
                customer_name: $scope.clientName,
                constitution: $scope.txtconstitution,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
                typeof_riskreview: $scope.cborisk_reviewtype,
                business_sector: $scope.txtbusiness_sector,
                registeredoffice_address: $scope.txtregister_address,
                present_address: $scope.txtactual_address,
                contact_details1: $scope.txtcontact_dtl1,
                contact_details2: $scope.txtcontact_dtl2,
                relationship_Startedfrom: $scope.txtfirstdisb_date,
                clientbusiness_vintage: $scope.txtbusiness_client,
                primarysecondary_valuechain: $scope.txtprimary_chain,
                geneticcode_complied: $scope.cbogenetic_code,
                RMD_visitedGid: $scope.assignedRMD_gid,
                RMD_visitedname: $scope.assigned_RMD,
                RM_name: $scope.relationship_managername,
                PPA_name: $scope.txtPPA_name,
                credit_managername: $scope.credit_managername,
                visittype: $scope.cbovisit_done,
                purpose_ofloan: $scope.txtpurposeof_loan,
                requestedamount_byclient: $scope.txtrequestedloan_byclient,
                sanctionedamount_byclient: $scope.txtsantionloan_bycredit,
                disbursement_Date: $scope.txtdisbursement_date,
                disbursement_amount: input,
                totalloan_outstanding: $scope.txttotalloan_oustanding,
                repayment_track: $scope.cborepayment_track,
                repayment_trackremarks: $scope.txtrepayment_borrowings,
                basicrecords_maintain: $scope.cbobasic_records,
                basicrecords_remarks: $scope.txtbasicrecord_remarks,
                turnover_lastFY: $scope.txtturnover_lastfy,
                presentFY_sales: $scope.txtpresent_fysales,
                deferral_pendency: $scope.txtdeferral_pendency,
                total_noofGroups: $scope.txtcbototal_groups,
                CBOfunded_noofGroups: $scope.txtCBOgroup_funded,
                RMD_visitgroups: $scope.txtRMDvisit_groupcount,
                assetverification_createdoutofloan: $scope.txtassetverification_comment,
                assetverification_securitydtls: $scope.txtsecurity_details,
                assetverification_mortgaged: $scope.txtassetverification_mortagged,
                assetverification_ROCcreation: $scope.txtROCcreation,
                purposeof_funding: $scope.txtpurpose_funding,
                utilisation_details: $scope.txt_utilisationdtls,
                adequacy_loanamount: $scope.txtadequacyloan_samunnati,
                adequacy_impactassessment: $scope.txtadequacyloan_impactassessment,
                adequacy_additionalfunding: $scope.txtadditional_funding,
                portfolio_noofmembers: $scope.txtportfolio_noofmember,
                portfolio_activemembers: $scope.txtportfolio_activemembers,
                total_disbursementamount: $scope.txtportfoliototal_loandisbursement,
                outstanding_ondate: $scope.txtportfolio_outstandingdate,
                overdue_beneficiary: $scope.txtportfolio_overduebeneficary,
                overdue_amount: $scope.txtportfolio_overdueAmount,
                overdueaccount_funding: $scope.txtportfolio_fundingoverdue,
                sanctioned_limit: $scope.txtsanctioned_limit,
                tenure_period: $scope.txttenure_period,
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                briefdtls_client: $scope.txtbriefdtls_client,
                overall_remarks: $scope.txtoverall_remarks,
                PDD_compliance: $scope.txtPDD_compliance,
                briefrpt_financials: $scope.txtbriefrpt_financials,
                briefrpt_process: $scope.txtbriefrpt_process,
                briefrpt_customer: $scope.txtbriefrpt_customer,
                briefrpt_learnings: $scope.txtbriefrpt_learnings,
                briefrpt_valuechain: $scope.txtbriefrpt_valuechain,
                valuechain_mapanalysis: $scope.txtvaluechain_mapanalysis,
                competitorbusiness_segment: $scope.txtcompetitorbusiness_segment,
                report_status: 'Save'
            }
        
            lockUI();
            var url = "api/visitReport/postVisitReport"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.uploadphoto = function (val, val1, name) {
            if (($scope.txtuploadphoto_title == null) || ($scope.txtuploadphoto_title == '') || ($scope.txtuploadphoto_title == undefined)) {
                $("#addPhotoupload").val('');
                Notify.alert('Kindly Enter the Photo Title', 'warning');
            }
            else {
                var frm = new FormData();

                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "photoformatonly");

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
                // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "photoformatonly");

                // if (IsValidExtension == false) {
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('txtuploadphoto_title', $scope.txtuploadphoto_title);
                frm.append('allocationdtl_gid', allocationdtl_gid);
                frm.append('project_flag', "photoformatonly");
                $scope.uploadfrm = frm;
            }
        }
        $scope.VisitReportPhotoUpload = function () {
            if ($scope.uploadfrm != undefined)
            {
                lockUI();
                var url = 'api/visitReport/visitReportPhotoUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#addPhotoupload").val('');
                    $scope.txtuploadphoto_title = "";
                    var params = {
                        allocationdtl_gid: allocationdtl_gid
                    }
                    var url = "api/visitReport/getvisitReportPhoto";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.visitreportphoto = resp.data.visitreportphoto;
                    });

                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
        }

        $scope.uploaddocumentcancel = function (visitreport_documentGid) {
            lockUI();
            var params = {
                visitreport_documentGid: visitreport_documentGid
            }
            var url = 'api/visitReport/visitReportUploadcancel';
            SocketService.getparams(url, params).then(function (resp) {
                var params = {
                    allocationdtl_gid:allocationdtl_gid
                }

                var url = "api/visitReport/getvisitReportDocument";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.visitreportdocument = resp.data.visitreportdocument;
                });
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
        }

        $scope.uploadvisitreport = function (val, val1, name) {
            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#addExternalupload").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
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
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('allocationdtl_gid', allocationdtl_gid);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
            }
        }

        $scope.VisitReportDocumentUpload = function () {

            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/visitReport/visitReportUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    var params = {
                        allocationdtl_gid: allocationdtl_gid
                    }

                    var url = "api/visitReport/getvisitReportDocument";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.visitreportdocument = resp.data.visitreportdocument;
                    });

                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = "";
                    $scope.txtdocument_type = "";
                    $scope.uploadfrm = undefined;

                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
        }


        $scope.uploadphotocancel = function (visitreport_photoGid) {
            lockUI();
            var params = {
                visitreport_photoGid: visitreport_photoGid
            }
            var url = 'api/visitReport/visitReportPhotocancel';
            SocketService.getparams(url, params).then(function (resp) {
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/visitReport/getvisitReportPhoto";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.visitreportphoto = resp.data.visitreportphoto;
                });
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('VisitReportDetailViewcontroller', VisitReportDetailViewcontroller);

    VisitReportDetailViewcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService'];

    function VisitReportDetailViewcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'VisitReportDetailViewcontroller';
        var allocationdtl_gid=$location.search().allocationdtl_gid;
     
        activate();

        function activate() {
            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }

            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.clientName = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;

            });

            var url = "api/visitReport/getvisitreportdtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreport_generateGid = resp.data.visitreport_generateGid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.txtbusiness_vintage = resp.data.business_vintage,
                $scope.cbotypeof_loan = resp.data.typeof_loanvertical,
                $scope.cboriskcode = resp.data.risk_code,
                //$scope.riskcode_classification = resp.data.riskcode_classification,
                $scope.cborisk_reviewtype = resp.data.typeof_riskreview;
                $scope.txtbusiness_sector = resp.data.business_sector,
                $scope.txtregister_address = resp.data.registeredoffice_address,
                $scope.txtactual_address = resp.data.present_address,
                $scope.txtcontact_dtl1 = resp.data.contact_details1,
                $scope.txtcontact_dtl2 = resp.data.contact_details2,
                 $scope.txtlattitude = resp.data.visit_latitude;
                $scope.txtlongitude = resp.data.visit_longitude;
                //$scope.firstdisb_date = resp.data.relationship_startedfrom
                $scope.txtbusiness_client = resp.data.clientbusiness_vintage,
                $scope.txtprimary_chain = resp.data.primarysecondary_valuechain,
                $scope.cbogenetic_code = resp.data.geneticcode_complied,
                $scope.cboRMD_gid = resp.data.RMD_visitedGid,
                $scope.RMD_visitedname = resp.data.RMD_visitedname;
                $scope.txtPPA_name = resp.data.PPA_name;
                $scope.cbovisit_done = resp.data.visit_done,
                $scope.txtpurposeof_loan = resp.data.purpose_ofloan,
                $scope.txtrequestedloan_byclient = resp.data.requestedamount_byclient,
                $scope.txtsantionloan_bycredit = resp.data.sanctionedamount_byclient;
                $scope.txtbasicrecords_remarks = resp.data.basicrecords_remarks;
                $scope.txtdisbursement_amount = resp.data.disbursement_amount,
                $scope.txttotalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.cborepayment_track = resp.data.repayment_track,
                $scope.cbobasic_records = resp.data.basicrecords_maintain,
                $scope.txtturnover_lastfy = resp.data.turnover_lastFY,
                $scope.txtpresent_fysales = resp.data.presentFY_sales,
                $scope.txtdeferral_pendency = resp.data.deferral_pendency,
                $scope.txtadditional_funding = resp.data.adequacy_additionalfunding,
                $scope.txtcbototal_groups = resp.data.total_noofGroups,
                $scope.txtCBOgroup_funded = resp.data.CBOfunded_noofGroups,
                $scope.txtRMDvisit_groupcount = resp.data.RMD_visitgroups,
                $scope.txtassetverification_comment = resp.data.assetverification_createdoutofloan,
                $scope.txtsecurity_details = resp.data.assetverification_securitydtls,
                $scope.txtassetverification_mortagged = resp.data.assetverification_mortgaged,
                $scope.txtROCcreation = resp.data.assetverification_ROCcreation,
                $scope.txtpurpose_funding = resp.data.purposeof_funding,
                $scope.txt_utilisationdtls = resp.data.utilisation_details,
                $scope.txtadequacyloan_samunnati = resp.data.adequacy_loanamount,
                $scope.txtadequacyloan_impactassessment = resp.data.adequacy_impactassessment,
                $scope.txtportfolio_noofmember = resp.data.portfolio_noofmembers,
                $scope.txtportfolio_activemembers = resp.data.portfolio_activemembers,
                $scope.txtportfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.txtportfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.txtportfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.txtportfolio_overdueAmount = resp.data.overdue_amount,
                $scope.txtportfolio_fundingoverdue = resp.data.overdueaccount_funding,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtrepayment_trackremarks = resp.data.repayment_trackremarks,
                //$scope.txtloan_clientdate = resp.data.loan_clientdate,
                $scope.txtoverdue = resp.data.overdue,
                $scope.txtborrower_commitment = resp.data.borrower_commitment,
                $scope.txtpending_documentation = resp.data.pending_documentation,
                //$scope.txtasset_verification = resp.data.asset_verification,
                $scope.txtbriefdtls_client = resp.data.briefdtls_client,
                $scope.txtenduse_loan = resp.data.enduse_loan,
                //$scope.txtadequacy_loan = resp.data.adequacy_loan,
                $scope.txtoverall_remarks = resp.data.overall_remarks,
                $scope.txtPDD_compliance = resp.data.PDD_compliance,
                $scope.txtbriefrpt_financials = resp.data.briefrpt_financials,
                $scope.txtbriefrpt_process = resp.data.briefrpt_process,
                $scope.txtbriefrpt_customer = resp.data.briefrpt_customer,
                $scope.txtbriefrpt_learnings = resp.data.briefrpt_learnings,
                $scope.txtbriefrpt_valuechain = resp.data.briefrpt_valuechain,
                $scope.txtvaluechain_mapanalysis = resp.data.valuechain_mapanalysis,
                $scope.txtcompetitorbusiness_segment = resp.data.competitorbusiness_segment;
                $scope.editvisittype = resp.data.editvisittype;
                if (resp.data.constitution != null) {
                    $scope.constitution = resp.data.constitution
                }
                if (resp.data.RM_name != null) {
                    $scope.relationship_managername = resp.data.RM_name
                }
                if (resp.data.credit_managername != null) {
                    $scope.credit_managername = resp.data.credit_managername;
                }
                if (resp.data.visit_date != null) {
                    var p = resp.data.visit_date.split(/\D/g)
                    $scope.visitdate = [p[2], p[1], p[0]].join("-");
                }
               
                if (resp.data.dealing_withsince != null) {
                    var p = resp.data.dealing_withsince.split(/\D/g)
                    $scope.txtincorporated_date = [p[2], p[1], p[0]].join("-");
                }
                if (resp.data.disbursement_date != null) {
                    var p = resp.data.disbursement_date.split(/\D/g)
                    $scope.txtdisbursement_date = [p[2], p[1], p[0]].join("-");
                }
               
            });

            var url = "api/visitReport/getvisitReportDocument";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreportdocument = resp.data.visitreportdocument;
            });

            var url = "api/visitReport/GetSanctionTenurePeriod";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiondetails = resp.data.loandtl;
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

            var url = "api/visitReport/getvisitReportPhoto";
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreportphoto = resp.data.visitreportphoto;
            });

            var url = "api/VisitReportCancel/GetVisitCancelLog";
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visistreportcancel = resp.data.visistreportcancel;
            });
            unlockUI();
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }


        $scope.cancel = function () {
            $state.go('app.rmVisitReport');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('visitReportGeneratecontroller', visitReportGeneratecontroller);

    visitReportGeneratecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService','cmnfunctionService'];

    function visitReportGeneratecontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'visitReportGeneratecontroller';
      
        var allocationdtl_gid = $location.search().allocationdtl_gid;
        var customer_gid = $location.search().allocation_customer_gid;

        activate();
      

        function activate() {

          

            lockUI();

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open4 = true;
            };
            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open5 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var Params = {
                allocationdtl_gid: allocationdtl_gid
            }
            
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, Params).then(function (resp) {

                $scope.clientName = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.txtconstitution = resp.data.constitution;
                $scope.relationship_managername = resp.data.relationship_managername;
                $scope.credit_managername = resp.data.credit_managername;
                $scope.creditmanager_gid = resp.data.creditmanager_gid;
                $scope.relationship_managerGid = resp.data.relationship_managerGid;
                $scope.assigned_RMD = resp.data.assigned_RM;
                $scope.assignedRMD_gid = resp.data.assignedRM_gid;
                $scope.totaldisb_amount = resp.data.totaldisb_amount;
                $scope.txtPPA_name = resp.data.PPA_name;

                if ($scope.totaldisb_amount != "") {
                    $scope.txtdisbursement_amount = resp.data.totaldisb_amount
                }
                if (resp.data.last_disb_date != "") {
                    $scope.txtdisbursement_date = resp.data.last_disb_date;

                }
                if (resp.data.firstdisb_date != "") {
                    $scope.txtfirstdisb_date = resp.data.firstdisb_date;

                }

            });


            var url = "api/visitReport/getvisitreportdtl";
            SocketService.getparams(url, Params).then(function (resp) {
                $scope.visitreport_generateGid = resp.data.visitreport_generateGid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.visitdate = resp.data.visit_date;
                $scope.txtlattitude = resp.data.visit_latitude;
                $scope.txtlongitude = resp.data.visit_longitude;
                $scope.cborisk_reviewtype = resp.data.typeof_riskreview;
                $scope.cboriskcode = resp.data.risk_code;
                //$scope.cboriskcode_classification = resp.data.riskcode_classification;
                if (resp.data.constitution != null) {
                    $scope.txtconstitution = resp.data.constitution
                }
                if (resp.data.dealing_withsince != "0001-01-01T00:00:00") {
                    $scope.txtfirstdisb_date = resp.data.dealing_withsince
                }
                $scope.txtbusiness_vintage = resp.data.business_vintage,
                $scope.cbotypeof_loan = resp.data.typeof_loanvertical,
                $scope.txtbusiness_sector = resp.data.business_sector,
                $scope.txtregister_address = resp.data.registeredoffice_address,
                $scope.txtactual_address = resp.data.present_address,
                $scope.txtcontact_dtl1 = resp.data.contact_details1,
                $scope.txtcontact_dtl2 = resp.data.contact_details2,
                //$scope.firstdisb_date = resp.data.relationship_startedfrom
                $scope.txtbusiness_client = resp.data.clientbusiness_vintage,
                $scope.txtprimary_chain = resp.data.primarysecondary_valuechain,
                $scope.cbogenetic_code = resp.data.geneticcode_complied,
                $scope.cboRMD_gid = resp.data.RMD_visitedGid,
                $scope.RMD_visitedname = resp.data.RMD_visitedname;
                if (resp.data.RM_name != null) {
                    $scope.relationship_managername = resp.data.RM_name
                }
                if ($scope.txtPPA_name == "") {
                    $scope.txtPPA_name = resp.data.PPA_name;
                }

                if (resp.data.credit_managername != null) {
                    $scope.credit_managername = resp.data.credit_managername;
                }
                $scope.cbovisit_done = resp.data.visit_done,
                $scope.txtpurposeof_loan = resp.data.purpose_ofloan,
                $scope.txtrequestedloan_byclient = resp.data.requestedamount_byclient;
                if (resp.data.disbursement_date != "0001-01-01T00:00:00") {
                    $scope.txtdisbursement_date = resp.data.disbursement_date
                }
                if ($scope.txtdisbursement_date == "") {
                    $scope.txtdisbursement_amount = resp.data.disbursement_amount;
                }
                $scope.txttotalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.cborepayment_track = resp.data.repayment_track,
                $scope.cbobasic_records = resp.data.basicrecords_maintain,
                $scope.txtbasicrecord_remarks = resp.data.basicrecords_remarks,
                $scope.txtturnover_lastfy = resp.data.turnover_lastFY,
                $scope.txtpresent_fysales = resp.data.presentFY_sales,
                $scope.txtdeferral_pendency = resp.data.deferral_pendency,
                $scope.txtcbototal_groups = resp.data.total_noofGroups,
                $scope.txtCBOgroup_funded = resp.data.CBOfunded_noofGroups,
                $scope.txtRMDvisit_groupcount = resp.data.RMD_visitgroups,
                $scope.txtassetverification_comment = resp.data.assetverification_createdoutofloan,
                $scope.txtsecurity_details = resp.data.assetverification_securitydtls,
                $scope.txtassetverification_mortagged = resp.data.assetverification_mortgaged,
                $scope.txtROCcreation = resp.data.assetverification_ROCcreation,
                $scope.txtpurpose_funding = resp.data.purposeof_funding,
                $scope.txt_utilisationdtls = resp.data.utilisation_details,
                $scope.txtadequacyloan_samunnati = resp.data.adequacy_loanamount,
                $scope.txtadequacyloan_impactassessment = resp.data.adequacy_impactassessment,
                $scope.txtadditional_funding = resp.data.adequacy_additionalfunding,
                $scope.txtportfolio_noofmember = resp.data.portfolio_noofmembers,
                $scope.txtportfolio_activemembers = resp.data.portfolio_activemembers,
                $scope.txtportfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.txtportfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.txtportfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.txtportfolio_overdueAmount = resp.data.overdue_amount,
                $scope.txtportfolio_fundingoverdue = resp.data.overdueaccount_funding,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                //$scope.txtloan_clientdate = resp.data.loan_clientdate,
                $scope.txtoverdue = resp.data.overdue,
                $scope.txtborrower_commitment = resp.data.borrower_commitment,
                $scope.txtpending_documentation = resp.data.pending_documentation,
                //$scope.txtasset_verification = resp.data.asset_verification,
                $scope.txtbriefdtls_client = resp.data.briefdtls_client,
                $scope.txtenduse_loan = resp.data.enduse_loan,
                //$scope.txtadequacy_loan = resp.data.adequacy_loan,
                $scope.txtoverall_remarks = resp.data.overall_remarks,
                $scope.txtPDD_compliance = resp.data.PDD_compliance,
                $scope.txtbriefrpt_financials = resp.data.briefrpt_financials,
                $scope.txtbriefrpt_process = resp.data.briefrpt_process,
                $scope.txtbriefrpt_customer = resp.data.briefrpt_customer,
                $scope.txtbriefrpt_learnings = resp.data.briefrpt_learnings,
                $scope.txtbriefrpt_valuechain = resp.data.briefrpt_valuechain,
                $scope.txtvaluechain_mapanalysis = resp.data.valuechain_mapanalysis,
                $scope.txtcompetitorbusiness_segment = resp.data.competitorbusiness_segment;
                $scope.visittypedtl = resp.data.visittype;
                $scope.txtrepayment_borrowings = resp.data.repayment_trackremarks,
                $scope.editvisittype = resp.data.editvisittype;
                $scope.cbovisit_done = [];
                if (resp.data.editvisittype != null) {
                    var count = resp.data.editvisittype.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.visittypedtl.map(function (x) { return x.vistdone_gid; }).indexOf(resp.data.editvisittype[i].vistdone_gid); 
                        $scope.cbovisit_done.push($scope.visittypedtl[indexs]);
                    }
                }
                
                if (resp.data.disbursement_amount != null) {
                    var str = resp.data.disbursement_amount.replace(/,/g, '');
                    var str = str.split('.')[0];
                    $scope.txtdisbursement_amount = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_disbursementamount').innerHTML = inWords(str);
                }
                else
                {

                    var str = $scope.txtdisbursement_amount.replace(/,/g, '');
                    var str = str.split('.')[0];
                    $scope.txtdisbursement_amount = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_disbursementamount').innerHTML = inWords(str);
                }
            });

            var customer_gid = {
                customer_gid: customer_gid
            }
            var url = "api/visitReport/GetSanctionTenurePeriod";
            SocketService.getparams(url, Params).then(function (resp) {
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.txtsantionloan_bycredit = resp.data.totalsanction_amount;
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

            var url = "api/visitReport/getvisitReportDocument";
            SocketService.getparams(url, Params).then(function (resp) {
                $scope.visitreportdocument = resp.data.visitreportdocument;
            });
            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = "api/visitReport/getvisitReportPhoto";
            SocketService.getparams(url, Params).then(function (resp) {
                $scope.visitreportphoto = resp.data.visitreportphoto;
                unlockUI();
            });


        }
        // Basic Details
        $scope.business_vintage = function (string) {
            if (string.length >= 64) {
                $scope.messagebusiness_vintage = "Allowed Only 64 Characters";
            }
            else {
                $scope.messagebusiness_vintage = ""
            }
        }
        $scope.business_sector = function (string) {
            if (string.length >= 128) {
                $scope.messagebusiness_sector = "Allowed Only 128 Characters";
            }
            else {
                $scope.messagebusiness_sector = ""
            }
        }
        $scope.register_address = function (string) {
            if (string.length >= 256) {
                $scope.messageregister_address = "Allowed Only 256 Characters";
            }
            else {
                $scope.messageregister_address = ""
            }
        }
        $scope.actual_address = function (string) {
            if (string.length >= 256) {
                $scope.messageactual_address = "Allowed Only 256 Characters";
            }
            else {
                $scope.messageactual_address = ""
            }
        }
        $scope.contact_dtl1 = function (string) {
            if (string.length >= 256) {
                $scope.messagecontact_dtl1 = "Allowed Only 256 Characters";
            }
            else {
                $scope.messagecontact_dtl1 = ""
            }
        }
        $scope.contact_dtl2 = function (string) {
            if (string.length >= 256) {
                $scope.messagecontact_dtl2 = "Allowed Only 256 Characters";
            }
            else {
                $scope.messagecontact_dtl2 = ""
            }
        }
        $scope.lattitude = function (string) {
            if (string.length >= 32) {
                $scope.message_lattitude = "Allowed Only 32 Characters";
            }
            else {
                $scope.message_lattitude = ""
            }
        }
        $scope.longitude = function (string) {
            if (string.length >= 32) {
                $scope.message_longitude = "Allowed Only 32 Characters";
            }
            else {
                $scope.message_longitude = ""
            }
        }

        // Visit Details
        $scope.primarychain = function (string) {
            if (string.length >= 128) {
                $scope.message = "Allowed Only 128 Characters";
            }
            else {
                $scope.message = ""
            }
        }
        $scope.purposeof_loan = function (string) {
            if (string.length >= 128) {
                $scope.message_loan = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_loan = ""
            }
        }
        $scope.overdue = function (string) {
            if (string.length >= 128) {
                $scope.message_overdue = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_overdue = ""
            }
        }
        $scope.repayment_borrowings = function (string) {
            if (string.length >= 128) {
                $scope.message_borrowings = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_borrowings = ""
            }
        }
        $scope.basicrecord_remarks = function (string) {
            if (string.length >= 128) {
                $scope.message_basicrecordremarks = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_basicrecordremarks = ""
            }
        }
        $scope.deferral_pendency = function (string) {
            if (string.length >= 128) {
                $scope.messagedeferral_pendency = "Allowed Only 128 Characters";
            }
            else {
                $scope.messagedeferral_pendency = ""
            }
        }
        $scope.cbototal_groups = function (string) {
            if (string.length >= 64) {
                $scope.messagecbototal_groups = "Allowed Only 64 Characters";
            }
            else {
                $scope.messagecbototal_groups = ""
            }
        }
        $scope.CBOgroup_funded = function (string) {
            if (string.length >= 64) {
                $scope.messageCBOgroup_funded = "Allowed Only 64 Characters";
            }
            else {
                $scope.messageCBOgroup_funded = ""
            }
        }
        $scope.RMDvisit_groupcount = function (string) {
            if (string.length >= 64) {
                $scope.messageRMDvisit_groupcount = "Allowed Only 64 Characters";
            }
            else {
                $scope.messageRMDvisit_groupcount = ""
            }
        }
        $scope.borrower_commitment = function (string) {
            if (string.length >= 1024) {
                $scope.messageborrower_commitment = "Allowed Only 1024 Characters";
            }
            else {
                $scope.messageborrower_commitment = ""
            }
        }
        $scope.pending_documentation = function (string) {
            if (string.length >= 1024) {
                $scope.messagepending_documentation = "Allowed Only 1024 Characters";
            }
            else {
                $scope.messagepending_documentation = ""
            }
        }

        // Asset Verification
        $scope.purpose_funding = function (string) {
            if (string.length >= 512) {
                $scope.message_funding = "Allowed Only 512 Characters";
            }
            else {
                $scope.message_funding = ""
            }
        }
        $scope.utilisationdtls = function (string) {
            if (string.length >= 512) {
                $scope.message_utilisationdtls = "Allowed Only 512 Characters";
            }
            else {
                $scope.message_utilisationdtls = ""
            }
        }
        $scope.adequacyloan_samunnati = function (string) {
            if (string.length >= 256) {
                $scope.message_samunnati = "Allowed Only 256 Characters";
            }
            else {
                $scope.message_samunnati = ""
            }
        }
        $scope.adequacyloan_impactassessment = function (string) {
            if (string.length >= 256) {
                $scope.message_impactassessment = "Allowed Only 256 Characters";
            }
            else {
                $scope.message_impactassessment = ""
            }
        }
        $scope.additional_funding = function (string) {
            if (string.length >= 1024) {
                $scope.message_additionalfunding = "Allowed Only 1024 Characters";
            }
            else {
                $scope.message_additionalfunding = ""
            }
        }

        // Numeric to Word - Indian Standard...//

        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }

        $scope.disbursementchange = function () {
            var input = document.getElementById('disbursement_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_disbursementamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdisbursement_amount = $scope.totaldisb_amount;

            }
            else {
                $scope.txtdisbursement_amount = output;
                document.getElementById('words_disbursementamount').innerHTML = lswords_disbursementamount;
            }
        }

        $scope.requestedloanamountChange = function () {
            var input = document.getElementById('requestloan_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_requestedloan = inWords(str);

            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtrequestedloan_byclient = "";
            }

            else {
                document.getElementById('words_requestedloan').innerHTML = lswords_requestedloan;
                $scope.txtrequestedloan_byclient = output;
            }

        }

        $scope.totalloanoustandingChange = function () {
            var input = document.getElementById('totalloan_oustanding').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalloan = inWords(str);

            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txttotalloan_oustanding = "";
            }
            else {
                $scope.txttotalloan_oustanding = output;
                document.getElementById('words_totalloan').innerHTML = lswords_totalloan;
            }
        }

        $scope.turnover_lastfyChange = function () {
            var input = document.getElementById('turnover_lastfy').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_turnoverFY = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtturnover_lastfy = "";
            }
            else {
                $scope.txtturnover_lastfy = output;
                document.getElementById('words_turnoverFY').innerHTML = lswords_turnoverFY;
            }
        }

        $scope.present_fysalesChange = function () {
            var input = document.getElementById('present_fysales').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_presentFY = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtpresent_fysales = "";
            }
            else {
                $scope.txtpresent_fysales = output;
                document.getElementById('words_presentFY').innerHTML = lswords_presentFY;
            }
        }
        $scope.portfoliototal_loandisbursementChange = function () {
            var input = document.getElementById('portfoliototal_loandisbursement').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfoliototal_loandisbursement = "";
            }
            else {
                $scope.txtportfoliototal_loandisbursement = output;
                document.getElementById('words_totalamount').innerHTML = lswords_totalamount;
            }
        }

        $scope.portfolio_outstandingdateChange = function () {
            var input = document.getElementById('portfolio_outstandingdate').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_outstandingdate = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_outstandingdate = "";
            }
            else {
                $scope.txtportfolio_outstandingdate = output;
                document.getElementById('words_outstandingdate').innerHTML = lswords_outstandingdate;
            }
        }

        $scope.portfolio_overduebeneficaryChange = function () {
            var input = document.getElementById('portfolio_overduebeneficary').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_overduebeneficary = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_overduebeneficary = "";
            }
            else {
                $scope.txtportfolio_overduebeneficary = output;
                document.getElementById('words_overduebeneficary').innerHTML = lswords_overduebeneficary;
            }
        }

        $scope.portfolio_overdueAmountChange = function () {
            var input = document.getElementById('portfolio_overdueAmount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_overdueAmount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_overdueAmount = "";
            }
            else {
                $scope.txtportfolio_overdueAmount = output;
                document.getElementById('words_overdueAmount').innerHTML = lswords_overdueAmount;
            }
        }

        $scope.portfolio_fundingoverdueChange = function () {
            var input = document.getElementById('portfolio_fundingoverdue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_fundingoverdue = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_fundingoverdue = "";
            }
            else {
                $scope.txtportfolio_fundingoverdue = output;
                document.getElementById('words_fundingoverdue').innerHTML = lswords_fundingoverdue;
            }
        }

        
        $scope.VisitreportbasicdtlSave = function (val) {
               
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,
                visit_date: $scope.visitdate,
                visitDate: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                //riskcode_classification: $scope.cboriskcode_classification,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                customer_gid: customer_gid,
                customer_name: $scope.clientName,
                constitution: $scope.txtconstitution,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
                typeof_riskreview: $scope.cborisk_reviewtype,
                business_sector: $scope.txtbusiness_sector,
                registeredoffice_address: $scope.txtregister_address,
                present_address: $scope.txtactual_address,
                contact_details1: $scope.txtcontact_dtl1,
                contact_details2: $scope.txtcontact_dtl2,
                relationship_Startedfrom: $scope.txtfirstdisb_date,
                clientbusiness_vintage: $scope.txtbusiness_client,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.VisitreportvisitdtlSave = function (val) {
         
            var input = $scope.txtdisbursement_amount;
            
            var RMD_visitedname = $('#cboRMD_name :selected').text();
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,
                primarysecondary_valuechain: $scope.txtprimary_chain,
                geneticcode_complied: $scope.cbogenetic_code,
                RMD_visitedGid: $scope.assignedRMD_gid,
                RMD_visitedname: $scope.assigned_RMD,
                RM_name: $scope.relationship_managername,
                PPA_name: $scope.txtPPA_name,
                credit_managername: $scope.credit_managername,
                visittype: $scope.cbovisit_done,
                purpose_ofloan: $scope.txtpurposeof_loan,
                requestedamount_byclient: $scope.txtrequestedloan_byclient,
                sanctionedamount_byclient: $scope.txtsantionloan_bycredit,
                disbursement_Date: $scope.txtdisbursement_date,
                disbursement_amount: input,
                totalloan_outstanding: $scope.txttotalloan_oustanding,
                repayment_track: $scope.cborepayment_track,
                repayment_trackremarks: $scope.txtrepayment_borrowings,
                basicrecords_maintain: $scope.cbobasic_records,
                basicrecords_remarks: $scope.txtbasicrecord_remarks,
                turnover_lastFY: $scope.txtturnover_lastfy,
                presentFY_sales: $scope.txtpresent_fysales,
                deferral_pendency: $scope.txtdeferral_pendency,
                total_noofGroups: $scope.txtcbototal_groups,
                CBOfunded_noofGroups: $scope.txtCBOgroup_funded,
                RMD_visitgroups: $scope.txtRMDvisit_groupcount,    
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.VisitreportassetdtlSave = function (val) {
            
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,
                assetverification_createdoutofloan: $scope.txtassetverification_comment,
                assetverification_securitydtls: $scope.txtsecurity_details,
                assetverification_mortgaged: $scope.txtassetverification_mortagged,
                assetverification_ROCcreation: $scope.txtROCcreation,
                purposeof_funding: $scope.txtpurpose_funding,
                utilisation_details: $scope.txt_utilisationdtls,
                adequacy_loanamount: $scope.txtadequacyloan_samunnati,
                adequacy_impactassessment: $scope.txtadequacyloan_impactassessment,
                adequacy_additionalfunding: $scope.txtadditional_funding,
                sanctioned_limit: $scope.txtsanctioned_limit,
                tenure_period: $scope.txttenure_period,
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                briefdtls_client: $scope.txtbriefdtls_client,
                overall_remarks: $scope.txtoverall_remarks,
                PDD_compliance: $scope.txtPDD_compliance,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }
        

        $scope.VisitreportportfoliodtlSave = function (val) {
            
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,              
                portfolio_noofmembers: $scope.txtportfolio_noofmember,
                portfolio_activemembers: $scope.txtportfolio_activemembers,
                total_disbursementamount: $scope.txtportfoliototal_loandisbursement,
                outstanding_ondate: $scope.txtportfolio_outstandingdate,
                overdue_beneficiary: $scope.txtportfolio_overduebeneficary,
                overdue_amount: $scope.txtportfolio_overdueAmount,
                overdueaccount_funding: $scope.txtportfolio_fundingoverdue,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        
        $scope.VisitreportbriefdtlSave = function (val) {
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,              
                briefrpt_financials: $scope.txtbriefrpt_financials,
                briefrpt_process: $scope.txtbriefrpt_process,
                briefrpt_customer: $scope.txtbriefrpt_customer,
                briefrpt_learnings: $scope.txtbriefrpt_learnings,
                briefrpt_valuechain: $scope.txtbriefrpt_valuechain,
                valuechain_mapanalysis: $scope.txtvaluechain_mapanalysis,
                competitorbusiness_segment: $scope.txtcompetitorbusiness_segment,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

      

        $scope.VisitreportSave = function (val) {
           
            var input = $scope.txtdisbursement_amount;
            //var arr = input.split(',');
            //var i;
            //for (i = 0; i < arr.length; i++) {
            //    var str = input.replace(',', '');
            //    input = str;
            //}
            var RMD_visitedname = $('#cboRMD_name :selected').text();
           
            var params = {
                allocationdtl_gid:allocationdtl_gid,
                visit_date: $scope.visitdate,
                visitDate: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                //riskcode_classification: $scope.cboriskcode_classification,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                customer_gid: customer_gid,
                customer_name: $scope.clientName,
                constitution: $scope.txtconstitution,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
                typeof_riskreview: $scope.cborisk_reviewtype,
                business_sector: $scope.txtbusiness_sector,
                registeredoffice_address: $scope.txtregister_address,
                present_address: $scope.txtactual_address,
                contact_details1: $scope.txtcontact_dtl1,
                contact_details2: $scope.txtcontact_dtl2,
                relationship_Startedfrom: $scope.txtfirstdisb_date,
                clientbusiness_vintage: $scope.txtbusiness_client,
                primarysecondary_valuechain: $scope.txtprimary_chain,
                geneticcode_complied: $scope.cbogenetic_code,
                RMD_visitedGid: $scope.assignedRMD_gid,
                RMD_visitedname: $scope.assigned_RMD,
                RM_name: $scope.relationship_managername,
                PPA_name: $scope.txtPPA_name,
                credit_managername: $scope.credit_managername,
                visittype: $scope.cbovisit_done,
                purpose_ofloan: $scope.txtpurposeof_loan,
                requestedamount_byclient: $scope.txtrequestedloan_byclient,
                sanctionedamount_byclient: $scope.txtsantionloan_bycredit,
                disbursement_Date: $scope.txtdisbursement_date,
                disbursement_amount: input,
                totalloan_outstanding: $scope.txttotalloan_oustanding,
                repayment_track: $scope.cborepayment_track,
                repayment_trackremarks: $scope.txtrepayment_borrowings,
                basicrecords_maintain: $scope.cbobasic_records,
                basicrecords_remarks: $scope.txtbasicrecord_remarks,
                turnover_lastFY: $scope.txtturnover_lastfy,
                presentFY_sales: $scope.txtpresent_fysales,
                deferral_pendency: $scope.txtdeferral_pendency,
                total_noofGroups: $scope.txtcbototal_groups,
                CBOfunded_noofGroups: $scope.txtCBOgroup_funded,
                RMD_visitgroups: $scope.txtRMDvisit_groupcount,
                assetverification_createdoutofloan: $scope.txtassetverification_comment,
                assetverification_securitydtls: $scope.txtsecurity_details,
                assetverification_mortgaged: $scope.txtassetverification_mortagged,
                assetverification_ROCcreation: $scope.txtROCcreation,
                purposeof_funding: $scope.txtpurpose_funding,
                utilisation_details: $scope.txt_utilisationdtls,
                adequacy_loanamount: $scope.txtadequacyloan_samunnati,
                adequacy_impactassessment: $scope.txtadequacyloan_impactassessment,
                adequacy_additionalfunding: $scope.txtadditional_funding,
                portfolio_noofmembers: $scope.txtportfolio_noofmember,
                portfolio_activemembers: $scope.txtportfolio_activemembers,
                total_disbursementamount: $scope.txtportfoliototal_loandisbursement,
                outstanding_ondate: $scope.txtportfolio_outstandingdate,
                overdue_beneficiary: $scope.txtportfolio_overduebeneficary,
                overdue_amount: $scope.txtportfolio_overdueAmount,
                overdueaccount_funding: $scope.txtportfolio_fundingoverdue,
                sanctioned_limit: $scope.txtsanctioned_limit,
                tenure_period: $scope.txttenure_period,
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                briefdtls_client: $scope.txtbriefdtls_client,
                overall_remarks: $scope.txtoverall_remarks,
                PDD_compliance: $scope.txtPDD_compliance,
                briefrpt_financials: $scope.txtbriefrpt_financials,
                briefrpt_process: $scope.txtbriefrpt_process,
                briefrpt_customer: $scope.txtbriefrpt_customer,
                briefrpt_learnings: $scope.txtbriefrpt_learnings,
                briefrpt_valuechain: $scope.txtbriefrpt_valuechain,
                valuechain_mapanalysis: $scope.txtvaluechain_mapanalysis,
                competitorbusiness_segment: $scope.txtcompetitorbusiness_segment,
                report_status: 'Save'
            }
        
            lockUI();
            var url = "api/visitReport/postVisitReport"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.VisitreportComplete = function () {
            var input = $scope.txtdisbursement_amount;
            //var arr = input.split(',');
            //var i;
            //for (i = 0; i < arr.length; i++) {
            //    var str = input.replace(',', '');
            //    input = str;
            //}
            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid,
                customer_gid: customer_gid,
                customer_name: $scope.clientName,
                visit_date: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                //riskcode_classification: $scope.cboriskcode_classification,
                visitDate: $scope.visitdate,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                constitution: $scope.txtconstitution,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
                typeof_riskreview: $scope.cborisk_reviewtype,
                business_sector: $scope.txtbusiness_sector,
                registeredoffice_address: $scope.txtregister_address,
                present_address: $scope.txtactual_address,
                contact_details1: $scope.txtcontact_dtl1,
                contact_details2: $scope.txtcontact_dtl2,
                relationship_Startedfrom: $scope.txtfirstdisb_date,
                clientbusiness_vintage: $scope.txtbusiness_client,
                primarysecondary_valuechain: $scope.txtprimary_chain,
                geneticcode_complied: $scope.cbogenetic_code,
                RMD_visitedGid: $scope.assignedRMD_gid,
                RMD_visitedname: $scope.assigned_RMD,
                RM_name: $scope.relationship_managername,
                PPA_name: $scope.txtPPA_name,
                credit_managername: $scope.credit_managername,
                visittype: $scope.cbovisit_done,
                purpose_ofloan: $scope.txtpurposeof_loan,
                requestedamount_byclient: $scope.txtrequestedloan_byclient,
                sanctionedamount_byclient: $scope.txtsantionloan_bycredit,
                disbursement_Date: $scope.txtdisbursement_date,
                disbursement_amount: input,
                totalloan_outstanding: $scope.txttotalloan_oustanding,
                repayment_track: $scope.cborepayment_track,
                repayment_trackremarks: $scope.txtrepayment_borrowings,
                basicrecords_maintain: $scope.cbobasic_records,
                basicrecords_remarks: $scope.txtbasicrecord_remarks,
                turnover_lastFY: $scope.txtturnover_lastfy,
                presentFY_sales: $scope.txtpresent_fysales,
                deferral_pendency: $scope.txtdeferral_pendency,
                total_noofGroups: $scope.txtcbototal_groups,
                CBOfunded_noofGroups: $scope.txtCBOgroup_funded,
                RMD_visitgroups: $scope.txtRMDvisit_groupcount,
                assetverification_createdoutofloan: $scope.txtassetverification_comment,
                assetverification_securitydtls: $scope.txtsecurity_details,
                assetverification_mortgaged: $scope.txtassetverification_mortagged,
                assetverification_ROCcreation: $scope.txtROCcreation,
                purposeof_funding: $scope.txtpurpose_funding,
                utilisation_details: $scope.txt_utilisationdtls,
                adequacy_loanamount: $scope.txtadequacyloan_samunnati,
                adequacy_impactassessment: $scope.txtadequacyloan_impactassessment,
                adequacy_additionalfunding: $scope.txtadditional_funding,
                portfolio_noofmembers: $scope.txtportfolio_noofmember,
                portfolio_activemembers: $scope.txtportfolio_activemembers,
                total_disbursementamount: $scope.txtportfoliototal_loandisbursement,
                outstanding_ondate: $scope.txtportfolio_outstandingdate,
                overdue_beneficiary: $scope.txtportfolio_overduebeneficary,
                overdue_amount: $scope.txtportfolio_overdueAmount,
                overdueaccount_funding: $scope.txtportfolio_fundingoverdue,
                sanctioned_limit: $scope.txtsanctioned_limit,
                tenure_period: $scope.txttenure_period,
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                briefdtls_client: $scope.txtbriefdtls_client,
                overall_remarks: $scope.txtoverall_remarks,
                PDD_compliance: $scope.txtPDD_compliance,
                briefrpt_financials: $scope.txtbriefrpt_financials,
                briefrpt_process: $scope.txtbriefrpt_process,
                briefrpt_customer: $scope.txtbriefrpt_customer,
                briefrpt_learnings: $scope.txtbriefrpt_learnings,
                briefrpt_valuechain: $scope.txtbriefrpt_valuechain,
                valuechain_mapanalysis: $scope.txtvaluechain_mapanalysis,
                competitorbusiness_segment: $scope.txtcompetitorbusiness_segment,
                report_status: 'Completed'
            }
            var url = "api/visitReport/postVisitReport"
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.rmVisitReport');
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
            unlockUI();
        }



        $scope.uploadvisitreport = function (val, val1, name) {
            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#addExternalupload").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
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
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('allocationdtl_gid', allocationdtl_gid);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
            }
        }

        $scope.VisitReportDocumentUpload = function () {

            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/visitReport/visitReportUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    var params = {
                        allocationdtl_gid: allocationdtl_gid
                    }

                    var url = "api/visitReport/getvisitReportDocument";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.visitreportdocument = resp.data.visitreportdocument;
                    });

                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = "";
                    $scope.txtdocument_type = "";
                    $scope.uploadfrm = undefined;

                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.uploadphoto = function (val, val1, name) {
            if (($scope.txtuploadphoto_title == null) || ($scope.txtuploadphoto_title == '') || ($scope.txtuploadphoto_title == undefined)) {
                $("#addPhotoupload").val('');
                Notify.alert('Kindly Enter the Photo Title', 'warning');
            }
            else {
                var frm = new FormData();

                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "photoformatonly");

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
                // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "photoformatonly");

                // if (IsValidExtension == false) {
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('txtuploadphoto_title', $scope.txtuploadphoto_title);
                frm.append('allocationdtl_gid', allocationdtl_gid);
                frm.append('project_flag', "photoformatonly");
                $scope.uploadfrm = frm;
            }
        }
        $scope.VisitReportPhotoUpload = function () {
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/visitReport/visitReportPhotoUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#addPhotoupload").val('');
                    $scope.txtuploadphoto_title = "";
                    var params = {
                        allocationdtl_gid: allocationdtl_gid
                    }
                    var url = "api/visitReport/getvisitReportPhoto";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.visitreportphoto = resp.data.visitreportphoto;
                    });
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
        }

        $scope.uploaddocumentcancel = function (visitreport_documentGid) {
            lockUI();
            var params = {
                visitreport_documentGid: visitreport_documentGid
            }
            var url = 'api/visitReport/visitReportUploadcancel';
            SocketService.getparams(url, params).then(function (resp) {
                var params = {
                    allocationdtl_gid:allocationdtl_gid
                }

                var url = "api/visitReport/getvisitReportDocument";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.visitreportdocument = resp.data.visitreportdocument;
                });
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
        }

        $scope.uploadphotocancel = function (visitreport_photoGid) {
            lockUI();
            var params = {
                visitreport_photoGid: visitreport_photoGid
            }
            var url = 'api/visitReport/visitReportPhotocancel';
            SocketService.getparams(url, params).then(function (resp) {
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/visitReport/getvisitReportPhoto";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.visitreportphoto = resp.data.visitreportphoto;
                });
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                unlockUI();
            });
        }


        $scope.cancel = function () {
            $state.go('app.rmVisitReport');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('zonalAllocation360controller', zonalAllocation360controller);

    zonalAllocation360controller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService'];

    function zonalAllocation360controller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'zonalAllocation360controller';

        activate();

        function activate() {

            lockUI();
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

            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
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
            var url = "api/customerManagement/EscrowSummary";
            SocketService.getparams(url, customer_gid).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowlist = resp.data.escrowSummary;
                }
            });

            var url = "api/allocationManagement/getAllocationdocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                    $scope.upload_list = resp.data.upload_list;
                    $scope.documentUpload=true
                }
                else {
                    $scope.documentNotUpload = true

                }
            });

            var url = "api/visitReport/GetAllocationLogDetail";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.scheduleList = resp.data.schedulelogdtl;
                $scope.calllogdtlList = resp.data.calllogdtl;
                
            });
            unlockUI();
        }

        $scope.escrowInfoView = function (escrow_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EscrowInfoModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    escrow_gid: escrow_gid
                }
                //var url = "api/visitReport/GetScheduleLogHistory";
                //SocketService.getparams(url, params).then(function (resp) {
                //    $scope.scheduleList = resp.data.schedulelogdtl;
                //});

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('zonalMappingcontroller', zonalMappingcontroller);

    zonalMappingcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function zonalMappingcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'zonalMappingcontroller';
        $scope.statedtlList = [];
        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/zonalMapping/getzonalMappingdtl";
            SocketService.get(url).then(function (resp) {
                $scope.zonalMappingList = resp.data.zonalMapping;
                //$scope.htmlContent = "<table></table>";
                if ($scope.zonalMappingList == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.zonalMappingList.length;
                    if ($scope.zonalMappingList.length < 100) {
                        $scope.totalDisplayed = $scope.zonalMappingList.length;
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

        $scope.addzonalMapping = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addZonalMapping.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();

                var url = 'api/newServiceTicket/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submitzonalmapping = function () {
                    lockUI();
                    var zonalrisk_managername = $('#zonalriskmanager :selected').text();
                    var params = {
                        zonal_name: $scope.txtzonalname,
                        zonalrisk_managerGid: $scope.cboemployeegid,
                        zonalrisk_managername: zonalrisk_managername
                    }
                    var url = "api/zonalMapping/postzonalMapping";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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


        $scope.tagstatedtl = function (zonalmapping_gid) {
            var params = {
                zonalmapping_gid: zonalmapping_gid
            }
            var modalInstance = $modal.open({
                templateUrl: '/tagZonalstate.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance','ngTableParams'];
            function ModalInstanceCtrl($scope, $modalInstance,ngTableParams) {
                lockUI();

                var url = "api/rmMapping/getstatedtls";
                SocketService.get(url).then(function (resp) {
                    $scope.statedtllist = resp.data.statedtl;
                });

                var url = 'api/zonalMapping/getviewzonalmappingdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.zonal_name = resp.data.zonal_name;
                    $scope.zonalrisk_managerGid = resp.data.zonalrisk_managerGid;
                    $scope.zonalrisk_managername = resp.data.zonalrisk_managername;
                    $scope.statelist = resp.data.tagzonalmapping;
                    var count = $scope.statelist.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.statedtllist.map(function (x) { return x.state_gid; }).indexOf($scope.statelist[i].state_gid); 
                        $scope.statedtllist[indexs].checked = true;
                    }
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

               
                $scope.tagzonalmapping = function () {
                    lockUI();
                    var stateGidList = [];
                    angular.forEach($scope.statedtllist, function (val) {
                       
                        if (val.checked == true) {
                            var stateGid = val.state_gid;
                            stateGidList.push(stateGid);
                        }
                       
                    });
                    if (stateGidList.length==0) {
                        alert('Choose Atleast One State');
                        unlockUI();
                        return
                    }
                    else
                    {
                        var params = {
                            zonalmapping_gid: zonalmapping_gid,
                            zonalrisk_managerGid: $scope.zonalrisk_managerGid,
                            state_gid: stateGidList
                        }

                        var url = "api/zonalMapping/poststatetag2zonal";
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                $modalInstance.close('closed');
                                activate();
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


        $scope.viewtagstatedtl = function (zonalmapping_gid) {
            var params = {
                zonalmapping_gid: zonalmapping_gid
            }
            var modalInstance = $modal.open({
                templateUrl: '/viewtaggedZonalstate.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'ngTableParams'];
            function ModalInstanceCtrl($scope, $modalInstance, ngTableParams) {
                lockUI();
                var url = 'api/zonalMapping/getviewzonalmappingdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.zonal_name = resp.data.zonal_name;
                    $scope.zonalrisk_managerGid = resp.data.zonalrisk_managerGid;
                    $scope.zonalrisk_managername = resp.data.zonalrisk_managername;
                    $scope.statedtlList = resp.data.tagzonalmapping;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.editstatedtl = function (zonalmapping_gid) {
            var params = {
                zonalmapping_gid: zonalmapping_gid
            }
            var modalInstance = $modal.open({
                templateUrl: '/edittaggedZonalstate.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                lockUI();

                var url = 'api/zonalMapping/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                var url = 'api/zonalMapping/getviewzonalmappingdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.zonal_name = resp.data.zonal_name;
                    $scope.zonalrisk_managerGid = resp.data.zonalrisk_managerGid;
                    $scope.zonalrisk_managername = resp.data.zonalrisk_managername;
                });

                unlockUI();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.updatezonalmapping = function (zonalrisk_managerGid) {
                    var zonalrisk_managername = $('#zonalrisk_managername :selected').text(); 
                    var params = {
                        zonalmapping_gid: zonalmapping_gid,
                        zonalrisk_managername: zonalrisk_managername,
                        zonalrisk_managerGid: $scope.zonalrisk_managerGid
                    }
                    var url = "api/zonalMapping/updatezonalMapping";

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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
