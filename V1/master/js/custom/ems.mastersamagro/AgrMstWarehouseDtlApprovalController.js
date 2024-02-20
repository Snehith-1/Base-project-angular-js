(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstWarehouseDtlApprovalController', AgrMstWarehouseDtlApprovalController);

    AgrMstWarehouseDtlApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstWarehouseDtlApprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstWarehouseDtlApprovalController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.warehouse_gid = searchObject.warehouse_gid;
        var warehouse2approval_gid = searchObject.warehouse2approval_gid;
        var lspage = searchObject.lspage;
        var queryval = searchObject.queryval;

        activate();
        //lockUI();
        function activate() {
            lockUI();
            //$scope.showapprovaldiv = false;
            //if (lspage == "ProductApproval" || lspage == "PMGApproval")
            //    $scope.showapprovaldiv = true;

            if (queryval == "N") {

                $scope.val = false

            }

            else if (queryval == "Y") {

                $scope.val = true

            }

            else { }


            var param = {
                warehouse_gid: $scope.warehouse_gid
            };

            var url = 'api/AgrMstWarehouseview/warehouseGSTView';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.gst_list = resp.data.agrmstgst_list;
            });

            var url = 'api/AgrMstWarehouseView/GetWarehouseRaiseQuerySummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.warehouseraisequerylist = resp.data.warehouseraisequerylist;

            });

            var url = 'api/AgrMstWarehouseView/GetOpenQueryStatus';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.openquery_flag = resp.data.openquery_flag;
                if ($scope.openquery_flag == "Y") {
                    $scope.hideapproval = true;
                }

            });

            var param = {
                warehouse_gid: $scope.warehouse_gid
            };

            //var url = 'api/AgrMstWarehouseview/warehouseAddressTmpList';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.agreementaddress_list = resp.data.agrmstaddress_list;
            //})

            var url = 'api/AgrMstWarehouseview/WarehouseAgreementDetailsView';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.agreementdocumentaddress_list = resp.data.Mdlagrmstagreementdtllist;

            });

            var url = 'api/AgrMstWarehouseview/warehouseMobileNoView';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mobile_list = resp.data.agrmstmobileno_list;
            });

            var url = 'api/AgrMstWarehouseview/warehouseEmailAddressView';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mailaddress_list = resp.data.agrmstemailaddress_list;
            });

            var url = 'api/AgrMstWarehouseview/warehouseAddressView';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.address_list = resp.data.agrmstaddress_list;
            });

            var url = 'api/AgrMstWarehouseview/WarehouseCommodityView';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.commodity_list = resp.data.Warehousevarietyname_list;
            });

            var url = 'api/AgrMstWarehouseview/WarehouseDocumentUploadView';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.warehouseuploaddocument_list = resp.data.agrmstwarhouse_upload;
            });

            var url = 'api/AgrMstWarehouseview/warehouseSpocView';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Warehousespoc_list = resp.data.Warehousespoc_list;
            });

            var url = 'api/AgrMstWarehouseview/Getwarehoueflag';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lsapproval_flag = resp.data.lsapproval_flag;
            });

            var url = 'api/AgrMstWarehouseview/GetProductApprovaldtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.productapproval_list = resp.data.Warehouseapproval_list;
            });
            var url = 'api/AgrMstWarehouseview/GetPmgApprovaldtl';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.pmgapproval_list = resp.data.Warehouseapproval_list;
            });

            var url = 'api/AgrMstWarehouseAdd/EditWarehouseDetails';

          
            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtpan_number = resp.data.warehouse_pan;
                $scope.txtfirst_name = resp.data.first_name;
                $scope.txtmiddle_name = resp.data.middle_name;
                $scope.txtlast_name = resp.data.last_name;
                $scope.txtwarehouse_name = resp.data.warehouse_name;
                $scope.txtwarehouse_no = resp.data.warehouse_ref_no;
                $scope.rdowned_by = resp.data.owned_by;
                $scope.txtsubsidiarywarshouse_name = resp.data.subsidiarywarshouse_name;
                $scope.cbowarehouse_facility = resp.data.warehousefacility_name;
                $scope.txtwarehouse_area = resp.data.warehouse_area;
                $scope.txtwarehousearea_uom = resp.data.warehousearea_uom;
                $scope.txtareacapacity = resp.data.totalcapacity_area;
                $scope.txtareacapacity_uom = resp.data.area_uom;
                $scope.txttotalcapacity_volume = resp.data.totalcapacity_volume;
                $scope.txtvolume_uom = resp.data.volume_uom;
                $scope.txtcharges = resp.data.charges;
                $scope.txtcapacity = resp.data.capacity;
                $scope.created_by = resp.data.created_by;
                $scope.created_date = resp.data.created_date;
                $scope.warehousetype_name = resp.data.typeofwarehouse_name;
                $scope.cboApplicant_name = resp.data.Applicant_name;
                $scope.productapproval_flag = resp.data.productapproval_flag;
                $scope.pmgapproval_flag = resp.data.pmgapproval_flag;
                $scope.warehousesubmit_flag = resp.data.warehousesubmit_flag;
                if ($scope.productapproval_flag == 'Y' && $scope.pmgapproval_flag == 'N' && lspage == "ProductApproval") {
                    $scope.remark_flag = false;
                    $scope.showdiv = false;
                }
                else if ($scope.warehousesubmit_flag = 'Y' && $scope.productapproval_flag == 'N' && $scope.pmgapproval_flag == 'N' && lspage == "ProductApproval") {
                    $scope.remark_flag = true;
                    $scope.showdiv = true;
                }
                else if ($scope.productapproval_flag == 'Y' && $scope.pmgapproval_flag == 'N' && lspage == "PMGApproval") {
                    $scope.remark_flag = true;
                    $scope.showdiv = true;

                }
                unlockUI();
            });


        }

        $scope.approve = function () {
            var product_approvalflag = "N";
            if (lspage == "ProductApproval")
                product_approvalflag = "Y"

            var params = {
                warehouse_gid: $scope.warehouse_gid,
                warehouse2approval_gid: warehouse2approval_gid,
                remarks: $scope.txtapproval_remarks,
                product_approvalflag: product_approvalflag,
                approvalstatus: 'Y'
            }
            var url = 'api/AgrMstWarehouseview/UpdateProductApprovalDtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == "ProductApproval")
                        $location.url('app/AgrMstPendingProductApproval');
                    else if (lspage == "PMGApproval")
                        $location.url('app/AgrMstWarehouseAprovalSummary'); 
                    // activate();
                    //$scope.initiate_flag = 'N';
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.reject = function () {

            var product_approvalflag = "N";
            if (lspage == "ProductApproval")
                product_approvalflag = "Y"

            var params = {
                warehouse_gid: $scope.warehouse_gid,
                warehouse2approval_gid: warehouse2approval_gid,
                remarks: $scope.txtapproval_remarks,
                product_approvalflag: product_approvalflag,
                approvalstatus: 'R'
            }
            var url = 'api/AgrMstWarehouseview/UpdateProductApprovalDtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/AgrMstRejectedWarehouses');
                    //activate();
                    //$scope.initiate_flag = 'N';
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.download_doc = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.back = function () {

            if (lspage == "ProductApproval")
                $location.url('app/AgrMstPendingProductApproval');
            else if (lspage == "PMGApproval")
                $location.url('app/AgrMstPendingPmgApproval');
            else if (lspage == "ApprovedWarehouse")
                $location.url('app/AgrMstWarehouseAprovalSummary');
            else if (lspage == "RejectedWarehouse")
                $location.url('app/AgrMstRejectedWarehouses');
            else
                $location.url('app/AgrMstWarehouseCreationSummary');
        }

        $scope.agreementdoc_upload = function (warehouse2agreement_gid, warehouse_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/defferal_docupload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //$scope.txtwarehousedocument_name = document_title;
                var params = {
                    warehouse2agreement_gid: warehouse2agreement_gid,
                }
                var url = 'api/AgrMstWarehouseEdit/WarehouseDocumentUploadTmpList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.warehouseuploaddocument_list = resp.data.agrmstwarhouse_upload;

                });

                $scope.warehousdocumentupload = function (val) {
                    if (($scope.txtwarehousedocument_name == null) || ($scope.txtwarehousedocument_name == '') || ($scope.txtwarehousedocument_name == undefined)) {
                        $("#momdocument").val('');
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
                            frm.append('document_title', $scope.txtwarehousedocument_name);
                            frm.append('warehouse_gid', $scope.warehouse_gid);
                            frm.append('warehouseagreement_gid', warehouse2agreement_gid);
                            frm.append('project_flag', "documentformatonly");
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

                        $scope.uploadfrm = frm;
                        if ($scope.uploadfrm != undefined) {
                            lockUI();
                            var url = 'api/AgrMstWarehouseAdd/WarehouseDocumentUpload';
                            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                                $scope.warehouseuploaddocument_list = resp.data.agrmstwarhouse_upload;
                                unlockUI();
                                $scope.txtwarehousedocument_name = '';
                                $("#institutionfile").val('');
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
                                        status: 'warning',
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
                }

                $scope.institutiondocument_delete = function (warehouse2docupload_gid) {
                    lockUI();
                    var params = {
                        warehouse2docupload_gid: warehouse2docupload_gid
                    }
                    var url = 'api/AgrMstWarehouseAdd/warehousedoc_delete';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.warehouseuploaddocument_list = resp.data.agrmstwarhouse_upload;
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
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

                $scope.download_doc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');

                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.warehouseuploaddocument_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.warehouseuploaddocument_list[i].document_path, $scope.warehouseuploaddocument_list[i].document_name);
                    }
                }

            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.warehouseuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.warehouseuploaddocument_list[i].document_path, $scope.warehouseuploaddocument_list[i].document_name);
            }
        }

        $scope.Raise_Query = function () {
            $scope.showraisequery = true;
            $scope.showdiv = false;
        }
        $scope.Cancel = function () {
            $scope.txtquery_title = "";
            $scope.txtquery_desc = "";
            $scope.showraisequery = false;
            $scope.showdiv = true;
        }

        $scope.submit = function () {

            if (lspage == "ProductApproval") {
                $scope.product_approvalflag == 'Y'
                $scope.pmgapproval_flag == 'N'
            }
           
            else {
                $scope.product_approvalflag == 'N'
                $scope.pmgapproval_flag == 'Y'
            }

            var params = {
                warehouse_gid: $scope.warehouse_gid,
                description: $scope.txtquery_desc,
                query_title: $scope.txtquery_title,
                query_from: lspage,
                productapproval_flag: $scope.product_approvalflag,
                pmgapproval_flag: $scope.pmgapproval_flag,
            }

            var url = 'api/AgrMstWarehouseView/PostWarehouseRaiseQuery';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.txtquery_title = "";
                    $scope.txtquery_desc = "";
                    $scope.txtapproval_remarks = "";
                    creditorquery_list();
                    $scope.showraisequery = false;
                    $scope.showdiv = true;
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });

            //$modalInstance.close('closed');

        }


        function creditorquery_list() {

            var param = {
                warehouse_gid: $scope.warehouse_gid
            };

            var url = 'api/AgrMstWarehouseView/GetWarehouseRaiseQuerySummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.warehouseraisequerylist = resp.data.warehouseraisequerylist;

            });

            var url = 'api/AgrMstWarehouseView/GetOpenQueryStatus';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.openquery_flag = resp.data.openquery_flag;
                if ($scope.openquery_flag == "Y") {
                    $scope.hideapproval = true;
                }

            });

        }


        $scope.close_query = function (warehouse2query_gid, warehouse_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var params =
                {
                    warehouse2query_gid: warehouse2query_gid
                }
                var url = 'api/AgrMstWarehouseView/GetRaiseQuerydesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquery_title = resp.data.query_title;

                });

                $scope.submit = function () {
                    var params = {
                        warehouse2query_gid: warehouse2query_gid,
                        warehouse_gid: warehouse_gid,
                        close_remarks: $scope.txtcloseremarks
                    }
                    var url = 'api/AgrMstWarehouseView/PostUpdateQueryStatus';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            creditorquery_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                    });

                    $modalInstance.close('closed');
                }

            }
        }



        $scope.view_querydesc = function (warehouse2query_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    warehouse2query_gid: warehouse2query_gid
                }
                var url = 'api/AgrMstWarehouseView/GetRaiseQuerydesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquery_desc = resp.data.description;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        $scope.view_queryremarks = function (warehouse2query_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    warehouse2query_gid: warehouse2query_gid
                }
                var url = 'api/AgrMstWarehouseView/GetRaiseQuerydesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblclose_remarks = resp.data.close_remarks;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

    }

})();
