(function () {
    'use strict';

    angular
        .module('angle')
        .controller('loanmanagement', loanmanagement);

    loanmanagement.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function loanmanagement($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title ='loanmanagement';

        activate();

        function activate() {

           $scope.totalDisplayed=100;
            var url = 'api/loan/loanSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.loan_data = resp.data.loanDetails;
                // new code start   
                if ($scope.loan_data == null) {
                                        $scope.total = 0;
                                        $scope.totalDisplayed = 0;
                                    }
                                    else {
                                        $scope.total = $scope.loan_data.length;
                                        if ($scope.loan_data.length < 100) {
                                            $scope.totalDisplayed = $scope.loan_data.length;
                                        }
                                    }
                    // new code end
               
                // if($scope.loan_data==null){
                //     $scope.total=0;
                // }
                // else{
                //     $scope.total=$scope.loan_data.length;
                // }

            });

            // document.getElementById('pagecount').onkeyup = function () {
           
            //     if($scope.pagecount==null){
            //      var el = document.getElementById('loadmore');
            //      el.style.backgroundColor = '#DCDCDC';  
            //     }
            //     else{
            //      var el = document.getElementById('loadmore');
            //      el.style.backgroundColor = '#ffa';
            //     }
            // };

            
  $scope.loadMore= function (pagecount) {
    if(pagecount==undefined){
        Notify.alert("Enter the Total Summary Count","warning");
        return;
    }
    lockUI();

    var Number = parseInt(pagecount);
    // new code start
        if ($scope.loan_data != null) {
       
                if (pagecount < $scope.loan_data.length) {
                    $scope.totalDisplayed += Number;
                    if($scope.loan_data.length<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.loan_data.length;
                        Notify.alert(" Total Summary " + $scope.loan_data.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.loan_data.length + " Records Only", "warning");
                    return;
                }
            }
            // new code end
        
    // $scope.totalDisplayed += Number;
    // console.log(pagecount);
    unlockUI();
};

        var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
          }
        $scope.popuploan = function () {
            $state.go('app.createLoan');
        }
   
       $scope.edit = function (val) {
            $scope.loan_gid = val;
            $scope.loan_gid = localStorage.setItem('loan_gid', val);
            $state.go('app.editLoan');
       }

       $scope.PopupNewLoanRef = function (loan_gid,loanref_no, customername) {
           var modalInstance = $modal.open({
               templateUrl: '/updateLoanref.html',
               controller: ModalInstanceCtrl,
               size: 'md'
           });
           ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
           function ModalInstanceCtrl($scope, $modalInstance) {

               $scope.customername = customername;
               $scope.loanref_no = loanref_no;
              
               $scope.ok = function () {
                   $modalInstance.close('closed');
               };

               $scope.LoanRefUpdate = function () {

                   var params = {
                       loan_gid: loan_gid,
                       newloanref_no: $scope.txtnewloanrefno,
                       oldloanref_no: loanref_no
                   }

                   lockUI();
                   var url = "api/loan/LoanRefUpdate";
                   SocketService.post(url, params).then(function (resp) {
                       if (resp.data.status == true) {

                          
                           unlockUI();
                           $modalInstance.close('closed');
                           activate();
                           Notify.alert(resp.data.message, {
                               status: 'success',
                               pos: 'top-center',
                               timeout: 3000
                           });

                       }
                       else {
                           $modalInstance.close('closed');
                           Notify.alert(resp.data.message, {
                               status: 'danger',
                               pos: 'top-center',
                               timeout: 3000
                           });
                           unlockUI();
                       }
                   });
               }
           }
       }

    $scope.createDeferral = function (val) {
                $scope.loan_gid = val;
                $scope.loan_gid= localStorage.setItem('loan_gid', val);
                $state.go('app.loan2deferral');
            }
            // View Issue 

           }
})();
