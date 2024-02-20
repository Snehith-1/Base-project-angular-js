(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprApplicationCreationViewController', AgrMstSuprApplicationCreationViewController);

    AgrMstSuprApplicationCreationViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService'];

    function AgrMstSuprApplicationCreationViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprApplicationCreationViewController';

        $scope.application_gid = $location.search().application_gid;
        $scope.lstab = $location.search().lstab;
        var lstab =  $scope.lstab;
        var application_gid = $scope.application_gid;
        lockUI();
        activate();
        function activate() {

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var params = {
                application_gid: $scope.application_gid
            }

            var url =  'api/AgrMstSuprApplicationView/GetApplicationBasicView';

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

            var url = 'api/AgrMstSuprApplicationView/GetGeneticDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.geneticcode_list = resp.data.geneticdetails_list;
            });

            var url = 'api/AgrMstSuprApplicationView/GetMobileMailDetailsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimary_number = resp.data.primary_mobileno;
                $scope.basicmobileno_list = resp.data.mobilenumber_list;
                $scope.txtprimary_emailassdress = resp.data.primary_email;
                $scope.mailaddress_list = resp.data.mail_list;
            });
           
            var url = 'api/AgrMstSuprApplicationView/GetBorrowerInstitutionView';
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
                // $scope.cicdocument_name = resp.data.cicdocument_name;
                // $scope.cicdocument_path = resp.data.cicdocument_path;
                $scope.Institutioncicdoc_list = resp.data.Institutioncicdoc_list;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txturn = resp.data.urn;
                $scope.txtcontact_firstname = resp.data.contactperson_firstname;
                $scope.txtcontact_middlename = resp.data.contactperson_middlename;
                $scope.txtcontact_lastname = resp.data.contactperson_lastname;
                $scope.txtdesignation = resp.data.designation;
                $scope.txtbusinessstart_date = resp.data.businessstart_date;
            }); 
            
            var url = 'api/AgrMstSuprApplicationView/GetBorrowerIndividualView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcustomer_name = resp.data.individual_name;
                $scope.txtpan_number = resp.data.pan_no;
                $scope.txtaadhar_number = resp.data.aadhar_no;
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
                $scope.txtmonthly_income = resp.data.monthly_income;
                $scope.txtincome_type = resp.data.user_type;
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
                // $scope.cicindividualdocument_name = resp.data.indcicdocument_name;
                // $scope.cicindividualdocument_path = resp.data.indcicinddocument_path;
                $scope.Individualcicdoc_list = resp.data.Individualcicdoc_list;
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
            }); 

            var url = 'api/AgrMstSuprApplicationView/GetProductChargesDtl';
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
            }); 

            var url = 'api/AgrMstSuprApplicationView/GetGurantorIndividualList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.GurantorIndividual_List = resp.data.GurantorIndividual_List;
            });

            var url = 'api/AgrMstSuprApplicationView/GetGurantorInstitutionList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.GurantorInstitution_List = resp.data.GurantorInstitution_List;
            });

            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'
            }
            var url = 'api/AgrMstSuprApplicationView/GetVisitReportList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.VisitReport_List = resp.data.VisitReport_List;
            });
            
            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM'
            }
            var url = 'api/AgrMstSuprApplicationView/GetGradingToolDtls';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.gradetoolsummary_list = resp.data.mstgradetoolsummary_list;
            });
                       

        }
        
        $scope.appcreagradingtool_view = function (application2gradingtool_gid) {
            var application2gradingtool_gid=application2gradingtool_gid;
            localStorage.setItem('application2gradingtool_gid', application2gradingtool_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprApplCreationGradingToolView";
            window.open(URL, '_blank');
        }

        $scope.appcreavisitreport_view = function (visitreport_gid) {
            var visitreport_gid=visitreport_gid;
            localStorage.setItem('visitreport_gid', visitreport_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprApplCreationVisitReportView";
            window.open(URL, '_blank');
        }
        
        $scope.appcreainstitution_view = function (institution_gid) {
            var institution_gid=institution_gid;
            localStorage.setItem('institution_gid', institution_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprApplCreationInstitutionGuarantorView";
            window.open(URL, '_blank');
        }

        $scope.appcreaindividual_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprApplCreationIndividualGuarantorView";
            window.open(URL, '_blank');
        }

        $scope.Back = function () {
            if (lstab == 'applicationcreation') {
                $location.url('app/AgrMstSuprApplicationCreationSummary');
            }
            else if (lstab == 'creditmapping') {
                $state.go('app.AgrSuprApplicationAssignmentSummary');
            }
            else if (lstab == 'BusinessApproval') {
                $state.go('app.AgrMstSuprBusinessApprovalSummary');
            }
            else if (lstab == 'BusinessReject') {
                $state.go('app.AgrMstSuprBusinessRejectedSummary');
            }
            else if (lstab == 'BusinessHold') {
                $state.go('app.AgrMstSuprBusinessHoldSummary');
            }
            else if (lstab == 'BusinessApproved') {
                $state.go('app.AgrMstSuprBusinessApprovedSummary');
            }
            else if (lstab == 'MyApplications') {
                $state.go('app.AgrMstSuprMyApplicationsSummary');
            }
            else if (lstab == 'RejectHoldAppl') {
                $state.go('app.AgrMstSuprRejectandHoldSummary');
            }
            else if (lstab == 'CCSkippedAppl') {
                $state.go('app.AgrMstSuprCCSkippedApplicationSummary');
            }
            else if (lstab == 'SubmittedToApproval') {
                $state.go('app.AgrMstSuprSubmittedtoApprovalSummary');
            }
            else if (lstab == 'SubmittedToCC') {
                $state.go('app.AgrMstSuprSubmittedtoCCSummary');
            }
            else if (lstab == 'CreditApproval') {
                $state.go('app.AgrMstSuprCreditApprovalSummary');
            }
            else if (lstab == 'CreditApproved') {
                $state.go('app.AgrMstSuprCreditApprovedSummary');
            }
            else if (lstab == 'CreditSubmittedtoCC') {
                $state.go('app.AgrMstSuprCreditSubmittedtoCCSummary');
            }
            else if (lstab == 'CreditCCSkipped') {
                $state.go('app.AgrMstSuprCreditCCSkippedSummary');
            }
            else if (lstab == 'CreditRejectHold') {
                $state.go('app.AgrMstSuprCreditRejectandHoldSummary');
            }
            else if (lstab == 'Pencreditmapping') {
                $state.go('app.AgrSuprApplicationAssignmentSummary');
            }
            else if (lstab == 'Asscreditmapping') {
                $state.go('app.AgrSuprAppassignedAssignmentSummary');
            }
            else if (lstab == 'ApplSubmittedToCC') {
                $state.go('app.AgrSuprApplSubmittedtoCCSummary');
            }

            else if (lstab == 'AppCCApproved') {
                $state.go('app.AgrMstSuprApprovedApplicationSummary');
            }

            else if (lstab == 'AppHoldApplications') {
                $state.go('app.AgrMstSuprHoldApplicationSummary');
            }
            else if (lstab == 'AppRejectedApplications') {
                $state.go('app.AgrMstSuprRejectedApplicationSummary');
            }
            else {
                
            }
        
        }

        //$scope.bureaudoc_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.bureaudoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.institutiondoc_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        //$scope.institutiondoc_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("EMS");

        //    var relpath1 = relPath[1].replace("\\", "/");

        //    var hosts = window.location.host;

        //    var prefix = location.protocol + "//";

        //    var str = prefix.concat(hosts, relpath1);

        //    var link = document.createElement("a");

        //    link.download = val2;

        //    var uri = str;

        //    link.href = uri;

        //    link.click();

        //}

        $scope.institutiondoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

        }

        $scope.form60_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.individualproof_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.individualproof_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.individualdoc_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.individualdoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.individualbureaudoc_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}
        $scope.individualbureaudoc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.institutionbureaudoc_downloads = function (val1, val2) {
        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    link.download = val2;
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

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
                var url = 'api/AgrMstSuprApplicationView/GetCollateralDocDtl';
               lockUI();
               SocketService.getparams(url, params).then(function (resp) {
                   unlockUI();
                   $scope.Collateraldoc_list = resp.data.CollatralDocumentList;
    
               });  
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                //$scope.download_Collateraldoc = function (val1, val2) {
                //    var phyPath = val1;
                //    var relPath = phyPath.split("EMS");
                //    var relpath1 = relPath[1].replace("\\", "/");
                //    var hosts = window.location.host;
                //    var prefix = location.protocol + "//";
                //    var str = prefix.concat(hosts, relpath1);
                //    var link = document.createElement("a");
                //    link.download = val2;
                //    var uri = str;
                //    link.href = uri;
                //    link.click();
                //}

        //        $scope.download_Collateraldoc = function (val1, val2) {
        //            var phyPath = val1;
        //            var relPath = phyPath.split("EMS");

        //            var relpath1 = relPath[1].replace("\\", "/");

        //            var hosts = window.location.host;

        //            var prefix = location.protocol + "//";

        //            var str = prefix.concat(hosts, relpath1);

        //            var link = document.createElement("a");

        //            link.download = val2;

        //            var uri = str;

        //            link.href = uri;

        //            link.click();

        //        }
              
        //    }
          
        //}

                $scope.download_Collateraldoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);

                }
                $scope.downloadall_2 = function () {
                    for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
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
                       application2hypothecation_gid : application2hypothecation_gid
                   }
                 var url = 'api/AgrMstSuprApplicationView/GetHypoDocDtl';
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
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
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
                var url = 'api/AgrMstSuprApplicationView/GetPurposeofLoan';
               lockUI();
               SocketService.getparams(url, params).then(function (resp) {
                   unlockUI();
                   $scope.txtpurposeof_loan = resp.data.enduse_purpose;
    
               });  
               var url = 'api/AgrMstSuprApplicationView/GetLoanProgramValueChain';
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
                    application2loan_gid : application2loan_gid
                   }
                 var url = 'api/AgrMstSuprApplicationView/GetLoantoBuyerList';
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

         
        $scope.group_view = function (group_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/GroupView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
               var params =
                {
                    group_gid : group_gid
                }

                lockUI();
                var url = 'api/AgrMstSuprApplicationEdit/EditGroup';               
                SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                    $scope.txtgroup_name = resp.data.group_name;
                    $scope.txtdate_formation = resp.data.date_of_formation;
                    $scope.txtgroup_type = resp.data.group_type;
                    $scope.txtmember_count = resp.data.groupmember_count;
                    $scope.txtmember_URN = resp.data.group_urn;
                    $scope.groupurn_status = resp.data.groupurn_status;
                   
            });  

            var params =
                {
                    group_gid : group_gid
                }

                lockUI();
                var url = 'api/AgrMstSuprApplicationEdit/GroupAddressList';               
                SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                    $scope.memberaddress_list = resp.data.mstaddress_list;
            });  

            var params =
                {
                    group_gid : group_gid
                }

                lockUI();
                var url = 'api/AgrMstSuprApplicationEdit/GroupBankList';               
                SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                    $scope.memberbank_list = resp.data.mstbank_list;
            });  

            var params =
                {
                    group_gid : group_gid
                }

                lockUI();
                var url = 'api/AgrMstSuprApplicationEdit/GroupDocumentList';               
                SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                    $scope.UploadMemberDocumentList = resp.data.groupdocument_list;
            });  
    
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.group_docs = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
               
            }
          
        }

        $scope.member_view = function (contact_gid) {
            var contact_gid = contact_gid;
            localStorage.setItem('contact_gid', contact_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrMstSuprApplGroupMemberdtlView";
            window.open(URL, '_blank');
        }

        var params = 
         {
            application_gid : application_gid
         }
        var url = "api/AgrMstSuprApplicationEdit/GetGroupSummary";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.group_list = resp.data.group_list;
                angular.forEach($scope.group_list, function (value, key) {
                    var params = {
                        group_gid: value.group_gid
                    };

                    var url = 'api/AgrMstSuprApplicationView/GetGrouptoMemberList';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.groupmember_list = resp.data.groupmember_list;
                        value.expand = false;
                    });
                });
            }); 


            $scope.gotoEconomicCapital = function() {
                $location.hash('EconomicCapitaldtl');          
                $anchorScroll();
              };

            $scope.gotoAddress = function() {
                $location.hash('Addressdtl');
                $anchorScroll();
            };

            $scope.gotoProductsCharges = function() {
                $location.hash('ProductsChargesdtl');
                $anchorScroll();
            };

            $scope.gotoBureauUpdates = function() {
                $location.hash('BureauUpdatesdtl');
                $anchorScroll();
            };

            $scope.gotoAssessedScore = function() {
                $location.hash('AssessedScoresdtl');
                $anchorScroll();
            };

            $scope.gotoVisitReport = function() {
                $location.hash('VisitReportdtl');
                $anchorScroll();
            };

            $scope.Kyc_view = function () {
                var application_gid = $scope.application_gid;
                var lstab = $scope.lstab;
                $location.url('app/AgrMstSuprApplicationEditKycView?application_gid=' + application_gid + '&lstab=' + lstab);
            }

            $scope.downloadall = function () {
                for (var i = 0; i < $scope.Hypothecationdoc_list.length; i++) {
                    DownloaddocumentService.Downloaddocument($scope.Hypothecationdoc_list[i].document_path, $scope.Hypothecationdoc_list[i].document_name);
                }
            }
            $scope.downloadall_2 = function () {
                for (var i = 0; i < $scope.Collateraldoc_list.length; i++) {
                    DownloaddocumentService.Downloaddocument($scope.Collateraldoc_list[i].document_path, $scope.Collateraldoc_list[i].document_name);
                }
            }
            $scope.downloadall_3 = function () {
                for (var i = 0; i < $scope.institutiondoc_list.length; i++) {
                    DownloaddocumentService.Downloaddocument($scope.institutiondoc_list[i].document_path, $scope.institutiondoc_list[i].document_name);
                }
            }
            $scope.downloadall_4 = function () {
                for (var i = 0; i < $scope.Institutioncicdoc_list.length; i++) {
                    DownloaddocumentService.Downloaddocument($scope.Institutioncicdoc_list[i].cicdocument_path, $scope.Institutioncicdoc_list[i].cicdocument_name);
                }
            }
            $scope.downloadall_5 = function () {
                for (var i = 0; i < $scope.Individualcicdoc_list.length; i++) {
                    DownloaddocumentService.Downloaddocument($scope.Individualcicdoc_list[i].cicindividualdocument_path, $scope.Individualcicdoc_list[i].cicindividualdocument_name);
                }
            }
            $scope.downloadall_6 = function () {
                for (var i = 0; i < $scope.individualproof_list.length; i++) {
                    DownloaddocumentService.Downloaddocument($scope.individualproof_list[i].document_path, $scope.individualproof_list[i].document_name);
                }
            }
            $scope.downloadall_7 = function () {
                for (var i = 0; i < $scope.individualdoc_list.length; i++) {
                    DownloaddocumentService.Downloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name);
                }
            }
            $scope.downloadall_8 = function () {
                for (var i = 0; i < $scope.UploadMemberDocumentList.length; i++) {
                    DownloaddocumentService.Downloaddocument($scope.UploadMemberDocumentList[i].document_path, $scope.UploadMemberDocumentList[i].document_name);
                }
            }
            
    }
})();
