(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstTeleCallingCloseController', MstTeleCallingCloseController);

    MstTeleCallingCloseController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstTeleCallingCloseController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstTeleCallingCloseController';
       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var inboundcall_gid = searchObject.lsinboundcall_gid;
        activate();
        function activate() {

            $scope.followup_show = false;

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var params = {
                inboundcall_gid:inboundcall_gid
            }
            var url = 'api/TeleCalling/GetIBCallAssignedView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid,
                $scope.txtentity_name = resp.data.entity_name,
                $scope.txtsourceofcontact_name = resp.data.sourceofcontact_name,
                $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name,
                $scope.txtcustomer_type = resp.data.customer_type,
                $scope.txtcallreceived_date = resp.data.callreceived_date,
                $scope.txtcaller_name = resp.data.caller_name,
                $scope.txtinternalreference_name = resp.data.internalreference_name,
                $scope.txtcallerassociate_company = resp.data.callerassociate_company,
                $scope.txtoffice_landlineno = resp.data.office_landlineno,
                $scope.txtcalltype_name = resp.data.calltype_name,
                $scope.txtfunction_name = resp.data.function_name,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.txtcompleted_by = resp.data.completed_by,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
                $scope.txttat_hours = resp.data.tat_hours,
                $scope.txttat_days = resp.data.tat_days,
                $scope.txttat_date = resp.data.tat_date,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                unlockUI();
            });
        
        }

        $scope.Back = function () {
            $location.url('app/MstTeleCallingCompletedCall');
        }

        $scope.changeclosurestatus = function (cboclosurestatus) {
            if (cboclosurestatus == 'Extend Follow Up') {
                $scope.followup_show = true;
            }
            else {
                $scope.followup_show = false;
            }
            if (cboclosurestatus == 'Closed') {
                $scope.Closed_show = true;
            }
            else {
                $scope.Closed_show = false;
            }
        }

        $scope.submit = function () {
            if (($scope.cboclosurestatus == 'Extend Follow Up') && ($scope.txtfollowup_date == '' || $scope.txtfollowup_date == null || $scope.txtfollowup_time == null || $scope.txtfollowup_time == null || $scope.txtremarks == null || $scope.txtremarks == null)) {
                Notify.alert('Kindly Fill Follow Up Details', 'warning')
            }
            else if (($scope.cboclosurestatus == 'Closed') && ($scope.closedremarks == '' || $scope.closedremarks == null)) {
                Notify.alert('Kindly Fill Closed Remarks', 'warning')
            }
            else {
                var params = {
                    inboundcall_gid: inboundcall_gid,
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                    followup_remarks: $scope.txtremarks,
                    closure_status: $scope.cboclosurestatus,
                    closed_remarks: $scope.closedremarks
                }
                var url = 'api/TeleCalling/PostCloseCall';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go("app.MstTeleCallingCompletedCall");
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
                $state.go("app.MstTeleCallingCompletedCall");
                activate();
            }
        }
}
})();
