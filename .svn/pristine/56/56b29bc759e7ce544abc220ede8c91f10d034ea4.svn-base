(function () {
    'use strict';

    angular
        .module('angle')
        .controller('VisitReportDetailViewcontroller', VisitReportDetailViewcontroller);

    VisitReportDetailViewcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','DownloaddocumentService'];

    function VisitReportDetailViewcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'VisitReportDetailViewcontroller';
        var allocationdtl_gid=$location.search().allocationdtl_gid;
     
        activate();

        function activate() {
            lockUI();
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }

            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.clientName = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;

            });

            var url = "api/visitReport/getvisitreportdtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreport_generateGid = resp.data.visitreport_generateGid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.txtbusiness_vintage = resp.data.business_vintage,
                $scope.cbotypeof_loan = resp.data.typeof_loanvertical,
                $scope.cboriskcode = resp.data.risk_code,
                //$scope.riskcode_classification = resp.data.riskcode_classification,
                $scope.cborisk_reviewtype = resp.data.typeof_riskreview;
                $scope.txtbusiness_sector = resp.data.business_sector,
                $scope.txtregister_address = resp.data.registeredoffice_address,
                $scope.txtactual_address = resp.data.present_address,
                $scope.txtcontact_dtl1 = resp.data.contact_details1,
                $scope.txtcontact_dtl2 = resp.data.contact_details2,
                 $scope.txtlattitude = resp.data.visit_latitude;
                $scope.txtlongitude = resp.data.visit_longitude;
                //$scope.firstdisb_date = resp.data.relationship_startedfrom
                $scope.txtbusiness_client = resp.data.clientbusiness_vintage,
                $scope.txtprimary_chain = resp.data.primarysecondary_valuechain,
                $scope.cbogenetic_code = resp.data.geneticcode_complied,
                $scope.cboRMD_gid = resp.data.RMD_visitedGid,
                $scope.RMD_visitedname = resp.data.RMD_visitedname;
                $scope.txtPPA_name = resp.data.PPA_name;
                $scope.cbovisit_done = resp.data.visit_done,
                $scope.txtpurposeof_loan = resp.data.purpose_ofloan,
                $scope.txtrequestedloan_byclient = resp.data.requestedamount_byclient,
                $scope.txtsantionloan_bycredit = resp.data.sanctionedamount_byclient;
                $scope.txtbasicrecords_remarks = resp.data.basicrecords_remarks;
                $scope.txtdisbursement_amount = resp.data.disbursement_amount,
                $scope.txttotalloan_oustanding = resp.data.totalloan_outstanding,
                $scope.cborepayment_track = resp.data.repayment_track,
                $scope.cbobasic_records = resp.data.basicrecords_maintain,
                $scope.txtturnover_lastfy = resp.data.turnover_lastFY,
                $scope.txtpresent_fysales = resp.data.presentFY_sales,
                $scope.txtdeferral_pendency = resp.data.deferral_pendency,
                $scope.txtadditional_funding = resp.data.adequacy_additionalfunding,
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
                $scope.txtportfoliototal_loandisbursement = resp.data.total_disbursementamount,
                $scope.txtportfolio_outstandingdate = resp.data.outstanding_ondate,
                $scope.txtportfolio_overduebeneficary = resp.data.overdue_beneficiary,
                $scope.txtportfolio_overdueAmount = resp.data.overdue_amount,
                $scope.txtportfolio_fundingoverdue = resp.data.overdueaccount_funding,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtsanctioned_limit = resp.data.sanctioned_limit,
                $scope.txttenure_period = resp.data.tenure_period,
                $scope.txtrepayment_trackremarks = resp.data.repayment_trackremarks,
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
                $scope.txtvaluechain_mapanalysis = resp.data.valuechain_mapanalysis,
                $scope.txtcompetitorbusiness_segment = resp.data.competitorbusiness_segment;
                $scope.editvisittype = resp.data.editvisittype;
                if (resp.data.constitution != null) {
                    $scope.constitution = resp.data.constitution
                }
                if (resp.data.RM_name != null) {
                    $scope.relationship_managername = resp.data.RM_name
                }
                if (resp.data.credit_managername != null) {
                    $scope.credit_managername = resp.data.credit_managername;
                }
                if (resp.data.visit_date != null) {
                    var p = resp.data.visit_date.split(/\D/g)
                    $scope.visitdate = [p[2], p[1], p[0]].join("-");
                }
               
                if (resp.data.dealing_withsince != null) {
                    var p = resp.data.dealing_withsince.split(/\D/g)
                    $scope.txtincorporated_date = [p[2], p[1], p[0]].join("-");
                }
                if (resp.data.disbursement_date != null) {
                    var p = resp.data.disbursement_date.split(/\D/g)
                    $scope.txtdisbursement_date = [p[2], p[1], p[0]].join("-");
                }
               
            });

            var url = "api/visitReport/getvisitReportDocument";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreportdocument = resp.data.visitreportdocument;
            });

            var url = "api/visitReport/GetSanctionTenurePeriod";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiondetails = resp.data.loandtl;
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

            var url = "api/visitReport/getvisitReportPhoto";
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visitreportphoto = resp.data.visitreportphoto;
            });

            var url = "api/VisitReportCancel/GetVisitCancelLog";
            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                $scope.visistreportcancel = resp.data.visistreportcancel;
            });
            unlockUI();
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }


        $scope.cancel = function () {
            $state.go('app.rmVisitReport');
        }
    }
})();
