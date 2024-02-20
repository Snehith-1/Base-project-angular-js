(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditStatusAddController', MstCreditStatusAddController);

    MstCreditStatusAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstCreditStatusAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditStatusAddController';

        $scope.buyer_gid = localStorage.getItem('buyer_gid');
        $scope.credit_status = localStorage.getItem('credit_status');

        activate();
     
        function activate() {

            $scope.lspage = localStorage.getItem('lspage');

            $scope.view_basicdetails = false;
            $scope.edit_basicdetails = false;

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

            // Calender Popup... //

            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };
            // Calender Popup... //

            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open4 = true;
            };

            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open5 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var url = 'api/MstCreditStatusAdd/GetTempClear';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            var url = 'api/customer/Getconstitution';
            SocketService.get(url).then(function (resp) {
                $scope.constitution_list = resp.data.constitution_list;

            });

            var url = 'api/MstApplication360/AssessmentAgencyList';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagency_list = resp.data.assessmentagency_list;
            });

            var url = 'api/MstApplication360/AssessmentAgencyRatingList';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            });

            var url = 'api/MstApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureauname_list = resp.data.bureauname_list;
            });

            var param = {
                buyer_gid: $scope.buyer_gid
            };

            var url = 'api/MstCreditStatusAdd/BureauScoreView';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bureauscore_list = resp.data.bureauscore_list;
                unlockUI();
            });

            var url = 'api/MstCreditStatusAdd/buyerMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mobileno_list = resp.data.mobileno_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Emailaddress_list = resp.data.email_list;
            });
        
            var url = 'api/MstCreditStatusAdd/buyerBankList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bankdetail_list = resp.data.bank_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.address_list = resp.data.buyeraddress_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.buyergst_list = resp.data.buyergst_list;
            });

            var url = 'api/MstCreditStatusAdd/buyerDetailsEdit';
           
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtbuyer_code = resp.data.buyer_code;
                $scope.txtbuyer_name = resp.data.buyer_name;
                $scope.txtcoi_date = resp.data.editcoi_date;
                $scope.txtbusinessstart_date = resp.data.editbusinessstart_date;
                $scope.txtyearin_business = resp.data.year_business;
                $scope.txtmonthsin_business = resp.data.month_business;
                $scope.txtconstitution = resp.data.constitution_gid;
                $scope.txtcin_regno = resp.data.cin_no;
                $scope.txtpan = resp.data.pan_no;
                $scope.txtfirst_name = resp.data.contactperson_firstname;
                $scope.txtmiddle_name = resp.data.contactperson_middlename;
                $scope.txtlast_name = resp.data.contactperson_lastname;
                $scope.txtcap_limit = resp.data.cap_limit;
                $scope.txtoverall_limit = resp.data.overall_limit;
                $scope.txtbuyer_limit = resp.data.buyer_limit;
                $scope.txtguarantor_limit = resp.data.guarantor_limit;
                $scope.txtborrower_limit = resp.data.borrower_limit;

                if (resp.data.credit_status == 'Completed')
                {
                    $scope.showsubmit = false;
                    $scope.showupdate = true;
                } else {
                    $scope.showsubmit = true;
                    $scope.showupdate = false;
                }

                unlockUI();
            });
        }

        $scope.EditBasic_Details = function () {

            $scope.edit_basicdetails = true;
            $scope.view_basicdetails = true;
        }

        $scope.onchangebusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            var url = 'api/Mstbuyer/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyearin_business = resp.data.year_business;
                $scope.txtmonthsin_business = resp.data.month_business;
            });
        }

        $scope.getPANbasedGST = function () {
            if ($scope.txtpan.length == 10) {

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
                    pan: $scope.txtpan
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
                            pan: $scope.txtpan
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
        function gst_templist() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };
            var url = 'api/MstCreditStatusAdd/GetGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.buyergst_list = resp.data.buyergst_list;
            });
        }


        $scope.update_basicdetails = function () {

            $scope.edit_basicdetails = false;
            $scope.view_basicdetails = false;

            var constitutionName = $('#constitution_Name :selected').text();
            var params = {
                buyer_code: $scope.txtbuyer_code,
                buyer_name: $scope.txtbuyer_name,
                coi_date: $scope.txtcoi_date,
                businessstart_date: $scope.txtbusinessstart_date,
                year_business: $scope.txtyearin_business,
                month_business: $scope.txtmonthsin_business,
                constitution_name: constitutionName,
                constitution_gid: $scope.txtconstitution,
                cin_no: $scope.txtcin_regno,
                pan_no: $scope.txtpan,
                contactperson_firstname: $scope.txtfirst_name,
                contactperson_middlename: $scope.txtmiddle_name,
                contactperson_lastname: $scope.txtlast_name,
                buyer_gid: $scope.buyer_gid
            }
            var url = 'api/MstCreditStatusAdd/buyerDetailsUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
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
                activate();
            });
        }

        $scope.back_basicdetails = function () {

            $scope.edit_basicdetails = false;
            $scope.view_basicdetails = false;
        }
        
        function mobilnolist() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };

            var url = 'api/MstCreditStatusAdd/GetMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mobileno_list = resp.data.mobileno_list;
            });
        }

        function emailaddresslist() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };

            var url = 'api/MstCreditStatusAdd/GetEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Emailaddress_list = resp.data.email_list;
            });
        }


        function bankdetaillist() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };

            var url = 'api/MstCreditStatusAdd/GetBankList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.bankdetail_list = resp.data.bank_list;
            });
        }

         function addresslist() {
            var param = {
                buyer_gid: $scope.buyer_gid
            };

            var url = 'api/MstCreditStatusAdd/GetAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.address_list = resp.data.buyeraddress_list;
            });
         }

         function gst_list() {
             var param = {
                 buyer_gid: $scope.buyer_gid
             };
             var url = 'api/MstCreditStatusAdd/GetGSTList';
             SocketService.getparams(url, param).then(function (resp) {
                 $scope.buyergst_list = resp.data.buyergst_list;
             });
         }

         $scope.add_GSTN = function () {
             var modalInstance = $modal.open({
                 templateUrl: '/addgstdetails.html',
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
                 
                 $scope.ok = function () {
                     $modalInstance.close('closed');
                 };

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

                 $scope.gst_add = function () {
                     var params = {
                         gststate_name: $scope.txtgst_state,
                         gst_no: $scope.txtgst_no,
                         gstregister_status: $scope.rdbgstregistered,
                     }
                     var url = 'api/MstCreditStatusAdd/PostGST';
                     lockUI();
                     SocketService.post(url, params).then(function (resp) {
                         unlockUI();
                         if (resp.data.status == true) {
                             $scope.rdbgstregistered = '';
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
                         $scope.cboGstState = '';
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
             var url = 'api/MstCreditStatusAdd/DeleteGST';
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

                 $scope.update_gstn = function () {
                     var params = {
                         gststate_name: $scope.txteditgst_state,
                         gst_no: $scope.txteditgst_number,
                         gstregister_status: $scope.rdbgstregistered,
                         buyer_gid: $scope.buyer_gid,
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
                             gst_list();
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

        $scope.add_mobileno = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addmobileno.html',
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

                $scope.submit_mobileno = function () {

                      var params = {
                          mobile_no: $scope.txtmobile_no,
                          primary_mobileno: $scope.rdbmobile_no,
                          whatsapp_mobileno: $scope.rdbwhatsapp_no,
                          buyer_gid : $scope.buyer_gid
                     }
                      var url = 'api/MstCreditStatusAdd/MobileNumberAdd';
                     lockUI();
                     SocketService.post(url, params).then(function (resp) {
                         unlockUI();
                         if (resp.data.status == true) {
                             Notify.alert(resp.data.message, {
                                 status: 'success',
                                 pos: 'top-center',
                                 timeout: 3000
                             });
                             mobilnolist();
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

                    $scope.txtmobile_noedit = resp.data.mobile_no;
                    $scope.rdbmobile_no = resp.data.primary_mobileno;
                    $scope.rdbwhatsapp_no = resp.data.whatsapp_mobileno;
                    $scope.buyer2mobileno_gid = resp.data.buyer2mobileno_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_mobileno = function () {

                     var params = {
                         mobile_no: $scope.txtmobile_noedit,
                         primary_mobileno: $scope.rdbmobile_no,
                         whatsapp_mobileno: $scope.rdbwhatsapp_no,
                         buyer2mobileno_gid: $scope.buyer2mobileno_gid,
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
                             mobilnolist();
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

        $scope.mobileno_delete = function (buyer2mobileno_gid) {
               var params =
                  {
                      buyer2mobileno_gid: buyer2mobileno_gid
                  }
               var url = 'api/MstCreditStatusAdd/DeleteMobileNo';
              lockUI();
              SocketService.getparams(url, params).then(function (resp) {
                  unlockUI();
                  if (resp.data.status == true) {
  
                      Notify.alert(resp.data.message, {
                          status: 'success',
                          pos: 'top-center',
                          timeout: 3000
                      });
                      mobilnolist();
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

        $scope.add_emailaddress = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addemailaddress.html',
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

                $scope.submit_emailaddress = function () {

                    var params = {
                        email_address: $scope.txtemail_address,
                        primary_emailaddress: $scope.rdbemailaddress,
                        buyer_gid: $scope.buyer_gid
                    }
                    var url = 'api/MstCreditStatusAdd/EmailAddressAdd';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            emailaddresslist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');

                }
            }
        }

        $scope.edit_emailaddress = function (buyer2emailaddress_gid) {
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

                    $scope.txtemail_address = resp.data.email_address;
                    $scope.rdbemailaddress = resp.data.primary_emailaddress;
                    $scope.buyer2emailaddress_gid = resp.data.buyer2emailaddress_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_emailaddress = function () {

                    var params = {
                        email_address: $scope.txtemail_address,
                        primary_emailaddress: $scope.rdbemailaddress,
                        buyer2emailaddress_gid: $scope.buyer2emailaddress_gid,
                        buyer_gid: $scope.buyer_gid,
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
                            emailaddresslist();
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

        $scope.delete_emailaddress = function (buyer2emailaddress_gid) {
               var params =
                  {
                      buyer2emailaddress_gid: buyer2emailaddress_gid
                  }
               var url = 'api/MstCreditStatusAdd/DeleteEmailAddress';
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
  
                  emailaddresslist();
              }); 

        }

        $scope.add_bankdetails = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addbankdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstApplication360/BankAccountLevelList';
                SocketService.get(url).then(function (resp) {
                    $scope.bankaccountlevel_list = resp.data.bankaccountlevel_list;
                });

                var url = 'api/Mstbuyer/GetBankAccountType';
                SocketService.get(url).then(function (resp) {
                    $scope.bankaccounttype_list = resp.data.bankaccounttype_list;
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
                                $scope.txtbank_accountname = resp.data.result.accountName;

                            } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                                $scope.bankaccvalidation = false;
                                Notify.alert('Bank Account is not verified..!', 'warning');
                                $scope.txtbank_accountname = '';
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });
                    }
                }

                $scope.add_bankdetails = function () {

                    var params = {
                        bank_name: $scope.txtbank_name,
                        branch_name: $scope.txtbranch_name,
                        ifsc_code: $scope.txtifsc_code,
                        bankaccount_name: $scope.txtbank_accountname,
                        bankaccountlevel_gid: $scope.cbobankaccountlevel.bankaccountlevel_gid,
                        bankaccountlevel_name: $scope.cbobankaccountlevel.bankaccountlevel_name,
                        bankaccount_number: $scope.txtbank_accountno,
                        buyer_gid: $scope.buyer_gid,
                        confirmbankaccountnumber: $scope.txtconfirmbankaccount_number,
                        micr_code: $scope.txtmicr_code,
                        bank_address: $scope.txtbank_address,
                        bankaccounttype_gid: $scope.cbobankaccounttype.bankaccounttype_gid,
                        bankaccounttype_name: $scope.cbobankaccounttype.bankaccounttype_name,
                    }
                    var url = 'api/MstCreditStatusAdd/BankDetailsAdd';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            bankdetaillist();
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

        $scope.edit_bankdetails = function (buyer2bank_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbankdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstApplication360/BankAccountLevelList';
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
                    $scope.txtifsc_code = resp.data.ifsc_code;
                    $scope.txtbank_accountname = resp.data.bankaccount_name;
                    $scope.bankaccountlevel_GID = resp.data.bankaccountlevel_gid;
                    $scope.bankaccount_level = resp.data.bankaccountlevel_name;
                    $scope.txtbank_accountno = resp.data.bankaccount_number;
                    $scope.buyer_gid = resp.data.buyer_gid;
                    $scope.buyer2bank_gid = resp.data.buyer2bank_gid;
                    $scope.txtmicr_code = resp.data.micr_code;
                    $scope.txtbank_address = resp.data.bank_address;
                    $scope.cbobankaccounttype = resp.data.bankaccounttype_gid;
                    $scope.txtconfirmbankaccount_number = resp.data.confirmbankaccountnumber;
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
                                $scope.txtbank_accountname = resp.data.result.accountName;

                            } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                                $scope.bankaccvalidation = false;
                                Notify.alert('Bank Account is not verified..!', 'warning');
                                $scope.txtbank_accountname = '';
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });
                    }
                }

                $scope.update_bankdetails = function () {
                    var bankaccountlevelname = $('#bankaccount_level :selected').text();
                    var bankaccounttypename = $('#bankaccounttype_name :selected').text();
                    var params = {
                        bank_name: $scope.txtbank_name,
                        branch_name: $scope.txtbranch_name,
                        ifsc_code: $scope.txtifsc_code,
                        bankaccount_name: $scope.txtbank_accountname,
                        bankaccountlevel_gid: $scope.bankaccountlevel_GID,
                        bankaccountlevel_name: bankaccountlevelname,
                        bankaccount_number: $scope.txtbank_accountno,
                        buyer_gid: $scope.buyer_gid,
                        buyer2bank_gid: $scope.buyer2bank_gid,
                        confirmbankaccountnumber: $scope.txtconfirmbankaccount_number,
                        micr_code: $scope.txtmicr_code,
                        bank_address: $scope.txtbank_address,
                        bankaccounttype_gid: $scope.cbobankaccounttype,
                        bankaccounttype_name: bankaccounttypename,
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
                            bankdetaillist();
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

        $scope.delete_bankdetails = function (buyer2bank_gid) {
            var params =
                 {
                     buyer2bank_gid: buyer2bank_gid
                 }
            var url = 'api/MstCreditStatusAdd/DeleteBankDetail';
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
                bankdetaillist();
            });
        }

        $scope.add_addressdetails = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addaddressdetails.html',
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
                        address_typegid: $scope.cboaddresstype.address_gid,
                        address_type: $scope.cboaddresstype.address_type,
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
                        longitude: $scope.txtlongitude

                    }
                    var url = 'api/MstCreditStatusAdd/AddressDetailAdd';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            addresslist();
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

        $scope.edit_addressdetails = function (buyer2address_gid) {
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

                    $scope.cboaddresstype_GID = resp.data.address_typegid;
                    $scope.address_type = resp.data.address_type;
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
                    var addresstype = $('#address_type :selected').text();

                    var params = {
                        address_typegid: $scope.cboaddresstype_GID,
                        address_type: addresstype,
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
                            addresslist();
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

        $scope.delete_addressdetails = function (buyer2address_gid) {
               var params =
                  {
                      buyer2address_gid: buyer2address_gid
                  }
               var url = 'api/MstCreditStatusAdd/DeleteAddressDetail';
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
  
                  addresslist();
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

        $scope.BureauDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                frm.append('project_flag', "Default");
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i])
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                }
                var url = 'api/MstCreditStatusAdd/BureauDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    $scope.upload_list = resp.data.upload_list;
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
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
            $scope.upload_list='';
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

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.uploaddocumentcancel = function (tmp_documentGid) {
            lockUI();
            var params = {
                tmp_documentGid: tmp_documentGid
            }
            var url = 'api/MstCreditStatusAdd/TmpDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
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
                unlockUI();
            });
        }

        $scope.txtlastyear_turnoverchange = function () {
            var input = document.getElementById('lastyear_turnover').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtlastyear_turnover = "";
            }
            else {
                $scope.txtlastyear_turnover = output;
                document.getElementById('words_totalamount').innerHTML = lswords_totalamount;
            }
        }

// Numeric to Word - Indian Standard...//

      
   
        $scope.borrower_limitChange = function () {
            var input = document.getElementById('borrower_limit').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_borroweramount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtborrower_limit = "";
            }
            else {
                $scope.txtborrower_limit = output;
                document.getElementById('words_borroweramount').innerHTML = lswords_borroweramount;
            }
        }

        $scope.guarantor_limitChange = function () {
            var input = document.getElementById('guarantor_limit').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_guarantoramount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtguarantor_limit = "";
            }
            else {
                $scope.txtguarantor_limit = output;
                document.getElementById('words_guarantoramount').innerHTML = lswords_guarantoramount;
            }
        }

        $scope.buyer_limitChange = function () {
            var input = document.getElementById('buyer_limit').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_buyeramount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtbuyer_limit = "";
            }
            else {
                $scope.txtbuyer_limit = output;
                document.getElementById('words_buyeramount').innerHTML = lswords_buyeramount;
            }
        }

        $scope.overall_limitChange = function () {
            var input = document.getElementById('overall_limit').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_overallamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtoverall_limit = "";
            }
            else {
                $scope.txtoverall_limit = output;
                document.getElementById('words_overallamount').innerHTML = lswords_overallamount;
            }
        }

        $scope.cap_limitChange = function () {
            var input = document.getElementById('cap_limit').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_capamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtcap_limit = "";
            }
            else {
                $scope.txtcap_limit = output;
                document.getElementById('words_capamount').innerHTML = lswords_capamount;
            }
        }

  // Bureau Score Add

        $scope.bureauscoredtl_add = function () {

            var params =
              {
                  bureauname_name: $scope.cbobureauname.bureauname_name,
                  bureauname_gid: $scope.cbobureauname.bureauname_gid,
                  bureau_score: $scope.txtbureau_score,
                  bureaugenerated_date: $scope.txtbureaugenerated_date,
                  lastyear_turnover: $scope.txtlastyear_turnover,
                  assessmentagency_name: $scope.cboAssessment_Agency.assessmentagency_name,
                  assessmentagency_gid: $scope.cboAssessment_Agency.assessmentagency_gid,
                  creditrating_name: $scope.cbocredit_rating.assessmentagencyrating_name,
                  creditrating_gid: $scope.cbocredit_rating.assessmentagencyrating_gid,
                  creditrating_date: $scope.txtcreditrating_date,
                  creditratingexpiry_date: $scope.txtcreditratingexpiry_date,
                  buyer_gid: $scope.buyer_gid,
              }
            var url = 'api/MstCreditStatusAdd/BureauScoreAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.cbobureauname = "";
                    $scope.txtbureau_score = "";
                    $scope.txtbureaugenerated_date = "";
                    $scope.txtlastyear_turnover = "";
                    $scope.cboAssessment_Agency = "";
                    $scope.cbocredit_rating = "";
                    $scope.txtcreditrating_date = "";
                    $scope.txtcreditratingexpiry_date = "";
                    $scope.words_borroweramount = "";
                    $scope.upload_list="";

                    unlockUI();
                    $scope.bureauscore_list = resp.data.bureauscore_list;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
            $scope.words_totalamount = "";
        }

        $scope.delete_bureauscore = function (bureauscoreadd_GID) {
            var params =
               {
                   bureauscoreadd_GID: bureauscoreadd_GID
               }
            var url = 'api/MstCreditStatusAdd/BureauScoreDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
            });
        }

        $scope.Back = function () {
            if ($scope.lspage == "CreditStatusApprovedBuyer") {
                $state.go('app.MstCreditStatusApprovedBuyer');
            }
            else if ($scope.lspage == "CreditStatusNonApprovedBuyer") {
                $state.go('app.MstCreditStatusNonApprovedBuyer');
            }
            else {
                $state.go('app.MstCreditStatusSummary');
            }
        }

        $scope.add_Update = function () {
            var params = {
                borrower_limit: $scope.txtborrower_limit,
                guarantor_limit: $scope.txtguarantor_limit,
                buyer_limit: $scope.txtbuyer_limit,
                overall_limit: $scope.txtoverall_limit,
                cap_limit: $scope.txtcap_limit,
                buyer_gid: $scope.buyer_gid
            }
            var url = 'api/MstCreditStatusAdd/UpdateCreditStatus';
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
            });
            if ($scope.lspage == "CreditStatusApprovedBuyer") {
                $state.go('app.MstCreditStatusApprovedBuyer');
            }
            else if ($scope.lspage == "CreditStatusNonApprovedBuyer") {
                $state.go('app.MstCreditStatusNonApprovedBuyer');
            }
            else {
                $state.go('app.MstCreditStatusSummary');
            }
        }

        $scope.add_Submit = function () {
            var params = {
                borrower_limit: $scope.txtborrower_limit,
                guarantor_limit: $scope.txtguarantor_limit,
                buyer_limit: $scope.txtbuyer_limit,
                overall_limit: $scope.txtoverall_limit,
                cap_limit: $scope.txtcap_limit,
                buyer_gid: $scope.buyer_gid
            }
            var url = 'api/MstCreditStatusAdd/SubmitCreditStatus';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if ($scope.lspage == "CreditStatusApprovedBuyer") {
                        $state.go('app.MstCreditStatusApprovedBuyer');
                    }
                    else if ($scope.lspage == "CreditStatusNonApprovedBuyer") {
                        $state.go('app.MstCreditStatusNonApprovedBuyer');
                    }
                    else {
                        $state.go('app.MstCreditStatusSummary');
                    }
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if ($scope.lspage == "CreditStatusApprovedBuyer") {
                        $state.go('app.MstCreditStatusApprovedBuyer');
                    }
                    else if ($scope.lspage == "CreditStatusNonApprovedBuyer") {
                        $state.go('app.MstCreditStatusNonApprovedBuyer');
                    }
                    else {
                        $state.go('app.MstCreditStatusSummary');
                    }
                    activate();
                }
            });
        }

        $scope.Save = function () {
            var params = {
                borrower_limit: $scope.txtborrower_limit,
                guarantor_limit: $scope.txtguarantor_limit,
                buyer_limit: $scope.txtbuyer_limit,
                overall_limit: $scope.txtoverall_limit,
                cap_limit: $scope.txtcap_limit,
                buyer_gid: $scope.buyer_gid
            }
            var url = 'api/MstCreditStatusAdd/SaveAsDraftbuyer';
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
            });
            if ($scope.lspage == "CreditStatusApprovedBuyer") {
                $state.go('app.MstCreditStatusApprovedBuyer');
            }
            else if ($scope.lspage == "CreditStatusNonApprovedBuyer") {
                $state.go('app.MstCreditStatusNonApprovedBuyer');
            }
            else {
                $state.go('app.MstCreditStatusSummary');
            }
        }

     $scope.uploadeddoc_bureauscore = function (bureauscoreadd_GID) {
        var modalInstance = $modal.open({
            templateUrl: '/bureaudocuments.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            var params =
               {
                bureauscoreadd_GID: bureauscoreadd_GID
               }
            var url = 'api/MstCreditStatusAdd/BureauDocList';
           lockUI();
           SocketService.getparams(url, params).then(function (resp) {
               unlockUI();
               if (resp.data.status == true) {
                $scope.bureaudoc_list= resp.data.upload_list;
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
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
            $scope.download_bureaudoc = function (val1, val2) {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
          
        }
      
    }

    

    }
})();

