(function () {
    'use strict';

    angular
        .module('angle')
        .controller('GSTAuthenticationViewController', GSTAuthenticationViewController);

    GSTAuthenticationViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function GSTAuthenticationViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'GSTAuthenticationViewController';
        var institution2branch_gid = localStorage.getItem('institution2branch_gid');

        lockUI();
        activate();

        function activate() {
            var params = {
                institution2branch_gid: institution2branch_gid,
            }

            var url = 'api/MstAPIVerifications/GSTAuthenticationViewDetails';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();

                $scope.gstin = resp.data.result.gstin;


                $scope.legalname = resp.data.result.lgnm;
                $scope.tradename = resp.data.result.tradeNam;
                $scope.status = resp.data.result.sts;
                $scope.constitutionofbusiness = resp.data.result.ctb;

                var nba = resp.data.result.nba;
                var natureofbusiness = "";
                for (var i = 0; i < nba.length; i++) {
                    natureofbusiness = natureofbusiness.concat(nba[i], ",");
                }
                natureofbusiness = natureofbusiness.replace(/,\s*$/, "");
                $scope.nob = natureofbusiness;
                

                $scope.taxpayertype = resp.data.result.dty;
                $scope.compliancerating = resp.data.result.cmpRt;

                $scope.centraljusridiction = resp.data.result.ctj;
                $scope.centraljusridiction_code = resp.data.result.ctjCd;
                $scope.statejurisdiction = resp.data.result.stj;
                $scope.statejurisdiction_code = resp.data.result.stjCd;



                $scope.dateofregistration = resp.data.result.rgdt;
                $scope.dateofcancellation = resp.data.result.cxdt;
                $scope.lastupdateddate = resp.data.result.lstupdt;


                $scope.contact_mobnum = resp.data.result.contacted.mobNum;
                $scope.contact_email = resp.data.result.contacted.email;
                $scope.contact_name = resp.data.result.contacted.name;

                $scope.contactdetail = "defined";
                if ($scope.contact_mobnum == null && $scope.contact_email == null && $scope.contact_name == null) {
                    $scope.contactdetail = null;
                }




                $scope.pradr_adr = resp.data.result.pradr.adr;
                $scope.pradr_ntr = resp.data.result.pradr.ntr;
                $scope.pradr_em = resp.data.result.pradr.em;
                $scope.pradr_mb = resp.data.result.pradr.mb;
                $scope.pradr_lastUpdatedDate = resp.data.result.pradr.lastUpdatedDate;


                $scope.adadr_list = resp.data.result.adadr;

                $scope.additionaladdress = "defined";
                if ($scope.adadr_list.length == 0) {
                    $scope.additionaladdress = null;
                }

            });






        }

        $scope.close = function () {
            window.close();
        }
    }
})();
