(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dn3Customer2loandetailscontroller', dn3Customer2loandetailscontroller);

    dn3Customer2loandetailscontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function dn3Customer2loandetailscontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dn3Customer2loandetailscontroller';

        activate();


        function activate() {
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];

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
            $scope.initiatedn3 = true;
            $scope.dn3format = true;
            $scope.button = true;
            $scope.revert = true;
            $scope.sanctiondtl = true;
            var url = "api/misDataimport/DN1ContentDTL"
            lockUI();
            var param = {
                urn: $scope.urn
            };

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.content = resp.data.dn3_content;
                document.getElementById('test').innerHTML += $scope.content;
                $scope.DN1_status=resp.data.DN1_status;
               
                if ((resp.data.DN1_status == 'DN3 Generated')) {
                    console.log(resp.data.DN1_status);
                    $scope.dn3format = false;
                    $scope.button = false;
                    $scope.courier_info = false;
                    $scope.info = true;
                }
                if (resp.data.DN1_status == 'DN3 Reverted') {
                    $scope.initiatedn3 = true;
                    $scope.otherloan = true;
                    $scope.click = true;
                    $scope.dn3format = true;
                    $scope.button = true;
                    $scope.courier_info = false;
                    $scope.revert=false;
                }
                if ((resp.data.DN1_status == 'DN3 Hold')) {
                    $scope.dn3format = false;
                    $scope.button = false;
                    $scope.courier_info = false;
                    $scope.info = true;
                }
            });

            var url = "api/misDataimport/getcustomerDNGID"
            lockUI();
            var param = {
                urn: $scope.urn
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.dncase_gid = resp.data.dncase_gid;
                console.log(resp.data.dncase_gid);
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
                $scope.DN3status = resp.data.DN3status;
                console.log(resp.data.customer_name);
                if ((resp.data.DN3status == 'DN3 Sent')) {
                    $scope.dn3format = false;
                    $scope.data = true;
                    $scope.courierdetails = true;
                    $scope.initiatedn3 = true;
                }
                if ((resp.data.DN3status == 'DN3 Skip')) {
                    $scope.skip = true;
                }
            });
            var url = "api/misDataimport/getDN1Status"
            lockUI();
            var param = {
                urn: $scope.urn
            };

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.dnhistory_list = resp.data.dnhistory_list;
                $scope.dn2history_list = resp.data.dn2history_list;
                $scope.dn3history_list = resp.data.dn3history_list;
                if ((resp.data.DN1status == 'DN1 Skip') || (resp.data.DN1status == 'DN1 Sent') || (resp.data.DN1status == 'DN1 Generated')) {
                    $scope.dndetails = false;
                }
                if ((resp.data.DN2status == 'DN2 Skip') || (resp.data.DN2status == 'DN2 Sent') || (resp.data.DN2status == 'DN2 Generated')) {
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
                $scope.dn3couriered_by = resp.data.dn3couriered_by;
                $scope.dn3courier_center = resp.data.dn3courier_center;
                $scope.dn3courier_date = resp.data.dn3courier_date;
                $scope.dn3courier_refno = resp.data.dn3courier_refno;
                $scope.dn3remarks = resp.data.dn3remarks;
                $scope.dn3courier_status = resp.data.dn3courier_status;
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
                console.log(resp.data.remarks);
                 });
        }
        $scope.DN3generate = function () {
            $scope.courier_info = true;
            $scope.info = true;

            var url = "api/misDataimport/DN3generate";
            lockUI();
            var param = {
                urn: $scope.urn,
                template_content: $scope.content

            };
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.dnTracker');

                    Notify.alert('DN3 Generated  Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Generating DN3 Status ')
                }
                activate();
                $location.url('app/dnTracker?lstab=dn3tracker');
              
            });

        }

        $scope.DN3send = function () {
            var url = "api/misDataimport/DN3Status"
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
                    Notify.alert('DN3 Status Updated Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Updating DN3 Status ')
                }
                activate();
                $location.url('app/dnTracker?lstab=dn3tracker');
            });
            $scope.courier_info = false;
            $scope.skip = false;
            $scope.info = true;
            $scope.courierdetails = true;
        }
        $scope.DN3skip = function () {
            var url = "api/misDataimport/DN3skip"
            var param = {
                urn: $scope.urn,
                dncase_gid: $scope.dncase_gid
            };
            SocketService.post(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    activate();
                    Notify.alert('DN3 Status Updated Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Updating DN3 Status ')
                }
                activate();
            });
            $scope.skip = false;
        }

        $scope.Dn3back = function () {
            $location.url('app/dnTracker?lstab=dn3tracker');
        }

        $scope.cancel = function () {
            $scope.courier_info = false;
            $scope.initiatedn3 = true;
            $scope.sanctiondtl=true
            $scope.info = false;
          
        }

        $scope.clickinitiatedn3 = function () {
            $scope.courier_info = true;
            $scope.initiatedn3=true;
            $scope.info = true;
            $scope.sanctiondtl = false;
            var url = 'api/misDataimport/Getsanctionloandetails';
            var param = {
                urn: $scope.urn

            };

            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtdn3sanctionref_no = resp.data.sanction_refno;
                $scope.txtdn3sanction_date = resp.data.sanction_date;
                $scope.txtdn3sanction_amount = resp.data.sanction_amount;
                $scope.txtdn3ref_no = "SAMFIN/RMD/";

            });

            var url = 'api/misDataimport/GetSanctiondtl';
            var param = {
                urn: $scope.urn

            };

            SocketService.getparams(url, param).then(function (resp) {

                $scope.dn3ref_no = resp.data.dn3ref_no;
                $scope.dn3sanctionref_no = resp.data.dn3sanctionref_no;
                $scope.dn3sanction_date = resp.data.dn3_date;
             //   $scope.dn3sanction_amount = resp.data.dn3sanction_amount;
                var amount_dn3 = new Intl.NumberFormat('en-IN').format(resp.data.dn3sanction_amount);
                $scope.dn3sanction_amount = amount_dn3;
                $scope.dn_flag = resp.data.dn_flag;
                if (resp.data.dn3_flag == "N") {
                    $scope.initiatedn3 = true;
                    $scope.sanctiondtl = false;
                }
                
                if (resp.data.dn3_flag == "Y") {
                    $scope.initiatedn3 = false;
                    $scope.sanctiondtl = true;
                }

            });
            var url = 'api/misDataimport/DN3Content';
            var param = {
                urn: $scope.urn

            };
            console.log(param);
            SocketService.getparams(url, param).then(function (resp) {
                $scope.content = resp.data.template_content;

            });
            

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
                    var url = 'api/misDataimport/RevertDN3';
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

                        }
                        $location.url('app/dnTracker?lstab=dn3tracker');
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
                    var url = 'api/misDataimport/HoldDN3';
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

                        }
                        $location.url('app/dnTracker?lstab=dn3tracker');
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
                    var url = 'api/misDataimport/UnholdDN3';
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

                        }
                        $location.url('app/dnTracker?lstab=dn3tracker');
                        activate();
                    });
                }
            }
        }

        $scope.dn3sanctionsubmit = function () {
            var url = 'api/misDataimport/DN3sanctiondtl';
            lockUI();
            var param = {
                urn: $scope.urn,
                dn3sanctionref_no: $scope.txtdn3sanctionref_no,
                dn3sanction_date: $scope.txtdn3sanction_date,
                dn3sanction_amount: $scope.txtdn3sanction_amount,
                dn3ref_no: $scope.txtdn3ref_no
            };
            console.log(param);
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                   
                    var url = 'api/misDataimport/GetSanctiondtl';
                    var param = {
                        urn: $scope.urn

                    };

                    SocketService.getparams(url, param).then(function (resp) {

                        $scope.dn3ref_no = resp.data.dn3ref_no;
                        $scope.dn3sanctionref_no = resp.data.dn3sanctionref_no;
                        $scope.dn3sanction_date = resp.data.dn3_date;
                   //     $scope.dn3sanction_amount = resp.data.dn3sanction_amount;
                        var amount_dn3 = new Intl.NumberFormat('en-IN').format(resp.data.dn3sanction_amount);
                        $scope.dn3sanction_amount = amount_dn3;
                        $scope.dn_flag = resp.data.dn_flag;
                        if (resp.data.dn_flag == "N") {
                            $scope.initiatedn3 = true;
                            $scope.sanctiondtl = false;
                        }
                        if (resp.data.dn3_flag == "Y") {
                            $scope.initiatedn3 = false;
                            $scope.sanctiondtl = true;
                        }
                    });
                    var url = 'api/misDataimport/DN3Content';
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
        $scope.dn3cancel = function () {
            $scope.courier_info = false;
            $scope.initiatedn3 = true;
            $scope.sanctiondtl = true
            $scope.info = false;
            $scope.txtdn3sanctionref_no = '';
            $scope.txtdn3sanction_date = '';
            $scope.txtdn3sanction_amount = '';
            $scope.txtdn3ref_no = '';
        }
        $scope.amountschange = function () {
            var input = document.getElementById('txtInput').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_sanctionamount = inWords(str);

            if (output == 'NaN') {
                Notify.alert('Accept Numeric Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdn3sanction_amount = "";

            }
            else {
                document.getElementById('sanctionamount_words').innerHTML = lswords_sanctionamount;
                $scope.txtdn3sanction_amount = output;
            }
          
        }
    }
})();