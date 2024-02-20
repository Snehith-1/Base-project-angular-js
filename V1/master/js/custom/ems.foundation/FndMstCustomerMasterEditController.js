(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerMasterEditController', FndMstCustomerMasterEditController);

    FndMstCustomerMasterEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndMstCustomerMasterEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerMasterEditController';
        
        $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid;
        $scope.fndmanagement2cheque_gid = cmnfunctionService.decryptURL($location.search().hash).lsfndmanagement2cheque_gid;
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
            vm.open2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened2 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            $scope.pastdatecheck = function (val) {
                var params = {
                    date: val.toDateString()
                }
                var url = 'api/FndTrnMyCampaignSummary/PastDateCheck';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == false) {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }

            var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
                $scope.employee_gid = resp.data.employee_gid;

            });

            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;
            });



            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customermobileno_list = resp.data.mobileno_list;
            });




            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/customerEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customeremailaddress_list = resp.data.email_list;

            });



            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/customerAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customeraddress_list = resp.data.customeraddress_list;
            });
            //var param = {
            //    customer_gid: $scope.customer_gid

            //};
            //var url = 'api/FndMstCustomerMasterAdd/customerEmailAddressList';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.email_list = resp.data.email_list;
            //});

            //var param = {
            //    customer_gid: $scope.customer_gid

            //};
            //var url = 'api/FndMstCustomerMasterAdd/GetMobileNoList';
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.customermobileno_list = resp.data.customermobileno_list;
            //});
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getconstitution';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getassessmentagency';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assessmentagency_list = resp.data.assessmentagency_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getassessmentagencyrating';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getamlcategory';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.amlcategory_list = resp.data.amlcategory_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/Getbusinesscategory';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.businesscategory_list = resp.data.businesscategory_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };

            var url = 'api/FndMstCustomerMasterAdd/Getdesignation';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });
            var param = {
                customer_gid: $scope.customer_gid

            };

            var url = 'api/FndMstCustomerMasterAdd/Getindividualproof';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.individualproof_list = resp.data.individualproof_list;
            });


            var param = {
                customer_gid: $scope.customer_gid

            };
            var url = 'api/FndMstCustomerMasterAdd/state';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });


            var params = {
                customer_gid: $scope.customer_gid
            }
            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummaryView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });
            var url = 'api/FndMstCustomerMasterAdd/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            var params = {
                customer_gid: $scope.customer_gid
            }

            var url = 'api/FndMstCustomerMasterAdd/customerDetailsEdit';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtcustomer_code = resp.data.customer_code;
                $scope.txtcustomer_name = resp.data.customer_name;
                $scope.txtbusinessstart_date = resp.data.businessstart_date;
                $scope.txtyear_business = resp.data.year_business;
                $scope.txtmonth_business = resp.data.month_business;
                $scope.cboconstitution = resp.data.constitution_gid;
                $scope.cboassessmentagency = resp.data.assessmentagency_gid;
                $scope.cboassessmentagencyrating = resp.data.assessmentagencyrating_gid;
                $scope.rating_date = resp.data.rating_date;
                $scope.cboamlcategory = resp.data.amlcategory_gid;
                $scope.cin_no = resp.data.cin_no;
                $scope.txtpan_no = resp.data.pan_no;
                $scope.cbobusinesscategory = resp.data.businesscategory_gid;
                $scope.remarks = resp.data.remarks;
                $scope.status_remarks = resp.data.status_remarks;
                $scope.dependency_name = resp.data.msme_registration;
                $scope.txtcontactperson_fn = resp.data.contactperson_fn;
                $scope.txtcontactperson_mn = resp.data.contactperson_mn;
                $scope.txtcontactperson_ln = resp.data.contactperson_ln;
                $scope.cboindividualproof = resp.data.individualproof_gid;
                $scope.cbodesignation = resp.data.designation_gid;
                $scope.newDependency = resp.data.msme_radio;

                if ($scope.newDependency == 'Yes') {
                    $scope.new_dependency = true;
                    $scope.new_row = true;
                }
                else {
                    $scope.new_dependency = false;
                    $scope.new_row = false;
                }

                unlockUI();
            });

            var url = 'api/FndMstCustomerMasterAdd/GetCustomerRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.customerraisequery_list = resp.data.customerraisequery_list;
            });

            //var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            //SocketService.get(url).then(function (resp) {
            //    $scope.cheque_list = resp.data.cheque_list;
            //});

        }

        $scope.customer_save = function () {
            var lsconstitution_gid = '';
            var lsconstitution_name = '';
            var lsassessmentagency_gid = '';
            var lsassessmentagency_name = '';
            var lsassessmentagencyrating_gid = '';
            var lsassessmentagencyrating_name = '';
            var lsamlcategory_gid = '';
            var lsamlcategory_name = '';
            var lsbusinesscategory_gid = '';
            var lsbusinesscategory_name = '';
            var lsdesignation_gid = '';
            var lsdesignation_type = '';
            var lsindividualproof_gid = '';
            var lsindividualproof_name = '';

            if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
                lsconstitution_gid = $scope.cboConstitution.constitution_gid;
                lsconstitution_name = $scope.cboConstitution.constitution_name;
            }
            if ($scope.cboassessmentagency != undefined || $scope.cboassessmentagency != null) {
                lsassessmentagency_gid = $scope.cboassessmentagency.assessmentagency_gid;
                lsassessmentagency_name = $scope.cboassessmentagency.assessmentagency_name;
            }
            if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
                lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
                lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            }
            if ($scope.cboamlcategory != undefined || $scope.cboamlcategory != null) {
                lsamlcategory_gid = $scope.cboamlcategory.amlcategory_gid;
                lsamlcategory_name = $scope.cboamlcategory.amlcategory_name;
            }
            if ($scope.cbobusinesscategory != undefined || $scope.cbobusinesscategory != null) {
                lsbusinesscategory_gid = $scope.cbobusinesscategory.businesscategory_gid;
                lsbusinesscategory_name = $scope.cbobusinesscategory.businesscategory_name;
            }
            if ($scope.cbodesignation != undefined || $scope.cbodesignation != null) {
                lsdesignation_gid = $scope.cbodesignation.designation_gid;
                lsdesignation_type = $scope.cbodesignation.designation_type;
            }
            if ($scope.cboindividualproof != undefined || $scope.cboindividualproof != null) {
                lsindividualproof_gid = $scope.cboindividualproof.individualproof_gid;
                lsindividualproof_name = $scope.cboindividualproof.individualproof_name;
            }



            var params = {
                customer_name: $scope.txtcustomer_name,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                //constitution_gid: $scope.cboConstitution.constitution_gid,
                //constitution_name: $scope.cboConstitution.constitution_name,
                constitution_gid: lsconstitution_gid,
                constitution_name: lsconstitution_name,
                assessmentagency_gid: lsassessmentagency_gid,
                assessmentagency_name: lsassessmentagency_name,
                assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                assessmentagencyrating_name: lsassessmentagencyrating_name,
                amlcategory_gid: lsamlcategory_gid,
                amlcategory_name: lsamlcategory_name,
                //assessmentagency_gid: $scope.cboassessmentagency.assessmentagency_gid,
                //assessmentagency_name: $scope.cboassessmentagency.assessmentagency_name,
                //assessmentagencyrating_gid: $scope.cboassessmentagencyrating.assessmentagencyrating_gid,
                //assessmentagencyrating_name: $scope.cboassessmentagencyrating.assessmentagencyrating_name,
                rating_date: $scope.txtrating_date,
                //amlcategory_gid: $scope.cboamlcategory.amlcategory_gid,
                //amlcategory_name: $scope.cboamlcategory.amlcategory_name,
                //businesscategory_gid: $scope.cbobusinesscategory.businesscategory_gid,
                //businesscategory_name: $scope.cbobusinesscategory.businesscategory_name,
                businesscategory_gid: lsbusinesscategory_gid,
                businesscategory_name: lsbusinesscategory_name,
                //designation_gid: $scope.cbodesignation.designation_gid,
                //designation_type: $scope.cbodesignation.designation_type,
                designation_gid: lsdesignation_gid,
                designation_type: lsdesignation_type,
                //individualproof_gid: $scope.cboindividualproof.individualproof_gid,
                //individualproof_name: $scope.cboindividualproof.individualproof_name,
                individualproof_gid: lsindividualproof_gid,
                individualproof_name: lsindividualproof_name,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                remarks: $scope.txtaddremarks,
                msme_radio: $scope.newDependency,
                msme_registration: $scope.txtdependency_name

            }
            var url = 'api/FndMstCustomerMasterAdd/customerSave';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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
        $scope.add_cheque = function () {
            //$location.url('app/FndMstcustomerAddCheque');

            $location.url('app/FndMstcustomerAddCheque?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + $scope.customer_gid + '&lsfndmanagement2cheque_gid=' + $scope.fndmanagement2cheque_gid + '&lstab=edit'));

        }

        var params = {
            customer_gid: $scope.customer_gid
        }
        var url = 'api/FndMstCustomerMasterAdd/ChequeDetailsEdit';
        SocketService.getparams(url, params).then(function (resp) {

            $scope.accountholder_name = resp.data.accountholder_name;
            $scope.account_number = resp.data.account_number;
            $scope.bank_name = resp.data.bank_name;
            $scope.cheque_no = resp.data.cheque_no;
            $scope.ifsc_code = resp.data.ifsc_code;
            $scope.micr = resp.data.micr;
            $scope.branch_address = resp.data.branch_address;
            $scope.branch_name = resp.data.branch_name;
            $scope.city = resp.data.city;
            $scope.district = resp.data.district;
            $scope.state = resp.data.state;

        });

        $scope.onchangebusinessstartdate = function (val) {

            var params = {
                date: val.toDateString()
            }
            var url = 'api/FndTrnMyCampaignSummary/FutureDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    $scope.txtbusinessstart_date = '';
                    Notify.alert(resp.data.message, 'warning')
                }
               
            });

            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            console.log(params);
            var url = 'api/FndMstCustomerMasterAdd/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyear_business = resp.data.year_business;
                $scope.txtmonth_business = resp.data.month_business;
            });
        }
        // For MSME
        $scope.onselectedDep_yes = function () {
            if ($scope.newDependency == 'Yes') {
                $scope.new_dependency = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dependency = false;
                //$scope.dependency_name = '';
                $scope.new_row = false;
            }

            if ($scope.newdll == 'Yes') {
                $scope.new_dll = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dll = false;
                $scope.new_row = false;
            }

        }
        $scope.getcustomerbasedGST = function () {

            var param = {
                pan: $scope.txtpan_no
            }
            var url = 'api/Kyc/PANNumber';
            lockUI();
            SocketService.post(url, param).then(function (resp) {
                unlockUI();
                if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                    $scope.panvalidation = true;
                    $scope.txtcustomer_name = resp.data.result.name;
                } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                    $scope.panvalidation = false;
                    Notify.alert('PAN is not verified..!', 'warning');
                } else {
                    Notify.alert(resp.data.message, 'warning')
                }

            });


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

                        var url = 'api/FndMstCustomerMasterAdd/PostGSTList';
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

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
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
                var url = 'api/FndMstCustomerMasterAdd/PostMobileNo';
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
                    mobileno_list();
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbwhatsappmobile_no = '';
                    $scope.rdbprimarymobile_no == false;
                });
            }
        }


        function mobileno_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.customermobileno_list = resp.data.customermobileno_list;

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
                var url = 'api/FndMstCustomerMasterAdd/PostGST';
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
                            status: 'warning',
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

        $scope.gst_delete = function (customer2gst_gid) {
            var params =
                {
                    customer2gst_gid: customer2gst_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteGST';
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

                gst_list();
            });

        }

        function gst_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;

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
                    if ($scope.txtpostal_code.length == 6) {
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
                    var url = 'api/FndMstCustomerMasterAdd/PostAddress';
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

        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_no;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/FndMstCustomerMasterAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }
        //$scope.address_edit = function (customer2address_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editaddressdetails.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'lg'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {
        //        $scope.geocodingFailed = false;

        //        var url = 'api/AddressType/GetAddressTypeASC';
        //        SocketService.get(url).then(function (resp) {
        //            $scope.addresstype_list = resp.data.addresstype_list;
        //        });

        //        var params = {
        //            customer2address_gid: customer2address_gid
        //        }
        //        var url = 'api/MstCreditStatusAdd/AddressDetailEdit';
        //        SocketService.getparams(url, params).then(function (resp) {

        //            $scope.cboaddresstype = resp.data.address_typegid;
        //            $scope.rdbprimaryaddress = resp.data.primary_address;
        //            $scope.txtaddressline1 = resp.data.addressline1;
        //            $scope.txtaddressline2 = resp.data.addressline2;
        //            $scope.txtlandmark = resp.data.landmark;
        //            $scope.txtpostal_code = resp.data.postal_code;
        //            $scope.txtcity = resp.data.city;
        //            $scope.txttaluka = resp.data.taluka;
        //            $scope.txtdistrict = resp.data.district;
        //            $scope.txtstate = resp.data.state;
        //            $scope.txtcountry = resp.data.country;
        //            $scope.txtlatitude = resp.data.latitude;
        //            $scope.txtlongitude = resp.data.longitude;
        //            $scope.customer_gid = resp.data.customer_gid;
        //            $scope.customer2address_gid = resp.data.customer2address_gid;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };
        //        $scope.onchangepostal_code = function () {
        //            var params = {
        //                postal_code: $scope.txtpostal_code
        //            }
        //            var url = 'api/Mstcustomer/GetPostalCodeDetails';

        //            SocketService.getparams(url, params).then(function (resp) {
        //                $scope.txtcity = resp.data.city;
        //                $scope.txttaluka = resp.data.taluka;
        //                $scope.txtdistrict = resp.data.district;
        //                $scope.txtstate = resp.data.state_name;
        //            });
        //        }

        //        $scope.getGeoCoding = function () {
        //            if ($scope.txtpostal_code.length == 6) {
        //                if ($scope.txtaddressline2 == undefined) {
        //                    var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
        //                } else {
        //                    var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
        //                }
        //                var params = {
        //                    address: addressString
        //                }
        //                var url = 'api/GoogleMapsAPI/GetGeoCoding';
        //                SocketService.getparams(url, params).then(function (resp) {
        //                    if (resp.data.status == "OK") {
        //                        $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
        //                        $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
        //                        $scope.geocodingFailed = false;
        //                    }
        //                    else if (resp.data.status == "ZERO_RESULTS") {
        //                        $scope.geocodingFailed = true;
        //                    }
        //                });
        //            }
        //        }

        //        $scope.txtcountry = "India";
        //        $scope.addressUpdate = function () {
        //            var address_type = $('#address_type :selected').text();

        //            var params = {
        //                address_typegid: $scope.cboaddresstype,
        //                address_type: address_type,
        //                addressline1: $scope.txtaddressline1,
        //                addressline2: $scope.txtaddressline2,
        //                primary_address: $scope.rdbprimaryaddress,
        //                landmark: $scope.txtlandmark,
        //                postal_code: $scope.txtpostal_code,
        //                taluka: $scope.txttaluka,
        //                city: $scope.txtcity,
        //                state: $scope.txtstate,
        //                district: $scope.txtdistrict,
        //                country: $scope.txtcountry,
        //                latitude: $scope.txtlatitude,
        //                longitude: $scope.txtlongitude,
        //                customer2address_gid: $scope.customer2address_gid,
        //                customer_gid: localStorage.getItem('customer_gid'),
        //            }
        //            var url = 'api/MstCreditStatusAdd/AddressDetailUpdate';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                address_templist();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //    }
        //}

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

                        var url = 'api/FndMstCustomerMasterAdd/PostGSTList';
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
        $scope.gst_edit = function (customer2gst_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editgstdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/customer/state';
                SocketService.get(url).then(function (resp) {
                    $scope.state_list = resp.data.state_list;
                });

                var params = {
                    customer2gst_gid: customer2gst_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/GSTEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditgst_state = resp.data.gststate_name;
                    $scope.txteditgst_number = resp.data.gst_no;
                    $scope.rdbgstregistered = resp.data.gstregister_status;
                    $scope.customer_gid = resp.data.customer_gid;
                    $scope.customer2gst_gid = resp.data.customer2gst_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangeeditgst_number = function () {
                    var gst_number = $scope.txteditgst_number;
                    var params = {
                        gst_code: gst_number.substring(0, 2)
                    }
                    var url = 'api/FndMstCustomerMasterAdd/GetGSTState';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txteditgst_state = resp.data.gst_state;
                    });
                }

                $scope.update_gst = function () {

                    var params = {
                        gststate_name: $scope.txteditgst_state,
                        gst_no: $scope.txteditgst_number,
                        gstregister_status: $scope.rdbgstregistered,
                        customer_gid: localStorage.getItem('customer_gid'),
                        customer2gst_gid: $scope.customer2gst_gid,
                    }
                    var url = 'api/FndMstCustomerMasterAdd/GSTUpdate';
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
                        gst_templist();
                        $scope.txtgst_no = '';

                    });

                    $modalInstance.close('closed');
                }
            }
        }
        $scope.emailaddress_edit = function (customer2emailaddress_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editemailaddress.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer2emailaddress_gid: customer2emailaddress_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/EmailAddressEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditemail_address = resp.data.email_address;
                    $scope.rdbeditprimary_emailaddress = resp.data.primary_emailaddress;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_emailaddress = function () {

                    var params = {
                        email_address: $scope.txteditemail_address,
                        primary_emailaddress: $scope.rdbeditprimary_emailaddress,
                        customer2emailaddress_gid: customer2emailaddress_gid,
                        customer_gid: localStorage.getItem('customer_gid'),
                    }
                    var url = 'api/FndMstCustomerMasterAdd/EmailAddressUpdate';
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
                        emailaddress_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.mobileno_edit = function (customer2mobileno_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editmobileno.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer2mobileno_gid: customer2mobileno_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/MobileNoEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditmobile_no = resp.data.mobile_no;
                    $scope.rdbeditprimarymobile_no = resp.data.primary_mobileno;
                    $scope.rdbeditwhatsappmobile_no = resp.data.whatsapp_mobileno;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_mobileno = function () {

                    var params = {
                        mobile_no: $scope.txteditmobile_no,
                        primary_mobileno: $scope.rdbeditprimarymobile_no,
                        whatsapp_mobileno: $scope.rdbeditwhatsappmobile_no,
                        customer2mobileno_gid: customer2mobileno_gid,
                        customer_gid: localStorage.getItem('customer_gid'),

                    }
                    var url = 'api/FndMstCustomerMasterAdd/MobileNoUpdate';
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

        $scope.emailaddress_delete = function (customer2emailaddress_gid) {
            var params =
                {
                    customer2emailaddress_gid: customer2emailaddress_gid
                }
            var url = 'api/FndMstCustomerMasterAdd/DeleteEmailAddress';
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

                emailaddress_templist();
            });

        }

        $scope.Back = function () {
            //customerupdate();
            $state.go('app.FndMstCustomerMaster');
        }

        $scope.edit_Submit = function () {
            $state.go('app.FndMstCustomerMaster');
        }
        $scope.viewcustomer = function (customer_gid, fndmanagement2cheque_gid) {
            //localStorage.setItem('customer_gid', val);
            //$state.go('app.FndMstCustomerMasterView');
            $location.url('app/FndMstCustomerChequeView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + customer_gid + '&lsfndmanagement2cheque_gid=' + fndmanagement2cheque_gid + '&lstab=edit'));
        }
        $scope.editCheque = function (udcmanagement2cheque_gid) {
            $location.url('app/FndMstcustomerEditCheque?hash=' + cmnfunctionService.encryptURL('lsfndmanagement2cheque_gid=' + fndmanagement2cheque_gid + '&lsfndmanagement_gid=' + $scope.fndmanagement_gid + '&lstab=edit'));
        }



        $scope.customer_submit = function () {
            //var lsconstitution_gid = '';
            //var lsconstitution_name = '';
            //var lsassessmentagency_gid = '';
            //var lsassessmentagency_name = '';
            //var lsassessmentagencyrating_gid = '';
            //var lsassessmentagencyrating_name = '';
            //var lsamlcategory_gid = '';
            //var lsamlcategory_name = '';
            //var lsbusinesscategory_gid = '';
            //var lsbusinesscategory_name = '';
            //var lsdesignation_gid = '';
            //var lsdesignation_type = '';
            //var lsindividualproof_gid = '';
            //var lsindividualproof_name = '';

            //if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
            //    lsconstitution_gid = $scope.cboConstitution.constitution_gid;
            //    lsconstitution_name = $scope.cboConstitution.constitution_name;
            //}
            //if ($scope.cboassessmentagency != undefined || $scope.cboassessmentagency != null) {
            //    lsassessmentagency_gid = $scope.cboassessmentagency.assessmentagency_gid;
            //    lsassessmentagency_name = $scope.cboassessmentagency.assessmentagency_name;
            //}
            //if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
            //    lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
            //    lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            //}
            //if ($scope.cboamlcategory != undefined || $scope.cboamlcategory != null) {
            //    lsamlcategory_gid = $scope.cboamlcategory.amlcategory_gid;
            //    lsamlcategory_name = $scope.cboamlcategory.amlcategory_name;
            //}
            //if ($scope.cbobusinesscategory != undefined || $scope.cbobusinesscategory != null) {
            //    lsbusinesscategory_gid = $scope.cbobusinesscategory.businesscategory_gid;
            //    lsbusinesscategory_name = $scope.cbobusinesscategory.businesscategory_name;
            //}

            //if ($scope.cbodesignation != undefined || $scope.cbodesignation != null) {
            //    lsdesignation_gid = $scope.cbodesignation.designation_gid;
            //    lsdesignation_type = $scope.cbodesignation.designation_type;
            //}
            //if ($scope.cboindividualproof != undefined || $scope.cboindividualproof != null) {
            //    lsindividualproof_gid = $scope.cboindividualproof.individualproof_gid;
            //    lsindividualproof_name = $scope.cboindividualproof.individualproof_name;
            //}

            var constitution_name;
            var constitution_name_index = $scope.constitution_list.map(function (e) { return e.constitution_gid }).indexOf($scope.cboconstitution);
            if (constitution_name_index == -1) { constitution_name = ''; } else { constitution_name = $scope.constitution_list[constitution_name_index].constitution_name; };

            var assessmentagency_name;
            var assessmentagency_name_index = $scope.assessmentagency_list.map(function (e) { return e.assessmentagency_gid }).indexOf($scope.cboassessmentagency);
            if (assessmentagency_name_index == -1) { assessmentagency_name = ''; } else { assessmentagency_name = $scope.assessmentagency_list[assessmentagency_name_index].assessmentagency_name; };

            var assessmentagencyrating_name;
            var assessmentagencyrating_name_index = $scope.assessmentagencyrating_list.map(function (e) { return e.assessmentagencyrating_gid }).indexOf($scope.cboassessmentagencyrating);
            if (assessmentagencyrating_name_index == -1) { assessmentagencyrating_name = ''; } else { assessmentagencyrating_name = $scope.assessmentagencyrating_list[assessmentagencyrating_name_index].assessmentagencyrating_name; };

            var amlcategory_name;
            var amlcategory_name_index = $scope.amlcategory_list.map(function (e) { return e.amlcategory_gid }).indexOf($scope.cboamlcategory);
            if (amlcategory_name_index == -1) { amlcategory_name = ''; } else { amlcategory_name = $scope.amlcategory_list[amlcategory_name_index].amlcategory_name; };

            var businesscategory_name;
            var businesscategory_name_index = $scope.businesscategory_list.map(function (e) { return e.businesscategory_gid }).indexOf($scope.cbobusinesscategory);
            if (businesscategory_name_index == -1) { businesscategory_name = ''; } else { businesscategory_name = $scope.businesscategory_list[businesscategory_name_index].businesscategory_name; };

            var designation_type;
            var designation_type_index = $scope.designation_list.map(function (e) { return e.designation_gid }).indexOf($scope.cbodesignation);
            if (designation_type_index == -1) { designation_type = ''; } else { designation_type = $scope.designation_list[designation_type_index].designation_type; };

            var individualproof_name;
            var individualproof_name_index = $scope.individualproof_list.map(function (e) { return e.individualproof_gid }).indexOf($scope.cboindividualproof);
            if (individualproof_name_index == -1) { individualproof_name = ''; } else { individualproof_name = $scope.individualproof_list[individualproof_name_index].employee_name; };

            //var auditeecheckername;
            //var auditeechecker_index = $scope.employee1_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboauditeechecker_edit);
            //if (auditeechecker_index == -1) { auditeecheckername = ''; } else { auditeecheckername = $scope.employee1_list[auditeechecker_index].employee_name; };


            var params = {
                customer_gid: $scope.customer_gid,
                customer_code: $scope.txtcustomer_code,
                customer_name: $scope.txtcustomer_name,
                coi_date: $scope.txtcoi_date,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                //constitution_gid: lsconstitution_gid,
                //constitution_name: lsconstitution_name,
                constitution_gid: $scope.cboConstitution,
                constitution_name: constitution_name,
                assessmentagency_gid: $scope.cboassessmentagency,
                assessmentagency_name: assessmentagency_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                assessmentagencyrating_name: assessmentagencyrating_name,
                amlcategory_gid: $scope.cboamlcategory,
                amlcategory_name: amlcategory_name,
                //businesscategory_gid: lsbusinesscategory_gid,
                //businesscategory_name: lsbusinesscategory_name,
                //designation_gid: lsdesignation_gid,
                //designation_type: lsdesignation_type,
                //individualproof_gid: lsindividualproof_gid,
                //individualproof_name: lsindividualproof_name,
                businesscategory_gid: $scope.cbobusinesscategory,
                businesscategory_name: businesscategory_name,
                designation_gid: $scope.cbodesignation,
                designation_type: designation_type,
                individualproof_gid: $scope.cboindividualproof,
                individualproof_name: individualproof_name,
                rating_date: $scope.rating_date,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                msme_registration: $scope.dependency_name,
                msme_radio: $scope.newDependency,
                remarks: $scope.txtaddremarks

            }
            var url = 'api/FndMstCustomerMasterAdd/customerEditSubmit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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
        $scope.customer_editupdate = function () {
            //var lsconstitution_gid = '';
            //var lsconstitution_name = '';
            //var lsassessmentagency_gid = '';
            //var lsassessmentagency_name = '';
            //var lsassessmentagencyrating_gid = '';
            //var lsassessmentagencyrating_name = '';
            //var lsamlcategory_gid = '';
            //var lsamlcategory_name = '';
            //var lsbusinesscategory_gid = '';
            //var lsbusinesscategory_name = '';
            //var lsdesignation_gid = '';
            //var lsdesignation_type = '';
            //var lsindividualproof_gid = '';
            //var lsindividualproof_name = '';

            //if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
            //    lsconstitution_gid = $scope.cboConstitution.constitution_gid;
            //    lsconstitution_name = $scope.cboConstitution.constitution_name;
            //}
            //if ($scope.cboassessmentagency != undefined || $scope.cboassessmentagency != null) {
            //    lsassessmentagency_gid = $scope.cboassessmentagency.assessmentagency_gid;
            //    lsassessmentagency_name = $scope.cboassessmentagency.assessmentagency_name;
            //}
            //if ($scope.cboassessmentagencyrating != undefined || $scope.cboassessmentagencyrating != null) {
            //    lsassessmentagencyrating_gid = $scope.cboassessmentagencyrating.assessmentagencyrating_gid;
            //    lsassessmentagencyrating_name = $scope.cboassessmentagencyrating.assessmentagencyrating_name;
            //}
            //if ($scope.cboamlcategory != undefined || $scope.cboamlcategory != null) {
            //    lsamlcategory_gid = $scope.cboamlcategory.amlcategory_gid;
            //    lsamlcategory_name = $scope.cboamlcategory.amlcategory_name;
            //}
            //if ($scope.cbobusinesscategory != undefined || $scope.cbobusinesscategory != null) {
            //    lsbusinesscategory_gid = $scope.cbobusinesscategory.businesscategory_gid;
            //    lsbusinesscategory_name = $scope.cbobusinesscategory.businesscategory_name;
            //}

            //if ($scope.cbodesignation != undefined || $scope.cbodesignation != null) {
            //    lsdesignation_gid = $scope.cbodesignation.designation_gid;
            //    lsdesignation_type = $scope.cbodesignation.designation_type;
            //}
            //if ($scope.cboindividualproof != undefined || $scope.cboindividualproof != null) {
            //    lsindividualproof_gid = $scope.cboindividualproof.individualproof_gid;
            //    lsindividualproof_name = $scope.cboindividualproof.individualproof_name;
            //}

            var constitution_name;
            var constitution_name_index = $scope.constitution_list.map(function (e) { return e.constitution_gid }).indexOf($scope.cboconstitution);
            if (constitution_name_index == -1) { constitution_name = ''; } else { constitution_name = $scope.constitution_list[constitution_name_index].constitution_name; };

            var assessmentagency_name;
            var assessmentagency_name_index = $scope.assessmentagency_list.map(function (e) { return e.assessmentagency_gid }).indexOf($scope.cboassessmentagency);
            if (assessmentagency_name_index == -1) { assessmentagency_name = ''; } else { assessmentagency_name = $scope.assessmentagency_list[assessmentagency_name_index].assessmentagency_name; };

            var assessmentagencyrating_name;
            var assessmentagencyrating_name_index = $scope.assessmentagencyrating_list.map(function (e) { return e.assessmentagencyrating_gid }).indexOf($scope.cboassessmentagencyrating);
            if (assessmentagencyrating_name_index == -1) { assessmentagencyrating_name = ''; } else { assessmentagencyrating_name = $scope.assessmentagencyrating_list[assessmentagencyrating_name_index].assessmentagencyrating_name; };

            var amlcategory_name;
            var amlcategory_name_index = $scope.amlcategory_list.map(function (e) { return e.amlcategory_gid }).indexOf($scope.cboamlcategory);
            if (amlcategory_name_index == -1) { amlcategory_name = ''; } else { amlcategory_name = $scope.amlcategory_list[amlcategory_name_index].amlcategory_name; };

            var businesscategory_name;
            var businesscategory_name_index = $scope.businesscategory_list.map(function (e) { return e.businesscategory_gid }).indexOf($scope.cbobusinesscategory);
            if (businesscategory_name_index == -1) { businesscategory_name = ''; } else { businesscategory_name = $scope.businesscategory_list[businesscategory_name_index].businesscategory_name; };

            var designation_type;
            var designation_type_index = $scope.designation_list.map(function (e) { return e.designation_gid }).indexOf($scope.cbodesignation);
            if (designation_type_index == -1) { designation_type = ''; } else { designation_type = $scope.designation_list[designation_type_index].designation_type; };

            var individualproof_name;
            var individualproof_name_index = $scope.individualproof_list.map(function (e) { return e.individualproof_gid }).indexOf($scope.cboindividualproof);
            if (individualproof_name_index == -1) { individualproof_name = ''; } else { individualproof_name = $scope.individualproof_list[individualproof_name_index].employee_name; };

            //var auditeecheckername;
            //var auditeechecker_index = $scope.employee1_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboauditeechecker_edit);
            //if (auditeechecker_index == -1) { auditeecheckername = ''; } else { auditeecheckername = $scope.employee1_list[auditeechecker_index].employee_name; };


            var params = {
                customer_gid: $scope.customer_gid,
                customer_code: $scope.txtcustomer_code,
                customer_name: $scope.txtcustomer_name,
                coi_date: $scope.txtcoi_date,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                //constitution_gid: lsconstitution_gid,
                //constitution_name: lsconstitution_name,
                constitution_gid: $scope.cboconstitution,
                constitution_name: constitution_name,
                assessmentagency_gid: $scope.cboassessmentagency,
                assessmentagency_name: assessmentagency_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                assessmentagencyrating_name: assessmentagencyrating_name,
                amlcategory_gid: $scope.cboamlcategory,
                amlcategory_name: amlcategory_name,
                //businesscategory_gid: lsbusinesscategory_gid,
                //businesscategory_name: lsbusinesscategory_name,
                //designation_gid: lsdesignation_gid,
                //designation_type: lsdesignation_type,
                //individualproof_gid: lsindividualproof_gid,
                //individualproof_name: lsindividualproof_name,
                businesscategory_gid: $scope.cbobusinesscategory,
                businesscategory_name: businesscategory_name,
                designation_gid: $scope.cbodesignation,
                designation_type: designation_type,
                individualproof_gid: $scope.cboindividualproof,
                individualproof_name: individualproof_name,
                rating_date: $scope.rating_date,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                msme_registration: $scope.dependency_name,
                msme_radio: $scope.newDependency,
                remarks: $scope.txtaddremarks

            }
            var url = 'api/FndMstCustomerMasterAdd/customerEditupdated';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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
        function customerupdate()
        {
        
            var constitution_name;
            var constitution_name_index = $scope.constitution_list.map(function (e) { return e.constitution_gid }).indexOf($scope.cboconstitution);
            if (constitution_name_index == -1) { constitution_name = ''; } else { constitution_name = $scope.constitution_list[constitution_name_index].constitution_name; };

            var assessmentagency_name;
            var assessmentagency_name_index = $scope.assessmentagency_list.map(function (e) { return e.assessmentagency_gid }).indexOf($scope.cboassessmentagency);
            if (assessmentagency_name_index == -1) { assessmentagency_name = ''; } else { assessmentagency_name = $scope.assessmentagency_list[assessmentagency_name_index].assessmentagency_name; };

            var assessmentagencyrating_name;
            var assessmentagencyrating_name_index = $scope.assessmentagencyrating_list.map(function (e) { return e.assessmentagencyrating_gid }).indexOf($scope.cboassessmentagencyrating);
            if (assessmentagencyrating_name_index == -1) { assessmentagencyrating_name = ''; } else { assessmentagencyrating_name = $scope.assessmentagencyrating_list[assessmentagencyrating_name_index].assessmentagencyrating_name; };

            var amlcategory_name;
            var amlcategory_name_index = $scope.amlcategory_list.map(function (e) { return e.amlcategory_gid }).indexOf($scope.cboamlcategory);
            if (amlcategory_name_index == -1) { amlcategory_name = ''; } else { amlcategory_name = $scope.amlcategory_list[amlcategory_name_index].amlcategory_name; };

            var businesscategory_name;
            var businesscategory_name_index = $scope.businesscategory_list.map(function (e) { return e.businesscategory_gid }).indexOf($scope.cbobusinesscategory);
            if (businesscategory_name_index == -1) { businesscategory_name = ''; } else { businesscategory_name = $scope.businesscategory_list[businesscategory_name_index].businesscategory_name; };

            var designation_type;
            var designation_type_index = $scope.designation_list.map(function (e) { return e.designation_gid }).indexOf($scope.cbodesignation);
            if (designation_type_index == -1) { designation_type = ''; } else { designation_type = $scope.designation_list[designation_type_index].designation_type; };

            var individualproof_name;
            var individualproof_name_index = $scope.individualproof_list.map(function (e) { return e.individualproof_gid }).indexOf($scope.cboindividualproof);
            if (individualproof_name_index == -1) { individualproof_name = ''; } else { individualproof_name = $scope.individualproof_list[individualproof_name_index].employee_name; };

            //var auditeecheckername;
            //var auditeechecker_index = $scope.employee1_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboauditeechecker_edit);
            //if (auditeechecker_index == -1) { auditeecheckername = ''; } else { auditeecheckername = $scope.employee1_list[auditeechecker_index].employee_name; };


            var params = {
                customer_gid: $scope.customer_gid,
                customer_code: $scope.txtcustomer_code,
                customer_name: $scope.txtcustomer_name,
                coi_date: $scope.txtcoi_date,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                //constitution_gid: lsconstitution_gid,
                //constitution_name: lsconstitution_name,
                constitution_gid: $scope.cboconstitution,
                constitution_name: constitution_name,
                assessmentagency_gid: $scope.cboassessmentagency,
                assessmentagency_name: assessmentagency_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                assessmentagencyrating_name: assessmentagencyrating_name,
                amlcategory_gid: $scope.cboamlcategory,
                amlcategory_name: amlcategory_name,
                //businesscategory_gid: lsbusinesscategory_gid,
                //businesscategory_name: lsbusinesscategory_name,
                //designation_gid: lsdesignation_gid,
                //designation_type: lsdesignation_type,
                //individualproof_gid: lsindividualproof_gid,
                //individualproof_name: lsindividualproof_name,
                businesscategory_gid: $scope.cbobusinesscategory,
                businesscategory_name: businesscategory_name,
                designation_gid: $scope.cbodesignation,
                designation_type: designation_type,
                individualproof_gid: $scope.cboindividualproof,
                individualproof_name: individualproof_name,
                rating_date: $scope.rating_date,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                msme_registration: $scope.dependency_name,
                msme_radio: $scope.newDependency,
                remarks: $scope.txtaddremarks

            }
            var url = 'api/FndMstCustomerMasterAdd/customerEditupdated';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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
        $scope.customer_update = function () {
            var params = {
                customer_name: $scope.customer_name,
                businessstart_date: $scope.businessstart_date,
                year_business: $scope.year_business,
                month_business: $scope.month_business,
                constitution_gid: $scope.cboConstitution,
                assessmentagency_gid: $scope.cboassessmentagency,
                constitution_name: $scope.constitution_name,
                assessmentagency_name: $scope.assessmentagency_name,
                assessmentagencyrating_name: $scope.assessmentagencyrating_name,
                amlcategory_name: $scope.amlcategory_name,
                businesscategory_name: $scope.businesscategory_name,
                designation_type: $scope.designation_type,
                individualproof_name: $scope.individualproof_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                rating_date: $scope.rating_date,
                amlcategory_gid: $scope.cboamlcategory,
                businesscategory_gid: $scope.cbobusinesscategory,
                designation_gid: $scope.cbodesignation,
                individualproof_gid: $scope.cboindividualproof,
                cin_no: $scope.cin_no,
                pan_no: $scope.pan_no,
                contactperson_fn: $scope.contactperson_fn,
                contactperson_mn: $scope.contactperson_mn,
                contactperson_ln: $scope.contactperson_ln,
                remarks: $scope.addremarks,
                msme_registration: $scope.dependency_name,
                msme_radio: $scope.newDependency,
                customer_gid: $scope.customer_gid

            }
            var url = 'api/FndMstCustomerMasterAdd/customerEditUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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
        $scope.customersubmit = function () {
            var params = {
                customer_name: $scope.customer_name,
                businessstart_date: $scope.businessstart_date,
                year_business: $scope.year_business,
                month_business: $scope.month_business,
                constitution_gid: $scope.cboConstitution,
                assessmentagency_gid: $scope.cboassessmentagency,
                constitution_name: $scope.constitution_name,
                assessmentagency_name: $scope.assessmentagency_name,
                assessmentagencyrating_name: $scope.assessmentagencyrating_name,
                amlcategory_name: $scope.amlcategory_name,
                businesscategory_name: $scope.businesscategory_name,
                designation_type: $scope.designation_type,
                individualproof_name: $scope.individualproof_name,
                assessmentagencyrating_gid: $scope.cboassessmentagencyrating,
                rating_date: $scope.rating_date,
                amlcategory_gid: $scope.cboamlcategory,
                businesscategory_gid: $scope.cbobusinesscategory,
                designation_gid: $scope.cbodesignation,
                individualproof_gid: $scope.cboindividualproof,
                cin_no: $scope.cin_no,
                pan_no: $scope.pan_no,
                contactperson_fn: $scope.contactperson_fn,
                contactperson_mn: $scope.contactperson_mn,
                contactperson_ln: $scope.contactperson_ln,
                remarks: $scope.addremarks,
                msme_registration: $scope.dependency_name,
                customer_gid: $scope.customer_gid

            }
            var url = 'api/FndMstCustomerMasterAdd/customersubmitapproval';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.FndMstCustomerMaster');
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


        $scope.gst_delete = function (customer2gst_gid) {
            var params =
                {
                    customer2gst_gid: customer2gst_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteGST';
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

                gst_list();
            });

        }

        function gst_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.customergst_list = resp.data.customergst_list;

            });
        }


        //$scope.mobileno_edit = function () {

        //    if (($scope.mobile_no == undefined) || ($scope.mobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
        //        Notify.alert('Enter Mobile No/Select Status');
        //    }
        //    else {


        //        var params = {
        //            mobile_no: $scope.txtmobile_no,
        //            primary_mobileno: $scope.rdbprimarymobile_no,
        //            whatsapp_mobileno: $scope.rdbwhatsappmobile_no
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/PostMobileNo';
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'info',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            customermobileno_list();
        //            $scope.txtmobile_no = '';
        //            $scope.rdbprimarymobile_no = '';
        //            $scope.rdbwhatsappmobile_no = '';
        //            $scope.rdbprimarymobile_no == false;
        //        });
        //    }
        //}

        //--------Delete Mobile No--------//
        $scope.mobileno_delete = function (customer2mobileno_gid) {
            var params =
                {
                    customer2mobileno_gid: customer2mobileno_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteMobileNo';
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


        $scope.cheque_delete = function (fndmanagement2cheque_gid) {
            var params =
                {
                    fndmanagement2cheque_gid: fndmanagement2cheque_gid
                }
            //console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteChequeDetail';
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

                customermobileno_list();
            });

        }

        //$scope.emailaddress_edit = function (customer2emailaddress_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editemailaddress.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var params = {
        //            customer2emailaddress_gid: customer2emailaddress_gid
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/EmailAddressEdit';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.txteditemail_address = resp.data.email_address;
        //            $scope.rdbeditprimary_emailaddress = resp.data.primary_emailaddress;
        //        });

        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.update_emailaddress = function () {

        //            var params = {
        //                email_address: $scope.txteditemail_address,
        //                primary_emailaddress: $scope.rdbeditprimary_emailaddress,
        //                customer2emailaddress_gid: customer2emailaddress_gid,
        //                customer_gid: localStorage.getItem('customer_gid'),
        //            }
        //            var url = 'api/FndMstCustomerMasterAdd/EmailAddressUpdate';
        //            lockUI();
        //            SocketService.post(url, params).then(function (resp) {
        //                unlockUI();
        //                if (resp.data.status == true) {

        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });

        //                }
        //                else {
        //                    Notify.alert(resp.data.message, {
        //                        status: 'info',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                }
        //                emailaddress_templist();
        //            });

        //            $modalInstance.close('closed');

        //        }
        //    }
        //}

        function emailaddress_templist() {
            var param = {
                customer_gid: $scope.customer_gid
            };
            var url = 'api/FndMstCustomerMasterAdd/GetEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.email_list = resp.data.email_list;

            });
        }
        $scope.emailaddress_delete = function (customer2emailaddress_gid) {
            var params =
                {
                    customer2emailaddress_gid: customer2emailaddress_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteEmailAddress';
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
            var url = 'api/FndMstCustomerMasterAdd/GetEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.customeremailaddress_list = resp.data.customeremailaddress_list;

            });
        }
        $scope.address_edit = function (customer2address_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editaddressdetails.html',
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

                var params = {
                    customer2address_gid: customer2address_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/AddressDetailEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboaddresstype = resp.data.address_typegid;
                    $scope.rdbprimaryaddress = resp.data.primary_address;
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
                    $scope.customer_gid = resp.data.customer_gid;
                    $scope.customer2address_gid = resp.data.customer2address_gid;
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
                $scope.addressUpdate = function () {
                    var address_type = $('#address_type :selected').text();

                    var params = {
                        address_typegid: $scope.cboaddresstype,
                        address_type: address_type,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        primary_address: $scope.rdbprimaryaddress,
                        landmark: $scope.txtlandmark,
                        postal_code: $scope.txtpostal_code,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude,
                        customer2address_gid: $scope.customer2address_gid,
                        customer_gid: localStorage.getItem('customer_gid'),
                    }
                    var url = 'api/FndMstCustomerMasterAdd/AddressDetailUpdate';
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
                        address_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }


        $scope.address_delete = function (customer2address_gid) {
            var params =
                {
                    customer2address_gid: customer2address_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteAddress';
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

                address_list();
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
                var url = 'api/FndMstCustomerMasterAdd/PostGST';
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
                            status: 'warning',
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
        $scope.emailaddress_add = function () {

            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimaryemail_address == undefined)) {
                Notify.alert('Enter Email Address/Select Status');
            }
            else {


                var params = {
                    email_address: $scope.txtemail_address,
                    primary_emailaddress: $scope.rdbprimaryemail_address,
                }
                var url = 'api/FndMstCustomerMasterAdd/PostEmailAddress';
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
                    emailaddress_list();
                    $scope.txtemail_address = '';
                    $scope.rdbprimaryemail_address = '';
                    $scope.rdbprimaryemail_address == false;
                });
            }
        }
        function address_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.customeraddress_list = resp.data.customeraddress_list;

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

        $scope.edit_cheque = function () {
            var params = {
                stakeholder_gid: $scope.cboStakeholder.stakeholder_gid,
                stakeholder_name: $scope.cboStakeholder.stakeholder_name,
                stakeholder_type: $scope.txtstakeholder_type,
                designation: $scope.txtdesignation,
                accountholder_name: $scope.txtaccountholder_name,
                account_number: $scope.txtaccount_number,
                bank_name: $scope.txtbank_name,
                cheque_no: $scope.txtcheque_no,
                ifsc_code: $scope.txtifsc_code,
                micr: $scope.txtmicr,
                branch_address: $scope.txtbranch_address,
                branch_name: $scope.txtbranch_name,
                city: $scope.txtcity,
                district: $scope.txtdistrict,
                state: $scope.txtstate,
                mergedbankingentity_gid: $scope.cbomergedbanking_entity.bankname_gid,
                mergedbankingentity_name: $scope.cbomergedbanking_entity.bankname_name,
                special_condition: $scope.txtspecial_condition,
                general_remarks: $scope.txtgeneral_remarks,
                cts_enabled: $scope.rbocts_enabled,
                cheque_type: $scope.cbocheque_type,
                date_chequetype: $scope.txtdate_chequetype,
                date_chequepresentation: $scope.txtdate_chequepresentation,
                status_chequepresentation: $scope.txtstatus_chequepresentation,
                date_chequeclearance: $scope.txtdate_chequeclearance,
                status_chequeclearance: $scope.txtstatus_chequeclearance
            }
            var url = 'api/UdcManagement/PostChequeDetail';
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
                cheque_list();

                $scope.cboStakeholder = '',
              $scope.txtstakeholder_type = '',
              $scope.txtdesignation = '',
              $scope.txtaccountholder_name = '',
              $scope.txtaccount_number = '',
              $scope.txtbank_name = "",
              $scope.txtcheque_no = "",
              $scope.txtifsc_code = "",
              $scope.txtmicr = "",
              $scope.txtbranch_address = "",
              $scope.txtbranch_name = "",
              $scope.txtcity = "",
              $scope.txtdistrict = "",
              $scope.txtstate = "",
              $scope.cbomergedbanking_entity = "",
              $scope.txtspecial_condition = "",
              $scope.txtgeneral_remarks = "",
              $scope.rbocts_enabled = "",
              $scope.cbocheque_type = "",
              $scope.txtdate_chequetype = "",
              $scope.txtdate_chequepresentation = "",
              $scope.txtstatus_chequepresentation = "",
              $scope.txtdate_chequeclearance = "",
              $scope.txtstatus_chequeclearance = ""
                $scope.uploadfrm = undefined;
                $scope.chequedocument_list = null;


            });


        }
        function cheque_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });
        }

        $scope.delete_cheque = function (udcmanagement2cheque_gid) {
            lockUI();
            var params = {
                udcmanagement2cheque_gid: udcmanagement2cheque_gid
            }
            var url = 'api/FndMstCustomerMasterAdd/DeleteChequeDetail';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                cheque_list();
                unlockUI();
            });
        }

        //$scope.bank_add = function () {

        //    if (($scope.txtbank_name == '') || ($scope.txtbank_name == undefined) || ($scope.txtbranch_name == '') || ($scope.txtbranch_name == undefined)
        //        || ($scope.txtbank_address == '') || ($scope.txtbank_address == undefined) || ($scope.txtifsc_code == '') || ($scope.txtifsc_code == undefined)
        //        || ($scope.txtbankaccount_name == '') || ($scope.txtbankaccount_name == undefined) || ($scope.cbobankaccountlevel.bankaccountlevel_name == '')
        //        || ($scope.cbobankaccountlevel.bankaccountlevel_name == undefined) || ($scope.cbobankaccounttype.bankaccounttype_name == '')
        //        || ($scope.cbobankaccounttype.bankaccounttype_name == undefined) || ($scope.txtbankaccount_number == '')
        //        || ($scope.txtbankaccount_number == undefined)) {
        //        $scope.mandatoryfields = true;
        //    }
        //    else {
        //        $scope.mandatoryfields = false;
        //        var params = {
        //            ifsc_code: $scope.txtifsc_code,
        //            bank_name: $scope.txtbank_name,
        //            branch_name: $scope.txtbranch_name,
        //            bank_address: $scope.txtbank_address,
        //            micr_code: $scope.txtmicr_code,
        //            bankaccount_name: $scope.txtbankaccount_name,
        //            bankaccountlevel_gid: $scope.cbobankaccountlevel.bankaccountlevel_gid,
        //            bankaccountlevel_name: $scope.cbobankaccountlevel.bankaccountlevel_name,
        //            bankaccounttype_gid: $scope.cbobankaccounttype.bankaccounttype_gid,
        //            bankaccounttype_name: $scope.cbobankaccounttype.bankaccounttype_name,
        //            bankaccount_number: $scope.txtbankaccount_number,
        //            confirmbankaccountnumber: $scope.txtconfirmbankaccount_number
        //        }
        //        var url = 'api/FndMstCustomerMasterAdd/PostBank';
        //        lockUI();
        //        SocketService.post(url, params).then(function (resp) {
        //            unlockUI();
        //            if (resp.data.status == true) {

        //                Notify.alert(resp.data.message, {
        //                    status: 'success',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'info',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //            }
        //            bank_list();
        //            $scope.txtbank_name = '';
        //            $scope.txtbranch_name = '';
        //            $scope.txtbank_address = '';
        //            $scope.txtmicr_code = '';
        //            $scope.txtifsc_code = '';
        //            $scope.txtbankaccount_name = '';
        //            $scope.cbobankaccountlevel = '';
        //            $scope.cbobankaccounttype = '';
        //            $scope.txtbankaccount_number = '';
        //            $scope.txtconfirmbankaccount_number = '';
        //        });
        //    }
        //}

        $scope.bank_delete = function (customer2bank_gid) {
            var params =
                {
                    customer2bank_gid: customer2bank_gid
                }
            console.log(params)
            var url = 'api/FndMstCustomerMasterAdd/DeleteBank';
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

                bank_list();
            });

        }

        function bank_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetBankList';
            SocketService.get(url).then(function (resp) {
                $scope.customerbank_list = resp.data.customerbank_list;

            });
        }


        $scope.myraisequery = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    customer_gid: customer_gid
                }

                $scope.submit = function () {


                    var params = {
                        customer_gid: customer_gid,
                        query_title: $scope.txtquery_title,
                        query_description: $scope.txtquery_description,

                    }
                    var url = 'api/FndMstCustomerMasterAdd/PostCustomerRaiseQuery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //activate();
                            query_list(customer_gid);
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


        function query_list(customer_gid) {
            var params = {
                customer_gid: customer_gid,

            }

            var url = 'api/FndMstCustomerMasterAdd/GetCustomerRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.customerraisequery_list = resp.data.customerraisequery_list;
            });
        }

        $scope.view_myquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/myqueryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = query_responseby;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.query_close = function (customerraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        customer_gid : cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid,
                        customerraisequery_gid: customerraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                       
                    }
                    var url = 'api/FndMstCustomerMasterAdd/PostCustomerresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
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


    }
})();

