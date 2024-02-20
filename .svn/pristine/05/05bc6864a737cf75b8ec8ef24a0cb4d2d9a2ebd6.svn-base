(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dn2Customer2loandetailscontroller', dn2Customer2loandetailscontroller);

    dn2Customer2loandetailscontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function dn2Customer2loandetailscontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dn2Customer2loandetailscontroller';

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
            $scope.initiatedn2 = true;
            $scope.dn2format = true;
            $scope.button=true;
            $scope.revert = true;
            $scope.dn1status = true;
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
                $scope.DN1status = resp.data.DN1status;
                $scope.DN2status = resp.data.DN2status;
                console.log(resp.data.DN1status);
                if ((resp.data.DN1status == 'DN1 Sent')) {
                    $scope.dn1status = false;
                }
                if ((resp.data.DN1status == 'DN1 Skip')) {
                    $scope.dn1status = false;
                }
                if ((resp.data.DN2status == 'DN2 Sent')) {
                    $scope.dn2format = false;
                    $scope.data = true;
                    $scope.courierdetails = true;
                    $scope.initiatedn2 = true;
                    $scope.dn1status = true;
                }

                if ((resp.data.DN2status == 'DN2 Skip')) {
                    $scope.skip = true;
                    $scope.dn1status = true;
                }
                if ((resp.data.DN1_status == 'DN2 Generated')) {
                    console.log(resp.data.DN1_status);
                    $scope.dn2format = false;
                    $scope.button = false;
                    $scope.courier_info = false;
                    $scope.info = true;
                    $scope.dn1status = true;
                }
                if (resp.data.DN1_status == 'DN2 Reverted') {
                    $scope.initiatedn2 = true;
                    $scope.otherloan = true;
                    $scope.click = true;
                    $scope.dn2format = true;
                    $scope.button = true;
                    $scope.courier_info = false;
                    $scope.revert = false;
                    $scope.dn1status = true;
               
                }
                if ((resp.data.DN1_status == 'DN2 Hold')) {
                    $scope.dn2format = false;
                    $scope.button = false;
                    $scope.courier_info = false;
                    $scope.info = true;
                    $scope.dn1status = true;
                }
            });
            var url = "api/misDataimport/DN1ContentDTL"
            lockUI();
            var param = {
                urn: $scope.urn
            };
            
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.templatecontent = resp.data.dn2_content;
                document.getElementById('test').innerHTML += $scope.templatecontent;
                $scope.DN1_status = resp.data.DN1_status;
          
                if ((resp.data.DN1_status == 'DN2 Generated')) {
                    console.log(resp.data.DN1_status);
                    $scope.dn2format = false;
                    $scope.button = false;
                    $scope.courier_info = false;
                    $scope.info = true;
                    $scope.dn1status = true;
                }
                if (resp.data.DN1_status == 'DN2 Reverted') {
                    $scope.initiatedn2 = true;
                    $scope.otherloan = true;
                    $scope.click = true;
                    $scope.dn2format = true;
                    $scope.button = true;
                    $scope.courier_info = false;
                    $scope.revert = false;
                    $scope.dn1status = true;
                }
                if ((resp.data.DN1_status == 'DN2 Hold')) {
                    $scope.dn2format = false;
                    $scope.button = false;
                    $scope.courier_info = false;
                    $scope.info = true;
                    $scope.dn1status = true;
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
                if ((resp.data.DN1status == 'DN1 Skip') || (resp.data.DN1status == 'DN1 Sent') || (resp.data.DN1status == 'DN1 Generated')) {
                    $scope.dndetails = false;
                    $scope.dn1status = true;
                }
              
            });
            var url = "api/misDataimport/getcourierinfo"
            lockUI();
            var param = {
                urn: $scope.urn
            };
          
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
                $scope.dn2couriered_by = resp.data.dn2couriered_by;
                $scope.dn2courier_center = resp.data.dn2courier_center;
                $scope.dn2courier_date = resp.data.dn2courier_date;
                $scope.dn2courier_refno = resp.data.dn2courier_refno;
                $scope.dn2remarks = resp.data.dn2remarks;
                $scope.dn2courier_status = resp.data.dn2courier_status;
                console.log(resp.data.dn2courier_refno);
                console.log(resp.data.dn2remarks);
                console.log(resp.data.dn2courier_center);
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
        $scope.DN2generate = function () {
            $scope.courier_info = true;
            $scope.info = true;
            $scope.dn1status = true;
            var url = "api/misDataimport/DN2generate";
            lockUI();
            var param = {
                urn: $scope.urn,
                template_content: $scope.content

            };
          
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                   
                    Notify.alert('DN2 Generated  Successfully', 'success')
                }
                else {
                    Notify.alert('Error Occurred While Generating DN2')
                }
                $location.url('app/dnTracker?lstab=dn2tracker');
                activate();
            });

        }
        // Numeric to Word - Indian Standard...//

        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }
        $scope.DN2send = function () {
            var url = "api/misDataimport/DN2Status"
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
                $location.url('app/dnTracker?lstab=dn2tracker');
                activate();
            });
            $scope.courier_info = false;
            $scope.dn1status = true;
            $scope.info = true;
        }
        $scope.DN2skip = function () {
            var url = "api/misDataimport/DN2skip"
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
            $scope.dn1status = true;
        }
        $scope.Dn2back = function () {
            $location.url('app/dnTracker?lstab=dn2tracker');
        }
        $scope.cancel = function () {
            $scope.courier_info = false;
            $scope.initiatedn2 = true;
            $scope.sanctiondtl = true;
            $scope.info = false;
        }
        $scope.clickinitiatedn2 = function () {
            $scope.initiatedn2 = true;
            $scope.courier_info = false;
            $scope.dn1status = true;
            $scope.sanctiondtl = false;
            var url = 'api/misDataimport/Getsanctionloandetails';
            var param = {
                urn: $scope.urn

            };

            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtdn2sanctionref_no = resp.data.sanction_refno;
                $scope.txtdn2sanction_date = resp.data.sanction_date;
                $scope.txtdn2sanction_amount = resp.data.sanction_amount;
                $scope.txtdn2ref_no = "SAMFIN/RMD/";

            });

            var url = 'api/misDataimport/GetSanctiondtl';
            var param = {
                urn: $scope.urn

            };

            SocketService.getparams(url, param).then(function (resp) {

                $scope.dn2ref_no = resp.data.dn2ref_no;
                $scope.dn2sanctionref_no = resp.data.dn2sanctionref_no;
                $scope.dn2sanction_date = resp.data.dn2_date;
              //  $scope.dn2sanction_amount = resp.data.dn2sanction_amount;

                var amount_dn2 = new Intl.NumberFormat('en-IN').format(resp.data.dn2sanction_amount);
                $scope.dn2sanction_amount = amount_dn2;
                $scope.dn_flag = resp.data.dn_flag;
                if (resp.data.dn2_flag == "N") {
                    $scope.initiatedn2 = true;
                    $scope.sanctiondtl = false;
                }
                if (resp.data.dn2_flag == "Y") {
                    $scope.initiatedn2 = false;
                    $scope.sanctiondtl = true;
                }

            });


            var url = 'api/misDataimport/DN2Content';
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
                    var url = 'api/misDataimport/RevertDN2';
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
                        $location.url('app/dnTracker?lstab=dn2tracker');
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
                    var url = 'api/misDataimport/HoldDN2';
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
                        $location.url('app/dnTracker?lstab=dn2tracker');
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
                    var url = 'api/misDataimport/UnholdDN2';
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
                        $location.url('app/dnTracker?lstab=dn2tracker');
                        activate();
                    });
                }
            }
        }

        $scope.dn2sanctionsubmit = function () {
            var url = 'api/misDataimport/DN2sanctiondtl';
            lockUI();
            var param = {
                urn: $scope.urn,
                dn2sanctionref_no: $scope.txtdn2sanctionref_no,
                dn2sanction_date: $scope.txtdn2sanction_date,
                dn2sanction_amount: $scope.txtdn2sanction_amount,
                dn2ref_no: $scope.txtdn2ref_no
            };
       
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = 'api/misDataimport/GetSanctiondtl';
                    var param = {
                        urn: $scope.urn

                    };

                    SocketService.getparams(url, param).then(function (resp) {

                        $scope.dn2ref_no = resp.data.dn2ref_no;
                        $scope.dn2sanctionref_no = resp.data.dn2sanctionref_no;
                        $scope.dn2sanction_date = resp.data.dn2_date;
                      //  $scope.dn2sanction_amount = resp.data.dn2sanction_amount;

                        var amount_dn2 = new Intl.NumberFormat('en-IN').format(resp.data.dn2sanction_amount);
                        $scope.dn2sanction_amount = amount_dn2;
                        $scope.dn_flag = resp.data.dn_flag;
                        if (resp.data.dn_flag == "N") {
                            $scope.initiatedn2 = false;
                            $scope.sanctiondtl = true;
                        }
                        if (resp.data.dn2_flag == "Y") {
                            $scope.initiatedn2 = false;
                            $scope.sanctiondtl = true;
                        }
                    });
                    var url = 'api/misDataimport/DN2Content';
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
        $scope.dn2cancel = function () {
            $scope.courier_info = false;
            $scope.initiatedn2 = true;
            $scope.sanctiondtl = true;
            $scope.info = false;
            $scope.txtdn2sanctionref_no = '';
            $scope.txtdn2sanction_date = '';
            $scope.txtdn2sanction_amount = '';
            $scope.txtdn2ref_no = '';
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
                $scope.txtdn2sanction_amount = "";

            }
            else {
                document.getElementById('sanctionamount_words').innerHTML = lswords_sanctionamount;
                $scope.txtdn2sanction_amount = output;
            }
         
        }
    }
})();