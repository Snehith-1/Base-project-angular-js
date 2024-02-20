(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstProductEditController', MstProductEditController);

    MstProductEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService', 'cmnfunctionService'];

    function MstProductEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstProductEditController';
        $scope.product_gid = $location.search().lsproduct_gid;
        var product_gid = $scope.product_gid;


        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();
        lockUI();
        function activate() {
            var param = {
                product_gid: product_gid
            };
            var url = 'api/MstApplication360/GetVarietyEditList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.varietyedit_list = resp.data.variety_list;
                unlockUI();
            });
            var params = {
                product_gid: product_gid
            }
            var url = 'api/MstApplication360/EditProduct';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtproduct_code = resp.data.product_code;
                $scope.txtproduct_name = resp.data.product_name;
                $scope.cboSector = resp.data.businessunit_gid;
                $scope.cboCategory = resp.data.valuechain_gid;
                $scope.cboCSACategory = resp.data.category_gid;
                unlockUI();
            });

            var url = 'api/MstApplication360/TempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstApplication360/GetDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.sector_list = resp.data.sector_list;
                $scope.category_list = resp.data.category_list;
                $scope.csacategory_list = resp.data.csacategory_list;
            });

            var url = 'api/AgrMstSamAgroMaster/GettypeofsupplynatureDropdown';
            SocketService.get(url).then(function (resp) {
                $scope.typeofsupplynature_list = resp.data.typeofsupplynature_list;
            });

            var url = 'api/AgrMstSamAgroMaster/GetsectorclassificationDropdown';
            SocketService.get(url).then(function (resp) {
                $scope.sectorclassification_list = resp.data.Mdlsectorclassification_list;
            });

        }
        $scope.update = function () {

            var businessunit_Name = $('#Sector :selected').text();
            var valuechain_Name = $('#Category :selected').text();
            var category_Name = $('#CSACategory :selected').text();
            

            lockUI();
            var url = 'api/MstApplication360/Updateproduct';
            var params = {
                product_code: $scope.txtproduct_code,
                product_name: $scope.txtproduct_name,
                businessunit_gid: $scope.cboSector,
                businessunit_name: businessunit_Name,
                valuechain_gid: $scope.cboCategory,
                valuechain_name: valuechain_Name,
                valuechain_gid: $scope.cboCategory,
                valuechain_name: valuechain_Name,
                category_gid: $scope.cboCSACategory,
                category_name: category_Name,
                product_gid: product_gid
            }
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstProductSummary');
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

        $scope.back = function () {
            $state.go('app.MstProductSummary');
        };

        $scope.variety_edit = function () {
            if (($scope.txtvariety_name == undefined) || ($scope.txtvariety_name == '')) {

                Notify.alert('Enter Variety', 'warning');
            }
            else {
                var lsbusinessunit_gid = '', lsbusinessunit_name = '', lsvaluechain_name = '', lsvaluechain_gid = '';
                if ($scope.cboCommoditySector != undefined || $scope.cboCommoditySector != null) {
                    lsbusinessunit_gid = $scope.cboCommoditySector.businessunit_gid;
                    lsbusinessunit_name = $scope.cboCommoditySector.businessunit_name;
                }
                if ($scope.cboCommodityCategory != undefined || $scope.cboCommodityCategory != null) {
                    lsvaluechain_gid = $scope.cboCommodityCategory.valuechain_gid;
                    lsvaluechain_name = $scope.cboCommodityCategory.valuechain_name;
                }
                var params = {
                    variety_name: $scope.txtvariety_name,
                    botanical_name: $scope.txtbotanical_name,
                    alternative_name: $scope.txtalternative_name,
                    hsn_code: $scope.txtHSN_code,
                    typeofsupplynature_gid: $scope.cbotypeofsupplynature.typeofsupplynature_gid,
                    typeofsupplynature_name: $scope.cbotypeofsupplynature.typeofsupplynature_name,
                    sectorclassification_gid: $scope.cbosectorclassification.sectorclassification_gid,
                    sectorclassification_name: $scope.cbosectorclassification.sectorclassification_name,
                    varietysector_gid: lsbusinessunit_gid,
                    varietysector_name: lsbusinessunit_name,
                    varietycategory_gid: lsvaluechain_gid,
                    varietycategory_name: lsvaluechain_name,
                    headingdesc_product: $scope.txtCommodityheadingdesc,
                }
                lockUI();
                var url = 'api/MstApplication360/CreateVariety';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
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
                    lockUI();
                    var params = {
                        product_gid: product_gid
                    }
                    var url = 'api/MstApplication360/GetVarietyTempEditList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.varietyedit_list = resp.data.application_list;
                        unlockUI();
                    });
                    $scope.txtvariety_name = '';
                    $scope.txtbotanical_name = '';
                    $scope.txtalternative_name = '';
                    $scope.cbotypeofsupplynature = '';
                    $scope.cbosectorclassification = '';
                    $scope.cboCommodityCategory = '';
                    $scope.cboCommoditySector = '';
                    $scope.txtCommodityheadingdesc = '';
                    $scope.txtHSN_code = '';
                });
            }
        }

        $scope.varity_editdelete = function (variety_gid) {
            var params =
                {
                    variety_gid: variety_gid,
                    product_gid: product_gid,
                }
            lockUI();
            var url = 'api/MstApplication360/DeleteVariety';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
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
                var params = {

                    product_gid: product_gid
                }
                lockUI();
                var url = 'api/MstApplication360/GetVarietyTempEditList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.varietyedit_list = resp.data.application_list;
                    unlockUI();
                });
            });

        }


        $scope.varity_addgststatus = function (variety_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/AddGSTStatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) { 
                $scope.calender01 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open01 = true;
                };
                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };
                GetCommodityGstList();
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        variety_gid: variety_gid,
                        IGST_percent: $scope.txtIGST_number,
                        SGST_percent: $scope.txtSGST_number,
                        CGST_percent: $scope.txtCGST_number,
                        CESS_percent: $scope.txtCess_number,
                        wef_date: $scope.txtwef_date
                    }
                    var url = 'api/AgrMstSamAgroMaster/CreateCommodityGst';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            GetCommodityGstList();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtIGST_number = '';
                            $scope.txtSGST_number = '';
                            $scope.txtCGST_number = '';
                            $scope.txtCess_number = '';
                            $scope.txtwef_date = '';
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

                $scope.deletegst_details = function (commoditygststatus_gid) {

                    var params = {
                        commoditygststatus_gid: commoditygststatus_gid
                    }
                    var url = 'api/AgrMstSamAgroMaster/DeleteCommodityGst';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            GetCommodityGstList();
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
                    });
                }

                function GetCommodityGstList() {
                    lockUI();
                    var params = {
                        variety_gid: variety_gid
                    }
                    var url = 'api/AgrMstSamAgroMaster/GetCommodityGstList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.commoditygststatuslist = resp.data.commoditygststatus;
                    });
                    unlockUI();
                }
            }
        }

        $scope.varity_addtradeproduct = function (variety_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/AddTradeProductDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.insurancecompanyselect = false;
                var url = 'api/AgrMstSamAgroMaster/GetMstProductDropdown';
                SocketService.get(url).then(function (resp) {
                    $scope.Productlist = resp.data.MstProductlist;
                });

                var url = 'api/AgrMstSamAgroMaster/GetinsurancecompanyDropdown';
                SocketService.get(url).then(function (resp) {
                    $scope.insurancecompany_list = resp.data.Mdlinsurancecompany_list;
                });

                $scope.Onchangeproducttype = function () {
                    var params = {
                        loanproduct_gid: $scope.cboloanProduct.loanproduct_gid,
                    }
                    var url = 'api/AgrMstSamAgroMaster/GetMstSubProductDropdown';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.loansubproductlist = resp.data.MstSubProductlist;
                    });
                }

                $scope.Onchangeinsurancecompany = function () {
                    var params = {
                        insurancecompany_gid: $scope.cboinsurancecompany.insurancecompany_gid,
                    }
                    var url = 'api/AgrMstSamAgroMaster/GetinsurancePolicyDropdown';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.insurancePolicy_list = resp.data.MdlinsurancePolicy_list;
                    });
                    $scope.insurancecompanyselect = true;
                }

                GetCommodityTradeProdctList();

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.tradedetailsubmit = function () {
                    var insurancecompany_gid, insurancecompany_name, insurancepolicy_gid, insurancepolicy_name;
                    if ($scope.cboinsurancecompany != null) {
                        insurancecompany_gid = $scope.cboinsurancecompany.insurancecompany_gid,
                        insurancecompany_name = $scope.cboinsurancecompany.insurancecompany_name
                    }
                    if ($scope.cboinsurancepolicy != null) {
                        insurancepolicy_gid = $scope.cboinsurancepolicy.insurancecompany2policy_gid,
                        insurancepolicy_name = $scope.cboinsurancepolicy.policy_name
                    }

                    var params = {
                        variety_gid: variety_gid,
                        product_gid: $scope.cboloanProduct.loanproduct_gid,
                        product_name: $scope.cboloanProduct.loanproduct_name,
                        subproduct_gid: $scope.cboloansubproduct.loansubproduct_gid,
                        subproduct_name: $scope.cboloansubproduct.loansubproduct_name,
                        insurancecompany_gid: insurancecompany_gid,
                        insurancecompany_name: insurancecompany_name,
                        insurancepolicy_gid: insurancepolicy_gid,
                        insurancepolicy_name: insurancepolicy_name
                    }
                    var url = 'api/AgrMstSamAgroMaster/CreateCommodityTradeProdct';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            GetCommodityTradeProdctList();
                            $scope.cboloanProduct = '';
                            $scope.cboloansubproduct = '';
                            $scope.cboinsurancecompany = '';
                            $scope.cboinsurancepolicy = '';
                            $scope.loansubproductlist = '';
                            $scope.insurancecompanyselect = false;
                            $scope.insurancePolicy_list = '';
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
                    }); 
                }

                $scope.deletetrade_details = function (commoditytradeproductdtl_gid) {

                    var params = {
                        commoditytradeproductdtl_gid: commoditytradeproductdtl_gid
                    }
                    var url = 'api/AgrMstSamAgroMaster/DeleteCommodityTradeProdct';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            GetCommodityTradeProdctList();
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
                    });
                }

                function GetCommodityTradeProdctList() {
                    lockUI();
                    var params = {
                        variety_gid: variety_gid
                    }
                    var url = 'api/AgrMstSamAgroMaster/GetCommodityTradeProdctList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.commodityTradeProdctlist = resp.data.commodityTradeProdct;
                        unlockUI();
                    });
                }
            }
        }

        $scope.varity_adddocumentupload = function (variety_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/AddDocumentUpload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance','DownloaddocumentService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService) {
                GetCommodityuploadList();
                $scope.calender01 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open01 = true;
                }; 
                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var frm = new FormData();
                $scope.firstUpload = "";
                $scope.CommodityReportDocUpload = function (val, val1, name) {   
                   /* frm.append('project_flag', "Default");*/
                    for (var i = 0; i < val.length; i++) {
                        var item = {
                            name: val[i].name,
                            file: val[i]
                        };
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name); 
                    }
                    if ($scope.firstUpload == "") {
                        $scope.firstUpload = "CommodityReport";
                        frm.append('FirstUpload', 'CommodityReport');
                    }  
                    $scope.uploadCommodityfrm = frm;
                    if ($scope.uploadCommodityfrm != undefined) {
                    }
                    else {
                        alert('Please select a file.') 
                    }
                }
                
                $scope.RiskAnalysisDocUpload = function (val, val1, name) {  
                   /* frm.append('project_flag', "Default");*/
                    for (var i = 0; i < val.length; i++) {
                        var item = {
                            name: val[i].name,
                            file: val[i]
                        };
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name); 
                    }
                    if ($scope.firstUpload == "") {
                        $scope.firstUpload = "RiskAnalysisDoc";
                        frm.append('FirstUpload', 'RiskAnalysisDoc');
                    }
                    
                    $scope.uploadRiskAnalysisfrm = frm;
                    if ($scope.uploadRiskAnalysisfrm != undefined) {
                    }
                    else {
                        alert('Please select a file.')
                    }
                }
                $scope.commoditydownloadall = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].commodityreport_filepath, $scope.commodityDocumentUpload[i].commodityreport_filename);
                    }
                }

                $scope.riskdownloadall = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].riskanalysisreport_filepath, $scope.commodityDocumentUpload[i].riskanalysisreport_filename);
                    }
                }
                $scope.uploadsubmit = function () {
                     
                    if ($scope.uploadCommodityfrm == undefined || $scope.uploadRiskAnalysisfrm == undefined) {
                        alert('Kindly fill all mandatory fields')
                    }
                    else {
                        var date = new Date($scope.txtason_date); 
                        var day = date.getDate();
                        var month = date.getMonth() + 1;
                        var year = date.getFullYear();
                        date = year + "-" + month + "-" + day
                        frm.append('txtason_date', date);
                        frm.append('variety_gid', variety_gid);
                        frm.append('project_flag', "Default");
                        var url = 'api/AgrMstSamAgroMaster/CreateCommodityDocumentUpload';
                        lockUI();
                        SocketService.postFile(url, frm).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                GetCommodityuploadList();
                                $scope.firstUpload = "";
                                $scope.txtason_date = '';
                                $("#commodityfile").val('');
                                $("#docRiskAnalysisfile").val('');
                                frm = new FormData();
                                $scope.uploadCommodityfrm = undefined;
                                $scope.uploadRiskAnalysisfrm = undefined;
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });

                            }
                            else {
                                $scope.firstUpload = "";
                                Notify.alert(resp.data.message, {
                                    status: 'info',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                        });
                    }  
                }
                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }
                $scope.deletedoc_details = function (commoditydocument_gid) {

                    var params = {
                        commoditydocument_gid: commoditydocument_gid
                    }
                    var url = 'api/AgrMstSamAgroMaster/DeleteCommodityDocumentUpload';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            GetCommodityuploadList();
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
                    });
                }

                function GetCommodityuploadList() {
                    var params = {
                        variety_gid: variety_gid
                    }
                    var url = 'api/AgrMstSamAgroMaster/GetCommodityDocumentUploadList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.commodityDocumentUpload = resp.data.commodityDocumentUpload;
                    });
                }

                $scope.doc_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
            }
        }

    }
})();