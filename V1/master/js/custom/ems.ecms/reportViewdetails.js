(function () {
    'use strict';

    angular
        .module('angle')
        .controller('reportViewdetails', reportViewdetails);

    reportViewdetails.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function reportViewdetails($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewDeferral';

        activate();
        function activate() {
            $scope.deferral_gid = localStorage.getItem('deferral_gid');
            var params = {
                deferral_gid: $scope.deferral_gid
            }
            var url = 'api/deferral/Getcaddoc';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.UploadDocumentname = resp.data;
                $scope.deferrals = resp.data.deferralSummaryDtls;
                $scope.deferral_gid = resp.data.deferral_gid;
                $scope.loanref_no = resp.data.loanref_no;
                $scope.loan_title = resp.data.loan_title;
                $scope.record_id = resp.data.record_id;
                $scope.deferral_name = resp.data.deferral_name;
                $scope.approval_remarks = resp.data.approval_remarks;
                $scope.approval_status = resp.data.approval_status;
                $scope.filename_list = resp.data.filename_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.zonal_name = resp.data.zonal_name;
                $scope.businesshead_name = resp.data.businesshead_name;
                $scope.rm_name = resp.data.rm_name;
                $scope.credit_manager = resp.data.credit_manager;
                $scope.customer_code = resp.data.customer_code;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
            });

            var url = 'api/deferral/getdeferralstages';

            SocketService.getparams(url, params).then(function (resp) {
               
                    $scope.stage_list = resp.data.stage_list;
               
            });

            // Close Modals

            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }
            $scope.downloads = function (val1, val2) {
                //var phyPath = val1;
                //var relPath = phyPath.split("StoryboardAPI");
                //var relpath1 = relPath[1].replace("\\", "/");
                //var hosts = window.location.host;
                //var prefix = location.protocol + "//";
                //var str = prefix.concat(hosts, relpath1);
                ////console.log(str);
                //var link = document.createElement("a");
                //var name = val2.split('.');
                //link.download = val2;
                //var uri = str;
                //link.href = uri;
                //link.click();
                DownloaddocumentService.Downloaddocument(val1, val2);

            }

        }

        $scope.deferralback = function (val) {
            $state.go('app.cadReport');
        }



    }
})();
