
(function () {
    'use strict';
    
    angular
        .module('angle')
        .controller('SysMstEmployeeDeactivateController', SysMstEmployeeDeactivateController);

        SysMstEmployeeDeactivateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert'];

    function SysMstEmployeeDeactivateController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog,SweetAlert ) {
        var employee_gid = $location.search().employee_gid;
        var lstab = $location.search().lstab;
        $scope.lstab = lstab;
        activate();
        var vm = this;
        vm.title = 'SysMstEmployeeDeactivateController';
        
        function activate() {
            lockUI();
            var url = 'api/ManageEmployee/PopRole';
            SocketService.get(url).then(function (resp) {
                $scope.roleList = resp.data.rolemaster;
                

                var params = {
                    employee_gid: employee_gid
                };
                if ($scope.lstab == 'pending') {
                    var url = 'api/ManageEmployee/EmployeePendingEditView';
                } 
                else {
                    var url = 'api/ManageEmployee/EmployeeEditView';
                }
                //var url = 'api/ManageEmployee/EmployeeEditView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.employee_details = resp.data;
                    unlockUI();
                });
               
            });

            var params = {
                deactivateemployee_gid: employee_gid
            }
            var url = 'api/ManageEmployee/GetDeactivationCondition';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
               
                $scope.lblasset_status = resp.data.asset_status;
                $scope.lbltempasset_status = resp.data.tempasset_status;
                $scope.lblemployeereporting_to = resp.data.employeereporting_to;
                $scope.lblmodule_name = resp.data.module_name;
                $scope.lbsubmitbutton = resp.data.submitbutton;
                //$scope.creditapproval = resp.data.appcreditapproval_gid;


                if ((resp.data.asset_status == "" || resp.data.asset_status == null)) {
                    $scope.showasset_status = false;
                    if (resp.data.tempasset_status == "" || resp.data.tempasset_status == null) {
                        $scope.showtempasset_status = false;
                    }
                    else {
                        $scope.showtempasset_status = true;
                    }
                    
                }               
                else {
                    $scope.showasset_status = true;
                    
                    if (resp.data.tempasset_status == "" || resp.data.tempasset_status == null) {
                        $scope.showtempasset_status = false;
                    }
                    else {
                        $scope.showtempasset_status = true;
                      
                    }
                }

                if (resp.data.module_name == "" || resp.data.module_name == null) {
                    $scope.showmodule_name = false;
                }
                else {
                    $scope.showmodule_name = true;
                }

                if (resp.data.applicationapproval_gid == "" || resp.data.applicationapproval_gid == null) {
                   $scope.showapplicationapproval_gid = false;
                }
                else {
                    $scope.showapplicationapproval_gid = true;
                }
                if (resp.data.appcreditapproval_gid == "" || resp.data.appcreditapproval_gid == null) {
                    $scope.showappcreditapproval_gid = false;
                }
                else {
                    $scope.showappcreditapproval_gid = true;
                }
                if (resp.data.ccmeeting2members_gid == "" || resp.data.ccmeeting2members_gid == null) {
                    $scope.showccmeeting2members_gid = false;
                }
                else {
                    $scope.showccmeeting2members_gid = true;
                }
                if (resp.data.cadgroupmanager_gid == "" || resp.data.cadgroupmanager_gid == null) {
                    $scope.showcadgroupmanager_gid = false;
                }
                else {
                    $scope.showcadgroupmanager_gid = true;
                }
                if (resp.data.cadgroupmembers_gid == "" || resp.data.cadgroupmembers_gid == null) {
                    $scope.showcadgroupmembers_gid = false;
                }
                else {
                    $scope.showcadgroupmembers_gid = true;
                }
                if (resp.data.creditops2maker_gid == "" || resp.data.creditops2maker_gid == null) {
                    $scope.showcreditops2maker_gid = false;
                }
                else {
                    $scope.showcreditops2maker_gid = true;
                }
                if (resp.data.creditops2checker_gid == "" || resp.data.creditops2checker_gid == null) {
                    $scope.showcreditops2checker_gid = false;
                }
                else {
                    $scope.showcreditops2checker_gid = true;
                }
                if (resp.data.creditmapping_gid == "False") {
                    $scope.showcreditmapping_gid = false;
                }
                else {
                    $scope.showcreditmapping_gid = true;
                }
                if (resp.data.processtype_assign == "False") {
                    $scope.showprocesstype_assign = false;
                }
                else {
                    $scope.showprocesstype_assign = true;
                }
                if (resp.data.application_gid == "" || resp.data.application_gid == null) {
                    $scope.showapplication_gid = false;
                }
                else {
                    $scope.showapplication_gid = true;
                }

                //Samagro
                if (resp.data.agrapplicationapproval_gid == "" || resp.data.agrapplicationapproval_gid == null) {
                    $scope.showagrapplicationapproval_gid = false;
                }
                else {
                    $scope.showagrapplicationapproval_gid = true;
                }
                if (resp.data.agrappcreditapproval_gid == "" || resp.data.agrappcreditapproval_gid == null) {
                    $scope.showagrappcreditapproval_gid = false;
                }
                else {
                    $scope.showagrappcreditapproval_gid = true;
                }
                if (resp.data.agrccmeeting2members_gid == "" || resp.data.agrccmeeting2members_gid == null) {
                    $scope.showagrccmeeting2members_gid = false;
                }
                else {
                    $scope.showagrccmeeting2members_gid = true;
                }
                if (resp.data.agrcadgroupmanager_gid == "" || resp.data.agrcadgroupmanager_gid == null) {
                    $scope.showagrcadgroupmanager_gid = false;
                }
                else {
                    $scope.showagrcadgroupmanager_gid = true;
                }
                if (resp.data.agrprocesstype_assign == "False") {
                    $scope.showagrprocesstype_assign = false;
                }
                else {
                    $scope.showagrprocesstype_assign = true;
                }

                if (resp.data.ccmembermaster_gid == "" || resp.data.ccmembermaster_gid == null) {
                    $scope.showccmembermaster_gid = false;
                }
                else {
                    $scope.showccmembermaster_gid = true;
                }
                if (resp.data.agrcreditmapping_gid == "False") {
                    $scope.showagrcreditmapping_gid = false;
                }
                else {
                    $scope.showagrcreditmapping_gid = true;
                }





                if (resp.data.productdeskmanager_gid == "" || resp.data.productdeskmanager_gid == null) {
                    $scope.showproductdeskmanager_gid = false;
                }
                else {
                    $scope.showproductdeskmanager_gid = true;
                }
                if (resp.data.productdeskmember_gid == "" || resp.data.productdeskmember_gid == null) {
                    $scope.showproductdeskmember_gid = false;
                }
                else {
                    $scope.showproductdeskmember_gid = true;
                }
                if (resp.data.mstpmgapproval_gid == "" || resp.data.mstpmgapproval_gid == null) {
                    $scope.showmstpmgapproval_gid = false;
                }
                else {
                    $scope.showmstpmgapproval_gid = true;
                }
                if (resp.data.mstproductapproval_gid == "" || resp.data.mstproductapproval_gid == null) {
                    $scope.showmstproductapproval_gid = false;
                }
                else {
                    $scope.showmstproductapproval_gid = true;
                }


                if (resp.data.appproductapproval_gid == "" || resp.data.appproductapproval_gid == null) {
                    $scope.showappproductapproval_gid = false;
                }
                else {
                    $scope.showappproductapproval_gid = true;
                }

                if (resp.data.warehouse2approval_gid == "" || resp.data.warehouse2approval_gid == null) {
                    $scope.showwarehouse2approval_gid = false;
                }
                else {
                    $scope.showwarehouse2approval_gid = true;
                }

                if (resp.data.agrapplication_gid == "" || resp.data.agrapplication_gid == null) {
                    $scope.showagrapplication_gid = false;
                }
                else {
                    $scope.showagrapplication_gid = true;
                }


                // Audit
                if (resp.data.auditmapping2employee_gid == "" || resp.data.auditmapping2employee_gid == null) {
                    $scope.showauditmapping2employee_gid = false;
                }
                else {
                    $scope.showauditmapping2employee_gid = true;
                }
                if (resp.data.multipleauditee_gid == "" || resp.data.multipleauditee_gid == null) {
                    $scope.showmultipleauditee_gid = false;
                }
                else {
                    $scope.showmultipleauditee_gid = true;
                }
                if (resp.data.auditcreation_gid == "False") {
                    $scope.showauditcreation_gid = false;
                }
                else {
                    $scope.showauditcreation_gid = true;
                }
                // Foundation
                if (resp.data.customerapproving_gid == "" || resp.data.customerapproving_gid == null) {
                    $scope.showcustomerapproving_gid = false;
                }
                else {
                    $scope.showcustomerapproving_gid = true;
                }
                if (resp.data.campaign_gid == "" || resp.data.campaign_gid == null) {
                    $scope.showcampaign_gid = false;
                }
                else {
                    $scope.showcampaign_gid = true;
                }
                if (resp.data.finalcampaign_gid == "" || resp.data.finalcampaign_gid == null) {
                    $scope.showfinalcampaign_gid = false;
                }
                else {
                    $scope.showfinalcampaign_gid = true;
                }



                if (resp.data.campaignapproving2employee_gid == "" || resp.data.campaignapproving2employee_gid == null) {
                    $scope.showcampaignapproving2employee_gid = false;
                }
                else {
                    $scope.showcampaignapproving2employee_gid = true;
                }
                if (resp.data.appcustomerapproving_gid == "" || resp.data.appcustomerapproving_gid == null) {
                    $scope.showappcustomerapproving_gid = false;
                }
                else {
                    $scope.showappcustomerapproving_gid = true;
                }

                // Business Developement
                if (resp.data.marketingcall_gid == "False") {
                    $scope.showmarketingcall_gid = false;
                }
                else {
                    $scope.showmarketingcall_gid = true;
                }
                //Inbound
                if (resp.data.inboundcall_gid == "False") {
                    $scope.showinboundcall_gid = false;
                }
                else {
                    $scope.showinboundcall_gid = true;
                }
                //Sa_onboarding
                //if (resp.data.sacontactinstitution_gid == "False") {
                //    $scope.showsacontactinstitution_gid = false;
                //}
                //else {
                //    $scope.showsacontactinstitution_gid = true;
                //}
                //if (resp.data.sacontact_gid == "False") {
                //    $scope.showsacontact_gid = false;
                //}
                //else {
                //    $scope.showsacontact_gid = true;
                //}
          
                if (resp.data.makersacontactinstitution_gid == "" || resp.data.makersacontactinstitution_gid == null) {
                    $scope.showmakersacontactinstitution_gid = false;
                }
                else {
                    $scope.showmakersacontactinstitution_gid = true;
                }



                if (resp.data.checkersacontactinstitution_gid == "" || resp.data.checkersacontactinstitution_gid == null) {
                    $scope.showcheckersacontactinstitution_gid = false;
                }
                else {
                    $scope.showcheckersacontactinstitution_gid = true;
                }

                if (resp.data.finalsacontactinstitution_gid == "" || resp.data.finalsacontactinstitution_gid == null) {
                    $scope.showfinalsacontactinstitution_gid = false;
                }
                else {
                    $scope.showfinalsacontactinstitution_gid = true;
                }

                if (resp.data.makersacontact_gid == "" || resp.data.makersacontact_gid == null) {
                    $scope.showmakersacontact_gid = false;
                }
                else {
                    $scope.showmakersacontact_gid = true;
                }



                if (resp.data.checkersacontact_gid == "" || resp.data.checkersacontact_gid == null) {
                    $scope.showcheckersacontact_gid = false;
                }
                else {
                    $scope.showcheckersacontact_gid = true;
                }

                if (resp.data.finalsacontact_gid == "" || resp.data.finalsacontact_gid == null) {
                    $scope.showfinalsacontact_gid = false;
                }
                else {
                    $scope.showfinalsacontact_gid = true;
                }


                //Service Request
                if (resp.data.activedepartment2manager_gid == "" || resp.data.activedepartment2manager_gid == null) {
                    $scope.showactivedepartment2manager_gid = false;
                }
                else {
                    $scope.showactivedepartment2manager_gid = true;
                }
                if (resp.data.activedepartment2member_gid == "" || resp.data.activedepartment2member_gid == null) {
                    $scope.showactivedepartment2member_gid = false;
                }
                else {
                    $scope.showactivedepartment2member_gid = true;
                }
                if (resp.data.supportteam2member_gid == "" || resp.data.supportteam2member_gid == null) {
                    $scope.showsupportteam2member_gid = false;
                }
                else {
                    $scope.showsupportteam2member_gid = true;
                }
                if (resp.data.requestapproval_gid == "" || resp.data.requestapproval_gid == null) {
                    $scope.showrequestapproval_gid = false;
                }
                else {
                    $scope.showrequestapproval_gid = true;
                }
                if (resp.data.servicerequest_gid == "" || resp.data.servicerequest_gid == null) {
                    $scope.showservicerequest_gid = false;
                }
                else {
                    $scope.showservicerequest_gid = true;
                }
                if (resp.data.email_gid == "" || resp.data.email_gid == null) {
                    $scope.showemail_gid = false;
                }
                else {
                    $scope.showemail_gid = true;
                }
                //if (resp.data.maildetails_gid == "" || resp.data.maildetails_gid == null) {
                //    $scope.showmaildetails_gid = false;
                //}
                //else {
                //    $scope.showmaildetails_gid = true;
                //}
                unlockUI();
            });
                              
        }

        $scope. deactive_cancel = function () {
            if (lstab == 'pending') {
                $location.url('app/SysMstEmployeePendingSummary');
            }
            else if (lstab == 'active') {
                $state.go('app.SysMstEmployeeActiveUserSummary');
            }
            else if (lstab == 'inactive') {
                $state.go('app.SysMstEmployeeInactiveSummary');
            }
            else if (lstab == 'relieving') {
                $state.go('app.SysMstEmployeeInactiveSummary');
            }
            else {
                $state.go('app.SysMstEmployeeSummary');   
            }
            //$state.go('app.SysMstEmployeeSummary');   
            };  

       /* Employee Deactive */
        
        $scope.deactive_submit = function () {
            if ($scope.receipt_date == "" || $scope.receipt_date == null || $scope.receipt_date == undefined)
            {
                Notify.alert('Kindly Enter Deactivation Date', 'warning')
            }
            else if ($scope.txtremarks == "" || $scope.txtremarks == null || $scope.txtremarks == undefined) {
                Notify.alert('Kindly Enter Remarks', 'warning')
            }
            else {
                var url = 'api/ManageEmployee/EmployeeDeactivate';
                var params = {
                    exit_date: $scope.receipt_date,
                    employee_gid: employee_gid,
                }

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 4000
                        });
                        activate();
                        if (lstab == 'pending') {
                            $location.url('app/SysMstEmployeePendingSummary');
                        }
                        else if (lstab == 'active') {
                            $state.go('app.SysMstEmployeeActiveUserSummary');
                        }
                        else if (lstab == 'inactive') {
                            $state.go('app.SysMstEmployeeInactiveSummary');
                        }
                        else if (lstab == 'relieving') {
                            $state.go('app.SysMstEmployeeInactiveSummary');
                        }
                        else {
                            $state.go('app.SysMstEmployeeSummary');
                        }
                    }
                    else {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 4000
                        });
                        activate();
                        if (lstab == 'pending') {
                            $location.url('app/SysMstEmployeePendingSummary');
                        }
                        else if (lstab == 'active') {
                            $state.go('app.SysMstEmployeeActiveUserSummary');
                        }
                        else if (lstab == 'inactive') {
                            $state.go('app.SysMstEmployeeInactiveSummary');
                        }
                        else if (lstab == 'relieving') {
                            $state.go('app.SysMstEmployeeInactiveSummary');
                        }
                        else {
                            $state.go('app.SysMstEmployeeSummary');
                        }
                    }
                });
            }
        }              
      
 }
    
})();
