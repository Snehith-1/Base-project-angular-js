//(function () {
//    'use strict';

//    angular
//        .module('angle')
//        .controller('CCMeetingApprovalController', CCMeetingApprovalController);

//    CCMeetingApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$timeout', '$sce'];

//    function CCMeetingApprovalController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $timeout, $sce) {
//        /* jshint validthis:true */
//        var vm = this;
//        vm.title = 'CCMeetingApprovalController';
//        var url = window.location.href;
//        var relPath = url.split("?id=");
//        var relpath1 = relPath[1];
       
//        activate();
//        function activate() {
//            $scope.showapproval = true;
//            $scope.hideapproval = true;           
         
//            var params = {
//                approval_token: relpath1
//            };
          
//            var url = 'api/MstCCMailApproval/GetApprovalMailList';
//            lockUI();
//            SocketService.getparams(url, params).then(function (resp) {
//                $scope.application_no = resp.data.application_no;
//                $scope.ccadmin_name = resp.data.ccadmin_name;
//                $scope.customer_name = resp.data.customer_name;
//                $scope.loanfacility_amount = resp.data.loanfacility_amount;
//                $scope.ccmeeting_date = resp.data.ccmeeting_date;
//                $scope.rm_name = resp.data.rm_name;
//                $scope.application_gid = resp.data.application_gid;
//                $scope.trustAsHtml = function (string) {
//                    return $sce.trustAsHtml(string);
//                };
//                $scope.mom_description = resp.data.mom_description;
              
//                 unlockUI();
//                if (resp.data.status == true) {
                   
//                    $scope.hideapproval = true;
//                    $scope.showapproval = true;
//                }
//                else {

//                    Notify.alert(resp.data.message, {
//                        status: 'warning',
//                        pos: 'top-center',
//                        timeout: 4000
//                    });
//                    $scope.showapproval = false;
//                    $scope.hideapproval = false;
//                }
//            }); 
//        }
        
//        $scope.approve_submit = function () {
            
          
//            var params = {
//                approval_token: relpath1,
//                application_gid: $scope.application_gid,
//                approval_status: 'Approved',
//                approval_remarks: $scope.txtremarks
//            }
//            lockUI();
//            var url = "api/MstCCMailApproval/PostCCMailApproved";
//            SocketService.post(url, params).then(function (resp) {
//                unlockUI();
//                if (resp.data.status == true) {
                    
//                    Notify.alert(resp.data.message, {
//                        status: 'success',
//                        pos: 'top-center',
//                        timeout: 3000
//                    });
                  
//                    $scope.showapproval = false;
//                    $scope.hideapproval = false;
//                }
//                else {
                    
//                    Notify.alert(resp.data.message, {
//                        status: 'danger',
//                        pos: 'top-center',
//                        timeout: 3000
//                    });
                 

//                }
//            });
//        }

//        $scope.reject_submit = function () {
            

//            var params = {
       
//                approval_token: relpath1,
//                application_gid: $scope.application_gid,
//                approval_status: 'Rejected',
//                approval_remarks: $scope.txtremarks
//            }
//            lockUI();
//            var url = "api/MstCCMailApproval/PostCCMailApproved";
//            SocketService.post(url, params).then(function (resp) {
//                unlockUI();
//                if (resp.data.status == true) {
                     
//                    Notify.alert(resp.data.message, {
//                        status: 'success',
//                        pos: 'top-center',
//                        timeout: 3000
//                    });
               
//                    $scope.showapproval = false;
//                    $scope.hideapproval = false;
//                }
//                else {
                     
//                    Notify.alert(resp.data.message, {
//                        status: 'danger',
//                        pos: 'top-center',
//                        timeout: 3000
//                    });
               
                  
//                }
//            });
//        }

//    }
//})();