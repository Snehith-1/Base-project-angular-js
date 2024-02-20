(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstInboundEditController', MstInboundEditController);

    MstInboundEditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function MstInboundEditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstInboundEditController';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.inboundcall_gid = searchObject.lsinboundcall_gid;
        activate();

        function activate() {
            $scope.cbotagemployee = [];
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
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.cbointernal_reference = "NA";

            var today = new Date();
            var date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
            $scope.txtcallreceived_date = date;

            var url = 'api/TeleCalling/IBCallTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var param = {
                inboundcall_gid: $scope.inboundcall_gid
            };

            var url = 'api/TeleCalling/GetEntity';
            SocketService.get(url).then(function (resp) {
                $scope.entity_list = resp.data.inboundentity_list;
            });
            var url = 'api/employee/Employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            }); 
            var url = 'api/MstApplication360/GetSourceofContact';
            SocketService.get(url).then(function (resp) {
                $scope.sourceofcontact_list = resp.data.application_list;
            });
            var url = 'api/MstApplication360/GetCallReceivedNumber';
            SocketService.get(url).then(function (resp) {
                $scope.callreceivednumber_list = resp.data.application_list;
            });
            var url = 'api/MstApplication360/GetCallType';
            SocketService.get(url).then(function (resp) {
                $scope.calltype_list = resp.data.application_list;
            });
            var url = 'api/MstApplication360/GetTelecallingFunction';
            SocketService.get(url).then(function (resp) {
                $scope.telecallingfunction_list = resp.data.application_list;
            });
            var url = 'api/TeleCalling/IBCallMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.ibcallmobileno_list;
            });
            var url = 'api/TeleCalling/IBCallEmailList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallemail_list = resp.data.ibcallemail_list;
            });
            var url = 'api/TeleCalling/IBCallAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.ibcalladdress_list;
            });
            var url = 'api/TeleCalling/IBCallFollowUpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.ibcallfollowup_list;
            });

            var url = 'api/TeleCalling/EditIBCall';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid;
                $scope.cboentity = resp.data.entity_gid;
                $scope.cbosourceofcontact = resp.data.sourceofcontact_gid;
                $scope.cbocallreceivednumber = resp.data.callreceivednumber_gid;
                $scope.cbocustomer_type = resp.data.customer_type;
                $scope.txtcallreceived_date = resp.data.callreceived_date;
                $scope.txtcaller_name = resp.data.caller_name;
                $scope.cbointernalreference = resp.data.internalreference_gid;
                $scope.txtcallerassociate_company = resp.data.callerassociate_company;
                $scope.txtoffice_landlineno = resp.data.office_landlineno;

                $scope.cbocalltype = resp.data.calltype_gid;
                $scope.cbofunction = resp.data.function_gid;
                $scope.txtfunction_remarks = resp.data.function_remarks;
                $scope.txtrequirement = resp.data.requirement;
                $scope.txtenquiry_description = resp.data.enquiry_description;
                $scope.cbocallclosure_status = resp.data.callclosure_status;
                $scope.cboassignemployee = resp.data.assignemployee_gid;
                $scope.txttat_hours = resp.data.tat_hours;
                $scope.txttat_date = resp.data.tat_date;
                $scope.txttat_days = resp.data.tat_days;
                $scope.emp_list = resp.data.emp_list;
                if (resp.data.tagemployee_list != null) {
                    var count = resp.data.tagemployee_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.emp_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.tagemployee_list[i].employee_gid);
                        //var indexs = $scope.emp_list.findIndex(x => x.employee_gid === resp.data.tagemployee_list[i].employee_gid);
                        $scope.cbotagemployee.push($scope.emp_list[indexs]);
                        $scope.$parent.cbotagemployee = $scope.cbotagemployee;
                    }
                }



                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks;

               

                if (resp.data.inboundcall_status == "Incomplete") {
                    $scope.ibcallSubmit = true;
                    $scope.ibcallUpdate = false;
                }
                else {
                    $scope.ibcallSubmit = false;
                    $scope.ibcallUpdate = true;
                }

                if (resp.data.function_name == 'Others') {
                    $scope.function_show = true;
                }
                else {
                    $scope.function_show = false;
                }

                unlockUI();
            });
        

        }

        $scope.changefunctionstatus = function (telecallingfunction_name) {
            if ($('#function :selected').text() == 'Others') {
                $scope.function_show = true;
            }
            else {
                $scope.function_show = false;
            }
        }

        $scope.back = function () {
            $location.url('app/MstTelecallingSummary');
        }
        $scope.save = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var callreceivednumber_Name = $('#callreceivednumber :selected').text();
            var internalreference_Name = $('#internalreference :selected').text();
            var calltype_Name = $('#calltype :selected').text();
            var function_Name = $('#function :selected').text();
            var assignemployee_Name = $('#assignemployee :selected').text();

          
            var tagemployee = $scope.cbotagemployee;
            if (tagemployee == [undefined]) {
                var cbotagemployee = null;
            }
            else {
                var cbotagemployee = $scope.cbotagemployee;
            }
            var params = {
                entity_name: entity_Name,
                entity_gid: $scope.cboentity,
                sourceofcontact_name: sourceofcontact_Name,
                sourceofcontact_gid: $scope.cbosourceofcontact,
                callreceivednumber_name: callreceivednumber_Name,
                callreceivednumber_gid: $scope.cbocallreceivednumber,
                customer_type: $scope.cbocustomer_type,
                callreceived_date: $scope.txtcallreceived_date,
                caller_name: $scope.txtcaller_name,
                internalreference_name: internalreference_Name,
                internalreference_gid: $scope.cbointernalreference,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno: $scope.txtoffice_landlineno,
                calltype_name: calltype_Name,
                calltype_gid: $scope.cbocalltype,
                function_name: function_Name,
                function_gid: $scope.cbofunction,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description: $scope.txtenquiry_description,
                callclosure_status: $scope.cbocallclosure_status,
                assignemployee_name: assignemployee_Name,
                assignemployee_gid: $scope.cboassignemployee,
                tat_hours: $scope.txttat_hours,
                tat_date: $scope.txttat_date,
                tat_days: $scope.txttat_days,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks,
                inboundcall_gid: $scope.inboundcall_gid
            }
            var url = 'api/TeleCalling/IBCallEditSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstTelecallingSummary");
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

        $scope.submit = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var callreceivednumber_Name = $('#callreceivednumber :selected').text();
            var internalreference_Name = $('#internalreference :selected').text();
            var calltype_Name = $('#calltype :selected').text();
            var function_Name = $('#function :selected').text();
            var assignemployee_Name = $('#assignemployee :selected').text();


            var tagemployee = $scope.cbotagemployee;
            if (tagemployee == [undefined]) {
                var cbotagemployee = null;
            }
            else {
                var cbotagemployee = $scope.cbotagemployee;
            }
            var params = {
                entity_name: entity_Name,
                entity_gid: $scope.cboentity,
                sourceofcontact_name: sourceofcontact_Name,
                sourceofcontact_gid: $scope.cbosourceofcontact,
                callreceivednumber_name: callreceivednumber_Name,
                callreceivednumber_gid: $scope.cbocallreceivednumber,
                customer_type: $scope.cbocustomer_type,
                callreceived_date: $scope.txtcallreceived_date,
                caller_name: $scope.txtcaller_name,
                internalreference_name: internalreference_Name,
                internalreference_gid: $scope.cbointernalreference,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno: $scope.txtoffice_landlineno,
                calltype_name: calltype_Name,
                calltype_gid: $scope.cbocalltype,
                function_name: function_Name,
                function_gid: $scope.cbofunction,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description: $scope.txtenquiry_description,
                callclosure_status: $scope.cbocallclosure_status,
                assignemployee_name: assignemployee_Name,
                assignemployee_gid: $scope.cboassignemployee,
                tat_hours:$scope.txttat_days,
                tat_days:$scope.txttatc_days,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks,
                inboundcall_gid: $scope.inboundcall_gid
            }
            var url = 'api/TeleCalling/IBCallEditSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url("app/MstTelecallingSummary");
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

        $scope.update = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var callreceivednumber_Name = $('#callreceivednumber :selected').text();
            var internalreference_Name = $('#internalreference :selected').text();
            var calltype_Name = $('#calltype :selected').text();
            var function_Name = $('#function :selected').text();
            var assignemployee_Name = $('#assignemployee :selected').text();
            var tat_day = $('#allocated_days :selected').text();


            var tagemployee = $scope.cbotagemployee;
            if (tagemployee == [undefined]) {
                var cbotagemployee = null;
            }
            else {
                var cbotagemployee = $scope.cbotagemployee;
            }

            if (($scope.cbocallclosure_status == 'Assign') && ($scope.cboassignemployee == null || $scope.cboassignemployee == '')) {
                Notify.alert('Kindly Select Assign Employee Name', 'warning')
            }
            else if (($scope.cbocallclosure_status == 'Assign') && ($scope.txttat_hours == null || $scope.txttat_hours == '')) {
                Notify.alert('Kindly Select Assign TAT Hours', 'warning')
            }
            else if (($('#function :selected').text() == 'Others') && ($scope.txtfunction_remarks == null || $scope.txtfunction_remarks == '')) {
                Notify.alert('Kindly Select Function Remarks', 'warning')
            }
            else {
                var params = {
                    entity_name: entity_Name,
                    entity_gid: $scope.cboentity,
                    sourceofcontact_name: sourceofcontact_Name,
                    sourceofcontact_gid: $scope.cbosourceofcontact,
                    callreceivednumber_name: callreceivednumber_Name,
                    callreceivednumber_gid: $scope.cbocallreceivednumber,
                    customer_type: $scope.cbocustomer_type,
                    callreceived_date: $scope.txtcallreceived_date,
                    caller_name: $scope.txtcaller_name,
                    internalreference_name: internalreference_Name,
                    internalreference_gid: $scope.cbointernalreference,
                    callerassociate_company: $scope.txtcallerassociate_company,
                    office_landlineno: $scope.txtoffice_landlineno,
                    calltype_name: calltype_Name,
                    calltype_gid: $scope.cbocalltype,
                    function_name: function_Name,
                    function_gid: $scope.cbofunction,
                    function_remarks: $scope.txtfunction_remarks,
                    requirement: $scope.txtrequirement,
                    enquiry_description: $scope.txtenquiry_description,
                    callclosure_status: $scope.cbocallclosure_status,
                    assignemployee_name: assignemployee_Name,
                    assignemployee_gid: $scope.cboassignemployee,
                    tat_hours:$scope.txttat_hours,
                    tat_date:$scope.txttat_date,
                    tat_days:$scope.txttat_days,
                    tagemployee_list: $scope.cbotagemployee,
                    assignclosure_remarks: $scope.txtassignclosure_remarks,
                    inboundcall_gid: $scope.inboundcall_gid
                }
                var url = 'api/TeleCalling/IBCallEditUpdate';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url("app/MstTelecallingSummary");
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
                    else

                     {
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
                        longitude: $scope.txtlongitude,
                        inboundcall_gid: $location.search().lsinboundcall_gid,
                    }
                    var url = 'api/TeleCalling/PostIBCallAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_templist();
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
        $scope.address_delete = function (inboundcall2address_gid) {
            var params =
                {
                    inboundcall2address_gid: inboundcall2address_gid
                }
            var url = 'api/TeleCalling/IBCallAddressDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    address_templist();
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
        

        function address_templist() {
            var param = {
                inboundcall_gid: $scope.inboundcall_gid
            };
            var url = 'api/TeleCalling/IBCallAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.ibcalladdress_list;
            });
        }

        $scope.address_edit = function (inboundcall2address_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcalladdress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/AddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                var params = {
                    inboundcall2address_gid: inboundcall2address_gid
                }
                var url = 'api/TeleCalling/EditIBCallAddress';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboAddressType = resp.data.addresstype_gid;

                    $scope.rdbprimary_status = resp.data.primary_status;
                    $scope.txtaddressline1 = resp.data.addressline1;
                    $scope.txtaddressline2 = resp.data.addressline2;
                    $scope.txtlandmark = resp.data.landmark;
                    $scope.txtpostal_code = resp.data.postal_code;
                    $scope.txtcity = resp.data.city;
                    $scope.txttaluka = resp.data.taluka;
                    $scope.txtdistrict = resp.data.district;
                    $scope.txtstate = resp.data.state;
                    $scope.txtcountry = resp.data.country;
                    $scope.txtlatitude = resp.data.latitude;
                    $scope.txtlongitude = resp.data.longitude;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.txtcountry = "India";
                $scope.address_update = function () {
                    var address_type = $('#address_type :selected').text();

                    var params = {
                        addresstype_gid: $scope.cboAddressType,
                        addresstype_name: address_type,
                        primary_status: $scope.rdbprimary_status,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        district: $scope.txtdistrict,
                        state: $scope.txtstate,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude,
                        inboundcall2address_gid: inboundcall2address_gid,
                        inboundcall_gid: $scope.inboundcall_gid,
                    }
                    var url = 'api/TeleCalling/UpdateIBCallAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            address_templist();
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

        //Mobile Number Multiple Add
        $scope.add_mobileno = function () {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_status == undefined) || ($scope.rdbsms_to == undefined)) {
                Notify.alert('Enter Mobile Number/Select Status', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_status: $scope.rdbprimary_status,
                    whatsapp_status: $scope.rdbwhatsapp_status,
                    sms_to: $scope.rdbsms_to,
                    inboundcall_gid: $scope.inboundcall_gid,
                }
                var url = 'api/TeleCalling/PostIBCallMobileNo';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        mobileno_templist();
                        $scope.txtmobile_no = '';
                        $scope.rdbprimary_status = '';
                        $scope.rdbprimary_status == false;
                        $scope.rdbsms_to = '';
                        $scope.rdbsms_to == false;
                        $scope.rdbwhatsapp_status = '';
                        $scope.rdbwhatsapp_status == false;
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
        $scope.delete_mobileno = function (inboundcall2mobileno_gid) {
            var params = {
                inboundcall2mobileno_gid: inboundcall2mobileno_gid
            }
            var url = 'api/TeleCalling/IBCallMobileNoDelete';
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
                mobileno_templist();
            });
        }
        function mobileno_templist() {
            var param = {
                inboundcall_gid: $scope.inboundcall_gid
            };
            var url = 'api/TeleCalling/IBCallMobileNoTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.ibcallmobileno_list;
            });            
        }
        $scope.edit_mobileno = function (inboundcall2mobileno_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallmobileno.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    inboundcall2mobileno_gid: inboundcall2mobileno_gid
                }
                var url = 'api/TeleCalling/EditIBCallMobileNo';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditmobile_no = resp.data.mobile_no;
                    $scope.rdbeditprimary_status = resp.data.primary_status;
                    $scope.rdbeditwhatsapp_status = resp.data.whatsapp_status;
                    $scope.rdbeditsms_to = resp.data.sms_to;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_mobileno = function () {

                    var params = {
                        mobile_no: $scope.txteditmobile_no,
                        primary_status: $scope.rdbeditprimary_status,
                        whatsapp_status: $scope.rdbeditwhatsapp_status,
                        sms_to: $scope.rdbeditsms_to,
                        inboundcall2mobileno_gid: inboundcall2mobileno_gid,
                        inboundcall_gid: $scope.inboundcall_gid,
                    }
                    var url = 'api/TeleCalling/UpdateIBCallMobileNo';
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
                        mobileno_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }


        
