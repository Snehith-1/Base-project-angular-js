(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdMstBusinessUnitEdit', osdMstBusinessUnitEdit);

    osdMstBusinessUnitEdit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal','cmnfunctionService'];

    function osdMstBusinessUnitEdit($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdMstBusinessUnitEdit';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var businessunit_gid = searchObject.businessunit_gid;
      

        activate();
        function activate() {

            var url = 'api/OsdMstDepartmentManagement/GetBusinessStatusTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var params = {
                            businessunit_gid: businessunit_gid
                        }
                        var url = 'api/OsdMstDepartmentManagement/BusinessUnitView';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.txtbusinessunit_codeedit = resp.data.businessunit_code,
                            $scope.txtbusinessunit_prefixedit = resp.data.businessunit_prefix;
                            $scope.txtbusinessunit_nameedit = resp.data.businessunit_name,
                            $scope.txtbusinessunit_emailaddressedit = resp.data.businessunit_emailaddress;
                            if(resp.data.sequence_flag == 'Y')
                            {
                                $scope.label = true
                                $scope.text = false
                            }
                            else {
                                $scope.label = false
                                $scope.text = true
                            }
                        });
                        
                        var url = 'api/OsdMstDepartmentManagement/GetBusinessstatusEdit';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.businessstatusEdit_list = resp.data.businessstatusEdit_list;
                            unlockUI();
                        });
            

        }
        $scope.businessunitUpdate = function () {
                  
                        var params = {
                            businessunit_prefix: $scope.txtbusinessunit_prefixedit,
                            businessunit_name: $scope.txtbusinessunit_nameedit,
                            businessunit_emailaddress: $scope.txtbusinessunit_emailaddressedit,
                            businessunit_gid: businessunit_gid,
                        }

                        var url = 'api/OsdMstDepartmentManagement/UpdateBusinessUnit';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                $state.go('app.osdMstBusinessUnit');
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                                activate();
                                unlockUI();
                            }
                        });
                    }
        $scope.businessstatus_add = function () {

            if (($scope.txtbusinessunit_status == undefined) || ($scope.txtbusinessunit_status == '')) {
                Notify.alert('Enter Business Unit Status', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                var businessunit_gid = cmnfunctionService.decryptURL($location.search().hash).businessunit_gid;
                var params = {
                    business_status: $scope.txtbusinessunit_status,
                    businessunit_gid: businessunit_gid,

                }
                var url = 'api/OsdMstDepartmentManagement/PostBusinessUnitStatusEdit';
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
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtbusinessunit_status = '';
                    businessstatus_list();

                });
            }

        }
        $scope.businessstatus_delete = function (businessstatus_gid) {
            var params =
                {
                    businessstatus_gid: businessstatus_gid
                }
            console.log(params)
            var url = 'api/OsdMstDepartmentManagement/DeleteBusinessstatus';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
                businessstatus_list();

            });

        }
        function businessstatus_list() {
            var url = 'api/OsdMstDepartmentManagement/GetBusinessstatusList';
            var params = {
                user_gid: $scope.user_gid,

            }
            SocketService.getparams(url, params).then(function (resp) {
                $scope.businessstatusunit_list = resp.data.businessstatusunit_list;

            });
            activate();
        }

        $scope.back = function () {

            $location.url('app/osdMstBusinessUnit');

        }
    }
})();