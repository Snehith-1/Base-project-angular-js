(function () {
    'use strict';

    angular
        .module('angle')
        .controller('registercustomerEdit', registercustomerEdit);

    registercustomerEdit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function registercustomerEdit($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'registercustomerEdit';




        activate();
        function activate() {
            $scope.Warning = false;
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.clusterlist = resp.data.employee_list;
                $scope.businesshead_list = resp.data.employee_list;
                $scope.regionalhead_list = resp.data.employee_list;
                $scope.relationshiplist = resp.data.employee_list;
                $scope.zonallist = resp.data.employee_list;
                $scope.creditlist = resp.data.employee_list;
                $scope.zonalrm_list = resp.data.employee_list;
                $scope.rm_list = resp.data.employee_list;
                $scope.riskmonitorning_list = resp.data.employee_list;
            });
            var url = 'api/customer/Getconstitution';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;

            });
            var url = 'api/vertical/vertical';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
            });

            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            $scope.customer_gid = localStorage.getItem('customer_gid');
            var url = 'api/customer/Getcustomerupdatedetails';
            var param = {
                customer_gid: $scope.customer_gid
            };

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.customerCodeedit = resp.data.customerCodeedit;
                $scope.customerNameedit = resp.data.customerNameedit;
                $scope.contactPersonedit = resp.data.contactPersonedit;
                $scope.mobileNoedit = resp.data.mobileNo_edit;
                $scope.contactnoedit = resp.data.contactno_edit;
                $scope.emailedit = resp.data.emailedit;
                $scope.txtaddress1 = resp.data.addressline1edit;
                $scope.txtaddress2 = resp.data.addressline2edit;
                $scope.regionedit = resp.data.regionedit;
                $scope.countryedit = resp.data.countryedit;
                $scope.vertical = resp.data.vertical_gid;
                $scope.vertical_code = resp.data.vertical_code;
                $scope.statename = resp.data.state;
                $scope.state_gid = resp.data.state_gid;

                $scope.postalcodeedit = resp.data.postalcode_edit;
                $scope.tomailedit = resp.data.tomailedit;
                $scope.ccmailedit = resp.data.ccmailedit;

                $scope.zonalHead = resp.data.zonalGid;
                $scope.businessHead = resp.data.businessHeadGid;
                $scope.regionalHead = resp.data.regionalHeadGid;
                $scope.clustermanager = resp.data.clustermanagerGid;
                $scope.relationshipMgmt = resp.data.relationshipMgmtGid;
                $scope.creditmanager = resp.data.creditmanagerGid;
                $scope.customerURNedit = resp.data.customer_urnedit;
                $scope.pan_number = resp.data.pan_number;
                $scope.gst_number = resp.data.gst_number;
                $scope.txtmajor_corporateedit = resp.data.major_corporateedit;
                $scope.cboconstitutionedit = resp.data.constitution_gidedit;
                $scope.ZonalRM = resp.data.zonal_riskmanagerGID;
                $scope.riskmanager = resp.data.risk_managerGID;
                $scope.RiskMonitoringName = resp.data.riskMonitoring_GID;
                
                unlockUI();
              
            });
       
        }



        $scope.customereditback = function () {
            $state.go('app.registerCustomersummary');
        }

        $scope.urnvalidation = function () {
            var params =
                {
                    urn: $scope.customerURNedit,
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
        $scope.customerUpdate = function () {
            var params =
           {
               urn: $scope.customerURNedit,
           }
            var url = 'api/MstCustomerAdd/GetURNInfo';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Already this URN is in Imported Customer', 'warning');
                }
                else {
                    var zonalHead_name = $('#zonalHead_name :selected').text();
                    var businessHead_name = $('#businessHead_name :selected').text();
                    var regionalHead_name = $('#regionalHead_name :selected').text();
                    var vertical_code = $('#vertical_code :selected').text();
                    var cluster_manager_name = $('#cluster_manager_name :selected').text();
                    var relationshipMgmt_name = $('#relationshipMgmt_name :selected').text();
                    var creditmanager_name = $('#creditmanager_name :selected').text();
                    var state_name = $('#statename :selected').text();
                    var constitutionname = $('#constitutionname :selected').text();
                    var zonlRM_name = $('#zonlRM_name :selected').text();
                    var riskmanager_name = $('#riskmanager_name :selected').text();
                    var RiskMonitoring_Name = $('#RiskMonitoring_Name :selected').text();
                    var params = {
                        customer_gid: $scope.customer_gid,
                        customerCodeedit: $scope.customerCodeedit,
                        customerNameedit: $scope.customerNameedit,
                        contactPersonedit: $scope.contactPersonedit,
                        mobileNoedit: $scope.mobileNoedit,
                        contactnoedit: $scope.contactnoedit,
                        emailedit: $scope.emailedit,
                        addressline1edit: $scope.txtaddress1,
                        regionedit: $scope.regionedit,
                        addressline2edit: $scope.txtaddress2,
                        countryedit: $scope.countryedit,
                        vertical_gid: $scope.vertical,
                        vertical_code: vertical_code,
                        state_gid: $scope.state_gid,
                        state: state_name,
                        tomailedit: $scope.tomailedit,
                        ccmailedit: $scope.ccmailedit,
                        postalcodeedit: $scope.postalcodeedit,
                        zonalGid: $scope.zonalHead,
                        businessHeadGid: $scope.businessHead,
                        regionalHeadGid: $scope.regionalHead,
                        clustermanagerGid: $scope.clustermanager,
                        creditmanagerGid: $scope.creditmanager,
                        relationshipMgmtGid: $scope.relationshipMgmt,
                        zonal_name: zonalHead_name,
                        businesshead_name: businessHead_name,
                        regionalhead_name: regionalHead_name,
                        cluster_manager_name: cluster_manager_name,
                        creditmanager_name: creditmanager_name,
                        relationshipmgmt_name: relationshipMgmt_name,
                        customer_urnedit: $scope.customerURNedit,
                        gst_number: $scope.gst_number,
                        pan_number: $scope.pan_number,
                        major_corporateedit: $scope.txtmajor_corporateedit,
                        constitution_nameedit: constitutionname,
                        constitution_gidedit: $scope.cboconstitutionedit,
                        zonal_riskmanagerGID: $scope.ZonalRM,
                        zonal_riskmanagerName: zonlRM_name,
                        risk_managerGID: $scope.riskmanager,
                        risk_managerName: riskmanager_name,
                        riskMonitoring_GID: $scope.RiskMonitoringName,
                        riskMonitoring_Name: RiskMonitoring_Name
                    }
                    var url = 'api/customer/customerUpdate';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            $state.go('app.registerCustomersummary');
                            Notify.alert('Customer Updated Successfully..!!', 'success')
                        }

                        else {
                            Notify.alert(resp.data.message)
                        }
                        activate();
                    });
                }
            });
            }

    }
})();
