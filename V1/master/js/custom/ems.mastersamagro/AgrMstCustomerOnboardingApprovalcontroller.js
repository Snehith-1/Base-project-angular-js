(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCustomerOnboardingApprovalcontroller', AgrMstCustomerOnboardingApprovalcontroller);

    AgrMstCustomerOnboardingApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstCustomerOnboardingApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCustomerOnboardingApprovalcontroller';
        //var selectedIndex = $location.search().selectedIndex;
        //var FromRM = $location.search().FromRM;
        //var application_gid = $location.search().application_gid;
        //var lsApp = $location.search().lsApp;
        //var lsapproved = $location.search().lsapproved;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var FromRM = searchObject.FromRM;
        var application_gid = searchObject.application_gid;
        var lsApp = searchObject.lsApp;
        var lsapproved = searchObject.lsapproved;
       
        activate();

        function activate() {
            if (FromRM == "N" && lsApp == "N") {
                $scope.showApprovereject = true;
                $scope.showdiv = true;
            }
            else if (FromRM == "Y" && lsApp == "N") {
                $scope.showApprovereject = false;
                $scope.showdiv = false;
            }

            else  {
                ////$scope.showApprovereject = false;
                ////$scope.showdiv = false;
            }

            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstBuyerOnboardEdit/GetOpenQueryStatus';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.openquery_flag = resp.data.openquery_flag;
                if ($scope.openquery_flag == "Y") {
                    $scope.hideapproval = true;
                }

            });

            var url = 'api/AgrMstBuyerOnboardEdit/GetByrQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.byrraisequerylist = resp.data.byrraisequerylist;

            });
            
        }
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.GetGeneralinfo = function () {
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrMstBuyerOnboard/GetOnboardApplicationGeneralInfo';
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
                $scope.txtcredit_group = resp.data.creditgroup_name;
                $scope.txtproduct_name = resp.data.product_name;
                $scope.txtsector_name = resp.data.sector_name;
                $scope.txtcategory_name = resp.data.category_name;
                $scope.txtvariety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txtprogram_name = resp.data.program_name;
                $scope.geneticcode_list = resp.data.geneticdetails_list;
                $scope.txtprimary_number = resp.data.primary_mobileno;
                $scope.basicmobileno_list = resp.data.mobilenumber_list;
                $scope.txtprimary_emailassdress = resp.data.primary_email;
                $scope.mailaddress_list = resp.data.mail_list;
                $scope.mstproduct_listdtl = resp.data.mstproduct_list;
                $scope.txtapplication_initiateddate = resp.data.application_initiateddate;
                $scope.txtapplication_initiatedremarks = resp.data.application_initiatedremarks;
                $scope.txtcustomer_buyer_name = resp.data.customerref_name;
                $scope.txtvertical = resp.data.vertical_name;
                $scope.txtbuyer_type = resp.data.buyersuppliertype_name;
                unlockUI();
            });

            var url = 'api/AgrMstBuyerOnboardEdit/GetByrQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.byrraisequerylist = resp.data.byrraisequerylist;
