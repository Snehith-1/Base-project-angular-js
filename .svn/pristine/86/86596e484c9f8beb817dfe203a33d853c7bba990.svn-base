(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerMasterAddController', FndMstCustomerMasterAddController);

    FndMstCustomerMasterAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function FndMstCustomerMasterAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerMasterAddController';

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
            vm.open3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened3 = true;
            };
            vm.open4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened4 = true;
            };
            vm.open5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened5 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/FndMstCustomerMasterAdd/GetcustomerTempClear';
            SocketService.get(url).then(function (resp) {
            });

            //var url = 'api/FndMstCustomerMasterAdd/GetBankAccountLevel';
            //SocketService.get(url).then(function (resp) {
            //    $scope.bankaccountlevel_list = resp.data.bankaccountlevel_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/GetBankAccountType';
            //SocketService.get(url).then(function (resp) {
            //    $scope.bankaccounttype_list = resp.data.bankaccounttype_list;
            //});

            var url = 'api/FndMstCustomerMasterAdd/Getconstitution';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;
            });

            var url = 'api/FndMstCustomerMasterAdd/Getassessmentagency';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagency_list = resp.data.assessmentagency_list;
            });
            var url = 'api/FndMstCustomerMasterAdd/Getassessmentagencyrating';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            });
            var url = 'api/FndMstCustomerMasterAdd/Getamlcategory';
            SocketService.get(url).then(function (resp) {
                $scope.amlcategory_list = resp.data.amlcategory_list;
            });
            var url = 'api/FndMstCustomerMasterAdd/Getbusinesscategory';
            SocketService.get(url).then(function (resp) {
                $scope.businesscategory_list = resp.data.businesscategory_list;
            });

            var url = 'api/FndMstCustomerMasterAdd/Getdesignation';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });

            var url = 'api/FndMstCustomerMasterAdd/Getindividualproof';
            SocketService.get(url).then(function (resp) {
                $scope.individualproof_list = resp.data.individualproof_list;
            });



            var url = 'api/FndMstCustomerMasterAdd/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

        }
       

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



        var url = 'api/FndMstCustomerMasterAdd/GetDropDownUdc';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.bankname_list = resp.data.bankname_list;
            unlockUI();
        });
        // For MSME
        $scope.onselectedDep_yes = function () {
            if ($scope.newDependency == 'Yes') {
                $scope.new_dependency = true;
                $scope.new_row = true;
            }
            else {
                $scope.new_dependency = false;
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

                    }  else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }

        }

        $scope.Back = function () {
            $state.go('app.FndMstCustomerMaster');
        }

        $scope.add_Submit = function () {
            $state.go('app.FndMstCustomerMaster');
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
                msme_radio:$scope.newDependency,
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

        $scope.customer_submit = function () {

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
                constitution_gid: $scope.cboConstitution.constitution_gid,
                constitution_name: $scope.cboConstitution.constitution_name,
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
                businesscategory_gid: $scope.cbobusinesscategory.businesscategory_gid,
                businesscategory_name: $scope.cbobusinesscategory.businesscategory_name,
                designation_gid: $scope.cbodesignation.designation_gid,
                designation_type: $scope.cbodesignation.designation_type,
                individualproof_gid: $scope.cboindividualproof.individualproof_gid,
                individualproof_name: $scope.cboindividualproof.individualproof_name,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                remarks: $scope.txtaddremarks,
                msme_registration: $scope.txtdependency_name,
                msme_radio: $scope.newDependency,

            }
            var url = 'api/FndMstCustomerMasterAdd/customerSubmit';
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
                        status: 'info',
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


        $scope.mobileno_add = function () {

            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbwhatsappmobile_no == undefined)) {
                Notify.alert('Enter Mobile No/Select Status');
                status: 'info';
                pos: 'top-center';
                timeout: 3000;
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                mobileno_list();
            });

        }



        function mobileno_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.customermobileno_list = resp.data.customermobileno_list;

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
                        status: 'info',
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
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                address_list();
            });

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

        $scope.add_cheque = function () {
          
            if (($scope.txtaccount_number == undefined) || ($scope.txtaccount_number == '')|| ($scope.txtcheque_no == undefined) || ($scope.txtcheque_no == '')) {
                Notify.alert('Enter Cheque Details', 'warning');
            }
            else {

                var lsbankname_gid = '';
                var lsbankname_name = '';

                if ($scope.cbomergedbanking_entity != undefined || $scope.cbomergedbanking_entity != null) {
                    lsbankname_gid = $scope.cbomergedbanking_entity.bankname_gid;
                    lsbankname_name = $scope.cbomergedbanking_entity.bankname_name;
                }

            var params = {              
               
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
                //mergedbankingentity_gid: $scope.cbomergedbanking_entity.bankname_gid,
                //mergedbankingentity_name: $scope.cbomergedbanking_entity.bankname_name,
                mergedbankingentity_gid: lsbankname_gid,
                mergedbankingentity_name: lsbankname_name,
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
            
            
            var url = 'api/FndMstCustomerMasterAdd/PostChequeDetail';
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

                //  $scope.cboStakeholder = '',
                //$scope.txtstakeholder_type = '',
                //$scope.txtdesignation = '',
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


        }
        //var param = {
        //    fndmanagement2cheque_gid: $scope.fndmanagement2cheque_gid

        //}
        
        function cheque_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });

            }
        

        //$scope.onChangeBorrowerName = function (application_gid) {
        //    var params = {
        //        application_gid: application_gid
        //    }
        //    var url = 'api/FndMstCustomerMasterAdd/GetStakeholders';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        $scope.StakeholderList = resp.data.StakeholderList;
        //    });
        //    $scope.txtstakeholder_type = '';
        //    $scope.txtdesignation = '';
        //}

        //$scope.onChangeStakeholderName = function (stakeholder_gid) {
        //    var list = $scope.StakeholderList;

        //    for (var i = 0; i < list.length; i++) {
        //        if (list[i].stakeholder_gid == stakeholder_gid) {
        //            $scope.txtstakeholder_type = list[i].stakeholder_type;
        //            $scope.txtdesignation = list[i].designation;
        //            break;
        //        }
        //    }

        //}

        $scope.delete_cheque = function (fndmanagement2cheque_gid) {
            lockUI();
            var params = {
                fndmanagement2cheque_gid: fndmanagement2cheque_gid
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
        $scope.viewCheque = function (fndmanagement2cheque_gid) {
            $location.url('app/MstUDCMakerView?hash=' + cmnfunctionService.encryptURL('lsfndmanagement_gid=' + $scope.fndmanagement_gid + '&lstab=add'));
        }
        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
        }

        function chequedocument_list() {
            var url = 'api/FndMstCustomerMasterAdd/GetChequeDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.chequedocument_list = resp.data.chequedocument_list;
            });
        }

          $scope.UploadDocument = function (val, val1, name) {
        var item = {
            name: val[0].name,
            file: val[0]
        };
        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

        if (IsValidExtension == false) {
            Notify.alert("File format is not supported..!", {
                status: 'danger',
                pos: 'top-center',
                timeout: 3000
            });
            return false;
        }
        var frm = new FormData();
        frm.append('fileupload', item.file);
        frm.append('file_name', item.name);
        frm.append('document_name', $scope.documentname);
        frm.append('project_flag', "Default");
        $scope.uploadfrm = frm;
        if ($scope.uploadfrm != undefined) {
            var url = 'api/FndMstCustomerMasterAdd/ChequeDocumentUpload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
          
                if (resp.data.status == true){
                       var url = 'api/Kyc/ChequeOCR';
                       SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                           if (resp.data.statusCode == 101) {
                               $scope.txtaccountholder_name = resp.data.result.name[0];
                               $scope.txtaccount_number = resp.data.result.accNo;
                               $scope.txtbank_name = resp.data.result.bank;
                               $scope.txtcheque_no = resp.data.result.chequeNo;
                               $scope.txtifsc_code = resp.data.result.ifsc;
                               $scope.txtmicr = resp.data.result.micr;
                               $scope.txtbranch_address = resp.data.result.bankDetails.address;
                               $scope.txtbranch_name = resp.data.result.bankDetails.branch;
                               $scope.txtcity = resp.data.result.bankDetails.city;
                               $scope.txtdistrict = resp.data.result.bankDetails.district;
                               $scope.txtstate = resp.data.result.bankDetails.state;
                           }
                           else {
                               Notify.alert('Error in fetching values from document..!', 'warning');
                           }
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
                $("#file").val('');
                $scope.uploadfrm = undefined;

                chequedocument_list();
                unlockUI();
            });
        }
        else {
            alert('Document is not Available..!');
            return;
        }
        
          }
          $scope.delete_document = function (cheque2document_gid) {
              lockUI();
              var params = {
                  cheque2document_gid: cheque2document_gid
              }
              var url = 'api/FndMstCustomerMasterAdd/ChequeDocumentDelete';
              SocketService.getparams(url, params).then(function (resp) {
                  $scope.documentupload_list = resp.data.documentupload_list;
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
                  chequedocument_list();
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
                        status: 'info',
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




    }
})();

