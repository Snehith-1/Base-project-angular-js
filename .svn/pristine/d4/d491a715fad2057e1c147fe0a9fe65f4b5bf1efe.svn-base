(function () {
    'use strict';

    angular
        .module('angle')
        .controller('createLoan', createLoan);

    createLoan.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function createLoan($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'createLoan';
       
         
        activate();
        function activate() {

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };


          
            // var url = 'api/customer/customer';
            // SocketService.get(url).then(function (resp) {
            //     $scope.customer_list = resp.data.customer_list;
            //    
            //     angular.forEach($scope.customer_list, function (value,key) {  
                  
            //         $scope.countryList.push([value.customer_gid, value.customername]);

            //         // list.push(  {customer_gid:value.customer_gid,
            //         //     customername:value.customername})
                      
                   
            // });
          
           
        // });

          
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
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
           $scope.message="Type atleast three character";
       }
              
              
                // var output=[];
                // angular.forEach($scope.countryList,function(country){
                    
                //     if(country[1].toLowerCase().indexOf(string.toLowerCase())>=0){
                //         output.push(country[1]);
                //        list_value.push(  {customer_gid:country[0],
                //         customername:country[1]})                       
                //     }
                // });
                // $scope.filterCountry=list_value;
                // console.log('filtercountry', $scope.filterCountry);
            }
            $scope.fillTextbox=function(customer_gid,customer_name){
             console.log('string',customer_name);
                $scope.customer=customer_name;
              $scope. customer_gid=customer_gid;
                $scope.customer_list=null;


                var params = {
                    customer_gid: customer_gid
                }
    
               
                var url = 'api/loan/customer_getheads';
    
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.mdlheadsofcustomer = resp.data;
                    $scope.zonalHead = resp.data.zonalGid;
                    $scope.businessHead = resp.data.businessHeadGid;
                    $scope.clustermanager = resp.data.clustermanagerGid;
                    $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                    $scope.creditmgmt_name = resp.data.creditmanager_gid;
                    $scope.vertical_code = resp.data.vertical_code;
                    $scope.vertical_code = true;
                    $scope.sanctiondtl = resp.data.sanctiondtl;
                   
                });
            }
        $scope.loanback = function (val) {
            $state.go('app.loanManagement');
        }
       
        
        $scope.sanctionrefnochange = function (sanction_gid) {
            var params = {
                sanction_gid: $scope.cbosanctionrefno.sanction_Gid
            }
            var url = 'api/loan/GetSanctionDate';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.sanctionDate = resp.data.sanctiondate;
                $scope.Sanction_Date = resp.data.Sanction_Date;
                $scope.facilitytype = resp.data.facility_type;
                $scope.facilitytype_gid = resp.data.facilitytype_gid;
            });
            var params = {
                customer2sanction_gid: $scope.cbosanctionrefno.sanction_Gid
            }
            var url = 'api/loan/SanctionLoanFacility';
            SocketService.getparams(url,params).then(function (resp) {
                if (resp.data.status == true)
                {
                   
                   
                     $scope.loan_list = resp.data.loanfacility;
                   
                }
              
            });
        }

        $scope.loanSubmit = function () {


            var vertical_code = $('#vertical_code :selected').text();
            var loan_title = $('#facility :selected').text();
            var zonal_name = $('#zonal_name :selected').text();
            var businesshead_name = $('#businesshead_name :selected').text();
            var relationshipmgmt_name = $('#relationshipmgmt_name :selected').text();
            var cluster_manager_name = $('#cluster_manager_name :selected').text();
            var creditmgmt_name = $('#creditmanager_name :selected').text();
            var sanctionrefno = $('#sanction :selected').text();

            var params = {
                loanRefNo: $scope.loanRefNo,
                loanmaster_gid: $scope.cbofacility.facility_gid,
                sanctionGid: $scope.cbosanctionrefno.sanction_Gid,
                sanctionRefno: sanctionrefno,
                sanctionDate: $scope.Sanction_Date,
                loanTitle: loan_title,
                customerGid: $scope.customer_gid,
                customer_name: $scope.customer,
                vertical_gid: $scope.vertical,
                vertical_code: vertical_code,
                zonal_name: zonal_name,
                businesshead_name: businesshead_name,
                relationshipmgmt_name: relationshipmgmt_name,
                cluster_manager_name: cluster_manager_name,
                creditmanager_name: creditmgmt_name,
                zonalGid: $scope.zonalHead,
                businessHeadGid: $scope.businessHead,
                relationshipMgmtGid: $scope.relationshipMgmt,
                clustermanagerGid: $scope.clustermanager,
                creditmanager_gid: $scope.creditmgmt_name,
            }

          
         
            var url = 'api/loan/createLoan';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert('Loan Created Successfully..!!', 'success')
                    $state.go('app.loanManagement');

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message,'warning')
                }
                activate();
            });

        }

    }
})();