;
            });
        }

        $scope.GetIndividualInfo = function () {
            $scope.individualViewdiv = false;
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrMstBuyerOnboard/GetOnboardIndividualInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.GurantorIndividual_List = resp.data.OnboardIndividual_List;
                }
                else {
                    unlockUI();
                }

            });

            var url = 'api/AgrMstBuyerOnboardEdit/GetByrQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.byrraisequerylist = resp.data.byrraisequerylist;
                
            });
        }

        $scope.GetInstitutionInfo = function () {
            $scope.institutionViewdiv = false;
            lockUI();
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrMstBuyerOnboard/GetOnboardInstitutionInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.GurantorInstitution_List = resp.data.OnboardInstitution_List;
                }
                else {
                    unlockUI();
                }

            });

            var url = 'api/AgrMstBuyerOnboardEdit/GetByrQuerySummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.byrraisequerylist = resp.data.byrraisequerylist;
                
            });
        }

        $scope.buyerapprove = function () {
            var params = {
                application_gid: application_gid,
                approval_status: 'Approved',
                approval_remarks: $scope.txtapproval_remarks
            }
            lockUI();
            var url = "api/AgrMstBuyerOnboard/PostBuyerOnboardApproval";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //$location.url('app/AgrMstCustomerApprovalSummary?selectedIndex=0');
                    $location.url('app/AgrMstCustomerApprovalSummary');
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.buyerreject = function () {
            var params = {
                application_gid: application_gid,
                approval_status: 'Rejected',
                approval_remarks: $scope.txtapproval_remarks
            }
            lockUI();
            var url = "api/AgrMstBuyerOnboard/PostBuyerOnboardApproval";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //$location.url('app/AgrMstCustomerApprovalSummary?selectedIndex=0');
                    $location.url('app/AgrMstCustomerApprovalSummary');
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.appcreainstitution_view = function (institution_gid) {
            $scope.institutionViewdiv = true;
            lockUI();
            var params = {
                institution_gid: institution_gid
            }

            var url = 'api/AgrMstBuyerOnboard/GetOnboardInstitutionView';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.txtcompany_name = resp.data.company_name;
                    $scope.txtborrower_type = resp.data.borrower_type;
                    $scope.txtcin_no = resp.data.cin_no;
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
                    $scope.instituionmailaddress_list = resp.data.instituionmail_list;
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
                    $scope.cbostakeholdertype = resp.data.stakeholder_type;
                    $scope.txtrevenue = resp.data.revenue;
                    $scope.txtprofit = resp.data.profit;
                    $scope.txtfixed_asset = resp.data.fixed_assets;
                    $scope.txtsundrydebt_adv = resp.data.sundrydebt_adv;
                    $scope.rdbincome_tax = resp.data.incometax_returnsstatus;
                    $scope.txttan_number = resp.data.tan_number;
                    $scope.txteditmsmereg = resp.data.msme_registration;
                    $scope.txteditlei = resp.data.lglentity_id;
                    $scope.txteditleirenewal_date  = resp.data.editlei_renewaldate;
                    $scope.txteditkin  = resp.data.kin;
                    unlockUI();
                }
                else {
                    unlockUI();
                }
            });
            var url = 'api/AgrMstBuyerOnboard/GetbyrInstitutionRatingList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionratinglist = resp.data.MdlRatingdtl;

            });

            var url = 'api/AgrMstBuyerOnboard/GetbyrInstitutionBankAccDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditbankacc_list = resp.data.institution2bankacc_list;

            });

        }
        $scope.insititutionclose = function () {
            $scope.institutionViewdiv = false;
        }

        $scope.appcreaindividual_view = function (contact_gid) {
            $scope.individualViewdiv = true;
            lockUI();
            var params = {
                contact_gid: contact_gid
            }

            var url = 'api/AgrMstBuyerOnboard/GetOnboardIndividualView';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) { 
                    $scope.txtcustomer_name = resp.data.individual_name;
                    $scope.txtborrower_type = resp.data.borrower_type;
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
                    $scope.txtownership_type = resp.data.ownershiptype_name;
                    $scope.txtproperty_name = resp.data.propertyholder_name;
                    $scope.txtresidence_type = resp.data.residencetype_name;
                    $scope.txtyear_currentresidence = resp.data.currentresidence_years;
                    $scope.txtdistance = resp.data.branch_distance;
                    $scope.individualproof_list = resp.data.contactidproof_list;
                    $scope.individualdoc_list = resp.data.uploadindividualdoc_list;
                    $scope.bureauname_gid = resp.data.bureauname_gid;
                    $scope.txtindividualbureau_name = resp.data.indbureauname_name;
                    $scope.txtindividualbureau_score = resp.data.indbureau_score;
                    $scope.txtindividualscore_on = resp.data.indbureauscore_date;
                    $scope.txtindividualobservations = resp.data.indobservations;
                    $scope.txtindividualbureau_response = resp.data.indbureau_response;
                    $scope.cicindividualdocument_name = resp.data.indcicdocument_name;
                    $scope.cicindividualdocument_path = resp.data.indcicinddocument_path;
                    $scope.individualaddress_list = resp.data.contactaddress_list;
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
                    $scope.txtfathernominee_status = resp.data.fathernominee_status;
                    $scope.txtmothernominee_status = resp.data.mothernominee_status;
                    $scope.txtspousenominee_status = resp.data.spousenominee_status;
                    $scope.txttotal_landacres = resp.data.totallandinacres;
                    $scope.txtcultivated_land = resp.data.cultivatedland;
                    $scope.txtprevious_crop = resp.data.previouscrop;
                    $scope.txtproposed_crop = resp.data.prposedcrop;
                    $scope.txtinstitution_name = resp.data.institution_name;
                    $scope.contactpanabsencereasons_list = resp.data.contactpanabsencereasons_list;
                    $scope.txtpan_status = resp.data.pan_status;
                    $scope.cbostakeholdertype = resp.data.stakeholder_type;
                    $scope.txtpep_status = resp.data.pep_status;
                    unlockUI();
                }
                else {
                    unlockUI();
                }
            });

            var url = 'api/AgrMstBuyerOnboard/GetbyrPANForm60List';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.contactpanform60_list = resp.data.contactpanform60_list;

            });
            var url = 'api/AgrMstBuyerOnboard/GetbyrIndividualBankAccDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditbankacc_list = resp.data.individual2bankacc_list;

            });

        }
        $scope.individualclose = function () {
            $scope.individualViewdiv = false;
        }

        $scope.back = function () {
            if (FromRM == "Y" && lsApp != "R") {
                //$location.url('app/AgrMstCustomerOnboardingSummary?selectedIndex=' + selectedIndex);
                $location.url('app/AgrMstCustomerOnboardingSummary');
            }
            else if (FromRM == "N" && lsApp == "N") {
                //$location.url('app/AgrMstCustomerApprovalSummary?selectedIndex=' + selectedIndex);
                $location.url('app/AgrMstCustomerApprovalSummary');
            }
            else if (FromRM == "N" && lsApp == "Y") {
                //$location.url('app/AgrMstCustomerApprovalSummary?selectedIndex=' + selectedIndex);
                $location.url('app/AgrMstOnboardingApprovalCompleted');
            }
            else if (lsApp == "R") {
                $location.url('app/AgrMstCustomerOnboardRejectedSummary?hash=' + cmnfunctionService.encryptURL('FromRM=' + FromRM));
            }
            else if (lsapproved == "byr") {
                $location.url('app/AgrMstBuyerApprovedSummary');
            }
            else if (lsapproved == "supr") {
                $location.url('app/AgrMstSupplierApprovedSummary');
            }
            else {
                //$location.url('app/AgrMstCustomerApprovalSummary?application_gid=' + application_gid + '&selectedIndex=' + selectedIndex);
                $location.url('app/AgrMstCustomerApprovalSummary?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid ));

            }
        }


        $scope.creditbankacctdtl_edit = function (institution2bankdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/companybankdtl.html',
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditAccountType';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.accounttype_list = resp.data.creditbankacc_list;
                });

                var param = {
                    institution2bankdtl_gid: institution2bankdtl_gid
                }

                var url = 'api/AgrMstBuyerOnboard/EditGetCreditBankAccDtl';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.txtIFSC_Code = resp.data.ifsc_code;
                    $scope.txtBank_Name = resp.data.bank_name;
                    $scope.txtBranch_Name = resp.data.branch_name;
                    $scope.txtBank_Address = resp.data.bank_address;
                    $scope.txtMICR_Code = resp.data.micr_code;
                    $scope.txtbankacct_no = resp.data.bankaccount_number;
                    $scope.txtconfirmbankacct_no = resp.data.confirmbankaccountnumber;
                    $scope.txtacctholder_name = resp.data.bankaccount_name;
                    $scope.cboAccountType = resp.data.bankaccounttype_gid;
                    $scope.rdbJoint_Account = resp.data.joint_account;
                    $scope.txtjointacctholder_name = resp.data.jointaccountholder_name;
                    $scope.rdbCheque_Book = resp.data.chequebook_status;
                    $scope.txtAccountOpen_Date = resp.data.accountopen_date;
                    //$scope.txtAccountOpen_Date = Date.parse($scope.txtAccountOpen_Date);
                    //$scope.credituploaddocument_list = resp.data.credituploaddocument_list;
                    unlockUI();
                });

                vm.submitted = false;
                vm.validateInput = function (name, type) {
                    var input = vm.formValidate[name];
                    return (input.$dirty || vm.submitted) && input.$error[type];
                };

                // Submit form
                vm.submitForm = function () {
                    vm.submitted = true;
                    if (vm.formValidate.$valid) {
                    } else {
                        return false;
                    }
                };

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.indivbankacctdtl_edit = function (contact2bankdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/companybankdtl.html',
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditAccountType';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.accounttype_list = resp.data.creditbankacc_list;
                });

                var param = {
                    contact2bankdtl_gid: contact2bankdtl_gid
                }

                var url = 'api/AgrMstBuyerOnboard/EditGetIndividualBankAccDtl';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.txtIFSC_Code = resp.data.ifsc_code;
                    $scope.txtBank_Name = resp.data.bank_name;
                    $scope.txtBranch_Name = resp.data.branch_name;
                    $scope.txtBank_Address = resp.data.bank_address;
                    $scope.txtMICR_Code = resp.data.micr_code;
                    $scope.txtbankacct_no = resp.data.bankaccount_number;
                    $scope.txtconfirmbankacct_no = resp.data.confirmbankaccountnumber;
                    $scope.txtacctholder_name = resp.data.bankaccount_name;
                    $scope.cboAccountType = resp.data.bankaccounttype_gid;
                    $scope.rdbJoint_Account = resp.data.joint_account;
                    $scope.txtjointacctholder_name = resp.data.jointaccountholder_name;
                    $scope.rdbCheque_Book = resp.data.chequebook_status;
                    $scope.txtAccountOpen_Date = resp.data.accountopen_date;
                    //$scope.txtAccountOpen_Date = Date.parse($scope.txtAccountOpen_Date);
                    //$scope.credituploaddocument_list = resp.data.credituploaddocument_list;
                    unlockUI();
                });

                vm.submitted = false;
                vm.validateInput = function (name, type) {
                    var input = vm.formValidate[name];
                    return (input.$dirty || vm.submitted) && input.$error[type];
                };

                // Submit form
                vm.submitForm = function () {
                    vm.submitted = true;
                    if (vm.formValidate.$valid) {
                    } else {
                        return false;
                    }
                };

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.institutiondoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.institutiondoc_list[i].document_path, $scope.institutiondoc_list[i].document_name);
            }
        }
        $scope.downloadall_2 = function () {
            for (var i = 0; i < $scope.contactpanform60_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.contactpanform60_list[i].document_path, $scope.contactpanform60_list[i].document_name);
            }
        }
        $scope.downloadall_3 = function () {
            for (var i = 0; i < $scope.individualproof_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.individualproof_list[i].document_path, $scope.individualproof_list[i].document_name);
            }
        }
        $scope.downloadall_4 = function () {
            for (var i = 0; i < $scope.individualdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name);
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

        $scope.query_close = function (onboardquery_gid) {
            $scope.showclosequery = true;
            $scope.onboardquery_gid = onboardquery_gid;
            /*$scope.showdiv = false;*/
        }


        $scope.submit = function () {

            var params = {
                application_gid: application_gid,
                description: $scope.txtquery_desc,
                query_title: $scope.txtquery_title,
               
            }

            var url = 'api/AgrMstBuyerOnboardEdit/PostByrRaiseQuery';
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
                    activate();
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


        //$scope.close_query = function (onboardquery_gid) {

        //    var params =
        //    {
        //        onboardquery_gid: onboardquery_gid
        //    }
        //    var url = 'api/AgrMstBuyerOnboardEdit/GetRaiseQuerydesc';
        //    lockUI();
        //    SocketService.getparams(url, params).then(function (resp) {
        //        unlockUI();
        //        $scope.lblquery_title = resp.data.query_title;

        //    });

        //    var params = {
        //        onboardquery_gid: onboardquery_gid,
        //        application_gid: application_gid,
        //        close_remarks: $scope.txtcloseremarks
        //    }
        //    var url = 'api/AgrMstBuyerOnboardEdit/GetUpdateByrQueryStatus';
        //    lockUI();
        //    SocketService.getparams(url, params).then(function (resp) {
        //        unlockUI();
        //        if (resp.data.status == true) {
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            activate();
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'info',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }

        //    });

        //}

        $scope.close_query = function (onboardquery_gid) {
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
                    onboardquery_gid: onboardquery_gid
                }
                var url = 'api/AgrMstBuyerOnboardEdit/GetRaiseQuerydesc';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquery_title = resp.data.query_title;

                });

                $scope.submit = function () {
                    var params = {
                        onboardquery_gid: onboardquery_gid,
                        application_gid: application_gid,
                        close_remarks: $scope.txtcloseremarks
                    }
                    var url = 'api/AgrMstBuyerOnboardEdit/GetUpdateByrQueryStatus';
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



        $scope.view_querydesc = function (onboardquery_gid) {
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
                    onboardquery_gid: onboardquery_gid
                }
                var url = 'api/AgrMstBuyerOnboardEdit/GetRaiseQuerydesc';
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


        $scope.view_queryremarks = function (onboardquery_gid) {
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
                    onboardquery_gid: onboardquery_gid
                }
                var url = 'api/AgrMstBuyerOnboardEdit/GetRaiseQuerydesc';
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
