(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerMasterViewController', FndMstCustomerMasterViewController);

    FndMstCustomerMasterViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndMstCustomerMasterViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerMasterViewController';
        $scope.customer_gid = cmnfunctionService.decryptURL($location.search().hash).lscustomer_gid;
        
      
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

           

            //var url = 'api/FndMstCustomerMasterAdd/GetBankAccountLevel';
            //SocketService.get(url).then(function (resp) {
            //    $scope.bankaccountlevel_list = resp.data.bankaccountlevel_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/GetBankAccountType';
            //SocketService.get(url).then(function (resp) {
            //    $scope.bankaccounttype_list = resp.data.bankaccounttype_list;
            //});

            //var url = 'api/FndMstCustomerMasterAdd/Getconstitution';
            //SocketService.get(url).then(function (resp) {
            //    $scope.constitution_list = resp.data.constitution_list;
            //});

            //var url = 'api/FndMstCustomerMasterAdd/Getassessmentagency';
            //SocketService.get(url).then(function (resp) {
            //    $scope.assessmentagency_list = resp.data.assessmentagency_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/Getassessmentagencyrating';
            //SocketService.get(url).then(function (resp) {
            //    $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/Getamlcategory';
            //SocketService.get(url).then(function (resp) {
            //    $scope.amlcategory_list = resp.data.amlcategory_list;
            //});
            //var url = 'api/FndMstCustomerMasterAdd/Getbusinesscategory';
            //SocketService.get(url).then(function (resp) {
            //    $scope.businesscategory_list = resp.data.businesscategory_list;
            //});

            //var url = 'api/FndMstCustomerMasterAdd/state';
            //SocketService.get(url).then(function (resp) {
            //    $scope.state_list = resp.data.state_list;
            //});



         
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
                    $scope.mobileno_list = resp.data.mobileno_list;
                });
  



                var param = {
                    customer_gid: $scope.customer_gid
                };
                var url = 'api/FndMstCustomerMasterAdd/customerEmailAddressList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.email_list = resp.data.email_list;

                });
            

         
                var param = {
                    customer_gid: $scope.customer_gid

                };
                var url = 'api/FndMstCustomerMasterAdd/customerAddressList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.address_list = resp.data.customeraddress_list;
                });
            


            var params = {
                customer_gid: $scope.customer_gid
            }

            var url = 'api/FndMstCustomerMasterAdd/customerDetailsEdit';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_code = resp.data.customer_code;
                $scope.customer_name = resp.data.customer_name;
                $scope.businessstart_date = resp.data.businessstart_date;
                $scope.year_business = resp.data.year_business;
                $scope.month_business = resp.data.month_business;
                $scope.constitution_name = resp.data.constitution_name;
                $scope.assessmentagency_name = resp.data.assessmentagency_name;
                $scope.assessmentagencyrating_name = resp.data.assessmentagencyrating_name;
                $scope.rating_date = resp.data.rating_date;
                $scope.amlcategory_name = resp.data.amlcategory_name;
                $scope.cin_no = resp.data.cin_no;
                $scope.pan_no = resp.data.pan_no;
                $scope.businesscategory_name = resp.data.businesscategory_name;
                $scope.remarks = resp.data.remarks;
                $scope.msme_registration = resp.data.msme_registration;
                $scope.contactperson_fn = resp.data.contactperson_fn;
                $scope.contactperson_mn = resp.data.contactperson_mn;
                $scope.contactperson_ln = resp.data.contactperson_ln;
                $scope.individualproof_name = resp.data.individualproof_name;
                $scope.designation_type = resp.data.designation_type;

                //if (resp.data.credit_status == 'Pending') {
                //    $scope.showsubmit = false;
                //    $scope.showupdate = true;
                //} else if (resp.data.credit_status == 'Completed') {
                //    $scope.showsubmit = false;
                //    $scope.showupdate = true;
                //}
                //else {
                //    $scope.showsubmit = true;
                //    $scope.showupdate = false;
                //}

                unlockUI();
            });

        }

        var params = {
            customer_gid: $scope.customer_gid
        }
            var url = 'api/FndMstCustomerMasterAdd/GetChequeSummaryView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
            });

        
        //var params={
        //    customer_gid: $scope.customer_gid
        //}
        //var url = 'api/FndMstCustomerMasterAdd/ChequeDetailsEdit';
        //SocketService.getparams(url, params).then(function (resp) {
          
        //    $scope.accountholder_name = resp.data.accountholder_name;
        //    $scope.account_number = resp.data.account_number;
        //    $scope.bank_name = resp.data.bank_name;
        //    $scope.cheque_no = resp.data.cheque_no;
        //    $scope.ifsc_code = resp.data.ifsc_code;
        //    $scope.micr = resp.data.micr;
        //    $scope.branch_address = resp.data.branch_address;
        //    $scope.branch_name = resp.data.branch_name;
        //    $scope.city = resp.data.city;
        //    $scope.district = resp.data.district;
        //    $scope.state = resp.data.state;
        //    $scope.mergedbankingentity_name = resp.data.mergedbankingentity_name;
        //    $scope.special_condition = resp.data.special_condition;
        //    $scope.general_remarks = resp.data.general_remarks;
        //    $scope.cts_enabled = resp.data.cts_enabled;
        //    $scope.cheque_type = resp.data.cheque_type;
        //    $scope.date_chequetype = resp.data.date_chequetype;
        //    $scope.date_chequepresentation = resp.data.date_chequepresentation;
        //    $scope.status_chequepresentation = resp.data.status_chequepresentation;
        //    $scope.date_chequeclearance = resp.data.date_chequeclearance;
        //    $scope.status_chequeclearance = resp.data.status_chequeclearance;
           
        //});
    
            //var url = 'api/FndMstCustomerMasterAdd/GetChequeDetails';
            //SocketService.get(url).then(function (resp) {
            //    $scope.cheque_list = resp.data.cheque_list;
            //});
       
        $scope.onchangebusinessstartdate = function () {
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

        $scope.Back = function () {
            $state.go('app.FndMstCustomerMaster');
        }

        $scope.edit_Submit = function () {
            $state.go('app.FndMstCustomerMaster');
        }



   
        //function gst_list() {
        //    var url = 'api/FndMstCustomerMasterAdd/GetGSTList';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.customergst_list = resp.data.customergst_list;

        //    });
        //}




     

     

        //function emailaddress_list() {
        //    var url = 'api/FndMstCustomerMasterAdd/GetEmailAddressList';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.customeremailaddress_list = resp.data.customeremailaddress_list;

        //    });
        //}



      

        //function address_list() {
        //    var url = 'api/FndMstCustomerMasterAdd/GetAddressList';
        //    SocketService.get(url).then(function (resp) {
        //        $scope.customeraddress_list = resp.data.customeraddress_list;

        //    });
        //}

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

       
        function cheque_list() {
            var url = 'api/UdcManagement/GetChequeSummary';
            SocketService.get(url).then(function (resp) {
                $scope.cheque_list = resp.data.cheque_list;
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





    }
})();

