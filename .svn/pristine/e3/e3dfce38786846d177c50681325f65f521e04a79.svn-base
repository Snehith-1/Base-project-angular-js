(function () {
    'use strict';

    angular
        .module('angle')
        .controller('IdasTrnBatchView', IdasTrnBatchView);

    IdasTrnBatchView.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'DownloaddocumentService'];

    function IdasTrnBatchView($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService) {
        $scope.title = 'IdasTrnBatchView';
        var sanction_gid;
        var customer_gid;
        var page;

        activate();

        function activate() {
            sanction_gid = localStorage.getItem('sanction_gid');
            page = localStorage.getItem('page');
            var url = 'api/IdasMstSanction/SanctionDtlsEdit';
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionrefno = resp.data.sanction_refno;
                $scope.SanctionDate = resp.data.sanction_date;
                $scope.SanctionAmount = resp.data.sanction_amount;
                $scope.FacilityType = resp.data.facility_type;
                $scope.customerName = resp.data.customername;
                $scope.Customerurn = resp.data.customer_urn;
                $scope.collateral_security = resp.data.collateral_security;
                $scope.zonalHeadName = resp.data.zonal_name;
                $scope.businessHeadName = resp.data.businesshead_name;
                $scope.clusterManager = resp.data.cluster_manager_name;
                $scope.creditManager = resp.data.creditmanager_name;
                $scope.relationshipmgmt = resp.data.relationshipmgmt_name;
                $scope.customercode = resp.data.customercode;
                $scope.verticalCode = resp.data.vertical;
                $scope.contactperson = resp.data.contactperson;
                $scope.mobileno = resp.data.mobileno;
                $scope.addressline1 = resp.data.addressline1;
                $scope.addressline2 = resp.data.addressline2;
                customer_gid = resp.data.customer_gid;
                $scope.batch_status = resp.data.batch_status;

            });

            var url = "api/IdasTrnSanctionDoc/ScanDocSummary";
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                $scope.documentation_list = resp.data.MdlScannDocSummary;

            });

            var url = "api/IdasTrnPhyDoc/GetPhyUnVerifiedCount";
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                $scope.phydocunverified_count = resp.data.phydocunverified_count;
                console.log('count', resp.data);
            });


            var url = 'api/IdasTrnSanctionDoc/GetCommonDoc';
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {

                $scope.commondocument = resp.data.uploaddocument;

            });

        }

        $scope.export = function () {
            var params = {
                sanction_gid: sanction_gid,
                type_of_copy: 'Physical Copy'
            }
            var url = 'api/IdasTrnSanctionDoc/ScanDocConExport';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.attachment_cloudpath, resp.data.attachment_name);
                    // var phyPath = resp.data.attachment_path;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.attachment_name.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();

                }
                else {
                    Notify.alert(resp.data.message, 'success')
                    activate();

                }

            });
        }
        $scope.downloadsdocument = function (val1, val2) {

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

        $scope.batchviewback = function () {
            if (page == "batch")
            {
                $state.go('app.idasTrnFile2Despatch?lstab=batch');
            }
            else if(page=="box")
            {
                $state.go('app.IdasTrnBoxDtlsView');
            }
            else if (page == "Despatch")
            {
                $state.go('app.IdasTrnDespatchBoxDtlsView');
            }
            else if (page == "pendingbatch") {
                $location.url('app/idasTrnFile2Despatch?lstab=pendingbatch');
            }
            else {
                $location.url('app/idasTrnFile2Despatch?lstab=batch');
            }

           
        }

        $scope.docConMkr=function(sanctiondocument_gid)
        {
            localStorage.setItem('sanctiondocument_gid', sanctiondocument_gid);
            $state.go('app.IdasTrnBatchConversationView');
        }

    }
})();
