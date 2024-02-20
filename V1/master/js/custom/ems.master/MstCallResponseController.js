(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCallResponseController', MstCallResponseController);

    MstCallResponseController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstCallResponseController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCallResponseController';
       
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
                   
                   $scope.ibcallfollowup_list = resp.data.ibcallfollowup_list;
                   $scope.ibcalltaggedmember_list = resp.data.ibcalltaggedmember_list;
                   $scope.txtacknowledgement_date = resp.data.acknowledge_date;
                   $scope.ibcalladdress_list = resp.data.ibcalladdress_list;
                   $scope.txtprimary_mobileno = resp.data.primary_mobileno,
                   $scope.ibcallmobileno_list = resp.data.ibcallmobileno_list,
                   $scope.txtprimary_email = resp.data.primary_email,
                   $scope.ibcallemail_list = resp.data.ibcallemail_list,
                   unlockUI();
               });
        }

        $scope.Back = function () {
            $location.url("app/MstMyAssignedCallSummary");
        }

        $scope.Reject = function () {
            var modalInstance = $modal.open({
                templateUrl: '/rejectrequest.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.rejectSubmit = function () {
                    var params = {
                        inboundcall_gid: inboundcall_gid,
                        reject_remarks: $scope.txtreject_remarks
                    }
                    var url = 'api/TeleCalling/RejectIBCall';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                            $state.go("app.MstMyAssignedCallSummary");
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            modalInstance.close('closed');
                        }
                    });
                    $state.go("app.MstMyAssignedCallSummary");
                }
            }
        }
        $scope.acknowledge = function () {
            var url = 'api/TeleCalling/PostUpdateAck';
            lockUI();
            var params = {
                inboundcall_gid: inboundcall_gid
            }
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMyAssignedCallSummary");
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMyAssignedCallSummary");
                }
            });
        }
    }
})();