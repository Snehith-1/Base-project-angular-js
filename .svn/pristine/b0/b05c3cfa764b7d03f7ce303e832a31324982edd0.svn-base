(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnSanctionDashboard', idasTrnSanctionDashboard);

    idasTrnSanctionDashboard.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route'];

    function idasTrnSanctionDashboard($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnSanctionDashboard';

        activate();

        function activate() {
            vm.calenderEdit = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openEdit = true;
            };
            vm.dateOptionsEdit = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

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
            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open6 = true;
            };

            vm.calender7 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open7 = true;
            };

            var url = 'api/loan/loan_list';
            SocketService.get(url).then(function (resp) {
                $scope.loan_list = resp.data.loanmasterdtls;
            });

            var url = 'api/newServiceTicket/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
 
            var params = {
                sanction_gid: localStorage.getItem('sanction_gid')
            };
            lockUI();
            var url = 'api/IdasMstSanction/SanctionDtlsEdit';
            SocketService.getparams(url, params).then(function (resp) {
                console.log(resp);
                $scope.txtsanctionrefnoEdit = resp.data.sanction_refno;
                $scope.SanctionDateEdit = resp.data.sanction_date;
                $scope.txtSanctionAmountEdit = resp.data.sanction_amount;
                $scope.txtSanctionLimitEdit = resp.data.sanction_limit;
                $scope.entityedit = resp.data.entity;
                $scope.cboFacilityTypeEdit = resp.data.facility_type;

                $scope.customerNameEdit = resp.data.customername;
                $scope.CustomerurnEdit = resp.data.customer_urn;

                $scope.txtcollateralEdit = resp.data.collateral_security;
                $scope.zonalHeadNameEdit = resp.data.zonal_name;
                $scope.businessHeadNameEdit = resp.data.businesshead_name;
                $scope.clusterManagerEdit = resp.data.cluster_manager_name;
                $scope.creditManagerEdit = resp.data.creditmanager_name;
                $scope.relationshipmgmtEdit = resp.data.relationshipmgmt_name;
                $scope.txtapprovalauthority = resp.data.approval_authority;
                $scope.verticalCodeEdit = resp.data.vertical;
                $scope.txtstate = resp.data.state;
                $scope.loan_type = resp.data.loan_type;
                $scope.txtccapproval_date = resp.data.ccapproval_date;
                $scope.txtnatureofproposal = resp.data.nature_ofproposal;
                $scope.txtEditclassificationofMSME = resp.data.classification_MSME;
                $scope.txtEditValidity = resp.data.sanction_validity;
                $scope.txtEditExpiryDate = resp.data.sanctionexpiry_Date;

                $scope.txtEditReviewDate = resp.data.sanctionreview_Date;

                $scope.earliersancrefnoEdit = resp.data.earlier_sanctionrefno;
                $scope.txtEditconstitution = resp.data.constitution;
                $scope.pincode = resp.data.pincode;
                $scope.contact_number = resp.data.contact_number;
                $scope.email_id = resp.data.email_id;
                $scope.contact_person = resp.data.contact_person;
                $scope.txtrm_phoneno = resp.data.rm_phoneno;
                $scope.txtrm_emailid = resp.data.rm_emailid;
                $scope.cboEditauthorizedsignatory = resp.data.authorized_signatory;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.txtEditexistingLimit = resp.data.existing_limit;
                $scope.txtEditAdditProposedLimit = resp.data.additional_proposedlimit;
                $scope.txtEditOverallLimit = resp.data.overall_limit;
                $scope.revisied_limit = resp.data.revisied_limit;
                $scope.txtEditpurpose = resp.data.purpose;
                $scope.txtEdittenureMonths = resp.data.tenure_months;
                $scope.txtEditRepaymentPrinicipal = resp.data.repayment_principal;
                $scope.txtEditRepInterest = resp.data.repayment_interest;
                $scope.txtEditPrimarySecurity = resp.data.primary_security;
                $scope.txtEditCollateralSecurity = resp.data.collateral_security;
                $scope.personal_guarantee = resp.data.personal_guarantee;
                $scope.txtEditSecurityBank = resp.data.securitycheque_bankname;
                $scope.txtEditSecurityAccount = resp.data.securitycheque_accountnumber;
                $scope.txtEditMargin = resp.data.margin;
                $scope.txtEditRateofInterest = resp.data.rateof_interest;
                $scope.txtEditPenalInterest = resp.data.penal_interest;
                $scope.txtEditProcessingFee = resp.data.processing_fee;
                $scope.txtEditBankCheque = resp.data.bankand_chequeno;
                $scope.txtEditChequeRealization = resp.data.chequerealizationDate;
                $scope.txtEditDocumentationCharge = resp.data.documentation_clientvisitcharge;
                $scope.txtEditGSTNumber = resp.data.GST_number;
                $scope.txtEditModeOperation = resp.data.modeof_operations;
                $scope.txtEditspecificcondition = resp.data.specific_condition;
                $scope.receiptdocsDateEdit = resp.data.dateof_receiptDocsVetting;
                $scope.txtEditNACHForm = resp.data.NACH_form;
                $scope.cboEditEscrowAccount = resp.data.escrow_account;
                $scope.txtEditVirtualAccountNo = resp.data.virtual_accountno;
                $scope.txtEditBuyersName = resp.data.nameofthe_buyers;
                $scope.txtEditStatusBAL = resp.data.status_ofBAL;
                $scope.txtEditROCApplicable = resp.data.roc_applicable;
                $scope.txtEditROCStatus = resp.data.roc_status;
                $scope.txtEditcersai = resp.data.cersai_status;
                $scope.txtEditNesl = resp.data.nesl_status;
                $scope.txtEditPreDisburse = resp.data.predisbursement_deferal;
                $scope.dateofdeviation = resp.data.deviation_Date;
                $scope.txtEditPreDisburseStatus = resp.data.statuspre_disbursementdeferal;
                $scope.txtEditPostDisb = resp.data.postdisbursement_covanent;
                $scope.txtEditPostDisbStatus = resp.data.statuspost_disbursementcovanent;
                $scope.dateofReleaseOrder = resp.data.releaseorder_Date;
                $scope.txtroissuing_totalamount = resp.data.roissuing_totalamount;
                $scope.txtcasesvetted_bycad = resp.data.casesvetted_bycad;
                $scope.txtEditoriginaldocs_receivedHO = resp.data.originaldocs_receivedHO;
                $scope.txtscanneduploaded_Drive = resp.data.scanneduploaded_Drive;
                $scope.txtmonitoring_visit = resp.data.monitoring_visit;
                $scope.txtbank_statement = resp.data.bank_statement;
                $scope.txtaudited_financials = resp.data.audited_financials;
                $scope.txtstock_statement = resp.data.stock_statement;
                $scope.txtpurchase_statement = resp.data.purchase_statement;
                $scope.txtsales_statement = resp.data.sales_statement;
                $scope.txtdebtors_statement = resp.data.debtors_statement;
                $scope.txtprovisionalfinancial_gst = resp.data.provisionalfinancial_gst;
                $scope.txtroc_30daysfromSLonetime = resp.data.roc_30daysfromSLonetime;
                $scope.txtnoliability_certificate = resp.data.noliability_certificate;
                $scope.txtbuyerconfirmation_letter = resp.data.buyerconfirmation_letter;
                $scope.txtcopyof_warehousereceipt = resp.data.copyof_warehousereceipt;
                $scope.txtinsurance_30daysfromSLonetime = resp.data.insurance_30daysfromSLonetime;
                $scope.txtloandisbursement_dtlfarmermember = resp.data.loandisbursement_dtlfarmermember;
                $scope.txtothers = resp.data.others;
                $scope.contactperson = resp.data.contactperson;
                $scope.mobileno = resp.data.mobileno;
                $scope.addressline1 = resp.data.addressline1;
                $scope.addressline2 = resp.data.addressline2;
                unlockUI();
            });


        }

        $scope.sanctiondetails1Update = function () {
            lockUI();
            var params = {
                approval_authority: $scope.txtapprovalauthority,
                CCapproval_date: $scope.txtccapproval_date,
                earlier_sanctionrefno: $scope.earliersancrefnoEdit,
                nature_ofproposal: $scope.txtnatureofproposal,
                classification_MSME: $scope.txtEditclassificationofMSME,
                sanction_validity: $scope.txtEditValidity,
                sanction_expirydate: $scope.txtEditExpiryDate,
                sanction_reviewdate: $scope.txtEditReviewDate,
                constitution: $scope.txtEditconstitution,
                authorized_signatory: $scope.cboEditauthorizedsignatory,
                existing_limit: $scope.txtEditexistingLimit,
                additional_proposedlimit: $scope.txtEditAdditProposedLimit,
                overall_limit: $scope.txtEditOverallLimit,
                purpose: $scope.txtEditpurpose,
                tenure_months: $scope.txtEdittenureMonths,
                repayment_principal: $scope.txtEditRepaymentPrinicipal,
                repayment_interest: $scope.txtEditRepInterest,
                primary_security: $scope.txtEditPrimarySecurity,
                collateral_security: $scope.txtEditCollateralSecurity,
                customer2sanction_gid: localStorage.getItem('sanction_gid')
            }

            var url = "api/IdasMstSanction/PostSanctionDetails1"
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
        }

        $scope.sanctiondetails2Update = function () {
            lockUI();
            var params = {
                securitycheque_bankname: $scope.txtEditSecurityBank,
                securitycheque_accountnumber: $scope.txtEditSecurityAccount,
                margin: $scope.txtEditMargin,
                rateof_interest: $scope.txtEditRateofInterest,
                penal_interest: $scope.txtEditPenalInterest,
                processing_fee: $scope.txtEditProcessingFee,
                bankand_chequeno: $scope.txtEditBankCheque,
                cheque_realizationdate: $scope.txtEditChequeRealization,
                documentation_clientvisitcharge: $scope.txtEditDocumentationCharge,
                GST_number: $scope.txtEditGSTNumber,
                modeof_operations: $scope.txtEditModeOperation,
                specific_condition: $scope.txtEditspecificcondition,
                dateof_receiptDocsVetting: $scope.receiptdocsDateEdit,
                NACH_form: $scope.txtEditNACHForm,
                escrow_account: $scope.cboEditEscrowAccount,
                virtual_accountno: $scope.txtEditVirtualAccountNo,
                nameofthe_buyers: $scope.txtEditBuyersName,
                status_ofBAL: $scope.txtEditStatusBAL,
                roc_applicable: $scope.txtEditROCApplicable,
                roc_status: $scope.txtEditROCStatus,
                cersai_status: $scope.txtEditcersai,
                nesl_status: $scope.txtEditNesl,
                customer2sanction_gid: localStorage.getItem('sanction_gid')
            }

            var url = "api/IdasMstSanction/PostSanctionDetails2"
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
        }

        $scope.sanctiondetails3Update = function () {
            lockUI();
            var params = {
                predisbursement_deferal: $scope.txtEditPreDisburse,
                dateof_deviation: $scope.dateofdeviation,
                statuspre_disbursementdeferal: $scope.txtEditPreDisburseStatus,
                postdisbursement_covanent: $scope.txtEditPostDisb,
                statuspost_disbursementcovanent: $scope.txtEditPostDisbStatus,
                dateof_releaseorder: $scope.dateofReleaseOrder,
                roissuing_totalamount: $scope.txtroissuing_totalamount,
                casesvetted_bycad: $scope.txtcasesvetted_bycad,
                originaldocs_receivedHO: $scope.txtEditoriginaldocs_receivedHO,
                scanneduploaded_Drive: $scope.txtscanneduploaded_Drive,
                monitoring_visit: $scope.txtmonitoring_visit,
                bank_statement: $scope.txtbank_statement,
                audited_financials: $scope.txtaudited_financials,
                stock_statement: $scope.txtstock_statement,
                purchase_statement: $scope.txtpurchase_statement,
                sales_statement: $scope.txtsales_statement,
                debtors_statement: $scope.txtdebtors_statement,
                provisionalfinancial_gst: $scope.txtprovisionalfinancial_gst,
                roc_30daysfromSLonetime: $scope.txtroc_30daysfromSLonetime,
                noliability_certificate: $scope.txtnoliability_certificate,
                buyerconfirmation_letter: $scope.txtbuyerconfirmation_letter,
                copyof_warehousereceipt: $scope.txtcopyof_warehousereceipt,
                insurance_30daysfromSLonetime: $scope.txtinsurance_30daysfromSLonetime,
                loandisbursement_dtlfarmermember: $scope.txtloandisbursement_dtlfarmermember,
                others: $scope.txtothers,
                customer2sanction_gid: localStorage.getItem('sanction_gid')
            }

            var url = "api/IdasMstSanction/PostSanctionDetails3"
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
        }


        $scope.sanctionback = function () {
            $state.go('app.idasTrnSanctionMIS');
        }

 
    }
})();
