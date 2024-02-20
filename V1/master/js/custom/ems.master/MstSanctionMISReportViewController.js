(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionMISReportViewController', MstSanctionMISReportViewController);

    MstSanctionMISReportViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstSanctionMISReportViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSanctionMISReportViewController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.lsreportpage = $location.search().lsreportpage;
        var lsreportpage = $scope.lsreportpage;
        
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;     
        $scope.application2sanction_gid = $location.search().application2sanction_gid;
        var application2sanction_gid = $location.search().application2sanction_gid;
        $scope.sanctionapprovallog_gid = $location.search().sanctionapprovallog_gid;
        var sanctionapprovallog_gid = $location.search().sanctionapprovallog_gid;

        activate();

        function activate() {
          
            var url = 'api/MstApplicationReport/CADSanctionDtls';
            var params = {
                application2sanction_gid: application2sanction_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanction_refno = resp.data.sanction_refno;
                $scope.SanctionDateEdit = resp.data.sanctionDate;
                $scope.sanction_date = resp.data.sanction_date;
                $scope.sanction_amount = resp.data.sanction_amount;
                $scope.sanctionto_name = resp.data.sanctionto_name;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.application_no = resp.data.application_no;

                $scope.application_type = resp.data.application_type;
                $scope.contactperson_address = resp.data.contactperson_address;
                $scope.contactperson_name = resp.data.contactperson_name;
                $scope.contactperson_number = resp.data.contactperson_number;
                $scope.contactpersonemail_address = resp.data.contactpersonemail_address;
                $scope.sanctionfrom_date = resp.data.sanctionfrom_date;
                $scope.sanctiontill_date = resp.data.sanctiontill_date;
                $scope.paycard = resp.data.paycard;
                $scope.sanction_type = resp.data.sanction_type;
                $scope.natureof_proposal = resp.data.natureof_proposal;
                $scope.branch_name = resp.data.branch_name;
                $scope.esdeclaration_status = resp.data.esdeclaration_status;

            });

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var url = 'api/MstApplicationReport/GetTemplateDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lspath = resp.data.makerfile_path;
                    $scope.lsname = resp.data.makerfile_name;
                    $scope.content = resp.data.template_content;
                    $scope.checkerletter_flag = resp.data.checkerletter_flag;
                    $scope.checkerapproval_flag = resp.data.checkerapproval_flag;
                    $scope.sanctionletter_flag = resp.data.sanctionletter_flag;
                    $scope.sanctionletter_status = resp.data.sanctionletter_status;
                    $scope.digitalsignature_flag = resp.data.digitalsignature_flag;
                    $scope.checkerupdated_by = resp.data.checkerupdated_by;
                    $scope.checkerupdated_on = resp.data.checkerupdated_on;
                    $scope.makersubmitted_by = resp.data.makersubmitted_by;
                    $scope.makersubmitted_on = resp.data.makersubmitted_on;
                    $scope.approved_by = resp.data.approved_by;
                    $scope.approved_date = resp.data.approved_date;
                    unlockUI();
                    console.log('flag', $scope.digitalsignature_flag);
                    console.log('lspage', lspage);
                    if ($scope.digitalsignature_flag != "Y" && lspage != "RMSanctionSummary") {
                        $scope.download_show = true;
                    }
                    else if ($scope.digitalsignature_flag != "Y" && lspage == "RMSanctionSummary") {
                        $scope.download_show = false;
                    }
                    else {

                    }
                });

                var url = 'api/MstApplicationReport/CADSanctionLetterSummary';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sanctiontocheckerlist = resp.data.reportsanctiondetails_list;
                });

                var url = 'api/MstApplicationReport/Getesdocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.UploadCADES_DocumentList = resp.data.UploadCADES_DocumentList;
                });

                var url = 'api/MstApplicationReport/GetMaildocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.DeviationCADMail_DocumentList = resp.data.DeviationCADMail_DocumentList;
                });

               
                $scope.downloadsCAM = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.downloaddocument = function (val1, val2) {
                    // var phyPath = val1;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = val2.split(".")
                    // link.download = val2;
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
        

                var params = {
                    application_gid: $scope.application_gid
                }
                var url = 'api/MstCADApplication/GetProductChargesDtl';
               
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtoveralllimit_amt = resp.data.overalllimit_amount;
                    $scope.txtvalidity_year = resp.data.validityoveralllimit_year;
                    $scope.txtvalidity_month = resp.data.validityoveralllimit_month;
                    $scope.txtvalidity_days = resp.data.validityoveralllimit_days;
                    $scope.txtcalculation_limitvalidity = resp.data.calculationoveralllimit_validity;
                    $scope.loandtls_list = resp.data.mstLoan_list;
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;
                    $scope.txt_processingfee = resp.data.processing_fee;
                    $scope.txtprocessing_collecttype = resp.data.processing_collectiontype;
                    $scope.txtdoc_charges = resp.data.doc_charges;
                    $scope.txtdoc_collecttype = resp.data.doccharge_collectiontype;
                    $scope.txtfield_visitcharges = resp.data.fieldvisit_charge;
                    $scope.txtfield_collecttype = resp.data.fieldvisit_collectiontype;
                    $scope.txtadhoc_fee = resp.data.adhoc_fee;
                    $scope.txtadhoccollection_type = resp.data.adhoc_collectiontype;
                    $scope.txtlife_insurance = resp.data.life_insurance;
                    $scope.txtlife_collectiontype = resp.data.lifeinsurance_collectiontype;
                    $scope.txtaccident_insurance = resp.data.acct_insurance;
                    $scope.txttotal_collectible = resp.data.total_collect;
                    $scope.txttotal_deductible = resp.data.total_deduct;
                    $scope.Collateral_list = resp.data.mstcollateral_list;
                    $scope.txtproduct_type = resp.data.product_type;
                    $scope.servicecharge_List = resp.data.servicecharge_List;
                    $scope.txtsecurity_type = resp.data.security_type;
                    $scope.txtsecurity_description = resp.data.security_description;
                    $scope.txtsecurity_value = resp.data.security_value;
                    $scope.txtsecurityassessed_date = resp.data.securityassessed_date;
                    $scope.txtasset_id = resp.data.asset_id;
                    $scope.txtroc_fillingid = resp.data.roc_fillingid;
                    $scope.txtCERSAI_fillingid = resp.data.CERSAI_fillingid;
                    $scope.txthypoobservation_summary = resp.data.hypoobservation_summary;
                    $scope.txtprimary_security = resp.data.primary_security;
                    $scope.application2hypothecation_gid = resp.data.application2hypothecation_gid;
                });
                
                var url = 'api/MstCadFlow/GetBuyerList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.rmbuyer_list = resp.data.rmbuyer_list;
                    $scope.creditbuyer_list = resp.data.creditbuyer_list;
                });
           
        }

        $scope.PurposeofLoanOther_view = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/PurposeOfLoanOther.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                   {
                       application2loan_gid: application2loan_gid
                   }
                var url = 'api/MstApplicationView/GetPurposeofLoan';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtpurposeof_loan = resp.data.enduse_purpose;

                });
                var url = 'api/MstApplicationView/GetLoanProgramValueChain';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program = resp.data.program;
                    $scope.primaryvaluechain_name = resp.data.primaryvaluechain_name;
                    $scope.secondaryvaluechain_name = resp.data.secondaryvaluechain_name;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }

           

        }

        

       

        $scope.Back = function () { 
            if (lsreportpage == "Maker") {
                $location.url('app/MstSanctionMISReportMaker');
            }
            else if (lsreportpage == "Checker") {
                $location.url('app/MstSanctionMISReportChecker');
            }
            else if (lsreportpage == "Approver") {
                $location.url('app/MstSanctionMISReportApprover');
            }
            else {
                $location.url('app/MstSanctionMISReport');
            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.DeviationCADMail_DocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.DeviationCADMail_DocumentList[i].document_path, $scope.DeviationCADMail_DocumentList[i].document_name);
            }
        }
        $scope.downloadallESdoc = function () {
            for (var i = 0; i < $scope.UploadCADES_DocumentList.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.UploadCADES_DocumentList[i].document_path, $scope.UploadCADES_DocumentList[i].document_name);
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
    }
})();