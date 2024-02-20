(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstStartCreditUnderwritingController', MstStartCreditUnderwritingController);

    MstStartCreditUnderwritingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function MstStartCreditUnderwritingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService,cmnfunctionService) {

        var vm = this;
        vm.title = 'MstStartCreditUnderwritingController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.appcreditapproval_gid = $location.search().appcreditapproval_gid;
        var appcreditapproval_gid = $scope.appcreditapproval_gid;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.product_gid = $location.search().product_gid;
        var product_gid = $scope.product_gid;
        $scope.variety_gid = $location.search().variety_gid;
        var variety_gid = $scope.variety_gid;

        const lspagename = 'MstStartCreditUnderwriting';
        const lspagetype = 'Credit';

        lockUI();
        activate();
        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var param = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationEdit/GetAppProductList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstproduct_list = resp.data.mstproduct_list;
            });

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/MstApplicationView/GetApplicationBasicView';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
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
                $scope.lblapproval_status = resp.data.approval_status;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.product_gid = resp.data.product_gid;
                $scope.variety_gid = resp.data.variety_gid;
                $scope.txtproduct_name = resp.data.product_name;
                $scope.txtsector_name = resp.data.sector_name;
                $scope.txtcategory_name = resp.data.category_name;
                $scope.txtvariety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txtprogram_name = resp.data.program_name; 
                $scope.txthypo_flag = resp.data.hypo_flag; 
                if (resp.data.scorecard_submit == "0" || resp.data.scorecard_submit == "") {
                    lockUI();
                    $scope.showsubmitscoreevent = true;
                    var url = 'api/MstCreditMapping/GetCreditScorecarddtl';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.GroupTitle_list = resp.data.GroupTitle_dtl;
                        $scope.GroupQuestion_list = resp.data.MdlCreditGroupTitleQuestion;
                        $scope.grouplistdtl = resp.data.listarray;

                        angular.forEach($scope.GroupQuestion_list, function (value, key) {
                            if (value.creditquestionrule_gid != "") {
                                var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.creditquestionrule_gid === value.creditquestionrule_gid });
                                if (getDropdownListArray != null) {
                                    value.DropdownListArraydtl = getDropdownListArray;
                                }
                            }
                        });

                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            if (value.grouptitle_gid != "") {
                                var getGroupQuestionListArray = $scope.GroupQuestion_list.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                if (getGroupQuestionListArray != null) {
                                    value.GroupQuestion_list = getGroupQuestionListArray;
                                }
                            }
                        });
                    });
                }
                else {
                    $scope.showsubmitscoreevent = false;
                    lockUI();
                    var url = 'api/MstCreditMapping/GetCreditScorecardViewdtl';
                    SocketService.getparams(url, param).then(function (resp) {
                        unlockUI();
                        $scope.GroupTitle_list = resp.data.GroupTitle_dtl;
                        $scope.GroupQuestion_list = resp.data.MdlCreditGroupTitleQuestion;

                        $scope.grouplistdtl = resp.data.listarray;

                        angular.forEach($scope.GroupQuestion_list, function (value, key) {
                            if (value.creditquestionrule_gid != "") {
                                var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.creditquestionrule_gid === value.creditquestionrule_gid });
                                if (getDropdownListArray != null) {
                                    value.DropdownListArraydtl = getDropdownListArray;
                                    value.cbofield_type = value.actualvalue_gid;
                                }
                                value.final_score = (value.answer_type=="Number") ? value.actual_value : value.actual_score;
                                value.field_number = value.actual_value;
                            }
                        });

                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            if (value.grouptitle_gid != "") {
                                var getGroupQuestionListArray = $scope.GroupQuestion_list.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                if (getGroupQuestionListArray != null) {
                                    value.GroupQuestion_list = getGroupQuestionListArray;
                                }
                            }
                        }); 
                    });

                } 
                unlockUI();
            }); 
          

            var url = 'api/MstApplicationView/GetGeneticDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.geneticcode_list = resp.data.geneticdetails_list;
            });

            var url = 'api/MstApplicationView/GetMobileMailDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimary_number = resp.data.primary_mobileno;
                $scope.basicmobileno_list = resp.data.mobilenumber_list;
                $scope.txtprimary_emailassdress = resp.data.primary_email;
                $scope.mailaddress_list = resp.data.mail_list;
            });

            var url = 'api/MstApplicationView/GetProductChargesDtl';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                $scope.lblamountseperator1 = (parseInt($scope.txtoveralllimit_amt.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords1 = defaultamountwordschange($scope.lblamountseperator1);
                $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                $scope.loandtls_list = resp.data.mstLoan_list;
                for (var i = 0; i < $scope.loandtls_list.length; i++) {
                    var lblloanfacility_amount = (parseInt($scope.loandtls_list[i].loanfacility_amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.loandtls_list[i].loanfacility_amountinwords = defaultamountwordschange(lblloanfacility_amount);
                    $scope.loandtls_list[i].lblloanfacility_amount = lblloanfacility_amount;
                }
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
                $scope.lblcsa_applicability = resp.data.csa_applicability;
                $scope.samplecsaactivity_gid = resp.data.csaactivity_gid;
                $scope.lblcsa_activity = resp.data.csaactivity_name;
                $scope.lblpercentageoftotal_limit = resp.data.percentageoftotal_limit;
                    if ($scope.Collateral_list != null) {
                        for (var i = 0; i < $scope.Collateral_list.length; i++) {
                            var lblguideline_value = (parseInt($scope.Collateral_list[i].guideline_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.Collateral_list[i].guideline_valueinwords = defaultamountwordschange(lblguideline_value);
                            $scope.Collateral_list[i].lblguideline_value = lblguideline_value;

                        }

                        for (var i = 0; i < $scope.Collateral_list.length; i++) {
                            var lblmarket_value = (parseInt($scope.Collateral_list[i].market_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.Collateral_list[i].market_valueinwords = defaultamountwordschange(lblmarket_value);
                            $scope.Collateral_list[i].lblmarket_value = lblmarket_value;

                        }

                        for (var i = 0; i < $scope.Collateral_list.length; i++) {
                            var lblforcedsource_value = (parseInt($scope.Collateral_list[i].forcedsource_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.Collateral_list[i].forcedsource_valueinwords = defaultamountwordschange(lblforcedsource_value);
                            $scope.Collateral_list[i].lblforcedsource_value = lblforcedsource_value;

                        }

                        for (var i = 0; i < $scope.Collateral_list.length; i++) {
                            var lblcollateralSSV_value = (parseInt($scope.Collateral_list[i].collateralSSV_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.Collateral_list[i].collateralSSV_valueinwords = defaultamountwordschange(lblcollateralSSV_value);
                            $scope.Collateral_list[i].lblcollateralSSV_value = lblcollateralSSV_value;

                        }
                    }


                    if ($scope.servicecharge_List != null) {

                        for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                            var lblprocessing_fee = (parseInt($scope.servicecharge_List[i].processing_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharge_List[i].processingfeeinwords = defaultamountwordschange(lblprocessing_fee);
                            $scope.servicecharge_List[i].lblprocessing_fee = lblprocessing_fee;

                        }
                        for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                            var lbldoc_charges = (parseInt($scope.servicecharge_List[i].doc_charges.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharge_List[i].doc_chargesinwords = defaultamountwordschange(lbldoc_charges);
                            $scope.servicecharge_List[i].lbldoc_charges = lbldoc_charges;

                        }
                        for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                            var lblfieldvisit_charge = (parseInt($scope.servicecharge_List[i].fieldvisit_charge.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharge_List[i].fieldvisit_chargeinwords = defaultamountwordschange(lblfieldvisit_charge);
                            $scope.servicecharge_List[i].lblfieldvisit_charge = lblfieldvisit_charge;

                        }
                        for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                            var lbladhoc_fee = (parseInt($scope.servicecharge_List[i].adhoc_fee.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharge_List[i].adhoc_feeinwords = defaultamountwordschange(lbladhoc_fee);
                            $scope.servicecharge_List[i].lbladhoc_fee = lbladhoc_fee;

                        }
                        for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                            var lbllife_insurance = (parseInt($scope.servicecharge_List[i].life_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharge_List[i].life_insuranceinwords = defaultamountwordschange(lbllife_insurance);
                            $scope.servicecharge_List[i].lbllife_insurance = lbllife_insurance;

                        }
                        for (var i = 0; i < $scope.servicecharge_List.length; i++) {
                            var lblacct_insurance = (parseInt($scope.servicecharge_List[i].acct_insurance.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                            $scope.servicecharge_List[i].acct_insuranceinwords = defaultamountwordschange(lblacct_insurance);
                            $scope.servicecharge_List[i].lblacct_insurance = lblacct_insurance;

                        }

                    }

                    if ($scope.txtsecurity_value != 'undefined') {
                        $scope.txtsecurity_value = resp.data.security_value;
                        $scope.lblamountseperator = (parseInt($scope.txtsecurity_value.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                        $scope.lblsecurity_value = defaultamountwordschange($scope.lblamountseperator);
                    }
            });

            var url = 'api/MstApplicationView/GetIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditIndividual_List = resp.data.individual_List;
            });

            var url = 'api/MstApplicationView/GetInstitutionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.CreditInstitution_List = resp.data.institution_List;
            });

            var url = 'api/MstApplicationView/GetRMDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtrmdepartment_name = resp.data.department_name;
                $scope.txtrm_name = resp.data.RM_Name;
                $scope.txtappl_initiateddate = resp.data.applicationinitiated_date;
            });
            //Get CAM Document 
            var url = 'api/MstAppCreditUnderWriting/GetCAM';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.camuploaddocument_list = resp.data.camdocument_list;
            });

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/MstCreditApproval/Getcreditheadsview';
            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
                $scope.txtcredit_head = resp.data.credithead_name;
                $scope.txtnational_manager = resp.data.nationalcredit_name;
                $scope.txtregional_manager = resp.data.regionalcredit_name;
                $scope.txtcredit_manager = resp.data.creditmanager_name;
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.remarks = resp.data.remarks;
                unlockUI();
            });

         

           

            var url = 'api/MstCreditApproval/GetAppCreditManagerRejectsendback';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.rejectstatus_flag = resp.data.rejectstatus_flag;

            });

            var url = 'api/MstCreditApproval/GetApprmquerysSummary';
            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
                $scope.appcreditquerylist = resp.data.appcreditquerylist; // check query staus
                unlockUI();
            });


            var url = 'api/MstCreditApproval/GetAppCreditManagerRejectsendback';

            var url = 'api/MstCreditApproval/GetAppqueryStatus';

            var params = {
                application_gid: $scope.application_gid,
                appcreditapproval_gid: appcreditapproval_gid
            }
            var url = 'api/MstCreditApproval/GetAppqueryStatus';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.rejectstatus_flag = resp.data.rejectstatus_flag;

            });


            var params = {
                application_gid: $scope.application_gid,
                appcreditapproval_gid: $scope.appcreditapproval_gid
            }
            var url = 'api/MstCreditApproval/GetAppqueryStatus';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.querystatus_flag = resp.data.querystatus_flag;
                $scope.submitapproval_flag = resp.data.submitapproval_flag;

            var url = 'api/MstCreditApproval/GetAppCreditManagerRejectsendback';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.rejectstatus_flag = resp.data.rejectstatus_flag;

            });

            });


        }

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        
        $scope.Back = function () {
            if (lspage == "myapp") {
                $state.go('app.MstMyApplicationsSummary');
            }
            else if (lspage == "CreditApproval") {
                $state.go('app.MstCreditApprovalSummary');
            }
            else if (lspage == "submittoapp") {
                $state.go('app.MstSubmittedtoApprovalSummary');
            }
            else if (lspage == "submittocc") {
                $state.go('app.MstSubmittedtoCCSummary');
            }
            else if (lspage == "rejecthold") {
                $state.go('app.MstRejectandHoldSummary');
            }
            else {
                $state.go('app.MstMyApplicationsSummary');
            }
        }
        $scope.Kyc_view = function (institution_gid) {
            var application_gid = $scope.application_gid;
            $location.url('app/CreditUnderwritingKycLogView?application_gid=' + application_gid + '&lspage=' + lspage);
        }
        $scope.CAM_generate = function () {
            localStorage.setItem('RefreshTemplate', 'N');
            var application_gid = $scope.application_gid;
           /* $location.url('app/MstCAMGenerate?application_gid=' + application_gid);*/
            $location.url('app/MstCAMGenerate?application_gid=' + application_gid + '&lspage=' + lspage);
        }
        $scope.appcreagradingtool_view = function (application2gradingtool_gid) {
            var application2gradingtool_gid = application2gradingtool_gid;
            localStorage.setItem('application2gradingtool_gid', application2gradingtool_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstApplCreationGradingToolView";
            window.open(URL, '_blank');
        }

        $scope.appcreavisitreport_view = function (visitreport_gid) {
            var visitreport_gid = visitreport_gid;
            localStorage.setItem('visitreport_gid', visitreport_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstApplCreationVisitReportView";
            window.open(URL, '_blank');
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
                var url = 'api/MstApplicationView/GetCollateralDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Collateraldoc_list = resp.data.CollatralDocumentList;

                });
                $scope.documentviewer = function (val1, val2, val3) {
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

                    if (val3 == 'N') {
                        DownloaddocumentService.DocumentViewer(val1, val2);
                    }
                    else {
                        DownloaddocumentService.OtherDocumentViewer(val1, val2, val3);
                    }

                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            
                $scope.downloadallcollateral = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        if ($scope.Collateraldoc_list[i].migration_flag == 'N') {
                            DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
                        }
                        else {
                            DownloaddocumentService.OtherDownloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name, $scope.Collateraldoc_list[i].migration_flag);
                        }
                    }
                }

                $scope.download_Collateraldoc = function (val1, val2, val3) {
                    if (val3 == 'N') {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
                    else {
                        DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
                    }
                }

            }

        }
        $scope.submitto_approval = function () {
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCreditApproval/Getappcreditapprovalinitiate';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    if (lspage == "myapp") {
                        $state.go('app.MstMyApplicationsSummary');
                    }               
                    else {
                        $state.go('app.MstSubmittedtoApprovalSummary');
                    }
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

        $scope.Update = function () {
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/MstCreditApproval/UpdateCreditApproval';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    if (lspage == "CreditApproval") {
                        $state.go('app.MstCreditApprovalSummary');
                    }
                    else {
                        $state.go('app.MstSubmittedtoApprovalSummary');
                    }
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
                      application2hypothecation_gid: application2hypothecation_gid
                  }
                var url = 'api/MstApplicationView/GetHypoDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Hypothecationdoc_list = resp.data.HypoDocumentList;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
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
                $scope.downloadallhypothe = function () {
                    for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
                    }
                }
                $scope.download_Hypothecationdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }


            }

        }

        $scope.PurposeofLoanOther_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoanOther.html',
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
                var url = 'api/MstApplicationView/GetPurposeofLoan';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                });
                var url = 'api/MstApplicationView/GetLoanProgramValueChain';
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
                    $scope.mstproductdtl_list = resp.data.mstproductdtl_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        $scope.Buyer_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/BuyerDetails.html',
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
                var url = 'api/MstApplicationView/GetLoantoBuyerList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.buyer_list = resp.data.mstbuyer_list;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //$scope.group_view = function (group_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/GroupView.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'lg'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {                
        //       var params = {
        //            group_gid : group_gid
        //        }

        //        lockUI();
        //        var url = 'api/MstApplicationEdit/EditGroup';               
        //        SocketService.getparams(url, params).then(function (resp) {
        //        unlockUI();
        //            $scope.txtgroup_name = resp.data.group_name;
        //            $scope.txtdate_formation = resp.data.date_of_formation;
        //            $scope.txtgroup_type = resp.data.group_type;
        //            $scope.txtmember_count = resp.data.groupmember_count;
        //            $scope.txtmember_URN = resp.data.group_urn;
        //            $scope.groupurn_status = resp.data.groupurn_status;                   
        //    });  

        //    var params = {
        //            group_gid : group_gid
        //        }
        //        lockUI();
        //        var url = 'api/MstApplicationEdit/GroupAddressList';               
        //        SocketService.getparams(url, params).then(function (resp) {
        //        unlockUI();
        //            $scope.memberaddress_list = resp.data.mstaddress_list;
        //    });  

        //    var params ={
        //            group_gid : group_gid
        //        }

        //        lockUI();
        //        var url = 'api/MstApplicationEdit/GroupBankList';               
        //        SocketService.getparams(url, params).then(function (resp) {
        //        unlockUI();
        //            $scope.memberbank_list = resp.data.mstbank_list;
        //    });  

        //    var params = {
        //            group_gid : group_gid
        //        }
        //        lockUI();
        //        var url = 'api/MstApplicationEdit/GroupDocumentList';               
        //        SocketService.getparams(url, params).then(function (resp) {
        //        unlockUI();
        //            $scope.UploadMemberDocumentList = resp.data.groupdocument_list;
        //    });  

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.group_docs = function (val1, val2) {
        //            DownloaddocumentService.Downloaddocument(val1, val2);
        //        }

        //    }

        //}

        $scope.group_view = function (group_gid) {
            var group_gid = group_gid;
            localStorage.setItem('group_gid', group_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstApplGroupdtlView";
            window.open(URL, '_blank');
        }

        $scope.member_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstApplGroupMemberdtlView";
            window.open(URL, '_blank');
        }

        var params =
         {
             application_gid: application_gid
         }
        var url = "api/MstApplicationEdit/GetGroupSummary";
        SocketService.getparams(url, params).then(function (resp) {
            $scope.group_list = resp.data.group_list;
            angular.forEach($scope.group_list, function (value, key) {
                var params = {
                    group_gid: value.group_gid
                };

                var url = 'api/MstApplicationView/GetGrouptoMemberList';
                SocketService.getparams(url, params).then(function (resp) {
                    value.groupmember_list = resp.data.groupmember_list;
                    value.expand = false;
                });
            });
        });

        $scope.appcreainstitution_view = function (institution_gid) {
            var institution_gid = institution_gid;
            localStorage.setItem('institution_gid', institution_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstRMInstitutionView";
            window.open(URL, '_blank');
        }

        $scope.appcreaindividual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstRMIndividualView";
            window.open(URL, '_blank');
        }

        $scope.creditinstitution_view = function (institution_gid) {
            var institution_gid = institution_gid;
            localStorage.setItem('institution_gid', institution_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCreditCompanyDtlView?application_gid=" + application_gid + '&institution_gid='+ institution_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.creditindividual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCreditIndividualDtlView?application_gid=" + application_gid + '&contact_gid='+ contact_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.creditgroup_view = function (group_gid) {
            var group_gid = group_gid;
            localStorage.setItem('group_gid', group_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCreditGroupDtlView?application_gid=" + application_gid + '&group_gid='+ group_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.creditmember_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCreditIndividualDtlView?application_gid=" + application_gid + '&contact_gid='+ contact_gid + '&lspage='+ lspage +'&lspagetype='+ lspagetype;
            window.open(URL, '_blank');
        }

        $scope.creditinstitution_add = function (institution_gid) {
            $location.url('app/MstCreditGuaranteeDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage + '&lstype=Institution');
        }

        $scope.creditindividual_add = function (contact_gid) {
            $location.url('app/MstCreditIndividualGuaranteeDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage + '&lstype=Individual');
        }

        $scope.creditgroup_add = function (group_gid) {
            $location.url('app/MstCreditGroupGuaranteeDtlAdd?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage + '&lstype=Group');
        }

        $scope.creditmember_add = function (contact_gid) {
            $location.url('app/MstCreditIndividualGuaranteeDtlAdd?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage + '&lstype=Individual');
        }

        $scope.creditgeneraldtl_edit = function () {
            $location.url('app/MstCreditGeneralDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }

        $scope.applcreainstitution_edit = function (institution_gid) {
            $location.url('app/MstCreditInstitutionDtlEdit?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage );
        }

        $scope.appcreaindividual_edit = function (contact_gid) {
            $location.url('app/MstCreditIndividualDtlEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage);
        }

        $scope.appcreagroup_edit = function (group_gid) {
            $location.url('app/MstCreditGroupDtlEdit?application_gid=' + application_gid + '&group_gid=' + group_gid + '&lspage=' + lspage);
        }

        $scope.appcreamember_edit = function (contact_gid) {
            $location.url('app/MstCreditIndividualDtlEdit?application_gid=' + application_gid + '&contact_gid=' + contact_gid + '&lspage=' + lspage );
        }

        //$scope.productcharges_edit = function () {
        //    $location.url('app/MstApplcreationProductchargesEdit?lsapplication_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        //}

        $scope.productcharges_edit = function () {
            $location.url('app/MstCreditProductChargesDtlEdit?application_gid=' + application_gid + '&lspage=' + lspage + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }


        $scope.hypothecation_edit = function () {
            $location.url('app/MstCreditHypothecationEdit?application_gid=' + application_gid + '&lspage=' + lspage );
        }
        $scope.submit = function () {
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstAppCreditUnderWriting/PostUnderwrite';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lspage == "myapp") {
                        $state.go('app.MstMyApplicationsSummary');
                    }
                    else {
                        $state.go('app.MstSubmittedtoApprovalSummary');
                    }
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
        //CAM Document Upload

        $scope.camdocumentUpload = function (val) {
            if (($scope.txtcamdocument_title == null) || ($scope.txtcamdocument_title == '') || ($scope.txtcamdocument_title == undefined)) {
                $("#momdocument").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            } else {
                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                 

                }
                frm.append('document_title', $scope.txtcamdocument_title);
                frm.append('application_gid', $scope.application_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstAppCreditUnderWriting/CAMocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.camuploaddocument_list = resp.data.camdocument_list;
                        unlockUI();

                        $("#camdocument").val('');
                        $scope.uploadfrm = undefined;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.$parent.txtcamdocument_title = '';
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

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.deleteCAM = function (val) {
            var params = {
                application2camdoc_gid: val,
                application_gid: application_gid
            };

            var url = 'api/MstAppCreditUnderWriting/CAMdoc_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.camuploaddocument_list = resp.data.camdocument_list;

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

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

        $scope.view_Querydescription = function (appcreditquery_gid) {
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
                     appcreditquery_gid: appcreditquery_gid
                 }
                var url = 'api/MstCreditApproval/GetAppcreditqueryesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquery_desc = resp.data.querydesc;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        $scope.create_Query = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addquery.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.query_add = function () {
                    var params = {
                        querytitle: $scope.txtquery_title,
                        querydesc: $scope.txtquery_desc,
                        application_gid: application_gid,
                        appcreditapproval_gid: appcreditapproval_gid,
                        queryraised_to: 'RM'
                    }
                    var url = 'api/MstCreditApproval/PostAppcreditqueryadd';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
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

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.camuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.camuploaddocument_list[i].document_path, $scope.camuploaddocument_list[i].document_name);
            }
        }

        $scope.changefieldtype = function (index, cbofield_type, DropdownListArray,questionindex) {
            var sum = 0;
            angular.forEach($scope.GroupTitle_list[index].GroupQuestion_list, function (value, key) {
                if (value.creditquestionrule_gid == cbofield_type.creditquestionrule_gid) {
                    if (cbofield_type.Score == 'Rejected') {
                        value.DropdownListArray = cbofield_type;
                        value.final_score = "0";
                        if (value.answer_type == "List")
                            value.Score = "Rejected";
                        if (value.addfinal_score == "Yes")
                            value.final_scoredisplay = "0";
                    }
                    else {
                        value.final_score = cbofield_type.Score;
                        value.DropdownListArray = [];
                        value.DropdownListArray.push(cbofield_type);
                        if (value.addfinal_score == "Yes")
                            value.final_scoredisplay = cbofield_type.Score;
                        if (value.answer_type == "List")
                            value.Score = cbofield_type.Score;
                    }
                   
                }
                if (value.final_scoredisplay && value.final_scoredisplay != "")
                    sum += parseFloat(value.final_scoredisplay);
            });
            $scope.GroupTitle_list[index].final_scoredisplay = sum;
            var getCalculateDtl = $scope.GroupTitle_list[index].GroupQuestion_list.filter(function (el) { return el.creditquestionrule_gid === cbofield_type.creditquestionrule_gid });
            var lscreditquestionrule_gid = $scope.GroupTitle_list[index].GroupQuestion_list[questionindex].creditquestionrule_gid;
            if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                var params = {
                    creditquestionrule_gid: lscreditquestionrule_gid,
                    GroupTitle_list: $scope.GroupTitle_list,
                    grouptitle_gid: getCalculateDtl[0].grouptitle_gid, 
                }
                var url = 'api/MstCreditMapping/GetCreditQuestionScore';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data != null) {
                        $scope.getreturndata = resp.data;
                        angular.forEach($scope.GroupTitle_list, function (value, key) { 
                            angular.forEach(value.GroupQuestion_list, function (value1, key1) {
                                var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.creditquestionrule_gid === value1.creditquestionrule_gid });
                                if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                    value1.final_score = getCalculateDtl[0].question_score;
                                    if (value1.addfinal_score == "Yes")
                                        value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                    value1.field_number = getCalculateDtl[0].question_score;
                                }
                            }); 
                        });

                        var sum = 0;
                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                             sum = 0;
                            angular.forEach(value.GroupQuestion_list, function (value1, key1) {
                                if (value1.final_scoredisplay && value1.final_scoredisplay != "")
                                    sum += parseFloat(value1.final_scoredisplay);
                            });
                            value.final_scoredisplay = parseFloat(sum).toFixed(2);
                        });
                        
                    }
                });
            }



        }

        $scope.ChangeNumberField = function (index, field_number, questionruleindex) {
            var creditquestionrule_gid = $scope.GroupTitle_list[index].GroupQuestion_list[questionruleindex].creditquestionrule_gid;
            var sum = 0;
            angular.forEach($scope.GroupTitle_list[index].GroupQuestion_list, function (value, key) {

                if (value.creditquestionrule_gid == creditquestionrule_gid) {
                    value.actual_number = field_number;
                    value.final_score = field_number;
                    if (value.addfinal_score == "Yes")
                        value.final_scoredisplay = field_number;
                    if (value.answer_type == "List")
                        value.Score = field_number;
                }
                if (value.final_scoredisplay && value.final_scoredisplay != "")
                    sum += parseFloat(value.final_scoredisplay);
            });
            $scope.GroupTitle_list[index].final_scoredisplay = sum;
            // if (field_number != "") {
                var getCalculateDtl = $scope.GroupTitle_list[index].GroupQuestion_list.filter(function (el) { return el.creditquestionrule_gid === creditquestionrule_gid });

                if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                    var params = {
                        creditquestionrule_gid: creditquestionrule_gid,
                        GroupTitle_list: $scope.GroupTitle_list,
                        grouptitle_gid: getCalculateDtl[0].grouptitle_gid
                    }
                    var url = 'api/MstCreditMapping/GetCreditQuestionScore';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data != null) {
                            $scope.getreturndata = resp.data;
                            angular.forEach($scope.GroupTitle_list, function (value, key) {
                                angular.forEach(value.GroupQuestion_list, function (value1, key1) {
                                    var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.creditquestionrule_gid === value1.creditquestionrule_gid });
                                    if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                        value1.final_score = getCalculateDtl[0].question_score;
                                        if (value1.addfinal_score == "Yes")
                                            value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                        value1.field_number = getCalculateDtl[0].question_score;
                                    }
                                });
                            });

                            var sum = 0;
                            angular.forEach($scope.GroupTitle_list, function (value, key) {
                                sum = 0;
                                angular.forEach(value.GroupQuestion_list, function (value1, key1) {
                                    if (value1.final_scoredisplay && value1.final_scoredisplay != "")
                                        sum += parseFloat(value1.final_scoredisplay);
                                });
                                value.final_scoredisplay = parseFloat(sum).toFixed(2);
                            });

                        } 
                    });
                }
            // }
            

        }


        $scope.changefieldtypeEdit = function (index, cbofield_type, DropdownListArray, questionindex) {
            $scope.btnupdateshow = true;
            var sum = 0;
            cbofield_type = DropdownListArray.filter(function (el) { return el.questionlistoption_gid === cbofield_type });
            angular.forEach($scope.GroupTitle_list[index].GroupQuestion_list, function (value, key) {
                if (value.creditquestionrule_gid == cbofield_type[0].creditquestionrule_gid) {
                    if (cbofield_type[0].Score == 'Rejected') {
                        value.DropdownListArray = cbofield_type;
                        value.final_score = "0";
                        if (value.answer_type == "List") {
                            value.actual_score = "Rejected";
                            value.Score = "Rejected";
                        } 
                        if (value.addfinal_score == "Yes")
                            value.final_scoredisplay = "0";
                    }
                    else {
                        value.final_score = cbofield_type[0].Score;
                        value.DropdownListArray = [];
                        value.DropdownListArray.push(cbofield_type[0]);
                        if (value.addfinal_score == "Yes")
                            value.final_scoredisplay = cbofield_type[0].Score;
                        if (value.answer_type == "List") {
                            value.actual_score = cbofield_type[0].Score;
                            value.Score = cbofield_type[0].Score;
                        } 
                    }

                }
                else {
                    if (value.cbofield_type != "" && value.cbofield_type.length != 0) {
                        value.DropdownListArray = [];
                        var getlist = value.DropdownListArraydtl.filter(function (el) { return el.questionlistoption_gid === value.cbofield_type });
                        value.DropdownListArray = getlist;
                    }
                }
                if (value.final_scoredisplay && value.final_scoredisplay != "")
                    sum += parseFloat(value.final_scoredisplay);
            });
            $scope.GroupTitle_list[index].final_scoredisplay = sum;
            var getCalculateDtl = $scope.GroupTitle_list[index].GroupQuestion_list.filter(function (el) { return el.creditquestionrule_gid === cbofield_type[0].creditquestionrule_gid });
            var lscreditquestionrule_gid = $scope.GroupTitle_list[index].GroupQuestion_list[questionindex].creditquestionrule_gid;
            if (getCalculateDtl != null && getCalculateDtl.length != 0) { 
                var params = {
                    creditquestionrule_gid: lscreditquestionrule_gid,
                    GroupTitle_list: $scope.GroupTitle_list,
                    grouptitle_gid: getCalculateDtl[0].grouptitle_gid,
                }
                var url = 'api/MstCreditMapping/GetCreditQuestionScore';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data != null) {
                        $scope.getreturndata = resp.data;
                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            angular.forEach(value.GroupQuestion_list, function (value1, key1) {
                                var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.creditquestionrule_gid === value1.creditquestionrule_gid });
                                if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                    value1.final_score = getCalculateDtl[0].question_score;
                                    if (value1.addfinal_score == "Yes")
                                        value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                    value1.field_number = getCalculateDtl[0].question_score;
                                }
                            });
                        });

                        var sum = 0;
                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            sum = 0;
                            angular.forEach(value.GroupQuestion_list, function (value1, key1) {
                                if (value1.final_scoredisplay && value1.final_scoredisplay != "")
                                    sum += parseFloat(value1.final_scoredisplay);
                            });
                            value.final_scoredisplay = parseFloat(sum).toFixed(2);
                        });

                    }
                });
            }



        }

        $scope.ChangeNumberFieldEdit = function (index, field_number, questionruleindex) {
            $scope.btnupdateshow = true;
            var creditquestionrule_gid = $scope.GroupTitle_list[index].GroupQuestion_list[questionruleindex].creditquestionrule_gid;
            var sum = 0;
            angular.forEach($scope.GroupTitle_list[index].GroupQuestion_list, function (value, key) {

                if (value.creditquestionrule_gid == creditquestionrule_gid) {
                    value.actual_number = field_number;
                    value.final_score = field_number;
                    if (value.addfinal_score == "Yes")
                        value.final_scoredisplay = field_number;
                    if (value.answer_type == "List") {
                        value.actual_score = field_number;
                        value.Score = field_number;
                    } 
                }
                if (value.final_scoredisplay && value.final_scoredisplay != "")
                    sum += parseFloat(value.final_scoredisplay);
            });
            $scope.GroupTitle_list[index].final_scoredisplay = sum;
            // if (field_number != "") {
                var getCalculateDtl = $scope.GroupTitle_list[index].GroupQuestion_list.filter(function (el) { return el.creditquestionrule_gid === creditquestionrule_gid });

                if (getCalculateDtl != null && getCalculateDtl.length != 0) {
                    var params = {
                        creditquestionrule_gid: creditquestionrule_gid,
                        GroupTitle_list: $scope.GroupTitle_list,
                        grouptitle_gid: getCalculateDtl[0].grouptitle_gid
                    }
                    var url = 'api/MstCreditMapping/GetCreditQuestionScore';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data != null) {
                            $scope.getreturndata = resp.data;
                            angular.forEach($scope.GroupTitle_list, function (value, key) {
                                angular.forEach(value.GroupQuestion_list, function (value1, key1) {
                                    var getCalculateDtl = $scope.getreturndata.filter(function (el) { return el.creditquestionrule_gid === value1.creditquestionrule_gid });
                                    if (getCalculateDtl.length != 0 && getCalculateDtl != null) {
                                        value1.final_score = getCalculateDtl[0].question_score;
                                        if (value1.addfinal_score == "Yes")
                                            value1.final_scoredisplay = getCalculateDtl[0].question_score;
                                        value1.field_number = getCalculateDtl[0].question_score;
                                    }
                                });
                            });

                            var sum = 0;
                            angular.forEach($scope.GroupTitle_list, function (value, key) {
                                sum = 0;
                                angular.forEach(value.GroupQuestion_list, function (value1, key1) {
                                    if (value1.final_scoredisplay && value1.final_scoredisplay != "")
                                        sum += parseFloat(value1.final_scoredisplay);
                                });
                                value.final_scoredisplay = parseFloat(sum).toFixed(2);
                            });

                        }
                    });
                }
            // }


        }

        $scope.saveScoreCard = function () {
            angular.forEach($scope.GroupTitle_list, function (value, key) {
                var getgroupquestion = $scope.GroupTitle_list[key].GroupQuestion_list;
                angular.forEach(getgroupquestion, function (value, key) {
                    if (value.cbofield_type != "" && value.cbofield_type.length != 0) {
                        value.DropdownListArray = [];
                        var getlist = value.DropdownListArraydtl.filter(function (el) { return el.questionlistoption_gid === value.cbofield_type });
                        value.DropdownListArray = getlist;
                    }  
                    value.Score = value.actual_score;
                });
            });
              
            var params = {
                application_gid: application_gid,
                GroupTitle_list: $scope.GroupTitle_list,
            }
            var url = 'api/MstCreditMapping/SaveScoreCard';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.btnupdateshow = false;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.showsubmitscoreevent = false;
                    var param = {
                        application_gid: $scope.application_gid
                    }
                    lockUI();
                    var url = 'api/MstCreditMapping/GetCreditScorecardViewdtl';
                    SocketService.getparams(url, param).then(function (resp) {
                        unlockUI();
                        $scope.GroupTitle_list = resp.data.GroupTitle_dtl;
                        $scope.GroupQuestion_list = resp.data.MdlCreditGroupTitleQuestion;
 						$scope.grouplistdtl = resp.data.listarray;

                        angular.forEach($scope.GroupQuestion_list, function (value, key) {
                            if (value.creditquestionrule_gid != "") {
                                var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.creditquestionrule_gid === value.creditquestionrule_gid });
                                if (getDropdownListArray != null) {
                                    value.DropdownListArraydtl = getDropdownListArray;
                                    value.cbofield_type = value.actualvalue_gid;
                                }

                                value.field_number = value.actual_value;
                            }
                        });

                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            if (value.grouptitle_gid != "") {
                                var getGroupQuestionListArray = $scope.GroupQuestion_list.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                if (getGroupQuestionListArray != null) {
                                    value.GroupQuestion_list = getGroupQuestionListArray;
                                }
                            }
                        });
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

        $scope.submitScoreCard = function () {
            var params = {
                application_gid: application_gid,
                GroupTitle_list: $scope.GroupTitle_list, 
            }
            var url = 'api/MstCreditMapping/SubmitScoreCard';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.showsubmitscoreevent = false;
                    var param = {
                        application_gid: $scope.application_gid
                    }
                    lockUI();
                    var url = 'api/MstCreditMapping/GetCreditScorecardViewdtl';
                    SocketService.getparams(url, param).then(function (resp) {
                        unlockUI();
                        $scope.GroupTitle_list = resp.data.GroupTitle_dtl;
                        $scope.GroupQuestion_list = resp.data.MdlCreditGroupTitleQuestion;
                        $scope.grouplistdtl = resp.data.listarray;

                        angular.forEach($scope.GroupQuestion_list, function (value, key) {
                            if (value.creditquestionrule_gid != "") {
                                var getDropdownListArray = $scope.grouplistdtl.filter(function (el) { return el.creditquestionrule_gid === value.creditquestionrule_gid });
                                if (getDropdownListArray != null) {
                                    value.DropdownListArraydtl = getDropdownListArray;
                                    value.cbofield_type = value.actualvalue_gid;
                                }

                                value.field_number = value.actual_value;
                            }
                        });

                        angular.forEach($scope.GroupTitle_list, function (value, key) {
                            if (value.grouptitle_gid != "") {
                                var getGroupQuestionListArray = $scope.GroupQuestion_list.filter(function (el) { return el.grouptitle_gid === value.grouptitle_gid });
                                if (getGroupQuestionListArray != null) {
                                    value.GroupQuestion_list = getGroupQuestionListArray;
                                }
                            }
                        });
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

        $scope.editRule_Company = function (application_gid,institution_gid,vertical_gid) {
            $location.url('app/MstVerticalBRETrnRule?lsinstitution_gid=' + institution_gid + '&lsvertical_gid=' + vertical_gid + '&lsapplication_gid=' + application_gid + '&lscompany=' + "Company" + '&lspage=' + lspage + '&lspagename=' + lspagename); //+ '&lscompany=' + "Company"
        }

        $scope.editRule_Individual = function (application_gid,contact_gid,vertical_gid) {
            $location.url('app/MstVerticalBRETrnRule?lscontact_gid=' + contact_gid + '&lsvertical_gid=' + vertical_gid + '&lsapplication_gid=' + application_gid + '&lsindividual=' + "Individual" + '&lspage=' + lspage + '&lspagename=' + lspagename); // + '&lsindividual=' + "Individual"
        }

        $scope.editRule_Group = function (application_gid,group_gid,vertical_gid) {
            $location.url('app/MstVerticalBRETrnRule?lsgroup_gid=' + group_gid + '&lsvertical_gid=' + vertical_gid + '&lsapplication_gid=' + application_gid + '&lsgroup=' + "Group" + '&lspage=' + lspage + '&lspagename=' + lspagename); // + '&lsgroup=' + "Group"
        }
        $scope.limit_management = function (val, customer_urn) {
            $location.url('app/MstLimitManagementView?application_gid=' + val + '&customer_urn=' + customer_urn + '&lspage=' + lspage);
        }
     
        $scope.loandetails = function (customer_urn) {
            $location.url('app/MstRMLoanDetailsDtls?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&customer_urn=' + customer_urn + '&lspage=' + lspage +'&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }


        // Credit Manager Reject 
       
        $scope.CreditManagerReject = function () {

            var params = {
                application_gid: application_gid,
            }
            
            var url = 'api/MstCreditApproval/GetApprmquerysSummary';
            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
                $scope.appcreditquerylist = resp.data.appcreditquerylist; // check query staus
                // $scope.application_no = resp.data.application_no; // check txtapplication_no
                unlockUI();
            });

            if($scope.appcreditquerylist!=undefined && $scope.appcreditquerylist!='' && $scope.appcreditquerylist!=null){
                const querylength = $scope.appcreditquerylist.length; // check query 
                if (querylength > 0){
                    for (i=0;i<querylength;i++){
                        if ($scope.appcreditquerylist[i].querystatus == "Open"){
                            //$modalInstance.close('closed');
                            Notify.alert('Kindly Close All Queries before Reject', 'warning', 2000);
                            //StyleSheet.alert(z-index)
                            return false;
                        }
                    }
                }
            }
            
            var modalInstance = $modal.open({
                templateUrl: '/creditmanagerreject.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submit = function () {
                    
                    var url = 'api/MstCreditApproval/PostAppCreditManagerReject';
                    var params = {
                        application_gid: application_gid,
                        //application_no: application_no,
                        approval_remarks : $scope.txtcloseremarks,
                        approval_status : 'Rejected',
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            $state.go('app.MstMyApplicationsSummary');
                            //activate();
                            
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                    });
                    //$modalInstance.close('closed');
                }
            
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


        $scope.Deleteindividual = function (contact_gid) {
            var params = {
                contact_gid: contact_gid
            }
            var url = 'api/MstApplicationAdd/Deleteindividual';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    /* $scope.individual_list = resp.data.cicindividual_list;*/
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //var url = 'api/MstApplicationAdd/GetIndividualSummary';
                    //SocketService.get(url).then(function (resp) {
                    //    $scope.individual_list = resp.data.cicindividual_list;
                    //});
                    activate();
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
        $scope.DeleteInstitution = function (institution_gid, application_gid) {
            var params = {
                institution_gid: institution_gid,
                application_gid: application_gid
            }
            var url = 'api/MstApplicationEdit/Deleteinstitution';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    //$scope.institution_list = resp.data.cicinstitution_list;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //var url = 'api/MstApplicationAdd/GetCICInstitutionSummary';
                    //SocketService.get(url).then(function (resp) {
                    //    $scope.institution_list = resp.data.cicinstitution_list;
                    //});
                    activate();

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
    }
})();
