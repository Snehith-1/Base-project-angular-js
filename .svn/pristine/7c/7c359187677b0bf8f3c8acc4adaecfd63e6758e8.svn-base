(function () {
    'use strict';

    angular
        .module('angle')
        .controller('createLSAController', createLSAController);

    createLSAController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function createLSAController ($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
      
        var vm = this;
        vm.title = 'createLSAController';

        activate();

        function activate() {
           

            $scope.customer_pnl = true;
            $scope.sanction_pnl = true;
          
            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

            // Calender Popup... //

            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };
            // Calender Popup... //

            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open4 = true;
            };
            var date = new Date(),
           mnth = ("0" + (date.getMonth() + 1)).slice(-2),
           day = ("0" + date.getDate()).slice(-2);
            $scope.txtdate = [day, mnth, date.getFullYear()].join("-");
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1

            };
          
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });
            var url = 'api/IdasTrnLsaManagement/branch';
            SocketService.get(url).then(function (resp) {
                $scope.branch_list = resp.data.branch_list;
            });
        }
        $scope.complete=function(string){
                
            if(string.length >=3){
                $scope.message="";
                var url = 'api/customer/ExploreCustomer';
                var params={
                    customername:string 
                }
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                        $scope.message="";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else{
                        $scope.message="No Records";
                    }
                    
                    
                });
            }
            else{
                $scope.customer_list=null;
                $scope.message="Enter atleast three character";
            }
        }
            $scope.fillTextbox=function(customer_gid,customer_name){
                $scope.customer=customer_name;
                $scope. customer_gid=customer_gid;
                $scope.customer_list=null;


                var params = {
                    customer_gid: customer_gid
                }
    
               
                var url = 'api/IdasTrnLsaManagement/customerdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customer_pnl = false;
             
                    $scope.txtzonalHead = resp.data.zonal_head;
                    $scope.txtbusinessHead = resp.data.business_head;
                    $scope.txtclustermanager = resp.data.cluster_head;
                    $scope.txtrelationshipMgmt = resp.data.rm_name;
                    $scope.txtcreditmgmt_name = resp.data.credit_manager;    
                    $scope.txtcustomer_location = resp.data.customer_location;
                    $scope.txtcustomer_urn = resp.data.customer_urn;
                    $scope.txtaddress = resp.data.address;
                    $scope.txtvertical = resp.data.vertical;
                    $scope.txtgst_no = resp.data.gst_no;
                    $scope.txtpan_no = resp.data.pan_no;
                    $scope.txtconstitution = resp.data.constitution;
                    $scope.txtmajor_corporate = resp.data.major_corporate;
                    $scope.lbladdress1 = resp.data.address1;
                   
                });
                var url = 'api/IdasTrnLsaManagement/customer2sanction';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customer2sanction_list = resp.data.customer2sanction_list;
              
                });
            }
      

        $scope.onselectedchangesanction = function (sanction) {
          
               var params = {
                customer2sanction_gid: $scope.cbocustomer2sanction_gid.customer2sanction_gid
               }
            //-----Loan Facility Validation-------------//
               var url = 'api/IdasTrnLsaManagement/Getloanfacilityinfo';
               SocketService.getparams(url, params).then(function (resp) {
                   $scope.sanction_pnl = true;

                   $scope.loanfacility_validation = resp.data.loanfacility_validation;
                   if (resp.data.loanfacility_validation == 'N')
                   {
                       //------- Document  Validation--------------//
                       var params = {
                                   customer2sanction_gid: $scope.cbocustomer2sanction_gid.customer2sanction_gid

                               }
                               var url = 'api/IdasTrnLsaManagement/customer2sanctiondtl';
                               SocketService.getparams(url, params).then(function (resp) {
                                   $scope.sanction_pnl = false;

                                   $scope.txtsanctiondate = resp.data.sanction_date;
                                   $scope.txtpurpose_lending = resp.data.purpose_lending;
                                   $scope.txtfacility = resp.data.facility;
                                   $scope.txtproduct_solution = resp.data.product_solution;
                                   $scope.txtmajor_intervention = resp.data.majot_intervention;
                                   $scope.txtprimaryvalue_chain = resp.data.primaryvalue_chain;
                                   $scope.txtsecondaryvalue_chain = resp.data.secondaryvalue_chain;
                                   $scope.lblsanction_branch_name = resp.data.sanction_branch_name;
                                   $scope.lblsanction_state_name = resp.data.sanction_state_name;
                                   $scope.lblccapproved_by = resp.data.ccapproved_by;
                                   $scope.lblccapproved_date = resp.data.ccapproved_date;
                                   $scope.lblsanction_type = resp.data.sanction_type;
                                   $scope.lblnatureof_proposal = resp.data.natureof_proposal;
                                   $scope.customer2sanction_flag = resp.data.customer2sanction_flag;                              
                                   $scope.limitinfo_limit = resp.data.limitinfo_limit;
                                  
                               });
                       
                        
           
                   }
                   else {
                       $scope.cbocustomer2sanction_gid = '';
                       Notify.alert('Cant able to create LSA, Loan Facility Type is not added in sanction', {
                           status: 'warning',
                           pos: 'top-center',
                           timeout: 3000
                       });
                   }
               });
        }
        $scope.lsaback=function()
        {
            $state.go('app.lsaManagement');
        }
        $scope.lsasubmit = function () {
          
            var params = {
                branch_name: $scope.lblsanction_branch_name,
                state: $scope.lblsanction_state_name,
                customer2sanction_gid: $scope.cbocustomer2sanction_gid.customer2sanction_gid,
                customer_name: $scope.customer,
                customer_gid: $scope.customer_gid,
                customer_urn:$scope.txtcustomer_urn,
                sanctionref_no: $scope.cbocustomer2sanction_gid.sanctionref_no,
                sanctiondate: $scope.txtsanctiondate,
                approved_by: $scope.lblccapproved_by,
                approveddate: $scope.lblccapproved_date,
                constitution:$scope.txtconstitution,
                gst_no: $scope.txtgst_no,
                pan_no: $scope.txtpan_no,
                customer_location: $scope.txtcustomer_location,
                address: $scope.txtaddress,
                address1: $scope.lbladdress1,
                rm_name:$scope .txtrelationshipMgmt,
                cluster_head:$scope.txtclustermanager,
                business_head:$scope.txtbusinessHead,
                credit_manager:$scope.txtcreditmgmt_name,
                zonal_head:$scope.txtzonalHead,
                sa_code:$scope.txtsa_code,
                purpose_lending: $scope.txtpurpose_lending,
                facility: $scope.txtfacility,
                major_corporate: $scope.txtmajor_corporate,
                hypothecation_date: $scope.txthypothecation_date,
                mortgage_date: $scope.txtmortgage_date,
                product_solution: $scope.txtproduct_solution,
                majot_intervention: $scope.txtmajor_intervention,
                sector: $scope.txtvertical,
                primaryvalue_chain: $scope.txtprimaryvalue_chain,
                secondaryvalue_chain:$scope.txtsecondaryvalue_chain,
                remarks: $scope.txtremarks,
                vertical: $scope.txtvertical,
                sanction_type: $scope.lblsanction_type,
                natureof_proposal: $scope.lblnatureof_proposal,
                 customer2sanction_flag:$scope.customer2sanction_flag
            }
            console.log(params);
            var url = 'api/IdasTrnLsaManagement/createLSA';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    activate();
                    $state.go('app.lsaManagement');
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
              
            });
            }
           
    }
})();
