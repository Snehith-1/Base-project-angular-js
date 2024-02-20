(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnStartScheduledMeetingController', AgrTrnStartScheduledMeetingController);

    AgrTrnStartScheduledMeetingController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function AgrTrnStartScheduledMeetingController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, $sce, $anchorScroll, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnStartScheduledMeetingController';
        $scope.lspage = $location.search().lspage;
        var lspage =  $scope.lspage;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;

        const lsdynamiclimitmanagementback = 'AgrTrnStartScheduledMeeting';
        const lspagename = 'AgrTrnStartScheduledMeeting';
        const lspagetype = 'CC';


      
        activate();
        lockUI();
        function activate() {
            var params = {
                application_gid: $scope.application_gid,
                tmp_status: false
            }
            var url = 'api/AgrMstApplicationAdd/GetApplicationTradeList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MdlTradelist = resp.data.MdlTradedtl;
                if ($scope.MdlTradelist == null) {
                    $scope.Tradedivshow = true;
                }
                else {
                    $scope.Tradedivshow = false;
                    $scope.TradeEditdivshow = false;
                }
                unlockUI();
            });

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            $scope.submit_to_approval = false;
            fnapplicationviewinfo();
            var params = {
                application_gid: application_gid
            }
            var url = "api/AgrTrnCAMGeneration/GetApp2CAM"
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cam_content = resp.data.template_content;
                $scope.lspath = resp.data.lspath;
                $scope.lsname = resp.data.lsname;
            });
            var url = 'api/AgrTrnAppCreditUnderWriting/Getproductprogramgid';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsproduct_gid = resp.data.product_gid;
                $scope.lsprogram_gid = resp.data.program_gid;

            });
            var url = 'api/AgrTrnAppCreditUnderWriting/Getrenewalamendmentflag';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lsrenewal_flag = resp.data.renewal_flag;
                $scope.lsamendment_flag = resp.data.amendment_flag;

            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'
            }

            var url = 'api/AgrMstApplicationView/GetRMDetailsView';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtrmdepartment_name = resp.data.department_name;
                $scope.txtrm_name = resp.data.RM_Name;
                $scope.txtappl_initiateddate = resp.data.applicationinitiated_date;
                $scope.txtunderwritten_date = resp.data.ccsubmitted_date;
                $scope.txtunderwritten_by = resp.data.ccsubmitted_by;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCC/GetCCRequestorlist';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.requestorlist = resp.data.ccrequestordtl;
            });

            var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
            lockUI();
            debugger
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.loandtls_list = resp.data.mstLoan_list;
                $scope.txtvalidityfrom_date = resp.data.validityfrom_date;
                $scope.txtvalidityto_date = resp.data.validityto_date;
            });
        }
        
        // <!-- product & charges view start -->
        $scope.tradedtl_view = function (application2trade_gid){
            if($scope.tradedtl_view_flag == undefined || $scope.tradedtl_view_flag == ''){
                $scope.tradedtl_view_flag = true;}
            else if($scope.tradedtl_view_flag == true){
                $scope.tradedtl_view_flag = false;}
            else{$scope.tradedtl_view_flag = true;}
            $scope.application2trade_gid = application2trade_gid;
            $scope.TradeEditdivshow = true;
            var params = {
                application2trade_gid: application2trade_gid
            }
            lockUI();
            var url = 'api/AgrMstApplicationAdd/GetApplicationTradeViewdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboeditproduct_type = resp.data.producttype_name;
                $scope.txteditProductsubType = resp.data.productsubtype_name;
                $scope.rdbeditsalescontract_availability = resp.data.salescontract_availability;
                $scope.cboeditScopeoftransport = resp.data.scopeof_transport;
                $scope.cboeditScopeofloading = resp.data.scopeof_loading;
                $scope.cboeditScopeofunloading = resp.data.scopeof_unloading;
                $scope.cboeditScopeofqualityandquantity = resp.data.scopeof_qualityandquantity;
                $scope.cboeditScopeofmoisturegainloss = resp.data.scopeof_moisturegainloss;
                $scope.cboScopeofInsurance = resp.data.scopeof_insurance;
                unlockUI();
            });
    
            var params = {
                application2trade_gid: application2trade_gid,
                application_gid: $scope.application_gid,
                tmp_status: "true",
                application2loan_gid:''
            }
    
            var url = 'api/AgrMstApplicationAdd/GetTrade2WarehouseTmpDetail';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tradewarehouse_list = resp.data.creditor2warehouse_list;
            });

            var url = 'api/AgrMstCreditorMaster/GetTrade2CreditorTmpDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.trade2creditor_list = resp.data.trade2creditor_list;
            });
    
        }

        $scope.view = function (applicationtrade2warehouse_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    
                    applicationtrade2warehouse_gid: applicationtrade2warehouse_gid
                }
                var url = 'api/AgrMstApplicationAdd/EditTrade2WarehouseDetail';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cbowarehouseagency = resp.data.warehouse_agency;
                    $scope.cbowarehouse_name = resp.data.warehouse_name;
                    $scope.cbowarehousetype_name = resp.data.typeofwarehouse_name;
                    $scope.txtvolume_uom = resp.data.volume_uom;
                    $scope.txtcapacity_volume = resp.data.totalcapacity_volume;
                    $scope.txtareacapacity = resp.data.totalcapacity_area;
                    $scope.txtareacapacity_uom = resp.data.area_uom;
                    $scope.cbowarehouseaddress = resp.data.warehouse_address;
                    $scope.txtcapacity_commodity = resp.data.capacity_commodity;
                    $scope.txtcapacity_panina = resp.data.capacity_panina;
                    
                    unlockUI();
                });
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }    

        $scope.OtherProducts_view = function (application2loan_gid, viewproduct_type) {
            debugger
            $scope.txtviewproduct_type = viewproduct_type;
            if($scope.Products_flag == undefined || $scope.Products_flag == ''){
                $scope.Products_flag = true;}
            else if($scope.Products_flag == true){
                $scope.Products_flag = false;}
            else{$scope.tradedtl_view_flag = true;}
            var params1 = {
                application_gid: '',
                application2loan_gid: application2loan_gid,
                tmp_status: 'false',
            }
            var url = 'api/AgrMstApplicationEdit/GetLoan2Supplierdtl';
            SocketService.getparams(url, params1).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.MdlSupplierdtllist = resp.data.MdlSupplierdtl;
                } else {
                    unlockUI();
                } 
            });
            var params = {
                application_gid: $scope.application_gid,
                application2loan_gid:application2loan_gid,
                tmp_status: 'both',
            }
            var url = 'api/AgrMstApplicationEdit/GetLoan2SupplierPaymentdtl';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.MdlSupplierPaymentlist = resp.data.MdlSupplierPaymentdtl;
                } else {
                    unlockUI();
                }

            });
            var params = {
                application2loan_gid: application2loan_gid,
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationView/GetLoanProgramValueChain';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
            });
            var params2 = {
                application_gid: $scope.application_gid,
                application2loan_gid: application2loan_gid,
                tmp_status: 'false',
            }
            var url = 'api/AgrMstApplicationEdit/GetLoan2Repaymentdtl';
            SocketService.getparams(url, params2).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.MdlrePaymentdtl = resp.data.MdlPaymentdtl;
                }
            });

            var param = {
                application_gid: $scope.application_gid,
                application2loan_gid: application2loan_gid,
            }
            var url = 'api/AgrMstApplicationEdit/GetEditLoanLimit';
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;

                $scope.onboarding_status = resp.data.onboarding_status;

                if (resp.data.overalllimit_amount == "0.00" || resp.data.onboarding_status == "Direct") {

                    $scope.zerofacility = true

                    $scope.txtloanfaility_amount = '0';

                }

                else {

                    $scope.zerofacility = false

                }

                $scope.lsloanfacility_amount = resp.data.loanfacility_amount;
                if (resp.data.loanfacility_amount == '' || resp.data.loanfacility_amount == null) {
                    $scope.lsloanfacility_amount = '0';
                }
            });
            var params = {
                application2loan_gid: application2loan_gid
            }
            var url = 'api/AgrMstApplicationEdit/LoanDetailsEdit';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtfacilityreqon_date = resp.data.facilityrequested_date;
                $scope.cboProductTypelist = resp.data.producttype_gid;
                $scope.loansubproduct_name = resp.data.productsub_type;
                $scope.product_type = resp.data.product_type;
                $scope.lblproducttype = resp.data.product_type;
                $scope.lblproductsub_type = resp.data.productsub_type;
                if ($scope.lblproductsub_type == 'STF') {
                    $scope.stfmandatory = true;
                    $scope.STFdivshow = true;
                }
                else {
                    $scope.stfmandatory = false;
                    $scope.STFdivshow = false;
                }
                var params = {
                    loanproduct_gid: resp.data.producttype_gid,
                    application_gid: '',
                    application2loan_gid: ''
                }
                var url = 'api/AgrMstApplicationAdd/GetLoanSubProduct';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loansubproductlist = resp.data.application_list;
                });

                $scope.cboProductSubTypelist = resp.data.productsubtype_gid;
                $scope.cboLoanTypelist = resp.data.loantype_gid;
                $scope.loan_type = resp.data.loan_type;
                if ($scope.loan_type == 'Secured') {
                    $scope.Collateralshow = true;
                }
                else {
                    $scope.Collateralshow = false;
                }
                $scope.cboSourceType = resp.data.source_type;
                $scope.txtguidelinevalue = resp.data.guideline_value;
               
                $scope.txtguideline_date = resp.data.guideline_date;
                $scope.txtmarketvalue_date = resp.data.marketvalue_date;
                $scope.txtmarketValue = resp.data.market_value;
                
                
                $scope.txtforcedsource_value = resp.data.forcedsource_value;
                
                
                $scope.txtcollateralSSV_value = resp.data.collateralSSV_value;
                
                
                $scope.txtforcedvalueassessed_on = resp.data.forcedvalueassessed_on;
                $scope.txtcolateralobservation_summary = resp.data.collateralobservation_summary;

                $scope.txtloanfaility_amount = resp.data.facilityloan_amount;
                
                
                $scope.txteditrate_interest = resp.data.rate_interest;
                $scope.txteditpanel_interest = resp.data.penal_interest;
                $scope.txteditvalidity_years = resp.data.facilityvalidity_year;
                $scope.txteditvalidity_months = resp.data.facilityvalidity_month;
                $scope.txteditvalidity_days = resp.data.facilityvalidity_days;
                $scope.txtoverallfacilityvalidity_limit = resp.data.facilityoverall_limit;
                //$scope.txtedittenure_years = resp.data.tenureproduct_year;
                //$scope.txtedittenure_months = resp.data.tenureproduct_month;
                $scope.txtedittenure_days = resp.data.tenureproduct_days;
                $scope.txteditoveralllimit_validity = resp.data.tenureoverall_limit;
                $scope.cboFacilityTypelist = resp.data.facility_type;
                $scope.cboFacilitymodelist = resp.data.facility_mode;
                $scope.cboprincipalfrequency = resp.data.principalfrequency_gid;
                $scope.cboInterestFrequency = resp.data.interestfrequency_gid;
                $scope.cboProgram = resp.data.program_gid;

                $scope.valuechainlist = resp.data.valuechainlist;

                $scope.rdbmilestone_applicablity = resp.data.milestone_applicability,
                $scope.rdbinsurance_applicability = resp.data.insurance_applicability,
                $scope.cbomilestonepaymenttype =  resp.data.milestonepayment_gid, 
                $scope.txtsapayout = resp.data.sa_payout,
                $scope.insurance_availability = resp.data.insurance_availability,
                $scope.txtinsurance_percent = resp.data.insurance_percent,
                $scope.txtinsurance_cost = resp.data.insurance_cost,
                $scope.txtnet_yield = resp.data.net_yield,
                   $scope.sa_status = resp.data.sa_status;
                if ($scope.sa_status == "Yes")
                    $scope.showsapayout = true;
                else
                    $scope.showsapayout = false;

                if ($scope.rdbmilestone_applicablity == "Yes") {
                    $scope.showmilestonepaymenttype = true;
                    $scope.disabledmilestonepaymenttype = false;
                }
                else { 
                    $scope.showmilestonepaymenttype = false;
                    $scope.disabledmilestonepaymenttype = true;
                } 

                $scope.rdbinterest_status = resp.data.interest_status;
                $scope.rdbmoratorium_status = resp.data.moratorium_status;
                $scope.cbomoratorium_type = resp.data.moratorium_type;
                $scope.txtmoratorium_startdate = resp.data.moratorium_startdate;
                $scope.txtmoratorium_enddate = resp.data.moratorium_enddate;
                $scope.txtenduse_purpose = resp.data.enduse_purpose; 
                $scope.rdbTradeOriginated = resp.data.trade_orginatedby,
                $scope.txtsabrokerage = resp.data.SA_Brokerage,
                $scope.txtholdingperiod = resp.data.holding_periods,
                $scope.txtholdingMonthlyprocurement = resp.data.holdingmonthly_procurement,
                $scope.txtextendedholdingperiod = resp.data.extendedholding_periods,
                $scope.txtextendedMonthlyprocurement = resp.data.extendedmonthly_procurement,
                $scope.txtcharges_extendedperiod = resp.data.charges_extendedperiod,
                $scope.txtcustomer_advance = resp.data.customer_advance,
                $scope.txtreimburesementof_expenses = resp.data.reimburesementof_expenses,
                $scope.txtreimburesementof_expensespenalty = resp.data.reimburesementof_expensespenalty,
                $scope.bankfunding_documentname = resp.data.bankfundingdata_filename,
                $scope.bankfunding_documentpath = resp.data.bankfundingdata_filepath,
                $scope.txtneedfor_stocking = resp.data.needfor_stocking,
                $scope.txtproduct_portfolio = resp.data.product_portfolio,
                $scope.txtproduction_capacity = resp.data.production_capacity,
                $scope.txtnatureof_operations = resp.data.natureof_operations,
                $scope.txtaveragemonthly_inventoryholding = resp.data.averagemonthly_inventoryholding,
                $scope.txtfinancialinstitutions_relationship = resp.data.financialinstitutions_relationship;
                $scope.txtProgramLimitValidityfrom = resp.data.programlimit_validdfrom,
                $scope.txtProgramLimitValidityTo = resp.data.programlimit_validdto,
                $scope.txtoverallprogramvalidity_limit = resp.data.programoverall_limit;

                // if ($scope.lbloveralllimit_amount != null || $scope.lbloveralllimit_amount != undefined || $scope.lbloveralllimit_amount != '' ){
                //     var lbloveralllimit_amount = ($scope.lbloveralllimit_amount).replaceAll(',','');
                // }
                // else {var lbloveralllimit_amount = $scope.lbloveralllimit_amount}
                // var lsamount = (parseFloat(lbloveralllimit_amount) - parseFloat($scope.txtloanfaility_amount));
                // $scope.txtremaining = parseFloat(lsamount);

                if(resp.data.product_type=='Agri Receivable Finance (ARF)') 
                {
                    $scope.ARF_condition = true;
                }
                else {
                    $scope.ARF_condition = false;
                }
            });
           
    
        }

        $scope.suppliergsttrnview = function (MdlSupplierGSTdtllist) {
            var modalInstance = $modal.open({
                templateUrl: '/SupplierGSTDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.SupplierGSTdtl_list = MdlSupplierGSTdtllist;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.commodity_view = function (application2product_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/CommodityViewDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', 'DownloaddocumentService', 'cmnfunctionService'];
            function ModalInstanceCtrl($scope, $modalInstance, DownloaddocumentService, cmnfunctionService) {
                lockUI();
                var params = {
                    application2product_gid: application2product_gid
                }
                var url = 'api/AgrMstApplicationEdit/GetAppCommodityDtls';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commoditydtls = resp.data;
                        unlockUI();
                    }
                });
                lockUI();
                var params = {
                    application2product_gid: application2product_gid
                }
                var url = 'api/AgrMstApplicationEdit/GetAppCommodityGstList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commoditygststatuslist = resp.data.commoditygststatus;
                        unlockUI();
                    }
                });
                var url = 'api/AgrMstApplicationEdit/GetAppCommodityTradeProdctList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commodityTradeProdctlist = resp.data.commodityTradeProdct;
                        unlockUI();
                    }
                });

                var url = 'api/AgrMstApplicationEdit/GetAppCommodityCustomerpaymentList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commoditycustomerpayment = resp.data.commoditycustomerpayment;
                        unlockUI();
                    }
                });

                var url = 'api/AgrMstApplicationEdit/GetAppCommodityDocumentUploadList';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.commodityDocumentUpload = resp.data.commodityDocumentUpload;
                        unlockUI();
                    }
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.doc_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall_8 = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].commodityreport_filepath, $scope.commodityDocumentUpload[i].commodityreport_filename);
                    }
                }
                $scope.downloadall_9 = function () {
                    for (var i = 0; i < $scope.commodityDocumentUpload.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.commodityDocumentUpload[i].riskanalysisreport_filepath, $scope.commodityDocumentUpload[i].riskanalysisreport_filename);
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


            }
        }
        // <!-- product & charges view end -->
     
        $scope.Back = function () {          
            if (lspage == 'CCMmeetingScheduledcompleted') {
                $location.url('app/AgrTrnCcCompletedScheduledMeeting');
            }             
            else {
                $state.go('app.AgrTrnCcScheduledMeetingSummary');
            }  
        }

        $scope.sendrequestorclick = function () {
            var params = {
                application_gid: application_gid,
                remarks: $scope.txtqueries
            }
            lockUI();
            var url = "api/AgrTrnCC/PostSendCCRequestor";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = "api/AgrTrnCC/GetCCRequestorlist"
                    var param = {
                        application_gid: application_gid
                    };
                    SocketService.getparams(url, param).then(function (resp) {
                        if ((resp.data.status == true)) {
                            $scope.requestorlist = resp.data.ccrequestordtl;
                            unlockUI();
                        }
                        else if (resp.data.status == false)
                            unlockUI();

                        if (resp.data.status == true) {
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
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $scope.txtqueries = "";
            });
        }
        $scope.uploaddocument = function (val, val1, name) {
            //var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            //            if (IsValidExtension == false) {
            //                Notify.alert("File format is not supported..!", {
            //                    status: 'danger',
            //                    pos: 'top-center',
            //                    timeout: 3000
            //                });
            //                return false;
            //            }

            //var item = {
            //    name: val[0].name,
            //    file: val[0]
            //};

            //var frm = new FormData();
            //frm.append('fileupload', item.file);
            //frm.append('file_name', item.name);
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
                    unlockUI();
                    return false;
                }
            }
            frm.append('document_name', $scope.documentname);
            frm.append('document_title', $scope.txtdocument_title);
            frm.append('application_gid', $scope.application_gid);
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            var url = 'api/AgrTrnCC/ConversationCCDocUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $("#addupload").val('');
                $scope.txtdocument_title = '';
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                    var url = "api/AgrTrnCC/GetCCRequestorlist"
                    var param = {
                        application_gid: application_gid
                    };
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {
                        
                        if ((resp.data.status == true)) {
                            $scope.requestorlist = resp.data.ccrequestorlist;
                            unlockUI();
                        }
                        else if (resp.data.status == false)
                            unlockUI();
                        if (resp.data.status == true) {
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
                        //unlockUI();
                    });
                }
                else {
                    //unlockUI();
                    Notify.alert('File Format Not Supported!')

                }

            });

        }

        $scope.downloadsdocument = function (val1, val2) {
            ////var phyPath = val1;
            ////console.log(val1)
            ////var relPath = phyPath.split("EMS");
            ////var relpath1 = relPath[1].replace("\\", "/");
            ////var hosts = window.location.host;
            ////var prefix = location.protocol + "//";
            ////var str = prefix.concat(hosts, relpath1);
            ////var link = document.createElement("a");
            ////var name = val2.split(".")
            ////link.download = val2;
            ////var uri = str;
            ////link.href = uri;
            ////link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.uploadeddoc_Hypothecation = function (application2hypothecation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Hypothecationdocuments.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                 var params =
                   {
                       application2hypothecation_gid : application2hypothecation_gid
                   }
                 var url = 'api/AgrMstApplicationView/GetHypoDocDtl';
               lockUI();
               SocketService.getparams(url, params).then(function (resp) {
                   unlockUI();
                   $scope.Hypothecationdoc_list = resp.data.HypoDocumentList;
    
               });  
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_Hypothecationdoc = function (val1, val2) {
                    ////var phyPath = val1;
                    ////var relPath = phyPath.split("EMS");
                    ////var relpath1 = relPath[1].replace("\\", "/");
                    ////var hosts = window.location.host;
                    ////var prefix = location.protocol + "//";
                    ////var str = prefix.concat(hosts, relpath1);
                    ////var link = document.createElement("a");
                    ////link.download = val2;
                    ////var uri = str;
                    ////link.href = uri;
                    ////link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall_3 = function () {
                    for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
                    }
                }
              
            }
          
        }

        $scope.uploadeddoc_Collateral = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Collateraldocuments.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                    application2loan_gid: application2loan_gid
                   }
                var url = 'api/AgrMstApplicationView/GetCollateralDocDtl';
               lockUI();
               SocketService.getparams(url, params).then(function (resp) {
                   unlockUI();
                   $scope.Collateraldoc_list = resp.data.CollatralDocumentList;
    
               });  
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_Collateraldoc = function (val1, val2) {
                    //var phyPath = val1;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //link.download = val2;
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall_4 = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
                    }
                }
              
            }
          
        }

        $scope.PurposeofLoan_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoan.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                    application2loan_gid: application2loan_gid
                   }
                var url = 'api/AgrMstApplicationView/GetPurposeofLoan';
               lockUI();
               SocketService.getparams(url, params).then(function (resp) {
                   unlockUI();
                   $scope.txtpurposeof_loan = resp.data.enduse_purpose;
    
               });  

               var params =
               {
                application2loan_gid : application2loan_gid
               }
            var url = 'api/AgrMstApplicationView/GetLoantoBuyerList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
               unlockUI();
               $scope.buyer_list = resp.data.mstbuyer_list;
           });  
           var url = 'api/AgrMstApplicationView/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                    $scope.product_gid = resp.data.product_gid;
                    $scope.product_name = resp.data.product_name;
                    $scope.variety_gid = resp.data.variety_gid;
                    $scope.variety_name = resp.data.variety_name;
                    $scope.sector_name = resp.data.sector_name;
                    $scope.category_name = resp.data.category_name;
                    $scope.botanical_name = resp.data.botanical_name;
                    $scope.alternative_name = resp.data.alternative_name;
                });
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                
              
            }
          
        }
      
        var params = 
        {
           application_gid : application_gid
        }
       var url = "api/AgrMstApplicationEdit/GetGroupSummary";
           SocketService.getparams(url, params).then(function (resp) {
               $scope.group_list = resp.data.group_list;
               angular.forEach($scope.group_list, function (value, key) {
                   var params = {
                       group_gid: value.group_gid
                   };

                   var url = 'api/AgrMstApplicationView/GetGrouptoMemberList';
                   SocketService.getparams(url, params).then(function (resp) {
                       value.groupmember_list = resp.data.groupmember_list;
                       value.expand = false;
                   });
               });
           }); 

           $scope.momdocumentUpload = function (val) {
            if (($scope.txtmomdocument_title == null) || ($scope.txtmomdocument_title == '') || ($scope.txtmomdocument_title == undefined)) {
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
                    // frm.append('document_title', $scope.txtmomdocument_title);
                    // frm.append('application_gid', $scope.application_gid);
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

                frm.append('document_title', $scope.txtmomdocument_title);
                frm.append('application_gid', $scope.application_gid);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/AgrTrnCC/MOMDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.momuploaddocument_list = resp.data.momdocument_list;
                        unlockUI();

                        $("#momdocument").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.txtmomdocument_title = '';
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                        fnmom();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }

        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.momdocumentcancel = function (application2momdoc_gid) {
            lockUI();
            var params = {
                application2momdoc_gid: application2momdoc_gid,
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCC/MOM_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.momuploaddocument_list = resp.data.momdocument_list;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); 
                    fnmom();
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
        
        $scope.MOM_save = function () {
       
            var params = {
                mom_description : $scope.MOMDescription,
                application_gid : application_gid
            }
            var url = 'api/AgrTrnCC/MOMDescSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });   
                    fnmom();
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

        $scope.MOM_submit=function()
        {
      
            var params = {
                application_gid: application_gid,
               }
            lockUI();
            var url = "api/AgrTrnCC/PostMOMSubmit";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); 
                    fnmom();
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

        $scope.MOM_resubmit=function()
        {
      
            var params = {
                application_gid: application_gid,
               }
            lockUI();
            var url = "api/AgrTrnCC/PostReMOMSubmit";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); 
                    fnmom();
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

        $scope.gradingtool_view = function (application2gradingtool_gid) {
            var application2gradingtool_gid=application2gradingtool_gid;
            localStorage.setItem('application2gradingtool_gid', application2gradingtool_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnApplCreationGradingToolView";
            window.open(URL, '_blank');
        }

        $scope.visitreport_view = function (visitreport_gid) {
            var visitreport_gid=visitreport_gid;
            localStorage.setItem('visitreport_gid', visitreport_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnApplCreationVisitReportView";
            window.open(URL, '_blank');
        }

        $scope.institution_view = function (institution_gid) {
            var institution_gid = institution_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('institution_gid', institution_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnCCCommitteeInstitutionView?application_gid=" + application_gid + '&institution_gid='+ institution_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.individual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('contact_gid', contact_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnCCCommitteeIndividualView?application_gid=" + application_gid + '&contact_gid='+ contact_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.group_view = function (group_gid) {
            var group_gid = group_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('group_gid', group_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnCCCommitteeGroupView";
            window.open(URL, '_blank');
        }

        $scope.member_view = function (contact_gid) {
            var contact_gid = contact_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('contact_gid', contact_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrTrnCCCommitteeIndividualView";
            window.open(URL, '_blank');
        }

        $scope.gotoGeneticCode = function() {
            $location.hash('GeneticCodedtl');          
            $anchorScroll();
          };

        $scope.gotoEconomicCapital = function() {
            $location.hash('EconomicCapitaldtl');          
            $anchorScroll();
          };

        $scope.gotoProductCharges = function() {
            $location.hash('ProductChargesdtl');
            $anchorScroll();
        };

        $scope.gotoAssessedScore = function() {
            $location.hash('AssessedScoredtl');
            $anchorScroll();
        };

        $scope.gotoVisitReport = function() {
            $location.hash('VisitReportdtl');
            $anchorScroll();
        };

        $scope.gotoCompanyInfo = function() {
            $location.hash('Companydtl');
            $anchorScroll();
        };

        $scope.gotoIndividualInfo = function() {
            $location.hash('Individualdtl');
            $anchorScroll();
        };

        $scope.gotoGroupInfo = function() {
            $location.hash('Groupdtl');
            $anchorScroll();
        };
        
        $scope.gotoTop = function() {
            $location.hash('Generaldtl');
            $anchorScroll();
        };

        $scope.scheduledmeeting_view = function () {
            var modalInstance = $modal.open({
                templateUrl: '/ScheduledMeetingdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    application_gid : application_gid
                }
                lockUI();
                var url = 'api/AgrTrnCC/GetScheduleMeeting';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblccmeeting_no = resp.data.ccmeeting_no;
                    $scope.lblccmeeting_title = resp.data.ccmeeting_title;
                    $scope.lblstart_time = resp.data.start_time;
                    $scope.lblend_time = resp.data.end_time;
                    $scope.lblccmeeting_mode = resp.data.ccmeeting_mode;
                    $scope.lblccgroup_name = resp.data.ccgroup_name;
                    $scope.lbldescription = resp.data.description;
                    $scope.lblccmeeting_date = resp.data.ccmeeting_date;
                    $scope.lblotheruser_name = resp.data.otheruser_name;
                }); 

                var url = 'api/AgrMstApplicationView/GetRMDetailsView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrmdepartment_name = resp.data.department_name;
                    $scope.txtrm_name = resp.data.RM_Name;
                    $scope.txtappl_initiateddate = resp.data.applicationinitiated_date;
                    $scope.txtunderwritten_date = resp.data.ccsubmitted_date;
                    $scope.txtunderwritten_by = resp.data.ccsubmitted_by;
                });
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                
              
            }
          
        }

        $scope.refresh = function () {
            //lockUI();
            activate();
            fnccmember_list();
            fnmom();
        }

        $scope.ccmember_present = function (ccmeeting2members_gid) {
            var params = {
                application_gid: application_gid,
                attendance_status: 'P',
                ccmeeting2members_gid: ccmeeting2members_gid
            }
            lockUI();
            var url = "api/AgrTrnCC/PostCCAttendance";
            SocketService.post(url, params).then(function (resp) {
               
                if (resp.data.status == true) {
                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000 
                        
                    });
                    fnccmember_list();
                    unlockUI();
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
        $scope.ccmember_absent = function (ccmeeting2members_gid) {
            var params = {
                application_gid: application_gid,
                attendance_status: 'A',
                ccmeeting2members_gid: ccmeeting2members_gid
            }
            lockUI();
            var url = "api/AgrTrnCC/PostCCAttendance";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); fnccmember_list();
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


        function fnccmember_list() {
            var param = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCC/GetScheduleMeetingList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                //if ((resp.data.ccmember_list != null && resp.data.ccmember_list.length > 0) || (resp.data.otheruser_list != null && resp.data.otheruser_list.length > 0)) {
                if ((resp.data.status == true)) {
                    $scope.ccmember_list = resp.data.ccmember_list;
                    $scope.otheruser_list = resp.data.otheruser_list;
                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
            });

        }

        $scope.ccother_present = function (ccmeeting2othermembers_gid) {
            var params = {
                application_gid: application_gid,
                attendance_status: 'P',
                ccmeeting2othermembers_gid: ccmeeting2othermembers_gid
            }
            lockUI();
            var url = "api/AgrTrnCC/PostothersAttendance";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); fnccmember_list();
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
        $scope.ccother_absent = function (ccmeeting2othermembers_gid) {
            var params = {
                application_gid: application_gid,
                attendance_status: 'A',
                ccmeeting2othermembers_gid: ccmeeting2othermembers_gid
            }
            lockUI();
            var url = "api/AgrTrnCC/PostothersAttendance";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); fnccmember_list();
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

       

        $scope.undocc_member = function (ccmeeting2members_gid)
        {
            var params = {
                application_gid: application_gid,
                ccmeeting2members_gid: ccmeeting2members_gid
            }
            lockUI();
            var url = "api/AgrTrnCC/PostUndoCCAttendance";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); fnccmember_list();
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

        $scope.undo_others = function (ccmeeting2othermembers_gid) {
            var params = {
                application_gid: application_gid,
                ccmeeting2othermembers_gid: ccmeeting2othermembers_gid
            }
            lockUI();
            var url = "api/AgrTrnCC/PostUndoOthersAttendance";
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    }); fnccmember_list();
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

        $scope.companyLLPnoView = function (companyllpno_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewCompanyandLLPNo.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       companyllpno_gid: companyllpno_gid
                   }
                var url = 'api/AgrMstAPIVerifications/CompanyLLPViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcompany_name = resp.data.company_name;
                    $scope.txtroc_code = resp.data.roc_code;
                    $scope.txtregistration_no = resp.data.registration_no;
                    $scope.txtcompany_category = resp.data.company_category;

                    $scope.txtcompany_subcategory = resp.data.company_subcategory;
                    $scope.txtclass_of_company = resp.data.class_of_company;
                    $scope.txtnumber_of_members = resp.data.number_of_members;
                    $scope.txtdate_of_incorporation = resp.data.date_of_incorporation;

                    $scope.txtcompany_status = resp.data.company_status;
                    $scope.txtregistered_address = resp.data.registered_address;
                    $scope.txtalternative_address = resp.data.alternative_address;
                    $scope.txtemail_address = resp.data.email_address;

                    $scope.txtlisted_status = resp.data.listed_status;
                    $scope.txtsuspended_at_stock_exchange = resp.data.suspended_at_stock_exchange;
                    $scope.txtdate_of_last_AGM = resp.data.date_of_last_AGM;
                    $scope.txtdate_of_balance_sheet = resp.data.date_of_balance_sheet;

                    $scope.txtpaid_up_capital = resp.data.paid_up_capital;
                    $scope.txtauthorised_capital = resp.data.authorised_capital;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.mcasignView = function (mcasignatories_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/MCASignatoriesViewDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       mcasignatories_gid: mcasignatories_gid
                   }
                var url = 'api/AgrMstAPIVerifications/MCASignatoriesViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.mcasignatorydetails_list = resp.data.mcasignatorydetails_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.IECView = function (iecdtl_gid) {
            var iecdtl_gid = iecdtl_gid;
            localStorage.setItem('iecdtl_gid', iecdtl_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrIECDetailedProfileView";
            window.open(URL, '_blank');
        }

        $scope.FSSAIView = function (fssailicenseauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewFSSAIDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       fssailicenseauthentication_gid: fssailicenseauthentication_gid
                   }
                var url = 'api/AgrMstAPIVerifications/FSSAIViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtfssai_status = resp.data.fssai_status;
                    $scope.txtlicense_type = resp.data.license_type;
                    $scope.txtlicense_no = resp.data.license_no;
                    $scope.txtfirm_name = resp.data.firm_name;
                    $scope.txtaddress = resp.data.address;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.FDAView = function (fdalicenseauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewFDADetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       fdalicenseauthentication_gid: fdalicenseauthentication_gid
                   }
                var url = 'api/AgrMstAPIVerifications/FDAViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtstore_name = resp.data.store_name;
                    $scope.txtcontact_no = resp.data.contact_no;
                    $scope.txtlicense_detail = resp.data.license_detail;
                    $scope.txtname = resp.data.name;
                    $scope.txtaddress = resp.data.address;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.authenticationView = function (institution2branch_gid) {
            var institution2branch_gid = institution2branch_gid;
            localStorage.setItem('institution2branch_gid', institution2branch_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrGSTAuthenticationView";
            window.open(URL, '_blank');
        }

        $scope.verificationView = function (institution2branch_gid) {
            var institution2branch_gid = institution2branch_gid;
            localStorage.setItem('institution2branch_gid', institution2branch_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrGSPGSTINAuthenticationView";
            window.open(URL, '_blank');
        }

        $scope.returnfillingView = function (institution2branch_gid) {
            var institution2branch_gid = institution2branch_gid;
            localStorage.setItem('institution2branch_gid', institution2branch_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrGSPGSTReturnFilingView";
            window.open(URL, '_blank');
        }

        $scope.LPGIDView = function (lpgiddtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewLPGID.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       lpgiddtl_gid: lpgiddtl_gid
                   }
                var url = 'api/AgrMstAPIVerifications/LPGIDViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtstatus = resp.data.result.status;
                    $scope.txtApproximateSubsidyAvailed = resp.data.result.ApproximateSubsidyAvailed;
                    $scope.txtSubsidizedRefillConsumed = resp.data.result.SubsidizedRefillConsumed;
                    $scope.txtpin = resp.data.result.pin;

                    $scope.txtConsumerEmail = resp.data.result.ConsumerEmail;
                    $scope.txtDistributorCode = resp.data.result.DistributorCode;
                    $scope.txtBankName = resp.data.result.BankName;
                    $scope.txtIFSCCode = resp.data.result.IFSCCode;

                    $scope.txtAadhaarNo = resp.data.result.AadhaarNo;
                    $scope.txtConsumerContact = resp.data.result.ConsumerContact;
                    $scope.txtDistributorAddress = resp.data.result.DistributorAddress;
                    $scope.txtConsumerName = resp.data.result.ConsumerName;

                    $scope.txtConsumerNo = resp.data.result.ConsumerNo;
                    $scope.txtDistributorName = resp.data.result.DistributorName;
                    $scope.txtBankAccountNo = resp.data.result.BankAccountNo;
                    $scope.txtGivenUpSubsidy = resp.data.result.GivenUpSubsidy;

                    $scope.txtConsumerAddress = resp.data.result.ConsumerAddress;
                    $scope.txtLastBookingDate = resp.data.result.LastBookingDate;
                    $scope.txtTotalRefillConsumed = resp.data.result.TotalRefillConsumed;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.ShopView = function (shopandestablishment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewShopDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       shopandestablishment_gid: shopandestablishment_gid
                   }
                var url = 'api/AgrMstAPIVerifications/ShopViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtcategory = resp.data.result.category;
                    $scope.txtstatus = resp.data.result.status;
                    $scope.txtcommenceDate = resp.data.result.commenceDate;
                    $scope.txttotalWorkers = resp.data.result.totalWorkers;

                    $scope.txtfatherNameOfOccupier = resp.data.result.fatherNameOfOccupier;
                    $scope.txtemail = resp.data.result.email;
                    $scope.txtwebsiteUrl = resp.data.result.websiteUrl;
                    $scope.txtpdfLink = resp.data.result.pdfLink;

                    $scope.txtownerName = resp.data.result.ownerName;
                    $scope.txtaddress = resp.data.result.address;
                    $scope.txtapplicantName = resp.data.result.applicantName;
                    $scope.txtvalidFrom = resp.data.result.validFrom;

                    $scope.txtnatureOfBusiness = resp.data.result.natureOfBusiness;
                    $scope.txtvalidTo = resp.data.result.validTo;
                    $scope.txtregistrationDate = resp.data.result.registrationDate;


                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.RCAuthAdvancedView = function (vehiclercauthadvanced_gid) {
            var vehiclercauthadvanced_gid = vehiclercauthadvanced_gid;
            localStorage.setItem('vehiclercauthadvanced_gid', vehiclercauthadvanced_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrRCAuthAdvancedView";
            window.open(URL, '_blank');
        }

        $scope.RCSearchView = function (vehiclercsearch_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewRCSearchDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       vehiclercsearch_gid: vehiclercsearch_gid
                   }
                var url = 'api/AgrMstAPIVerifications/RCSearchViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrc_manu_month_yr = resp.data.result.rc_manu_month_yr;
                    $scope.txtrc_maker_model = resp.data.result.rc_maker_model;
                    $scope.txtrc_f_name = resp.data.result.rc_f_name;
                    $scope.txtrc_eng_no = resp.data.result.rc_eng_no;

                    $scope.txtrc_owner_name = resp.data.result.rc_owner_name;
                    $scope.txtrc_vh_class_desc = resp.data.result.rc_vh_class_desc;
                    $scope.txtrc_present_address = resp.data.result.rc_present_address;
                    $scope.txtrc_color = resp.data.result.rc_color;

                    $scope.txtrc_regn_no = resp.data.result.rc_regn_no;
                    $scope.txttax_paid_upto = resp.data.result.tax_paid_upto;
                    $scope.txtrc_maker_desc = resp.data.result.rc_maker_desc;
                    $scope.txtrc_chasi_no = resp.data.result.rc_chasi_no;

                    $scope.txtrc_mobile_no = resp.data.result.rc_mobile_no;
                    $scope.txtrc_registered_at = resp.data.result.rc_registered_at;
                    $scope.txtrc_valid_upto = resp.data.result.rc_valid_upto;
                    $scope.txtrc_regn_dt = resp.data.result.rc_regn_dt;

                    $scope.txtrc_financer = resp.data.result.rc_financer;
                    $scope.txtrc_permanent_address = resp.data.result.rc_permanent_address;



                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.PropertyTaxView = function (propertytax_gid) {
            var propertytax_gid = propertytax_gid;
            localStorage.setItem('propertytax_gid', propertytax_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrPropertyTaxView";
            window.open(URL, '_blank');
        }

        $scope.Approve = function (txtremarks)
        {
              var params = {
                application_gid: application_gid,
                approval_status: 'Approved',
                remarks: txtremarks
            }           
            lockUI();
            var url = "api/AgrTrnCC/PostCCApprove";
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
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $scope.txtqueries = "";
                fnmom();
            });
        }
        $scope.Reject = function (txtremarks) {
            var params = {
                application_gid: application_gid,
                approval_status: 'Rejected',
                remarks: txtremarks
            }
            lockUI();
            var url = "api/AgrTrnCC/PostCCApprove";
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
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $scope.txtqueries = "";
                fnmom();
            });
        }

        $scope.cc_remarksview = function (ccmeeting2members_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewccremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       application_gid: application_gid,
                       ccmeeting2members_gid: ccmeeting2members_gid
                   }
                var url = 'api/AgrTrnCC/ViewCCRemarks';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblremarks = resp.data.remarks;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }
        $scope.other_remarksview = function (ccmeeting2othermembers_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewccremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       ccmeeting2othermembers_gid: ccmeeting2othermembers_gid,
                       application_gid: application_gid,
                   }
                var url = 'api/AgrTrnCC/ViewOtherRemarks';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblremarks = resp.data.remarks;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.epicauthenticationView = function (kycepicauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewEpicAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycepicauthentication_gid: kycepicauthentication_gid
                   }
                var url = 'api/AgrKycView/VoterIDViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.name = resp.data.result.name;
                    $scope.rln_name = resp.data.result.rln_name;
                    $scope.rln_type = resp.data.result.rln_type;
                    $scope.gender = resp.data.result.gender;

                    $scope.district = resp.data.result.district;
                    $scope.ac_name = resp.data.result.ac_name;
                    $scope.pc_name = resp.data.result.pc_name;
                    $scope.state = resp.data.result.state;

                    $scope.epic_no = resp.data.result.epic_no;
                    $scope.dob = resp.data.result.dob;
                    $scope.age = resp.data.result.age;
                    $scope.part_no = resp.data.result.part_no;

                    $scope.slno_inpart = resp.data.result.slno_inpart;
                    $scope.ps_name = resp.data.result.ps_name;
                    $scope.part_name = resp.data.result.part_name;
                    $scope.last_update = resp.data.result.last_update;

                    $scope.ps_lat_long = resp.data.result.ps_lat_long;
                    $scope.rln_name_v1 = resp.data.result.rln_name_v1;
                    $scope.rln_name_v2 = resp.data.result.rln_name_v2;
                    $scope.rln_name_v3 = resp.data.result.rln_name_v3;

                    $scope.section_no = resp.data.result.section_no;
                    $scope.id = resp.data.result.id;
                    $scope.name_v1 = resp.data.result.name_v1;
                    $scope.name_v2 = resp.data.result.name_v2;

                    $scope.name_v3 = resp.data.result.name_v3;
                    $scope.ac_no = resp.data.result.ac_no;
                    $scope.st_code = resp.data.result.st_code;
                    $scope.house_no = resp.data.result.house_no;


                 
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.dlauthenticationView = function (kycdlauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewDLAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycdlauthentication_gid: kycdlauthentication_gid
                   }
                var url = 'api/AgrKycView/DrivingLicenseViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.status = resp.data.result.status;
                    $scope.fatherhusband = resp.data.result.fatherhusband;
                    $scope.bloodGroup = resp.data.result.bloodGroup;
                    $scope.dlNumber = resp.data.result.dlNumber;

                    $scope.name = resp.data.result.name;
                    $scope.dob = resp.data.result.dob;
                    $scope.issueDate = resp.data.result.issueDate;

                    $scope.validity_nonTransport = resp.data.result.validity.nonTransport;
                    $scope.validity_transport = resp.data.result.validity.transport;

                    $scope.statusDetails_remarks = resp.data.result.statusDetails.remarks;
                    $scope.statusDetails_to = resp.data.result.statusDetails.to;
                    $scope.statusDetails_from = resp.data.result.statusDetails.from;

                    $scope.address_list = resp.data.result.address;
                    $scope.covDetails_list = resp.data.result.covDetails;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.passportauthenticationView = function (kycpassportauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewPassportAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycpassportauthentication_gid: kycpassportauthentication_gid
                   }
                var url = 'api/AgrKycView/PassportViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.passportNumberFromSource = resp.data.result.passportNumber.passportNumberFromSource;
                    $scope.passportNumberMatch = resp.data.result.passportNumber.passportNumberMatch;

                    $scope.typeOfApplication = resp.data.result.typeOfApplication;
                    $scope.applicationDate = resp.data.result.applicationDate;

                    $scope.dispatchedOnFromSource = resp.data.result.dateOfIssue.dispatchedOnFromSource;
                    $scope.dateOfIssueMatch = resp.data.result.dateOfIssue.dateOfIssueMatch;

                    $scope.nameFromPassport = resp.data.result.name.nameFromPassport;
                    $scope.surnameFromPassport = resp.data.result.name.surnameFromPassport;
                    $scope.nameMatch = resp.data.result.name.nameMatch;
                    $scope.nameScore = resp.data.result.name.nameScore;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.GSTSBPANView = function (kycgstsbpan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewGSTSBPAN.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycgstsbpan_gid: kycgstsbpan_gid
                   }
                var url = 'api/AgrKycView/GSTSBPANViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.GSTSBPAN_list = resp.data.result;                   

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.IFSCAuthenticationView = function (kycifscauthentication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewIFSCAuthentication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycifscauthentication_gid: kycifscauthentication_gid
                   }
                var url = 'api/AgrKycView/IFSCViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.city = resp.data.result.city;
                    $scope.office = resp.data.result.office;
                    $scope.district = resp.data.result.district;
                    $scope.ifsc = resp.data.result.ifsc;
                    $scope.micr = resp.data.result.micr;
                    $scope.state = resp.data.result.state;
                    $scope.contact = resp.data.result.contact;
                    $scope.branch = resp.data.result.branch;
                    $scope.address = resp.data.result.address;
                    $scope.bank = resp.data.result.bank;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.BankAccVerificationView = function (kycbankaccverification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/viewBankAccVerification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       kycbankaccverification_gid: kycbankaccverification_gid
                   }
                var url = 'api/AgrKycView/BankAccViewDetails';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.bankTxnStatus = resp.data.result.bankTxnStatus;
                    $scope.accountNumber = resp.data.result.accountNumber;
                    $scope.ifsc = resp.data.result.ifsc;
                    $scope.accountName = resp.data.result.accountName;
                    $scope.bankResponse = resp.data.result.bankResponse;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.getReApprovalRequest = function (ccmeeting2members_gid, CCMember_name) {
            var modalInstance = $modal.open({
                templateUrl: '/getReApprovalmodal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //var params =
                //{
                //    ccmeeting2members_gid: ccmeeting2members_gid,
                //    application_gid: application_gid

                //}
                //var url = 'api/AgrTrnCC/GetApprovalInitiate';
                //lockUI();
                //SocketService.getparams(url, params).then(function (resp) {
                //    unlockUI();
                //    // $scope.employee_list = resp.data.employee_list;
                //    $scope.txtccmember_name = resp.data.ccmember_name;
                //    $scope.ccmember_gid = resp.data.ccmember_gid;                                  

                //});
                ////var url = 'api/OsdMstActivity/GetTeamSummary';
                ////SocketService.get(url).then(function (resp) {
                ////    $scope.supportdtllist = resp.data.supportdtl;
                ////});
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.txtccmember_name = CCMember_name;

                $scope.getreapprovalclick = function () {
                    var params = {
                        ccmember_name: $scope.txtccmember_name,                     
                        ccmeeting2members_gid: ccmeeting2members_gid,
                        application_gid: application_gid
                       
                    }
                    lockUI();
                    var url = "api/AgrTrnCC/PostApprovalInitiate";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            fnccmember_list();

                        }
                        else {
                            modalInstance.close('closed');
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

        $scope.cancelapprovalinitiate = function (ccmeeting2members_gid) {
            var params = {
                ccmeeting2members_gid: ccmeeting2members_gid,
                application_gid: application_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Cancel the Approval Initiated ?',
                showCancelButton: true,
                cancelButtonText: 'Close',
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Cancel it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = "api/AgrTrnCC/CancelApprovalInitiate"
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                           
                            unlockUI();
                            fnccmember_list();
                            
                            $state.go('app.AgrTrnStartScheduledMeeting');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            fnccmember_list();
                        }
                    });
                    SweetAlert.swal('Cancelled Successfully!');
                }

            });
        };

        $scope.TaggedCase_View = function (data) {
            var modalInstance = $modal.open({
                templateUrl: '/TaggedCaseView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.lblcinNumber = data.cin_number;
                $scope.lblcaseType = data.case_type;
                $scope.lblcaseStatus = data.case_status;
                $scope.lblpetitioner = data.petitioner;
                $scope.lblpetitionerAddress = data.petitioner_address;
                $scope.lblrespondent = data.respondent;
                $scope.lblrespondentAddress = data.respondent_address;
                $scope.lblcaseTypeName = data.casetype_name;
                $scope.lblcaseName = data.case_name
                $scope.lblcourtType = data.court_type;
                $scope.lbldistrict = data.district;
                $scope.lblstate = data.state;
                $scope.lblyear = data.year;
                $scope.lblgfc_updated_at = data.gfc_updated_at;
                $scope.lblgfc_uniqueid = data.gfc_uniqueid;
                $scope.lblcasedetails_link = data.casedetails_link;
                




                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }

        }

        $scope.download_crimereport = function (val1, val2) {          
            var link = document.createElement("a");
            link.download = val2;
            var uri = val1;
            link.href = uri;
            link.click();
            //DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.getapplicationviewinfo = function () {
            fnapplicationviewinfo();
        }

        $scope.getapiverifiedinfo = function () {
            fngetapiverifiedinfo();
        }

        $scope.getcrimecases = function () {
            fngetcrimecases();
        }

        $scope.getcamviewdownload = function () {
            fncamviewdownload();
        }

        $scope.getmom = function () {
            fnmom();
        }

        $scope.getattendance = function () {
            fnattendance();
        }

        function fnapplicationviewinfo() {
           $scope.amendmentshow = false;
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {

                $scope.txtapplication_no = resp.data.application_no;
                $scope.txtbasiccustomer_name = resp.data.customer_name;
                $scope.txtcustomer_urn = resp.data.customer_urn;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtvertical_tag = resp.data.verticaltaggs_name;
                $scope.txtconstitution = resp.data.constitution_name;
                $scope.txt_strategicbusiness_unit = resp.data.businessunit_name;
                $scope.txtprimayvalue_chain = resp.data.primaryvaluechain_name;
                $scope.txtsecondaryvalue_chain = resp.data.secondaryvaluechain_name;
                $scope.txtvernacular_language = resp.data.vernacular_language;
                $scope.txtApplfrom_SA = resp.data.sa_status;
                $scope.txtSAM_associateID = resp.data.sa_id;
                $scope.txtSAM_associatename = resp.data.sa_name;
                $scope.txtcontactperson_name = resp.data.contactperson_name;
                $scope.txtbasicdesignation = resp.data.designation_type;
                $scope.txtlandline_number = resp.data.landline_no;
                $scope.txtsocial_capital = resp.data.social_capital;
                $scope.txttrade_capital = resp.data.trade_capital;
                $scope.borrower_flag = resp.data.borrower_flag;
                $scope.borrower_type = resp.data.borrower_type;
                $scope.momapproval_flag = resp.data.momapproval_flag;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.txtproduct_name = resp.data.product_name;
                $scope.txtsector_name = resp.data.sector_name;
                $scope.txtcategory_name = resp.data.category_name;
                $scope.txtvariety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txtprogram_name = resp.data.program_name;
                $scope.cccompleted_flag = resp.data.cccompleted_flag;
                $scope.onboard_gid = resp.data.buyeronboard_gid;
                $scope.lblamendment_remarks = resp.data.amendment_remarks;
                unlockUI();
                if ($scope.lblamendment_remarks == null || $scope.lblamendment_remarks == '' || $scope.lblamendment_remarks == undefined) {
                    $scope.amendmentshow = false;
                }
                else {
                    $scope.amendmentshow = true;
                }
                unlockUI();
            });

            var url = 'api/AgrMstApplicationView/GetMobileMailDetailsView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimary_number = resp.data.primary_mobileno;
                $scope.basicmobileno_list = resp.data.mobilenumber_list;
                $scope.txtprimary_emailassdress = resp.data.primary_email;
                $scope.mailaddress_list = resp.data.mail_list;
            });

            var url = 'api/AgrMstApplicationView/GetGeneticDetailsView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.geneticcode_list = resp.data.geneticdetails_list;
            });

            var url = 'api/AgrMstApplicationView/GetProductChargesDtl';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                $scope.loandtls_list = resp.data.mstLoan_list;
                $scope.buyer_list = resp.data.mstbuyer_list;
                $scope.txtpurposeof_loan = resp.data.enduse_purpose;
                $scope.txt_processingfee = resp.data.processing_fee;
                $scope.txtprocessing_collecttype = resp.data.processing_collectiontype;
                $scope.txtdoc_charges = resp.data.doc_charges;
                $scope.txtdoc_collecttype = resp.data.doccharge_collectiontype;
                $scope.txtfield_visitcharges = resp.data.fieldvisit_charge;
                $scope.txtfield_collecttype = resp.data.fieldvisit_collectiontype;
                $scope.txtadhoc_fee = resp.data.adhoc_fee;
                $scope.txtadhoccollection_type = resp.data.adhoc_collectiontype;
                $scope.txtlife_insurance = resp.data.life_insurance;
                $scope.txtlife_collectiontype = resp.data.lifeinsurance_collectiontype;
                $scope.txtaccident_insurance = resp.data.acct_insurance;
                $scope.txttotal_collectible = resp.data.total_collect;
                $scope.txttotal_deductible = resp.data.total_deduct;
                $scope.Collateral_list = resp.data.mstcollateral_list;
                $scope.txtproduct_type = resp.data.product_type;
                $scope.servicecharge_List = resp.data.servicecharge_List;
                $scope.txtsecurity_type = resp.data.security_type;
                $scope.txtsecurity_description = resp.data.security_description;
                $scope.txtsecurity_value = resp.data.security_value;
                $scope.txtsecurityassessed_date = resp.data.securityassessed_date;
                $scope.txtasset_id = resp.data.asset_id;
                $scope.txtroc_fillingid = resp.data.roc_fillingid;
                $scope.txtCERSAI_fillingid = resp.data.CERSAI_fillingid;
                $scope.txthypoobservation_summary = resp.data.hypoobservation_summary;
                $scope.txtprimary_security = resp.data.primary_security;
                $scope.application2hypothecation_gid = resp.data.application2hypothecation_gid;
                $scope.txtvalidityfrom_date = resp.data.validityfrom_date;
                $scope.txtvalidityto_date = resp.data.validityto_date;
            });


            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'
            }
            var url = 'api/AgrMstApplicationView/GetGradingToolDtls';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.gradetoolsummary_list = resp.data.mstgradetoolsummary_list;
            });


            var params = {
                application_gid: application_gid,
                statusupdated_by: 'Credit',
            }
            var url = 'api/AgrMstApplicationGradingTool/GetGradingTool';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.gradingtoolcredit_list = resp.data.grading_list;

            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'

            }
            var url = 'api/AgrMstApplicationView/GetVisitReportList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReport_List = resp.data.VisitReport_List;
            });

            var url = 'api/AgrMstApplicationView/GetIndividualList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/AgrMstApplicationView/GetInstitutionList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });


            var params = {
                application_gid: application_gid,
                statusupdated_by: 'Credit',
            }
            var url = 'api/AgrMstApplicationVisitReport/GetVisitReportList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReportCreditList = resp.data.VisitReportList;

            });

            var params = {
                application_gid: application_gid
            }
            var url = "api/AgrMstApplicationEdit/GetGroupSummary";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.group_list = resp.data.group_list;
                angular.forEach($scope.group_list, function (value, key) {
                    var params = {
                        group_gid: value.group_gid
                    };

                    var url = 'api/AgrMstApplicationView/GetGrouptoMemberList';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.groupmember_list = resp.data.groupmember_list;
                        value.expand = false;
                    });
                });
            });
        }

        function fngetapiverifiedinfo() {
            var param = {
                application_gid: application_gid
            }

            var url = 'api/AgrKycView/GetPANAuthenticationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.panauthentication_list = resp.data.panauthentication_list;
            });

            var url = 'api/AgrKycView/GetDLAuthenticationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.dlauthentication_list = resp.data.dlauthentication_list;
            });
            var url = 'api/AgrKycView/GetEPICAuthenticationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.epicauthentication_list = resp.data.epicauthentication_list;
            });

            var url = 'api/AgrKycView/GetPassportAuthenticationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.passportauthentication_list = resp.data.passportauthentication_list;
            });

            var url = 'api/AgrKycView/GetIFSCAuthenticationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.ifscauthentication_list = resp.data.ifscauthentication_list;
            });

            var url = 'api/AgrKycView/GetBankAccVerificationDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.bankaccverification_list = resp.data.bankaccverification_list;
            });

            var url = 'api/AgrKycView/GetGSTSBPANDtl';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.gstsbpan_list = resp.data.gstsbpan_list;
            });


          

            var url = 'api/AgrMstAPIVerifications/AppnCompanyLLPList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.cin_list = resp.data.cin_list;
                $scope.cinlist_length = $scope.cin_list.length;
            });
            var url = 'api/AgrMstAPIVerifications/AppnMCASignatureList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mcasign_list = resp.data.cin_list;
                $scope.mcasignlist_length = $scope.mcasign_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnIECDetailedList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.IECDetailed_list = resp.data.IECDetailed_list;
                $scope.IECDetailedlist_length = $scope.IECDetailed_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnFSSAIList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.fssai_list = resp.data.fssai_list;
                $scope.fssailist_length = $scope.fssai_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnFDAList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.fda_list = resp.data.fda_list;
                $scope.fdalist_length = $scope.fda_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnGSTVerificationList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.gstverification_list = resp.data.gst_list;
                $scope.gstverificationlist_length = $scope.gstverification_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnGSTReturnFilingList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.gstreturnfiling_list = resp.data.gst_list;
                $scope.gstreturnfilinglist_length = $scope.gstreturnfiling_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnGSTAuthenticationList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.gstauthentication_list = resp.data.gst_list;
                $scope.gstauthenticationlist_length = $scope.gstauthentication_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnLPGIDAuthenticationList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.LPGID_list = resp.data.LPGID_list;
                $scope.LPGIDlist_length = $scope.LPGID_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnShopList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.shop_list = resp.data.shop_list;
                $scope.shoplist_length = $scope.shop_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnRCAuthAdvancedList';
             lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.RCAuthAdvanced_list = resp.data.RCAuthAdvanced_list;
                $scope.RCAuthAdvancedlist_length = $scope.RCAuthAdvanced_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnRCSearchList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.RCSearch_list = resp.data.RCSearch_list;
                $scope.RCSearchlist_length = $scope.RCSearch_list.length;
            });

            var url = 'api/AgrMstAPIVerifications/AppnPropertyTaxList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.PropertyTax_list = resp.data.PropertyTax_list;
                $scope.PropertyTaxlist_length = $scope.PropertyTax_list.length;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.requestorlist = resp.data.ccrequestordtl;
            });
        }

        function fngetcrimecases() {

            var paramcrimetc = {
                application_gid: application_gid
            }
            var url = 'api/AgrCrimeCheckAPI/GetCCCaseTaggedIndividualSummary';
            lockUI();
            SocketService.getparams(url, paramcrimetc).then(function (resp) {
                unlockUI();
                $scope.cccasetaggedindividual_list = resp.data.cccasetaggedindividual_list;
            });
            var url = 'api/AgrCrimeCheckAPI/GetCCCaseTaggedInstitutionSummary';
            lockUI();
            SocketService.getparams(url, paramcrimetc).then(function (resp) {
                unlockUI();
                $scope.cccasetaggedinstitution_list = resp.data.cccasetaggedinstitution_list;
            });
            var url = 'api/AgrCrimeCheckAPI/GetCCReportInstitutionSummary';
            lockUI();
            SocketService.getparams(url, paramcrimetc).then(function (resp) {
                unlockUI();
                $scope.ccreportinstitution_list = resp.data.ccreportinstitution_list;
            });
            var url = 'api/AgrCrimeCheckAPI/GetCCReportIndividualSummary';
            lockUI();
            SocketService.getparams(url, paramcrimetc).then(function (resp) {
                unlockUI();
                $scope.ccreportindividual_list = resp.data.ccreportindividual_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.requestorlist = resp.data.ccrequestordtl;
            });

        }

        function fncamviewdownload() {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnAppCreditUnderWriting/GetCAM';
            lockUI();
           
            SocketService.getparams(url, params).then(function (resp) {  
                if ((resp.data.status == true)) {
                $scope.camuploaddocument_list = resp.data.camdocument_list;
                unlockUI();
            }
            else if (resp.data.status == false)
            unlockUI();
              
            });


            var url = 'api/AgrTrnCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {           
                $scope.requestorlist = resp.data.ccrequestordtl;
                unlockUI();
            });


            var url = "api/AgrTrnCAMGeneration/GetApp2CAM";
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cam_content = resp.data.template_content;
                $scope.lspath = resp.data.lspath;
                $scope.lsname = resp.data.lsname;
                unlockUI();
            });

        }

        function fnmom() {

            var param = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCC/GetAdminPrivilege';
           lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.privilege_gid = resp.data.privilege_gid;
            });
            var url = 'api/AgrTrnApplicationApproval/Getapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {

                $scope.shortclosing_reason = resp.data.shortclosing_reason;
                $scope.expired_flag = resp.data.expired_flag;
            });

            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.momapproval_flag = resp.data.momapproval_flag;
                $scope.cccompleted_flag = resp.data.cccompleted_flag;

            });

            var url = 'api/AgrTrnCC/GetMOMDescription';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.MOMDescription = resp.data.mom_description;
                $scope.momuploaddocument_list = resp.data.momdocument_list;
            });

            var url = 'api/AgrTrnCC/GetApprovalList';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.ccmembers_list = resp.data.ccmembers_list;
                for (var i = 0; i < $scope.ccmembers_list.length; i++) {
                    if ($scope.ccmembers_list[i].ccapproval_flag == 'Y') {
                        $scope.submit_to_approval = true;
                        break;
                    }
                }

            });

            var url = 'api/AgrTrnCC/GetMOMApprovalFlag';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.proceed_flag = resp.data.proceed_flag;
            });

            var url = 'api/AgrTrnCC/GetMOMDescriptions';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mom_descflag = resp.data.mom_descflag;
            });

            var url = 'api/AgrTrnCC/GetMomFlag';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mom_flag = resp.data.mom_flag;
            });

            var url = 'api/AgrTrnCC/GetAdminPrivilege';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.privilege_gid = resp.data.privilege_gid;
            });

            var url = 'api/AgrTrnCC/GetApprovalFlag';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.approval_flag = resp.data.approval_flag;
            });

            var url = 'api/AgrTrnCC/GetApprovalShowFlag';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.approvalshow_flag = resp.data.approvalshow_flag;
            });

            var url = 'api/AgrTrnCC/GetScheduleMeeting';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.ccmember_list = resp.data.ccmember_list;
                $scope.otheruser_list = resp.data.otheruser_list;
            });

            var url = 'api/AgrTrnCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.requestorlist = resp.data.ccrequestordtl;
            });


            var param = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.requestorlist = resp.data.ccrequestordtl;
            });

            var url = 'api/AgrTrnCC/GetMOMReapproval';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mom_reapprove = resp.data.mom_reapprove;
            });
            var url = 'api/AgrTrnCC/GetMOMRemail';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mom_ccmailflag = resp.data.mom_ccmailflag;
            });
        }

        function fnattendance() {

            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCC/GetScheduleMeeting';
           
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblccadmin_name = resp.data.ccadmin_name;
             

            });



            var url = 'api/AgrTrnCC/GetScheduleMeeting';

            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
                //if ((resp.data.status == true)) {
                if ((resp.data.ccmember_list != null && resp.data.ccmember_list.length > 0) || (resp.data.otheruser_list != null && resp.data.otheruser_list.length > 0)) {

                    $scope.ccmember_list = resp.data.ccmember_list;
                    $scope.otheruser_list = resp.data.otheruser_list;
                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
            });



            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrTrnCC/GetCCRequestorlist';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
             
                if ((resp.data.status == true)) {
                    $scope.requestorlist = resp.data.ccrequestordtl;
                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
               
            });

            var url = 'api/AgrTrnCC/GetAdminPrivilege';
            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.privilege_gid = resp.data.privilege_gid;
              
            });
        }

        $scope.onboardappdetailinfo = function (onboard_gid) {
            if(typeof lspage === 'undefined')
                $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsparent=AppSchedulemeet' )); //+ '&lstab='+ lstab + '&lspage='+ lspage
            else
                $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lspage=' + lspage + '&lsparent=AppSchedulemeet')); //+ '&lstab='+ lstab
        }
        $scope.productcomaparisonview = function (onboard_gid, program_gid, product_gid) {
            if (typeof lspage === 'undefined')
                $location.url('app/AgrMstByrProductcomparisonView?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsparent=AppSchedulemeet')); //+ '&lstab='+ lstab + '&lspage='+ lspage

            //$location.url('app/AgrMstByrProductcomparisonView?hash=' + cmnfunctionService.encryptURL("&onboard_gid=" + onboard_gid));
            //    $location.url('app/AgrMstByrProductcomparisonView?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lstab=' + lstab + '&lsparent=AppRMView'));
            else
                $location.url('app/AgrMstByrProductcomparisonView?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&application_gid=' + application_gid + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsparent=AppSchedulemeet' + '&product_gid=' + product_gid + '&program_gid=' + program_gid)); //+ '&lstab='+ lstab + '&lspage='+ lspage

        }
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.camuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.camuploaddocument_list[i].document_path, $scope.camuploaddocument_list[i].document_name);
            }
        }
        $scope.downloadall_2 = function () {
            for (var i = 0; i < $scope.momuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.momuploaddocument_list[i].document_path, $scope.momuploaddocument_list[i].document_name);
            }
        }
        $scope.downloadall_3 = function () {
            for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
            }
        }
        $scope.downloadall_4 = function () {
            for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
            }
        }

    }
})();
