(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrSuprIECDetailedProfileViewController', AgrSuprIECDetailedProfileViewController);

    AgrSuprIECDetailedProfileViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function AgrSuprIECDetailedProfileViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrSuprIECDetailedProfileViewController';
        var iecdtl_gid = localStorage.getItem('iecdtl_gid');

        activate();

        function activate() {
            var params = {
                iecdtl_gid: iecdtl_gid,
            }

            var url = 'api/AgrMstSuprAPIVerifications/IECProfileViewDetails';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtname = resp.data.result.name;
                $scope.txtie_code = resp.data.result.ie_code;
                $scope.txtaddress = resp.data.result.address;
                $scope.txtiecgate_status = resp.data.result.iecgate_status;
                $scope.txtpan = resp.data.result.pan;

                $scope.directors_list = resp.data.result.directors;
                $scope.branches_list = resp.data.result.branches;
                $scope.rcmcdetails_list = resp.data.result.rcmc_details;


                $scope.txtregistration_details = resp.data.result.registration_details;
                $scope.txtbank_details = resp.data.result.bank_details;
                $scope.txtiec_allotment_date = resp.data.result.iec_allotment_date;
                $scope.txtfile_number = resp.data.result.file_number;
                $scope.txtfile_date = resp.data.result.file_date;
                $scope.txtparty_name_and_address = resp.data.result.party_name_and_address;
                $scope.txtphone_no = resp.data.result.phone_no;
                $scope.txte_mail = resp.data.result.e_mail;
                $scope.txtexporter_type = resp.data.result.exporter_type;
                $scope.txtdate_of_establishment = resp.data.result.date_of_establishment;
                $scope.txtbin_pan_extension = resp.data.result.bin_pan_extension;
                $scope.txtpan_issue_date = resp.data.result.pan_issue_date;
                $scope.txtpan_issued_by = resp.data.result.pan_issued_by;
                $scope.txtnature_of_concern = resp.data.result.nature_of_concern;
                $scope.txtiec_status = resp.data.result.iec_status;
                $scope.txtno_of_branches = resp.data.result.no_of_branches;
             
            });






        }

        $scope.close = function () {
            window.close();
        }
    }
})();
