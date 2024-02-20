(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApplCreationIndividualGuarantorViewController', AgrMstApplCreationIndividualGuarantorViewController);

    AgrMstApplCreationIndividualGuarantorViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService', 'cmnfunctionService'];

    function AgrMstApplCreationIndividualGuarantorViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApplCreationIndividualGuarantorViewController';
        var contact_gid = localStorage.getItem('contact_gid');

        lockUI();
        activate();

        function activate() {

            var params = {
                contact_gid: contact_gid
            }

            var url = 'api/AgrMstApplicationView/GetGurantorIndividualView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcustomer_name = resp.data.individual_name;
                $scope.txtborrower_type = resp.data.borrower_type;
                $scope.txtpan_number = resp.data.pan_no;
                $scope.txtaadhar_number = resp.data.aadhar_no;
                $scope.txt_dob = resp.data.individual_dob;
                $scope.txtage = resp.data.age;
                $scope.txtgender = resp.data.gender_name;
                $scope.txtdesignation = resp.data.designation_name;
                $scope.txt_peppoliticallyperson = resp.data.pep_status;
                $scope.txtpep_verifiesdate = resp.data.pepverified_date;
                $scope.txtmarital_status = resp.data.maritalstatus_name;
                $scope.txtfather_name = resp.data.father_name;
                $scope.txtfatherdob_date = resp.data.father_dob;
                $scope.txtfather_age = resp.data.father_age;
                $scope.txtmother_name = resp.data.mother_name;
                $scope.txtmotherdob_date = resp.data.mother_dob;
                $scope.txtmother_age = resp.data.mother_age;
                $scope.txtspouse_name = resp.data.spouse_name;
                $scope.txtspousedob_date = resp.data.spouse_dob;
                $scope.txtspouse_age = resp.data.spouse_age;
                $scope.txtEdu_qualification = resp.data.educationalqualification_name;
                $scope.txtmain_occupation = resp.data.main_occupation;
                $scope.txtannual_income = resp.data.annual_income;
                $scope.txtmonthly_income = resp.data.monthly_income;
                $scope.txtincome_type = resp.data.user_type;
                $scope.txtindividualprimary_number = resp.data.primaryindividual_mobileno;
                $scope.individualmobile_list = resp.data.contactmobileno_list;
                $scope.txtindividualprimary_emailaddress = resp.data.primaryindividual_email;
                $scope.individualmailaddress_list = resp.data.contactemail_list;
                $scope.txtownership_type = resp.data.ownershiptype_name;
                $scope.txtproperty_name = resp.data.propertyholder_name;
                $scope.txtresidence_type = resp.data.residencetype_name;
                $scope.txtyear_currentresidence = resp.data.currentresidence_years;
                $scope.txtdistance = resp.data.branch_distance;
                $scope.individualproof_list = resp.data.contactidproof_list;
                $scope.individualdoc_list = resp.data.uploadindividualdoc_list; //check download
                $scope.bureauname_gid = resp.data.bureauname_gid;
                $scope.txtindividualbureau_name = resp.data.indbureauname_name;
                $scope.txtindividualbureau_score = resp.data.indbureau_score;
                $scope.txtindividualscore_on = resp.data.indbureauscore_date;
                $scope.txtindividualobservations = resp.data.indobservations;
                $scope.txtindividualbureau_response = resp.data.indbureau_response;
                // $scope.cicindividualdocument_name = resp.data.indcicdocument_name;
                // $scope.cicindividualdocument_path = resp.data.indcicinddocument_path;
                $scope.Individualcicdoc_list = resp.data.Individualcicdoc_list;
                $scope.individualaddress_list = resp.data.contactaddress_list;
                $scope.txtgroup_name = resp.data.group_name;
                $scope.txtprofile = resp.data.profile;
                $scope.txturn_status = resp.data.urn_status;
                $scope.txt_urn = resp.data.urn;
                $scope.txtother_nominee = resp.data.othernominee_status;
                $scope.txtrelationship_type = resp.data.relationshiptype;
                $scope.txtnomineedob_date = resp.data.nominee_dob;
                $scope.nomineefirst_name = resp.data.nomineefirst_name;
                $scope.nominee_middlename = resp.data.nominee_middlename;
                $scope.nominee_lastname = resp.data.nominee_lastname;
                $scope.txtnominee_age = resp.data.nominee_age;
                $scope.txtfathernominee_status = resp.data.fathernominee_status;
                $scope.txtmothernominee_status = resp.data.mothernominee_status;
                $scope.txtspousenominee_status = resp.data.spousenominee_status;
                $scope.txttotal_landacres = resp.data.totallandinacres;
                $scope.txtcultivated_land = resp.data.cultivatedland;
                $scope.txtprevious_crop = resp.data.previouscrop;
                $scope.txtproposed_crop = resp.data.prposedcrop;
                $scope.txtinstitution_name = resp.data.institution_name;
                $scope.contactpanabsencereasons_list = resp.data.contactpanabsencereasons_list;
                $scope.txtpan_status = resp.data.pan_status;
            });
            
            var url = 'api/AgrMstApplicationAdd/Individual2bankTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditbankacc_list = resp.data.individual2bankacc_list;

            });

            var url = 'api/AgrMstApplicationAdd/GetContactBureauList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.contactbureau_list = resp.data.contactbureau_list;
            });

            var url = 'api/AgrMstApplicationEdit/GetPANForm60List';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.contactpanform60_list = resp.data.contactpanform60_list;
            });

        }

        $scope.close = function () {
            window.close();
        }

        $scope.individualproof_downloads = function (val1, val2) {
            // var phyPath = val1;
            // var relPath = phyPath.split("StoryboardAPI");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = location.protocol + "//";
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // link.download = val2;
            // var uri = str;
            // link.href = uri;
            // link.click();
            DownloaddocumentService.Downloaddocument(val1, val2); //check download
        }

        $scope.individualdoc_downloads = function (val1, val2) {
            // var phyPath = val1;
            // var relPath = phyPath.split("StoryboardAPI");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = location.protocol + "//";
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // link.download = val2;
            // var uri = str;
            // link.href = uri;
            // link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.individualbureaudoc_downloads = function (val1, val2) {
            // var phyPath = val1;
            // var relPath = phyPath.split("StoryboardAPI");
            // var relpath1 = relPath[1].replace("\\", "/");
            // var hosts = window.location.host;
            // var prefix = location.protocol + "//";
            // var str = prefix.concat(hosts, relpath1);
            // var link = document.createElement("a");
            // link.download = val2;
            // var uri = str;
            // link.href = uri;
            // link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.downloadall_3 = function () {
            for (var i = 0; i < $scope.individualproof_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.individualproof_list[i].document_path, $scope.individualproof_list[i].document_name);
            }
        }
        $scope.downloadall_4 = function () {
            for (var i = 0; i < $scope.individualdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.individualdoc_list[i].document_path, $scope.individualdoc_list[i].document_name);
            }
        }
        $scope.downloadall_5 = function () {
            for (var i = 0; i < $scope.Individualcicdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Individualcicdoc_list[i].cicindividualdocument_path, $scope.Individualcicdoc_list[i].cicindividualdocument_name);
            }
        }
        $scope.creditbankacctdtl_edit = function (contact2bankdtl_gid) {
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
                    contact2bankdtl_gid: contact2bankdtl_gid
                }

                var url = 'api/AgrMstApplicationAdd/EditGetIndividualBankAccDtl';

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
        $scope.bureau_view = function (contact2bureau_gid) {

            if($scope.bureaeu_view == false || $scope.bureaeu_view == undefined || $scope.bureaeu_view ==""){ 
            $scope.bureaeu_view = true}
            else
            {$scope.bureaeu_view = false}
            var param = {
                contact2bureau_gid: contact2bureau_gid
            };

            var url = 'api/AgrMstApplicationEdit/CICIndividualEdit';

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
            var url = 'api/AgrMstApplicationEdit/CICUploadIndividualDocList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });
        }

        $scope.downloadall_6 = function () {
            for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
            }
        }

        $scope.downloadall_form60 = function () {
            for (var i = 0; i < $scope.contactpanform60_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.contactpanform60_list[i].document_path, $scope.contactpanform60_list[i].document_name);
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
