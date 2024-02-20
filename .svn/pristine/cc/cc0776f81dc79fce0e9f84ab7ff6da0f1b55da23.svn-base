(function () {
    'use strict';

    angular
        .module('angle')
        .controller('cboCustomer2loandetailscontroller', cboCustomer2loandetailscontroller);

        cboCustomer2loandetailscontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function cboCustomer2loandetailscontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'cboCustomer2loandetailscontroller';

        activate();


        function activate() {
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.urn = localStorage.getItem('urn');
            $scope.otherloan = true;
            $scope.click = true;
            $scope.initiatedn = true;
            $scope.dn2format = true;
            $scope.button=true;
            $scope.revert = true;
            $scope.sanctiondtl = true;
            var url = "api/misDataimport/getcustomerDNGID"
            lockUI();
            var param = {
                urn: $scope.urn
            };
            console.log(param);
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.dncase_gid = resp.data.dncase_gid;
            });

            var url = "api/misDataimport/getcustomer2Loan"
            lockUI();
            var param = {
                urn: $scope.urn
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.mdlMisdataimport = resp.data.mdlMisdataimport;
                $scope.otherloan_list = resp.data.otherloan_list;
                $scope.customer_name = resp.data.customer_name;
                $scope.cbo_status = resp.data.cbo_status;
                if ((resp.data.cbo_status == 'DN Sent')) {
                    $scope.dn2format = false;
                    $scope.data = true;
                    $scope.courierdetails = true;
                    $scope.initiatedn = true;
                }

                if ((resp.data.cbo_status == 'DN Skip')) {
                    $scope.skip = true;
                }

            });
            var url = "api/misDataimport/DN1ContentDTL"
            lockUI();
            var param = {
                urn: $scope.urn
            };
            
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.templatecontent = resp.data.cbotemplate_content;
                document.getElementById('test').innerHTML += $scope.templatecontent;
                $scope.DN1_status=resp.data.DN1_status;
                console.log(resp.data.DN1_status);
                  
                if ((resp.data.DN1_status == 'DN Generated')) {
                    $scope.dn2format = false;
                    $scope.button = false;
                    $scope.courier_info = false;
                    $scope.info = true;
                }
                if ((resp.data.DN1_status == 'DN Hold')) {
                    $scope.dn2format = false;
                    $scope.button = false;
                    $scope.courier_info = false;
                    $scope.info = true;
                }
                if (resp.data.DN1_status == 'DN Reverted') {
                    $scope.initiatedn1 = true;
                    $scope.otherloan = true;
                    $scope.click = true;
                    $scope.dn1format = true;
                    $scope.button = true;
                    $scope.courier_info = false;
                    $scope.revert=false;
                }
            });

            var url = "api/misDataimport/getDN1Status"
            lockUI();
            var param = {
                urn: $scope.urn
            };
        
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.CBOhistory_list = resp.data.CBOhistory_list;
           
                if ((resp.data.cbo_status == 'DN Skip') || (resp.data.cbo_status == 'DN Sent') || (resp.data.cbo_status == 'DN Generated')) {
                    $scope.dndetails = false;
                }
              
            });
            var url = "api/misDataimport/getcourierinfo"
            lockUI();
            var param = {
                urn: $scope.urn
            };
          
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.CBOcouriered_by = resp.data.CBOcouriered_by;
                $scope.CBOcourier_center = resp.data.CBOcourier_center;
                $scope.CBOcourier_date = resp.data.CBOcourier_date;
                $scope.CBOcourier_refno = resp.data.CBOcourier_refno;
                $scope.CBOremarks = resp.data.CBOremarks;
                $scope.CBOcourier_status = resp.data.CBOcourier_status;
                 });
                 var url = "api/misDataimport/Getrevertdetails"
            lockUI();
            var param = {
                urn: $scope.urn
            };
          
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.updated_date = resp.data.updated_date;
                $scope.updated_by = resp.data.updated_by;
                $scope.dn_status = resp.data.dn_status;
                $scope.remarks = resp.data.remarks;
               
                 });
        }
        $scope.cbogenerate = function () {
            $scope.courier_info = true;
            $scope.info = true;

            var url = "api/misDataimport/cbogenerate"
            var param = {
                urn: $scope.urn,
                template_content: $scope.content

            };
         
            SocketService.post(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    
                    Notify.alert('DN Generated  Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Generating DN')
                }
                activate();
                $state.go('app.dnTrackerCBO');
            });

        }

        $scope.DNsend = function () {
            var url = "api/misDataimport/CBOStatus"
            var param = {
                urn: $scope.urn,
                dncase_gid: $scope.dncase_gid,
                courier_refno: $scope.txtcourierefno,
                courier_center: $scope.txtcouriercenter,
                courier_date: $scope.txtcourierdate,
                couriered_by: $scope.txtcourierby,
                courier_remarks: $scope.txtcourieredremarks
            };
            SocketService.post(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    Notify.alert('DN2 Status Updated Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Updating DN2 Status ')
                }
                activate();
                $state.go('app.dnTrackerCBO');
            });
            $scope.courier_info = false;
         
            $scope.info = true;
        }
        $scope.DNskip = function () {
            var url = "api/misDataimport/CBOskip"
            var param = {
                urn: $scope.urn,
                dncase_gid:$scope.dncase_gid
            };
            SocketService.post(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    Notify.alert('DN2 Status Updated Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Updating DN2 Status ')
                }
                activate();
            });
            $scope.skip = true;
        }
        $scope.Dnback = function () {
            $state.go('app.dnTrackerCBO')
        }
        $scope.cancel = function () {
            $scope.courier_info = false;
            $scope.initiatedn= true;
            $scope.sanctiondtl = true;
            $scope.info = false;
        }
        $scope.clickinitiatedn = function () {
            $scope.initiatedn = true;
            $scope.courier_info = false;
            $scope.sanctiondtl = false;
            var url = 'api/misDataimport/Getsanctionloandetails';
            var param = {
                urn: $scope.urn

            };

            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtdnCBOsanctionref_no = resp.data.sanction_refno;
                $scope.txtdnCBOsanction_date = resp.data.sanction_date;
                $scope.txtdnCBOsanction_amount = resp.data.sanction_amount;
                $scope.txtdnCBOref_no = "SAMFIN/RMD/";

            });

            var url = 'api/misDataimport/GetSanctiondtl';
            var param = {
                urn: $scope.urn

            };

            SocketService.getparams(url, param).then(function (resp) {

                $scope.dnCBOref_no = resp.data.dnCBOref_no;
                $scope.dnCBOsanctionref_no = resp.data.dnCBOsanctionref_no;
                $scope.dnCBOsanction_date = resp.data.dnCBO_date;
               
                var amount_dn = new Intl.NumberFormat('en-IN').format(resp.data.dnCBOsanction_amount);
                $scope.dnCBOsanction_amount = amount_dn;
                $scope.dn_flag = resp.data.dn_flag;
                if (resp.data.dn_flag == "N") {
                    $scope.initiatedn = true;
                    $scope.sanctiondtl = false;
                }
                if (resp.data.dnCBO_flag == "Y") {
                    $scope.initiatedn = false;
                    $scope.sanctiondtl = true;
                }
                if (resp.data.dnCBO_flag == "N") {
                    $scope.initiatedn = true;
                    $scope.sanctiondtl = false;
                }
            });

            var url = 'api/misDataimport/DNCBOContent';
            var param = {
                urn: $scope.urn

            };
            console.log(param);
            SocketService.getparams(url, param).then(function (resp) {
                $scope.content = resp.data.template_content;

            });
            $scope.info = true;
        }
        $scope.otherloandetails = function () {
            $scope.otherloan = false;
            $scope.click = false;
        }
        $scope.minimizeloan = function () {
            $scope.otherloan = true;
            $scope.click = true;
        }
        $scope.revert = function (urn) {

            var modalInstance = $modal.open({
                templateUrl: '/revertdn.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
           
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
                $scope.revert_yes = function () {

                    var params = {
                       
                        urn:urn,
                        remarks:$scope.txtremarks
                    }
                    console.log(params);
                    var url = 'api/misDataimport/RevertDN_CBO';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                        $state.go('app.dnTrackerCBO');
                        activate();
                    });
                }
            }
        }
        $scope.hold = function (urn) {

            var modalInstance = $modal.open({
                templateUrl: '/holddn.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
           
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
                $scope.hold_yes = function () {

                    var params = {
                       
                        urn:urn,
                        remarks:$scope.txtremarks
                    }
                    console.log(params);
                    var url = 'api/misDataimport/HoldDN_CBO';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                        $state.go('app.dnTrackerCBO');
                        activate();
                    });
                }
            }
        }
        $scope.unhold = function (urn) {

            var modalInstance = $modal.open({
                templateUrl: '/unholddn.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
           
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
               
                $scope.unhold_yes = function () {

                    var params = {
                       
                        urn:urn,
                       }
                    console.log(params);
                    var url = 'api/misDataimport/UnholdDN_CBO';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                        $state.go('app.dnTrackerCBO');
                        activate();
                    });
                }
            }
        }

        $scope.dnCBOsanctionsubmit = function () {
            var url = 'api/misDataimport/DNCBOsanctiondtl';

            var param = {
                urn: $scope.urn,
                dnCBOsanctionref_no: $scope.txtdnCBOsanctionref_no,
                dnCBOsanction_date: $scope.txtdnCBOsanction_date,
                dnCBOsanction_amount: $scope.txtdnCBOsanction_amount,
                dnCBOref_no: $scope.txtdnCBOref_no
            };
            console.log(param);
            SocketService.post(url, param).then(function (resp) {
                if (resp.data.status == true) {

                    var url = 'api/misDataimport/GetSanctiondtl';
                    var param = {
                        urn: $scope.urn

                    };

                    SocketService.getparams(url, param).then(function (resp) {

                        $scope.dnCBOref_no = resp.data.dnCBOref_no;
                        $scope.dnCBOsanctionref_no = resp.data.dnCBOsanctionref_no;
                        $scope.dnCBOsanction_date = resp.data.dnCBOsanction_date;
                        //   $scope.dnCBOsanction_amount = resp.data.dnCBOsanction_amount;

                        var amount_dn = new Intl.NumberFormat('en-IN').format(resp.data.dnCBOsanction_amount);
                        $scope.dnCBOsanction_amount = amount_dn;
                        $scope.dn_flag = resp.data.dn_flag;
                        if (resp.data.dn_flag == "N") {
                            $scope.initiatedn = false;
                            $scope.sanctiondtl = true;
                        }
                        
                        if (resp.data.dnCBO_flag == "Y") {
                            $scope.initiatedn= false;
                            $scope.sanctiondtl = true;
                        }
                    });
                    var url = 'api/misDataimport/DNCBOContent';
                    var param = {
                        urn: $scope.urn

                    };
                    console.log(param);
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.content = resp.data.template_content;

                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.dnCBOcancel = function () {
            $scope.courier_info = false;
            $scope.initiatedn = true;
            $scope.sanctiondtl = true;
            $scope.info = false;
            $scope.txtdnCBOsanctionref_no='';
            $scope.txtdnCBOsanction_date = '';
            $scope.txtdnCBOsanction_amount = '';
            $scope.txtdnCBOref_no = '';
        }
        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var arr = input.split(',');
            var i;
            for (i = 0; i < arr.length; i++) {

                var str = input.replace(',', '');
                input = str;
            }
            var output = Number(str).toLocaleString('en-IN');
            $scope.txtdnCBOsanction_amount = output;

        }
    }
})();