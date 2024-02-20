(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstSanctionEdit', idasMstSanctionEdit);

    idasMstSanctionEdit.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function idasMstSanctionEdit($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */  var vm = this;
        vm.title = 'idasMstSanctionEdit';
        var sanction_gid;
        activate();

        function activate() {
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1  = relPath[1];
            $scope.uploaddclickdiv = true;
            $scope.uploadMOMclickdiv = true;
            $scope.bal_pnl = false;           
            $scope.warningfacility_amount = true;
            $scope.existing_customer = true;
            $scope.amount_validation = true;
            $scope.facility_pnl = false;
            $scope.addfacility_pnl = true;
            $scope.panel = true;
            $scope.panel1 = true;
            $scope.warningfacility_amount = true;
            $scope.amount_validation = true;
            $scope.interchangeabilityno = false;
            $scope.interchangeabilityyes = false;
            $scope.onchangefacility = false;
            $scope.colandingyes = false;
            $scope.sanction_validation = false;
            $scope.warningcbomember = true;
            $scope.warningcbofacility = true;
            $scope.mandatoryfields = false;
            $scope.validitymonth = false;
            vm.calenderEdit = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.openEdit = true;
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
            vm.dateOptionsEdit = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            $scope.cboloanfacility_typeedit = [];
            $scope.cbocc_membersedit = [];
            var url = 'api/entity/Entity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.entity_list;
            });
            var url = 'api/loan/loan_list';
            SocketService.get(url).then(function (resp) {
                $scope.loan_list = resp.data.loanmasterdtls;
            });
            var url = 'api/IdasTrnLsaManagement/loanfacility';
            SocketService.get(url).then(function (resp) {
                $scope.loanfacility_list = resp.data.loanfacility_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/MstCCMember/Getccgroup';
            SocketService.get(url).then(function (resp) {
                $scope.ccgroup_list = resp.data.ccgroup_list;
            });
             sanction_gid=localStorage.getItem('sanction_gid');
            var url = 'api/IdasMstSanction/SanctionDtlsEdit';
            var params = {
                sanction_gid: sanction_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefnoEdit = resp.data.sanction_refno;
                $scope.SanctionDateEdit = resp.data.sanctionDate;
                $scope.SanctionAmountEdit = resp.data.sanction_amount;
                $scope.FacilityTypeEdit = resp.data.facilitytype_gid;
                $scope.customerNameEdit = resp.data.customername;
                $scope.CustomerurnEdit = resp.data.customer_urn;
                $scope.collateralEdit = resp.data.collateral_security;
                $scope.zonalHeadNameEdit = resp.data.zonal_name;
                $scope.businessHeadNameEdit = resp.data.businesshead_name;
                $scope.clusterManagerEdit = resp.data.cluster_manager_name;
                $scope.creditManagerEdit = resp.data.creditmanager_name;
                $scope.relationshipmgmtEdit = resp.data.relationshipmgmt_name;
                $scope.verticalCodeEdit = resp.data.vertical;
                $scope.txtSanctionLimit = resp.data.sanction_limit;
                $scope.txtpurpose_lendingedit = resp.data.purpose_lending;
                $scope.rdbfacility_typeedit = resp.data.facility_secure_type;
                $scope.txtproduct_solutionedit = resp.data.product_solution;
                $scope.txtmajor_interventionedit = resp.data.major_intervention;
                $scope.txtprimaryvalue_chainedit = resp.data.primary_value_chain;
                $scope.txtsecondaryvalue_chainedit = resp.data.secondary_value_chain;
                $scope.customer2security_list = resp.data.customersecurity_list;
                $scope.filename_list = resp.data.UploadgeneralDocumentList;
                $scope.sanction_branch_gid = resp.data.sanction_branch_gid;
                $scope.sanction_branch_name = resp.data.sanction_branch_name;
                $scope.sanction_state_name = resp.data.sanction_state_name;
                $scope.sanction_state_gid = resp.data.sanction_state_gid;
                $scope.sanctionfilename_list = resp.data.UploadSANDocumentList;
                $scope.MOMfilename_list = resp.data.UploadMOMDocumentList;
                $scope.CAMfilename_list = resp.data.UploadCOMDocumentList;
                $scope.buyer_list = resp.data.buyer_list;
                $scope.rdb_bal = resp.data.status_ofBAL;
                $scope.txtvirtualaccount_no = resp.data.virtual_accountno;
                $scope.txtexternalratingedit = resp.data.external_rating;
                $scope.txtbusiness_descriptionedit = resp.data.business_description;
                $scope.txtassociate_nameedit = resp.data.associate_name;
                $scope.txtsapayoutedit = resp.data.sa_payout;
                $scope.txttypeof_enterprisesedit = resp.data.typeof_enterprise;
                $scope.txtrisk_categorizationedit = resp.data.risk_categorization;
                $scope.txtes_applicationedit = resp.data.es_application;
                $scope.txtesrisk_categorizationedit = resp.data.esrisk_categorization;
                //$scope.txtapplicability_categoryedit = resp.data.applicability_category;
                $scope.txtinternal_ratingedit = resp.data.internal_rating;
                $scope.rdb_psl = resp.data.status_ofPSL;
                $scope.msme_classification = resp.data.msme_classification;
                $scope.txtindustryedit = resp.data.industry;
                $scope.txtinvesment_pmedit = resp.data.invesment_pme;
                $scope.txtturn_overedit = resp.data.turn_over;
                $scope.txtregno_msmeedit = resp.data.regno_msme;
                $scope.txtvalidity_monthsedit = resp.data.validity_months;
                $scope.txtpenal_interestedit = resp.data.penal_interest;
                $scope.rdbdeclaration = resp.data.esdeclaration_status;
                $scope.uploadesfilename_list = resp.data.UploadES_DocumentList;
                $scope.mailfilename_list = resp.data.DeviationMail_DocumentList;
                if (resp.data.status_ofBAL == 'Yes')
                {
                    $scope.bal_pnl=true
                }
                else {
                    $scope.bal_pnl = false
                }
                if (resp.data.colanding_status == 'Yes') {
                    $scope.colandingyes = true
                }
                else {
                    $scope.colandingyes = false
                }
                if (resp.data.esdeclaration_status == 'Yes') {
                    $scope.esdeclarationyes = true
                    $scope.esdeclarationno = false
                }
                if(resp.data.esdeclaration_status == 'No') {
                    $scope.esdeclarationyes = false
                    $scope.esdeclarationno = true
                }
                $scope.cboapproved_byedit = resp.data.ccapprovedby_gid;
                $scope.cbocc_decisionedit = resp.data.ccdecision; 
                $scope.txtccfeedbackedit = resp.data.ccfeedback;
                $scope.txtgeneral_remarksedit = resp.data.general_remarks;
                $scope.mdlccmember = resp.data.mdlccmember;
                $scope.loanfacilitytype_list = resp.data.loanfacilitytype_list;
                console.log(resp.data.loanfacilitytype_list);
                if (resp.data.ccapprovedDate != '0001-01-01T00:00:00') {
                    $scope.txtapproveddateedit = resp.data.ccapprovedDate;
                }
                $scope.rdbsanction_type = resp.data.sanction_type;
               
                if (resp.data.sanction_type == 'Existing Customer')
                {
                    $scope.existing_customer = true;
                }
                else {
                    $scope.existing_customer = false;
                }
                $scope.cbonature_proposal = resp.data.natureof_proposal;
                $scope.rdbcolanding = resp.data.colanding_status;
                $scope.txtcolander_name = resp.data.colander_name;
                $scope.cboentity_type = resp.data.entity_gid;
                $scope.rdbpaycard = resp.data.paycard;
            });
            var url = 'api/IdasMstSanction/GetPenalInterest';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.penal_interest = resp.data.loanfacilitytype_list;

            });
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });
            var url = 'api/IdasTrnLsaManagement/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });
            var url = 'api/IdasMstSanction/tempdelete';
            SocketService.getparams(url, params).then(function (resp) {
            });
            unlockUI();
            var url = 'api/IdasMstSanction/editvalidation';
            SocketService.getparams(url, params).then(function (resp) {              
                if ((resp.data.totalloanfacility_amount == "0")||(resp.data.totalloanfacility_amount == '')) {                  
                    $scope.totalloanfacilityamount = "0,0";
                }
                else {
                    $scope.totalloanfacilityamount = resp.data.totalloanfacility_amount;
                }
                if ((resp.data.total_documentlimit == '') || (resp.data.total_documentlimit == "0")) {
                    $scope.totaldocumentlimitamount = "0,0";
                }
                else {
                    $scope.totaldocumentlimitamount = resp.data.total_documentlimit;
                }
                $scope.interchangeability_amount = resp.data.interchangeability_amount;               
            });          
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

        $scope.sanctionback = function (relpath1) {       
            $location.url('app/idasMstSanctionSummary?lstab='+relpath1);       
        }

        $scope.sanctiontype_existing=function()
        {
            $scope.existing_customer = true;
        }

        $scope.sanctiontype_new = function () {
            $scope.existing_customer = false;
        }
   
        $scope.interchangeability_yes = function () {
            $scope.interchangeabilityno = false;
            $scope.interchangeabilityyes = true;
            $scope.mandatoryfields = false;
        }
        $scope.interchangeability_no = function () {
            $scope.interchangeabilityno = true;
            $scope.interchangeabilityyes = false;
            $scope.mandatoryfields = false;
        }
        $scope.rdbcolanding_yes = function () {
            $scope.colandingyes = true;
        }
        $scope.rdbcolanding_no = function () {
            $scope.colandingyes = false;
        }
        $scope.rdbdeclaration_yes = function () {
            $scope.esdeclarationyes = true;
            $scope.esdeclarationno = false;
        }
        $scope.rdbdeclaration_no = function () {
            $scope.esdeclarationyes = false;
            $scope.esdeclarationno = true;
        }
        //Sanction Amount
        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_sanctionamount = inWords(str);

            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.SanctionAmountEdit = "";
            }
            else {
                document.getElementById('sanctionamount_words').innerHTML = lswords_sanctionamount;
                $scope.SanctionAmountEdit = output;
            }         
        }
    //Sanction Update Event
        $scope.sanctionUpdate = function (relpath1) {
       var lssanctionamount=parseInt($scope.SanctionAmountEdit.replace(/[\s,]+/g, '').trim());
       var lsloanamount = parseInt($scope.totalloanfacilityamount.replace(/[\s,]+/g, '').trim());
       var lsinterchangeability_amount = parseInt($scope.interchangeability_amount.replace(/[\s,]+/g, '').trim());
       if ($scope.rdbsanction_type == 'Existing Customer')
       {
           if ($scope.cbonature_proposal == '' || $scope.cbonature_proposal == null)
           {
               Notify.alert('Kindly Select Nature of Proposal','warning');
           }
           else {
                if (lssanctionamount < (lsloanamount + lsinterchangeability_amount)) {

                   Notify.alert('Loan Facility Amount is exceeded from Sanction Amount', 'warning')
               }
               else {

                   var url = 'api/IdasMstSanction/editmandatory_check';
                   var params = {
                       sanction_gid: sanction_gid,
                       esdeclaration_status: $scope.rdbdeclaration,
                   };
                   SocketService.getparams(url, params).then(function (resp) {
                       if (resp.data.status == true) {
                            if ($scope.verticalCodeEdit == 'FPO') {
                               if (($scope.rdbpaycard == "") || ($scope.rdbpaycard == undefined)) {
                                   Notify.alert("Kindly Select Paycard value");
                               }
                               else
                               {
                               var input = $scope.SanctionAmountEdit;
                               var arr = input.split(',');
                               var i;
                               for (i = 0; i < arr.length; i++) {
                                   var str = input.replace(',', '');
                                   input = str;
                               }

                               var facility_type = $('#FacilityType_Edit :selected').text();
                               var branch_name = $('#branch_name :selected').text();
                               var state_name = $('#state_name :selected').text();
                               var ccapproved_by = $('#ccgroup_name :selected').text();
                               var params = {
                                   sanction_refno: $scope.sanctionrefnoEdit,
                                   sanction_amount: input,
                                   sanction_date: $scope.SanctionDateEdit,
                                   facility_type: facility_type,
                                   facilitytype_gid: $scope.FacilityTypeEdit,
                                   collateral_security: $scope.collateralEdit,
                                   vertical: $scope.verticalCodeEdit,
                                   customer2sanction_gid: sanction_gid,
                                   purpose_lending: $scope.txtpurpose_lendingedit,
                                   facility_secure_type: $scope.rdbfacility_typeedit,
                                   product_solution: $scope.txtproduct_solutionedit,
                                   major_intervention: $scope.txtmajor_interventionedit,
                                   primary_value_chain: $scope.txtprimaryvalue_chainedit,
                                   secondary_value_chain: $scope.txtsecondaryvalue_chainedit,
                                   sanction_branch_name: branch_name,
                                   sanction_state_name: state_name,
                                   sanction_branch_gid: $scope.sanction_branch_gid,
                                   sanction_state_gid: $scope.sanction_state_gid,
                                   virtual_accountno: $scope.txtvirtualaccount_no,
                                   status_ofBAL: $scope.rdb_bal,
                                   ccmember_listedit: $scope.cbocc_membersedit,
                                   ccapprovedby_gid: $scope.cboapproved_byedit,
                                   ccapproved_by: ccapproved_by,
                                   ccapproved_date: $scope.txtapproveddateedit,
                                   ccdecision: $scope.cbocc_decisionedit,
                                   ccfeedback: $scope.txtccfeedbackedit,
                                   general_remarks: $scope.txtgeneral_remarksedit,
                                   colanding_status: $scope.rdbcolanding,
                                   colander_name: $scope.txtcolander_name,
                                   sanction_type: $scope.rdbsanction_type,
                                   natureof_proposal: $scope.cbonature_proposal,
                                   entity: $scope.cboentity_type,
                                   external_rating: $scope.txtexternalratingedit,
                                   business_description: $scope.txtbusiness_descriptionedit,
                                   associate_name: $scope.txtassociate_nameedit,
                                   sa_payout: $scope.txtsapayoutedit,
                                   typeof_enterprise: $scope.txttypeof_enterprisesedit,
                                   risk_categorization: $scope.txtrisk_categorizationedit,
                                   es_application: $scope.txtes_applicationedit,
                                   esrisk_categorization: $scope.txtesrisk_categorizationedit,
                                   //applicability_category: $scope.txtapplicability_categoryedit,
                                   internal_rating: $scope.txtinternal_ratingedit,
                                   status_ofPSL: $scope.rdb_psl,
                                   msme_classification: $scope.msme_classification,
                                   industry: $scope.txtindustryedit,
                                   invesment_pme: $scope.txtinvesment_pmedit,
                                   turn_over: $scope.txtturn_overedit,
                                   regno_msme: $scope.txtregno_msmeedit,
                                   validity_months: $scope.txtvalidity_monthsedit,
                                   penal_interest: $scope.txtpenal_interestedit,
                                   paycard: $scope.rdbpaycard
                               }
                               console.log(ccapproved_by);
                               var url = 'api/IdasMstSanction/UpdateSanction';
                               lockUI();
                               SocketService.post(url, params).then(function (resp) {
                                   if (resp.data.status == true) {
                                       unlockUI();

                                       Notify.alert(resp.data.message, 'success')
                                       $location.url('app/idasMstSanctionSummary?lstab=' + relpath1);

                                   }
                                   else {
                                       unlockUI();
                                       Notify.alert(resp.data.message)
                                   }
                                   activate();
                               });
                           }
                           }
                           else {
                              
                               var input = $scope.SanctionAmountEdit;
                               var arr = input.split(',');
                               var i;
                               for (i = 0; i < arr.length; i++) {
                                   var str = input.replace(',', '');
                                   input = str;
                               }

                               var facility_type = $('#FacilityType_Edit :selected').text();
                               var branch_name = $('#branch_name :selected').text();
                               var state_name = $('#state_name :selected').text();
                               var ccapproved_by = $('#ccgroup_name :selected').text();
                               var params = {
                                   sanction_refno: $scope.sanctionrefnoEdit,
                                   sanction_amount: input,
                                   sanction_date: $scope.SanctionDateEdit,
                                   facility_type: facility_type,
                                   facilitytype_gid: $scope.FacilityTypeEdit,
                                   collateral_security: $scope.collateralEdit,
                                   vertical: $scope.verticalCodeEdit,
                                   customer2sanction_gid: sanction_gid,
                                   purpose_lending: $scope.txtpurpose_lendingedit,
                                   facility_secure_type: $scope.rdbfacility_typeedit,
                                   product_solution: $scope.txtproduct_solutionedit,
                                   major_intervention: $scope.txtmajor_interventionedit,
                                   primary_value_chain: $scope.txtprimaryvalue_chainedit,
                                   secondary_value_chain: $scope.txtsecondaryvalue_chainedit,
                                   sanction_branch_name: branch_name,
                                   sanction_state_name: state_name,
                                   sanction_branch_gid: $scope.sanction_branch_gid,
                                   sanction_state_gid: $scope.sanction_state_gid,
                                   virtual_accountno: $scope.txtvirtualaccount_no,
                                   status_ofBAL: $scope.rdb_bal,
                                   ccmember_listedit: $scope.cbocc_membersedit,
                                   ccapprovedby_gid: $scope.cboapproved_byedit,
                                   ccapproved_by:ccapproved_by,
                                   ccapproved_date: $scope.txtapproveddateedit,
                                   ccdecision: $scope.cbocc_decisionedit,
                                   ccfeedback: $scope.txtccfeedbackedit,
                                   general_remarks: $scope.txtgeneral_remarksedit,
                                   colanding_status: $scope.rdbcolanding,
                                   colander_name: $scope.txtcolander_name,
                                   sanction_type: $scope.rdbsanction_type,
                                   natureof_proposal: $scope.cbonature_proposal,
                                   entity: $scope.cboentity_type,
                                   external_rating: $scope.txtexternalratingedit,
                                   business_description: $scope.txtbusiness_descriptionedit,
                                   associate_name: $scope.txtassociate_nameedit,
                                   sa_payout: $scope.txtsapayoutedit,
                                   typeof_enterprise: $scope.txttypeof_enterprisesedit,
                                   risk_categorization: $scope.txtrisk_categorizationedit,
                                   es_application: $scope.txtes_applicationedit,
                                   esrisk_categorization: $scope.txtesrisk_categorizationedit,
                                   //applicability_category: $scope.txtapplicability_categoryedit,
                                   internal_rating: $scope.txtinternal_ratingedit,
                                   status_ofPSL: $scope.rdb_psl,
                                   msme_classification: $scope.msme_classification,
                                   industry: $scope.txtindustryedit,
                                   invesment_pme: $scope.txtinvesment_pmedit,
                                   turn_over: $scope.txtturn_overedit,
                                   regno_msme: $scope.txtregno_msmeedit,
                                   validity_months: $scope.txtvalidity_monthsedit,
                                   penal_interest: $scope.txtpenal_interestedit,
                                   paycard: $scope.rdbpaycard,
                                   esdeclaration_status: $scope.rdbdeclaration,
                               }
                                console.log(ccapproved_by)
                               var url = 'api/IdasMstSanction/UpdateSanction';
                               lockUI();
                               SocketService.post(url, params).then(function (resp) {
                                   if (resp.data.status == true) {
                                       unlockUI();

                                       Notify.alert(resp.data.message, 'success')
                                       $location.url('app/idasMstSanctionSummary?lstab=' + relpath1);

                                   }
                                   else {
                                       unlockUI();
                                       Notify.alert(resp.data.message)
                                   }
                                   activate();
                               });
                           }
                          
                       }
                       else {

                           Notify.alert(resp.data.message, 'warning')
                       }
                   });
               }
           }
       }
       else
           {
       if (lssanctionamount<(lsloanamount+lsinterchangeability_amount))
            {
                
                Notify.alert('Loan Facility Amount is exceeded from Sanction Amount', 'warning')
            }
            else
            {

                var url = 'api/IdasMstSanction/editmandatory_check';
                var params = {
                    sanction_gid: sanction_gid,
                    esdeclaration_status: $scope.rdbdeclaration,
                };
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        if ($scope.verticalCodeEdit == 'FPO') {
                            if (($scope.rdbpaycard == "") || ($scope.rdbpaycard == undefined)) {
                                Notify.alert("Kindly Select Paycard value");
                            }
                            else {


                                var input = $scope.SanctionAmountEdit;
                                var arr = input.split(',');
                                var i;
                                for (i = 0; i < arr.length; i++) {
                                    var str = input.replace(',', '');
                                    input = str;
                                }

                                var facility_type = $('#FacilityType_Edit :selected').text();
                                var branch_name = $('#branch_name :selected').text();
                                var state_name = $('#state_name :selected').text();
                                var ccapproved_by = $('#ccgroup_name :selected').text();
                                var params = {
                                    sanction_refno: $scope.sanctionrefnoEdit,
                                    sanction_amount: input,
                                    sanction_date: $scope.SanctionDateEdit,
                                    facility_type: facility_type,
                                    facilitytype_gid: $scope.FacilityTypeEdit,
                                    collateral_security: $scope.collateralEdit,
                                    vertical: $scope.verticalCodeEdit,
                                    customer2sanction_gid: sanction_gid,
                                    purpose_lending: $scope.txtpurpose_lendingedit,
                                    facility_secure_type: $scope.rdbfacility_typeedit,
                                    product_solution: $scope.txtproduct_solutionedit,
                                    major_intervention: $scope.txtmajor_interventionedit,
                                    primary_value_chain: $scope.txtprimaryvalue_chainedit,
                                    secondary_value_chain: $scope.txtsecondaryvalue_chainedit,
                                    sanction_branch_name: branch_name,
                                    sanction_state_name: state_name,
                                    sanction_branch_gid: $scope.sanction_branch_gid,
                                    sanction_state_gid: $scope.sanction_state_gid,
                                    virtual_accountno: $scope.txtvirtualaccount_no,
                                    status_ofBAL: $scope.rdb_bal,
                                    ccmember_listedit: $scope.cbocc_membersedit,
                                    ccapprovedby_gid: $scope.cboapproved_byedit,
                                    ccapproved_date: $scope.txtapproveddateedit,
                                    ccdecision: $scope.cbocc_decisionedit,
                                    ccfeedback: $scope.txtccfeedbackedit,
                                    general_remarks: $scope.txtgeneral_remarksedit,
                                    colanding_status: $scope.rdbcolanding,
                                    colander_name: $scope.txtcolander_name,
                                    sanction_type: $scope.rdbsanction_type,
                                    natureof_proposal: $scope.cbonature_proposal,
                                    entity: $scope.cboentity_type,
                                    external_rating: $scope.txtexternalratingedit,
                                    business_description: $scope.txtbusiness_descriptionedit,
                                    associate_name: $scope.txtassociate_nameedit,
                                    sa_payout: $scope.txtsapayoutedit,
                                    typeof_enterprise: $scope.txttypeof_enterprisesedit,
                                    risk_categorization: $scope.txtrisk_categorizationedit,
                                    es_application: $scope.txtes_applicationedit,
                                    esrisk_categorization: $scope.txtesrisk_categorizationedit,
                                    //applicability_category: $scope.txtapplicability_categoryedit,
                                    internal_rating: $scope.txtinternal_ratingedit,
                                    status_ofPSL: $scope.rdb_psl,
                                    msme_classification: $scope.msme_classification,
                                    industry: $scope.txtindustryedit,
                                    invesment_pme: $scope.txtinvesment_pmedit,
                                    turn_over: $scope.txtturn_overedit,
                                    regno_msme: $scope.txtregno_msmeedit,
                                    validity_months: $scope.txtvalidity_monthsedit,
                                    penal_interest: $scope.txtpenal_interestedit,
                                    paycard: $scope.rdbpaycard,
                                    esdeclaration_status: $scope.rdbdeclaration,
                                    ccapproved_by:ccapproved_by
                                }
                                console.log(ccapproved_by)
                                var url = 'api/IdasMstSanction/UpdateSanction';
                                lockUI();
                                SocketService.post(url, params).then(function (resp) {
                                    if (resp.data.status == true) {
                                        unlockUI();

                                        Notify.alert(resp.data.message, 'success')
                                        $location.url('app/idasMstSanctionSummary?lstab=' + relpath1);

                                    }
                                    else {
                                        unlockUI();
                                        Notify.alert(resp.data.message)
                                    }
                                    activate();
                                });
                            }
                        }
                            else {

                           
                            var input = $scope.SanctionAmountEdit;
                            var arr = input.split(',');
                            var i;
                            for (i = 0; i < arr.length; i++) {
                                var str = input.replace(',', '');
                                input = str;
                            }

                            var facility_type = $('#FacilityType_Edit :selected').text();
                            var branch_name = $('#branch_name :selected').text();
                            var state_name = $('#state_name :selected').text();
                            var ccapproved_by = $('#ccgroup_name :selected').text();
                            var params = {
                                sanction_refno: $scope.sanctionrefnoEdit,
                                sanction_amount: input,
                                sanction_date: $scope.SanctionDateEdit,
                                facility_type: facility_type,
                                facilitytype_gid: $scope.FacilityTypeEdit,
                                collateral_security: $scope.collateralEdit,
                                vertical: $scope.verticalCodeEdit,
                                customer2sanction_gid: sanction_gid,
                                purpose_lending: $scope.txtpurpose_lendingedit,
                                facility_secure_type: $scope.rdbfacility_typeedit,
                                product_solution: $scope.txtproduct_solutionedit,
                                major_intervention: $scope.txtmajor_interventionedit,
                                primary_value_chain: $scope.txtprimaryvalue_chainedit,
                                secondary_value_chain: $scope.txtsecondaryvalue_chainedit,
                                sanction_branch_name: branch_name,
                                sanction_state_name: state_name,
                                sanction_branch_gid: $scope.sanction_branch_gid,
                                sanction_state_gid: $scope.sanction_state_gid,
                                virtual_accountno: $scope.txtvirtualaccount_no,
                                status_ofBAL: $scope.rdb_bal,
                                ccmember_listedit: $scope.cbocc_membersedit,
                                ccapprovedby_gid: $scope.cboapproved_byedit,
                                ccapproved_date: $scope.txtapproveddateedit,
                                ccdecision: $scope.cbocc_decisionedit,
                                ccfeedback: $scope.txtccfeedbackedit,
                                general_remarks: $scope.txtgeneral_remarksedit,
                                colanding_status: $scope.rdbcolanding,
                                colander_name: $scope.txtcolander_name,
                                sanction_type: $scope.rdbsanction_type,
                                natureof_proposal: $scope.cbonature_proposal,
                                entity: $scope.cboentity_type,
                                external_rating: $scope.txtexternalratingedit,
                                business_description: $scope.txtbusiness_descriptionedit,
                                associate_name: $scope.txtassociate_nameedit,
                                sa_payout: $scope.txtsapayoutedit,
                                typeof_enterprise: $scope.txttypeof_enterprisesedit,
                                risk_categorization: $scope.txtrisk_categorizationedit,
                                es_application: $scope.txtes_applicationedit,
                                esrisk_categorization: $scope.txtesrisk_categorizationedit,
                                //applicability_category: $scope.txtapplicability_categoryedit,
                                internal_rating: $scope.txtinternal_ratingedit,
                                status_ofPSL: $scope.rdb_psl,
                                msme_classification: $scope.msme_classification,
                                industry: $scope.txtindustryedit,
                                invesment_pme: $scope.txtinvesment_pmedit,
                                turn_over: $scope.txtturn_overedit,
                                regno_msme: $scope.txtregno_msmeedit,
                                validity_months: $scope.txtvalidity_monthsedit,
                                penal_interest: $scope.txtpenal_interestedit,
                                paycard: $scope.rdbpaycard,
                                esdeclaration_status: $scope.rdbdeclaration,
                                ccapproved_by:ccapproved_by
                            }
                            console.log(ccapproved_by)
                            var url = 'api/IdasMstSanction/UpdateSanction';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    unlockUI();

                                    Notify.alert(resp.data.message, 'success')
                                    $location.url('app/idasMstSanctionSummary?lstab=' + relpath1);

                                }
                                else {
                                    unlockUI();
                                    Notify.alert(resp.data.message)
                                }
                                activate();
                            });
                        }
                    
                    }
                    else {

                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
       }
        }
      
        //Upload General Document
        $scope.upload = function (val, val1, name) {
            if (($scope.document_type == null) || ($scope.document_type == '') || ($scope.document_type == undefined)) {
                $("#addupload").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }

                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_type', $scope.document_type);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasMstSanction/Editgeneraldocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#addupload").val('');
                    if (resp.data.status == true) {
                        unlockUI();
                        $scope.document_type = '';
                        $scope.showdiv = true;
                        $scope.hidediv = false;
                        Notify.alert(resp.data.message, 'success')
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }
                    generaldocument();
                });
            }
        }
        //Delete CAM Document
        $scope.deleteCAM = function (val, data) {
            var params = { document_gid: val };
            var url = 'api/IdasMstSanction/camdoc_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.CAMfilename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.CAMfilename_list.splice(key, 1);
                        }
                    });
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

        //Delete MOM Document
        $scope.deleteMOM = function (val, data) {
            var params = { document_gid: val };         
            var url = 'api/IdasMstSanction/momdoc_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.MOMfilename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.MOMfilename_list.splice(key, 1);
                        }
                    });
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

        // Delete the sanction letter
        $scope.sandocument_cancelclick = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/IdasMstSanction/sanctionletter_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.sanctionfilename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.sanctionfilename_list.splice(key, 1);
                        }
                    });
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

      $scope.document_cancelclick = function (val, data) {
            var params = { document_gid: val };
            var url = 'api/IdasMstSanction/documentdelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.filename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.filename_list.splice(key, 1);
                        }
                    });
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
       
        function generaldocument() {
            var params = {
                sanction_gid: sanction_gid
            };      
            var url = 'api/IdasMstSanction/Getgeneraldocment';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.filename_list = resp.data.UploadDocumentList;
            });
        }     
        $scope.uploadclick = function () {
            $scope.uploadddiv = true;
            $scope.uploaddclickdiv = false;
        }

        $scope.cancelupload = function () {
            $scope.uploadddiv = false;
            $scope.uploaddclickdiv = true;
            $("#addupload").val('');
        }
        $scope.uploadclickMOM = function () {
            $scope.uploadMOMdiv = true;
            $scope.uploadMOMclickdiv = false;
        }

        $scope.cancelMOMupload = function () {
            $scope.uploadMOMdiv = false;
            $scope.uploadMOMclickdiv = true;
            $("#addupload").val('');
        }
        $scope.uploadCAM_doc = function (val, val1, name) {
            if (($scope.CAMdocument_type == null) || ($scope.CAMdocument_type == '') || ($scope.CAMdocument_type == undefined)) {
                $("#addCAMupload").val('');
                 Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_type', $scope.CAMdocument_type);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasMstSanction/EditCAMddocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#addCAMupload").val('');
                 
                    if (resp.data.status == true) {
                        $scope.CAMdocument_type = '';
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        camdocument();
                    }
                    else {
                        unlockUI();
                        Notify.alert('File Format Not Supported!', {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
            }
        }
        $scope.uploadMOM_doc = function (val, val1, name) {
            if (($scope.MOMdocument_type == null) || ($scope.MOMdocument_type == '') || ($scope.MOMdocument_type == undefined)) {
                $("#addMOMupload").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_type', $scope.MOMdocument_type);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasMstSanction/EditMOMddocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#addMOMupload").val('');
                    $scope.MOMfilename_list = resp.data.UploadMOMDocumentList;

                    unlockUI();

                    if (resp.data.status == true) {
                        $scope.MOMdocument_type = '';
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        momdocument();
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
        function camdocument() {
            var params = {
                sanction_gid: sanction_gid
            };
            var url = 'api/IdasMstSanction/Getcamdocment';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.CAMfilename_list = resp.data.UploadDocumentList;               
            });
        }
        function momdocument() {
            var params = {
                sanction_gid: sanction_gid
            };
            var url = 'api/IdasMstSanction/Getmomdocment';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MOMfilename_list = resp.data.UploadMOMDocumentList;
            });
        }

        //Upload Sanction Letter
        $scope.sanupload = function (val, val1, name) {
            if (($scope.SANdocument_type == null) || ($scope.SANdocument_type == '') || ($scope.SANdocument_type == undefined)) {
                $("#addSANupload").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_type', $scope.SANdocument_type);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasMstSanction/Uploadsanctionletter';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    if (resp.data.status == true) {
                        $("#addSANupload").val('');
                        unlockUI();
                        $scope.SANdocument_type = '';
                        $scope.showdiv = true;
                        $scope.hidediv = false;
                        Notify.alert(resp.data.message, 'success')
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }
                    sandocument();
                });
            }
        }
        function sandocument() {
            var params = {
                sanction_gid: sanction_gid
            };
            var url = 'api/IdasMstSanction/GetEditsanctionletter';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionfilename_list = resp.data.UploadSANDocumentList;
            });
        }

        $scope.downloadsCAM = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }

        $scope.downloadsMOM = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }
        $scope.downloadsanctionletter = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }

        $scope.downloadsgeneral = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }

        $scope.addbuyer = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addbuyer.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtbuyer_exposure = 0;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.uploadBAL_doc = function (val, val1, name) {
                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        return false;
                    }

                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
               }
                $scope.submit_buyer = function () {
                    var url = 'api/IdasMstSanction/postBALdocument';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {                      
                            var params = {
                                buyer_name: $scope.txtbuyer_name,
                                buyer_exposure: $scope.txtbuyer_exposure
                            }                      
                            var url = 'api/IdasMstSanction/PostBuyerInfo';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    buyer_list();
                                }
                                else {
                                    Notify.alert('File Format Not Supported!', {
                                        status: 'info',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                }
                            });
                       
                        $modalInstance.close('closed');
                    });
                }
            }
        }
        function buyer_list() {
            var url = 'api/IdasMstSanction/GetBuyerinfoEdit';
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url,params).then(function (resp) {
                $scope.buyer_list = resp.data.buyer_list;
            });
        }
        $scope.rdbbal_yes = function () {
            $scope.bal_pnl = true;
        }
        $scope.rdbbal_no = function () {
            $scope.bal_pnl = false;
        }
        $scope.downloadsBAL = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }

        $scope.delete_buyer = function (val, data) {
            var params = { buyer_gid: val };
            var url = 'api/IdasMstSanction/buyerdelete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.buyer_list, function (value, key) {
                        if (value.buyer_gid == val) {
                            $scope.buyer_list.splice(key, 1);
                        }
                    });
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
        $scope.ccmember_add = function () {
            if (!$scope.cbocc_members) {
                $scope.warningcbomember = false;
            }
            else {
                $scope.warningcbomember = true;
                var params =
                    {
                        ccmember_gid: $scope.cbocc_members.ccmember_gid,
                        ccmember_name: $scope.cbocc_members.CCMember_name,
                        ccmember_remarks: $scope.txtccmember_remarks,
                        ccgroup_name: $scope.cboccgroup_name.ccgroup_name,
                        sanction_gid: sanction_gid
                    }
                var url = 'api/IdasMstSanction/updateccmembers';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI();
                        $scope.txtccmember_remarks = '';
                        $scope.cbocc_members = '';
                        Notify.alert(resp.data.message, 'success')
                        ccmember_list();
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }
        function ccmember_list() {
            var url = 'api/IdasMstSanction/Editccmember';
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                $scope.mdlccmember = resp.data.mdlccmember;
            });
        }
        $scope.deleteccmember = function (ccmemberlist_gid) {
            var params =
                {
                    ccmemberlist_gid: ccmemberlist_gid
                }
            var url = 'api/IdasMstSanction/deleteccmember';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, 'success')
                    ccmember_list();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning')
                }
            });
        }
        $scope.onselectedchangeccgroup = function () {
            var params = {
                ccgroupname_gid: $scope.cboccgroup_name.ccgroupname_gid
            }
            var url = 'api/MstCCMember/Getccgroup2member';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.ccmember_list = resp.data.ccmember_list;               
            });
        }
        $scope.addloanfacilitytype = function () {
            var params = {
                sanction_gid: sanction_gid
            };
            var url = 'api/IdasMstSanction/editvalidation';
            SocketService.getparams(url, params).then(function (resp) {             
                if (resp.data.totalloanfacility_amount == '') {
                    $scope.totalloanfacilityamount = "0,0";
                }
                else {
                    $scope.totalloanfacilit_amount = resp.data.totalloanfacility_amount;
                }
                if (resp.data.total_documentlimit == '') {
                    $scope.totaldocumentlimit_amount = "0,0";
                }
                else {
                    $scope.totaldocumentlimit_amount = resp.data.total_documentlimit;
                }
            });
            if ($scope.SanctionAmountEdit == undefined) {
                $scope.facility_pnl = false;
                $scope.addfacility_pnl = true;
                $scope.sanction_validation = true;
            }
            else {
                $scope.facility_pnl = true;
                $scope.addfacility_pnl = false;
            }          
        }

        $scope.changevaliditymnt = function (txtvalidity_monthsedit) {
            if ($scope.txtvalidity_monthsedit == undefined) {
                $scope.validitymonth = true;
            }
            else {
                $scope.validitymonth = false;
            }
        }

        $scope.loanfacilitytype_add = function () {
            if (!$scope.cboloanfacility_typeedit) {
                $scope.warningcbofacility = false;
            }
            else {
                $scope.warningcbofacility = true;
                if (!$scope.txtloanfacility_amount) {
                    $scope.warningfacility_amount = false;
                }
                else {
                    $scope.warningcbofacility = true;
                    $scope.warningfacility_amount = true;
                    var params =
                        {
                            loanfacility_gid: $scope.cboloanfacility_typeedit.loanmaster_gid,
                            loanfacility_type: $scope.cboloanfacility_typeedit.loanTitle,
                            loanfacility_amount: $scope.txtloanfacility_amount,
                            sanction_amount: $scope.SanctionAmountEdit,
                            totalloanfacility_amount: $scope.totalloanfacilityamount,
                            total_documentlimit: $scope.totaldocumentlimit_amount
                        }
                    var url = 'api/IdasMstSanction/postloanfacilitytype';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $scope.cbofacility_type = '';
                            $scope.txtloanfacility_amount = '';
                            Notify.alert(resp.data.message, 'success')
                            loanfacilitytype_list();
                            var params = {
                                sanction_gid: sanction_gid
                            };
                            var url = 'api/IdasMstSanction/editvalidation';
                            SocketService.getparams(url, params).then(function (resp) {                              
                                if (resp.data.totalloanfacility_amount == '') {
                                    $scope.totalloanfacility_amount = "0,0";
                                }
                                else {
                                    $scope.totalloanfacility_amount = resp.data.totalloanfacility_amount;
                                }
                            });
                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, 'warning')
                        }
                    });
                }
            }
        }
        $scope.onchangemandaotory = function () {
            $scope.mandatoryfields = false;
        }
        $scope.deleteloanfacility = function (sanction2loanfacilitytype_gid) {
            var params =
                {
                    sanction2loanfacilitytype_gid: sanction2loanfacilitytype_gid
                }

            var url = 'api/IdasMstSanction/deleteloanfacilitytype';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, 'success')
                    var params = {
                        sanction_gid: sanction_gid
                    };
                    var url = 'api/IdasMstSanction/editvalidation';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.totalloanfacility_amount == '') {
                            $scope.totalloanfacilityamount = "0,0";
                        }
                        else {
                            $scope.totalloanfacilityamount = resp.data.totalloanfacility_amount;
                        }
                        if (resp.data.total_documentlimit == '') {
                           $scope.totaldocumentlimitamount = "0,0";
                        }
                        else {
                            $scope.totaldocumentlimitamount = resp.data.total_documentlimit;
                        }
                        $scope.interchangeability_amount = resp.data.interchangeability_amount;
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, 'warning')
                }
                loanfacilitytype_list();
            });
        }
        function loanfacilitytype_list() {
            var params = {
                sanction_gid: sanction_gid
            };
            var url = 'api/IdasMstSanction/GetPenalInterest';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.penal_interest = resp.data.loanfacilitytype_list;

            });
            var params = {
                sanction_gid: sanction_gid
            };
            var url = 'api/IdasMstSanction/EditLoanfacilitytype';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loanfacilitytype_list = resp.data.loanfacilitytype_list;
                

            });
            //Facility Type Drop Down
            var url = 'api/IdasTrnLsaManagement/loanfacility';
            SocketService.get(url).then(function (resp) {
                $scope.loanfacility_list = resp.data.loanfacility_list;
            });
        }
     
        $scope.addloanfacility = function () {          
            if (($scope.txtloanfacility_amount == undefined) || ($scope.loanmaster_gid == undefined) || ($scope.txtdocument_limit == undefined) || ($scope.rdbrevolving == undefined) || ($scope.txtexpiry_date == undefined) || ($scope.rdbinterchangeability == undefined) || ($scope.rdbrevolving == "") || ($scope.rdbinterchangeability == "") || ($scope.txtexpiry_date == "") || ($scope.txtloanfacility_amount == "") || ($scope.txtdocument_limit == "") || ($scope.txtproposed_roi == undefined) || ($scope.txtproposed_roi == "")) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;
                if ($scope.rdbinterchangeability == 'No') {
                    if (($scope.cboapplicable_condition == undefined)|| ($scope.cboapplicable_condition == "")) {
                        $scope.mandatoryfields = true;
                    }
                    else {
                        $scope.mandatoryfields = false;
                        var params =
                                {
                                    loanfacility_gid: $scope.loanmaster_gid.loanmaster_gid,
                                    loanfacility_type: $scope.loanmaster_gid.loan_title,
                                    loanfacility_amount: $scope.txtloanfacility_amount,
                                    sanction_amount: $scope.SanctionAmountEdit,
                                    totalloanfacility_amount: $scope.totalloanfacilityamount,
                                    margin: $scope.txtmargin,
                                    document_limit: $scope.txtdocument_limit,
                                    tenure: $scope.txttenure,
                                    revolving_type: $scope.rdbrevolving,
                                    expiry_date: $scope.txtexpiry_date,
                                    interchangeability: $scope.rdbinterchangeability,
                                    report_structure: $scope.$parent.cboreport_structure,
                                    total_documentlimit: $scope.totaldocumentlimitamount,
                                    customer2sanction_gid: sanction_gid,
                                    applicable_condition: $scope.cboapplicable_condition,
                                    proposed_roi: $scope.txtproposed_roi
                                }
                         var url = 'api/IdasMstSanction/updateloanfacilitytype';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();
                                $scope.loanmaster_gid.loanmaster_gid = '';
                                $scope.loanmaster_gid.loan_title = '';
                                $scope.txtloanfacility_amount = '';
                                $scope.txtproposed_roi = '';
                                $scope.totalloanfacility_amount = '';
                                $scope.txtmargin = '';
                                $scope.txtdocument_limit = '';
                                $scope.txttenure = '';
                                $scope.rdbrevolving = '';
                                $scope.txtexpiry_date = '';
                                $scope.rdbinterchangeability = '';
                                $scope.$parent.cboreport_structure = '';
                                var params = {
                                    sanction_gid: sanction_gid
                                };
                                var url = 'api/IdasMstSanction/editvalidation';
                                SocketService.getparams(url, params).then(function (resp) {
                                     if (resp.data.totalloanfacility_amount == '') {
                                        $scope.totalloanfacilityamount = "0,0";
                                    }
                                    else {
                                        $scope.totalloanfacilityamount = resp.data.totalloanfacility_amount;
                                    }
                                    if (resp.data.total_documentlimit == '') {
                                        $scope.totaldocumentlimitamount = "0,0";
                                    }
                                    else {
                                        $scope.totaldocumentlimitamount = resp.data.total_documentlimit;
                                    }
                                    $scope.interchangeability_amount = resp.data.interchangeability_amount;
                                   });
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, 'warning')
                            }
                            loanfacilitytype_list();
                            $scope.facility_pnl = false;
                            $scope.addfacility_pnl = true;
                        });
                    }
                }
                else {
                    if (($scope.cboreport_structure == undefined) || ($scope.cboreport_structure == "")) {
                        $scope.mandatoryfields = true;
                    }
                    else {
                        $scope.mandatoryfields = false;
                        var params =
                                {
                                    loanfacility_gid: $scope.loanmaster_gid.loanmaster_gid,
                                    loanfacility_type: $scope.loanmaster_gid.loan_title,
                                    loanfacility_amount: $scope.txtloanfacility_amount,
                                    sanction_amount: $scope.SanctionAmountEdit,
                                    totalloanfacility_amount: $scope.totalloanfacilityamount,
                                    margin: $scope.txtmargin,
                                    document_limit: $scope.txtdocument_limit,
                                    tenure: $scope.txttenure,
                                    revolving_type: $scope.rdbrevolving,
                                    expiry_date: $scope.txtexpiry_date,
                                    interchangeability: $scope.rdbinterchangeability,
                                    report_structure: $scope.$parent.cboreport_structure,
                                    total_documentlimit: $scope.totaldocumentlimitamount,
                                    customer2sanction_gid: sanction_gid,
                                    applicable_condition: $scope.cboapplicable_condition,
                                    proposed_roi: $scope.txtproposed_roi
                                }
                        var url = 'api/IdasMstSanction/updateloanfacilitytype';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();
                                $scope.loanmaster_gid.loanmaster_gid = '';
                                $scope.loanmaster_gid.loan_title = '';
                                $scope.txtloanfacility_amount = '';
                                $scope.txtproposed_roi = '';
                                $scope.totalloanfacility_amount = '';
                                $scope.txtmargin = '';
                                $scope.txtdocument_limit = '';
                                $scope.txttenure = '';
                                $scope.rdbrevolving = '';
                                $scope.txtexpiry_date = '';
                                $scope.rdbinterchangeability = '';
                                $scope.$parent.cboreport_structure = '';
                                var params = {
                                    sanction_gid: sanction_gid
                                };
                                var url = 'api/IdasMstSanction/editvalidation';
                                SocketService.getparams(url, params).then(function (resp) {
                                    if (resp.data.totalloanfacility_amount == '') {
                                        $scope.totalloanfacilityamount = "0,0";
                                    }
                                    else {
                                        $scope.totalloanfacilityamount = resp.data.totalloanfacility_amount;
                                    }
                                    if (resp.data.total_documentlimit == '') {

                                        $scope.totaldocumentlimitamount = "0,0";
                                    }
                                    else {
                                        $scope.totaldocumentlimitamount = resp.data.total_documentlimit;
                                    }
                                    $scope.interchangeability_amount = resp.data.interchangeability_amount;
                                    });
                            }

                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, 'warning')
                            }
                            loanfacilitytype_list();
                            $scope.facility_pnl = false;
                            $scope.addfacility_pnl = true;
                        });
                    }
                }
            }
        }
       
        $scope.loanfacilitycancel = function () {
            $scope.facility_pnl = false;
            $scope.addfacility_pnl = true;
            $scope.loanmaster_gid.loanmaster_gid = '';
            $scope.loanmaster_gid.loan_title = '';
            $scope.txtloanfacility_amount = '';
            $scope.totalloanfacility_amount = '';
            $scope.txtmargin = '';
            $scope.txtdocument_limit = '';
            $scope.txttenure = '';
            $scope.rdbrevolving = '';
            $scope.txtexpiry_date = '';
            $scope.rdbinterchangeability = '';
            $scope.$parent.cboreport_structure = '';
        }
        $scope.cbofacility_msg = function () {
            $scope.warningcbofacility = true;

        }
        $scope.cbomember_msg = function () {
            $scope.warningcbomember = true;
        }
        $scope.facilitytype_amount = function () {
            var input = document.getElementById('txtInputloanfacility_type').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-IN');
            var lsloanfacilityamount_words = inWords(str);
            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtloanfacility_amount = "";
            }
            else {
                $scope.txtloanfacility_amount = output;
                $scope.warningfacility_amount = true;
                document.getElementById('loanfacilityamount_words').innerHTML = lsloanfacilityamount_words;
                if (($scope.SanctionAmountEdit.replace(/[\s,]+/g, '').trim() - $scope.totalloanfacilityamount.replace(/[\s,]+/g, '').trim()) >= ($scope.txtloanfacility_amount.replace(/[\s,]+/g, '').trim())) {
                    $scope.amount_validation = true;
                }
                else {
                    $scope.amount_validation = false;
                }
            }
            $scope.mandatoryfields = false;
        }
        $scope.documentlimitchange = function () {
            var input = document.getElementById('txtInput1').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-US');
            var lswords_documentlimit = inWords(str);
            var amount = new Intl.NumberFormat('en-IN').format(Number(str));
            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdocument_limit = "";

            }
            else {
                document.getElementById('documentlimitamount_words').innerHTML = lswords_documentlimit;
                $scope.txtdocument_limit = amount;

                if ((($scope.SanctionAmountEdit.replace(/[\s,]+/g, '').trim()) - ($scope.totaldocumentlimitamount.replace(/[\s,]+/g, '').trim())) < ($scope.txtdocument_limit.replace(/[\s,]+/g, '').trim())) {
                    $scope.panel1 = false;
                }
                else {
                    $scope.panel1 = true;
                }
            }
            $scope.mandatoryfields = false;
        }

        //ROI update event
        $scope.btnupdate = function (sanction2loanfacilitytype_gid, loanfacilityref_no, loanfacility_type) {
            var modalInstance = $modal.open({
                templateUrl: '/updateROI.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.loanfacility_type = loanfacility_type;
                $scope.loanfacilityref_no = loanfacilityref_no;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.UrnUpdate = function () {

                    var params = {
                        sanction2loanfacilitytype_gid: sanction2loanfacilitytype_gid,
                        proposed_roi: $scope.txtroi
                    }

                    lockUI();
                    var url = 'api/IdasMstSanction/UpdateProposedROI';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            loanfacilitytype_list();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            loanfacilitytype_list();
                        }
                        loanfacilitytype_list();
                    });
                }
            }
        }

        //Upload es_declaration available document
        $scope.uploades_declaration = function (val, val1, name) {
            if (($scope.es_declarationdocument_type == null) || ($scope.es_declarationdocument_type == '') || ($scope.es_declarationdocument_type == undefined)) {
                $("#adduploades_declaration").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_type', $scope.es_declarationdocument_type);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasMstSanction/Uploades_declarationdocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    if (resp.data.status == true) {
                        $("#adduploades_declaration").val('');
                        unlockUI();
                        $scope.es_declarationdocument_type = '';

                        var url = 'api/IdasMstSanction/Getesdocument';
                        SocketService.get(url).then(function (resp) {
                            $scope.uploadesfilename_list = resp.data.UploadES_DocumentList;
                        });

                        Notify.alert(resp.data.message, 'success')
                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }
                });
            }
        }

        $scope.esdownloaddocument = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }

        // Delete the Normal Document
        $scope.esdocument_cancelclick = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/IdasMstSanction/uploadesdocument_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.uploadesfilename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.uploadesfilename_list.splice(key, 1);
                        }
                    });
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


        //Upload Deviation Mail Document
        $scope.deviationmailupload = function (val, val1, name) {
            if (($scope.deviationmaildocument_type == null) || ($scope.deviationmaildocument_type == '') || ($scope.deviationmaildocument_type == undefined)) {
                $("#addmailupload").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_type', $scope.deviationmaildocument_type);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/IdasMstSanction/Uploadmaildocument';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    if (resp.data.status == true) {
                        $("#addmailupload").val('');
                        unlockUI();
                        var url = 'api/IdasMstSanction/GetMaildocument';
                        SocketService.get(url).then(function (resp) {
                            $scope.mailfilename_list = resp.data.DeviationMail_DocumentList;
                        });
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    else {
                        unlockUI();
                        Notify.alert(resp.data.message)
                    }

                });
            }
            
        }

        $scope.downloadmail = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);

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
        }

        $scope.maildocument_cancelclick = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/IdasMstSanction/Maildocument_delete';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    angular.forEach($scope.mailfilename_list, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.mailfilename_list.splice(key, 1);
                        }
                    });
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
    }
})();
