(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnArchivalSummary', iasnTrnArchivalSummary);

        iasnTrnArchivalSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnArchivalSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnArchivalSummary';

        activate();

        function activate() {
            $scope.PnlSpecific = false;
            $scope.archival={}
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total=100;
            $scope.totalDisplayed=0;  
            $scope.page = localStorage.getItem('page');

            var url = 'api/IasnTrnWorkItem/ArchivalCounts';
            SocketService.get(url).then(function (resp) {
                $scope.workitem_count = resp.data.workitem_count;
                $scope.archivalcustomer_count=resp.data.archivalcustomer_count;
                $scope.archivalspecific_count = resp.data.archivalspecific_count;
                   
                
            });


            var url = 'api/IasnTrnWorkItem/WorkItemArchivalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItem_List = resp.data.MdlWorkItem;
                //if ($scope.WorkItem_List == null) {
                //    $scope.total = 0;
                //    $scope.totalDisplayed = 0;
                //}
                //else {
                //    $scope.total = $scope.WorkItem_List.length;
                //    if ($scope.WorkItem_List.length < 100) {
                //        $scope.totalDisplayed = $scope.WorkItem_List.length;
                //    }
                //}
            });
        }

        $scope.workitem = function () {
            var url = 'api/IasnTrnWorkItem/WorkItemArchivalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.WorkItem_List = resp.data.MdlWorkItem;
                //if ($scope.WorkItem_List == null) {
                //    $scope.total = 0;
                //    $scope.totalDisplayed = 0;
                //}
                //else {
                //    $scope.total = $scope.WorkItem_List.length;
                //    if ($scope.WorkItem_List.length < 100) {
                //        $scope.totalDisplayed = $scope.WorkItem_List.length;
                //    }
                //}
            });
        }

        $scope.archivalcustomer = function () {

            var url = 'api/IasnTrnWorkItem/CustomerArchival';
            SocketService.get(url).then(function (resp) {
                $scope.ArchivalCustomer_list = resp.data.MdlArchivalCustomer;
                //if ($scope.ArchivalCustomer_list == null) {
                //    $scope.total = 0;
                //    $scope.totalDisplayed = 0;
                //}
                //else {
                //    $scope.total = $scope.ArchivalCustomer_list.length;
                //    if ($scope.ArchivalCustomer_list.length < 100) {
                //        $scope.totalDisplayed = $scope.ArchivalCustomer_list.length;
                //    }
                //}
                
            });

        }
        $scope.onclickspecific=function(){
            $scope.PnlSpecific = true;
            $scope.archival.customer = '';
            $scope.archival.cbosanctionrefno = '';
        }
        $scope.onclickcustomer = function () {
            $scope.PnlSpecific = false;
            $scope.archival.customer = '';
            
        }
        $scope.archivalspecific = function () {
            var url = 'api/IasnTrnWorkItem/SpecificArchival';
            SocketService.get(url).then(function (resp) {
                $scope.SpecificArchival_List = resp.data.MdlArchivalCustomer;
                //if ($scope.SpecificArchival_List == null) {
                //    $scope.total = 0;
                //    $scope.totalDisplayed = 0;
                //}
                //else {
                //    $scope.total = $scope.SpecificArchival_List.length;
                //    if ($scope.SpecificArchival_List.length < 100) {
                //        $scope.totalDisplayed = $scope.SpecificArchival_List.length;
                //    }
                //}
            });
        }

        $scope.createArchival = function () {
            var email_gid;
            angular.forEach($scope.WorkItem_List, function (val) {

                if (val.checked == true) {
                    email_gid = val.email_gid;

                   
                }
            });
            if (email_gid == undefined)
                {
                Notify.alert('Select Atleast One Record!')
            }
            else {
                $scope.IsCreate = true;
                $scope.archival.types_of_archival="Customer"
            }
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
                      $scope.customer="";
                     $scope.message="No Records";
                  }
                 
                 
          });
    }
    else{
     $scope.customer_list=null;
        $scope.message="Type atleast three character";
    }
         }

       $scope.fillTextbox=function(customer_gid,customer_name){
   
       $scope.archival.customer=customer_name;
       $scope. customer_gid=customer_gid;
       $scope.customer_list=null;

       var params = {
        customer_gid: customer_gid
    }

   
    var url = 'api/loan/customer_getheads';

    SocketService.getparams(url, params).then(function (resp) {
      
        $scope.sanctiondtl = resp.data.sanctiondtl;
       
    });
        }

        $scope.close=function(){
            $scope.IsCreate = false;
        }

        $scope.ArchivalSubmit=function(){
            var WorkItem_List = [];
            var email_gid;
            var sanctionref_no='';
            var sanction_gid='';
            angular.forEach($scope.WorkItem_List, function (val) {

                if (val.checked == true) {
                    email_gid = val.email_gid;
                    
                    WorkItem_List.push(email_gid);

                }
            });
            if($scope.archival.types_of_archival=='Specific'){
                if($scope.archival.cbosanctionrefno == undefined){
                  
                    Notify.alert('Select the Sanction Ref No.','warning');
                    return;
                  }
                  else{
                    sanctionref_no=$scope.archival.cbosanctionrefno.sanctionrefno;
                    sanction_gid=$scope.archival.cbosanctionrefno.sanction_Gid;
                }
            }
            
           
            var params={
                email_gid: WorkItem_List,
                archival_type:$scope.archival.types_of_archival,
                remarks: $scope.archival.Remarks,
                customer_gid:$scope.customer_gid,
                customer_name:$scope.archival.customer,
                sanctionref_no:sanctionref_no,
                sanction_gid:sanction_gid
            }

            if (email_gid != undefined) {
                var url = 'api/IasnTrnWorkItem/PostArchival';
                lockUI()
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        unlockUI()
                        Notify.alert(resp.data.message, 'success')



                    }
                    else {
                        unlockUI();

                        Notify.alert(resp.data.message)
                    }
                  
                    activate();


                });
            }
            else {
                Notify.alert('Select Atleast One Record!')
            }
        }
        $scope.checkall = function (selected) {
            angular.forEach($scope.WorkItem_List, function (val) {  
                
                    val.checked = selected;
            });
        }

      
        //$scope.loadMore = function (pagecount) {
        //    if (pagecount == undefined) {
        //        Notify.alert("Enter the Total Summary Count", "warning");
        //        return;
        //    }
        //    lockUI();

        //    var Number = parseInt(pagecount);
        //    // new code start
        //    if ($scope.total != 0) {

        //        if (pagecount < $scope.total) {
        //            $scope.totalDisplayed += Number;
        //            if ($scope.total < $scope.totalDisplayed) {
        //                $scope.totalDisplayed = $scope.total;
        //                Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
        //            }
        //            unlockUI();
        //        }
        //        else {
        //            unlockUI();
        //            Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
        //            return;
        //        }
        //    }
        //    // new code end
        //    unlockUI();
        //};
       
        $scope.WorkItem360 = function (val) {
            localStorage.setItem('email_gid', val)
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','archival')
            }
            else{
                localStorage.setItem('page','archival')
            }
            localStorage.setItem('page' , 'Archival')
            $state.go("app.iasnTrnWorkItem360");
        }

        $scope.WorkItemSummary = function (val1, val2,val3,val4) {
            localStorage.setItem('customer_gid', val1)
            localStorage.setItem('type', val2)
            // localStorage.setItem("CustomerName",val3)
            // localStorage.setItem("SanctionRefNo",val4)
            $state.go('app.iasnTrnCustomerWrkSummary');
        }
      
    }
})();
