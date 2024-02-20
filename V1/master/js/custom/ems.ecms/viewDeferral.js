(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewDeferral', viewDeferral);

    viewDeferral.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService'];

    function viewDeferral($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService) {
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
                $scope.vertical_code = resp.data.vertical_code;
                $scope.entity_name = resp.data.entity_name;
                $scope.branch_name = resp.data.branch_name;
                $scope.customer_code = resp.data.customer_code;
                $scope.cluster_manager_name = resp.data.cluster_manager_name;
            });
            // Close Modals


            $scope.close = function (val) {
                document.getElementById("userform").reset();
                var doc = document.getElementById(val);
                doc.style.display = 'none';
            }

            //$scope.upload = function (val, val1, name) {
            //    var item = {
            //        name: val[0].name,
            //        file: val[0]
            //    };
            //    var frm = new FormData();
            //    frm.append('fileupload', item.file);
            //    frm.append('file_name', item.name);
            //    frm.append('document_name', $scope.documentname);
            //    frm.append('deferral_gid', $scope.deferral_gid);
            //    frm.append('loan_gid', $scope.loan_gid);
            //    frm.append('project_flag', "Default");
            //    $scope.uploadfrm = frm;
            //    var url = 'api/deferral/UploadcadDocument';
            //    lockUI();
            //    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
            //        unlockUI();
            //        $scope.filename_list = resp.data.filename_list;

            //        $("#addupload").val('');

            //        if (resp.data.status == true) {
            //            activate();
            //            Notify.alert('Document Uploaded Successfully..!!', 'success')
            //            var modalInstance = $modal.open({
            //                templateUrl: '/UploadDocument.html',
            //                controller: ModalInstanceCtrl,
            //                size: 'md'
            //            });
            //        }
            //        else {
            //            unlockUI();
            //            Notify.alert('File Format Not Supported!')
            //        }
            //        activate();
            //    });

            //}
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
            $state.go('app.DeferralManagement');
        }
         


    }
})();
