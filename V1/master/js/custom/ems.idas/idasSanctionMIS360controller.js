(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasSanctionMIS360controller', idasSanctionMIS360controller);

    idasSanctionMIS360controller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams'];

    function idasSanctionMIS360controller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasSanctionMIS360controller';
        $scope.sanction_gid = $location.search().sanction_gid;
        var sanction_gid = $scope.sanction_gid;

        activate();

        function activate() {

            var params = {
                sanction_gid: sanction_gid
            };
            lockUI();
            var url = 'api/IdasSanctionMIS/GetSanctionMISDetails';
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
                $scope.txtEditValidity = resp.data.validity_months;
                $scope.txtEditExpiryDate = resp.data.sanctionexpiry_Date;

                $scope.txtEditReviewDate = resp.data.sanctionreview_Date;

                $scope.earliersancrefnoEdit = resp.data.earlier_sanctionrefno;
                $scope.txtEditconstitution = resp.data.constitution;
                $scope.postalcode = resp.data.postalcode;
                $scope.customer_email = resp.data.customer_email;
                $scope.contact_person = resp.data.contact_person;
                $scope.txtrm_mobileno = resp.data.rm_mobileno;
                $scope.txtrm_emailid = resp.data.rm_email;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.txtEditexistingLimit = resp.data.existing_limit;
                $scope.txtEditAdditProposedLimit = resp.data.additional_proposedlimit;
                $scope.txtEditOverallLimit = resp.data.overall_limit;
                $scope.revisied_limit = resp.data.revisied_limit;
                $scope.txtEditpurpose = resp.data.purpose_lending;
                $scope.txtEdittenureMonths = resp.data.tenure_months;
                $scope.txtEditRepaymentPrinicipal = resp.data.repayment_principal;
                $scope.txtEditRepInterest = resp.data.repayment_interest;
                $scope.txtdateof_receiptofOriginalDoc = resp.data.dateof_receiptofOriginalDoc
                $scope.txtEditCollateralSecurity = resp.data.collateral_security;
                $scope.personal_guarantee = resp.data.personal_guarantee;
                $scope.txtEditMargin = resp.data.margin;
                $scope.txtEditRateofInterest = resp.data.rateof_interest;

                $scope.penal_interest = resp.data.penal_interest;
                //$scope.txtEditPenalInterest = resp.data.penal_interest;
                $scope.txtEditProcessingFee = resp.data.processing_fee;
                $scope.txtEditChequeRealization = resp.data.chequerealizationDate;
                $scope.txtEditDocumentationCharge = resp.data.documentation_clientvisitcharge;
                $scope.txtEditGSTNumber = resp.data.GST_number;
                $scope.receiptdocsDateEdit = resp.data.dateof_receiptDocsVetting;
                $scope.cboEditEscrowAccount = resp.data.escrow_account;
                $scope.txtEditVirtualAccountNo = resp.data.virtual_accountno;
                $scope.txtEditBuyersName = resp.data.nameofthe_buyers;
                $scope.txtEditStatusBAL = resp.data.status_ofBAL;
                $scope.dateofReleaseOrder = resp.data.releaseorder_Date;
                $scope.lblcasesvetted_bycadmaker = resp.data.casesvetted_bycadmaker;
                $scope.lblcasesvetted_bycadchecker = resp.data.casesvetted_bycadchecker;
                $scope.txtothers = resp.data.others;
                $scope.contactperson = resp.data.contactperson;
                $scope.mobileno = resp.data.mobileno;
                $scope.addressline1 = resp.data.addressline1;
                $scope.addressline2 = resp.data.addressline2;
                $scope.contact_number = resp.data.contact_no;
                $scope.lbldocumentation_list = resp.data.documentationlist;
                $scope.txthypothecation_date = resp.data.hypothecation_date;
                $scope.txtmortgage_date = resp.data.mortgage_date;
                $scope.txtprimaryvalue_chain = resp.data.primaryvalue_chain;
                $scope.txtsecondaryvalue_chain = resp.data.secondaryvalue_chain;
                $scope.txtpan_number = resp.data.pan_number;
                $scope.txtsanctiontype = resp.data.sanction_type;
                $scope.customersecurity_listdtl = resp.data.customersecurity_listdtl;
                $scope.sanction_status = resp.data.sanction_status;
                $scope.esdeclaration_status = resp.data.esdeclaration_status;
                $scope.es_application = resp.data.es_application;
                $scope.esrisk_categorization = resp.data.esrisk_categorization;
                $scope.colander_name = resp.data.colander_name;
                unlockUI();
            });


        }

        $scope.sanctionback = function () {
            $state.go('app.idasTrnSanctionMIS');
        }

    }
})();
