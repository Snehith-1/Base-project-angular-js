(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstPSLCSAManagementController', MstPSLCSAManagementController);

    MstPSLCSAManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstPSLCSAManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstPSLCSAManagementController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstCAD/GetPSLCSAManagementSummary';
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                   if (resp.data.cadapplicationlist != null && resp.data.cadapplicationlist.length > 0) {
                $scope.Cadacceptedcustomer_list = resp.data.cadapplicationlist;
                unlockUI();
            }
            else if (resp.data.status == false)
            unlockUI();
            });
            // var url = 'api/MstCAD/CADApplicationCount';
            // SocketService.get(url).then(function (resp) {
            //     unlockUI();
            //     $scope.cadreview_count = resp.data.cadreview_count;
            //     $scope.sentbackcc_count = resp.data.sentbackcc_count;
            //     $scope.accept_count = resp.data.accept_count;
            //     $scope.backtounderwriting_count = resp.data.backtounderwriting_count;
            //     $scope.ccrejected_count = resp.data.ccrejected_count;
            //     $scope.lstotalcount = resp.data.lstotalcount;
            // });
        }

        // $scope.view = function (val) {
        //     $location.url('app/MstPSLCSAApplicationView?application_gid=' + val + '&lspage=CadDocumentChecklist');
        // }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=PSLCSAManagement');
        }


        // $scope.process = function () {
        //     $location.url('app/MstPSLCSAGuarantorDetails');
        // }

        $scope.process = function (val) {
            $location.url('app/MstCADPSLCSAGuarantorDetails?application_gid=' + val + '&lspage=PSLCSAGuarantor');
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

        $scope.pslcsareportpending = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportMstPSLCSAManagementPending';
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

        $scope.pslcsareportcompleted = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportMstPSLCSAManagement';
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
