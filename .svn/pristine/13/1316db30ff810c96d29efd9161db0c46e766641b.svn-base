(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRejectedCallViewController', MstRejectedCallViewController);

    MstRejectedCallViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstRejectedCallViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRejectedCallViewController';
       
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var inboundcall_gid = searchObject.inboundcall_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();
        lockUI();
        function activate() {
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
                $scope.txtrequirement = resp.data.requirement,
                $scope.txtenquiry_description = resp.data.enquiry_description,
                $scope.txtcallclosure_status = resp.data.callclosure_status,
                $scope.txtassignemployee_name = resp.data.assignemployee_name,
                $scope.txttagemployee_name = resp.data.tagemployee_name,
                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks,
                $scope.ibcalladdress_list = resp.data.ibcalladdress_list;
                $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                $scope.ibcallmobileno_list = resp.data.ibcallmobileno_list;
                $scope.txtprimary_email = resp.data.primary_email,
                $scope.ibcallemail_list = resp.data.ibcallemail_list;
                $scope.ibcallfollowup_list = resp.data.ibcallfollowup_list;
                $scope.ibcalltaggedmember_list = resp.data.ibcalltaggedmember_list;
                $scope.txtrejected_date = resp.data.rejected_date;
                $scope.txtrejected_by = resp.data.rejected_by;
                $scope.txtrejected_remarks = resp.data.rejected_remarks;
                $scope.txttat_hours = resp.data.tat_hours,
                $scope.txttat_date = resp.data.tat_date,
                $scope.txttat_days = resp.data.tat_days,
                $scope.txtfunction_remarks = resp.data.function_remarks,
                unlockUI();
            });
        }
        $scope.Back = function () {
            $location.url('app/MstRejectedCallSummary');
        }
    }
})();
