(function () {
    'use strict';
    angular
        .module('angle')
        .controller('MstCcCommitteeApplicationViewController', MstCcCommitteeApplicationViewController);

    MstCcCommitteeApplicationViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService','cmnfunctionService'];

    function MstCcCommitteeApplicationViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCcCommitteeApplicationViewController';

        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;

        const lspagename = 'MstCcCommitteeApplicationView';

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
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.txtproduct_name = resp.data.product_name;
                $scope.txtsector_name = resp.data.sector_name;
                $scope.txtcategory_name = resp.data.category_name;
                $scope.txtvariety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txtprogram_name = resp.data.program_name;
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
                $scope.lbloverallamountseperator = (parseInt($scope.txtoveralllimit_amt.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lbloverallamountwords = defaultamountwordschange($scope.lbloverallamountseperator);
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

                $scope.txtsecurityassessed_date = resp.data.securityassessed_date;
                $scope.txtasset_id = resp.data.asset_id;
                $scope.txtroc_fillingid = resp.data.roc_fillingid;
                $scope.txtCERSAI_fillingid = resp.data.CERSAI_fillingid;
                $scope.txthypoobservation_summary = resp.data.hypoobservation_summary;
                $scope.txtprimary_security = resp.data.primary_security;
                $scope.application2hypothecation_gid = resp.data.application2hypothecation_gid;
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

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'

            }
            var url = 'api/MstApplicationView/GetVisitReportList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReport_List = resp.data.VisitReport_List;
            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'
            }
            var url = 'api/MstApplicationView/GetGradingToolDtls';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.gradetoolsummary_list = resp.data.mstgradetoolsummary_list;
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
                $scope.txtunderwritten_date = resp.data.creditunderwritten_date;
                $scope.txtunderwritten_by = resp.data.creditunderwritten_by;
            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'Credit',
            }
            var url = 'api/MstApplicationGradingTool/GetGradingTool';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.gradingtoolcredit_list = resp.data.grading_list;

            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'Credit',
            }
            var url = 'api/MstApplicationVisitReport/GetVisitReportList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReportCreditList = resp.data.VisitReportList;

            });

            //Get CAM Document
            var url = 'api/MstAppCreditUnderWriting/GetCAM';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.camuploaddocument_list = resp.data.camdocument_list;
            });

            var params = {
                application_gid: application_gid
            }
            var url = "api/MstCAMGeneration/GetApp2CAM"
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lspath = resp.data.lspath;
                $scope.lsname = resp.data.lsname;
            });

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

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
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

                $scope.download_Hypothecationdoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
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
                $scope.downloadallhyp = function () {
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
                var url = 'api/MstApplicationView/GetCollateralDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Collateraldoc_list = resp.data.CollatralDocumentList;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
                $scope.download_Collateraldoc = function (val1, val2, val3) {
                    if (val3 == 'N') {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
                    else {
                        DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
                    }
                }

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

                $scope.downloadallcol = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        if ($scope.Collateraldoc_list[i].migration_flag == 'N') {
                            DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
                        }
                        else {
                            DownloaddocumentService.OtherDownloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name, $scope.Collateraldoc_list[i].migration_flag);
                        }
                    }
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

        $scope.Back = function () {
            if (lspage == 'ScheduleMeeting') {
                $location.url('app/MstCreditCommitteeSummary');
            }
            else if (lspage == 'ScheduledMeetingsummary') {
                $state.go('app.MstCCscheduledSummary');
            }
        
            else if (lspage == 'CompletedMeetingsummary') {
                $state.go('app.MstCCCompletedSummary');
            }
            else if (lspage == 'CCMmeetingScheduledcompleted') {
                $state.go('app.MstCcCompletedScheduledMeeting');
            }
            else if (lspage == 'RejectHoldAppl') {
                $state.go('app.MstRejectandHoldSummary');
            }
            else if (lstab == 'CCSkippedAppl') {
                $state.go('app.MstCCSkippedApplicationSummary');
            }
            else if (lspage == 'SubmittedToApproval') {
                $state.go('app.MstSubmittedtoApprovalSummary');
            }
            else if (lspage == 'SubmittedToCC') {
                $state.go('app.MstSubmittedtoCCSummary');
            }
            else if (lspage == 'CreditApproval') {
                $state.go('app.MstCreditApprovalSummary');
            }
            else if (lspage == 'CreditApproved') {
                $state.go('app.MstCreditApprovedSummary');
            }
            else if (lspage == 'CreditSubmittedtoCC') {
                $state.go('app.MstCreditSubmittedtoCCSummary');
            }
            else if (lstab == 'CreditCCSkipped') {
                $state.go('app.MstCreditCCSkippedSummary');
            }
            else if (lspage == 'CreditRejectHold') {
                $state.go('app.MstCreditRejectandHoldSummary');
            }
            else if (lstab == 'Pencreditmapping') {
                $state.go('app.MstApplicationAssignmentSummary');
            }
            else if (lstab == 'Asscreditmapping') {
                $state.go('app.MstAppassignedAssignmentSummary');
            }
            else if (lstab == 'ApplSubmittedToCC') {
                $state.go('app.MstApplSubmittedtoCCSummary');
            }
            else if (lspage == 'SentBackToCredit') {
                $state.go('app.MstSentbackcctoCredit');
            }
            else if (lspage == 'UpcomingCreditApproval') {
                $state.go('app.MstUpcomingCreditApprovalSummary');
            }
            else if (lspage == 'CreditRejectRevokeAppl') {
                $state.go('app.MstCreditRevokeSummary');
            }
            else if (lspage == 'CreditHoldRevokeAppl') {
                $state.go('app.MstCreditHoldRevokeSummary');
            }
            else if (lspage == 'CreditRevokedAppl') {
                $state.go('app.MstCreditRevokedApplSummary');
            }
            else if (lstab == 'CreditStage') {
                $state.go('app.MstCreditStageSummary');
            }
            else if (lstab == 'CcStage') {
                $state.go('app.MstCcStageSummary');
            }
            else if (lstab == 'CCApprovedVerification') {
                $state.go('app.MstColendingCCApprovedVerifySummary');
            }
            else if (lstab == 'finalverified') {
                $state.go('app.MstCCFinalApprovedVerifiedSummary');
            }
            else if (lspage == 'CCApproved') {
                $state.go('app.MstCCApprovedSummary');
            }
            else if (lspage == 'CreditManagerRejectRevokeAppl') {
                $state.go('app.MstCreditManagerRejectRevokeSummary');
            }
            else {
                $state.go('app.MstCcScheduledMeetingSummary');
            }
        }

        $scope.Kyc_view = function () {
            var application_gid = $scope.application_gid;
            var lspage = $scope.lspage;
            $location.url('app/MstCcCommitteeKycView?application_gid=' + application_gid + '&lspage=' + lspage);
        }

        $scope.scorecard_view = function () {
            var application_gid = $scope.application_gid;
            var lspage = $scope.lspage;
            $location.url('app/MstAppScoreCardViewdtl?application_gid=' + application_gid + '&lspage=' + lspage);
        }

        $scope.gradingtool_view = function (application2gradingtool_gid) {
            var application2gradingtool_gid = application2gradingtool_gid;
            localStorage.setItem('application2gradingtool_gid', application2gradingtool_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstApplCreationGradingToolView";
            window.open(URL, '_blank');
        }

        $scope.visitreport_view = function (visitreport_gid) {
            var visitreport_gid = visitreport_gid;
            localStorage.setItem('visitreport_gid', visitreport_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstApplCreationVisitReportView";
            window.open(URL, '_blank');
        }

        $scope.institution_view = function (institution_gid) {
            var institution_gid = institution_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('institution_gid', institution_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCcCommitteeInstitutionView?application_gid=" + application_gid + '&institution_gid='+ institution_gid + '&lspage='+ lspage +'&lspagetype='+'CC';
            window.open(URL, '_blank');
        }

        $scope.individual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('contact_gid', contact_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCcCommitteeIndividualView?application_gid=" + application_gid + '&contact_gid='+ contact_gid + '&lspage='+ lspage +'&lspagetype='+'CC';
            window.open(URL, '_blank');
        }

        $scope.group_view = function (group_gid) {
            var group_gid = group_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('group_gid', group_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCcCommitteeGroupView?application_gid=" + application_gid + '&group_gid='+ group_gid + '&lspage='+ lspage +'&lspagetype='+'CC';
            window.open(URL, '_blank');
        }

        $scope.member_view = function (contact_gid) {
            var contact_gid = contact_gid;
            var application_gid = $scope.application_gid;
            localStorage.setItem('contact_gid', contact_gid);
            localStorage.setItem('application_gid', application_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstCcCommitteeIndividualView?application_gid=" + application_gid + '&contact_gid='+ contact_gid + '&lspage='+ lspage +'&lspagetype='+'CAD Pending';
            window.open(URL, '_blank');
        }

        $scope.downloadsdocument = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("EMS");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //var name = val2.split(".")
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.camuploaddocument_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.camuploaddocument_list[i].document_path, $scope.camuploaddocument_list[i].document_name);
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

        $scope.editRule_Company = function (application_gid,institution_gid,vertical_gid) {
            $location.url('app/MstVerticalBRETrnRule?lsinstitution_gid=' + institution_gid + '&lsvertical_gid=' + vertical_gid + '&lsapplication_gid=' + application_gid + '&lscompany=' + "Company" + '&lspage=' + lspage + '&lspagename=' + lspagename); //+ '&lscompany=' + "Company"
        }

        $scope.editRule_Individual = function (application_gid,contact_gid,vertical_gid) {
            $location.url('app/MstVerticalBRETrnRule?lscontact_gid=' + contact_gid + '&lsvertical_gid=' + vertical_gid + '&lsapplication_gid=' + application_gid + '&lsindividual=' + "Individual" + '&lspage=' + lspage + '&lspagename=' + lspagename); // + '&lsindividual=' + "Individual"
        }
    }
})();
