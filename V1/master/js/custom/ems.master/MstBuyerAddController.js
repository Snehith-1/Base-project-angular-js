(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBuyerAddController', MstBuyerAddController);

    MstBuyerAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBuyerAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBuyerAddController';

        activate();

        function activate() { 
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.open1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/Mstbuyer/GetbuyerTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/Mstbuyer/GetBankAccountLevel';
            SocketService.get(url).then(function (resp) {
                $scope.bankaccountlevel_list = resp.data.bankaccountlevel_list;
            });
            var url = 'api/Mstbuyer/GetBankAccountType';
            SocketService.get(url).then(function (resp) {
                $scope.bankaccounttype_list = resp.data.bankaccounttype_list;
            });

            var url = 'api/customer/Getconstitution';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
            });

            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

        }

        $scope.onchangebusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            console.log(params);
            var url = 'api/Mstbuyer/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyear_business = resp.data.year_business;
                $scope.txtmonth_business = resp.data.month_business;
            });
        }

        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_no;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/MstApplicationAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }


        $scope.IFSCValidation = function () {

            if ($scope.txtifsc_code.length == 11) {
                var params = {
                    ifsc: $scope.txtifsc_code
                }

                var url = 'api/Kyc/IfscVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtbank_name = resp.data.result.bank;
                        $scope.txtbranch_name = resp.data.result.branch;
                        $scope.txtbank_address = resp.data.result.address;
                        $scope.txtmicr_code = resp.data.result.micr;

                        if (resp.data.result.micr == "" || resp.data.result.micr == null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtbank_name = '';
                        $scope.txtbranch_name = '';
                        $scope.txtbank_address = '';
                        $scope.txtmicr_code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }


        $scope.BankAccValidation = function () {

            if ($scope.txtbankaccount_number == $scope.txtconfirmbankaccount_number) {
                var params = {
                    ifsc: $scope.txtifsc_code,
                    accountNumber: $scope.txtconfirmbankaccount_number
                }

                var url = 'api/Kyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtbankaccount_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtbankaccount_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.getPANbasedGST = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/Kyc/GSTSBPAN';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/Mstbuyer/PostGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                            if (resp.data.status == true) {

                                gst_list();
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }
                            
                        });
                        
                    } else if (resp.data.statusCode == 103) {
                        var param = {
                            pan: $scope.txtpan_no
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidation = true;
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;
                                Notify.alert('PAN is not verified..!', 'warning');
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });
                      
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.Back = function () {
            $state.go('app.MstBuyerSummary');
        }

        $scope.add_Submit = function () {
            $state.go('app.MstBuyerSummary');
        }

    

        $scope.buyer_save = function () {
            var lsconstitution_gid = '';
            var lsconstitution_name = '';

            if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
                lsconstitution_gid = $scope.cboConstitution.constitution_gid;
                lsconstitution_name = $scope.cboConstitution.constitution_name;
            }
                var params = {
                    buyer_name: $scope.txtbuyer_name,
                    coi_date: $scope.txtcoi_date,
                    businessstart_date: $scope.txtbusinessstart_date,
                    year_business: $scope.txtyear_business,
                    month_business: $scope.txtmonth_business,
                    constitution_gid: lsconstitution_gid,
                    constitution_name: lsconstitution_name,
                    cin_no: $scope.txtcin_no,
                    pan_no: $scope.txtpan_no,
                    contactperson_fn: $scope.txtcontactperson_fn,
                    contactperson_mn: $scope.txtcontactperson_mn,
                    contactperson_ln: $scope.txtcontactperson_ln
  
                }
                var url = 'api/Mstbuyer/buyerSave';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.MstBuyerSummary');
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                 
                });
                
        }

        $scope.buyer_submit = function () {
            var params = {
                buyer_name: $scope.txtbuyer_name,
                coi_date: $scope.txtcoi_date,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                constitution_gid: $scope.cboConstitution.constitution_gid,
                constitution_name: $scope.cboConstitution.constitution_name,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln

            }
            var url = 'api/Mstbuyer/buyerSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstBuyerSummary');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
            
        }

        $scope.gst_add = function () {

            if (($scope.txtgst_no == '') || ($scope.txtgst_no == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_state == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;

                var params = {
                    gststate_name: $scope.txtgst_state,
                    gst_no: $scope.txtgst_no,
                    gstregister_status: $scope.rdbgstregister_status
                }
                var url = 'api/Mstbuyer/PostGST';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.cboGstState = '';
                        $scope.rdbgstregister_status = '';
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
                    $scope.txtgst_no = '';
                    $scope.txtgst_state == '';
                    
                });
            }
        }

        $scope.gst_delete = function (buyer2gst_gid) {
            var params =
                {
                    buyer2gst_gid: buyer2gst_gid
                }
            console.log(params)
            var url = 'api/Mstbuyer/DeleteGST';
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
            });

        }

        function gst_list() {
            var url = 'api/Mstbuyer/GetGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.buyergst_list = resp.data.buyergst_list;

            });
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
                 var url = 'api/Mstbuyer/PostMobileNo';
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
                }); 
            }
        }

        //--------Delete Mobile No--------//
        $scope.mobileno_delete = function (buyer2mobileno_gid) {
            var params =
                {
                    buyer2mobileno_gid: buyer2mobileno_gid
                }
            console.log(params)
            var url = 'api/Mstbuyer/DeleteMobileNo';
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
            });

        }

        function mobileno_list() {
            var url = 'api/Mstbuyer/GetMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.buyermobileno_list = resp.data.buyermobileno_list;

            });
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
                var url = 'api/Mstbuyer/PostEmailAddress';
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
                });
            }
        }

        $scope.emailaddress_delete = function (buyer2emailaddress_gid) {
            var params =
                {
                    buyer2emailaddress_gid: buyer2emailaddress_gid
                }
            console.log(params)
            var url = 'api/Mstbuyer/DeleteEmailAddress';
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
            });

        }

        function emailaddress_list() {
            var url = 'api/Mstbuyer/GetEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.buyeremailaddress_list = resp.data.buyeremailaddress_list;

            });
        }

        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined){
                        $scope.txtlatitude = '';
                        $scope.txtlongitude = '';
                    }
                    else if ($scope.txtpostal_code.length == 6) {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });
                    }
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
                        state_name: $scope.txtstate,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/Mstbuyer/PostAddress';
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
        
        $scope.address_delete = function (buyer2address_gid) {
            var params =
                {
                    buyer2address_gid: buyer2address_gid
                }
            console.log(params)
            var url = 'api/Mstbuyer/DeleteAddress';
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
            });

        }

        function address_list() {
            var url = 'api/Mstbuyer/GetAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.buyeraddress_list = resp.data.buyeraddress_list;

            });
        }

        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.bank_add = function () {

            if (($scope.txtbank_name == '') || ($scope.txtbank_name == undefined) || ($scope.txtbranch_name == '') || ($scope.txtbranch_name == undefined)
                || ($scope.txtbank_address == '') || ($scope.txtbank_address == undefined) || ($scope.txtifsc_code == '') || ($scope.txtifsc_code == undefined)
                || ($scope.txtbankaccount_name == '') || ($scope.txtbankaccount_name == undefined) || ($scope.cbobankaccountlevel.bankaccountlevel_name == '')
                || ($scope.cbobankaccountlevel.bankaccountlevel_name == undefined) || ($scope.cbobankaccounttype.bankaccounttype_name == '')
                || ($scope.cbobankaccounttype.bankaccounttype_name == undefined) || ($scope.txtbankaccount_number == '')
                || ($scope.txtbankaccount_number == undefined)) {
                $scope.mandatoryfields = true;
            }
            else {
                $scope.mandatoryfields = false;
                var params = {
                    ifsc_code: $scope.txtifsc_code,
                    bank_name: $scope.txtbank_name,
                    branch_name: $scope.txtbranch_name,
                    bank_address: $scope.txtbank_address,
                    micr_code: $scope.txtmicr_code,
                    bankaccount_name: $scope.txtbankaccount_name,
                    bankaccountlevel_gid: $scope.cbobankaccountlevel.bankaccountlevel_gid,
                    bankaccountlevel_name: $scope.cbobankaccountlevel.bankaccountlevel_name,
                    bankaccounttype_gid: $scope.cbobankaccounttype.bankaccounttype_gid,
                    bankaccounttype_name: $scope.cbobankaccounttype.bankaccounttype_name,
                    bankaccount_number: $scope.txtbankaccount_number,
                    confirmbankaccountnumber: $scope.txtconfirmbankaccount_number                    
                }
                var url = 'api/Mstbuyer/PostBank';
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
                    bank_list();
                    $scope.txtbank_name = '';
                    $scope.txtbranch_name = '';
                    $scope.txtbank_address = '';
                    $scope.txtmicr_code = '';
                    $scope.txtifsc_code = '';
                    $scope.txtbankaccount_name = '';
                    $scope.cbobankaccountlevel = '';
                    $scope.cbobankaccounttype = '';
                    $scope.txtbankaccount_number = '';
                    $scope.txtconfirmbankaccount_number = ''; 
                });
            }
        }

        $scope.bank_delete = function (buyer2bank_gid) {
            var params =
                {
                    buyer2bank_gid: buyer2bank_gid
                }
            console.log(params)
            var url = 'api/Mstbuyer/DeleteBank';
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

                bank_list();
            });

        }

        function bank_list() {
            var url = 'api/Mstbuyer/GetBankList';
            SocketService.get(url).then(function (resp) {
                $scope.buyerbank_list = resp.data.buyerbank_list;

            });
        }

        
  
       
    }
})();

