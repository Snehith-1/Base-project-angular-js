(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditMappingSummaryController', MstCreditMappingSummaryController);

    MstCreditMappingSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstCreditMappingSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditMappingSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/MstCreditMapping/GetCreditGroupSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.CreditGroup = resp.data.CreditGroup;
                unlockUI();
            });
        }

        $scope.addcreditgroup = function () {
            $location.url('app/MstCreditMappingAdd');
        }

        $scope.editcreditgroup = function (creditmapping_gid) {
            $location.url('app/MstCreditMappingEdit?lscreditmapping_gid=' + creditmapping_gid);
        }
        $scope.logcreditgroup = function (creditmapping_gid) {
            $location.url('app/MstCreditMappingDetails?lscreditmapping_gid=' + creditmapping_gid);
        }
        $scope.editRule = function (creditmapping_gid) {
            $location.url('app/MstCreditMappingRule?lscreditmapping_gid=' + creditmapping_gid); 
        }

        $scope.creditmapplingexcel = function () {
            lockUI();
            var url = 'api/MstCreditMapping/ExportMstCreditMapping';
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
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }

        //$scope.addcreditgroup = function () {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/addcreditmapping.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });

        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        var url = 'api/SystemMaster/GetEmployeelist';
        //        lockUI();
        //        SocketService.get(url).then(function (resp) {
        //            $scope.cbocredithead_list = resp.data.employeelist;
        //            unlockUI();
        //        });
        //        var url = 'api/SystemMaster/GetEmployeelist';
        //        lockUI();
        //        SocketService.get(url).then(function (resp) {
        //            $scope.cbonationalmanager_list = resp.data.employeelist;
        //            unlockUI();
        //        });
        //        var url = 'api/SystemMaster/GetEmployeelist';
        //        lockUI();
        //        SocketService.get(url).then(function (resp) {
        //            $scope.cboregionalmanager_list = resp.data.employeelist;
        //            unlockUI();
        //        });
        //        var url = 'api/SystemMaster/GetEmployeelist';
        //        lockUI();
        //        SocketService.get(url).then(function (resp) {
        //            $scope.cbocreditmanager_list = resp.data.employeelist;
        //            unlockUI();
        //        });
        //        $scope.submit = function () {

        //            var params = {
        //                Credithead: $scope.cbocredithead,
        //                Creditnationalmanager: $scope.cbonationalmanager,
        //                Creditregionalmanager: $scope.cboregionalmanager,
        //                CreditManager: $scope.cbocreditmanager,
        //                creditgroup_name: $scope.txtcreditgroupname

        //            }
        //            var url = 'api/MstCreditMapping/PostCreditGroupAdd';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    $modalInstance.close('closed');
        //                    activate();
        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //            });

        //            $modalInstance.close('closed');

        //        }

        //    }
        //}

        //$scope.editcreditgroup = function (creditmapping_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editcreditmapping.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });

        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        lockUI();
        //        var params = {
        //            creditmapping_gid: creditmapping_gid
        //        }
        //        var url = 'api/MstCreditMapping/GetCreditGroupEdit';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            unlockUI();
        //            $scope.txteditcreditgroup_name = resp.data.creditgroup_name;
        //            $scope.cbocredithead_editlist = resp.data.Creditheadem_list;
        //            $scope.Credithead = resp.data.Credithead;
        //            $scope.cbocredithead_edit = [];
        //            if (resp.data.Credithead != null) {
        //                var count = resp.data.Credithead.length;
        //                for (var i = 0; i < count; i++) {
        //                    var indexs = $scope.cbocredithead_editlist.findIndex(x => x.employee_gid === resp.data.Credithead[i].employee_gid);
        //                    $scope.cbocredithead_edit.push($scope.cbocredithead_editlist[indexs]);
        //                    $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
        //                }
        //            }
        //            $scope.cbonational_editlist = resp.data.Creditnationalmanagerem_list;
        //            $scope.Creditnationalmanager = resp.data.Creditnationalmanager;
        //            $scope.cbonational_edit = [];
        //            if (resp.data.Creditnationalmanager != null) {
        //                var count = resp.data.Creditnationalmanager.length;
        //                for (var i = 0; i < count; i++) {
        //                    var indexs = $scope.cbonational_editlist.findIndex(x => x.employee_gid === resp.data.Creditnationalmanager[i].employee_gid);
        //                    $scope.cbonational_edit.push($scope.cbonational_editlist[indexs]);
        //                    $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
        //                }
        //            }
        //            $scope.cboregional_editlist = resp.data.Creditregionalmanagerem_list;
        //            $scope.Creditregionalmanager = resp.data.Creditregionalmanager;
        //            $scope.cboregional_edit = [];
        //            if (resp.data.Creditregionalmanager != null) {
        //                var count = resp.data.Creditregionalmanager.length;
        //                for (var i = 0; i < count; i++) {
        //                    var indexs = $scope.cboregional_editlist.findIndex(x => x.employee_gid === resp.data.Creditregionalmanager[i].employee_gid);
        //                    $scope.cboregional_edit.push($scope.cboregional_editlist[indexs]);
        //                    $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
        //                }
        //            }
        //            $scope.cbocreditmanager_editlist = resp.data.CreditManagerem_list;
        //            $scope.CreditManager = resp.data.CreditManager;
        //            $scope.cbocreditmanager_edit = [];
        //            if (resp.data.CreditManager != null) {
        //                var count = resp.data.CreditManager.length;
        //                for (var i = 0; i < count; i++) {
        //                    var indexs = $scope.cbocreditmanager_editlist.findIndex(x => x.employee_gid === resp.data.CreditManager[i].employee_gid);
        //                    $scope.cbocreditmanager_edit.push($scope.cbocreditmanager_editlist[indexs]);
        //                    $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
        //                }
        //            }
        //        });


        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.update = function () {
        //            lockUI();
        //            var url = 'api/MstCreditMapping/PostCreditGroupUpdate';
        //            var params = {
        //                creditmapping_gid: creditmapping_gid,
        //                Credithead: $scope.cbocredithead_edit,
        //                Creditnationalmanager: $scope.cbonational_edit,
        //                Creditregionalmanager: $scope.cboregional_edit,
        //                CreditManager: $scope.cbocreditmanager_edit,
        //                creditgroup_name: $scope.txteditcreditgroup_name
        //            }
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();

        //                }
        //                else {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //            });
        //            $modalInstance.close('closed');
                   
        //        }
        //    }
        //}

        $scope.showPopover = function (creditmapping_gid, creditgroup_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showcreditheads.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    creditmapping_gid: creditmapping_gid
                }
                lockUI();
                var url = 'api/MstCreditMapping/GetCreditgroupHeads';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.Credithead = resp.data.credithead;
                    $scope.Creditnationalmanager = resp.data.creditnational_manager;
                    $scope.Creditregionalmanager = resp.data.creditregional_manager;
                    $scope.CreditManager = resp.data.creditmanager;
                    $scope.creditgroup_name = creditgroup_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Status_update = function (creditmapping_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscreditgroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    creditmapping_gid: creditmapping_gid
                }
                var url = 'api/MstCreditMapping/GetCreditGroupEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.creditmapping_gid = resp.data.creditmapping_gid
                    $scope.creditgroup_name = resp.data.creditgroup_name;
                    $scope.rbo_status = resp.data.creditgroup_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        creditmapping_gid: creditmapping_gid,
                        creditgroup_name: $scope.creditgroup_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstCreditMapping/PostCreditgroupInactive';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    creditmapping_gid: creditmapping_gid
                }

                var url = 'api/MstCreditMapping/GetCreditgroupInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.Creditlog = resp.data.Creditlog;
                    unlockUI();
                });

            }
        }
    }
})();