$scope.tatdate_change = function (txttat_date) {
    var date2 = txttat_date;
    var nowtoday = new Date();
    var yyyy = nowtoday.getFullYear();
    var mm = nowtoday.getMonth() + 1; // Months start at 0!
    var dd = nowtoday.getDate();
    var dt = date2.getDate();
    var yytt = date2.getFullYear();
    var mt = date2.getMonth() + 1; // Months start at 0!
    
    //if (dd < 10) dd = '0' + dd;
  //  if (mm < 10) mm = '0' + mm;
    
    //var formattedToday = dd + '-' + mm + '-' + yyyy;
    var diff = (date2 - nowtoday)/1000;
    var diff = Math.abs(Math.floor(diff));

    var days = Math.floor(diff/(24*60*60));
    var tatc_days; 

    if((mm == mt)&&(dd == dt)&&(yyyy == yytt))
    {
        tatc_days = days + 1; 
    }
    else{  
         tatc_days = days + 2; 
    }
    $scope.txttat_days = tatc_days;
    }
            //Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_email == undefined)) {
                Notify.alert('Enter Email Address/Select Status', 'warning');
            }
            else {
                var params = {
                    email_address: $scope.txtemail_address,
                    primary_status: $scope.rdbprimary_email,
                    inboundcall_gid: $scope.inboundcall_gid,
                }
                var url = 'api/TeleCalling/PostIBCallEmail';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        email_templist();
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
        $scope.delete_emailaddress = function (inboundcall2email_gid) {
            var params = {
                inboundcall2email_gid: inboundcall2email_gid
            }
            var url = 'api/TeleCalling/IBCallEmailDelete';
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
                email_templist();
            });
        }
        

        function email_templist() {
            var param = {
                inboundcall_gid: $scope.inboundcall_gid
            };
            var url = 'api/TeleCalling/IBCallEmailTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallemail_list = resp.data.ibcallemail_list;
            });
        }

        $scope.edit_emailaddress = function (inboundcall2email_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallemail.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    inboundcall2email_gid: inboundcall2email_gid
                }
                var url = 'api/TeleCalling/EditIBCallEmail';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditemail_address = resp.data.email_address;
                    $scope.rdbeditprimary_status = resp.data.primary_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_emailaddress = function () {

                    var params = {
                        email_address: $scope.txteditemail_address,
                        primary_status: $scope.rdbeditprimary_status,
                        inboundcall2email_gid: inboundcall2email_gid,
                        inboundcall_gid: $scope.inboundcall_gid,
                    }
                    var url = 'api/TeleCalling/UpdateIBCallEmail';
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
                        email_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        //Follow Up Multiple Add
        $scope.minDate = new Date();
        $scope.add_followup = function () {
            if (($scope.txtfollowup_date == undefined) || ($scope.txtfollowup_date == '') || ($scope.txtfollowup_time == undefined) || ($scope.txtfollowup_time == '')) {
                Notify.alert('Enter Follow Up Date/Follow Up Time', 'warning');
            }
            else {
                var params = {
                    followup_date: $scope.txtfollowup_date,
                    followup_time: $scope.txtfollowup_time,
                }
                var url = 'api/TeleCalling/PostIBCallFollowUp';
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
                    followup_templist();
                    $scope.txtfollowup_date = '';
                    $scope.txtfollowup_time = '';
                });
            }
        }
        $scope.delete_followup = function (inboundcall2followup_gid) {
            var params = {
                inboundcall2followup_gid: inboundcall2followup_gid
            }
            var url = 'api/TeleCalling/IBCallFollowUpDelete';
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
                followup_templist();
            });
        }       

        function followup_templist() {
            var param = {
                inboundcall_gid: $scope.inboundcall_gid
            };
            var url = 'api/TeleCalling/IBCallFollowUpTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.ibcallfollowup_list;
            });
        }

        $scope.edit_followup = function (inboundcall2followup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinboundcallfollowup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {                

                var params = {
                    inboundcall2followup_gid: inboundcall2followup_gid
                }
                var url = 'api/TeleCalling/EditIBCallFollowUp';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditfollowup_date = new Date(resp.data.followup_date);

                    if (resp.data.Tfollowup_time == '0001-01-01T00:00:00') {
                        $scope.txteditfollowup_time = '';
                    }
                    else {
                        $scope.txteditfollowup_time = new Date(resp.data.Tfollowup_time);
                    }

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_followup = function () {

                    var params = {
                        followup_date: $scope.txteditfollowup_date,
                        followup_time: $scope.txteditfollowup_time,
                        inboundcall2followup_gid: inboundcall2followup_gid,
                        inboundcall_gid: $scope.inboundcall_gid,
                    }
                    var url = 'api/TeleCalling/UpdateIBCallFollowUp';
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
                        followup_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

    }
})();