(function () {
    'use strict';

    angular
        .module('angle')
        .controller('visitReportCancelcontroller', visitReportCancelcontroller);

    visitReportCancelcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService','cmnfunctionService'];

    function visitReportCancelcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'visitReportCancelcontroller';
        var allocationdtl_gid = $location.search().allocationdtl_gid;
        var customer_gid = $location.search().allocation_customer_gid;
        
        activate();

        function activate() {
            lockUI();

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

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
            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open4 = true;
            };
            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open5 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, params).then(function (resp) {

                $scope.clientName = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;

                $scope.relationship_managername = resp.data.relationship_managername;
                $scope.credit_managername = resp.data.credit_managername;
                $scope.creditmanager_gid = resp.data.creditmanager_gid;
                $scope.relationship_managerGid = resp.data.relationship_managerGid;
                $scope.assigned_RMD = resp.data.assigned_RM;
                $scope.assignedRMD_gid = resp.data.assignedRM_gid;

            });


            var url = "api/visitReport/getvisitreportdtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreport_generateGid = resp.data.visitreport_generateGid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.visitdate = resp.data.visit_date;
                $scope.txtlattitude = resp.data.visit_latitude;
                $scope.txtlongitude = resp.data.visit_longitude;
                if (resp.data.constitution != null) {
                    $scope.txtconstitution = resp.data.constitution
                }
                if (resp.data.dealing_withsince != "0001-01-01T00:00:00") {
                    $scope.txtfirstdisb_date = resp.data.dealing_withsince
                }
                $scope.cboriskcode = resp.data.risk_code,
                $scope.cborisk_reviewtype = resp.data.typeof_riskreview;
                $scope.txtbusiness_vintage = resp.data.business_vintage,
                $scope.cbotypeof_loan = resp.data.typeof_loanvertical,
                $scope.txtbusiness_sector = resp.data.business_sector,
                $scope.txtregister_address = resp.data.registeredoffice_address,
                $scope.txtactual_address = resp.data.present_address,
                $scope.txtcontact_dtl1 = resp.data.contact_details1,
                $scope.txtcontact_dtl2 = resp.data.contact_details2,
                //$scope.firstdisb_date = resp.data.relationship_startedfrom
                $scope.txtbusiness_client = resp.data.clientbusiness_vintage,
                $scope.txtprimary_chain = resp.data.primarysecondary_valuechain,
                $scope.cbogenetic_code = resp.data.geneticcode_complied,
                $scope.cboRMD_gid = resp.data.RMD_visitedGid,
                $scope.RMD_visitedname = resp.data.RMD_visitedname;
                if (resp.data.RM_name != null) {
                    $scope.relationship_managername = resp.data.RM_name
                }
                if ($scope.txtPPA_name == "") {
                    $scope.txtPPA_name = resp.data.PPA_name;
                }

                if (resp.data.credit_managername != null) {
                    $scope.credit_managername = resp.data.credit_managername;
                }
                $scope.cbovisit_done = resp.data.visit_done,
                $scope.txtpurposeof_loan = resp.data.purpose_ofloan,
                $scope.txtrequestedloan_byclient = resp.data.requestedamount_byclient;
                $scope.requestedloan_byclient = resp.data.requestedamount_byclient;
                if (resp.data.disbursement_date != "0001-01-01T00:00:00") {
                    $scope.txtdisbursement_date = resp.data.disbursement_date
                }
                $scope.changedisbursement_amount = resp.data.disbursement_amount,
                $scope.txttotalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.totalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.cborepayment_track = resp.data.repayment_track,
                $scope.cbobasic_records = resp.data.basicrecords_maintain,
                $scope.txtturnover_lastfy = resp.data.turnover_lastFY,
                $scope.turnover_lastfy = resp.data.turnover_lastFY,
                $scope.txtpresent_fysales = resp.data.presentFY_sales,
                $scope.present_fysales = resp.data.presentFY_sales,
                $scope.txtdeferral_pendency = resp.data.deferral_pendency,
                $scope.txtcbototal_groups = resp.data.total_noofGroups,
                $scope.txtCBOgroup_funded = resp.data.CBOfunded_noofGroups,
                $scope.txtRMDvisit_groupcount = resp.data.RMD_visitgroups,
                $scope.txtassetverification_comment = resp.data.assetverification_createdoutofloan,
                $scope.txtsecurity_details = resp.data.assetverification_securitydtls,
                $scope.txtassetverification_mortagged = resp.data.assetverification_mortgaged,
                $scope.txtROCcreation = resp.data.assetverification_ROCcreation,
                $scope.txtpurpose_funding = resp.data.purposeof_funding,
                $scope.txt_utilisationdtls = resp.data.utilisation_details,
                $scope.txtadequacyloan_samunnati = resp.data.adequacy_loanamount,
                $scope.txtadequacyloan_impactassessment = resp.data.adequacy_impactassessment,
                $scope.txtadditional_funding = resp.data.adequacy_additionalfunding,
                $scope.txtbasicrecord_remarks = resp.data.basicrecords_remarks,
                $scope.txtportfolio_noofmember = resp.data.portfolio_noofmembers,
                $scope.txtportfolio_activemembers = resp.data.portfolio_activemembers,
                $scope.txtportfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.portfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.txtportfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.portfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.txtportfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.portfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.txtportfolio_overdueAmount = resp.data.overdue_amount,
                $scope.portfolio_overdueAmount = resp.data.overdue_amount,
                $scope.txtportfolio_fundingoverdue = resp.data.overdueaccount_funding,
                $scope.portfolio_fundingoverdue = resp.data.overdueaccount_funding,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                //$scope.txtloan_clientdate = resp.data.loan_clientdate,
                $scope.txtoverdue = resp.data.overdue,
                $scope.txtborrower_commitment = resp.data.borrower_commitment,
                $scope.txtpending_documentation = resp.data.pending_documentation,
                //$scope.txtasset_verification = resp.data.asset_verification,
                $scope.txtbriefdtls_client = resp.data.briefdtls_client,
                $scope.txtenduse_loan = resp.data.enduse_loan,
                //$scope.txtadequacy_loan = resp.data.adequacy_loan,
                $scope.txtoverall_remarks = resp.data.overall_remarks,
                $scope.txtPDD_compliance = resp.data.PDD_compliance,
                $scope.txtbriefrpt_financials = resp.data.briefrpt_financials,
                $scope.txtbriefrpt_process = resp.data.briefrpt_process,
                $scope.txtvaluechain_mapanalysis = resp.data.valuechain_mapanalysis,
                $scope.txtcompetitorbusiness_segment = resp.data.competitorbusiness_segment,
                $scope.txtbriefrpt_customer = resp.data.briefrpt_customer,
                $scope.txtbriefrpt_learnings = resp.data.briefrpt_learnings,
                $scope.txtbriefrpt_valuechain = resp.data.briefrpt_valuechain,
                $scope.visittypedtl = resp.data.visittype;
                $scope.txtrepayment_borrowings = resp.data.repayment_trackremarks,
                $scope.editvisittype = resp.data.editvisittype;
                $scope.cbovisit_done = [];
                if (resp.data.editvisittype != null) {
                    var count = resp.data.editvisittype.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.visittypedtl.map(function (x) { return x.vistdone_gid; }).indexOf(resp.data.editvisittype[i].vistdone_gid); 
                        $scope.cbovisit_done.push($scope.visittypedtl[indexs]);
                    }
                }
                if (resp.data.requestedamount_byclient != "" && resp.data.requestedamount_byclient !=null) {
                    
                    var str = resp.data.requestedamount_byclient.replace(/,/g, '');
                    $scope.txtrequestedloan_byclient = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_requestedloan').innerHTML = inWords(str);
                }
                if (resp.data.disbursement_amount != "" && resp.data.disbursement_amount != null) {
                    var str = resp.data.disbursement_amount.replace(/,/g, '');
                    var str = str.split('.')[0];
                    $scope.txtdisbursement_amount = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_disbursementamount').innerHTML = inWords(str);
                }
                if (resp.data.totalloan_outstanding != "" && resp.data.totalloan_outstanding !=null) {
                    var str = resp.data.totalloan_outstanding.replace(/,/g, '');
                    $scope.txttotalloan_oustanding = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_totalloan').innerHTML = inWords(str);
                }
                if (resp.data.presentFY_sales != "" && resp.data.presentFY_sales!=null) {
                    var str = resp.data.presentFY_sales.replace(/,/g, '');
                    $scope.txtpresent_fysales = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_presentFY').innerHTML = inWords(str);
                }
                if (resp.data.turnover_lastFY != "" && resp.data.turnover_lastFY!=null) {
                    var str = resp.data.turnover_lastFY.replace(/,/g, '');
                    $scope.txtturnover_lastfy = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_turnoverFY').innerHTML = inWords(str);
                }
                if (resp.data.total_disbursementamount != "" && resp.data.total_disbursementamount!=null) {
                    var str = resp.data.total_disbursementamount.replace(/,/g, '');
                    $scope.txtportfoliototal_loandisbursement = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_totalamount').innerHTML = inWords(str);
                }

                if (resp.data.outstanding_ondate != "" && resp.data.outstanding_ondate != null) {
                    var str = resp.data.outstanding_ondate.replace(/,/g, '');
                    $scope.txtportfolio_outstandingdate = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_outstandingdate').innerHTML = inWords(str);
                }
                if (resp.data.overdue_beneficiary != "" && resp.data.overdue_beneficiary != null) {
                    var str = resp.data.overdue_beneficiary.replace(/,/g, '');
                    $scope.txtportfolio_overduebeneficary = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_overduebeneficary').innerHTML = inWords(str);
                }
                if (resp.data.overdue_amount != "" && resp.data.overdue_amount != null) {
                    var str = resp.data.overdue_amount.replace(/,/g, '');
                    $scope.txtportfolio_overdueAmount = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_overdueAmount').innerHTML = inWords(str);
                }
                if (resp.data.overdueaccount_funding != "" && resp.data.overdueaccount_funding != null) {
                    var str = resp.data.overdueaccount_funding.replace(/,/g, '');
                    $scope.txtportfolio_fundingoverdue = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_fundingoverdue').innerHTML = inWords(str);
                }
            });

            var customer_gid = {
                customer_gid:customer_gid
            }
            var url = "api/visitReport/GetSanctionTenurePeriod";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.txtsantionloan_bycredit = resp.data.totalsanction_amount;
                angular.forEach($scope.sanctiondetails, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid,
                        allocationdtl_gid: allocationdtl_gid
                    };

                    var url = 'api/allocationManagement/GetAllocateloanList';
                    SocketService.post(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanList;
                        value.expand = false;
                    });
                });
            });

            var url = "api/visitReport/getvisitReportDocument";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreportdocument = resp.data.visitreportdocument;
            });
            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = "api/visitReport/getvisitReportPhoto";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreportphoto = resp.data.visitreportphoto;
                unlockUI();
            });


        }

        // Basic Details
        $scope.business_vintage = function (string) {
            if (string.length >= 64) {
                $scope.messagebusiness_vintage = "Allowed Only 64 Characters";
            }
            else {
                $scope.messagebusiness_vintage = ""
            }
        }
        $scope.business_sector = function (string) {
            if (string.length >= 128) {
                $scope.messagebusiness_sector = "Allowed Only 128 Characters";
            }
            else {
                $scope.messagebusiness_sector = ""
            }
        }
        $scope.register_address = function (string) {
            if (string.length >= 256) {
                $scope.messageregister_address = "Allowed Only 256 Characters";
            }
            else {
                $scope.messageregister_address = ""
            }
        }
        $scope.actual_address = function (string) {
            if (string.length >= 256) {
                $scope.messageactual_address = "Allowed Only 256 Characters";
            }
            else {
                $scope.messageactual_address = ""
            }
        }
        $scope.contact_dtl1 = function (string) {
            if (string.length >= 256) {
                $scope.messagecontact_dtl1 = "Allowed Only 256 Characters";
            }
            else {
                $scope.messagecontact_dtl1 = ""
            }
        }
        $scope.contact_dtl2 = function (string) {
            if (string.length >= 256) {
                $scope.messagecontact_dtl2 = "Allowed Only 256 Characters";
            }
            else {
                $scope.messagecontact_dtl2 = ""
            }
        }
        $scope.lattitude = function (string) {
            if (string.length >= 32) {
                $scope.message_lattitude = "Allowed Only 32 Characters";
            }
            else {
                $scope.message_lattitude = ""
            }
        }
        $scope.longitude = function (string) {
            if (string.length >= 32) {
                $scope.message_longitude = "Allowed Only 32 Characters";
            }
            else {
                $scope.message_longitude = ""
            }
        }

        // Visit Details
        $scope.primarychain = function (string) {
            if (string.length >= 128) {
                $scope.message = "Allowed Only 128 Characters";
            }
            else {
                $scope.message = ""
            }
        }
        $scope.purposeof_loan = function (string) {
            if (string.length >= 128) {
                $scope.message_loan = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_loan = ""
            }
        }
        $scope.overdue = function (string) {
            if (string.length >= 128) {
                $scope.message_overdue = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_overdue = ""
            }
        }
        $scope.repayment_borrowings = function (string) {
            if (string.length >= 128) {
                $scope.message_borrowings = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_borrowings = ""
            }
        }
        $scope.basicrecord_remarks = function (string) {
            if (string.length >= 128) {
                $scope.message_basicrecordremarks = "Allowed Only 128 Characters";
            }
            else {
                $scope.message_basicrecordremarks = ""
            }
        }
        $scope.deferral_pendency = function (string) {
            if (string.length >= 128) {
                $scope.messagedeferral_pendency = "Allowed Only 128 Characters";
            }
            else {
                $scope.messagedeferral_pendency = ""
            }
        }
        $scope.cbototal_groups = function (string) {
            if (string.length >= 64) {
                $scope.messagecbototal_groups = "Allowed Only 64 Characters";
            }
            else {
                $scope.messagecbototal_groups = ""
            }
        }
        $scope.CBOgroup_funded = function (string) {
            if (string.length >= 64) {
                $scope.messageCBOgroup_funded = "Allowed Only 64 Characters";
            }
            else {
                $scope.messageCBOgroup_funded = ""
            }
        }
        $scope.RMDvisit_groupcount = function (string) {
            if (string.length >= 64) {
                $scope.messageRMDvisit_groupcount = "Allowed Only 64 Characters";
            }
            else {
                $scope.messageRMDvisit_groupcount = ""
            }
        }
        $scope.borrower_commitment = function (string) {
            if (string.length >= 1024) {
                $scope.messageborrower_commitment = "Allowed Only 1024 Characters";
            }
            else {
                $scope.messageborrower_commitment = ""
            }
        }
        $scope.pending_documentation = function (string) {
            if (string.length >= 1024) {
                $scope.messagepending_documentation = "Allowed Only 1024 Characters";
            }
            else {
                $scope.messagepending_documentation = ""
            }
        }

        // Asset Verification
        $scope.purpose_funding = function (string) {
            if (string.length >= 512) {
                $scope.message_funding = "Allowed Only 512 Characters";
            }
            else {
                $scope.message_funding = ""
            }
        }
        $scope.utilisationdtls = function (string) {
            if (string.length >= 512) {
                $scope.message_utilisationdtls = "Allowed Only 512 Characters";
            }
            else {
                $scope.message_utilisationdtls = ""
            }
        }
        $scope.adequacyloan_samunnati = function (string) {
            if (string.length >= 256) {
                $scope.message_samunnati = "Allowed Only 256 Characters";
            }
            else {
                $scope.message_samunnati = ""
            }
        }
        $scope.adequacyloan_impactassessment = function (string) {
            if (string.length >= 256) {
                $scope.message_impactassessment = "Allowed Only 256 Characters";
            }
            else {
                $scope.message_impactassessment = ""
            }
        }
        $scope.additional_funding = function (string) {
            if (string.length >= 1024) {
                $scope.message_additionalfunding = "Allowed Only 1024 Characters";
            }
            else {
                $scope.message_additionalfunding = ""
            }
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

        $scope.disbursementchange = function () {
            var input = document.getElementById('disbursement_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_disbursementamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdisbursement_amount = $scope.changedisbursement_amount;

            }
            else {
                $scope.txtdisbursement_amount = output;
                document.getElementById('words_disbursementamount').innerHTML = lswords_disbursementamount;
            }

        }

        $scope.requestedloanamountChange = function () {
            var input = document.getElementById('requestloan_amount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_requestedloan = inWords(str);

            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtrequestedloan_byclient = $scope.requestedloan_byclient;
            }
            else {
                $scope.txtrequestedloan_byclient = output;
                document.getElementById('words_requestedloan').innerHTML = lswords_requestedloan;
            }
        }

        $scope.totalloanoustandingChange = function () {
            var input = document.getElementById('totalloan_oustanding').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalloan = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txttotalloan_oustanding = $scope.totalloan_oustanding;
            }
            else {
                $scope.txttotalloan_oustanding = output;
                document.getElementById('words_totalloan').innerHTML = lswords_totalloan;
            }
        }

        $scope.turnover_lastfyChange = function () {
            var input = document.getElementById('turnover_lastfy').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_turnoverFY = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtturnover_lastfy = $scope.turnover_lastfy;
            }
            else {
                $scope.txtturnover_lastfy = output;
                document.getElementById('words_turnoverFY').innerHTML = lswords_turnoverFY;
            }
        }

        $scope.present_fysalesChange = function () {
            var input = document.getElementById('present_fysales').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_presentFY = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtpresent_fysales = $scope.present_fysales;
            }
            else {
                $scope.txtpresent_fysales = output;
                document.getElementById('words_presentFY').innerHTML = lswords_presentFY;
            }
        }

        $scope.portfoliototal_loandisbursementChange = function () {
            var input = document.getElementById('portfoliototal_loandisbursement').value;
             var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfoliototal_loandisbursement = $scope.portfoliototal_loandisbursement;
            }
            else {
                $scope.txtportfoliototal_loandisbursement = output;
                document.getElementById('words_totalamount').innerHTML = lswords_totalamount;
            }
        }

        $scope.portfolio_outstandingdateChange = function () {
            var input = document.getElementById('portfolio_outstandingdate').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_outstandingdate = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_outstandingdate = "";
            }
            else {
                $scope.txtportfolio_outstandingdate = output;
                document.getElementById('words_outstandingdate').innerHTML = lswords_outstandingdate;
            }
        }

        $scope.portfolio_overduebeneficaryChange = function () {
            var input = document.getElementById('portfolio_overduebeneficary').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_overduebeneficary = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_overduebeneficary = "";
            }
            else {
                $scope.txtportfolio_overduebeneficary = output;
                document.getElementById('words_overduebeneficary').innerHTML = lswords_overduebeneficary;
            }
        }

        $scope.portfolio_overdueAmountChange = function () {
            var input = document.getElementById('portfolio_overdueAmount').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_overdueAmount = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_overdueAmount = "";
            }
            else {
                $scope.txtportfolio_overdueAmount = output;
                document.getElementById('words_overdueAmount').innerHTML = lswords_overdueAmount;
            }
        }

        $scope.portfolio_fundingoverdueChange = function () {
            var input = document.getElementById('portfolio_fundingoverdue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_fundingoverdue = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtportfolio_fundingoverdue = "";
            }
            else {
                $scope.txtportfolio_fundingoverdue = output;
                document.getElementById('words_fundingoverdue').innerHTML = lswords_fundingoverdue;
            }
        }

        $scope.VisitreportComplete = function () {
            var input = $scope.txtdisbursement_amount;

            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid,
                customer_gid:customer_gid,
                customer_name: $scope.clientName,
                visit_date: $scope.visitdate,
                visitDate: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                constitution: $scope.txtconstitution,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
                typeof_riskreview: $scope.cborisk_reviewtype,
                business_sector: $scope.txtbusiness_sector,
                registeredoffice_address: $scope.txtregister_address,
                present_address: $scope.txtactual_address,
                contact_details1: $scope.txtcontact_dtl1,
                contact_details2: $scope.txtcontact_dtl2,
                relationship_Startedfrom: $scope.txtfirstdisb_date,
                clientbusiness_vintage: $scope.txtbusiness_client,
                primarysecondary_valuechain: $scope.txtprimary_chain,
                basicrecords_remarks: $scope.txtbasicrecord_remarks,
                geneticcode_complied: $scope.cbogenetic_code,
                RMD_visitedGid: $scope.assignedRMD_gid,
                RMD_visitedname: $scope.assigned_RMD,
                RM_name: $scope.relationship_managername,
                PPA_name: $scope.txtPPA_name,
                credit_managername: $scope.credit_managername,
                visittype: $scope.cbovisit_done,
                purpose_ofloan: $scope.txtpurposeof_loan,
                requestedamount_byclient: $scope.txtrequestedloan_byclient,
                sanctionedamount_byclient: $scope.txtsantionloan_bycredit,
                disbursement_Date: $scope.txtdisbursement_date,
                disbursement_amount: input,
                totalloan_outstanding: $scope.txttotalloan_oustanding,
                repayment_track: $scope.cborepayment_track,
                repayment_trackremarks: $scope.txtrepayment_borrowings,
                basicrecords_maintain: $scope.cbobasic_records,
                turnover_lastFY: $scope.txtturnover_lastfy,
                presentFY_sales: $scope.txtpresent_fysales,
                deferral_pendency: $scope.txtdeferral_pendency,
                total_noofGroups: $scope.txtcbototal_groups,
                CBOfunded_noofGroups: $scope.txtCBOgroup_funded,
                RMD_visitgroups: $scope.txtRMDvisit_groupcount,
                assetverification_createdoutofloan: $scope.txtassetverification_comment,
                assetverification_securitydtls: $scope.txtsecurity_details,
                assetverification_mortgaged: $scope.txtassetverification_mortagged,
                assetverification_ROCcreation: $scope.txtROCcreation,
                purposeof_funding: $scope.txtpurpose_funding,
                utilisation_details: $scope.txt_utilisationdtls,
                adequacy_loanamount: $scope.txtadequacyloan_samunnati,
                adequacy_impactassessment: $scope.txtadequacyloan_impactassessment,
                adequacy_additionalfunding: $scope.txtadditional_funding,
                portfolio_noofmembers: $scope.txtportfolio_noofmember,
                portfolio_activemembers: $scope.txtportfolio_activemembers,
                total_disbursementamount: $scope.txtportfoliototal_loandisbursement,
                outstanding_ondate: $scope.txtportfolio_outstandingdate,
                overdue_beneficiary: $scope.txtportfolio_overduebeneficary,
                overdue_amount: $scope.txtportfolio_overdueAmount,
                overdueaccount_funding: $scope.txtportfolio_fundingoverdue,
                sanctioned_limit: $scope.txtsanctioned_limit,
                tenure_period: $scope.txttenure_period,
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                briefdtls_client: $scope.txtbriefdtls_client,
                overall_remarks: $scope.txtoverall_remarks,
                PDD_compliance: $scope.txtPDD_compliance,
                briefrpt_financials: $scope.txtbriefrpt_financials,
                briefrpt_process: $scope.txtbriefrpt_process,
                briefrpt_customer: $scope.txtbriefrpt_customer,
                briefrpt_learnings: $scope.txtbriefrpt_learnings,
                briefrpt_valuechain: $scope.txtbriefrpt_valuechain,
                valuechain_mapanalysis: $scope.txtvaluechain_mapanalysis,
                competitorbusiness_segment: $scope.txtcompetitorbusiness_segment,
                report_status: 'Completed'
            }
            var url = "api/VisitReportCancel/PostCancelReportSubmit"
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.rmVisitReport');
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
            unlockUI();
        }

        $scope.VisitreportbasicdtlSave = function (val) {
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,
                visit_date: $scope.visitdate,
                visitDate: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                customer_gid: customer_gid,
                customer_name: $scope.clientName,
                constitution: $scope.txtconstitution,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
                typeof_riskreview: $scope.cborisk_reviewtype,
                business_sector: $scope.txtbusiness_sector,
                registeredoffice_address: $scope.txtregister_address,
                present_address: $scope.txtactual_address,
                contact_details1: $scope.txtcontact_dtl1,
                contact_details2: $scope.txtcontact_dtl2,
                relationship_Startedfrom: $scope.txtfirstdisb_date,
                clientbusiness_vintage: $scope.txtbusiness_client,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.VisitreportvisitdtlSave = function (val) {
         
            var input = $scope.txtdisbursement_amount;
            
            var RMD_visitedname = $('#cboRMD_name :selected').text();
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,
                primarysecondary_valuechain: $scope.txtprimary_chain,
                geneticcode_complied: $scope.cbogenetic_code,
                RMD_visitedGid: $scope.assignedRMD_gid,
                RMD_visitedname: $scope.assigned_RMD,
                RM_name: $scope.relationship_managername,
                PPA_name: $scope.txtPPA_name,
                credit_managername: $scope.credit_managername,
                visittype: $scope.cbovisit_done,
                purpose_ofloan: $scope.txtpurposeof_loan,
                requestedamount_byclient: $scope.txtrequestedloan_byclient,
                sanctionedamount_byclient: $scope.txtsantionloan_bycredit,
                disbursement_Date: $scope.txtdisbursement_date,
                disbursement_amount: input,
                totalloan_outstanding: $scope.txttotalloan_oustanding,
                repayment_track: $scope.cborepayment_track,
                repayment_trackremarks: $scope.txtrepayment_borrowings,
                basicrecords_maintain: $scope.cbobasic_records,
                basicrecords_remarks: $scope.txtbasicrecord_remarks,
                turnover_lastFY: $scope.txtturnover_lastfy,
                presentFY_sales: $scope.txtpresent_fysales,
                deferral_pendency: $scope.txtdeferral_pendency,
                total_noofGroups: $scope.txtcbototal_groups,
                CBOfunded_noofGroups: $scope.txtCBOgroup_funded,
                RMD_visitgroups: $scope.txtRMDvisit_groupcount,    
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.VisitreportassetdtlSave = function (val) {
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,
                assetverification_createdoutofloan: $scope.txtassetverification_comment,
                assetverification_securitydtls: $scope.txtsecurity_details,
                assetverification_mortgaged: $scope.txtassetverification_mortagged,
                assetverification_ROCcreation: $scope.txtROCcreation,
                purposeof_funding: $scope.txtpurpose_funding,
                utilisation_details: $scope.txt_utilisationdtls,
                adequacy_loanamount: $scope.txtadequacyloan_samunnati,
                adequacy_impactassessment: $scope.txtadequacyloan_impactassessment,
                adequacy_additionalfunding: $scope.txtadditional_funding,
                sanctioned_limit: $scope.txtsanctioned_limit,
                tenure_period: $scope.txttenure_period,
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                briefdtls_client: $scope.txtbriefdtls_client,
                overall_remarks: $scope.txtoverall_remarks,
                PDD_compliance: $scope.txtPDD_compliance,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }
        

        $scope.VisitreportportfoliodtlSave = function (val) {
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,              
                portfolio_noofmembers: $scope.txtportfolio_noofmember,
                portfolio_activemembers: $scope.txtportfolio_activemembers,
                total_disbursementamount: $scope.txtportfoliototal_loandisbursement,
                outstanding_ondate: $scope.txtportfolio_outstandingdate,
                overdue_beneficiary: $scope.txtportfolio_overduebeneficary,
                overdue_amount: $scope.txtportfolio_overdueAmount,
                overdueaccount_funding: $scope.txtportfolio_fundingoverdue,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        
        $scope.VisitreportbriefdtlSave = function (val) {
           
            var params = {
                tab_name:val,
                allocationdtl_gid:allocationdtl_gid,              
                briefrpt_financials: $scope.txtbriefrpt_financials,
                briefrpt_process: $scope.txtbriefrpt_process,
                briefrpt_customer: $scope.txtbriefrpt_customer,
                briefrpt_learnings: $scope.txtbriefrpt_learnings,
                briefrpt_valuechain: $scope.txtbriefrpt_valuechain,
                valuechain_mapanalysis: $scope.txtvaluechain_mapanalysis,
                competitorbusiness_segment: $scope.txtcompetitorbusiness_segment,
                report_status: 'Save'
            }
            lockUI();
            var url = "api/visitReport/postVisitReportGenerate"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.VisitreportSave = function () {
            var input = $scope.txtdisbursement_amount;
            //var arr = input.split(',');
            //var i;
            //for (i = 0; i < arr.length; i++) {
            //    var str = input.replace(',', '');
            //    input = str;
            //}
            var RMD_visitedname = $('#cboRMD_name :selected').text();
           
            var params = {
                allocationdtl_gid:allocationdtl_gid,
                visit_date: $scope.visitdate,
                visitDate: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                customer_gid: customer_gid,
                customer_name: $scope.clientName,
                constitution: $scope.txtconstitution,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
                typeof_riskreview: $scope.cborisk_reviewtype,
                business_sector: $scope.txtbusiness_sector,
                registeredoffice_address: $scope.txtregister_address,
                present_address: $scope.txtactual_address,
                contact_details1: $scope.txtcontact_dtl1,
                contact_details2: $scope.txtcontact_dtl2,
                relationship_Startedfrom: $scope.txtfirstdisb_date,
                clientbusiness_vintage: $scope.txtbusiness_client,
                primarysecondary_valuechain: $scope.txtprimary_chain,
                geneticcode_complied: $scope.cbogenetic_code,
                RMD_visitedGid: $scope.assignedRMD_gid,
                RMD_visitedname: $scope.assigned_RMD,
                RM_name: $scope.relationship_managername,
                PPA_name: $scope.txtPPA_name,
                credit_managername: $scope.credit_managername,
                visittype: $scope.cbovisit_done,
                purpose_ofloan: $scope.txtpurposeof_loan,
                requestedamount_byclient: $scope.txtrequestedloan_byclient,
                sanctionedamount_byclient: $scope.txtsantionloan_bycredit,
                disbursement_Date: $scope.txtdisbursement_date,
                disbursement_amount: input,
                totalloan_outstanding: $scope.txttotalloan_oustanding,
                repayment_track: $scope.cborepayment_track,
                repayment_trackremarks: $scope.txtrepayment_borrowings,
                basicrecords_maintain: $scope.cbobasic_records,
                basicrecords_remarks: $scope.txtbasicrecord_remarks,
                turnover_lastFY: $scope.txtturnover_lastfy,
                presentFY_sales: $scope.txtpresent_fysales,
                deferral_pendency: $scope.txtdeferral_pendency,
                total_noofGroups: $scope.txtcbototal_groups,
                CBOfunded_noofGroups: $scope.txtCBOgroup_funded,
                RMD_visitgroups: $scope.txtRMDvisit_groupcount,
                assetverification_createdoutofloan: $scope.txtassetverification_comment,
                assetverification_securitydtls: $scope.txtsecurity_details,
                assetverification_mortgaged: $scope.txtassetverification_mortagged,
                assetverification_ROCcreation: $scope.txtROCcreation,
                purposeof_funding: $scope.txtpurpose_funding,
                utilisation_details: $scope.txt_utilisationdtls,
                adequacy_loanamount: $scope.txtadequacyloan_samunnati,
                adequacy_impactassessment: $scope.txtadequacyloan_impactassessment,
                adequacy_additionalfunding: $scope.txtadditional_funding,
                portfolio_noofmembers: $scope.txtportfolio_noofmember,
                portfolio_activemembers: $scope.txtportfolio_activemembers,
                total_disbursementamount: $scope.txtportfoliototal_loandisbursement,
                outstanding_ondate: $scope.txtportfolio_outstandingdate,
                overdue_beneficiary: $scope.txtportfolio_overduebeneficary,
                overdue_amount: $scope.txtportfolio_overdueAmount,
                overdueaccount_funding: $scope.txtportfolio_fundingoverdue,
                sanctioned_limit: $scope.txtsanctioned_limit,
                tenure_period: $scope.txttenure_period,
                overdue: $scope.txtoverdue,
                borrower_commitment: $scope.txtborrower_commitment,
                pending_documentation: $scope.txtpending_documentation,
                briefdtls_client: $scope.txtbriefdtls_client,
                overall_remarks: $scope.txtoverall_remarks,
                PDD_compliance: $scope.txtPDD_compliance,
                briefrpt_financials: $scope.txtbriefrpt_financials,
                briefrpt_process: $scope.txtbriefrpt_process,
                briefrpt_customer: $scope.txtbriefrpt_customer,
                briefrpt_learnings: $scope.txtbriefrpt_learnings,
                briefrpt_valuechain: $scope.txtbriefrpt_valuechain,
                valuechain_mapanalysis: $scope.txtvaluechain_mapanalysis,
                competitorbusiness_segment: $scope.txtcompetitorbusiness_segment,
                report_status: 'Save'
            }
        
            lockUI();
            var url = "api/visitReport/postVisitReport"
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();

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
            unlockUI();
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.uploadphoto = function (val, val1, name) {
            if (($scope.txtuploadphoto_title == null) || ($scope.txtuploadphoto_title == '') || ($scope.txtuploadphoto_title == undefined)) {
                $("#addPhotoupload").val('');
                Notify.alert('Kindly Enter the Photo Title', 'warning');
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
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "photoformatonly");

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
                // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "photoformatonly");

                // if (IsValidExtension == false) {
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('txtuploadphoto_title', $scope.txtuploadphoto_title);
                frm.append('allocationdtl_gid', allocationdtl_gid);
                frm.append('project_flag', "photoformatonly");
                $scope.uploadfrm = frm;
            }
        }
        $scope.VisitReportPhotoUpload = function () {
            if ($scope.uploadfrm != undefined)
            {
                lockUI();
                var url = 'api/visitReport/visitReportPhotoUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#addPhotoupload").val('');
                    $scope.txtuploadphoto_title = "";
                    var params = {
                        allocationdtl_gid: allocationdtl_gid
                    }
                    var url = "api/visitReport/getvisitReportPhoto";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.visitreportphoto = resp.data.visitreportphoto;
                    });

                    if (resp.data.status == true) {

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
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
        }

        $scope.uploaddocumentcancel = function (visitreport_documentGid) {
            lockUI();
            var params = {
                visitreport_documentGid: visitreport_documentGid
            }
            var url = 'api/visitReport/visitReportUploadcancel';
            SocketService.getparams(url, params).then(function (resp) {
                var params = {
                    allocationdtl_gid:allocationdtl_gid
                }

                var url = "api/visitReport/getvisitReportDocument";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.visitreportdocument = resp.data.visitreportdocument;
                });
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
                unlockUI();
            });
        }

        $scope.uploadvisitreport = function (val, val1, name) {
            if (($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#addExternalupload").val('');
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
                //     Notify.alert("File format is not supported..!", {
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
                frm.append('document_title', $scope.txtdocument_title);
                frm.append('allocationdtl_gid', allocationdtl_gid);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
            }
        }

        $scope.VisitReportDocumentUpload = function () {

            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/visitReport/visitReportUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    var params = {
                        allocationdtl_gid: allocationdtl_gid
                    }

                    var url = "api/visitReport/getvisitReportDocument";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.visitreportdocument = resp.data.visitreportdocument;
                    });

                    $("#addExternalupload").val('');
                    $scope.txtdocument_title = "";
                    $scope.txtdocument_type = "";
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
                            status: 'info',
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


        $scope.uploadphotocancel = function (visitreport_photoGid) {
            lockUI();
            var params = {
                visitreport_photoGid: visitreport_photoGid
            }
            var url = 'api/visitReport/visitReportPhotocancel';
            SocketService.getparams(url, params).then(function (resp) {
                var params = {
                    allocationdtl_gid: allocationdtl_gid
                }
                var url = "api/visitReport/getvisitReportPhoto";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.visitreportphoto = resp.data.visitreportphoto;
                });
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
                unlockUI();
            });
        }
    }
})();
