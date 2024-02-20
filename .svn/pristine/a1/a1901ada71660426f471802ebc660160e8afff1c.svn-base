(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lsaapprovalviewController', lsaapprovalviewController);

    lsaapprovalviewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams', '$parse', 'DownloaddocumentService'];

    function lsaapprovalviewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, $parse, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'lsaapprovalviewController';

        activate();

        function activate() {
            $scope.lsacreate_gid = localStorage.getItem('lsacreate_gid');
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];

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
                $scope.MOMfilename_list = resp.data.MOMDocumentList;
                $scope.CAMfilename_list = resp.data.COMDocumentList;
                $scope.sanctionfilename_list = resp.data.SANDocumentList;
                $scope.generalfilename_list = resp.data.GeneralDocumentList;
                $scope.lsaapproved_by = resp.data.lsaapproved_by;
                $scope.lsaapproved_date = resp.data.lsaapproved_date;
            });
            var url = 'api/IdasTrnLsaManagement/Getsanction2Colletarl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer2security_list = resp.data.customersecurity_list;

            });

          
        }



        $scope.downloadsCAM = function (val1, val2) {
            //console.log(val1);
            //console.log(val2);
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

        $scope.downloadsMOM = function (val1, val2) {
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

       
        $scope.downloadsanctionletter = function (val1, val2) {
            ////var phyPath = val1;

            ////var relPath = phyPath.split("EMS");

            ////var relpath1 = relPath[1].replace("\\", "/");
            ////var hosts = window.location.host;
            ////var prefix = location.protocol + "//";
            ////var str = prefix.concat(hosts, relpath1);
            ////var link = document.createElement("a");
            ////var name = val2.split(".")
            ////link.download = val2;
            ////var uri = str;
            ////link.href = uri;
            ////link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadsgeneral = function (val1, val2) {
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

        
        $scope.lsaback = function (relpath1) {
            $location.url('app/idasTrnLSAapproval?lstab=' + relpath1);
           // $state.go('app.idasTrnLSAapproval');
        }
        $scope.lsaapprove = function (relpath1)
        {

            var params = {
                lsacreate_gid: $scope.lsacreate_gid,

            };
               
            var url = 'api/IdasTrnLsaManagement/postLSAstatusapprove';
                lockUI()
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI()
                        activate();
                        $location.url('app/idasTrnLSAapproval?lstab=' + relpath1);
                     //   $state.go('app.idasTrnLSAapproval');
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
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
     
        $scope.lsaback = function () {
            $state.go('app.idasTrnLSAapproval');
        }
       

        $scope.LSApendingpdf = function () {
            var params = {
                lsacreate_gid: $scope.lsacreate_gid,
            };
            var url = 'api/IdasTrnLsaManagement/GetLSApdf';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.file_path, "LSA Report.pdf");
                    Notify.alert('LSA Report Downloaded Successfully', 'success');
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
            });

        }
    }
})();
