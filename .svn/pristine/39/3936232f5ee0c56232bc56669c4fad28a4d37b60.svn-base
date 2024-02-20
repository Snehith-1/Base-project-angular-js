(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingAddController', MstSAOnboardingAddController);

        MstSAOnboardingAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSAOnboardingAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
    /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingAddController';

        activate();

        function activate() { 
            
          lockUI();
          vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
     
                vm.open1 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            }; 
            //var url='api/';
            //SocketService.get(url).then(function (resp) {
              //  $scope.gstregistertion_list = resp.data.application_list;
            //});
           unlockUI();
        }
       
        $scope.addsaonboaring =function () {
                var params = {
                 sa_type: $scope.cbosatype,
                 sa_entity_type: $scope.cbosaentitytype,
                 designation: $scope.cbodesignation,
                 samunnati_associate_name: $scope.txtsamunnati_associate_name,
                 sacontact_person_first_name: $scope.txtsacontact_person_first_name,
                 sacontact_person_middle_name: $scope.txtsacontact_person_middle_name,
                 sacontact_person_last_name: $scope.txtsacontact_person_last_name,
                 date_of_incorporation: $scope.txtdateofincorporation_date,
                 annual_turnover: $scope.txtannual_turnover,
                 company_pannumber: $scope.txtcompany_pannumber,
                 gst_registered_state: $scope.cbogstregisteredstate,
                 gst_registration_number: $scope.txtgst_registration_number, 
                 txtifsc_code :$scope.txtifsc_code,
                 txtbankaccount_number : $scope.txtbankaccount_number,
                 txtaccountholder_name :$scope.txtaccountholder_name,
                 txtcancelledchequenumber_name :$scope.txtcancelledchequenumber_name,
                 txtbank_name: $scope.txtbank_name,
                 txtbranch_name : $scope.txtbranch_name
                }
               // var url = 'api/';
               // lockUI();
                SocketService.post(url, params).then(function (resp) {
                 //   unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            $state.go('app.MstSAOnboardingSummary');
        }
        $scope.back = function () {

            $state.go('app.MstSAOnboardingSummary');
        }
        $scope.update=function() {
            var param= {
                gst_registered_state: $scope.cbogstregisteredstate,
                rbogst_status: $scope.rbogst_status,
            }
            // var url = 'api/';
               //lockUI();
            SocketService.post(url, params).then(function (resp) {
                //   unlockUI();
                   if (resp.data.status == true) {

                       Notify.alert(resp.data.message, {
                           status: 'success',
                           pos: 'top-center',
                           timeout: 3000
                       });
                   }
                   else {
                       Notify.alert(resp.data.message, {
                           status: 'info',
                           pos: 'top-center',
                           timeout: 3000
                       });
                   }
                   activate();
               });   
        }
        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAddress.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                //var url = 'api/';
              //  SocketService.get(url).then(function (resp) {
             //      $scope.addresstype_list = resp.data.addresstype_list;
             //   });
              //  var url = 'api/';
             //   SocketService.get(url).then(function (resp) {
             //       $scope.state_list = resp.data.state_list;
             //   });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                  //  var url = 'api/';

                 /*   SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                    });*/
                }

                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        addresstype_name: $scope.cboaddresstype.address_type,
                        primary_address: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        landmark: $scope.txtlandmark,
                        postal_code: $scope.txtpostal_code,
                        city: $scope.txtcity,
                        taluka: $scope.txttaluka,
                        district: $scope.txtdistrict,
                        state_gid: $scope.cbostate.state_gid,
                        state_name: $scope.cbostate.state_name,
                        country: $scope.txtcountry
                        
                    }
                    
                    //var url = 'api/;
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        
                    });

                    $modalInstance.close('closed');

                }

               

            }
        }
        $scope.address_delete = function (companyaddress_gid) {
            var params =
                {
                    companyaddress_gid: companyaddress_gid
                }
        /*    var url = 'api/';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                address_list();
            }); */

        }

        function address_list() {
          /*  var url = 'api';
            SocketService.get(url).then(function (resp) {
                $scope.companyaddress_list = resp.data.companyaddress_list;

            }); */
        }
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
        $scope.annual_turnoverChange = function () {
            var input = document.getElementById('annual_turnover').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualturnover = inWords(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtannual_turnover = "";
            }
            else {
                $scope.txtannual_turnover = output;
                document.getElementById('words_annualturnover').innerHTML = lswords_annualturnover;
            }
        }

        $scope.mobileno_add = function () {

            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
                Notify.alert('Enter Mobile No/Select Status');
            }
            else {


                 var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_mobileno: $scope.rdbprimarymobile_no,
                    whatsapp_mobileno: $scope.rdbwhatsappmobile_no
                }
            /*  var url = 'api/';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    mobileno_list();
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbwhatsappmobile_no = '';
                    $scope.rdbprimarymobile_no == false;
                }); */
            }
        }

        //--------Delete Mobile No--------//
        $scope.mobileno_delete = function () {
            var params =
                {
                    companymobileno_gid: companymobileno_gid
                }
            
          /*  var url = 'api/';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                mobileno_list();
            }); */

        }

        function mobileno_list() {
         /*   var url = 'api/';
            SocketService.get(url).then(function (resp) {
                $scope.companymobileno_list = resp.data.companymobileno_list;

            }); */
        }

        $scope.emailaddress_add = function () {

            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter Email Address/Select Status');
            }
            else {


                var params = {
                    email_address: $scope.txtemail_address,
                    primary_emailaddress: $scope.rdbprimaryemail_address,
                }
            /*    var url = 'api/';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    emailaddress_list();
                    $scope.txtemail_address = '';
                    $scope.rdbprimaryemail_address = '';
                    $scope.rdbprimaryemail_address == false;
                }); */
            }
        }

        $scope.emailaddress_delete = function (companyemailaddress_gid) {
            var params =
                {
                    companyemailaddress_gid: companyemailaddress_gid
                }
         /*   var url = 'api/';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                emailaddress_list();
            }); */
        }

        function emailaddress_list() {
         /*   var url = 'api/';
            SocketService.get(url).then(function (resp) {
                $scope.companyemailaddress_list = resp.data.companyemailaddress_list;

            }); */
        }

        
        $scope.gst_add = function () {

            if (($scope.txtgst_registration_number == '') || ($scope.txtgst_registration_number == undefined) || ($scope.cbogstregisteredstate == '') || ($scope.rbogst_status == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;

                var params = {
                    txtgst_registration_number: $scope.txtgst_registration_number,
                    rbogst_status : $scope.rbogst_status,
                    cbogstregisteredstate : $scope.cbogstregisteredstate
                }
            /*    var url = 'api/';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.cbogstregisteredstate = '';
                        $scope.rbogst_status = '';
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    gst_list();
                    $scope.txtgst_registration_number = '';
                    
                }); */
            }
        }
        function gst_list() {
            var url='api/'
            SocketService.post(url, params).then(function (resp) {
                $scope.companygst_list = resp.data.companygst_list; 
            });
        }

        $scope.gst_delete = function (companyprospects_gid) {
            var params =
                {
                    companyprospects_gid: companyprospects_gid
                }
         /*   var url = 'api/';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                gst_list();
            }); */
        }

        $scope.prospects_add = function () {
            var params={
                lead_name : $scope.lead_name,
                sector_name : $scope.sector_name
            }
           /* var url='api/'
            lockUI();
            SocketService.post(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                 prospects_list();
            }
            else {
                Notify.alert(resp.data.message, {
                    status: 'info',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
                
            }); */

            prospects_list();
            $scope.lead_name = '',
            $scope.sector_name = ''
        }
        function prospects_list() {
            var url='api/'
            SocketService.post(url, params).then(function (resp) {
                $scope.companyprospects_list = resp.data.companyprospects_list; 
            });
        }
        $scope.prospects_delete = function (companyprospects_gid) {
            var params =
                {
                    companyprospects_gid: companyprospects_gid
                }
         /*   var url = 'api/';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                prospects_list();
            }); */
        }
    }
})();
