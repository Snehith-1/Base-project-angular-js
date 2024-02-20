(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstInboundAddController', MstInboundAddController);

        MstInboundAddController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function MstInboundAddController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstInboundAddController';
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
            var url = 'api/TeleCalling/IBCallTempClear';
            SocketService.get(url).then(function (resp) {
            });
           
            var url = 'api/TeleCalling/GetIBCallMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.ibcallmobileno_list;
            });

            var url = 'api/TeleCalling/GetIBCallEmailList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallemail_list = resp.data.ibcallemail_list;
            });
            var url = 'api/TeleCalling/GetIBCallAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.ibcalladdress_list;
            });
          
       
          

            $scope.cbointernal_reference="NA";
     //       

     var today = new Date();
     var yyyy = today.getFullYear();
     var mm = today.getMonth() + 1; // Months start at 0!
     var dd = today.getDate();
     
     if (dd < 10) dd = '0' + dd;
     if (mm < 10) mm = '0' + mm;
     
     var formattedToday = dd + '-' + mm + '-' + yyyy;
     
//              
          
         //   var today = new Date();
          //  var date =  0+today.getDate()+'-'+(today.getMonth()+1)+'-'+today.getFullYear();
            $scope.txtcallreceived_date = formattedToday;

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
        }

        $scope.changefunctionstatus = function (telecallingfunction_name) {
            if ($('#function :selected').text() == 'Others') {
                $scope.function_show = true;
            }
            else {
                $scope.function_show = false;
            }
        }
//tatdate_change(txttat_days)

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

//




//
        $scope.back = function(){
            $location.url('app/MstTelecallingSummary');
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
                entity_name:lsentity_name,
                entity_gid:lsentity_gid,
                sourceofcontact_name:lssourceofcontact_name,
                sourceofcontact_gid:lssourceofcontact_gid,
                callreceivednumber_name:lscallreceivednumber_name,
                callreceivednumber_gid:lscallreceivednumber_gid,
                customer_type:$scope.cbocustomertype,
                callreceived_date:$scope.txtcallreceived_date,
                caller_name: $scope.txtcaller_name,
                internalreference_name: lsinternalreference_name,
                internalreference_gid: lsinternalreference_gid,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno : $scope.txtoffice_landlineno,
                calltype_name:lscalltype_name,
                calltype_gid:lscalltype_gid,
                function_name:lsfunction_name,
                function_gid: lsfunction_gid,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description:$scope.txtenquiry_description,
                callclosure_status:$scope.cbocallclosure_status,
                assignemployee_name:lsassignemployee_name,
                assignemployee_gid: lsassignemployee_gid,
                tat_hours: $scope.txttat_days,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks
            }
            var url = 'api/TeleCalling/IBCallSave';
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
             
        $scope.submit = function(){
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
            else if (($scope.cbocallclosure_status == 'Assign') && ($scope.txttat_hours == null || $scope.txttat_hours == '')) {
                Notify.alert('Kindly Select Assign TAT Hours', 'warning')
            }
            else if (($('#function :selected').text() == 'Others') && ($scope.txtfunction_remarks == null || $scope.txtfunction_remarks == '')) {
                Notify.alert('Kindly Select Function Remarks', 'warning')
            }
            else {
                var params = {
                    entity_name: lsentity_name,
                    entity_gid: lsentity_gid,
                    sourceofcontact_name: lssourceofcontact_name,
                    sourceofcontact_gid: lssourceofcontact_gid,
                    callreceivednumber_name: lscallreceivednumber_name,
                    callreceivednumber_gid: lscallreceivednumber_gid,
                    customer_type: $scope.cbocustomertype,
                    callreceived_date: $scope.txtcallreceived_date,
                    caller_name: $scope.txtcaller_name,
                    internalreference_name: lsinternalreference_name,
                    internalreference_gid: lsinternalreference_gid,
                    callerassociate_company: $scope.txtcallerassociate_company,
                    office_landlineno: $scope.txtoffice_landlineno,
                    calltype_name: lscalltype_name,
                    calltype_gid: lscalltype_gid,
                    function_name: lsfunction_name,
                    function_gid: lsfunction_gid,
                    function_remarks: $scope.txtfunction_remarks,
                    requirement: $scope.txtrequirement,
                    enquiry_description: $scope.txtenquiry_description,
                    callclosure_status: $scope.cbocallclosure_status,
                    assignemployee_name: lsassignemployee_name,
                    assignemployee_gid: lsassignemployee_gid,
					tat_hours: $scope.txttat_hours,
                    tat_date: $scope.txttat_date,
                    tat_days:$scope.txttat_days,
                    tagemployee_list: $scope.cbotagemployee,
                    assignclosure_remarks: $scope.txtassignclosure_remarks
                }
                var url = 'api/TeleCalling/IBCallSubmit';
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
                        longitude: $scope.txtlongitude
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
            var url = 'api/TeleCalling/GetIBCallAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.ibcalladdress_list;
            });
        }

        //Mobile Number Multiple Add
        $scope.add_mobileno = function () {
           var mobile_no= $scope.txtmobile_no
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_status == undefined) || ($scope.rdbsms_to == undefined)) {
                Notify.alert('Enter Mobile Number/Select Status', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_status: $scope.rdbprimary_status,
                    whatsapp_status: $scope.rdbwhatsapp_status,
                    sms_to: $scope.rdbsms_to
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
                mobileno_list();
            });
        }
        function mobileno_list() {
            var url = 'api/TeleCalling/GetIBCallMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.ibcallmobileno_list;
            });
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
                emailaddress_list();
            });
        }
        function emailaddress_list() {
            var url = 'api/TeleCalling/GetIBCallEmailList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallemail_list = resp.data.ibcallemail_list;
            });
        }
//Follow Up Multiple Add

       $scope.minDate = new Date();


        $scope.add_followup = function () {
            if (($scope.txtfollowup_date == undefined) || ($scope.txtfollowup_date == '') || ($scope.txtfollowup_time == undefined) || ($scope.txtfollowup_time == '')) {
                Notify.alert('Enter Follow Up Date/Follow Up Time','warning');
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
                    followup_list();
                   
                    $scope.txtfollowup_time = '';
                }); 
            }
        }
        $scope.delete_followup = function (inboundcall2followup_gid) {
            var params ={
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
                followup_list();
            });
        }
        function followup_list() {
            var url = 'api/TeleCalling/GetIBCallFollowUpList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.ibcallfollowup_list;
            }); 
        }
    }
})();