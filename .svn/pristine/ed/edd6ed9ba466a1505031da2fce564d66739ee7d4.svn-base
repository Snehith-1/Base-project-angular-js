(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewLSA', viewLSA);

    viewLSA.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams', '$parse', 'DownloaddocumentService'];

    function viewLSA($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, $parse, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewLSA';

        activate();

        function activate() {
            $scope.lsacreate_gid = localStorage.getItem('lsacreate_gid');
            $scope.stepone = false;
            $scope.steptwo = true;
            $scope.stepthree = true;
            $scope.stepfour = true;
            $scope.stepfive = true;
            $scope.stepsix = true;
            $scope.tobe_recovered = false;
            $scope.already_recovered = false;
            $scope.customer_pnl = true;
            $scope.sanction_pnl = true;
            $scope.signmatching = false;
            $scope.nach_no = false;
            $scope.signmatch_kycprovide = false;
            $scope.escrow_no = false;
            $scope.stamp = false;
            $scope.roc_no = false;

            var params = {
                lsacreate_gid: $scope.lsacreate_gid,

            };
            var url = 'api/IdasTrnLsaManagement/Getlsainfo';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.document_charge_flag = resp.data.document_charge_flag;
                $scope.recover_flag = resp.data.recover_flag;
                $scope.approval_status = resp.data.approval_status;
                $scope.customername = resp.data.customer_name;
                $scope.clarify_flag = resp.data.clarify_flag;
                $scope.compliance_flag = resp.data.compliance_flag;
                $scope.proceed_flag = resp.data.proceed_flag;
            });
            var url = 'api/IdasTrnLsaManagement/Getcustomer2sanctioninfo';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtmargin = resp.data.margin;
                $scope.txttenure = resp.data.tenure;
                $scope.txtexpiry_date = resp.data.expiry_date;
                $scope.txtrate_interest = resp.data.rate_interest;
            });
            var url = 'api/IdasTrnLsaManagement/limitinfodtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.limitinfo_limit = resp.data.limitinfo_limit;
                $scope.total_document_limit = resp.data.total_document_limit;
                $scope.totol_limit_released = resp.data.totol_limit_released;
               
            });
            var url = 'api/IdasTrnLsaManagement/bankinfodtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.bankinfo_list = resp.data.bankinfo_list;

            });
            var url = 'api/IdasTrnLsaManagement/GetPenalInterest';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.penalinterest_list = resp.data.loanfacilitytype_list;
                console.log(resp.data.loanfacilitytype_list)
            });

            var url = 'api/IdasTrnLsaManagement/loanfacility';
            SocketService.get(url).then(function (resp) {
                $scope.loanfacility_list = resp.data.loanfacility_list;
            });
            var url = 'api/IdasTrnLsaManagement/Getprocessingfeeinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.recovered_type = resp.data.recovered_type;
                $scope.recovered_amount = resp.data.recovered_amount;
                $scope.chequeno_details = resp.data.chequeno_details;
                $scope.chequedate_details = resp.data.chequedate_details;
                $scope.processingfeebank_name = resp.data.processingfeebank_name;
                $scope.processingfeaccount_name = resp.data.processingfeaccount_name;
                $scope.recover_remarks = resp.data.recover_remarks;
            });

            var url = 'api/IdasTrnLsaManagement/Getdocumentchargeinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.doc_recovered_amount = resp.data.doc_recovered_amount;
                $scope.doc_chequeno_details = resp.data.doc_chequeno_details;
                $scope.doc_chequedate_details = resp.data.doc_chequedate_details;
                $scope.doc_feebank_name = resp.data.doc_feebank_name;
                $scope.doc_feaccount_name = resp.data.doc_feaccount_name;
                $scope.document_name = resp.data.document_name;
                $scope.document_path = resp.data.document_path;
                $scope.document_charge = new Intl.NumberFormat('en-IN').format(Number(resp.data.document_charge)) + ".00";
                $scope.document_charge_gst = resp.data.document_charge_gst + "%";
                if (resp.data.document_name == null) {
                    $scope.document = false;
                }
                else {
                    $scope.document = true;
                }
                $scope.lbldocumentcharge_applicable = resp.data.documentcharge_applicable;
                $scope.lbldocumentcharge_remarks = resp.data.documentcharge_remarks;
            });
            var url = 'api/IdasTrnLsaManagement/Getmakerinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.maker_signature = resp.data.maker_signature;
                $scope.terms_conditions = resp.data.terms_conditions;
                $scope.deferral_captured = resp.data.deferral_captured;
                $scope.head = resp.data.head;

            });
            var url = 'api/IdasTrnLsaManagement/Getcompliancecheckinfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.nach_mandate = resp.data.nach_mandate;
                $scope.sign_match = resp.data.sign_match;
                $scope.sign_match_kyc = resp.data.sign_match_kyc;
                $scope.escrow_opened = resp.data.escrow_opened;
                $scope.appropriate_stamp = resp.data.nach_mandate;
                $scope.roc_filling = resp.data.roc_filling;
                $scope.nach_mandate_remarks = resp.data.nach_mandate_remarks;
                $scope.sign_match_remarks = resp.data.sign_match_remarks;
                $scope.sign_match_kyc_remarks = resp.data.sign_match_kyc_remarks;
                $scope.escrow_opened_remarks = resp.data.escrow_opened_remarks;
                $scope.appropriate_stamp_remarks = resp.data.appropriate_stamp_remarks;
                $scope.roc_filling_remarks = resp.data.roc_filling_remarks;
                $scope.cersai = resp.data.cersai;
                $scope.cersai_remarks = resp.data.cersai_remarks;
            });
            var url = 'api/IdasTrnLsaManagement/Getdocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.filename_list = resp.data.UploadDocumentList;
              
            });
            var url = "api/IdasTrnLsaManagement/GetdetailsLSA";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblcustomer_name = resp.data.customer_name;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblstate = resp.data.state;
                $scope.lblcustomer_urn = resp.data.customer_urn;
                $scope.lblcustomer_location = resp.data.customer_location;
                $scope.lbladdress = resp.data.address1;
                $scope.lblrm_name = resp.data.rm_name;
                $scope.lblcluster_head = resp.data.cluster_head;
                $scope.lblzonal_head = resp.data.zonal_head;
                $scope.lblbusiness_head = resp.data.business_head;
                $scope.lblcredit_manager = resp.data.credit_manager;
                $scope.lblvertical = resp.data.vertical;
                $scope.lblgst_no = resp.data.gst_no;
                $scope.lblpan_no = resp.data.pan_no;
                $scope.lblsa_code = resp.data.sa_code;
                $scope.lblsanctionref_no = resp.data.sanctionref_no;
                $scope.lblsanction_date = resp.data.sanction_date;
                $scope.lblapproved_by = resp.data.approved_by;
                $scope.lblapproved_date = resp.data.approved_date;
                $scope.lblconstitution = resp.data.constitution;
                $scope.lblpurpose_lending = resp.data.purpose_lending;
                $scope.lblfacility = resp.data.facility;
                $scope.lblmajor_corporate = resp.data.major_corporate;
                $scope.lblhypothecation_date = resp.data.hypothecation_date;
                $scope.lblmortgage_date = resp.data.mortgage_date;
                $scope.lblproduct_solution = resp.data.product_solution;
                $scope.lblmajot_intervention = resp.data.majot_intervention;
                $scope.lblsector = resp.data.sector;
                $scope.lblprimaryvalue_chain = resp.data.primaryvalue_chain;
                $scope.lblsecondaryvalue_chain = resp.data.secondaryvalue_chain;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbllsacreated_date = resp.data.lsacreated_date;
                $scope.lblsanction_type = resp.data.sanction_type;
                $scope.address1 = resp.data.address;
                $scope.lbllsaref_no = resp.data.lsaref_no;
                $scope.lsaapproved_by = resp.data.lsaapproved_by;
                $scope.lsaapproved_date = resp.data.lsaapproved_date;
               
                 });
            var url = 'api/IdasTrnLsaManagement/Getsanction2Colletarl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer2security_list = resp.data.customersecurity_list;
             
            });
        }

        $scope.downloads = function (val1, val2) {
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
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.clickstep1 = function () {
            $scope.stepone = false;
            $scope.steptwo = true;
            $scope.stepthree = true;
            $scope.stepfour = true;
            $scope.stepfive = true;
            $scope.stepsix = true;
        }
        $scope.clickstep2 = function () {
            $scope.stepone = true;
            $scope.steptwo = false;
            $scope.stepthree = true;
            $scope.stepfour = true;
            $scope.stepfive = true;
            $scope.stepsix = true;
        }
        $scope.clickstep3 = function () {
            $scope.stepone = true;
            $scope.steptwo = true;
            $scope.stepthree = false;
            $scope.stepfour = true;
            $scope.stepfive = true;
            $scope.stepsix = true;
        }
        $scope.clickstep4 = function () {
            $scope.stepone = true;
            $scope.steptwo = true;
            $scope.stepthree = true;
            $scope.stepfour = false;
            $scope.stepfive = true;
            $scope.stepsix = true;
        }
        $scope.clickstep5 = function () {
            $scope.stepone = true;
            $scope.steptwo = true;
            $scope.stepthree = true;
            $scope.stepfour = true;
            $scope.stepfive = false;
            $scope.stepsix = true;
        }
        $scope.clickstep6 = function () {
            $scope.stepone = true;
            $scope.steptwo = true;
            $scope.stepthree = true;
            $scope.stepfour = true;
            $scope.stepfive = true;
            $scope.stepsix = false;
        }

        $scope.lsaback = function () {
            $state.go('app.lsaManagement');
        }

        $scope.view_security = function (collateral_gid) {
          
            var collateral_gid = collateral_gid;
            var modalInstance = $modal.open({
                templateUrl: '/viewcollateral.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    collateral_gid: collateral_gid
                }
                var url = 'api/IdasTrnLsaManagement/GetColletarl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.security_type = resp.data.security_type;
                   
                    $scope.txtsecurity_description = resp.data.security_description;
                    $scope.cboaccount_status = resp.data.account_status;
                    $scope.collateralref_no = resp.data.collateralref_no;
                    $scope.security_code = resp.data.security_code;
                    $scope.collateral_gid = resp.data.collateral_gid;
                    $scope.txtborrowercheque_no = resp.data.borrowercheque_no;
                    $scope.txtborroweraccount_no = resp.data.borroweraccount_no;
                    $scope.txtborrowertbank_name = resp.data.borrowertbank_name;
                    $scope.txtborrowerdeviation = resp.data.borrowerdeviation;
                    $scope.txtborrowerother_remarks = resp.data.borrowerother_remarks;
                    $scope.txtguarantor_cheque = resp.data.guarantor_cheque;
                    $scope.txtguarantor_acno = resp.data.guarantor_acno;
                    $scope.txtguarantor_bankname = resp.data.guarantor_bankname;
                    $scope.txtguarantor_deviation = resp.data.guarantor_deviation;
                    $scope.txtpersonalguarantor_name = resp.data.personalguarantor_name;
                    $scope.txtguarantor_panno = resp.data.guarantor_panno;
                    $scope.txtcorporate_guarantee = resp.data.corporate_guarantee;
                    $scope.txtpersonal_guarantee = resp.data.personal_guarantee;
                    $scope.txtfd_bank_name = resp.data.fd_bank_name;
                    $scope.txtfd_no = resp.data.fd_no;
                    $scope.txtfd_expiry_date =(resp.data.fd_expiry_date);
                    $scope.txtauto_renewal = resp.data.auto_renewal;
                    $scope.txtbankguarantee_bankname = resp.data.bankguarantee_bankname;
                    $scope.txtbankguarantee_expirydate = (resp.data.bankguarantee_expirydate);
                    $scope.txtinsurancecompany_name = resp.data.insurancecompany_name;
                    $scope.txtpolicy_no = resp.data.policy_no;
                    $scope.txtpolicy_expiry_date = resp.data.policy_expiry_date;
                    
                    if (resp.data.security_type == 'UDC From Borrower') {
                        $scope.borrower = false;
                        $scope.guarantor = true;
                        $scope.insurance = true;
                        $scope.bank_guarantee = true;
                        $scope.fixed_deposits = true;
                        $scope.personal_guarantee = true;
                        $scope.corporate_guarantee = true;
                    }
                    else if (resp.data.security_type == 'UDC From Guarantor') {
                        $scope.guarantor = false;
                        $scope.borrower = true;
                        $scope.insurance = true;
                        $scope.bank_guarantee = true;
                        $scope.fixed_deposits = true;
                        $scope.personal_guarantee = true;
                        $scope.corporate_guarantee = true;
                    }
                    else if (resp.data.security_type == 'Personal Guarantee') {
                        $scope.personal_guarantee = false;
                        $scope.borrower = true;
                        $scope.guarantor = true;
                        $scope.insurance = true;
                        $scope.bank_guarantee = true;
                        $scope.fixed_deposits = true;
                        $scope.corporate_guarantee = true;
                    }
                    else if (resp.data.security_type == 'Corporate Guarantee') {
                        $scope.corporate_guarantee = false;
                        $scope.borrower = true;
                        $scope.guarantor = true;
                        $scope.insurance = true;
                        $scope.bank_guarantee = true;
                        $scope.fixed_deposits = true;
                        $scope.personal_guarantee = true;
                    }
                    else if (resp.data.security_type == 'Bank Guarantee') {
                        $scope.bank_guarantee = false;
                        $scope.borrower = true;
                        $scope.guarantor = true;
                        $scope.insurance = true;
                        $scope.fixed_deposits = true;
                        $scope.personal_guarantee = true;
                        $scope.corporate_guarantee = true;
                    }
                    else if (resp.data.security_type == 'Fixed Deposits') {
                        $scope.fixed_deposits = false;
                        $scope.borrower = true;
                        $scope.guarantor = true;
                        $scope.insurance = true;
                        $scope.bank_guarantee = true;
                        $scope.personal_guarantee = true;
                        $scope.corporate_guarantee = true;
                    }
                    else if (resp.data.security_type == 'Assignment of Insurance Policy') {
                        $scope.insurance = false;
                        $scope.borrower = true;
                        $scope.guarantor = true;
                        $scope.bank_guarantee = true;
                        $scope.fixed_deposits = true;
                        $scope.personal_guarantee = true;
                        $scope.corporate_guarantee = true;
                    }
                    else {
                        $scope.borrower = true;
                        $scope.guarantor = true;
                        $scope.insurance = true;
                        $scope.bank_guarantee = true;
                        $scope.fixed_deposits = true;
                        $scope.personal_guarantee = true;
                        $scope.corporate_guarantee = true;
                    }
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.borrowerinfo_submit = function () {
                
                    var params = {
                        borrower_chequeno: $scope.txtborrowercheque_no,
                        borrower_bankname: $scope.txborrowertbank_name,
                        borrower_acno: $scope.txtborroweraccount_no,
                        borrower_deviation: $scope.txtborrowerdeviation,
                        borrower_remarks: $scope.txtborrowerother_remarks,
                        lsacreate_gid: lsacreate_gid

                    }
                  
                    var url = 'api/IdasTrnLsaManagement/borrowerinfo';
                    SocketService.post(url, params).then(function (resp) {


                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');




                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        activate();
                    });


                }
            }
        }

        $scope.editlsa = function (lsacreate_gid)
        {
            
            //var lsacreate_gid = lsacreate_gid;
            var modalInstance = $modal.open({
                templateUrl: '/editlsainfo.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    lsacreate_gid: $scope.lsacreate_gid,
                    lsacreate_gid: lsacreate_gid,
                };
                console.log(params);
                var url = "api/IdasTrnLsaManagement/GetdetailsLSA";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblcustomer_name = resp.data.customer_name;                 
                    $scope.lblcustomer_urn = resp.data.customer_urn;               
                    $scope.lblsanctionref_no = resp.data.sanctionref_no;
                    $scope.lblsanction_date = resp.data.sanction_date;
                    $scope.txtsa_code = resp.data.sa_code;
                    console.log(resp.data.hypothecationdate)
                    if (resp.data.hypothecationdate != '0001-01-01T00:00:00')
                    {
                        $scope.txthypothecation_date = new Date(resp.data.hypothecationdate);
                    }
                    if (resp.data.mortgagedate != '0001-01-01T00:00:00') {
                        $scope.txtmortgage_date = new Date(resp.data.mortgagedate);
                    }                 
                   
                    $scope.txtremarks = resp.data.remarks;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_lsa = function () {

                    var params = {
                        remarks: $scope.txtremarks,
                        hypothecation_date: $scope.txthypothecation_date,
                        mortgage_date: $scope.txtmortgage_date,
                        sa_code:$scope.txtsa_code,
                        lsacreate_gid: lsacreate_gid

                    }
                    console.log(params);
                    var url = 'api/IdasTrnLsaManagement/updatelsa';
                    SocketService.post(url, params).then(function (resp) {


                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        activate();
                    });


                }
            }
        }
    }
})();
