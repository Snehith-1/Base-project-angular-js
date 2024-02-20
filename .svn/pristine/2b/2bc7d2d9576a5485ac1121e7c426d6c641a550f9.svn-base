(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerAlertGeneratecontroller', customerAlertGeneratecontroller);

    customerAlertGeneratecontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function customerAlertGeneratecontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerAlertGeneratecontroller';

        activate();

        function activate() {
            $scope.customer_gid = localStorage.getItem('customer_gid');
            var url = 'api/customer/Getcustomerdetails';
            var params = {
                customer_gid: $scope.customer_gid
            };
            SocketService.getparams(url, params).then(function (resp)
            {
                $scope.customeredit = resp.data;
               
            });

            var url = 'api/customer/deferraldetails';
            SocketService.getparams(url, params).then(function (resp) {
                
                $scope.customer_data = resp.data.customerdeferral_list;
                console.log(resp.data.customerdeferral_list);
            });

            var url = 'api/template/Content';

            SocketService.get(url).then(function (resp) {
                $scope.content = resp.data.template_content;

            });
           
        }

        $scope.checkall = function (selected) {
            angular.forEach($scope.customer_data, function (val) {
                val.checked = selected;
            });
        }

        //$scope.onselectedchangeTemplate = function (template) {
        //    var params = {
        //        template_gid: template
        //    }
        //    var url = 'api/template/Content';

        //    SocketService.getparams(url, params).then(function (resp) {
        //        $scope.content = resp.data.template_content;

        //    });

        //}
        $scope.Contentback = function () {
            $state.go('app.customerAlert');
        }

        $scope.ContentSave = function () {
            var def_gid;
            var deferralGidList = [];
            angular.forEach($scope.customer_data, function (val) {

                if (val.checked == true) {
                    var deferral_gid = val.deferral_gid;
                    def_gid = val.deferral_gid;
                    deferralGidList.push(deferral_gid);
                }
               
            });
                var params = {
                    deferral_gid: deferralGidList,
                    customer_gid: localStorage.getItem('customer_gid'),
                    template_content: $scope.content
                }
                
                if (def_gid != undefined)
                {
                    var url = 'api/customerAlertGenerate/Generate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert('Template Content Saved Successfully!', 'success');
                            $state.go('app.customerAlert');
                        }
                        else {
                            unlockUI();
                            Notify.alert('Oops something went wrong!')                
                        }

                    });
                }
                else {
                    Notify.alert('Select Atleast One Deferral!')
                }       
            }
            }
         })();
