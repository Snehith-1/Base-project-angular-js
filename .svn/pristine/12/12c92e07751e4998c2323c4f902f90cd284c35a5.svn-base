(function () {
    'use strict';

    angular
        .module('angle')
        .controller('externalvisitReportGeneratecontroller', externalvisitReportGeneratecontroller);

        externalvisitReportGeneratecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function externalvisitReportGeneratecontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'externalvisitReportGeneratecontroller';

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
            var allocationdtl_gid = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
            }
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
               
                $scope.clientName = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.txtconstitution = resp.data.constitution;
               
                $scope.relationship_managername = resp.data.relationship_managername;
                $scope.credit_managername = resp.data.credit_managername;
                $scope.creditmanager_gid = resp.data.creditmanager_gid;
                $scope.relationship_managerGid = resp.data.relationship_managerGid;
                $scope.assigned_RMD = resp.data.assigned_RM;
                $scope.assignedRMD_gid = resp.data.assignedRM_gid;
                $scope.totaldisb_amount = resp.data.totaldisb_amount;
                $scope.txtPPA_name = resp.data.PPA_name;
                 
                if ($scope.totaldisb_amount != "")
                {
                    $scope.txtdisbursement_amount = resp.data.totaldisb_amount
                }
                if (resp.data.last_disb_date != "")
                {
                    $scope.txtdisbursement_date = resp.data.last_disb_date;
                  
                }
                if (resp.data.firstdisb_date != "")
                {
                    $scope.txtfirstdisb_date = resp.data.firstdisb_date;
                     
                }
                
            });

           
            var url = "api/visitReport/getvisitreportdtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreport_generateGid = resp.data.visitreport_generateGid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.visitdate = resp.data.visit_date;
                 
                if(resp.data.constitution!=null)
                {
                    $scope.txtconstitution = resp.data.constitution
                }
                if (resp.data.dealing_withsince!="0001-01-01T00:00:00")
                {
                    $scope.txtfirstdisb_date = resp.data.dealing_withsince
                }
                $scope.txtbusiness_vintage = resp.data.business_vintage,
                $scope.cbotypeof_loan = resp.data.typeof_loanvertical,
                $scope.cboriskcode = resp.data.risk_code,
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
                if (resp.data.RM_name != null)
                {
                    $scope.relationship_managername = resp.data.RM_name
                }
                if ($scope.txtPPA_name == "")
                {
                    $scope.txtPPA_name = resp.data.PPA_name;
                }
                
                if (resp.data.credit_managername != null) {
                    $scope.credit_managername = resp.data.credit_managername;
                }
                $scope.cbovisit_done = resp.data.visit_done,
                $scope.txtpurposeof_loan = resp.data.purpose_ofloan,
                $scope.txtrequestedloan_byclient = resp.data.requestedamount_byclient;
                if (resp.data.disbursement_date !="0001-01-01T00:00:00")
                {
                    $scope.txtdisbursement_date = resp.data.disbursement_date
                }
                if ($scope.txtdisbursement_date == "")
                {
                    $scope.txtdisbursement_amount = resp.data.disbursement_amount
                }
                $scope.txtlattitude = resp.data.visit_latitude;
                $scope.txtlongitude = resp.data.visit_longitude;
                $scope.txttotalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.cborepayment_track = resp.data.repayment_track,
                $scope.cbobasic_records = resp.data.basicrecords_maintain,
                $scope.txtturnover_lastfy = resp.data.turnover_lastFY,
                $scope.txtpresent_fysales = resp.data.presentFY_sales,
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
                $scope.txtportfolio_noofmember = resp.data.portfolio_noofmembers,
                $scope.txtportfolio_activemembers = resp.data.portfolio_activemembers,
                $scope.cborisk_reviewtype = resp.data.typeof_riskreview;
                $scope.txtportfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.txtbasicrecord_remarks = resp.data.basicrecords_remarks,
                $scope.txtportfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.txtportfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.txtportfolio_overdueAmount = resp.data.overdue_amount,
                $scope.txtportfolio_fundingoverdue = resp.data.overdueaccount_funding,
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
                        var indexs = $scope.visittypedtl.findIndex(x => x.vistdone_gid === resp.data.editvisittype[i].vistdone_gid);
                        $scope.cbovisit_done.push($scope.visittypedtl[indexs]);
                    }
                }
                if (resp.data.disbursement_amount != null) {
                    var str = resp.data.disbursement_amount.replace(/,/g, '');
                    var str = str.split('.')[0];
                    $scope.txtdisbursement_amount = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_disbursementamount').innerHTML = inWords(str);
                }
                else {
                    var str = $scope.txtdisbursement_amount.replace(/,/g, '');
                    var str = str.split('.')[0];
                    $scope.txtdisbursement_amount = Number(str).toLocaleString('en-IN');
                    document.getElementById('words_disbursementamount').innerHTML = inWords(str);
                }
            });

            var customer_gid = {
                customer_gid: localStorage.getItem('allocation_customer_gid')
            }
            var url = "api/visitReport/GetSanctionTenurePeriod";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.txtsantionloan_bycredit = resp.data.totalsanction_amount;
                angular.forEach($scope.sanctiondetails, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid,
                        allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
                    };

                    var url = 'api/allocationManagement/GetAllocateloanList';
                    SocketService.post(url, params).then(function (resp) {
                        value.loandetails = resp.data.loanList;
                        value.expand = false;
                    });
                });
            });
           
            var url = "api/visitReport/getvisitReportDocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreportdocument = resp.data.visitreportdocument;
            });
            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = "api/visitReport/getvisitReportPhoto";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreportphoto = resp.data.visitreportphoto;
                unlockUI();
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
                $scope.txtdisbursement_amount = $scope.totaldisb_amount;

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
                $scope.txtrequestedloan_byclient = "";
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
                $scope.txttotalloan_oustanding = "";
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
                $scope.txtturnover_lastfy = "";
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
                $scope.txtpresent_fysales = "";
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
                $scope.txtportfoliototal_loandisbursement = "";
            }
            else {
                $scope.txtportfoliototal_loandisbursement = output;
                document.getElementById('words_totalamount').innerHTML = lswords_totalamount;
            }
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
            lockUI();
            var params = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid'),
                visit_date: $scope.visitdate,
                visitDate: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                customer_gid: localStorage.getItem('allocation_customer_gid'),
                customer_name: $scope.clientName,
                constitution: $scope.txtconstitution,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                typeof_riskreview: $scope.cborisk_reviewtype,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
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
                report_status: 'Save'
            }
            
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

        $scope.VisitreportComplete = function () {
            var input = $scope.txtdisbursement_amount;
            //var arr = input.split(',');
            //var i;
            //for (i = 0; i < arr.length; i++) {
            //    var str = input.replace(',', '');
            //    input = str;
            //}
            lockUI();
            var params = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid'),
                customer_gid: localStorage.getItem('allocation_customer_gid'),
                customer_name: $scope.clientName,
                visit_date: $scope.visitdate,
                visitDate: $scope.visitdate,
                risk_code: $scope.cboriskcode,
                constitution: $scope.txtconstitution,
                visit_latitude: $scope.txtlattitude,
                visit_longitude: $scope.txtlongitude,
                typeof_riskreview: $scope.cborisk_reviewtype,
                dealing_withsince: $scope.txtfirstdisb_date,
                business_vintage: $scope.txtbusiness_vintage,
                typeof_loanvertical: $scope.cbotypeof_loan,
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
                report_status: 'Completed'
            }
            var url = "api/visitReport/postVisitReport"
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.caseAllocation');
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

        $scope.uploadvisitreport = function (val, val1, name) {
            lockUI();
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('allocationdtl_gid', localStorage.getItem('allocationdtl_gid'));
            $scope.uploadfrm = frm;
            var url = 'api/visitReport/visitReportUpload';

            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                var allocationdtl_gid = {
                    allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
                }

                var url = "api/visitReport/getvisitReportDocument";
                SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                    $scope.visitreportdocument = resp.data.visitreportdocument;
                });

                $("#addExternalupload").val('');
                $scope.txtdocument_type = "";

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

        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.uploadphoto = function (val, val1, name) {
            lockUI();
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('allocationdtl_gid', localStorage.getItem('allocationdtl_gid'));
            $scope.uploadfrm = frm;
            var url = 'api/visitReport/visitReportPhotoUpload';

            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $("#addPhotoupload").val('');

                var allocationdtl_gid = {
                    allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
                }
                var url = "api/visitReport/getvisitReportPhoto";
                SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
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

        $scope.uploaddocumentcancel = function (visitreport_documentGid) {
            lockUI();
            var params = {
                visitreport_documentGid: visitreport_documentGid
            }
            var url = 'api/visitReport/visitReportUploadcancel';
            SocketService.getparams(url, params).then(function (resp) {
                var allocationdtl_gid = {
                    allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
                }

                var url = "api/visitReport/getvisitReportDocument";
                SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
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

        $scope.uploadphotocancel = function (visitreport_photoGid) {
            lockUI();
            var params = {
                visitreport_photoGid: visitreport_photoGid
            }
            var url = 'api/visitReport/visitReportPhotocancel';
            SocketService.getparams(url, params).then(function (resp) {
                var allocationdtl_gid = {
                    allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
                }
                var url = "api/visitReport/getvisitReportPhoto";
                SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
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


        $scope.cancel = function () {
            $state.go('app.caseAllocation');
        }
    }
})();
