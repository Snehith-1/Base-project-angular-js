(function () {
    'use strict';

    angular
        .module('angle')
        .controller('raisesr2authenticationController', raisesr2authenticationController);

    raisesr2authenticationController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function raisesr2authenticationController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'raisesr2authenticationController';

        activate();


        function activate() {
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];
            $scope. templegalsr_gid = $location.search().lstemplegalsr_gid;
            $scope. customer_gid = $location.search().lscustomer_gid;
      
            //$scope.templegalsr_gid = localStorage.getItem('templegalsr_gid');
            //$scope.customer_gid = localStorage.getItem('customer_gid');
         

            var params = {
                templegalsr_gid: $scope.templegalsr_gid,
                customer_gid: $scope.customer_gid
               

            };
         
            var param1 = {
                customer_gid: $scope.customer_gid
            };

            var url = 'api/raiseLegalSR/getdeletetempcontRM';

            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/raiseLegalSR/gettemprmdtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.contactdetailsRM = resp.data.contactdetailsRM;
            });

            var url = 'api/raiseLegalSR/Getpromoter';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.promoters_list = resp.data.promoter_list;

            });

            var url = 'api/CustomerReport/Getsanctionloandetails';

            SocketService.getparams(url, param1).then(function (resp) {

                $scope.sanction_list = resp.data.sanctionloanListurn;

                angular.forEach($scope.sanction_list, function (value, key) {
                    var params = {
                        sanction_gid: value.sanction_gid
                    };
                    var url = 'api/CustomerReport/GetloanFacilityDetails';
                    SocketService.getparams(url, params).then(function (resp) {
                        value.loanfacilitytype = resp.data.loanfacilitytype_list;
                        value.expand = false;

                    });
                });
            });

            /*  var url = "api/customerManagement/Getsanctionloandetails";
              SocketService.getparams(url, params).then(function (resp) {
                  $scope.loandetails = resp.data.loandtl;
  
              }); */
            var url = 'api/raiseLegalSR/Getguarantor';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.guarantors_list = resp.data.guarantor_list;
                $scope.remarks = resp.data.remarks;
            });
            var url = "api/customerManagement/getCollateraldetail";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerCollateral = resp.data.customerCollateral;
            });

            var url = 'api/raiseLegalSR/Getcustomerdetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerdetails = resp.data;
            });

            var url = 'api/raiseLegalSR/getlegalSRcontactdtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.contactdetailsRM = resp.data.contactdetailsRM;
            });


            var url = 'api/raiseLegalSR/getfacilitydtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.facility_list = resp.data.facility_list;

            });

            var url = 'api/raiseLegalSR/Getcustomerdtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.address = resp.data.address;
                $scope.email = resp.data.email_id;
                $scope.deal_year = resp.data.deal_year;
                //Notify.alert(resp.data.message, {
                //    status: 'warning',
                //    pos: 'top-center',
                //    timeout: 3000
                //});

            });

            var url = 'api/raiseLegalSR/GetTempLegalSRdtls';
            SocketService.getparams(url, params).then(function (resp) {
                
                $scope.legalSR_gid = resp.data.legalSR_gid;
                $scope.raiselegalSR_gid = resp.data.raiselegalSR_gid;
                $scope.account_name = resp.data.account_name;
                $scope.rdbfinanced_by = resp.data.financed_by;
                $scope.cboconstitution = resp.data.constitution;
                $scope.deal_year = resp.data.deal_year;
                $scope.txtcycles_date = resp.data.cycles_sanctiondated;
                $scope.txtlimit_lastsanctioned = resp.data.limit_sanction;
                $scope.txtbusiness_activity = resp.data.business_activity;
                $scope.txtprimary_securities = resp.data.primary_securities;
                $scope.txtcollateral_securities = resp.data.collateral_securities;
                $scope.txtdetail_udc_pdc = resp.data.details_UDC_PDC;
                $scope.txtunitworking_status = resp.data.unit_working_status;
                $scope.txtother_banker = resp.data.txtother_banker;
                $scope.txtbanker_exposures = resp.data.other_banker_exposures;
                $scope.txtoverdue = resp.data.status_current_overdue;
                $scope.txtcibil_data = resp.data.cibil_data;
                $scope.txtchuring_data = resp.data.churing_account;
                $scope.txtcompanies_dtl = resp.data.other_group_companies;
                $scope.txtmeeting_dtl = resp.data.meeting_details;
                $scope.rdbsrestructring = resp.data.restructuring_data;
                $scope.txtother_banker = resp.data.other_banker_borrower;
                $scope.txtptp = resp.data.instances_PTP;
                $scope.txtlegalstatus = resp.data.statuslegal_action;
                $scope.txtdemanddetails = resp.data.demandnotice_details;
                $scope.created_date = resp.data.created_date;
                $scope.remarks = resp.data.remarks;
                $scope.auth_status = resp.data.auth_status;
                $scope.auth_remarks_list = resp.data.auth_remarks_list;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.auth_status = resp.data.auth_status;


            });

            var url = "api/CustomerDashboard/Getcustomerloandetails";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loandetails = resp.data.loandtl;
            });
            var url = 'api/raiseLegalSR/GetDemandNoticedtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.demandnotice_list = resp.data.demandnotice_list;
                $scope.demand_status = resp.data.demand_status;

            });
            //var url = 'api/raiseLegalSR/GetHoldremarks';

            //SocketService.getparams(url, params).then(function (resp) {
            //    $scope.holdremarks_list = resp.data.holdremarks_list;
            //    $scope.auth_remarks = resp.data.auth_remarks;

            //});

            var url = "api/raiseLegalSR/Getsanctionloandtl";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctiondetails = resp.data.customer2loandtl;


            });

            var url = 'api/customer/Getconstitution';

            SocketService.get(url).then(function (resp) {
                $scope.cboconstitution_list = [];
                for (var i = 0; i < resp.data.constitution_list.length; i++) {
                    $scope.cboconstitution_list.push(resp.data.constitution_list[i].constitution_name);
                }
            });
        }

        $scope.legalsrsave = function (templegalsr_gid) {

            var params = {

                templegalsr_gid:templegalsr_gid,
                account_name: $scope.customer_name,
                constitution: $scope.cboconstitution,
                financed_by: $scope.rdbfinanced_by,
                deal_year: $scope.deal_year,
                address: $scope.address,
                business_activity: $scope.txtbusiness_activity,
                email_id: $scope.email,
                cycles_sanctiondated: $scope.txtcycles_date,
                limit_sanction: $scope.txtlimit_lastsanctioned,
                primary_securities: $scope.txtprimary_securities,
                collateral_securities: $scope.txtcollateral_securities,
                details_UDC_PDC: $scope.txtdetail_udc_pdc,
                unit_working_status: $scope.txtunitworking_status,
                other_banker_exposures: $scope.txtbanker_exposures,
                cibil_data: $scope.txtcibil_data,
                restructuring_data: $scope.rdbsrestructring,
                churing_account: $scope.txtchuring_data,
                status_current_overdue: $scope.txtoverdue,
                other_group_companies: $scope.txtcompanies_dtl,
                meeting_details: $scope.txtmeeting_dtl,
                instances_PTP: $scope.txtptp,
                demandnotice_details: $scope.txtdemanddetails,
                statuslegal_action: $scope.txtlegalstatus,
                other_banker_borrower: $scope.txtother_banker
            }
            
            var url = 'api/raiseLegalSR/savelegalsr';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    activate();
                    $location.url('app/requestCompliancesummary?lstab=legalsr');
                    //$state.go('app.legalSRsummary');
                    Notify.alert('Legal SR saved  Successfully', 'success')
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });

        }

        function contactdtl() {
            var params = {
             
                customer_gid: $scope.customer_gid
            };

            var url = 'api/raiseLegalSR/getlegalSRtmpcontactdtl';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.contactdetailsRM = resp.data.contactdetailsRM;
            });
        }

        $scope.addcontactdetails = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/addcontactdetails.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.contactdetailsformsubmit = function () {

                    var params = {
                        contact_name: $scope.txtname,
                        contact_location: $scope.txtlocation,
                        contact_mobileno: $scope.txtmobile,
                        customer_gid: $scope.customer_gid,
                        customer_gid:customer_gid
                    }
                    console.log(params);

                    var url = 'api/raiseLegalSR/postlegalSRcontactdtl';
                    SocketService.post(url, params).then(function (resp) {


                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                            contactdtl();


                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });


                }
            }
        }

        $scope.legalSRSubmit = function (templegalsr_gid) {
          
            var params = {
               
                templegalsr_gid: templegalsr_gid,
                account_name: $scope.customer_name,
                constitution: $scope.cboconstitution,
                financed_by: $scope.rdbfinanced_by,
                deal_year: $scope.deal_year,
                address: $scope.address,
                business_activity: $scope.txtbusiness_activity,
                email_id: $scope.email,
                cycles_sanctiondated: $scope.txtcycles_date,
                limit_sanction: $scope.txtlimit_lastsanctioned,
                primary_securities: $scope.txtprimary_securities,
                collateral_securities: $scope.txtcollateral_securities,
                details_UDC_PDC: $scope.txtdetail_udc_pdc,
                unit_working_status: $scope.txtunitworking_status,
                other_banker_exposures: $scope.txtbanker_exposures,
                cibil_data: $scope.txtcibil_data,
                restructuring_data: $scope.rdbsrestructring,
                churing_account: $scope.txtchuring_data,
                status_current_overdue: $scope.txtoverdue,
                other_group_companies: $scope.txtcompanies_dtl,
                meeting_details: $scope.txtmeeting_dtl,
                instances_PTP: $scope.txtptp,
                demandnotice_details: $scope.txtdemanddetails,
                statuslegal_action: $scope.txtlegalstatus,
                other_banker_borrower: $scope.txtother_banker,
                customer_gid:$scope.customer_gid
            }
      
            var url = 'api/raiseLegalSR/submitraiselegalsr';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    activate();
                    $location.url('app/requestCompliancesummary?lstab=legalsr');
                    //$state.go('app.legalSRsummary');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }







        $scope.legalSRUpdate = function (templegalsr_gid) {

            var params = {
                legalSR_gid: $scope.legalSR_gid,
                raiselegalSR_gid: $scope.raiselegalSR_gid,
                templegalsr_gid: templegalsr_gid,
                account_name: $scope.customer_name,
                constitution: $scope.cboconstitution,
                financed_by: $scope.rdbfinanced_by,
                deal_year: $scope.deal_year,
                address: $scope.address,
                business_activity: $scope.txtbusiness_activity,
                email_id: $scope.email,
                cycles_sanctiondated: $scope.txtcycles_date,
                limit_sanction: $scope.txtlimit_lastsanctioned,
                primary_securities: $scope.txtprimary_securities,
                collateral_securities: $scope.txtcollateral_securities,
                details_UDC_PDC: $scope.txtdetail_udc_pdc,
                unit_working_status: $scope.txtunitworking_status,
                other_banker_exposures: $scope.txtbanker_exposures,
                cibil_data: $scope.txtcibil_data,
                restructuring_data: $scope.rdbsrestructring,
                churing_account: $scope.txtchuring_data,
                status_current_overdue: $scope.txtoverdue,
                other_group_companies: $scope.txtcompanies_dtl,
                meeting_details: $scope.txtmeeting_dtl,
                instances_PTP: $scope.txtptp,
                demandnotice_details: $scope.txtdemanddetails,
                statuslegal_action: $scope.txtlegalstatus,
                other_banker_borrower: $scope.txtother_banker,
                customer_gid: $scope.customer_gid
            }

            var url = 'api/raiseLegalSR/updateraiselegalsr';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    activate();
                    $location.url('app/requestCompliancesummary?lstab=legalsr');
                    //$state.go('app.legalSRsummary');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }



        $scope.legalSRback=function()
        {
            
            var url = 'api/raiseLegalSR/getdeletetempcontRM';

            SocketService.get(url).then(function (resp) {
              
            });
            if ($scope.relpath1 == 'dntracker') {
                $location.url('app/LglTrnDNTrackerAE');
            }
            else
            {
                $location.url('app/requestCompliancesummary?lstab=legalsr');
            }                    
        }

        //$scope.forward = function () {

        //    var legalsr = "Forward";
        //    var templegalsr_gid = "forward";
        //    var customer_gid = "forward";
        //    $location.url('app/requestCompliancesummary?lstab' = legalsr + '&templegalsr_gid=' + templegalsr_gid + '&customer_gid=' + customer_gid);
        //}

    }
})();
