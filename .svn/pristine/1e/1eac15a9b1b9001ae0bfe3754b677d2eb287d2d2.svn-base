(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApplCreationInstitutionGuarantorViewController', AgrMstApplCreationInstitutionGuarantorViewController);

    AgrMstApplCreationInstitutionGuarantorViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrMstApplCreationInstitutionGuarantorViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApplCreationInstitutionGuarantorViewController';
        var institution_gid = localStorage.getItem('institution_gid');

        lockUI();
        activate();

        function activate() {

            var params = {
                institution_gid: institution_gid
            }

            var paramss = {
                institution_gid: institution_gid,
                tmp_status: false
            }
            var url = 'api/AgrMstApplicationAdd/GetInstitutionRatingList';
            SocketService.getparams(url, paramss).then(function (resp) {
                $scope.institutionratinglist = resp.data.MdlRatingdtl;
            });

            var url = 'api/AgrMstApplicationView/GetGurantorInstitutionView';
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
                $scope.cbostakeholdertype = resp.data.stakeholder_type;
                $scope.txtrevenue = resp.data.revenue;
                $scope.txtprofit = resp.data.profit;
                $scope.txtfixed_asset = resp.data.fixed_assets;
                $scope.txtsundrydebt_adv = resp.data.sundrydebt_adv;
                $scope.rdbincome_tax = resp.data.incometax_returnsstatus;
                $scope.txttan_number = resp.data.tan_number;
                $scope.txteditmsmereg = resp.data.msme_registration;
                $scope.txteditlei = resp.data.lglentity_id;
                $scope.txteditleirenewal_date  = resp.data.editlei_renewaldate;
                $scope.txteditkin  = resp.data.kin;
            });
            var param = {
                institution_gid: institution_gid
            };

            var url = 'api/AgrMstApplicationAdd/Institution2bankTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditbankacc_list = resp.data.institution2bankacc_list;

            });

            var url = 'api/AgrMstApplicationAdd/GetInstitutionBureauList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionbureau_list = resp.data.institutionbureau_list;
            });

        }

        $scope.close = function () {
            window.close();
        }

        $scope.bureaudoc_downloads = function (val1, val2) {
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
        $scope.creditbankacctdtl_edit = function (institution2bankdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/companybankdtl.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var param = {
                    institution2bankdtl_gid: institution2bankdtl_gid
                }

                var url = 'api/AgrMstApplicationAdd/EditGetCreditBankAccDtl';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.txtIFSC_Code = resp.data.ifsc_code;
                    $scope.txtBank_Name = resp.data.bank_name;
                    $scope.txtBranch_Name = resp.data.branch_name;
                    $scope.txtBank_Address = resp.data.bank_address;
                    $scope.txtMICR_Code = resp.data.micr_code;
                    $scope.txtbankacct_no = resp.data.bankaccount_number;
                    $scope.txtconfirmbankacct_no = resp.data.confirmbankaccountnumber;
                    $scope.txtacctholder_name = resp.data.bankaccount_name;
                    $scope.cboAccountType = resp.data.bankaccounttype_gid;
                    $scope.rdbJoint_Account = resp.data.joint_account;
                    $scope.txtjointacctholder_name = resp.data.jointaccountholder_name;
                    $scope.rdbCheque_Book = resp.data.chequebook_status;
                    $scope.txtAccountOpen_Date = resp.data.accountopen_date;
                    $scope.rdbprimarystatus = resp.data.primary_status;

                    //$scope.txtAccountOpen_Date = Date.parse($scope.txtAccountOpen_Date);
                    //$scope.credituploaddocument_list = resp.data.credituploaddocument_list;
                    unlockUI();
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.bureau_view = function (institution2bureau_gid) {

            if($scope.bureaeu_view == false || $scope.bureaeu_view == undefined || $scope.bureaeu_view ==""){ 
            $scope.bureaeu_view = true}
            else
            {$scope.bureaeu_view = false}
            var param = {
                institution2bureau_gid: institution2bureau_gid
            };

            var url = 'api/AgrMstApplicationEdit/CICInstitutionEdit';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtbureauname_name = resp.data.bureauname_name;
                $scope.bureau_gid = resp.data.bureauname_gid;
                $scope.txtbureau_score = resp.data.bureau_score;
                $scope.txtbureauscore_date = resp.data.bureauscore_date;
                $scope.txtobservations = resp.data.observations;
                $scope.txtbureau_response = resp.data.bureau_response;
                $scope.institution2bureau_gid = resp.data.institution2bureau_gid;

                unlockUI();
            });
            var url = 'api/AgrMstApplicationEdit/CICUploadInstitutionDocList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });
        }

    }
})();
