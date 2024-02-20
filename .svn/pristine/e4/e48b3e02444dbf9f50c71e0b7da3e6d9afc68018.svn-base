(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBuyerEditController', MstBuyerEditController);

    MstBuyerEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBuyerEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBuyerEditController';

        $scope.buyer_gid = localStorage.getItem('buyer_gid');

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

            var url = 'api/Mstbuyer/GetbuyerTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var param = {
                buyer_gid: $scope.buyer_gid
            };

            gst_list();

            mobileno_list();

            emailaddress_list();

            address_list();

            bank_list();

            $scope.mandatoryfields = false;


            var url = 'api/MstCreditStatusAdd/buyerDetailsEdit';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtbuyer_code = resp.data.buyer_code;
                $scope.txtbuyer_name = resp.data.buyer_name;
                $scope.txtcoi_date = resp.data.editcoi_date;
                $scope.txtbusinessstart_date = resp.data.editbusinessstart_date;
                $scope.txtyear_business = resp.data.year_business;
                $scope.txtmonth_business = resp.data.month_business;
                $scope.cboconstitution = resp.data.constitution_gid;
                $scope.txtcin_no = resp.data.cin_no;
                $scope.txtpan_no = resp.data.pan_no;
                $scope.txtcontactperson_fn = resp.data.contactperson_firstname;
                $scope.txtcontactperson_mn = resp.data.contactperson_middlename;
                $scope.txtcontactperson_ln = resp.data.contactperson_lastname;

                if (resp.data.credit_status == 'Pending') {
                    $scope.showsubmit = false;
                    $scope.showupdate = true;
                } else if (resp.data.credit_status == 'Completed') {
                    $scope.showsubmit = false;
                    $scope.showupdate = true;
                }
                else {
                    $scope.showsubmit = true;
                    $scope.showupdate = false;
                }

                unlockUI(); 
            });


        }

        $scope.onchangebusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
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

                if ($scope.buyergst_list != null) {
                    var paramsdel =
                    {
                        buyer_gid: $scope.buyer_gid
                    }
                    var url = 'api/Mstbuyer/DeleteGSTBuyer';
                    lockUI();
                    SocketService.getparams(url, paramsdel).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                        }
                        else {
                            Notify.alert('Error occured while deleting the existing GST Details..!', 'warning');
                        }
                    });
                }
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/Kyc/GSTSBPAN';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                unlockUI();
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

                                gst_templist();
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else if (resp.data.statusCode == 103) {
                        setTimeout(function () {
                            gst_templist();
                        }, 3200);
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

        function gst_list() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };
            var url = 'api/MstCreditStatusAdd/buyerGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.buyergst_list = resp.data.buyergst_list;
            });
        }

        function mobileno_list() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };
            var url = 'api/MstCreditStatusAdd/buyerMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mobileno_list = resp.data.mobileno_list;
            });
        }

       
        function emailaddress_list() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };
            var url = 'api/MstCreditStatusAdd/buyerEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.email_list = resp.data.email_list;

            });
        }

        function address_list() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };

            var url = 'api/MstCreditStatusAdd/buyerAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.address_list = resp.data.buyeraddress_list;
            });
        }

        function bank_list() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };

            var url = 'api/MstCreditStatusAdd/buyerBankList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bankdetail_list = resp.data.bank_list;
            });
        }

        function gst_templist() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };
            var url = 'api/MstCreditStatusAdd/GetGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.buyergst_list = resp.data.buyergst_list;
            });
        }

        function mobileno_templist() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };
            var url = 'api/MstCreditStatusAdd/GetMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mobileno_list = resp.data.mobileno_list;
            });
        }


        function emailaddress_templist() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };
            var url = 'api/MstCreditStatusAdd/GetEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.email_list = resp.data.email_list;

            });
        }

        function address_templist() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };

            var url = 'api/MstCreditStatusAdd/GetAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.address_list = resp.data.buyeraddress_list;
            });
        }

        function bank_templist() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };

            var url = 'api/MstCreditStatusAdd/GetBankList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bankdetail_list = resp.data.bank_list;
            });
        }


        $scope.buyer_save = function () {

            var constitutionName = $('#constitution_name :selected').text();

            var params = {
                buyer_code: $scope.txtbuyer_code,
                buyer_name: $scope.txtbuyer_name,
                editcoi_date: $scope.txtcoi_date,
                editbusinessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyear_business,
                month_business: $scope.txtmonth_business,
                constitution_gid: $scope.cboconstitution,
                constitution_name: constitutionName,
                cin_no: $scope.txtcin_no,
                pan_no: $scope.txtpan_no,
                contactperson_fn: $scope.txtcontactperson_fn,
                contactperson_mn: $scope.txtcontactperson_mn,
                contactperson_ln: $scope.txtcontactperson_ln,
                buyer_gid: $scope.buyer_gid
            }
            var url = 'api/Mstbuyer/buyerEditSave';
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
                $state.go('app.MstBuyerSummary');
            });

        }

        $scope.buyer_submit = function () {

            if (($scope.txtbuyer_code == '') || ($scope.txtbuyer_code == undefined) || ($scope.txtbuyer_name == '') || ($scope.txtbuyer_name == undefined) || ($scope.txtcoi_date == '') || ($scope.txtcoi_date == undefined) || ($scope.txtbusinessstart_date == '') || ($scope.txtbusinessstart_date == undefined) || ($scope.txtyear_business == '') || ($scope.txtyear_business == undefined) || ($scope.txtmonth_business == '') || ($scope.txtmonth_business == undefined) ||
               ($scope.txtpan_no == '') || ($scope.txtpan_no == undefined) || ($scope.txtcontactperson_fn == '') || ($scope.txtcontactperson_fn == undefined) )
            {
                Notify.alert('Please Fill Mandatory Fields');
            }
            else {
                $scope.mandatoryfields = false;

                var constitutionName = $('#constitution_name :selected').text();
                var params = {
                    buyer_code: $scope.txtbuyer_code,
                    buyer_name: $scope.txtbuyer_name,
                    editcoi_date: $scope.txtcoi_date,
                    editbusinessstart_date: $scope.txtbusinessstart_date,
                    year_business: $scope.txtyear_business,
                    month_business: $scope.txtmonth_business,
                    constitution_gid: $scope.cboconstitution,
                    constitution_name: constitutionName,
                    cin_no: $scope.txtcin_no,
                    pan_no: $scope.txtpan_no,
                    contactperson_fn: $scope.txtcontactperson_fn,
                    contactperson_mn: $scope.txtcontactperson_mn,
                    contactperson_ln: $scope.txtcontactperson_ln,
                    buyer_gid: $scope.buyer_gid

                }
                var url = 'api/Mstbuyer/buyerEditSubmit';
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
                    $state.go('app.MstBuyerSummary');
                });
            }
        }

        $scope.buyer_update = function () {
            if (($scope.txtbuyer_code == '') || ($scope.txtbuyer_code == undefined) || ($scope.txtbuyer_name == '') || ($scope.txtbuyer_name == undefined) || ($scope.txtcoi_date == '') || ($scope.txtcoi_date == undefined) || ($scope.txtbusinessstart_date == '') || ($scope.txtbusinessstart_date == undefined) || ($scope.txtyear_business == '') || ($scope.txtyear_business == undefined) || ($scope.txtmonth_business == '') || ($scope.txtmonth_business == undefined) ||
               ($scope.txtpan_no == '') || ($scope.txtpan_no == undefined) || ($scope.txtcontactperson_fn == '') || ($scope.txtcontactperson_fn == undefined)) {
                Notify.alert('Please Fill Mandatory Fields');
            }
            else {
                $scope.mandatoryfields = false;
                var constitutionName = $('#constitution_name :selected').text();

                var params = {
                    buyer_code: $scope.txtbuyer_code,
                    buyer_name: $scope.txtbuyer_name,
                    editcoi_date: $scope.txtcoi_date,
                    editbusinessstart_date: $scope.txtbusinessstart_date,
                    year_business: $scope.txtyear_business,
                    month_business: $scope.txtmonth_business,
                    constitution_gid: $scope.cboconstitution,
                    constitution_name: constitutionName,
                    cin_no: $scope.txtcin_no,
                    pan_no: $scope.txtpan_no,
                    contactperson_fn: $scope.txtcontactperson_fn,
                    contactperson_mn: $scope.txtcontactperson_mn,
                    contactperson_ln: $scope.txtcontactperson_ln,
                    buyer_gid: $scope.buyer_gid

                }
                var url = 'api/Mstbuyer/buyerEditUpdate';
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
                    $state.go('app.MstBuyerSummary');
                });
            }
        }

        $scope.gst_add = function () {

            if (($scope.txtgst_no == '') || ($scope.txtgst_no == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_state == undefined)) {
                $scope.mandatoryfieldsgst = true;
            }
            else {
                $scope.mandatoryfieldsgst = false;

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
                    gst_templist();
                    $scope.txtgst_state = '';
                    $scope.txtgst_no = '';
                    document.getElementById("gstregisterstatus_yes").checked = false;
                    document.getElementById("gstregisterstatus_no").checked = false;
                });
            }
        }

        $scope.gst_edit = function (buyer2gst_gid) {
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
                    buyer2gst_gid: buyer2gst_gid
                }
                var url = 'api/MstCreditStatusAdd/GSTEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditgst_state = resp.data.gststate_name;
                    $scope.txteditgst_number = resp.data.gst_no;
                    $scope.rdbgstregistered = resp.data.gstregister_status;
                    $scope.buyer_gid = resp.data.buyer_gid;
                    $scope.buyer2gst_gid = resp.data.buyer2gst_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangeeditgst_number = function () {
                    var gst_number = $scope.txteditgst_number;
                    var params = {
                        gst_code: gst_number.substring(0, 2)
                    }
                    var url = 'api/MstApplicationAdd/GetGSTState';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txteditgst_state = resp.data.gst_state;
                    });
                }

                $scope.update_gst = function () {
                    
                    var params = {
                        gststate_name: $scope.txteditgst_state,
                        gst_no: $scope.txteditgst_number,
                        gstregister_status: $scope.rdbgstregistered,
                        buyer_gid: localStorage.getItem('buyer_gid'),
                        buyer2gst_gid: $scope.buyer2gst_gid,
                    }
                    var url = 'api/MstCreditStatusAdd/GSTUpdate';
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
                        gst_templist();
                        $scope.txtgst_no = '';
                      
                    });

                    $modalInstance.close('closed');
                }
            }
        }

        $scope.gst_delete = function (buyer2gst_gid) {
            var params =
                {
                    buyer2gst_gid: buyer2gst_gid
                }
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

                gst_templist();
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
                    whatsapp_mobileno: $scope.rdbwhatsappmobile_no,
                    buyer_gid: localStorage.getItem('buyer_gid')

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
                    mobileno_templist();
                    $scope.txtmobile_no = '';
                    document.getElementById("primarymobileno_yes").checked = false;
                    document.getElementById("primarymobileno_no").checked = false;
                    document.getElementById("whatsappmobileno_yes").checked = false;
                    document.getElementById("whatsappmobileno_no").checked = false;


                });
            }
        }

        $scope.mobileno_edit = function (buyer2mobileno_gid) {
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
                    buyer2mobileno_gid: buyer2mobileno_gid
                }
                var url = 'api/MstCreditStatusAdd/MobileNoEdit';
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
                         buyer2mobileno_gid: buyer2mobileno_gid,
                         buyer_gid: localStorage.getItem('buyer_gid'),
                        
                     }
                      var url = 'api/MstCreditStatusAdd/MobileNoUpdate';
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
                         mobileno_templist();
                     }); 

                    $modalInstance.close('closed');

                }
            }
        }

        //--------Delete Mobile No--------//
        $scope.mobileno_delete = function (buyer2mobileno_gid) {
            var params =
                {
                    buyer2mobileno_gid: buyer2mobileno_gid
                }
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
                mobileno_templist();
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
                    buyer_gid: localStorage.getItem('buyer_gid')
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
                    emailaddress_templist();
                    $scope.txtemail_address = '';
                    document.getElementById("rdbprimaryemailaddress_yes").checked = false;
                    document.getElementById("rdbprimaryemailaddress_no").checked = false;
                });
            }
        }

        $scope.emailaddress_edit = function (buyer2emailaddress_gid) {
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
                    buyer2emailaddress_gid: buyer2emailaddress_gid
                }
                var url = 'api/MstCreditStatusAdd/EmailAddressEdit';
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
                          buyer2emailaddress_gid: buyer2emailaddress_gid,
                          buyer_gid: localStorage.getItem('buyer_gid'),
                     }
                      var url = 'api/MstCreditStatusAdd/EmailAddressUpdate';
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
                         emailaddress_templist();
                     }); 

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.emailaddress_delete = function (buyer2emailaddress_gid) {
            var params =
                {
                    buyer2emailaddress_gid: buyer2emailaddress_gid
                }
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

                emailaddress_templist();
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
                            
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
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

        $scope.address_edit = function (buyer2address_gid) {
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
                    buyer2address_gid: buyer2address_gid
                }
                var url = 'api/MstCreditStatusAdd/AddressDetailEdit';
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
                    $scope.buyer_gid = resp.data.buyer_gid;
                    $scope.buyer2address_gid = resp.data.buyer2address_gid;
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
                        buyer2address_gid: $scope.buyer2address_gid,
                        buyer_gid: localStorage.getItem('buyer_gid'),
                    }
                    var url = 'api/MstCreditStatusAdd/AddressDetailUpdate';
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
                        address_templist();
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

                address_templist();
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
                $scope.mandatoryfieldsbank = true;
            }
            else {
                $scope.mandatoryfieldsbank = false;
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
                    bank_templist();
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

        $scope.bank_edit = function (buyer2bank_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbankdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/Mstbuyer/GetBankAccountLevel';
                SocketService.get(url).then(function (resp) {
                    $scope.bankaccountlevel_list = resp.data.bankaccountlevel_list;
                });

                var url = 'api/Mstbuyer/GetBankAccountType';
                SocketService.get(url).then(function (resp) {
                    $scope.bankaccounttype_list = resp.data.bankaccounttype_list;
                });

                var params = {
                    buyer2bank_gid: buyer2bank_gid
                }
                var url = 'api/MstCreditStatusAdd/BankDetailEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtbank_name = resp.data.bank_name;
                    $scope.txtbranch_name = resp.data.branch_name;
                    $scope.txtbank_address = resp.data.bank_address;
                    $scope.txtmicr_code = resp.data.micr_code;
                    $scope.txtifsc_code = resp.data.ifsc_code;
                    $scope.txtbank_accountname = resp.data.bankaccount_name;
                    $scope.cbobankaccountlevel = resp.data.bankaccountlevel_gid;
                    $scope.cbobankaccounttype = resp.data.bankaccounttype_gid;
                    $scope.txtbank_accountno = resp.data.bankaccount_number;
                    $scope.txtconfirmbankaccount_number = resp.data.bankaccount_number;
                    $scope.buyer_gid = resp.data.buyer_gid;
                    $scope.buyer2bank_gid = resp.data.buyer2bank_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

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

                                if (resp.data.result.micr != "" || resp.data.result.micr != null) {
                                    $scope.micrempty = true;
                                }

                            } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                                $scope.ifscvalidation = false;
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
                    if ($scope.txtbank_accountno == $scope.txtconfirmbankaccount_number) {
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
                                $scope.txtbankaccount_name = '';
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });
                    }
                }

                $scope.update_bank = function () {
                    var bankaccountlevelname = $('#bankaccount_level :selected').text();
                    var bankaccounttypename = $('#bankaccount_type :selected').text();
                    var params = {
                        bank_name: $scope.txtbank_name,
                        branch_name: $scope.txtbranch_name,
                        bank_address: $scope.txtbank_address,
                        micr_code: $scope.txtmicr_code,
                        ifsc_code: $scope.txtifsc_code,
                        bankaccount_name: $scope.txtbank_accountname,
                        bankaccountlevel_gid: $scope.cbobankaccountlevel,
                        bankaccountlevel_name: bankaccountlevelname,
                        bankaccounttype_gid: $scope.cbobankaccounttype,
                        bankaccounttype_name: bankaccounttypename,
                        bankaccount_number: $scope.txtbank_accountno,
                        buyer_gid: localStorage.getItem('buyer_gid'),
                        buyer2bank_gid: $scope.buyer2bank_gid,
                        confirmbankaccountnumber: $scope.txtconfirmbankaccount_number
                    }
                    var url = 'api/MstCreditStatusAdd/BankDetailUpdate';
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
                        bank_templist();
                    });

                    $modalInstance.close('closed');
                }
            }
        }

        $scope.bank_delete = function (buyer2bank_gid) {
            var params =
                {
                    buyer2bank_gid: buyer2bank_gid
                }
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

                bank_templist();
            });

        }

      




    }
})();

