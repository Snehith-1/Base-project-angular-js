(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDisbCoApplicantContactDtlViewController', MstDisbCoApplicantContactDtlViewController);

    MstDisbCoApplicantContactDtlViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstDisbCoApplicantContactDtlViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDisbCoApplicantContactDtlViewController';

        var contactcoapplicant_gid = localStorage.getItem('contactcoapplicant_gid');

        lockUI();
        activate();
        function activate() {

            var params = {
                contactcoapplicant_gid: contactcoapplicant_gid
            }
            var url = 'api/MstCreditOpsApplication/GetDisbCoApplicantDtlView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.application_gid = resp.data.application_gid;
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblpan_status = resp.data.pan_status;
                $scope.lblpan_no = resp.data.pan_no;
                $scope.lblaadhar_no = resp.data.aadhar_no;
                $scope.lblindividual_name = resp.data.individual_name;
                $scope.lblindividual_dob = resp.data.individual_dob;
                $scope.lbldesignation_name = resp.data.designation_name;
                $scope.lbleducationalqualification_name = resp.data.educationalqualification_name;
                $scope.lblmain_occupation = resp.data.main_occupation;
                $scope.lblannual_income = resp.data.annual_income;
                $scope.lblmonthly_income = resp.data.monthly_income;
                $scope.lblpep_status = resp.data.pep_status;
                $scope.lblpepverified_date = resp.data.pepverified_date;
                $scope.lblstakeholder_type = resp.data.stakeholder_type;
                $scope.lblmaritalstatus_name = resp.data.maritalstatus_name;
                $scope.lblfather_name = resp.data.father_name;
                $scope.lblfather_dob = resp.data.father_dob;
                $scope.lblmother_name = resp.data.mother_name;
                $scope.lblmother_dob = resp.data.mother_dob;
                $scope.lblspouse_name = resp.data.spouse_name;
                $scope.lblspouse_dob = resp.data.spouse_dob;
                $scope.lblcurrentresidence_years = resp.data.currentresidence_years;
                $scope.lblbranch_distance = resp.data.branch_distance;
                $scope.lblurn_status = resp.data.urn_status;
                $scope.lblurn = resp.data.urn;
                $scope.lblfathernominee_status = resp.data.fathernominee_status;
                $scope.lblmothernominee_status = resp.data.mothernominee_status;
                $scope.lblspousenominee_status = resp.data.spousenominee_status;
                $scope.lblifsc_code = resp.data.ifsc_code;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblbranch_address = resp.data.branch_address;
                $scope.lblmicr_code = resp.data.micr_code;
                $scope.lblbankaccount_number = resp.data.bankaccount_number;
                $scope.lblaccountholder_name = resp.data.accountholder_name;
                $scope.lblaccount_type = resp.data.account_type;
                $scope.lbljoint_account = resp.data.joint_account;
                $scope.lbljointaccountholder_name = resp.data.jointaccountholder_name;
                $scope.lblchequebookfacility_available = resp.data.chequebookfacility_available;
                $scope.lblaccountopen_date = resp.data.accountopen_date;
                $scope.lblmobile_no = resp.data.mobile_no;
                $scope.lblmobileno_primarystatus = resp.data.mobileno_primarystatus;
                $scope.lblwhatsapp_no = resp.data.whatsapp_no;
                $scope.lblemail_address = resp.data.email_address;
                $scope.lblemailprimary_status = resp.data.emailprimary_status;
                $scope.lbladdresstype_name = resp.data.addresstype_name;
                $scope.lbladdressprimary_status = resp.data.addressprimary_status;
                $scope.lbladdressline1 = resp.data.addressline1;
                $scope.lbladdressline2 = resp.data.addressline2;
                $scope.lblpostal_code = resp.data.postal_code;
                $scope.lblcity = resp.data.city;
                $scope.lbltaluka = resp.data.taluka;
                $scope.lbldistrict = resp.data.district;
                $scope.lblstate = resp.data.state;
                $scope.lblcountry = resp.data.country;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.lblcreated_date = resp.data.created_date;
                $scope.Coapplicantcontactpanform60_list = resp.data.Coapplicantcontactpanform60_list;
                $scope.coapplicantuploadindividualdoc_list = resp.data.coapplicantuploadindividualdoc_list;
                $scope.coapplicantpanabsencereasons_list = resp.data.coapplicantpanabsencereasons_list;
            });
        }

        $scope.form60_documentviewer = function (val1, val2) {
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

        $scope.downloadall = function () {

            for (var i = 0; i < $scope.coapplicantuploadindividualdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.coapplicantuploadindividualdoc_list[i].document_path, $scope.coapplicantuploadindividualdoc_list[i].document_name);
            }
        }

        $scope.downloadallform60 = function () {
            for (var i = 0; i < $scope.Coapplicantcontactpanform60_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Coapplicantcontactpanform60_list[i].document_path, $scope.Coapplicantcontactpanform60_list[i].document_name);
            }
        }

        $scope.form60_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }   

        $scope.downloads1 = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2,val3);
            }
        }

        $scope.coapplicantdoc_documentviewer = function (val1, val2) {
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

        $scope.close = function () {
            window.close();
        }

    }
})();