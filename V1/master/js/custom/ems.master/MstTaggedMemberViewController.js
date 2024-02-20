(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstTaggedMemberViewController', MstTaggedMemberViewController);

    MstTaggedMemberViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstTaggedMemberViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstTaggedMemberViewController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var inboundcall_gid = searchObject.inboundcall_gid;
        lockUI();
        activate();
        function activate() {
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
                inboundcall_gid: inboundcall_gid
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
                 $scope.txtfunction_remarks = resp.data.function_remarks,
                 $scope.txttat_hours = resp.data.tat_hours,
                 $scope.txttat_days = resp.data.tat_days,
                 $scope.txttat_date = resp.data.tat_date,
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txtassign_date = resp.data.assign_date,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.ibcalladdress_list;
                $scope.ibcallfollowup_list = resp.data.ibcallfollowup_list;
                $scope.ibcalltaggedmember_list = resp.data.ibcalltaggedmember_list;
                $scope.txtcompleted_by = resp.data.completed_by,
                $scope.txtcompleted_date = resp.data.completed_date,
                $scope.txtcompleted_remarks = resp.data.completed_remarks,
                $scope.txtclosed_date = resp.data.closed_date,
                $scope.txtclosed_by = resp.data.closed_by,
                $scope.txtclosed_remarks = resp.data.closed_remarks
                $scope.ibcalladdress_list = resp.data.ibcalladdress_list;
                $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                $scope.ibcallmobileno_list = resp.data.ibcallmobileno_list,
                $scope.txtprimary_email = resp.data.primary_email,
                $scope.ibcallemail_list = resp.data.ibcallemail_list,
                unlockUI();
            });
        }

        $scope.Back = function () {
            $location.url("app/MstTaggedMemberSummary");
        }

       
    }
})();
