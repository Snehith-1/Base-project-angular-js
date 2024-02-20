(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnTrnMyWorkItemSummary', iasnTrnMyWorkItemSummary);

        iasnTrnMyWorkItemSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnTrnMyWorkItemSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnTrnMyWorkItemSummary';

        activate();
        lockUI();
        function activate() {
            $scope.assignto_team = false;
            $scope.assignto_employee = false;
            $scope.total=100;
            $scope.totalDisplayed=0;
            if ($scope.page == undefined) {
                localStorage.setItem('page', 'iasnTrnMyWorkItemSummary')
            }
           
            $scope.page = localStorage.getItem('page');    
            var url = 'api/IasnTrnMyWorkItem/MyWorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_myworkitempending = resp.data.count_myworkitempending;
                $scope.count_myworkitempushback = resp.data.count_myworkitempushback;
                $scope.count_myworkitemforward = resp.data.count_myworkitemforward;
                $scope.count_myworkitemclose = resp.data.count_myworkitemclose;

            });

            var url = 'api/IasnTrnMyWorkItem/Pending';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.Pending_list = resp.data.MdlWorkItem;
          
                if ($scope.Pending_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.Pending_list.length;
                    if ($scope.Pending_list.length < 100) {
                        $scope.totalDisplayed = $scope.Pending_list.length;
                    }
                }

            });

           
        }
        $scope.EmployeeProfile=function(emp_gid){
            var url = 'api/IasnTrnWorkItem/EmployeeProfile';
            var params={
                employee_gid:emp_gid
            }
            SocketService.getparams(url,params).then(function (resp) {
               if(resp.data.status==true){
                $scope.user_code=resp.data.user_code;
                $scope.user_name=resp.data.user_name;
                $scope.user_photo=resp.data.user_photo;
                $scope.user_designation=resp.data.user_designation;
                $scope.user_department=resp.data.user_department;
                $scope.user_mobileno=resp.data.user_mobileno;
               }
               else{
                $scope.user_code="-";
                $scope.user_name="-";
                $scope.user_photo="N";
                $scope.user_designation="-";
                $scope.user_department="-";
               }
            });

        }
        
             
      /*   $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {

                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if ($scope.total < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            unlockUI();
        }; */
        
        // Action Work Item 360
        $scope.WorkItem360=function(val){
            localStorage.setItem('email_gid',val);
            var params={
                email_gid:val
            }
            var url = 'api/IasnTrnMyWorkItem/MailSeen';
            SocketService.getparams(url,params).then(function (resp) {
            });
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','MyWorkItemSummary')
            }
            localStorage.setItem('page', 'Workitem')
            $state.go("app.iasnTrnMyWorkItem360");
        }

        $scope.mergeworkitem = function (email_gid,subject,ref_no) {
            localStorage.setItem('email_gid',email_gid);
            localStorage.setItem('email_subject',subject);
            localStorage.setItem('workitemref_no',ref_no);         
            if ($scope.page ==undefined)
            {
                localStorage.setItem('page','MyWorkItemSummarypage')
            }
             else 
             {
                localStorage.setItem('page','MyWorkItemSummarypage')
             }
            $state.go('app.iasnWomMergeWorkItem');
        }

        $scope.TransferWorkItem = function (email_gid, workitemref_no, email_from, email_subject, zone_name, checkeremployee_name) {
           
            var modalInstance = $modal.open({
                templateUrl: '/transferWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
           
           
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no=workitemref_no;
                $scope.subject=email_subject;
                $scope.from=email_from;
                $scope.zone_name=zone_name;
                $scope.checkeremployee_name = checkeremployee_name;
             
                var url = 'api/IasnTrnWorkItem/IsnEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.MdlIsnEmployee;
                   
                });
                var params={
                    lsemail_gid:email_gid
                }
                var url = 'api/IasnTrnWorkItem/TransferLog';
                SocketService.getparams(url,params).then(function (resp) {
                    if(resp.data.status==true){
                        $scope.transferlog_list = resp.data.MdlTransferLog;
                        $scope.showtransfer=true;
                    }
                    else{
                        $scope.showtransfer=false;
                    }
                   
                   
                });
                $scope.close = function () {
                    modalInstance.close('closed');
                };

                $scope.transferWIUpdate=function(){
                    
                    if($scope.transfer_to==undefined){
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person','warning');
                        return;
                    }

                    var params={
                        email_gid:email_gid,
                        employee_gid:$scope.transfer_to,
                        employee_name:$('#transfer_to :selected').text(),
                        zone_gid:'',
                        zone_name:'',
                        zone_flag:'Y'
                    }
                
                    var url="api/IasnTrnWorkItem/AssignTo";
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status=true){
                            Notify.alert(resp.data.message,'success');
                        }
                        else{
                            Notify.alert(resp.data.message,'warning');
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                    
                }
            }
        }

        $scope.CloseWorkItem = function (email_gid,workitemref_no,email_from,email_subject,zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/closeWIContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                $scope.workitemref_no=workitemref_no;
                $scope.subject=email_subject;
                $scope.from=email_from;
              
                $scope.CloseWIUpdate=function(){
                    
                    var params={
                        email_gid:email_gid,
                        decision:'Close',
                        employee_gid:'',
                        employee_name:'',
                        remarks:$scope.close_remarks,
                        close_acknowledge:$scope.Acknowledge_mail_trigger,
                        mailcontent:'Close',
                        customer_gid:'',
                        customer_name:'',
                        subject:'',
                        tomail_id:'',
                        ccmail_id:'',
                        bccmail_id:''
                    }
                   

                    var url='api/IasnTrnWorkItem/PostDecision';
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
                        unlockUI();
                        if(resp.data.status==true){
                            Notify.alert(resp.data.message,'success')      
                                                   
                        }
                        else{
                            Notify.alert(resp.data.message,'warning')
                        }
                        modalInstance.close('closed');
                        activate();
                    });
                }

                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
       
        $scope.Pushback = function () {
            $state.go('app.iasnTrnMyWorkItemPushback');
        }
        $scope.Forward = function () {
            $state.go('app.iasnTrnMyWorkItemForward');
        }
        $scope.Close = function () {
            $state.go('app.iasnTrnMyWorkItemClose');
        }
        $scope.WorkItem = function () {
            $state.go('app.iasnTrnMyWorkItemSummary');
        }
      
    }
})();
