
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnApplGroupMemberdtlViewController', AgrTrnApplGroupMemberdtlViewController);

        AgrTrnApplGroupMemberdtlViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function AgrTrnApplGroupMemberdtlViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnApplGroupMemberdtlViewController';
        var contact_gid = localStorage.getItem('contact_gid');

        /* lockUI(); */
        activate();

        function activate() {
             
              var params = {
                contact_gid: contact_gid
            }
           
            var url = 'api/AgrMstApplicationEdit/EditIndividual';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtindfirst_name = resp.data.first_name;
                $scope.txtindmiddle_name = resp.data.middle_name;
                $scope.txtindlast_name = resp.data.last_name;
                $scope.txtstakeholdertype_name = resp.data.stakeholdertype_name;
                $scope.txtfathernominee_status = resp.data.fathernominee_status;
                $scope.txtmothernominee_status = resp.data.mothernominee_status;
                $scope.txtspousenominee_status = resp.data.spousenominee_status;
                $scope.txtpan_number = resp.data.pan_no;
                $scope.txtaadhar_number = resp.data.aadhar_no;
                $scope.txt_dob = resp.data.individual_dob;
                $scope.txtage = resp.data.age;
                $scope.txtgender = resp.data.gender_name;
                $scope.txtdesignation = resp.data.main_occupation;
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
                $scope.txtownership_type = resp.data.ownershiptype_name;
                $scope.txtproperty_name = resp.data.propertyholder_name;
                $scope.txtresidence_type = resp.data.residencetype_name;
                $scope.txtyear_currentresidence = resp.data.currentresidence_years;
                $scope.txtdistance = resp.data.branch_distance;
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
                $scope.txttotal_landacres = resp.data.totallandinacres;
                $scope.txtcultivated_land = resp.data.cultivatedland;
                $scope.txtprevious_crop = resp.data.previouscrop;
                $scope.txtproposed_crop = resp.data.prposedcrop;
                $scope.txtinstitution_name = resp.data.institution_name;
            }); 

            var url = 'api/AgrMstApplicationEdit/GetIndividualAddressList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.individualaddress_list = resp.data.contactaddress_list;

            });

            var url = 'api/AgrMstApplicationEdit/GetIndividualProofList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.contactidproof_list = resp.data.contactidproof_list;

            });

            var url = 'api/AgrMstApplicationEdit/GetIndividualDocList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.uploadindividualdoc_list = resp.data.uploadindividualdoc_list;

            });

            var url = 'api/AgrMstApplicationView/GetPrimaryAndOtherMobileNumbers';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimaryindividual_mobileno = resp.data.primaryindividual_mobileno;
                $scope.individualmobileno_list = resp.data.individualmobileno_list;
            });
           
            var url = 'api/AgrMstApplicationView/GetPrimaryAndOtherEmails';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtprimaryindividual_email = resp.data.primaryindividual_email;
                $scope.individualemail_list = resp.data.individualemail_list;
            });

            var url = 'api/AgrMstApplicationView/GetIndividualBureauDtls';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtbureauname_gid = resp.data.bureauname_gid;
                $scope.txtbureauname_name = resp.data.bureauname_name;
                $scope.txtbureau_score = resp.data.bureau_score;
                $scope.txtobservations = resp.data.observations;
                $scope.txtbureau_response = resp.data.bureau_response;
                $scope.txtbureauscore_date = resp.data.bureauscore_date;
                // $scope.txtcicdocument_name = resp.data.cicdocument_name;
                // $scope.txtcicdocument_path = resp.data.cicdocument_path;
                $scope.Individualcicdoc_list = resp.data.Individualcicdoc_list;
            });

        }

        $scope.close = function () {
            window.close();
        }

        $scope.individualproof_downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.individualdoc_downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.individualbureaudoc_downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.downloadall_3 = function () {
            for (var i = 0; i < $scope.contactidproof_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.contactidproof_list[i].document_path, $scope.contactidproof_list[i].document_name);
            }
        }
        $scope.downloadall_4 = function () {
            for (var i = 0; i < $scope.uploadindividualdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.uploadindividualdoc_list[i].document_path, $scope.uploadindividualdoc_list[i].document_name);
            }
        }
        $scope.downloadall_5 = function () {
            for (var i = 0; i < $scope.Individualcicdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.Individualcicdoc_list[i].cicindividualdocument_path, $scope.Individualcicdoc_list[i].cicindividualdocument_name);
            }
        }

    }
})();