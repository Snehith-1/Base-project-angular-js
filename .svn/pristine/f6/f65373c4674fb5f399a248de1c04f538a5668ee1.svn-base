(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCadAcceptedCustomersController', AgrTrnCadAcceptedCustomersController);

    AgrTrnCadAcceptedCustomersController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrTrnCadAcceptedCustomersController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCadAcceptedCustomersController';
        //lockUI();
        activate();
        
        function activate() {
            lockUI();

            var url = 'api/AgrTrnCAD/CADApplicationCount';
            SocketService.get(url).then(function (resp) {
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.advancerejected_count = resp.data.advancerejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });

            var url = 'api/AgrTrnCAD/GetCADAcceptedCustomerSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.cadapplicationlist != null && resp.data.cadapplicationlist.length > 0) {
                    $scope.Cadacceptedcustomer_list = resp.data.cadapplicationlist;
                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
            });
          
          

         
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CADAcceptanceCustomers');
        }

        $scope.edit = function (val) {
            $location.url('app/AgrTrnCADApplicationEdit?application_gid=' + val + '&lspage=CADAcceptanceCustomers');
        }
       
        $scope.pendincad_review = function () {
            $location.url('app/AgrTrnPendingCADReview');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/AgrTrnCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/AgrTrnSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/AgrTrnSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/AgrTrnCCRejectedApplications');
        }

        $scope.assignment_view = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/assignmentdtl_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      application_gid: application_gid
                  }
                var url = 'api/AgrTrnCAD/GetAssignmentView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.assignment_list = resp.data.assignment_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_doc = function (val1, val2) {
                    var phyPath = val1;
                    var relPath = phyPath.split("EMS");
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


            }

        }
        
        $scope.reassign_application = function (val) {
            $location.url('app/AgrTrnCADReassignApplication?application_gid=' + val);
        }

        $scope.assign_application = function (val) {
            $location.url('app/AgrTrnCADGroupProcessAssign?application_gid=' + val + '&lspage=CADAcceptanceCustomers');
        }

        $scope.Advance_rejected = function () {
            $location.url('app/AgrTrnPmgAdvanceRejectedSummary');
        }

        $scope.exportreport = function () {
            lockUI();
            var url = 'api/AgrMstApplicationReport/GetExportCADAcceptedAppReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname); 
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }

        $scope.posttoerp_contract = function (application_gid) {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/SamAgroHBAPIContract/PostContractToERP';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {                   
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    GetCADAcceptedCustomerSummaryERP();
                }
                else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });               
                }
                

            });
        }

        function GetCADAcceptedCustomerSummaryERP() {
            getCADApplicationCount();
            lockUI();
            var url = 'api/AgrTrnCAD/GetCADAcceptedCustomerSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.cadapplicationlist != null && resp.data.cadapplicationlist.length > 0) {
                    $scope.Cadacceptedcustomer_list = resp.data.cadapplicationlist;
                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
            });
        }

        function getCADApplicationCount() {
            lockUI();
            var url = 'api/AgrTrnCAD/CADApplicationCount';
            SocketService.get(url).then(function (resp) {
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.advancerejected_count = resp.data.advancerejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
                unlockUI();
            });

        }


    }
})();
