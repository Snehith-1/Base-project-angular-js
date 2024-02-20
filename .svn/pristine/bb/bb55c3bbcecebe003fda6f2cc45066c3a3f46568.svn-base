(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstPSLCSACompleteController', MstPSLCSACompleteController);

        MstPSLCSACompleteController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstPSLCSACompleteController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstPSLCSACompleteController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstCAD/GetPSLCSACompleteSummary';
            SocketService.get(url).then(function (resp) {
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
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=PSLCSAComplete');
        }


        // $scope.process = function () {
        //     $location.url('app/MstPSLCSAGuarantorDetails');
        // }

        $scope.process = function (val) {
            $location.url('app/MstCADPSLCSAGuarantorDetails?application_gid=' + val + '&lspage=PSLCSAComplete');
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

        
        // Showoverpopup

        $scope.completeremarks = function (pslcompleteremarks) {
            var modalInstance = $modal.open({
                templateUrl: '/completeremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.pslcompleteremarks = pslcompleteremarks;
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
            }
        }
    }
})();
