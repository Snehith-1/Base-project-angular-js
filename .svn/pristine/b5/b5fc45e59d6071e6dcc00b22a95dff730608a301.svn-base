(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprApplCreationInstitutionGuarantorViewController', AgrMstSuprApplCreationInstitutionGuarantorViewController);

    AgrMstSuprApplCreationInstitutionGuarantorViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function AgrMstSuprApplCreationInstitutionGuarantorViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprApplCreationInstitutionGuarantorViewController';
        var institution_gid = localStorage.getItem('institution_gid');

        lockUI();
        activate();

        function activate() {

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
