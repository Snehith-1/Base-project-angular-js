(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCcCommitteeInstitutionViewController', AgrTrnSuprCcCommitteeInstitutionViewController);

    AgrTrnSuprCcCommitteeInstitutionViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', 'DownloaddocumentService'];

    function AgrTrnSuprCcCommitteeInstitutionViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCcCommitteeInstitutionViewController';
        var institution_gid = localStorage.getItem('institution_gid');
        var application_gid = localStorage.getItem('application_gid');

        lockUI();
        activate();

        function activate() {

            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var params = {
                institution_gid: institution_gid
            }

            var url = 'api/AgrMstSuprApplicationView/GetGurantorInstitutionView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcompany_name = resp.data.company_name;
                $scope.txtborrower_type = resp.data.borrower_type;
                $scope.txtCIN_number = resp.data.cin_no;
                $scope.txtcompanyPAN_number = resp.data.companypan_no;
                $scope.txtincorporation_date = resp.data.date_incorporation;
                $scope.txtbusiness_year = resp.data.year_business;
                $scope.txtbusiness_month = resp.data.month_business;
                $scope.txtcompany_type = resp.data.companytype_name;
                $scope.txtescrow = resp.data.escrow;
                $scope.txtlastyear_turnover = resp.data.lastyear_turnover;
                $scope.txtstart_date = resp.data.start_date;
                $scope.txtend_date = resp.data.end_date;
                $scope.txtofficial_teleno = resp.data.official_telephoneno;
                $scope.txtofficial_mailaddress = resp.data.officialemail_address;
                $scope.gst_list = resp.data.mstgst_list;
                $scope.txtcredit_assessmentagency = resp.data.assessmentagency_name;
                $scope.txtassessment_rating = resp.data.assessmentagencyrating_name;
                $scope.txtrating_on = resp.data.ratingas_on;
                $scope.txtAML_category = resp.data.amlcategory_name;
                $scope.txtbusiness_category = resp.data.businesscategory_name;
                $scope.txtinstituionprimary_number = resp.data.primaryinstitution_mobileno;
                $scope.instituionmobile_list = resp.data.instituionmobilenumber_list;
                $scope.txtinstituionprimary_emailaddress = resp.data.primaryinstitution_email;
                $scope.instituionmailaddress_list = resp.data.mail_list;
                $scope.instituionaddress_list = resp.data.mstaddress_list;
                $scope.institutionform60_list = resp.data.institutionform60_list;
                $scope.institutiondoc_list = resp.data.institutiondoc_list;
                $scope.mstlicense_list = resp.data.mstlicense_list;
                $scope.bureauname_gid = resp.data.bureauname_gid;
                $scope.txbureau_name = resp.data.bureauname_name;
                $scope.txtbureau_score = resp.data.bureau_score;
                $scope.txtscore_on = resp.data.bureau_response;
                $scope.txtobservations = resp.data.observations;
                $scope.txtbureau_response = resp.data.bureauscore_date;
                // $scope.cicdocument_name = resp.data.cicdocument_name;
                // $scope.cicdocument_path = resp.data.cicdocument_path;
                $scope.Institutioncicdoc_list = resp.data.Institutioncicdoc_list;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txturn = resp.data.urn;
                $scope.txtcontact_firstname = resp.data.contactperson_firstname;
                $scope.txtcontact_middlename = resp.data.contactperson_middlename;
                $scope.txtcontact_lastname = resp.data.contactperson_lastname;
                $scope.txtdesignation = resp.data.designation;
                $scope.txtbusinessstart_date = resp.data.businessstart_date;

            });

            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditOperationsView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtinstitution_name = resp.data.company_name;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txturn_number = resp.data.urn;
            });

            var params = {
                credit_gid: institution_gid
            }

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetGeneticCodeList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.geneticcode_list = resp.data.mstcuwgeneticcode_list;
            });

            var params = {
                credit_gid: institution_gid,
                applicant_type: 'Institution'
            }

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/EditSocialAndTradeCapital';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtsocial_capital = resp.data.social_capital;
                $scope.txttrade_capital = resp.data.trade_capital;
                unlockUI();
            });

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/EditPSLDataFlagging';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtstartloan_date = resp.data.startupasofloansanction_date;
                $scope.txtoccupation = resp.data.occupation;
                $scope.txtline_activity = resp.data.lineofactivity;
                $scope.txtbsr_code = resp.data.bsrcode;
                $scope.txtpsl_category = resp.data.pslcategory;
                $scope.txtweaker_section = resp.data.weakersection;
                $scope.txtpsl_purpose = resp.data.pslpurpose;
                $scope.txttotal_financialinstitution = resp.data.totalsanction_financialinstitution;
                $scope.txtpsl_sanctionlimit = resp.data.pslsanction_limit;
                $scope.txtnature_entity = resp.data.natureofentity;
                $scope.txtmarketing_activities = resp.data.indulgeinmarketing_activity;
                $scope.txtplant_machienery = resp.data.plantandmachineryinvestment;
                $scope.txtturnover = resp.data.turnover;
                $scope.txtmsme_classification = resp.data.msmeclassification;
                $scope.txtloansanction_date = resp.data.loansanction_date;
                $scope.txtentityincorporate_date = resp.data.entityincorporation_date;
                $scope.txthq_city = resp.data.hq_metropolitancity;
                $scope.txtclient_details = resp.data.clientdtl_name;
            });

            var params = {
                credit_gid: institution_gid
            }

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditSupplierList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.supplierdtls_list = resp.data.supplier_list;
            });

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditBuyerList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.Buyerdtls_list = resp.data.creditbuyer_list;
            });

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCrediBankAccList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.BankacctDtl_list = resp.data.creditbankacc_list;
            });

            var params = {
                credit_gid: institution_gid
            }

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetExistingBankFacility';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.ExistingBankacctDtl_list = resp.data.cuwexistingbankfacility_list;
            });

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetRepaymentTrack';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.RepaymentDtl_list = resp.data.cuwrepaymenttrack_list;
            });

            var params = {
                credit_gid: institution_gid
            }

            var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditObservationList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.creditobservation_list = resp.data.CreditObservation_list;
            });

            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstSuprApplicationView/GetApplicationBasicView';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtapplication_no = resp.data.application_no;
            });

        }

        $scope.close = function () {
            window.close();
        }

        $scope.bureaudoc_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.institutiondoc_downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.form60_downloads = function (val1, val2) {
            ////var phyPath = val1;
            ////var relPath = phyPath.split("StoryboardAPI");
            ////var relpath1 = relPath[1].replace("\\", "/");
            ////var hosts = window.location.host;
            ////var prefix = location.protocol + "//";
            ////var str = prefix.concat(hosts, relpath1);
            ////var link = document.createElement("a");
            ////link.download = val2;
            ////var uri = str;
            ////link.href = uri;
            ////link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.repayment_remarks = function (creditrepaymentdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Remarksdetails.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      creditrepaymentdtl_gid: creditrepaymentdtl_gid
                  }
                var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditRepaymentDtlRemarks';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.repayment_remarks = resp.data.Repayment_remarks;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.Existingbank_remarks = function (existingbankfacility_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ExistingRemarksdetails.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    existingbankfacility_gid: existingbankfacility_gid
                }
                var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditExistingBankDtlRemarks';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.existing_remarks = resp.data.Existingbank_remarks;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.relationship_supplier = function (creditsupplier_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/SupplierRelationship.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      creditsupplier_gid: creditsupplier_gid
                  }
                var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditSupplierTextData';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrelationship_supplier = resp.data.relationship_supplier;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.relationship_buyer = function (creditbuyer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/SupplierRelationshipMonitoring.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false

            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      creditbuyer_gid: creditbuyer_gid
                  }
                var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditBuyerTextData';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtrelationship_borrower = resp.data.relationship_borrower;
                    $scope.txtenduse_monitoring = resp.data.enduse_monitoring;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.uploadeddoc_bankacctdtl = function (creditbankdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Bankacctdocuments.html',
                controller: ModalInstanceCtrl,
                size: 'lg',
                backdrop: 'static',
                keyboard: false
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      creditbankdtl_gid: creditbankdtl_gid
                  }
                var url = 'api/AgrTrnSuprAppCreditUnderWriting/GetCreditBankDocumentUpload';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.chequeleaf_list = resp.data.credituploaddocument_list;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.download_chequeleafdoc = function (val1, val2) {
                    ////var phyPath = val1;
                    ////var relPath = phyPath.split("EMS");
                    ////var relpath1 = relPath[1].replace("\\", "/");
                    ////var hosts = window.location.host;
                    ////var prefix = location.protocol + "//";
                    ////var str = prefix.concat(hosts, relpath1);
                    ////var link = document.createElement("a");
                    ////link.download = val2;
                    ////var uri = str;
                    ////link.href = uri;
                    ////link.click();
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.chequeleaf_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.chequeleaf_list[i].chequeleaf_path, $scope.chequeleaf_list[i].chequeleaf_name);
                    }
                }
            }

        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.chequeleaf_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.chequeleaf_list[i].chequeleaf_path, $scope.chequeleaf_list[i].chequeleaf_name);
            }
        }
        $scope.downloadall_3 = function () {
            for (var i = 0; i < $scope.institutiondoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.institutiondoc_list[i].document_path, $scope.institutiondoc_list[i].document_name);
            }
        }
        $scope.downloadall_4 = function () {
            for (var i = 0; i < $scope.Institutioncicdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Institutioncicdoc_list[i].cicdocument_path, $scope.Institutioncicdoc_list[i].cicdocument_name);
            }
        }

    }
})();
