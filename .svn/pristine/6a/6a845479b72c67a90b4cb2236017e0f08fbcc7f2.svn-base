(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationHistoryView', allocationHistoryView);

    allocationHistoryView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function allocationHistoryView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationHistoryView';
        activate();

        function activate() {
            lockUI();
            var allocationdtl_gid = {
                allocationdtl_gid: localStorage.getItem('allocationdtl_gid')
            }
            var url = "api/allocationManagement/getallocatedtls";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.zonal_name = resp.data.zonal_name;
                $scope.state_name = resp.data.state_name;
                $scope.district_name = resp.data.district_name;
                $scope.assigned_RM = resp.data.assigned_RM;
                $scope.customername = resp.data.customername;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.ZonalRMname = resp.data.ZonalRMname;
                $scope.clientName = resp.data.customername;
            });

            var url = "api/visitReport/GetAllocationLogDetail";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.scheduleList = resp.data.schedulelogdtl;
                $scope.calllogdtlList = resp.data.calllogdtl;

            });

            var url = "api/allocationManagement/GetAllocationCustomerDtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.customerdetails = resp.data;
                $scope.sanctiondetails = resp.data.loandtl;
                $scope.customerCollateral = resp.data.collateraldtl;
                $scope.holdallocationlist = resp.data.holdallocation;
                $scope.U2CMovedallocation = resp.data.U2CMovedallocation;
                $scope.customerguarantorlist = resp.data.Guarantorsdtl;
                $scope.customerPromotorlist = resp.data.Promoterdtl;
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

            var url = "api/customerManagement/HistoryEscrowSummary";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.escrowlist = resp.data.escrowSummary;
                }
            });

            var url = "api/allocationManagement/getAllocationdocument";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                if (Array.isArray(resp.data.upload_list) && resp.data.upload_list.length) {
                    $scope.upload_list = resp.data.upload_list;
                    $scope.documentUpload = true;
                }
                else {

                    $scope.documentNotUpload = true;
                }
            });

            var url = "api/visitReport/getvisitreportdtl";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
               
                $scope.visitreport_generateGid = resp.data.visitreport_generateGid;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.txtbusiness_vintage = resp.data.business_vintage,
                $scope.cbotypeof_loan = resp.data.typeof_loanvertical,
                $scope.txtbusiness_sector = resp.data.business_sector,
                $scope.txtregister_address = resp.data.registeredoffice_address,
                $scope.cboriskcode = resp.data.risk_code,
                $scope.txtactual_address = resp.data.present_address,
                $scope.txtcontact_dtl1 = resp.data.contact_details1,
                $scope.txtcontact_dtl2 = resp.data.contact_details2,
                $scope.cborisk_reviewtype = resp.data.typeof_riskreview;
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
                $scope.txtbasicrecord_remarks = resp.data.basicrecords_remarks,
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
                 $scope.txtvaluechain_mapanalysis = resp.data.valuechain_mapanalysis,
                $scope.txtcompetitorbusiness_segment = resp.data.competitorbusiness_segment;
                $scope.txtbriefrpt_learnings = resp.data.briefrpt_learnings,
                $scope.txtbriefrpt_valuechain = resp.data.briefrpt_valuechain,
                $scope.editvisittype = resp.data.editvisittype;
                if (resp.data.RM_name != null) {
                    $scope.relationship_managername = resp.data.RM_name
                }
                if (resp.data.constitution != null) {
                    $scope.constitution = resp.data.constitution
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
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreportdocument = resp.data.visitreportdocument;
            });

            var url = "api/visitReport/getvisitReportPhoto";
            SocketService.getparams(url, allocationdtl_gid).then(function (resp) {
                $scope.visitreportphoto = resp.data.visitreportphoto;
                unlockUI();
            });
        }

        $scope.escrowInfoView = function (escrow_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EscrowInfoModal.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    escrow_gid: escrow_gid
                }
                var url = "api/customerManagement/HistoryEscrowView";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.escrowview = resp.data;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.scheduleLoghistory = function (schedulelog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/SchedulehistoryModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    schedulelog_gid: schedulelog_gid
                }
                var url = "api/visitReport/GetScheduleLogHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.scheduleList = resp.data.schedulelogdtl;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }

        $scope.downloads = function (val1, val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();
