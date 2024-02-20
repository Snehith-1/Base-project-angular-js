(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnMyCampaignApprovalViewController', FndTrnMyCampaignApprovalViewController);

    FndTrnMyCampaignApprovalViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'DownloaddocumentService','cmnfunctionService'];

    function FndTrnMyCampaignApprovalViewController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnMyCampaignApprovalViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.campaign_gid = searchObject.lscampaign_gid;
        var campaign_gid = searchObject.lscampaign_gid;
        
        activate();
        function activate() {
           
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnCampaign/campaignDetailsView';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.campaign_code = resp.data.campaign_code;
                $scope.campaign_name = resp.data.campaign_name;
                $scope.cboCampaign_type = resp.data.campaigntype_gid;
                $scope.cboCampaign_type = resp.data.campaigntype_name;
                $scope.cboCustomer = resp.data.customer_gid;
                $scope.cboCustomer = resp.data.customer_name;
                $scope.contact_name = resp.data.contact_name;
                $scope.contact_mobile = resp.data.contact_mobile;
                $scope.contact_email = resp.data.contact_email;
                $scope.cboCampaign_apr = resp.data.campaign_approver,
                $scope.campaign_cost = resp.data.campaign_cost;
                $scope.start_date = resp.data.start_date;
                //$scope.cboCampaign_apr = resp.data.campaign_approver;
                $scope.end_date = resp.data.end_date;
                $scope.assesment_date = resp.data.assesment_date;
                $scope.os_assesment_date = resp.data.os_assesment_date;
                $scope.loan_availed = resp.data.loan_availed;
                $scope.cboCampaign_mgr = resp.data.employee_gid;
                $scope.cboCampaign_mgr = resp.data.manager_name;

                unlockUI();
            });
            var url = 'api/FndTrnMyCampaignSummary/GetMyCampaignApprovalRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mycampiagnraisequery_list = resp.data.mycampiagnraisequery_list;
            });


            var params = {

                campaign_gid: campaign_gid,
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSingleFormView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.singleform_list = resp.data.singleform_list;
            });
               defaultdynamic();
            unlockUI();
        }
        $scope.Back = function () {
            $state.go('app.FndTrnMyCampaignApprovalPending');
        }
        function defaultdynamic() {
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/GetSampleDynamicdata';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.SampleDynamicTabledata = "";
                    $scope.SampleDynamicdata = "";
                    $scope.SampleDynamicdata = JSON.parse(resp.data.JSONdata);
                    $scope.SampleDynamicTabledata = angular.copy($scope.SampleDynamicdata);
                 
                    $scope.SampleDynamicTable = angular.copy($scope.SampleDynamicTabledata);
             

                }
                else {
                    $scope.SampleDynamicTabledata = "";
                   
                 
                   
                }
            });
        }



        $scope.view_campaignquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/mycampaignqueryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = query_responseby;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        //Divya
        $scope.ExportExcel = function () {
            var params = {
                campaign_gid: campaign_gid
            }
            var url = 'api/FndTrnMyCampaignSummary/ExportSingleMultipleFormDetails';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
               
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    //DownloaddocumentService.Downloaddocument(val1, val2);
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Exporting !', 'warning')

                }
            });
        }

    }
})();
