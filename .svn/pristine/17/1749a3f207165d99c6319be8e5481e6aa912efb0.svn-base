(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstSanctioneset', idasMstSanctioneset);

    idasMstSanctioneset.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasMstSanctioneset($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'idasMstSanctioneset';

        activate();

        function activate() {


            $scope.tab = {};
            lockUI();
            var url = "api/IdasMstSanction/GetSanctionsummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionlist = resp.data.sanctiondetails;
               
            });
            var url = window.location.href;
            var relPath = url.split("lstab=");
            var relpath1 = relPath[1];
            if (relpath1 != undefined) {
                if (relpath1 == "pending") {
                    $scope.tabpending = true;
                }
                else if (relpath1 == "completed") {
                    $scope.tabcompleted = true;
                }

            }
            else {
                if ($scope.tab.activeTabId == undefined) {
                    $scope.tabpending = true;
                }
                else if ($scope.tab.activeTabId == 'pending') {
                    $scope.tabpending = true;

                }
                else if ($scope.tab.activeTabId == 'completed') {
                    $scope.tabcompleted = true;
                }

            }

        }
       
        $scope.history = function (customer2sanction_gid) {
            localStorage.setItem('customer2sanction_gid', customer2sanction_gid);
          $state.go('app.IdasMstHistorySanctionRefNo');


        }
      
       
        $scope.sanction_cancel = function (customer2sanction_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/cancelsanction.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/entity/Entity';

                SocketService.get(url).then(function (resp) {
                    $scope.entity_list = resp.data.entity_list;

                });
                var params = {
                    sanction_gid: customer2sanction_gid
                }

                var url = 'api/IdasMstSanction/GetSanctioninfo';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.lblcustomername = resp.data.customername;
                    $scope.lblsanction_refno = resp.data.sanction_refno;
                    $scope.lblcustomer_urn = resp.data.customer_urn;
                    $scope.lblsanction_date = resp.data.sanction_date;
                    $scope.lblsanction_amount = resp.data.sanction_amount;
                    $scope.cboentity_type = resp.data.entity_gid;

                    $scope.rdbcolanding = resp.data.colanding_status;
                    $scope.txtcolander_name = resp.data.colander_name;
                    if (resp.data.colanding_status == 'Yes') {
                        $scope.colandingyes = true;
                    }
                });
                $scope.default = true;
                $scope.close = function () {
                    $modalInstance.close('closed');
                };
                $scope.rdbcolanding_yes = function () {
                    $scope.colandingyes = true;
                    $scope.mandatoryremarks = false;
                }
                $scope.rdbcolanding_no = function () {
                    $scope.colandingyes = false;
                    $scope.mandatoryremarks = false;
                }
                $scope.remarks = function () {
                    $scope.mandatoryremarks = false;
                }
                $scope.reset = function () {
                    if (($scope.txtremarks == "") || ($scope.txtremarks == undefined)) {
                        $scope.mandatoryremarks = true;
                    }
                    else {
                        $scope.mandatoryremarks = false;

                        var params = {
                            customer2sanction_gid: customer2sanction_gid,
                            general_remarks: $scope.txtremarks,
                            colanding_status: $scope.rdbcolanding,
                            colander_name: $scope.txtcolander_name,
                            entity_gid: $scope.cboentity_type,

                        }
                        console.log(params)
                        var url = 'api/IdasMstSanction/Sanction_cancel';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                activate();

                            }
                            else {
                                $modalInstance.close('closed');

                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });


                            }
                        });
                    }
                }
            }

        }

    }
})();
