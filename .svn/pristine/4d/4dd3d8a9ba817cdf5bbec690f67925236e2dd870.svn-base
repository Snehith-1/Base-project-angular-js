(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplicationCreationViewController', MstApplicationCreationViewController);

    MstApplicationCreationViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstApplicationCreationViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplicationCreationViewController';

        $scope.application_gid = $location.search().application_gid;
        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;
        var application_gid = $scope.application_gid;
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

            var url = 'api/MstApplicationView/GetBorrowerInstitutionView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcompany_name = resp.data.company_name;
                $scope.txtCIN_number = resp.data.cin_no;
                $scope.txtcompanyPAN_number = resp.data.companypan_no;
                $scope.txtincorporation_date = resp.data.date_incorporation;
                $scope.txtbusiness_year = resp.data.year_business;
                $scope.txtbusiness_month = resp.data.month_business;
                $scope.txtcompany_type = resp.data.companytype_name;
                $scope.txtescrow = resp.data.escrow;
                $scope.txtlastyear_turnover = resp.data.lastyear_turnover;
                $scope.lbllastyearamountseperator = (parseInt($scope.txtlastyear_turnover.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lbllastyearamountwords = defaultamountwordschange($scope.lbllastyearamountseperator);
                $scope.txtstart_date = resp.data.start_date;
                $scope.txtend_date = resp.data.end_date;
                $scope.txtofficial_teleno = resp.data.official_telephoneno;
                $scope.txtofficial_mailaddress = resp.data.officialemail_address;
                $scope.gst_list = resp.data.mstgst_list;
                $scope.txtcredit_assessmentagency = resp.data.assessmentagency_name;
                $scope.txtassessment_rating = resp.data.assessmentagencyrating_name;
                $scope.txtrating_on = resp.data.ratingas_on;
                $scope.txtAML_category = resp.data.amlcategory_name;
                $scope.txtbusiness_category = resp.data.businesscategory_name;
                $scope.txtinstituionprimary_number = resp.data.primaryinstitution_mobileno;
                $scope.instituionmobile_list = resp.data.instituionmobilenumber_list;
                $scope.txtinstituionprimary_emailaddress = resp.data.primaryinstitution_email;
                $scope.instituionmailaddress_list = resp.data.mail_list;
                $scope.instituionaddress_list = resp.data.mstaddress_list;
                $scope.institutionform60_list = resp.data.institutionform60_list;
                $scope.institutiondoc_list = resp.data.institutiondoc_list;
                $scope.mstlicense_list = resp.data.mstlicense_list;
                $scope.bureauname_gid = resp.data.bureauname_gid;
                $scope.txbureau_name = resp.data.bureauname_name;
                $scope.txtbureau_score = resp.data.bureau_score;
                $scope.txtscore_on = resp.data.bureau_response;
                $scope.txtobservations = resp.data.observations;
                $scope.txtbureau_response = resp.data.bureauscore_date;
                $scope.cicdocument_name = resp.data.cicdocument_name;
                $scope.cicdocument_path = resp.data.cicdocument_path;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txturn = resp.data.urn;
                $scope.txtcontact_firstname = resp.data.contactperson_firstname;
                $scope.txtcontact_middlename = resp.data.contactperson_middlename;
                $scope.txtcontact_lastname = resp.data.contactperson_lastname;
                $scope.txtdesignation = resp.data.designation;
                $scope.txtbusinessstart_date = resp.data.businessstart_date;
                $scope.borrowerinstitution_gid = resp.data.institution_gid;
                $scope.txtnearsamunnatiabranch_gid = resp.data.nearsamunnatiabranch_gid;
                $scope.txtnearsamunnati_branch = resp.data.nearsamunnatiabranch_name;
                $scope.txtudhayam_registration = resp.data.udhayam_registration;
                $scope.txttan_number = resp.data.tan_number;
                $scope.txtbusiness_description = resp.data.business_description;
                $scope.txttanstate_gid = resp.data.tanstate_gid;
                $scope.txttan_state = resp.data.tanstate_name;
                $scope.txtinternalrating_gid = resp.data.internalrating_gid;
                $scope.txtinternal_rating = resp.data.internalrating_name;
                $scope.txtsales = resp.data.sales;
                $scope.txtpurchase = resp.data.purchase;
                $scope.txtcredit_summation = resp.data.credit_summation;
                $scope.txtcheque_bounce = resp.data.cheque_bounce;
                $scope.txtnumberof_boardmeetings = resp.data.numberof_boardmeetings;
                $scope.txtfarmer_count = resp.data.farmer_count;
                $scope.txtcrop_cycle = resp.data.crop_cycle;
                $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;
                $scope.mstequipmentholding_list = resp.data.mstequipmentholding_list;
                $scope.mstreceivable_list = resp.data.mstreceivable_list;
                $scope.city_name = resp.data.city_name;
                $scope.txtcalamities_prone = resp.data.calamities_prone;

                var parambur = {
                    institution_gid: $scope.borrowerinstitution_gid
                }
                var url = 'api/MstApplicationAdd/GetInstitutionBureauList';
                SocketService.getparams(url, parambur).then(function (resp) {
                    $scope.institutionbureau_list = resp.data.institutionbureau_list;
                });

            });

            var url = 'api/MstApplicationView/GetBorrowerIndividualView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcustomer_name = resp.data.individual_name;
                $scope.txtpan_number = resp.data.pan_no;
                $scope.txtaadhar_number = resp.data.aadhar_no;
                var aadhar = $scope.txtaadhar_number;
                var mask = aadhar.slice(-4);
                var maskaadhar = 'XXXX-XXXX-' + mask;
                $scope.individualaadhar_number = maskaadhar;
                $scope.txt_dob = resp.data.individual_dob;
                $scope.txtage = resp.data.age;
                $scope.txtgender = resp.data.gender_name;
                $scope.txtdesignation = resp.data.designation_name;
                $scope.txt_peppoliticallyperson = resp.data.pep_status;
                $scope.txtpep_verifiesdate = resp.data.pepverified_date;
                $scope.txtmarital_status = resp.data.maritalstatus_name;
                $scope.txtfather_name = resp.data.father_name;
                $scope.txtfatherdob_date = resp.data.father_dob;
                $scope.txtfather_age = resp.data.father_age;
                $scope.txtmother_name = resp.data.mother_name;
                $scope.txtmotherdob_date = resp.data.mother_dob;
                $scope.txtmother_age = resp.data.mother_age;
                $scope.txtspouse_name = resp.data.spouse_name;
                $scope.txtspousedob_date = resp.data.spouse_dob;
                $scope.txtspouse_age = resp.data.spouse_age;
                $scope.txtEdu_qualification = resp.data.educationalqualification_name;
                $scope.txtmain_occupation = resp.data.main_occupation;
                $scope.txtannual_income = resp.data.annual_income;
                $scope.lblannual_incomeseperator = (parseInt($scope.txtannual_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblannual_incomewords = defaultamountwordschange($scope.lblannual_incomeseperator);
                $scope.txtmonthly_income = resp.data.monthly_income;
                $scope.lblmonthly_incomeseperator = (parseInt($scope.txtmonthly_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblmonthly_incomewords = defaultamountwordschange($scope.lblmonthly_incomeseperator);
                $scope.txtincome_type = resp.data.incometype_name;
                $scope.txtindividualprimary_number = resp.data.primaryindividual_mobileno;
                $scope.individualmobile_list = resp.data.contactmobileno_list;
                $scope.txtindividualprimary_emailaddress = resp.data.primaryindividual_email;
                $scope.individualmailaddress_list = resp.data.contactemail_list;
                $scope.individualaddress_list = resp.data.contactaddress_list;
                $scope.txtownership_type = resp.data.ownershiptype_name;
                $scope.txtproperty_name = resp.data.propertyholder_name;
                $scope.txtresidence_type = resp.data.residencetype_name;
                $scope.txtyear_currentresidence = resp.data.currentresidence_years;
                $scope.txtdistance = resp.data.branch_distance;
                $scope.individualproof_list = resp.data.contactidproof_list;
                $scope.individualdoc_list = resp.data.uploadindividualdoc_list;
                $scope.txtindividualbureau_name = resp.data.indbureauname_name;
                $scope.txtindividualbureau_score = resp.data.indbureau_score;
                $scope.txtindividualscore_on = resp.data.indbureauscore_date;
                $scope.txtindividualobservations = resp.data.indobservations;
                $scope.txtindividualbureau_response = resp.data.indbureau_response;
                $scope.cicindividualdocument_name = resp.data.indcicdocument_name;
                $scope.cicindividualdocument_path = resp.data.indcicinddocument_path;
                $scope.txtfathernominee_status = resp.data.fathernominee_status;
                $scope.txtmothernominee_status = resp.data.mothernominee_status;
                $scope.txtspousenominee_status = resp.data.spousenominee_status;
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtprofile = resp.data.profile;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txt_urn = resp.data.urn;
                $scope.txtother_nominee = resp.data.othernominee_status;
                $scope.txtrelationship_type = resp.data.relationshiptype;
                $scope.txtnomineedob_date = resp.data.nominee_dob;
                $scope.nomineefirst_name = resp.data.nomineefirst_name;
                $scope.nominee_middlename = resp.data.nominee_middlename;
                $scope.nominee_lastname = resp.data.nominee_lastname;
                $scope.txtnominee_age = resp.data.nominee_age;
                $scope.txttotal_landacres = resp.data.totallandinacres;
                $scope.txtcultivated_land = resp.data.cultivatedland;
                $scope.txtprevious_crop = resp.data.previouscrop;
                $scope.txtproposed_crop = resp.data.prposedcrop;
                $scope.txtinstitution_name = resp.data.institution_name;
                $scope.contactpanabsencereasons_list = resp.data.contactpanabsencereasons_list;
                $scope.txtpan_status = resp.data.pan_status;
                $scope.txtindnearsamunnati_gid = resp.data.nearsamunnatiabranch_gid;
                $scope.txtindnearsamunnati_name = resp.data.nearsamunnatiabranch_name;
                $scope.txtindphysicalstatus_gid = resp.data.physicalstatus_gid;
                $scope.txtindphysical_status = resp.data.physicalstatus_name;
                $scope.txtindinternalrating_gid = resp.data.internalrating_gid;
                $scope.txtindinternal_rating = resp.data.internalrating_name;
                $scope.mstindlivestockholding_list = resp.data.mstlivestockholding_list;
                $scope.mstindequipmentholding_list = resp.data.mstequipmentholding_list;
                
                $scope.borrowercontact_gid = resp.data.contact_gid;

                var parambur = {
                    contact_gid: $scope.borrowercontact_gid
                }
                var url = 'api/MstApplicationAdd/GetContactBureauList';
                SocketService.getparams(url, parambur).then(function (resp) {
                    $scope.contactbureau_list = resp.data.contactbureau_list;
                });

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
                $scope.txttotal_collectible = resp.data.total_collect;
                $scope.txtprocessing_collecttype = resp.data.processing_collectiontype;
                $scope.txtdoc_collecttype = resp.data.doccharge_collectiontype;
                $scope.txtfield_collecttype = resp.data.fieldvisit_collectiontype;
                $scope.txtadhoccollection_type = resp.data.adhoc_collectiontype;
                $scope.txtlife_collectiontype = resp.data.lifeinsurance_collectiontype;
                $scope.txttotal_deductible = resp.data.total_deduct;
                $scope.Collateral_list = resp.data.mstcollateral_list;
                $scope.txtproduct_type = resp.data.product_type;
                $scope.servicecharge_List = resp.data.servicecharge_List;
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
                $scope.txtsecurity_type = resp.data.security_type;
                $scope.txtsecurity_description = resp.data.security_description;
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
            });            

            var url = 'api/MstApplicationView/GetGurantorIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.GurantorIndividual_List = resp.data.GurantorIndividual_List;
            });

            var url = 'api/MstApplicationView/GetGurantorInstitutionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.GurantorInstitution_List = resp.data.GurantorInstitution_List;
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

        }

        //function amountWordsConverter(amount) {
        //    var amountseparator = (parseInt(amount.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
        //    var amountinwords = defaultamountwordschange(amountseparator);
        //    return amountinwords;
        //    return amountseparator;
        //}

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
        }

        // function inWords1(num) {
        //     var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
        //     var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
        //     var s = num.toString();
        //     s = s.replace(/[\, ]/g, '');
        //     if (s != parseFloat(s)) return '';
        //     if ((num = num.toString()).length > 9) return 'Overflow';
        //     var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
        //     if (!n) return; var str = '';
        //     str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
        //     str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
        //     str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
        //     str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

        //     str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
        //     return str;
        // }

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

        $scope.appcreainstitution_view = function (institution_gid) {
            var institution_gid = institution_gid;
            localStorage.setItem('institution_gid', institution_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstApplCreationInstitutionGuarantorView";
            window.open(URL, '_blank');
        }

        $scope.appcreaindividual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/MstApplCreationIndividualGuarantorView";
            window.open(URL, '_blank');
        }

        $scope.Back = function () {
            if (lstab == 'applicationcreation') {
                $location.url('app/MstApplicationCreationSummary');
            }
            else if (lstab == 'creditmapping') {
                $state.go('app.MstApplicationAssignmentSummary');
            }
            else if (lstab == 'BusinessApproval') {
                $state.go('app.MstBusinessApprovalSummary');
            }
            else if (lstab == 'BusinessReject') {
                $state.go('app.MstBusinessRejectedSummary');
            }
            else if (lstab == 'BusinessHold') {
                $state.go('app.MstBusinessHoldSummary');
            }
            else if (lstab == 'BusinessApproved') {
                $state.go('app.MstBusinessApprovedSummary');
            }
            else if (lstab == 'MyApplications') {
                $state.go('app.MstMyApplicationsSummary');
            }
            else if (lstab == 'RejectHoldAppl') {
                $state.go('app.MstRejectandHoldSummary');
            }
            else if (lstab == 'CCSkippedAppl') {
                $state.go('app.MstCCSkippedApplicationSummary');
            }
            else if (lstab == 'SubmittedToApproval') {
                $state.go('app.MstSubmittedtoApprovalSummary');
            }
            else if (lstab == 'SubmittedToCC') {
                $state.go('app.MstSubmittedtoCCSummary');
            }
            else if (lstab == 'CreditApproval') {
                $state.go('app.MstCreditApprovalSummary');
            }
            else if (lstab == 'CreditApproved') {
                $state.go('app.MstCreditApprovedSummary');
            }
            else if (lstab == 'CreditSubmittedtoCC') {
                $state.go('app.MstCreditSubmittedtoCCSummary');
            }
            else if (lstab == 'CreditCCSkipped') {
                $state.go('app.MstCreditCCSkippedSummary');
            }
            else if (lstab == 'CreditRejectHold') {
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
            else if (lstab == 'CCApproved') {
                $state.go('app.MstApplCCApproved');
            }
            else if (lstab == 'UpcomingBusinessApproval') {
                $state.go('app.MstUpcomingBusinessApprovalSummary');
            }
            else if (lstab == 'RejectRevokeApplication') {
                $state.go('app.MstBusinessRevokeSummary');
            }
            else if (lstab == 'HoldRevokeApplication') {
                $state.go('app.MstBusinessHoldRevokeSummary');
            } 
            else if (lstab == 'BusinessRevokedApplication') {
                $state.go('app.MstBusinessRevokedApplSummary');
            }  
            else if (lstab == 'CreditRejectRevokeAppl') {
                $state.go('app.MstCreditRevokeSummary');
            }
            else if (lstab == 'CreditHoldRevokeAppl') {
                $state.go('app.MstCreditHoldRevokeSummary');
            }
            else if (lstab == 'CreditRevokedAppl') {
                $state.go('app.MstCreditRevokedApplSummary');
            }
            else if (lstab == 'BusinessStage') {
                $state.go('app.MstBusinessHierarchyUpdateSummary');
            }
            else if (lstab == 'IncompleteStage') {
                $state.go('app.MstIncompleteStageSummary');
            }
            else if (lstab == 'applicationcreationReject') {
                $state.go('app.MstRejectedApplicationSummary');
            }
            else if (lstab == 'applicationcreationHold') {
                $state.go('app.MstHoldApplicationSummary');
            }
            else if (lstab == 'applicationcreationCcApproved') {
                $state.go('app.MstApprovedApplicationSummary');
            }
            else if (lstab == 'RejectedMyApplication') {
                $state.go('app.MstApplicationAssigRejectSummary');
            }
            else if (lstab == 'CreditPendingVerification') {
                $state.go('app.MstColendingVerificationSummary');
            }
            else if (lstab == 'CreditCompletedVerification') {
                $state.go('app.MstColendingVerifyCompletedSummary');
            }
            else {

            }

        }

        $scope.bureaudoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.institutiondoc_downloads = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            }
        }

        $scope.form60_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.individualproof_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.individualdoc_downloads = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            }
        }

        $scope.individualbureaudoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.institutionbureaudoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
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

                $scope.downloadallhypothe = function () {
                    for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
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

        //        var params =
        //        {
        //            group_gid: group_gid
        //        }

        //        lockUI();
        //        var url = 'api/MstApplicationEdit/EditGroup';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.txtgroup_name = resp.data.group_name;
        //            $scope.txtdate_formation = resp.data.date_of_formation;
        //            $scope.txtgroup_type = resp.data.group_type;
        //            $scope.txtmember_count = resp.data.groupmember_count;
        //            $scope.txtmember_URN = resp.data.group_urn;
        //            $scope.groupurn_status = resp.data.groupurn_status;
        //            $scope.internalrating_gid = resp.data.internalrating_gid,
        //            $scope.txtinternalrating_name = resp.data.internalrating_name,
        //            $scope.txtmale_count = resp.data.male_count,
        //            $scope.txtfemale_count = resp.data.female_count
        //        })

        //        var params =
        //        {
        //            group_gid: group_gid
        //        }

        //        lockUI();
        //        var url = 'api/MstApplicationEdit/GroupAddressList';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.memberaddress_list = resp.data.mstaddress_list;
        //        });

        //        var params =
        //        {
        //            group_gid: group_gid
        //        }

        //        lockUI();
        //        var url = 'api/MstApplicationEdit/GroupBankList';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.memberbank_list = resp.data.mstbank_list;
        //        });

        //        var params =
        //        {
        //            group_gid: group_gid
        //        }

        //        lockUI();
        //        var url = 'api/MstApplicationEdit/GroupDocumentList';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.UploadMemberDocumentList = resp.data.groupdocument_list;
        //        });

        //        var params = {
        //            group_gid: group_gid
        //        }
        //        var url = 'api/MstApplicationEdit/GetGroupEquipmentHoldingList';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.mstequipmentholding_list = resp.data.mstequipmentholding_list;
        //        });

        //        var params = {
        //            group_gid: group_gid
        //        }
        //        var url = 'api/MstApplicationEdit/GetGroupLivestockList';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;
        //        });

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


        $scope.gotoEconomicCapital = function () {
            $location.hash('EconomicCapitaldtl');
            $anchorScroll();
        };

        $scope.gotoAddress = function () {
            $location.hash('Addressdtl');
            $anchorScroll();
        };

        $scope.gotoProductsCharges = function () {
            $location.hash('ProductsChargesdtl');
            $anchorScroll();
        };

        $scope.gotoBureauUpdates = function () {
            $location.hash('BureauUpdatesdtl');
            $anchorScroll();
        };

        $scope.gotoAssessedScore = function () {
            $location.hash('AssessedScoresdtl');
            $anchorScroll();
        };

        $scope.gotoVisitReport = function () {
            $location.hash('VisitReportdtl');
            $anchorScroll();
        };

        $scope.Kyc_view = function () {
            var application_gid = $scope.application_gid;
            var lstab = $scope.lstab;
            $location.url('app/MstApplicationEditKycView?application_gid=' + application_gid + '&lstab=' + lstab);
        }

        $scope.scorecard_view = function () {
            var application_gid = $scope.application_gid;
            var lstab = $scope.lstab;
            $location.url('app/MstAppScoreCardViewdtl?application_gid=' + application_gid + '&lstab=' + lstab);
        }

        $scope.bureauinstitution_view = function (institution2bureau_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/InsBureauRespObsDoc.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var param = {
                    institution2bureau_gid: institution2bureau_gid
                };

                var url = 'api/MstApplicationEdit/CICInstitutionEdit';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtbureauscore_date = resp.data.bureauscore_date;
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.institution2bureau_gid = resp.data.institution2bureau_gid;

                    unlockUI();
                });
                var url = 'api/MstApplicationEdit/CICUploadInstitutionDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                //$scope.downloadallcicupload = function () {
                //    for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                //        DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
                //    }
                //}

                $scope.downloadallcicupload = function () {
                    for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                        if ($scope.cicuploaddoc_list[i].migration_flag == 'N') {
                            DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
                        }
                        else {
                            DownloaddocumentService.OtherDownloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name, $scope.cicuploaddoc_list[i].migration_flag);
                        }
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
                //$scope.bureaudoc_downloads = function (val1, val2) {
                //    DownloaddocumentService.Downloaddocument(val1, val2);
                //}
                $scope.bureaudoc_downloads = function (val1, val2, val3) {
                    if (val3 == 'N') {
                        DownloaddocumentService.Downloaddocument(val1, val2);
                    }
                    else {
                        DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
                    }
                }

            }

        }

        $scope.bureaucontact_view = function (contact2bureau_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/IndBureauRespObsDoc.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var param = {
                    contact2bureau_gid: contact2bureau_gid
                };
    
                var url = 'api/MstApplicationEdit/CICIndividualEdit';   
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtbureauscore_date = resp.data.bureauscore_date;
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.contact2bureau_gid = resp.data.contact2bureau_gid;
    
                    unlockUI();
                });
                var url = 'api/MstApplicationEdit/CICUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadallcicupload = function () {
                    for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
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

                $scope.bureaudoc_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }


            }

        }

        $scope.companyequipment_View = function (institution2equipment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EquipmentholdingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    institution2equipment_gid: institution2equipment_gid
                }
                var url = 'api/MstApplicationAdd/GetEquipmentHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquantity = resp.data.quantity;
                    $scope.lbldescription = resp.data.description;
                    $scope.lblinsurancedetails = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

       

        $scope.companylivestock_View = function (institution2livestock_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/LiveStockHoldingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    institution2livestock_gid: institution2livestock_gid
                }
                var url = 'api/MstApplicationAdd/GetLivestockHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblbreed = resp.data.Breed;
                    $scope.lbllivestockinsurance_details = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.contactequipment_View = function (contact2equipment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EquipmentholdingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    contact2equipment_gid: contact2equipment_gid
                }
                var url = 'api/MstApplicationAdd/GetContactEquipmentHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquantity = resp.data.quantity;
                    $scope.lbldescription = resp.data.description;
                    $scope.lblinsurancedetails = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.contactlivestock_View = function (contact2livestock_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/LiveStockHoldingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    contact2livestock_gid: contact2livestock_gid
                }
                var url = 'api/MstApplicationAdd/GetContactLivestockHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblbreed = resp.data.Breed;
                    $scope.lbllivestockinsurance_details = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.groupequipment_View = function (group2equipment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EquipmentholdingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    group2equipment_gid: group2equipment_gid
                }
                var url = 'api/MstApplicationAdd/GetGroupEquipmentHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquantity = resp.data.quantity;
                    $scope.lbldescription = resp.data.description;
                    $scope.lblinsurancedetails = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.grouplivestock_View = function (group2livestock_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/LiveStockHoldingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    group2livestock_gid: group2livestock_gid
                }
                var url = 'api/MstApplicationAdd/GetGroupLivestockHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblbreed = resp.data.Breed;
                    $scope.lbllivestockinsurance_details = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.institutiondoc_list.length; i++) {
               // DownloaddocumentService.Downloaddocument($scope.institutiondoc_list[i].document_path, $scope.institutiondoc_list[i].document_name);

                if ($scope.institutiondoc_list[i].migration_flag == 'N') {
                    //DownloaddocumentService.Downloaddocument(val1, val2);
                    DownloaddocumentService.Downloaddocument($scope.institutiondoc_list[i].document_path, $scope.institutiondoc_list[i].document_name);
                }
                else {
                    //DownloaddocumentService.OtherDownloaddocument(val1, val2);
                    DownloaddocumentService.OtherDownloaddocument($scope.institutiondoc_list[i].document_path, $scope.institutiondoc_list[i].document_name, $scope.institutiondoc_list[i].migration_flag);
                }
            }
        }

        $scope.downloadallidproof = function () {
            for (var i = 0; i < $scope.individualproof_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.individualproof_list[i].document_path, $scope.individualproof_list[i].document_name);
            }
        }

        $scope.downloadallindividualdoc = function () {
            for (var i = 0; i < $scope.individualdoc_list.length; i++) {
               // DownloaddocumentService.Downloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name);

                if ($scope.individualdoc_list[i].migration_flag == 'N') {
                    //DownloaddocumentService.Downloaddocument(val1, val2);
                    DownloaddocumentService.Downloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name);
                }
                else {
                    //DownloaddocumentService.OtherDownloaddocument(val1, val2);
                    DownloaddocumentService.OtherDownloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name, $scope.individualdoc_list[i].migration_flag);
                }
            }
        }

        $scope.downloadallhypothe = function () {
            for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
            }
        }

        $scope.downloadallcollateral = function () {
            for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
            }
        }

        $scope.downloadallmember = function () {
            for (var i = 0; i < $scope.UploadMemberDocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadMemberDocumentList[i].document_path, $scope.UploadMemberDocumentList[i].document_name);
            }
        }

        $scope.downloadallcicupload = function () {
            for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
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

        $scope.documentviewerinstitution = function (val1, val2, val3) {
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
    }
})();
