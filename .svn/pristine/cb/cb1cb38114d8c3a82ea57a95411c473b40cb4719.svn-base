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
