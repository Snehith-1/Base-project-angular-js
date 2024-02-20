(function () {
    'use strict';
    angular
        .module('angle')
        .controller('AtmTrnSamplingController', AtmTrnSamplingController);

    AtmTrnSamplingController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmTrnSamplingController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnSamplingController';
        
        var auditcreation_gid = $location.search().auditcreation_gid;
        //$scope.auditcreation_gid = $location.search().auditcreation_gid;
        activate();
        function activate() {

            var params = {
                auditcreation_gid: auditcreation_gid,
                
            };


            var url = 'api/AtmTrnAuditCreation/GetSample';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI()
                $scope.sample_list = resp.data.sample_list
                
            });
                 
        }
               
        $scope.btntaguser = function (sampleimport_gid, auditcreation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/taguser.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    sampleimport_gid: sampleimport_gid
                }

                var url = 'api/AtmTrnSampling/GetSampleName';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sampleimport_gid = resp.data.sampleimport_gid;
                    $scope.txtsample_name = resp.data.sample_name;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.btnconfirm = function () {

                    var params = {
                        employelist: $scope.cboemployee_name,
                        sample_name: $scope.txtsample_name,
                        sampleimport_gid: $scope.sampleimport_gid,
                        auditcreation_gid: auditcreation_gid
                    }

                    var url = 'api/AtmTrnSampling/GetTagUser';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $location.url('app/AtmTrnSampling?auditcreation_gid=' + val1)
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                    $modalInstance.close('closed');

                }

                //var params = {
                //    auditcreation_gid: auditcreation_gid,

                //};

                //$location.url('app/AtmTrnSampling?auditcreation_gid=' + val1)

                //var url = 'api/AtmTrnSampling/GetSample';
                //SocketService.getparams(url, params).then(function (resp) {
                //    unlockUI()
                //    auditcreation_gid = resp.data.auditcreation_gid
                //    $scope.sample_list = resp.data.sample_list

                //});

            }
        }

        $scope.showPopover = function (sampleimport_gid, sample_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showemployee.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sampleimport_gid: sampleimport_gid
                }
                lockUI();
                var url = 'api/AtmTrnSampling/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.employee_name = resp.data.employee_name;
                    $scope.sample_name = resp.data.sample_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        //$scope.raisequery = function () {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/raisequery.html',
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
        //            $scope.employee_list = resp.data.employeelist;
        //            unlockUI();
        //        });

        //        var url = 'api/vertical/vertical';
        //        lockUI();
        //        SocketService.get(url).then(function (resp) {
        //            $scope.vertical = resp.data.vertical_list;
        //            unlockUI();
        //        });

        //        $scope.submit = function () {

        //            var params = {
        //                employee_gid: $scope.cboemployee.employee_gid,
        //                employee_name: $scope.cboemployee.employee_name,
        //                vertical_gid: $scope.cbovertical.vertical_gid,
        //                vertical_name: $scope.cbovertical.vertical_name
        //            }

        //            //var url = 'api//';
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


        $scope.raisequery = function (sampleimport_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/raisequery.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    sampleimport_gid: sampleimport_gid
                }
                var url = 'api/AtmTrnSampling/EditSampleQuery';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sampleimport_gid = resp.data.sampleimport_gid

                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });



                $scope.submit = function () {

                    var params = {
                        sampleimport_gid: $scope.sampleimport_gid,
                        employelist: $scope.cboemployee_name,
                        description: $scope.txtdescription,

                    }

                    var url = 'api/AtmTrnSampling/PostRaiseQuery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $location.url('app/AtmTrnSampling?auditcreation_gid=' + val1)
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                    $modalInstance.close('closed');

                }

            }
        }

        
        $scope.view = function ( val1) {
            var auditcreation_gid = $scope.auditcreation_gid;
            $location.url('app/AtmTrnSamplingView?auditcreation_gid=' + auditcreation_gid + '&sampleimport_gid=' + val1)
        }


        $scope.back = function (val) {
            $state.go('app.AtmTrnMyAuditTaskAuditeeSummary');
        }

    }

})();