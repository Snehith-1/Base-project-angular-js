(function () {
    'use strict';

    angular
        .module('angle')
        .controller('addcustomerController', addcustomerController);

    addcustomerController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function addcustomerController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'addcustomerController';
        activate();

        function activate() {         
            $scope.Warning = false;
            var url = 'api/customer/cMmail';
            SocketService.get(url).then(function (resp) {
                $scope.txtccmail = resp.data.ccmail;
            });

            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });
            var url = 'api/customer/Getconstitution';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
                console.log(resp.data.constitution_list);
            });
            $scope.txtcountry = "India";
        }

        $scope.urnvalidation=function()
        {           
            var params =
                {
                    urn: $scope.txtcustomerurn,
                }
            var url = 'api/MstCustomerAdd/GetURNInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.Warning = true;
                }
                else {
                    $scope.Warning = false;
                }
            });
        }
        $scope.customerback=function(val)
        {
            $state.go('app.customerMaster');
        }
        $scope.customerSubmit = function () {
            if ($scope.customer == undefined || $scope.customer == null || $scope.customer == "") {
                Notify.alert("Kindly check the customer", 'warning')

            }
            else {
                if ($scope.message == "You can't add this Customer. Tag the customer from master.")
                {
                    Notify.alert("You can't add this Customer. Tag the customer from master.", 'warning')
                }
                else{
                var params =
                    {
                        urn: $scope.txtcustomerurn,
                    }
                var url = 'api/MstCustomerAdd/GetURNInfo';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        Notify.alert('Already this URN is in Imported Customer', 'warning');
                    }
                    else {
                        var vertical_code = $('#vertical_code :selected').text();
                        var zonalHead_name = $('#zonalHead_name :selected').text();
                        var businessHead_name = $('#businessHead_name :selected').text();
                        var regionalHead_name = $('#regionalHead_name :selected').text();
                        var cluster_manager_name = $('#cluster_manager_name :selected').text();
                        var relationshipMgmt_name = $('#relationshipMgmt_name :selected').text();
                        var creditmanager_name = $('#creditmanager_name :selected').text();
                        var state_name = $('#statename :selected').text();
                        var zonlRM_name = $('#zonlRM_name :selected').text();
                        var riskmanager_name = $('#riskmanager_name :selected').text();
                        var RiskMonitoring_Name = $('#RiskMonitoring_Name :selected').text();

                        var params = {
                            vertical_gid: $scope.vertical,
                            vertical_code: vertical_code,
                            customercode: $scope.txtcustomercode,
                            //   customer_name: $scope.customer,
                            customername: $scope.customer,
                            contactperson: $scope.txtcontactperson,
                            customer_urn: $scope.txtcustomerurn,
                            contactnumber: $scope.txtcontactno,
                            mobileno: $scope.txtmobileno,
                            email: $scope.txtemail,
                            address1: $scope.txtaddress1,
                            //address2: $scope.txtaddress2,
                            region: $scope.txtregion,
                            address2: $scope.txtaddress2,
                            state_gid: $scope.state_gid,
                            state: state_name,
                            postalcode: $scope.txtpostalcode,
                            country: $scope.txtcountry,
                            tomail: $scope.txttomail,
                            ccmail: $scope.txtccmail,
                            zonalGid: $scope.zonalHead,
                            businessHeadGid: $scope.businessHead,
                            regionalHeadGid: $scope.regionalHead,
                            relationshipMgmtGid: $scope.relationshipMgmt,
                            clustermanagerGid: $scope.clustermanager,
                            creditmanagerGid: $scope.creditmanager,
                            zonal_name: zonalHead_name,
                            businesshead_name: businessHead_name,
                            regionalhead_name: regionalHead_name,
                            cluster_manager_name: cluster_manager_name,
                            relationshipmgmt_name: relationshipMgmt_name,
                            creditmanager_name: creditmanager_name,
                            gst_number: $scope.gst_number,
                            pan_number: $scope.pan_number,
                            constitution_name: $scope.cboconstitution.constitution_name,
                            constitution_gid: $scope.cboconstitution.constitution_gid,
                            major_corporate: $scope.txtmajor_corporate,
                            zonal_riskmanagerGID: $scope.zonalRM_GID,
                            zonal_riskmanagerName: zonlRM_name,
                            risk_managerGID: $scope.riskmanager_GID,
                            riskmanager_name: riskmanager_name,
                            riskMonitoring_GID: $scope.RiskMonitoring_GID,
                            riskMonitoring_Name: RiskMonitoring_Name
                        }
                        var url = 'api/customer/customerSubmit';
                        lockUI()
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI()
                                activate();
                                $state.go('app.customerMaster');
                                Notify.alert('Customer Created Successfully..!!', 'success')
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message)
                            }
                            // activate();
                        });

                    }

                });
                }
            }
           
        }

        $scope.complete = function (string) {
            if (string.length >= 3) {
                var url = 'api/customer/CommonCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        var message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        if (resp.data.message == null) {
                            $scope.customer_list = null;
                            $scope.message = "";
                        }
                        else {
                            $scope.customer_list = null;
                            $scope.message = resp.data.message;
                        }
                    }
                });
                $scope.message = "";
            }
            else {
                $scope.message = "";
                $scope.customer_list = null;
            }
        }
        $scope.fillTextbox = function (customer_gid, customer_name) {
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;
           
            var url = 'api/customer/CommonCustomer';
            var params = {
                customername: customer_name
            }
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    $scope.message = "";
                   
                }
                else {
                    if (resp.data.message == null) {
                        $scope.customer_list = null;
                        $scope.message = "";
                    }
                    else{
                    $scope.customer_list = null;
                    $scope.message = resp.data.message;
                    }
                }
            });
        }
    }
})();
