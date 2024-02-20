(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingAddController', MstMarketingAddController);

    MstMarketingAddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function MstMarketingAddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingAddController';
        activate();

        function activate() {
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            
            var url = 'api/Marketing/MarketingCallTempClear';
            SocketService.get(url).then(function (resp) {
            });

            $scope.cbointernal_reference="NA";
            var d = new Date();
            var time = d.toLocaleString([], { hour: '2-digit', minute: '2-digit' });


            var today = new Date();
            var date = 0 + today.getDate() + '-' +(today.getMonth() + 1) + '-' + today.getFullYear();
            var todaytime = date + ' ' + '/' + ' ' + time;
            $scope.txtcallreceived_date = todaytime;

            $scope.minDate = new Date();
            var d = new Date();
            var time = d.toLocaleString([], { hour: '2-digit', minute: '2-digit' });
           
            $scope.txtcallreceived_time = time;

            var url = 'api/Marketing/GetEntity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.inboundentity_list;
            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/Marketing/GetMarketingSourceofContact';
            SocketService.get(url).then(function (resp) {
                $scope.sourceofcontact_list = resp.data.MarketingSourceofContact_list;
            });
            var url = 'api/Marketing/GetMarketingCallReceivedNumber';
            SocketService.get(url).then(function (resp) {
                $scope.callreceivednumber_list = resp.data.MarketingCallReceivedNumber_list;
            });
            var url = 'api/Marketing/GetMarketingCallType';
            SocketService.get(url).then(function (resp) {
                $scope.calltype_list = resp.data.MarketingCallType_list;
            });
            var url = 'api/Marketing/GetMarketingTelecallingFunction';
            SocketService.get(url).then(function (resp) {
                $scope.telecallingfunction_list = resp.data.MarketingTelecallingFunction_list;
            });
            var url = 'api/Marketing/GetLeadRequestType';
            SocketService.get(url).then(function (resp) {
                $scope.leadrequesttype_list = resp.data.leadrequest_list;
            });
        }

        $scope.changefunctionstatus = function (marketingtelecallingfunction_name) {
            if ($('#function :selected').text() == 'Others') {
                $scope.function_show = true;
            }
            else {
                $scope.function_show = false;
            }
        }

        $scope.back = function(){
            $location.url('app/MstMarketingSummary');
        }

        $scope.auditname_change = function (cboassignemployee) {
            var params = {
                employee_gid: $scope.cboassignemployee
            }
            var url = 'api/Marketing/GetBaselocation';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.employee_gid = resp.data.employee_gid;
                $scope.txtbase_location = resp.data.baselocation_name;
               
            });
           
        }


        $scope.save = function(){
            var lsentity_name = '';
            var lsentity_gid = '';
            var lssourceofcontact_name = '';
            var lssourceofcontact_gid = '';
            var lscallreceivednumber_name = '';
            var lscallreceivednumber_gid = '';
            var lsinternalreference_name = '';
            var lsinternalreference_gid = '';
            var lscalltype_name = '';
            var lscalltype_gid = '';
            var lsfunction_name = '';
            var lsfunction_gid = '';
            var lsassignemployee_name = '';
            var lsassignemployee_gid = '';
            var lstagemployee_name = '';
            var lstagemployee_gid = '';
            if ($scope.cboentity != undefined || $scope.cboentity != null) {
                lsentity_name = $('#entity :selected').text();
                lsentity_gid = $scope.cboentity;
           }
           if ($scope.cbosourceofcontact != undefined || $scope.cbosourceofcontact != null) {
                lssourceofcontact_name = $('#sourceofcontact :selected').text();
                lssourceofcontact_gid = $scope.cbosourceofcontact;
           }
           if ($scope.cbocallreceivednumber != undefined || $scope.cbocallreceivednumber != null) {
                lscallreceivednumber_name = $('#callreceivednumber :selected').text();
                lscallreceivednumber_gid = $scope.cbocallreceivednumber;
           }
           if ($scope.cboleadrequesttype != undefined || $scope.cboleadrequesttype != null) {
               lsleadrequesttype_name = $('#leadrequesttype :selected').text();
               lsleadrequesttype_gid = $scope.cboleadrequesttype;
           }
           if ($scope.cbointernalreference != undefined || $scope.cbointernalreference != null) {
                lsinternalreference_name = $('#internalreference :selected').text();
                lsinternalreference_gid = $scope.cbointernalreference;
           }
           if ($scope.cbocalltype != undefined || $scope.cbocalltype != null) {
                lscalltype_name = $('#call_type :selected').text();
                lscalltype_gid = $scope.cbocalltype;
           }
           if ($scope.cbofunction != undefined || $scope.cbofunction != null) {
                lsfunction_name = $('#function :selected').text();
                lsfunction_gid = $scope.cbofunction;
           }
           if ($scope.cboassignemployee != undefined || $scope.cboassignemployee != null) {
                lsassignemployee_name = $('#assignemployee :selected').text();
                lsassignemployee_gid = $scope.cboassignemployee;
           }
     
            var params={    
                entity_name: $scope.lsentity_name,
                entity_gid:lsentity_gid,
                marketingsourceofcontact_name:lssourceofcontact_name,
                marketingsourceofcontact_gid: lssourceofcontact_gid,
                leadrequesttype_name: lsleadrequesttype_name,
                leadrequesttype_gid: lsleadrequesttype_gid,
                marketingcallreceivednumber_name: lscallreceivednumber_name,
                marketingcallreceivednumber_gid: lscallreceivednumber_gid,
                customer_type:$scope.cbocustomertype,
                callreceived_date: $scope.txtcallreceived_date,
                callreceived_time: $scope.txtcallreceived_time,
                caller_name: $scope.txtcaller_name,
                internalreference_name: lsinternalreference_name,
                internalreference_gid: lsinternalreference_gid,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno : $scope.txtoffice_landlineno,
                marketingcalltype_name: lscalltype_name,
                marketingcalltype_gid: lscalltype_gid,
                marketingfunction_name: lsfunction_name,
                marketingfunction_gid: lsfunction_gid,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description:$scope.txtenquiry_description,
                callclosure_status:$scope.cbocallclosure_status,
                assignemployee_name:lsassignemployee_name,
                assignemployee_gid: lsassignemployee_gid,
                baselocation_name: $scope.txtbase_location,
                tat_hours: $scope.txttat_hours,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks
            }
            var url = 'api/Marketing/MarketingCallSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstMarketingSummary");
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
             
        $scope.submit = function(){
            var lsentity_name = '';
            var lsentity_gid = '';
            var lssourceofcontact_name = '';
            var lssourceofcontact_gid = '';
            var lsleadrequesttype_name = '';
            var lsleadrequesttype_gid = '';
            var lscallreceivednumber_name = '';
            var lscallreceivednumber_gid = '';
            var lsinternalreference_name = '';
            var lsinternalreference_gid = '';
            var lscalltype_name = '';
            var lscalltype_gid = '';
            var lsfunction_name = '';
            var lsfunction_gid = '';
            var lsassignemployee_name = '';
            var lsassignemployee_gid = '';
            var lstagemployee_name = '';
            var lstagemployee_gid = '';
            if ($scope.cboentity != undefined || $scope.cboentity != null) {
                lsentity_name = $('#entity :selected').text();
                lsentity_gid = $scope.cboentity;
            }
            if ($scope.cbosourceofcontact != undefined || $scope.cbosourceofcontact != null) {
                lssourceofcontact_name = $('#sourceofcontact :selected').text();
                lssourceofcontact_gid = $scope.cbosourceofcontact;
            }
            if ($scope.cbocallreceivednumber != undefined || $scope.cbocallreceivednumber != null) {
                lscallreceivednumber_name = $('#callreceivednumber :selected').text();
                lscallreceivednumber_gid = $scope.cbocallreceivednumber;
            }
            if ($scope.cboleadrequesttype != undefined || $scope.cboleadrequesttype != null) {
                lsleadrequesttype_name = $('#leadrequesttype :selected').text();
                lsleadrequesttype_gid = $scope.cboleadrequesttype;
            }
            if ($scope.cbointernalreference != undefined || $scope.cbointernalreference != null) {
                lsinternalreference_name = $('#internalreference :selected').text();
                lsinternalreference_gid = $scope.cbointernalreference;
            }
            if ($scope.cbointernalreference == undefined || $scope.cbointernalreference == null) {
                lsinternalreference_name = 'NA';
                lsinternalreference_gid = 'gid';
            }
            if ($scope.cbocalltype != undefined || $scope.cbocalltype != null) {
                lscalltype_name = $('#call_type :selected').text();
                lscalltype_gid = $scope.cbocalltype;
            }
            if ($scope.cbofunction != undefined || $scope.cbofunction != null) {
                lsfunction_name = $('#function :selected').text();
                lsfunction_gid = $scope.cbofunction;
            }
            if ($scope.cboassignemployee != undefined || $scope.cboassignemployee != null) {
                lsassignemployee_name = $('#assignemployee :selected').text();
                lsassignemployee_gid = $scope.cboassignemployee;
            }

            if (($scope.cbocallclosure_status == 'Assign') && ($scope.cboassignemployee == null || $scope.cboassignemployee == '' )) {
                Notify.alert('Kindly Select Assign Employee Name', 'warning')
            }
            //else if (($scope.cbocallclosure_status == 'Assign') && ($scope.txttat_hours == null || $scope.txttat_hours == '')) {
            //    Notify.alert('Kindly Select Assign TAT Hours', 'warning')
            //}
            else if ((($scope.txtclosure_remarks == '') || ($scope.txtclosure_remarks == null ) )&& (($scope.txtassignclosure_remarks == '')||($scope.txtassignclosure_remarks == null))) {
                Notify.alert('Kindly Enter Remark', 'warning')
            }
           
            else {

                var params = {
                    entity_name: lsentity_name,
                    entity_gid: lsentity_gid,
                    marketingsourceofcontact_name: lssourceofcontact_name,
                    marketingsourceofcontact_gid: lssourceofcontact_gid,
                    marketingcallreceivednumber_name: lscallreceivednumber_name,
                    marketingcallreceivednumber_gid: lscallreceivednumber_gid,
                    leadrequesttype_name: lsleadrequesttype_name,
                    leadrequesttype_gid: lsleadrequesttype_gid,
                    customer_type: $scope.cbocustomertype,
                    callreceived_date: $scope.txtcallreceived_date,
                    callreceived_time: $scope.txtcallreceived_time,
                    caller_name: $scope.txtcaller_name,
                    internalreference_name: lsinternalreference_name,
                    internalreference_gid: lsinternalreference_gid,
                    callerassociate_company: $scope.txtcallerassociate_company,
                    office_landlineno: $scope.txtoffice_landlineno,
                    marketingcalltype_name: lscalltype_name,
                    marketingcalltype_gid: lscalltype_gid,
                    marketingfunction_name: lsfunction_name,
                    marketingfunction_gid: lsfunction_gid,
                    function_remarks: $scope.txtfunction_remarks,
                    requirement: $scope.txtrequirement,
                    enquiry_description: $scope.txtenquiry_description,
                    callclosure_status: $scope.cbocallclosure_status,
                    assignemployee_name: lsassignemployee_name,
                    assignemployee_gid: lsassignemployee_gid,
                    baselocation_name: $scope.txtbase_location,
                    tat_hours: $scope.txttat_hours,
                    tagemployee_list: $scope.cbotagemployee,
                    assignclosure_remarks: $scope.txtassignclosure_remarks,
                    closed_remarks: $scope.txtclosure_remarks
                }
                var url = 'api/Marketing/MarketingCallSubmit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstMarketingSummary");
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
           
        }

        //Address Multiple Add

        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addaddress.html',
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
                    if($scope.txtpostal_code == undefined || $scope.txtpostal_code.length < 6){

                        $scope.txtlatitude = '';

                        $scope.txtlongitude = '';

                    }

                    else {
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
                        primary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        landmark: $scope.txtLand_Mark,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/Marketing/PostMarketingCallAddress';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');
                }
            }
        }
        $scope.address_delete = function (Marketingcall2address_gid) {
            var params =
                {
                    Marketingcall2address_gid: Marketingcall2address_gid
                }
            var url = 'api/Marketing/MarketingCallAddressDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });

        }
        function address_list() {
            var url = 'api/Marketing/GetMarketingCallAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
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

        //Mobile Number Multiple Add
        $scope.add_mobileno = function () {
            //if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_status == undefined) || ($scope.rdbsms_to == undefined)) {
            //    Notify.alert('Enter Mobile Number/Select Status', 'warning');
            //}
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_status == undefined) || ($scope.rdbsms_to == undefined) || ($scope.rdbprimary_status == '') || ($scope.rdbwhatsapp_status == '') || ($scope.rdbsms_to == '')) {
                Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_status: $scope.rdbprimary_status,
                    whatsapp_status: $scope.rdbwhatsapp_status,
                    sms_to: $scope.rdbsms_to
                }
                var url = 'api/Marketing/PostMarketingCallMobileNo';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        
                        $scope.txtmobile_no = '';
                        $scope.rdbprimary_status = '';
                        $scope.rdbprimary_status == false;
                        $scope.rdbsms_to = '';
                        $scope.rdbsms_to == false;
                        $scope.rdbwhatsapp_status = '';
                        $scope.rdbwhatsapp_status == false;
                        mobileno_list();
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
        }
        $scope.delete_mobileno = function (marketingcall2mobileno_gid) {
            var params = {
                marketingcall2mobileno_gid: marketingcall2mobileno_gid
            }
            var url = 'api/Marketing/MarketingCallMobileNoDelete';
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
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                mobileno_list();
            });
        }
        function mobileno_list() {
            var url = 'api/Marketing/GetMarketingCallMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
            });
        }
        //Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_email == undefined) || ($scope.rdbprimary_email == '')) {
                Notify.alert('Enter Email Address/Select Status', 'warning');
            }
            else {
                var params = {
                    email_address: $scope.txtemail_address,
                    primary_status: $scope.rdbprimary_email,
                }
                var url = 'api/Marketing/PostMarketingCallEmail';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        emailaddress_list();
                        $scope.txtemail_address = '';
                        $scope.rdbprimary_email = '';
                        $scope.rdbprimary_email == false;
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
        }
        $scope.delete_emailaddress = function (marketingcall2email_gid) {
            var params = {
                marketingcall2email_gid: marketingcall2email_gid
            }
            var url = 'api/Marketing/MarketingCallEmailDelete';
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
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                emailaddress_list();
            });
        }
        function emailaddress_list() {
            var url = 'api/Marketing/GetMarketingCallEmailList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
            });
        }
//Follow Up Multiple Add
        $scope.add_followup = function () {
            if (($scope.txtfollowup_date == undefined) || ($scope.txtfollowup_date == '') || ($scope.txtfollowup_time == undefined) || ($scope.txtfollowup_time == '')) {
                Notify.alert('Enter Follow Up Date/Follow Up Time', 'warning');
            }
            else {
                var params = {
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                }
                var url = 'api/Marketing/PostMarketingCallFollowUp';
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
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtfollowup_date = '';
                    followup_list();

                    $scope.txtfollowup_time = '';
                });
            }
        }
        $scope.delete_followup = function (marketingcall2followup_gid) {
            var params ={
                marketingcall2followup_gid: marketingcall2followup_gid
                }
            var url = 'api/Marketing/MarketingCallFollowUpDelete';
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
                        status: 'warning', 
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                followup_list();
            });
        }
        function followup_list() {
            var url = 'api/Marketing/GetMarketingCallFollowUpList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
            }); 
        }
    }
})();