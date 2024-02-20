(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sendMailalert', sendMailalert);

    sendMailalert.$inject = ['$rootScope','$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function sendMailalert($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sendMailalert';

        activate();

        function activate() {
            $scope.customeralert_gid = localStorage.getItem('customeralert_gid');
            var url = 'api/customerAlertGenerate/Getcustomerdetails';
            var params = {
                customeralert_gid: $scope.customeralert_gid
            };
           
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customermail_list = resp.data;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.customeralert_gid = resp.data.customeralert_gid;
                $scope.customer_code = resp.data.customercode;
                $scope.customer_name = resp.data.customername;
                $scope.content = resp.data.content;

                document.getElementById('test').innerHTML += $scope.content;
              
                $scope.mailalert_list = resp.data.mailalert_list;

            });
            
           

        }

       

        $scope.onselectedchangeTemplate = function (template) {
            var params = {
                template_gid: template
            }
            var url = 'api/template/Content';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.content = resp.data.template_content;

            });

        }

        $scope.sendmail = function (customeralert_gid) {
          
            var params = {
                customeralert_gid: $scope.customeralert_gid,
                customer_gid: $scope.customer_gid,
                content: $scope.content,
                customercode: $scope.customer_code,
                customername: $scope.customer_name
            }
           
            var url = 'api/customerAlertGenerate/sendMail';
            lockUI();
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Mail Sent to the Customer Successfully!', 'success');
                    $state.go('app.mailManagement');
                }
                else {
                    unlockUI();
                    Notify.alert('Oops!Problem While Sent Mail.Kindly Check Mail ID')
                }

            });

        }


        $scope.sendback = function () {
            $state.go('app.mailManagement');
        }

      
    }
})();
