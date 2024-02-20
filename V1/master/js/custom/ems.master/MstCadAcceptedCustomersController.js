(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadAcceptedCustomersController', MstCadAcceptedCustomersController);

    MstCadAcceptedCustomersController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstCadAcceptedCustomersController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadAcceptedCustomersController';

        activate();
        //lockUI();
        function activate() {
            lockUI();
            var url = 'api/MstCAD/GetCADAcceptedCustomerSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.cadapplicationlist != null && resp.data.cadapplicationlist.length > 0) {
                    $scope.Cadacceptedcustomer_list = resp.data.cadapplicationlist;

                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
               
            });
            var url = 'api/MstCAD/CADApplicationCount';
           //lockUI();
            SocketService.get(url).then(function (resp) {
              
                $scope.cadreview_count = resp.data.cadreview_count;
                $scope.sentbackcc_count = resp.data.sentbackcc_count;
                $scope.accept_count = resp.data.accept_count;
                $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
                $scope.ccrejected_count = resp.data.ccrejected_count;
                $scope.lstotalcount = resp.data.lstotalcount;
                $scope.urngrouping_count = resp.data.urngrouping_count;
                //unlockUI();
            });

            //unlockUI();
        }

        $scope.view = function (val,val1) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&employee_gid=' + val1 + '&lspage=CADAcceptanceCustomers');
        }

        $scope.edit = function (val,product_gid,variety_gid) {
            $location.url('app/MstCADApplicationEdit?application_gid=' + val +  '&product_gid=' + product_gid + '&variety_gid=' + variety_gid  + '&lspage=CADAcceptanceCustomers');
        }
       
        $scope.pendincad_review = function () {
            $location.url('app/MstPendingCADReview');
        }

        $scope.urn_grouping = function () {
            $location.url('app/MstCadUrnAcceptedCustomers');
        }

        $scope.cadaccepted_customers = function () {
            $location.url('app/MstCadAcceptedCustomers');
        }

        $scope.sendback_underwriting = function () {
            $location.url('app/MstSentBackToUnderwriting');
        }

        $scope.sendback_cc = function () {
            $location.url('app/MstSentBackToCC');
        }

        $scope.cc_rejected = function () {
            $location.url('app/MstCCRejectedApplications');
        }


        $scope.cadaccepted_verification = function (application_gid) {
            $location.url('app/MstCADAcceptColendingAdd?application_gid=' + application_gid + '&lspage=CADAcceptanceCustomers');
        }
        $scope.initiateLMS = function (application_gid) {
            $location.url('app/MstCADCustomerCreationLMS?application_gid=' + application_gid + '&lspage=CADCustomerInitiateLMS');
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
                var url = 'api/MstCAD/GetAssignmentView';
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
            $location.url('app/MstCADReassignApplication?application_gid=' + val);
        }

        $scope.assign_application = function (val) {
            $location.url('app/MstCADGroupProcessAssign?application_gid=' + val + '&lspage=CADAcceptanceCustomers');
        }

        $scope.ExportexcelCADAccepted = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportexcelCADAcceptedCus';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    
                }

            });
        }
        $scope.TATExportexcelCADAccepted = function () {
            lockUI();
            var url = 'api/MstApplicationReport/TATExportexcelCADAcceptedCus';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                    
                }

            });
        }

    }
})();
