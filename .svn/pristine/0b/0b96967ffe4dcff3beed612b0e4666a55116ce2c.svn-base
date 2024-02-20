(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingEditController', MstMarketingEditController);

    MstMarketingEditController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function MstMarketingEditController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingEditController';
        //$scope.marketingcall_gid = $location.search().lsmarketingcall_gid;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.marketingcall_gid = searchObject.lsmarketingcall_gid;
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
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            $scope.cbointernal_reference = "NA";
            $scope.minDate = new Date();

            var today = new Date();
            var date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
          //  $scope.txtcallreceived_date = date;

            var url = 'api/Marketing/MarketingCallTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };

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
            var url = 'api/MarMstMilletRequire/GetMilletRequire';
            SocketService.get(url).then(function (resp) {
                $scope.milletrequire_list = resp.data.milletrequire_list;
            });
            var url = 'api/MarMstLeadRequire/GetLeadRequire';
            SocketService.get(url).then(function (resp) {
                $scope.leadrequire_list = resp.data.leadrequire_list;
            });
            var url = 'api/MstEnquiryRequire/GetEnquiryRequire';
            SocketService.get(url).then(function (resp) {
                $scope.enquiryrequire_list = resp.data.enquiryrequire_list;
            });
            var url = 'api/MstStartupRequire/GetStartupRequire';
            SocketService.get(url).then(function (resp) {
                $scope.startuprequire_list = resp.data.startuprequire_list;
            });
            var url = 'api/Marketing/MarketingCallMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
            });
            var url = 'api/Marketing/MarketingCallEmailList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
            });
            var url = 'api/Marketing/MarketingCallAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
            });
            var url = 'api/Marketing/MarketingCallFollowUpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.marketingcallfollowup_list;
            });
            var url = 'api/Marketing/GetDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lsfilename = resp.data.filename;
                $scope.lsfilepath = resp.data.filepath;
                $scope.document_list = resp.data.document_list;
            });
            var url = 'api/Marketing/GetMilletDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.milletdocument_list = resp.data.milletdocument_list;
            });
            var url = 'api/Marketing/GetEnquiryDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lmfilename = resp.data.filename;
                $scope.lmfilepath = resp.data.filepath;
                $scope.enquirydocument_list = resp.data.enquirydocument_list;
            });
            var url = 'api/Marketing/EditMarketingCall';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtticket_refid = resp.data.ticket_refid;
                $scope.cboentity = resp.data.entity_gid;
                $scope.cbosourceofcontact = resp.data.marketingsourceofcontact_gid;
                $scope.cboleadrequesttype = resp.data.leadrequesttype_gid;
                $scope.cbocallreceivednumber = resp.data.marketingcallreceivednumber_gid;
                $scope.cbocustomer_type = resp.data.customer_type;
                $scope.txtcallreceived_date = resp.data.callreceived_date;
                $scope.origination = resp.data.origination;
                $scope.txtcaller_name = resp.data.caller_name;
                $scope.cbointernalreference = resp.data.internalreference_gid;
                $scope.txtcallerassociate_company = resp.data.callerassociate_company;
                $scope.txtoffice_landlineno = resp.data.office_landlineno;
                $scope.txtbase_location = resp.data.baselocation_name;
                $scope.cbocalltype = resp.data.marketingcalltype_gid;
                $scope.cbofunction = resp.data.marketingfunction_gid;
                $scope.txtfunction_remarks = resp.data.function_remarks;
                $scope.txtrequirement = resp.data.requirement;
                $scope.txtenquiry_description = resp.data.enquiry_description;
                $scope.cbocallclosure_status = resp.data.callclosure_status;
                $scope.cboassignemployee = resp.data.assignemployee_gid;
                $scope.txttat_hours = resp.data.tat_hours;
                $scope.cboleadrequirename = resp.data.leadrequire_gid;
                $scope.cbomilletrequirename = resp.data.milletrequire_gid;
                $scope.cboenquiryrequirename = resp.data.enquiryrequire_gid;
                $scope.cbostartuprequirename = resp.data.startuprequire_gid;
                $scope.txtbusiness_name = resp.data.business_name;
                $scope.emp_list = resp.data.emp_list;
                if (resp.data.tagemployee_list != null) {
                    var count = resp.data.tagemployee_list.length;
                    for (var i = 0; i < count; i++) {
                        var workerIndex = $scope.emp_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.tagemployee_list[i].employee_gid);
                        //var indexs = $scope.emp_list.findIndex(x => x.employee_gid === resp.data.tagemployee_list[i].employee_gid);
                        $scope.cbotagemployee.push($scope.emp_list[workerIndex]);
                        $scope.$parent.cbotagemployee = $scope.cbotagemployee;
                    }
                }






                $scope.txtassignclosure_remarks = resp.data.assignclosure_remarks;

               

                if (resp.data.marketingcall_status == "Incomplete") {
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

        $scope.changefunctionstatus = function (Marketingfunction_name) {
            if ($('#function :selected').text() == 'Others') {
                $scope.function_show = true;
            }
            else {
                $scope.function_show = false;
            }
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
        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
            Notify.alert("View is not supported for this format..!", {
             status: 'danger',
             pos: 'top-center',
             timeout: 3000
             });
             unlockUI();
           return false;
             }
             DownloaddocumentService.DocumentViewer(val1, val2);
             }

             $scope.documentviewermillet = function (val1, val2) {
                lockUI();
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                 status: 'danger',
                 pos: 'top-center',
                 timeout: 3000
                 });
                 unlockUI();
               return false;
                 }
                 DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.documentviewerenquiry = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
        $scope.back = function () {
            $location.url('app/MstMarketingSummary');
        }
        $scope.save = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var leadrequesttype = $('#leadrequesttype :selected').text();
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
                marketingsourceofcontact_name: sourceofcontact_Name,
                marketingsourceofcontact_gid: $scope.cbosourceofcontact,
                marketingcallreceivednumber_name: callreceivednumber_Name,
                marketingcallreceivednumber_gid: $scope.cbocallreceivednumber,
                leadrequesttype_name: leadrequesttype,
                leadrequesttype_gid: $scope.cboleadrequesttype,
                customer_type: $scope.cbocustomer_type,
                callreceived_date: $scope.txtcallreceived_date,
                caller_name: $scope.txtcaller_name,
                internalreference_name: internalreference_Name,
                internalreference_gid: $scope.cbointernalreference,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno: $scope.txtoffice_landlineno,
                marketingcalltype_name: calltype_Name,
                marketingcalltype_gid: $scope.cbocalltype,
                marketingfunction_name: function_Name,
                marketingfunction_gid: $scope.cbofunction,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description: $scope.txtenquiry_description,
                callclosure_status: $scope.cbocallclosure_status,
                assignemployee_name: assignemployee_Name,
                assignemployee_gid: $scope.cboassignemployee,
                tat_hours: $scope.txttat_hours,
                baselocation_name: $scope.txtbase_location,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks,
                marketingcall_gid: $scope.marketingcall_gid
            }
            var url = 'api/Marketing/MarketingCallEditSave';
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

        $scope.submit = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var callreceivednumber_Name = $('#callreceivednumber :selected').text();
            var leadrequesttype = $('#leadrequesttype :selected').text();
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
                marketingsourceofcontact_name: sourceofcontact_Name,
                marketingsourceofcontact_gid: $scope.cbosourceofcontact,
                marketingcallreceivednumber_name: callreceivednumber_Name,
                marketingcallreceivednumber_gid: $scope.cbocallreceivednumber,
                leadrequesttype_name: leadrequesttype,
                leadrequesttype_gid: $scope.cboleadrequesttype,
                customer_type: $scope.cbocustomer_type,
                callreceived_date: $scope.txtcallreceived_date,
                caller_name: $scope.txtcaller_name,
                internalreference_name: internalreference_Name,
                internalreference_gid: $scope.cbointernalreference,
                callerassociate_company: $scope.txtcallerassociate_company,
                office_landlineno: $scope.txtoffice_landlineno,
                marketingcalltype_name: calltype_Name,
                marketingcalltype_gid: $scope.cbocalltype,
                marketingfunction_name: function_Name,
                marketingfunction_gid: $scope.cbofunction,
                function_remarks: $scope.txtfunction_remarks,
                requirement: $scope.txtrequirement,
                enquiry_description: $scope.txtenquiry_description,
                callclosure_status: $scope.cbocallclosure_status,
                assignemployee_name: assignemployee_Name,
                assignemployee_gid: $scope.cboassignemployee,
                tat_hours: $scope.txttat_hours,
                baselocation_name: $scope.txtbase_location,
                tagemployee_list: $scope.cbotagemployee,
                assignclosure_remarks: $scope.txtassignclosure_remarks,
                marketingcall_gid: $scope.marketingcall_gid
            }
            var url = 'api/Marketing/MarketingCallEditSubmit';
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

        $scope.update = function () {

            var entity_Name = $('#entity :selected').text();
            var sourceofcontact_Name = $('#sourceofcontact :selected').text();
            var callreceivednumber_Name = $('#callreceivednumber :selected').text();
            var leadrequesttypename = $('#leadrequesttype :selected').text();
            var internalreference_Name = $('#internalreference :selected').text();
            var calltype_Name = $('#calltype :selected').text();
            var function_Name = $('#function :selected').text();
            var assignemployee_Name = $('#assignemployee :selected').text();
            var leadrequire_name = $('#leadrequire :selected').text();
            var milletrequire_name = $('#milletrequire :selected').text();
            var enquiryrequire_name = $('#enquiryrequire_name :selected').text();
            var startuprequire_name = $('#startuprequire_name :selected').text();

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
            //else if (($scope.cbocallclosure_status == 'Assign') && ($scope.txttat_hours == null || $scope.txttat_hours == '')) {
            //    Notify.alert('Kindly Select Assign TAT Hours', 'warning')
            //}
            
            if ((($scope.cbocallclosure_status == 'Assign')||($scope.cbocallclosure_status == 'Follow Up')) && ($scope.txtassignclosure_remarks == null || $scope.txtassignclosure_remarks == '')) {
                Notify.alert('Kindly Enter Remark', 'warning')
            }
            else if (($scope.cbocallclosure_status == 'Rejected') && ($scope.txtclosure_remarks == null || $scope.txtclosure_remarks == '')) {
                Notify.alert('Kindly Enter Remark', 'warning')
            }
           
            else {
                var params = {
                    entity_name: entity_Name,
                    entity_gid: $scope.cboentity,
                    marketingsourceofcontact_name: sourceofcontact_Name,
                    marketingsourceofcontact_gid: $scope.cbosourceofcontact,
                    marketingcallreceivednumber_name: callreceivednumber_Name,
                    marketingcallreceivednumber_gid: $scope.cbocallreceivednumber,
                    leadrequesttype_name: leadrequesttypename,
                    leadrequesttype_gid: $scope.cboleadrequesttype,
                    customer_type: $scope.cbocustomer_type,
                    callreceived_date: $scope.txtcallreceived_date,
                    caller_name: $scope.txtcaller_name,
                    internalreference_name: internalreference_Name,
                    internalreference_gid: $scope.cbointernalreference,
                    callerassociate_company: $scope.txtcallerassociate_company,
                    office_landlineno: $scope.txtoffice_landlineno,
                    marketingcalltype_name: calltype_Name,
                    marketingcalltype_gid: $scope.cbocalltype,
                    marketingfunction_name: function_Name,
                    marketingfunction_gid: $scope.cbofunction,
                    function_remarks: $scope.txtfunction_remarks,
                    requirement: $scope.txtrequirement,
                    enquiry_description: $scope.txtenquiry_description,
                    callclosure_status: $scope.cbocallclosure_status,
                    assignemployee_name: assignemployee_Name,
                    assignemployee_gid: $scope.cboassignemployee,
                    tat_hours: $scope.txttat_hours,
                    baselocation_name: $scope.txtbase_location,
                    tagemployee_list: $scope.cbotagemployee,
                    assignclosure_remarks: $scope.txtassignclosure_remarks,
                    closed_remarks: $scope.txtclosure_remarks,
                    leadrequire_name: leadrequire_name,
                    leadrequire_gid: $scope.cboleadrequirename,
                    milletrequire_name: milletrequire_name,
                    milletrequire_gid: $scope.cbomilletrequirename,
                    milletrequire_name: milletrequire_name,
                    milletrequire_gid: $scope.cbomilletrequirename,
                    enquiryrequire_name: enquiryrequire_name,
                    enquiryrequire_gid: $scope.cboenquiryrequirename,
                    startuprequire_name: startuprequire_name,
                    startuprequire_gid: $scope.cbostartuprequirename,
                    business_name: $scope.txtbusiness_name,
                    industry_name: $scope.txtindustry_name,
                    marketingcall_gid: $scope.marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallEditUpdate';
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
        function address_list() {
            var url = 'api/Marketing/GetMarketingCallAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
            });
        }
        $scope.address_delete = function (marketingcall2address_gid) {
            var params =
                {
                    marketingcall2address_gid: marketingcall2address_gid
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
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcalladdress_list = resp.data.MarketingCalladdress_list;
            });
        }

        $scope.address_edit = function (marketingcall2address_gid) {
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
                var url = 'api/Marketing/EditMarketingCallAddress';
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
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallAddress';
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
                    sms_to: $scope.rdbsms_to,
                    marketingcall_gid: $scope.marketingcall_gid,
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
                mobileno_templist();
            });
        }
        function mobileno_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallMobileNoTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallmobileno_list = resp.data.MarketingCallmobileno_list;
            });            
        }
        $scope.edit_mobileno = function (marketingcall2mobileno_gid) {
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
                    marketingcall2mobileno_gid: marketingcall2mobileno_gid
                }
                var url = 'api/Marketing/EditMarketingCallMobileNo';
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
                        marketingcall2mobileno_gid: marketingcall2mobileno_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallMobileNo';
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
        //Email Address Multiple Add
        $scope.add_emailaddress = function () {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_email == undefined) || ($scope.rdbprimary_email == '')) {
                Notify.alert('Enter Email Address/Select Status', 'warning');
            }
            else {
                var params = {
                    email_address: $scope.txtemail_address,
                    primary_status: $scope.rdbprimary_email,
                    marketingcall_gid: $scope.marketingcall_gid,
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
                email_templist();
            });
        }
        

        function email_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallEmailTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallemail_list = resp.data.MarketingCallemail_list;
            });
        }

        $scope.edit_emailaddress = function (marketingcall2email_gid) {
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
                    marketingcall2email_gid: marketingcall2email_gid
                }
                var url = 'api/Marketing/EditMarketingCallEmail';
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
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallEmail';
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
                    followup_templist();
                    $scope.txtfollowup_date = '';
                    $scope.txtfollowup_time = '';
                });
            }
        }
        $scope.delete_followup = function (marketingcall2followup_gid) {
            var params = {
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
                followup_templist();
            });
        }       

        function followup_templist() {
            var param = {
                marketingcall_gid: $scope.marketingcall_gid
            };
            var url = 'api/Marketing/MarketingCallFollowUpTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.ibcallfollowup_list = resp.data.MarketingCallfollowup_list;
            });
        }

        $scope.edit_followup = function (marketingcall2followup_gid) {
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
                    marketingcall2followup_gid: marketingcall2followup_gid
                }
                var url = 'api/Marketing/EditMarketingCallFollowUp';
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
                        marketingcall2followup_gid: marketingcall2followup_gid,
                        marketingcall_gid: $scope.marketingcall_gid,
                    }
                    var url = 'api/Marketing/UpdateMarketingCallFollowUp';
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
        $scope.download_all = function (val1, val2) {

            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }

        }

        $scope.document_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allmillet = function (val1,val2) {
            for (var i = 0; i < val2.length; i++) {
               //  console.log(array[i]);
               DownloaddocumentService.Downloaddocument(val1, val2[i]);
           }
       }        
       $scope.milletdocument_downloads = function (val1,val2) {
           DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.download_allenquiry = function (val1, val2) {
            for (var i = 0; i < val2.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument(val1, val2[i]);
            }
        }
        $scope.enquirydocument_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
    }
})();